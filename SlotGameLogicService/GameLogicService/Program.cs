using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using Log;

namespace GameLogicService
{
    using Associative = Dictionary<string, string>;
    using GameId = String;
    using UserId = String;
    using Json = String;

    public class GameLogic
    {
        TextWriter writer = Console.Out;

        /// <summary>
        /// Request Response Log Function
        /// </summary>
        Action<string> ReqResLog;

        Dictionary<Tuple<GameId, UserId>, IMachine> machines =
            new Dictionary<Tuple<GameId, UserId>, IMachine>();

        public static String settingUrl;

        static void Main(string[] args)
        {
            new GameLogic().Run(args);
        }

        void Run(string[] args)
        {
            settingUrl = args[0];

            if (args.Contains("debug"))
            {
                ReqResLog = (msg) =>
                {
                    Logger.Info(msg);
                };
            } else
            {
                ReqResLog = (msg) => {};
            }

            var listener = new HttpListener();

            try
            {
                listener.Prefixes.Add("http://*:9876/");
                listener.Start();
            }
            catch (Exception e)
            {
                Logger.Error("[ERROR]GAMELOGIC SERVER RUN FAILED");
                Logger.Error(e);
                return;
            }

            Logger.Info("[INFO]GAMELOGIC SERVER RUN PORT:9876");

            while (true)
            {
                var context = listener.GetContext();

                // マルチスレッド対応
                ThreadPool.QueueUserWorkItem(o => HandleRequest(context));
            }
        }

        void HandleRequest(HttpListenerContext context)
        {
            var pathTable = new[]
            {
                new { path = "/config", response = (Func<Associative, Json>) ConfigResponse },
                new { path = "/init", response = (Func<Associative, Json>)InitResponse },
                new { path = "/play", response = (Func<Associative, Json>)PlayResponse },
                new { path = "/collect", response = (Func<Associative, Json>)CollectResponse },
            };

            var req = context.Request;
            var res = context.Response;
            var url = req.Url;
            var localPath = url.LocalPath;
            var reqparams = null as Associative;

            if (req.HttpMethod == "POST")
            {
                if (!req.HasEntityBody)
                {
                    ReqResLog($"REQ => {url} : no param.");

                    reqparams = new Associative();
                }
                else
                {
                    using (var body = req.InputStream)
                    {
                        using (var reader = new StreamReader(body, req.ContentEncoding))
                        {
                            var param = reader.ReadToEnd();

                            ReqResLog($"REQ => {url} : {param}");

                            reqparams = PostBody2KeyValues(param);
                        }
                    }
                }
            }
            else if (req.HttpMethod == "GET")
            {
                reqparams = Query2KeyValues(url.Query);
            }
            else
            {
                throw new Exception("[ERROR]NOT REQUEST POST/GET METHOD");
            }

            var preTime = DateTime.Now;

            var responseString = pathTable.Where(pt => pt.path == localPath)
                                          .Select(pt => pt.response(reqparams))
                                          .FirstOrDefault() ?? defaultResponse(reqparams);


            ReqResLog($"RES <= {responseString}");

            var timeSpan = DateTime.Now - preTime;

            //writer.Log($"{localPath} : takes {timeSpan}");

            var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

            res.ContentLength64 = buffer.Length;
            try
            {
                res.OutputStream.Write(buffer, 0, buffer.Length);
            }
            catch(Exception)
            {
                Logger.Error("Stream Crashed");
            }

            res.Close();
        }

        /// <summary>
        /// POST Body => Associative Array
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Associative PostBody2KeyValues(string body)
        {
            var result = new Associative();

            try
            {
                body.Split('&')
                    .Select(str => str.Split('='))
                    .Select(strs => new KeyValuePair<string, string>(strs[0], strs[1]))
                    .ToList()
                    .ForEach(kv => result.Add(kv.Key, kv.Value));
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return result;
        }

        /// <summary>
        /// URL Query => Associative Array
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Associative Query2KeyValues(string query)
        {
            var result = new Associative();

            try
            {
                query.Split('?')
                     .Skip(1) // remove head '?'
                     .Select(str => str.Split('='))
                     .Select(strs => new KeyValuePair<string, string>(strs[0], strs[1]))
                     .ToList()
                     .ForEach(kv => result.Add(kv.Key, kv.Value));
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return result;
        }

        Json defaultResponse(Associative param)
        {
            param.Add("result", "error".DQ());

            return "{" +
                   string.Join(",", param.Select(e => e.Key.DQ() + ":" + e.Value)) +
                   "}";
        }

        /// <summary>
        /// Create Machine Instance
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Json ConfigResponse(Associative param)
        {
            // setting expect 0%2C0%2C0%2C0%2C20%2C30%2C50

            var setting = null as string;
            var gameId = null as GameId;
            var userId = null as UserId;
            var settingValue = 6;

            try
            {
                setting = param["setting"];
                gameId = param["gameId"];
                userId = param["userId"];

                var delimiter = new string[]{ "%2C", "%252C" };
                var parts = setting.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                var nums = parts.Select(s => s.ParseInt());
                var ary = nums.ToArray();
                settingValue = Setting.Get(ary);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return defaultResponse(param);
            }

            var machine = null as IMachine;
            var key = Tuple.Create(gameId, userId);

            if (machines.ContainsKey(key))
            {
                machines.Remove(key);
                Logger.Info($"[INFO] DESTROY MACHINE GAMEID:{gameId} USERID:{userId}");
            }

            machine = MachineFactory.Create(gameId, userId, settingValue);
            if (machine == null)
            {
                Logger.Error($"[ERROR] CREATE MACHINE FAILD GAMEID:{gameId} USERID:{userId}");
                return defaultResponse(param);
            }

            machines.Add(key, machine);

            Logger.Info($"[INFO] CREATE MACHINE GAMEID:{gameId} USERID:{userId}");

            var table = machine.Config(param);

            var res = "{" +
                      string.Join(",", table.Select(e => e.Key.DQ() + ":" + e.Value)) +
                      "}";

            return res;
        }

        Json InitResponse(Associative param)
        {
            var gameId = null as GameId;
            var userId = null as UserId;

            try
            {
                gameId = param["gameId"];
                userId = param["userId"];
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return defaultResponse(param);
            }

            var machine = null as IMachine;
            var key = Tuple.Create(gameId, userId);

            if (!machines.ContainsKey(key))
            {
                return defaultResponse(param);
            }

            machine = machines[key];

            var table = machine.Init(param);

            var res = "{" +
                      string.Join(",", table.Select(e => e.Key.DQ() + ":" + e.Value)) +
                      "}";

            return res;
        }

        Json PlayResponse(Associative param)
        {
            var gameId = null as GameId;
            var userId = null as UserId;

            try
            {
                gameId = param["gameId"];
                userId = param["userId"];
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return defaultResponse(param);
            }

            var machine = null as IMachine;
            var key = Tuple.Create(gameId, userId);

            if (!machines.ContainsKey(key))
            {
                return defaultResponse(param);
            }

            machine = machines[key];

            var table = machine.Play(param);

            var res = "{" +
                      string.Join(",", table.Select(e => e.Key.DQ() + ":" + e.Value)) +
                      "}";

            return res;
        }

        Json CollectResponse(Associative param)
        {
            var gameId = null as GameId;
            var userId = null as UserId;

            try
            {
                gameId = param["gameId"];
                userId = param["userId"];
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return defaultResponse(param);
            }

            var machine = null as IMachine;
            var key = Tuple.Create(gameId, userId);

            if (!machines.ContainsKey(key))
            {
                return defaultResponse(param);
            }

            machine = machines[key];

            var table = machine.Collect(param);

            var res = "{" +
                      string.Join(",", table.Select(e => e.Key.DQ() + ":" + e.Value)) +
                      "}";

            return res;
        }
    }

}

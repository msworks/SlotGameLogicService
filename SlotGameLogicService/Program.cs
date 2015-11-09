using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;

namespace GameLogicService
{
    using Associative = Dictionary<string, string>;
    using GameId = String;
    using UserId = String;
    using Json = String;

    class GameLogic
    {
        TextWriter writer = Console.Out;

        Dictionary<Tuple<GameId, UserId>, IMachine> machines =
            new Dictionary<Tuple<GameId, UserId>, IMachine>();

        static void Main(string[] args)
        {
            new GameLogic().Run();
        }

        void Run()
        {
            var listener = new HttpListener();

            try
            {
                listener.Prefixes.Add("http://*:9876/");
                listener.Start();
            }
            catch (Exception e)
            {
                writer.WriteLine("[ERROR]GAMELOGIC SERVER RUN FAILED");
                writer.WriteLine(e);
                return;
            }

            writer.WriteLine("[INFO]GAMELOGIC SERVER RUN PORT:9876");

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
                    reqparams = new Associative();
                }
                else
                {
                    using (var body = req.InputStream)
                    {
                        using (var reader = new StreamReader(body, req.ContentEncoding))
                        {
                            var param = reader.ReadToEnd();
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

            var responseString = pathTable.Where(pt => pt.path == localPath)
                                          .Select(pt => pt.response(reqparams))
                                          .FirstOrDefault() ?? defaultResponse(reqparams);

            var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

            res.ContentLength64 = buffer.Length;
            res.OutputStream.Write(buffer, 0, buffer.Length);
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
                writer.WriteLine(ex);
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
                writer.WriteLine(ex);
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
            var gameId = null as GameId;
            var userId = null as UserId;

            try
            {
                gameId = param["gameId"];
                userId = param["userId"];
            }
            catch (Exception ex)
            {
                writer.WriteLine(ex);
                return defaultResponse(param);
            }

            var machine = null as IMachine;
            var key = Tuple.Create(gameId, userId);

            if (machines.ContainsKey(key))
            {
                machines.Remove(key);
                writer.WriteLine("[INFO] DESTROY MACHINE GAMEID:" + gameId + " USERID:" + userId);
            }

            machine = MachineFactory.Create(gameId, userId);
            if (machine == null)
            {
                writer.WriteLine("[ERROR] CREATE MACHINE FAILD GAMEID:" + gameId + " USERID:" + userId);
                return defaultResponse(param);
            }

            machines.Add(key, machine);

            writer.WriteLine("[INFO] CREATE MACHINE GAMEID:" + gameId + " USERID:" + userId);

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
                writer.WriteLine(ex);
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
                writer.WriteLine(ex);
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
                writer.WriteLine(ex);
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

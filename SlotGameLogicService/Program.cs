using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;

namespace SlotGameLogicService
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

            var pathTable = new[]
            {
                new { path = "/config", response = (Func<Associative, Json>) configResponse },
                new { path = "/init", response = (Func<Associative, Json>)initResponse },
                new { path = "/play", response = (Func<Associative, Json>)playResponse },
                new { path = "/correct", response = (Func<Associative, Json>)correctResponse },
            };

            while (true)
            {
                var context = listener.GetContext();
                var req = context.Request;
                var res = context.Response;

                var url = req.Url;
                var localPath = url.LocalPath;
                var reqparams = Query2KeyValues(url.Query);

                var responseString = pathTable.Where(pt => pt.path == localPath)
                                              .Select(pt => pt.response(reqparams))
                                              .FirstOrDefault() ?? defaultResponse(reqparams);

                var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

                res.ContentLength64 = buffer.Length;
                res.OutputStream.Write(buffer, 0, buffer.Length);
                res.Close();
            }
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
        Json configResponse(Associative param)
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
            }

            machine = MachineFactory.Create(gameId);
            machines.Add(key, machine);

            var table = new[]
            {
                    new { key = "setting", value = "1" },
                    new { key = "reelleft", value = "0" },
                    new { key = "reelcenter", value = "0" },
                    new { key = "reelright", value = "0" },
                    new { key = "seed", value = "0" },
                };

            var res = "{" +
                      string.Join(",", table.Select(e => e.key.DQ() + ":" + e.value)) +
                      "}";

            return res;
        }

        Json initResponse(Associative param)
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

            var res = "{}";

            return res;
        }

        Json playResponse(Associative param)
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

            var table = new[]
            {
                    new { key = "yaku", value = "1" },
                    new { key = "route", value = "2" },
                };

            var res = "{" +
                      string.Join(",", table.Select(e => e.key.DQ() + ":" + e.value)) +
                      "}";

            return res;
        }

        Json correctResponse(Associative param)
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

            var table = new[]
            {
                    new { key = "result", value = "WIN".DQ() },
                };

            var res = "{" +
                      string.Join(",", table.Select(e => e.key.DQ() + ":" + e.value)) +
                      "}";

            return res;
        }

    }
}

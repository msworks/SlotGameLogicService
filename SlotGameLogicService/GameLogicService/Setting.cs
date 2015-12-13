using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using static System.Linq.Enumerable;

namespace GameLogicService
{
    using GameId = String;

    /// <summary>
    /// 設定値を読み出すクラス
    /// </summary>
    class Setting
    {
        static Random rnd = new Random();

        static public int Get(GameId gameId)
        {
            return 6;
        }

        //static public int Get(GameId gameId)
        //{
        //    var url = "https://raw.githubusercontent.com/msworks/Setting/master/Setting.txt";

        //    var text = "";

        //    try
        //    {
        //        var request = WebRequest.Create(url);
        //        var response = request.GetResponse();
        //        var stream = response.GetResponseStream();
        //        var reader = new StreamReader(stream, System.Text.Encoding.UTF8);
        //        text = reader.ReadToEnd();
        //        stream.Close();
        //        reader.Close();
        //        response.Close();
        //    }
        //    catch (Exception)
        //    {
        //        return 6;
        //    }

        //    var games = new[]
        //    {
        //        "Oomatsuri",
        //        "OceanStories",
        //    };

        //    var gameId_gameName = new Dictionary<GameId, string>()
        //    {
        //        {"1", "OceanStories"},
        //        {"2", "Oomatsuri" },
        //    };

        //    var settings = new[]
        //    {
        //        "Setting0",
        //        "Setting1",
        //        "Setting2",
        //        "Setting3",
        //        "Setting4",
        //        "Setting5",
        //        "Setting6",
        //    };

        //    var jObject = JObject.Parse(text);

        //    var table = from game in games
        //                from setting in settings
        //                select new
        //                {
        //                    game,
        //                    setting = setting.Substring(7, 1).ParseInt(),
        //                    value = jObject[game][setting].ToString().DropPercent().ParseInt()
        //                };

        //    var t2 = table.Where(t => t.game == gameId_gameName[gameId]);
        //    var ary = table.Select(t => Range(0, t.value).Select(v => t.setting)).Flatten().ToArray();
        //    var len = ary.Count();
        //    var value = ary[rnd.Next(0, len - 1)];

        //    return value;
        //}
    }

    static class stringExtension
    {
        public static string DropPercent(this string source)
        {
            var length = source.Length;
            return source.Substring(0, length - 1);
        }
    }
}

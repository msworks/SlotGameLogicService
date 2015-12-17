using System;
using System.Linq;
using System.Net;
using System.IO;
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
            // 暫定 gameId変換テーブル
            // gameId: 000..099 -> useGameId : 1
            // gameId: 100..199 -> useGameId : 2
            var gid = gameId.ParseInt();

            var gidTable =
            Enumerable.Range(0, 100).Select(i => new { useGameId = "1", gid = i }).Concat(
            Enumerable.Range(100, 100).Select(i => new { useGameId = "2", gid = i }));

            var useGameId = gidTable.Where(e => e.gid == gid).First().useGameId;

            var url = GameLogic.settingUrl + $"?gameId={useGameId}";

            Console.WriteLine("SettingUrl:" + url);

            var text = "";

            try
            {
                var request = WebRequest.Create(url);
                var response = request.GetResponse();
                var stream = response.GetResponseStream();
                var reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                text = reader.ReadToEnd();
                stream.Close();
                reader.Close();
                response.Close();
            }
            catch (Exception)
            {
                return 6;
            }

            Console.Out.WriteLine("Server response:" + text);

            var value = 6;

            try
            {
                var values = text.Split(',').Select(v => v.ParseInt());
                var table = values.Select((v, setting) => new { setting = setting, value = v });
                var ary = table.Select(t => Range(0, t.value).Select(v => t.setting)).Flatten().ToArray();
                var len = ary.Count();
                value = ary[rnd.Next(0, len - 1)];
            }
            catch(Exception e)
            {
                Console.Out.WriteLine("format exception");
                return 6;
            }

            return value;
        }
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

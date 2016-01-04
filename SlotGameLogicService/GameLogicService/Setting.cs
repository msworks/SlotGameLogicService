using System;
using System.Collections.Generic;
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

        static public int Get(IEnumerable<int> settings)
        {
            var table = settings.Select((v, setting) => new { setting = setting, value = v });
            var ary = table.Select(t => Range(0, t.value).Select(v => t.setting)).Flatten().ToArray();
            var len = ary.Count();
            var value = ary[rnd.Next(0, len - 1)];

            return value;
        }
    }
}

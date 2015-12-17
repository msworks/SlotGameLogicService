using System;
using System.Collections.Generic;
using System.Linq;

namespace TheOcean
{
    /// <summary>
    /// ルート決定器
    /// </summary>
    class RouteDeterminer
    {
        /// <summary>
        /// 乱数用seed
        /// </summary>
        int seed;

        /// <summary>
        /// 機械割 0..6
        /// </summary>
        int setting;

        /// <summary>
        /// 乱数テーブル
        /// </summary>
        Random rnd;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="seed"></param>
        /// <param name="setting"></param>
        public RouteDeterminer(int seed, int setting)
        {
            this.seed = seed;
            this.setting = setting;

            rnd = new Random(seed);
        }

        /// <summary>
        /// ルート取得
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        public Route GetRoute(RoundMachine roundMachine, int power)
        {
            var route = settingTable.Where(st => st.setting == setting)
                                    .First()
                                    .table[RndFF];

            if(roundMachine.State == RoundState.EnsyutuSyuryo)
            {
                route = Route.Atacker;
            }
            else if(roundMachine.State == RoundState.Kaitentai)
            {
                if(route==Route.Chacker||route==Route.Chacker7)
                {
                    route = Route.Kaitenti;
                }
            }
            else if (roundMachine.State == RoundState.Turip)
            {
                if(power > 150)
                {
                    route = Route.Syokyu15;
                }
            }

            return route;
        }

        /// <summary>
        /// 0～255のランダム値を返す
        /// </summary>
        int RndFF
        {
            get
            {
                return rnd.RndFF();
            }
        }

        class SettingTable
        {
            public int setting;
            public Route[] table;
        }

        //-- static --

        static List<SettingTable> settingTable;

        /// <summary>
        /// 静的コンストラクタ
        /// </summary>
        static RouteDeterminer()
        {
            // setting:機械割 atari:賞球% chacker:
            var table = new[]
            {
                new { setting = 6, atari=9.0f, chacker7=12.0f  },
                new { setting = 5, atari=8.0f, chacker7=10.0f  },
                new { setting = 4, atari=7.0f, chacker7=8.0f  },
                new { setting = 3, atari=6.0f, chacker7=7.0f  },
                new { setting = 2, atari=5.0f, chacker7=6.0f  },
                new { setting = 1, atari=4.0f, chacker7=5.0f  },
                new { setting = 0, atari=4.0f, chacker7=5.0f  },
            };

            // in:当たり確率 out:入賞テーブル
            Func<float, float, Route[]> percentToTalbe = (atari, chacker) =>
            {
                var length = 256;
                var atariLength = (int)(atari / 100 * (float)length);
                var chackerLength = (int)(chacker / 100 * (float)length);
                var hazureLength = length - atariLength - chackerLength;

                var a = Enumerable.Range(0, atariLength)
                                  .Select(v => Route.Syokyu7);

                var c = Enumerable.Range(0, chackerLength)
                                  .Select(v => Route.Chacker7);

                var h = Enumerable.Range(0, hazureLength)
                                  .Select(v => Route.Abandon);

                var result = a.Concat(c).Concat(h).ToArray();

                return result;
            };

            settingTable = table.Select(e => new SettingTable {
                setting = e.setting,
                table = percentToTalbe(e.atari, e.chacker7)
            }).ToList();

        }
    }
}

using System.Linq;

namespace TheOcean
{
    /// <summary>
    /// サーバーサイドのTheOceanマシン
    /// </summary>
    class TheOceanMachine
    {
        /// <summary>
        /// 抽選器
        /// </summary>
        MainLogic mainLogic;

        /// <summary>
        /// 確変の状態遷移マシン
        /// </summary>
        KakuhenMachine kakuhenMachine;

        /// <summary>
        /// ルート決定器
        /// </summary>
        RouteDeterminer routeDeterminer;

        /// <summary>
        /// リールの状態遷移マシン
        /// </summary>
        ReelMachine reelMachine;

        /// <summary>
        /// ラウンドの状態遷移マシン
        /// </summary>
        RoundMachine roundMachine;

        int seed;
        int setting;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="seed">0～FFFF</param>
        /// <param name="setting">1～6</param>
        public TheOceanMachine(int seed, int setting)
        {
            this.seed = seed;
            this.setting = setting;

            mainLogic = new MainLogic(seed);
            kakuhenMachine = new KakuhenMachine();
            routeDeterminer = new RouteDeterminer(seed, setting);
            reelMachine = new ReelMachine();
            roundMachine = new RoundMachine();
        }

        /// <summary>
        /// 進捗させる
        /// </summary>
        public ShootResult Progress()
        {
            var yaku = Yaku.Hazure;
            var route = Route.Nothing;
            var payout = 0;

            var result = new ShootResult()
            {
                yaku = yaku,
                route = route,
                payout = payout,
            };

            // 状態遷移する
            roundMachine.Progress();

            return result;
        }

        /// <summary>
        /// 玉発射
        /// </summary>
        /// <param name="power">0～255</param>
        /// <returns></returns>
        public ShootResult Shoot(int power)
        {
            var yaku = Yaku.Hazure;

            // powerと機械の状態から、yakuとrouteを決定する
            var route = routeDeterminer.GetRoute(roundMachine, power);

            if( route==Route.Chacker7 || route==Route.Chacker )
            {
                yaku = mainLogic.Chusen(kakuhenMachine.Mode, setting);

                //デバッグ用 大当たり固定
                yaku = Yaku.Atari;
            }

            var payout = payoutTable.Where(pt=>pt.route== route)
                                    .FirstOrDefault()
                                    .payout;

            var result = new ShootResult()
            {
                yaku = yaku,
                route = route,
                payout = payout,
            };

            return result;
        }

        public class ShootResult
        {
            public Yaku yaku;
            public Route route;
            public int payout;
        }

        public class RoutePayout
        {
            public Route route;
            public int payout;
        }

        // static

        static RoutePayout[] payoutTable = new RoutePayout[]
        {
            new RoutePayout{ route = Route.Abandon,  payout = 0  },
            new RoutePayout{ route = Route.Chacker,  payout = 0  },
            new RoutePayout{ route = Route.Chacker7, payout = 7  },
            new RoutePayout{ route = Route.Syokyu7,  payout = 7  },
            new RoutePayout{ route = Route.Syokyu15, payout = 15 },
            new RoutePayout{ route = Route.Atacker,  payout = 0  },
            new RoutePayout{ route = Route.Nothing,  payout = 0  },
            new RoutePayout{ route = Route.Kaitenti, payout = 0  },
        };
    }
}

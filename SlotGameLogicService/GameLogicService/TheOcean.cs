using System;
using System.Collections.Generic;
using TheOcean;

namespace GameLogicService
{
    using Associative = Dictionary<string, string>;
    using GameId = String;
    using UserId = String;

    /// <summary>
    /// パチンコサーバー
    /// </summary>
    public class TheOcean : IMachine
    {
        GameId gameId;
        UserId userId;
        TheOceanMachine machine;

        public MACHINE_STATE State
        {
            get;
            private set;
        }

        public TheOcean(GameId gameId, UserId userId)
        {
            State = MACHINE_STATE.CREATED;
            this.gameId = gameId;
            this.userId = userId;
        }

        public Associative Config(Associative param)
        {
            var seed = (((int)(Util.GetMilliSeconds())) & 0xFFFF);
            Console.WriteLine("[INFO][TheOcean]Seed:" + seed);

            // TODO settingの値を6に固定しているのでサーバーから取得する
            var setting = 6;
            Console.WriteLine("[INFO][TheOcean]Setting:" + setting);

            machine = new TheOceanMachine(seed:seed, setting:setting);

            var result = new Associative()
            {
                { "setting", setting.ToString() },
                { "reelleft", "0" },
                { "reelcenter", "0" },
                { "reelright", "0" },
                { "seed", seed.ToString() },
            };

            State = MACHINE_STATE.CONFIG;

            return result;
        }

        public Associative Init(Associative param)
        {
            var result = new Associative();
            State = MACHINE_STATE.INIT;
            return result;
        }

        public Associative Play(Associative param)
        {
            var betcount = null as string;
            var rate = null as string;
            var power = null as string;
            var pow = 0;

            try
            {
                betcount = param["betCount"];
                rate = param["rate"];
                power = param["power"];
                pow = int.Parse(power);
            }
            catch
            {
                return new Associative() { { "result", "error".DQ() } };
            }

            var shootResult = null as TheOceanMachine.ShootResult;

            if (betcount == "0" && power == "0")
            {
                shootResult = machine.Progress();
            }
            else
            {
                // 玉発射
                shootResult = machine.Shoot(pow);
            }

            var result = new Associative();
            result.Add("yaku", ((int)shootResult.yaku).ToString());
            result.Add("route", ((int)shootResult.route).ToString());
            result.Add("payout", ((int)shootResult.payout).ToString());

            if (shootResult.route != Route.Abandon)
            {
                Console.WriteLine("G:{5} U:{6} power:{3} rate:{4} yaku:{0} route:{1} payout:{2}",
                shootResult.yaku,
                              shootResult.route,
                              shootResult.payout,
                              power,
                              rate,
                              gameId,
                              userId
                              );
            }

            State = MACHINE_STATE.PLAY;
            return result;
        }

        public Associative Collect(Associative param)
        {
            var reelstopleft = null as string;
            var reelstopcenter = null as string;
            var reelstopright = null as string;
            var oshijun = null as string;

            try
            {
                reelstopleft = param["reelStopLeft"];
                reelstopcenter = param["reelStopCenter"];
                reelstopright = param["reelStopRight"];
                oshijun = param["oshijun"];
            }
            catch
            {
                return new Associative() { { "result", "error".DQ() } };
            }

            var result = new Associative();
            result.Add("result", "WIN".DQ());

            State = MACHINE_STATE.CORRECT;
            return result;
        }
    }
}

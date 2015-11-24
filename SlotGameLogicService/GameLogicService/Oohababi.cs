using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GameLogicService
{
    using Associative = Dictionary<string, string>;
    using GameId = String;
    using UserId = String;
    using Json = String;

    public class Oohababi : IMachine
    {
        public MACHINE_STATE State
        {
            get { return _state; }
            private set { _state = value; }
        }

        MACHINE_STATE _state;
        Mobile mobile;
        GameId gameId;
        UserId userId;

        public Oohababi(GameId gameId, UserId userId)
        {
            State = MACHINE_STATE.CREATED;
            this.gameId = gameId;
            this.userId = userId;
        }

        public Associative Config(Associative param)
        {
            mobile = new Mobile();

            var result = new Associative();

            var seed = mobile.Seed.ToString();

            // TODO settingの値を6に固定しているのでサーバーから取得する
            var setting = 6;

            if (setting == 0)
            {
                mobile.GameManager.SettingZeroMode = true;
                mobile.SetRate(1);
            }
            else
            {
                mobile.GameManager.SettingZeroMode = false;
                mobile.SetRate(setting);
            }

            result.Add("setting", setting.ToString());
            result.Add("reelleft", "0");
            result.Add("reelcenter", "0");
            result.Add("reelright", "0");
            result.Add("seed", seed);

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
            Console.WriteLine("[INFO]PLAY");

            var betcount = null as string;
            var rate = null as string;

            try
            {
                betcount = param["betCount"];
                rate = param["rate"];
            }
            catch
            {
                return new Associative() { { "result", "error".DQ() } };
            }

            var beforeCoinCount = mobile.CoinCount;
            //Console.WriteLine("[INFO]Coin:" + beforeCoinCount);

            var bet = betcount.ParseInt();
            mobile.InsertCoin(bet);

            Console.WriteLine("[INFO]Bet:" + bet);

            //--------------------------------------
            // 内部のスロットマシンにコインを入れて
            // レバーを引いてリールが停止するまで
            // 同期で待つ
            //--------------------------------------

            foreach (var state in PlaystateCheck(mobile))
            {
                mobile.ZZ.int_value[Defines.DEF_Z_INT_KEYPRESS] |= 0;
                mobile.exec();
                mobile.ZZ.int_value[Defines.DEF_Z_INT_KEYPRESS] |= (1 << 5);
                mobile.exec();
                Thread.Sleep(20);
            }

            foreach (var n in Enumerable.Range(0, 100))
            {
                mobile.exec();
                Thread.Sleep(20);
            }

            var yaku = mobile.Yaku;
            var afterCoinCount = mobile.CoinCount;
            var payout = afterCoinCount - beforeCoinCount;

            if (payout < 0) payout = 0;

            //Console.WriteLine("[INFO]ALL REEL STOPPED");
            Console.WriteLine("[INFO]YAKU:" + yaku);
            //Console.WriteLine("[INFO]Coin:" + afterCoinCount);
            Console.WriteLine("[INFO]PAYOUT:" + payout);

            var result = new Associative();
            result.Add("yaku", ((int)yaku).ToString());
            result.Add("route", "0");
            result.Add("payout", payout.ToString());

            State = MACHINE_STATE.PLAY;
            return result;
        }

        public IEnumerable<PLAYSTATE> PlaystateCheck(Mobile mobile)
        {
            var state = PLAYSTATE.InsertCoin;

            while (true)
            {
                if (mobile.Playstate == PLAYSTATE.Lever)
                {
                    state = PLAYSTATE.Lever;
                    break;
                }

                yield return state;
            }

            while (true)
            {
                if (mobile.IsReelsStopped())
                {
                    mobile.Playstate = PLAYSTATE.AllReelStopped;
                }

                if (mobile.Playstate == PLAYSTATE.AllReelStopped)
                {
                    state = PLAYSTATE.AllReelStopped;
                    break;
                }

                yield return state;
            }

            yield return state;
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

using System;
using System.Collections.Generic;

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
        public MACHINE_STATE State
        {
            get;
            private set;
        }

        GameId gameId;
        UserId userId;

        public TheOcean(GameId gameId, UserId userId)
        {
            State = MACHINE_STATE.CREATED;
            this.gameId = gameId;
            this.userId = userId;
        }

        public Associative Config(Associative param)
        {
            var result = new Associative();

            // TODO settingの値を6に固定しているのでサーバーから取得する
            var setting = 6;
            var seed = 0;

            result.Add("setting", setting.ToString());
            result.Add("reelleft", "0");
            result.Add("reelcenter", "0");
            result.Add("reelright", "0");
            result.Add("seed", seed.ToString());

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

            try
            {
                betcount = param["betCount"];
                rate = param["rate"];
            }
            catch
            {
                return new Associative() { { "result", "error".DQ() } };
            }

            var yaku = 0;
            var payout = 0;

            var result = new Associative();
            result.Add("yaku", ((int)yaku).ToString());
            result.Add("route", "0");
            result.Add("payout", payout.ToString());

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

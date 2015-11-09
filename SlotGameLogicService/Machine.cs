using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameLogicService
{
    using Associative = Dictionary<string, string>;
    using GameId = String;
    using UserId = String;
    using Json = String;

    interface IMachine
    {
        Associative Config(Associative param);
        Associative Init(Associative param);
        Associative Play(Associative param);
        Associative Collect(Associative param);
    }

    class MachineFactory
    {
        static public IMachine Create(GameId gameId, UserId userId)
        {
            var machineFactory = new[]
            {
                new { gameId = "1", createFunc = (Func<GameId, UserId, IMachine>)CreateTheOcean },
                new { gameId = "2", createFunc = (Func<GameId, UserId, IMachine>)CreateOohanabi },
            };

            return machineFactory.Where(table => table.gameId == gameId)
                                 .Select(table => table.createFunc(gameId, userId))
                                 .FirstOrDefault();
        }

        static IMachine CreateOohanabi(GameId gameId, UserId userId)
        {
            return new Oohababi(gameId, userId);
        }

        static IMachine CreateTheOcean(GameId gameId, UserId userId)
        {
            return new TheOcean();
        }
    }

    enum MACHINE_STATE
    {
        CREATED,
        CONFIG,
        INIT,
        PLAY,
        CORRECT,
    }

    class Oohababi : IMachine
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
            mobile.SetRate(setting);

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
            var betcount = null as string;
            var rate = null as string;

            try
            {
                betcount = param["betCount"];
                rate = param["rate"];
            }
            catch
            {
                return new Associative() { {"result", "error".DQ()} };
            }

            var bet = betcount.ParseInt();
            mobile.InsertCoin(bet);

            //--------------------------------------
            // 内部のスロットマシンにコインを入れて
            // レバーを引いてリールが停止するまで
            // 同期で待つ
            //--------------------------------------

            foreach (var state in PlaystateCheck(mobile))
            {
                //Console.WriteLine("[INFO]STATE" + state);
                mobile.ZZ.int_value[Defines.DEF_Z_INT_KEYPRESS] |= 0;
                mobile.exec();
                mobile.ZZ.int_value[Defines.DEF_Z_INT_KEYPRESS] |= (1 << 5);
                mobile.exec();
                Thread.Sleep(20);
            }

            Console.WriteLine("[INFO]ALL REEL STOPPED");

            var yaku = mobile.Yaku;
            Console.WriteLine("[INFO]YAKU:" + yaku);

            var result = new Associative();
            result.Add("yaku", ((int)yaku).ToString());
            result.Add("route", "0");

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

    class TheOcean : IMachine
    {
        public Associative Config(Associative param) { return new Associative(); }
        public Associative Init(Associative param) { return new Associative(); }
        public Associative Play(Associative param) { return new Associative(); }
        public Associative Collect(Associative param) { return new Associative(); }
    }
}

public enum PLAYSTATE
{
    InsertCoin,
    Lever,
    AllReelStopped,
};


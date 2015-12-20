using System;
using System.Collections.Generic;
using System.IO;
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
        TextWriter writer = Console.Out;

        Yaku currentYaku;
        int settingValue;

        public Oohababi(GameId gameId, UserId userId, int settingValue)
        {
            State = MACHINE_STATE.CREATED;
            this.gameId = gameId;
            this.userId = userId;
            this.settingValue = settingValue;
        }

        public Associative Config(Associative param)
        {
            mobile = new Mobile();

            var result = new Associative();
            var seed = mobile.Seed.ToString();
            var setting = this.settingValue;

            writer.Log("[INFO][Oomatsuri]Setting:" + setting);

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
            var betcount = null as string;
            var bet = 0;

            try
            {
                betcount = param["betCount"];
                bet = betcount.ParseInt();
            }
            catch
            {
                writer.Log("[ERROR]Palameter error.");
                return new Associative() { { "result", "error".DQ() } };
            }

            // bet 0 check
            if (bet==0)
            {
                if (!(currentYaku == Yaku.Replay ||
                      currentYaku == Yaku.JACIN ||
                      currentYaku == Yaku.JAC))
                {
                    // bet0のとき、リプレイでなければエラー
                    return new Associative() { { "result", "error".DQ() } };
                }
            }

            mobile.InsertCoin(bet);

            // レバーを引くまで回す
            foreach (var state in ProgressToLever(mobile))
            {
                Action<int> winCoinCallback = (coin) => { };
                mobile.ZZ.int_value[Defines.DEF_Z_INT_KEYPRESS] |= 0;
                mobile.exec(winCoinCallback);
                mobile.ZZ.int_value[Defines.DEF_Z_INT_KEYPRESS] |= (1 << 5);
                mobile.exec(winCoinCallback);
                Thread.Sleep(1);
            }

            var afterCoinCount = mobile.CoinCount;

            var result = new Associative();
            result.Add("route", "0");

            State = MACHINE_STATE.PLAY;

            writer.Log($"[INFO]Play GameId:{gameId} UserId:{userId} Bet:{bet}");

            return result;
        }

        public IEnumerable<PLAYSTATE> ProgressToLever(Mobile mobile)
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

            yield return state;
        }

        public Associative Collect(Associative param)
        {
            var reelstopleft = null as string;
            var reelstopcenter = null as string;
            var reelstopright = null as string;
            var oshijun = null as string;

            var left = 0;
            var center = 0;
            var right = 0;
            var oshijuns = new int[] { 1, 2, 3 };

            try
            {
                reelstopleft = param["reelStopLeft"];
                reelstopcenter = param["reelStopCenter"];
                reelstopright = param["reelStopRight"];
                oshijun = param["oshijun"];

                left = reelstopleft.ParseInt();
                center = reelstopcenter.ParseInt();
                right = reelstopright.ParseInt();
                oshijuns = oshijun.Split('_').Select(s => s.ParseInt()).ToArray();
            }
            catch
            {
                return new Associative() { { "result", "error".DQ() } };
            }

            var yaku = mobile.Yaku;
            var winCoins = 0;
            var gotCoinCountFlg = false;

            Action<int> winCoinCallback = (coin) =>
            {
                winCoins = coin;
                gotCoinCountFlg = true;
            };

            // 止まるまで回す
            while(gotCoinCountFlg == false)
            {
                mobile.ZZ.int_value[Defines.DEF_Z_INT_KEYPRESS] |= 0;
                mobile.exec(winCoinCallback);
                mobile.ZZ.int_value[Defines.DEF_Z_INT_KEYPRESS] |= (1 << 5);
                mobile.exec(winCoinCallback);
                Thread.Sleep(1);
            }

            var result = new Associative();
            result.Add("result", "WIN".DQ());
            result.Add("payout", winCoins.ToString());
            result.Add("yaku", ((int)yaku).ToString());

            State = MACHINE_STATE.COLLECT;

            writer.Log($"[INFO]Collect GameId:{gameId} UserId:{userId} Payout:{winCoins} Yaku:{yaku}");

            // 役を保存しておく
            currentYaku = yaku;

            return result;
        }
    }
}

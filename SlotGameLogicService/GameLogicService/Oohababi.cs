using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Log;

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

            Logger.Info($"[INFO][Oomatsuri]Setting:{setting}");

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
                Logger.Error("[ERROR]Palameter error.");
                return new Associative() { { "result", "error".DQ() } };
            }

            // bet 0 check
            if (bet == 0)
            {
                Logger.Warn("BET 0 ERROR!!!");
                Logger.Warn("BET 0 ERROR!!!");
                Logger.Warn("BET 0 ERROR!!!");
                return new Associative() { { "result", "error".DQ() } };
            }

            mobile.InsertCoin(bet);

            // マシンからのコールバック群

            Action<int> Payout = (coinCount) =>
            {
            };

            var isLeverPulled = false;
            Action<int> ReelStart = (_yaku) =>
            {
                Yaku y = (Yaku)_yaku;
                isLeverPulled = true;
            };

            Action<int, int> ButtonStop = (button, reelIndex) =>
            {
            };

            Action<int> KeyTrigger = (key) =>
            {
            };

            Action Bet = () =>
            {
            };

            var callbacks = new CallbackToController()
            {
                Payout = Payout,
                ReelStart = ReelStart,
                ButtonStop = ButtonStop,
                KeyTrigger = KeyTrigger,
                Bet = Bet
            };

            // レバーを引くまで回す
            while(isLeverPulled == false)
            {
                mobile.ZZ.int_value[Defines.DEF_Z_INT_KEYPRESS] |= (1 << 5);
                mobile.exec(callbacks);
                Thread.Sleep(20);
            }

            var afterCoinCount = mobile.CoinCount;
            var yaku = mobile.Yaku;

            var result = new Associative();
            result.Add("route", "0");
            result.Add("yaku", ((int)yaku).ToString());

            State = MACHINE_STATE.PLAY;

            Logger.Info($"[INFO]Play GameId:{gameId} UserId:{userId} Bet:{bet} ------ Yaku:{yaku}");

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

            var left = 0;   // ボタン０押下時リール位置
            var center = 0; // ボタン１押下時リール位置
            var right = 0;  // ボタン２押下時リール位置
            var order = null as int[]; // 押し順

            try
            {
                reelstopleft = param["reelStopLeft"];
                reelstopcenter = param["reelStopCenter"];
                reelstopright = param["reelStopRight"];
                oshijun = param["oshijun"];

                left = reelstopleft.ParseInt();
                center = reelstopcenter.ParseInt();
                right = reelstopright.ParseInt();
                order = oshijun.ToCharArray().Select(c => c.ToString().ParseInt()-1).ToArray();
            }
            catch
            {
                return new Associative() { { "result", "error".DQ() } };
            }

            var winCoins = 0;
            var gotCoinCountFlg = false;

            // マシンからのコールバック群

            Action<int> Payout = (coinCount) =>
            {
                winCoins = coinCount;
                gotCoinCountFlg = true;
            };

            Action<int> ReelStart = (_yaku) =>
            {
            };

            Action<int, int> ButtonStop = (button, reelIndex) =>
            {
            };

            Action<int> KeyTrigger = (key) =>
            {
            };

            Action Bet = () =>
            {
            };

            var callbacks = new CallbackToController()
            {
                Payout = Payout,
                ReelStart = ReelStart,
                ButtonStop = ButtonStop,
                KeyTrigger = KeyTrigger,
                Bet = Bet
            };

            var indexis = new int[] { left, center, right };

            mobile.SetClientPressed(indexis);

            // 押し順、押した位置で止める
            while(gotCoinCountFlg == false)
            {
                mobile.ZZ.int_value[Defines.DEF_Z_INT_KEYPRESS] |= (1 << 5);
                mobile.exec(callbacks);
                Thread.Sleep(20);
            }

            var v1 = reelstopleft.Substring(1, (reelstopleft.Length - 1));
            var v2 = reelstopcenter.Substring(1, (reelstopcenter.Length - 1));
            var v3 = reelstopright.Substring(1, (reelstopright.Length - 1));
            var v4 = oshijun.Substring(1, (oshijun.Length - 1));
            var xor = v1 + v2 + v3 + v4;
            var code = (xor.ParseLong()^(0xffeeddcc)).ToString();
            var yaku = code.Substring(4, 2).ParseInt();
            var payout = code.Substring(6, 2).ParseInt();

            var result = new Associative();
            result.Add("result", "WIN".DQ());
            result.Add("payout", payout.ToString());
            result.Add("yaku", ((int)yaku).ToString());

            State = MACHINE_STATE.COLLECT;

            Logger.Info($"[INFO]Collect GameId:{gameId} UserId:{userId} Payout:{winCoins} Yaku:{yaku}");

            // 役を保存しておく
            currentYaku = (Yaku)yaku;

            return result;
        }
    }
}

public static class stringExtension2
{
    public static long ParseLong(this string source)
    {
        return long.Parse(source);
    }
}

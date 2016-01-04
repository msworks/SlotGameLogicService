using System;
using System.IO;
using Log;

namespace TheOcean
{
    // 何か状態遷移を管理するいいモジュールないかな

    public enum RoundState
    {
        EndRound,
        EnsyutuSyuryo,      // 演出終了 
        Kaitentai,          // 回転体待ち
        Turip,              // チューリップ待ち
    }

    class RoundMachine
    {
        TextWriter writer = Console.Out;
        public RoundState State { private set; get; }
        public int Round { private set; get; }

        public RoundMachine()
        {
            State = RoundState.EndRound;
            Round = 0;
        }

        public void RoundStart()
        {
            State = RoundState.Kaitentai;
            Round = 1;
            Logger.Info($"[INFO]Round Start:{Round}");
        }

        public void Progress(KakuhenMachine kakuhenMachine)
        {
            if( State == RoundState.EndRound)
            {
                State = RoundState.EnsyutuSyuryo;
                Logger.Info($"[INFO]EnsyutuSyuryo");
            }
            else if( State == RoundState.EnsyutuSyuryo)
            {
                State = RoundState.Kaitentai;
                Logger.Info($"[INFO]Kaitentai");
            }
            else if ( State == RoundState.Kaitentai)
            {
                State = RoundState.Turip;
                Logger.Info($"[INFO]Turip Open");
            }
            else if( State == RoundState.Turip)
            {
                if( Round>= 15)
                {
                    // 最終ラウンド終了
                    State = RoundState.EndRound;
                    Logger.Info($"[INFO]End round");
                    Round = 0;

                    kakuhenMachine.Syoka();

                    return;
                }

                State = RoundState.Kaitentai;
                Round++;
                Logger.Info($"[INFO]Round Start:{Round}");
            }
        }
    }
}

using System.Linq;

namespace TheOcean
{
    public enum KakuhenMode
    {
        Normal,
        Kakuhen3,
        Kakuhen2,
        Kakuhen1,
    }

    /// <summary>
    /// 確変の状態遷移マシン
    /// </summary>
    class KakuhenMachine
    {
        public KakuhenMode Mode
        {
            get;
            private set;
        }

        public KakuhenMachine()
        {
            Mode = KakuhenMode.Normal;
        }

        public void Syoka()
        {
            var table = new[]
            {
                new { pre = KakuhenMode.Normal, post = KakuhenMode.Kakuhen1 },
                new { pre = KakuhenMode.Kakuhen1, post = KakuhenMode.Kakuhen2 },
                new { pre = KakuhenMode.Kakuhen2, post = KakuhenMode.Kakuhen1 },
            };

            Mode = table.Where(t => t.pre == Mode)
                        .First()
                        .post;
        }
    }
}

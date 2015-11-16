using System.Linq;

namespace TheOcean
{
    enum KakuhenMode
    {
        Normal,
        Kakuhen3,
        Kakuhen2,
        Kakuhen1,
    }

    class Kakuhen
    {
        KakuhenMode mode = KakuhenMode.Normal;

        public void Syoka()
        {
            var table = new[]
            {
                new { pre = KakuhenMode.Normal, post = KakuhenMode.Kakuhen1 },
                new { pre = KakuhenMode.Kakuhen3, post = KakuhenMode.Kakuhen2 },
                new { pre = KakuhenMode.Kakuhen2, post = KakuhenMode.Kakuhen1 },
                new { pre = KakuhenMode.Kakuhen1, post = KakuhenMode.Normal },
            };

            mode = table.Where(t => t.pre == mode)
                        .First()
                        .post;
        }
    }
}

using System;
using System.Linq;

namespace TheOcean
{
    /// <summary>
    /// メインロジック
    /// </summary>
    public class MainLogic
    {
        /// <summary>
        /// 乱数テーブル
        /// </summary>
        Random rnd;

        /// <summary>
        /// 動的コンストラクタ
        /// </summary>
        /// <param name="seed"></param>
        public MainLogic(int seed)
        {
            rnd = new Random(seed);
        }

        /// <summary>
        /// 大当たり抽選 
        /// </summary>
        /// <param name="KenriKaisu">権利回数（０でなければ確変中）</param>
        /// <param name="setting">機械割</param>
        /// <returns></returns>
        public Yaku Chusen(KakuhenMode mode, int setting)
        {
            var defaultLine = new { mode = KakuhenMode.Normal, setting = 1, table=NormalChusenTable };

            var lines = new[]
            {
                new { mode = KakuhenMode.Normal, setting = 0, table=NormalChusenTableSetting0 },
                new { mode = KakuhenMode.Normal, setting = 1, table=NormalChusenTable },
                new { mode = KakuhenMode.Normal, setting = 2, table=NormalChusenTable },
                new { mode = KakuhenMode.Normal, setting = 3, table=NormalChusenTable },
                new { mode = KakuhenMode.Normal, setting = 4, table=NormalChusenTable },
                new { mode = KakuhenMode.Normal, setting = 5, table=NormalChusenTable },
                new { mode = KakuhenMode.Normal, setting = 6, table=NormalChusenTable },

                new { mode = KakuhenMode.Kakuhen1, setting = 0, table=KakuhenChusenTable },
                new { mode = KakuhenMode.Kakuhen1, setting = 1, table=KakuhenChusenTable },
                new { mode = KakuhenMode.Kakuhen1, setting = 2, table=KakuhenChusenTable },
                new { mode = KakuhenMode.Kakuhen1, setting = 3, table=KakuhenChusenTable },
                new { mode = KakuhenMode.Kakuhen1, setting = 4, table=KakuhenChusenTable },
                new { mode = KakuhenMode.Kakuhen1, setting = 5, table=KakuhenChusenTable },
                new { mode = KakuhenMode.Kakuhen1, setting = 6, table=KakuhenChusenTable },

                new { mode = KakuhenMode.Kakuhen2, setting = 0, table=KakuhenChusenTable },
                new { mode = KakuhenMode.Kakuhen2, setting = 1, table=KakuhenChusenTable },
                new { mode = KakuhenMode.Kakuhen2, setting = 2, table=KakuhenChusenTable },
                new { mode = KakuhenMode.Kakuhen2, setting = 3, table=KakuhenChusenTable },
                new { mode = KakuhenMode.Kakuhen2, setting = 4, table=KakuhenChusenTable },
                new { mode = KakuhenMode.Kakuhen2, setting = 5, table=KakuhenChusenTable },
                new { mode = KakuhenMode.Kakuhen2, setting = 6, table=KakuhenChusenTable },

                new { mode = KakuhenMode.Kakuhen3, setting = 0, table=KakuhenChusenTable },
                new { mode = KakuhenMode.Kakuhen3, setting = 1, table=KakuhenChusenTable },
                new { mode = KakuhenMode.Kakuhen3, setting = 2, table=KakuhenChusenTable },
                new { mode = KakuhenMode.Kakuhen3, setting = 3, table=KakuhenChusenTable },
                new { mode = KakuhenMode.Kakuhen3, setting = 4, table=KakuhenChusenTable },
                new { mode = KakuhenMode.Kakuhen3, setting = 5, table=KakuhenChusenTable },
                new { mode = KakuhenMode.Kakuhen3, setting = 6, table=KakuhenChusenTable },
            };

            var line = lines.Where(t => t.mode == mode && t.setting == setting)
                            .FirstOrDefault() ?? defaultLine;

            var chusen = line.table;

            var atari = chusen[RndFFFF];
            var result = Yaku.Hazure;

            if(atari==true)
            {
                result = Yaku.Atari;
            }

            return result;
        }

        /// <summary>
        /// 0～65535のランダム値を返す
        /// </summary>
        int RndFFFF
        {
            get
            {
                return rnd.RndFFFF();
            }
        }

        /// <summary>
        /// 設定０通常時抽選テーブル
        /// </summary>
        static bool[] NormalChusenTableSetting0;

        /// <summary>
        /// 通常時抽選テーブル
        /// </summary>
        static bool[] NormalChusenTable;

        /// <summary>
        /// 確変時抽選テーブル
        /// </summary>
        static bool[] KakuhenChusenTable;             

        /// <summary>
        /// 静的コンストラクタ
        /// </summary>
        static MainLogic()
        {
            var S0_ATARI_NUM = 163;       // 設定０大当たり
            var S0_HAZURE_NUM = 65373;    // 設定０はずれ
            var ATARI_NUM = 202;          // 大当たり
            var HAZURE_NUM = 65334;       // はずれ
            var KH_ATARI_NUM = 2029;      // 確率変動時大当たり
            var KH_HAZURE_NUM = 63507;    // 確率変動時はずれ

            // 大当たり抽選テーブルの初期化
            var S0_ATARI = Enumerable.Range(0, S0_ATARI_NUM).Select(v => true);
            var S0_HAZURE = Enumerable.Range(0, S0_HAZURE_NUM).Select(v => false);
            NormalChusenTableSetting0 = S0_ATARI.Concat(S0_HAZURE).ToArray();

            // 大当たり抽選テーブルの初期化
            var ATARI = Enumerable.Range(0, ATARI_NUM).Select(v => true);
            var HAZURE = Enumerable.Range(0, HAZURE_NUM).Select(v => false);
            NormalChusenTable = ATARI.Concat(HAZURE).ToArray();

            // 大当たり（確変）抽選テーブルの初期化
            var KH_ATARI = Enumerable.Range(0, KH_ATARI_NUM).Select(v => true);
            var KH_HAZURE = Enumerable.Range(0, KH_HAZURE_NUM).Select(v => false);
            KakuhenChusenTable = KH_ATARI.Concat(KH_HAZURE).ToArray();
        }
    }
}

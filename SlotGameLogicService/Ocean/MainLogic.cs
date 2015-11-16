using System;
using System.Linq;

namespace TheOcean
{
    /// <summary>
    /// メインロジック
    /// </summary>
    public class MainLogic
    {
        Kenri kenri;

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
        /// <returns></returns>
        public bool DrawLot(int KenriKaisu)
        {
            var chusen = (KenriKaisu == 0) ? NML_Chusen : KH_Chusen;
            var Atari = chusen[RndFFFF];
            return Atari;
        }

        /// <summary>
        /// 権利回数を返す
        /// </summary>
        /// <returns></returns>
        int KenriKaisu
        {
            get
            {
                // TODO 実装
                return -1;
            }
        }

        /// <summary>
        /// 0～65535のランダム値を返す
        /// </summary>
        int RndFFFF
        {
            get
            {
                var CHUSEN_LEN = 65536;       // 抽選のサイズ
                return rnd.Next(CHUSEN_LEN);
            }
        }

        // 通常時抽選テーブル
        static bool[] NML_Chusen;

        // 確変時抽選テーブル
        static bool[] KH_Chusen;             

        /// <summary>
        /// 静的コンストラクタ
        /// </summary>
        static MainLogic()
        {
            var ATARI_NUM = 202;          // 大当たり
            var HAZURE_NUM = 65334;       // はずれ
            var KH_ATARI_NUM = 2029;      // 確率変動時大当たり
            var KH_HAZURE_NUM = 63507;    // 確率変動時はずれ

            // 大当たり抽選テーブルの初期化
            var ATARI = Enumerable.Range(0, ATARI_NUM).Select(v => true);
            var HAZURE = Enumerable.Range(0, HAZURE_NUM).Select(v => false);
            NML_Chusen = ATARI.Concat(HAZURE).ToArray();

            // 大当たり（確変）抽選テーブルの初期化
            var KH_ATARI = Enumerable.Range(0, KH_ATARI_NUM).Select(v => true);
            var KH_HAZURE = Enumerable.Range(0, KH_HAZURE_NUM).Select(v => false);
            KH_Chusen = KH_ATARI.Concat(KH_HAZURE).ToArray();
        }
    }
}

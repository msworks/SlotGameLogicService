//using UnityEngine;
using System.Collections;

public partial class Defines {


/**
 * リールの状態定数
 * 
 * @author A03605WO with AK
 */


	// ////////////////////////////////////////////////////////////////////
	// スロット汎用：今のところ固定だが新基準では？

	/**
	 * １リールの絵柄総数
	 */
	public static readonly int DEF_N_FRAME = 21;
	
	// 		======================================================================================
	// 			 図柄コード
	// 		======================================================================================

	// TODO 画像ファイルの並び順にあわせる
	/** BLUE_7 */
	public static readonly int DEF_ID_REEL_BSVN = 0;
	/** RED_7 */
	public static readonly int DEF_ID_REEL_RSVN = 1;
	/** WMELON */
	public static readonly int DEF_ID_REEL_WMLN = 2;
	/** BELL */
	public static readonly int DEF_ID_REEL_BELL = 3;
	/** CHERRY */
	public static readonly int DEF_ID_REEL_CHRY = 4;
	/** REPLAY */
	public static readonly int DEF_ID_REEL_RPLY = 5;
	/** BAR */
	public static readonly int DEF_ID_REEL_BAR_ = 6;

	/** リール絵柄種類数 */
	public static readonly int DEF_N_ID_REEL = 7;

	// ////////////////////////////////////////////////////////////////////
	// 遊技状態

	/** RB作動中 */
	public static readonly int DEF_PS_RB_RUN = 0;

	/** BB作動中 */
	public static readonly int DEF_PS_BB_RUN = 1;

	/** 一般遊技中 */
	public static readonly int DEF_PS_NORMAL = 2;

	/** RBフラグ間 */
	public static readonly int DEF_PS_RB_FLAG = 3;

	/** BBﾌﾗｸﾞ間 */
	public static readonly int DEF_PS_BB_FLAG = 4;

	/** 遊技状態数 */
	public static readonly int DEF_N_PLAY = 5;

	/** RB作動中 */
    // TODO 未定義値を使用
	//public static readonly int DEF_GMLVSTS_RB_RUN = (1<<PS_RB_RUN);

	/** BB作動中 */
    // TODO 未定義値を使用
    //public static readonly int DEF_GMLVSTS_BB_RUN = (1<<PS_BB_RUN);

	/** 一般遊技中 */
    // TODO 未定義値を使用
    //public static readonly int DEF_GMLVSTS_NORMAL = (1<<PS_NORMAL);

	/** RBフラグ間 */
    // TODO 未定義値を使用
    //public static readonly int DEF_GMLVSTS_RB_FLAG = (1<<PS_RB_FLAG);

	/** BBﾌﾗｸﾞ間 */
    // TODO 未定義値を使用
    //public static readonly int DEF_GMLVSTS_BB_FLAG = (1<<PS_BB_FLAG);


	// ////////////////////////////////////////////////////////////////////
	// 当選フラグ：頂いたデータ順
//	はずれ, 中ﾁｪﾘｰ, 上下ﾁｪﾘｰ, ﾌﾞｯｸ, ｶｰﾄﾞ, JAC

	/** ハズレ */
	public static readonly int DEF_HF_HAZURE = 0;

	/** 2チェリー */
	public static readonly int DEF_HF_2CHERRY = 1;

	/** 4チェリー */
	public static readonly int DEF_HF_4CHERRY = 2;

	/** ｶｰﾄﾞ */
	public static readonly int DEF_HF_BELL = 3;
	
	/** ﾌﾞｯｸ */
	public static readonly int DEF_HF_WMLN = 4;

	/** コンビ */
	public static readonly int DEF_HF_CONB = 5;

	/** REP,JACIN */
	public static readonly int DEF_HF_REPLAY = 6;

	/** RB */
	public static readonly int DEF_HF_REG = 7;

	/** BB */
	public static readonly int DEF_HF_BIG = 8;

	/**
	 * 当選フラグ数
	 */
	public static readonly int DEF_N_HIT = 9;

	// ////////////////////////////////////////////////////////////////////
	// ランダム：頂いたデータによる

	/**
	 * テーブルラインの選択肢の最大数
	 */
	public static readonly int DEF_TL_RAND_MAX = 4;


	// ////////////////////////////////////////////////////////////////////
	// ライン
	public static readonly int DEF_LINE_CENTER = 0;
	public static readonly int DEF_LINE_TOP = 1;
	public static readonly int DEF_LINE_BOTTOM = 2;
	public static readonly int DEF_LINE_CROSS_UP = 3;
	public static readonly int DEF_LINE_CROSS_DOWN = 4;
	/**ライン数*/
	public static readonly int DEF_N_LINE = 5;
	// ////////////////////////////////////////////////////////////////////
	// リール
	public static readonly int DEF_REEL_LEFT = 0;
	public static readonly int DEF_REEL_CENTER = 1;
	public static readonly int DEF_REEL_RIGHT = 2;
	/**
	 * リールの数
	 */
	public static readonly int DEF_N_REELS = 3;
	
	
	// ////////////////////////////////////////////////////////////////////
	// 当選枚数：当たった時の払い出し数

	/** リプレイビット */
	public static readonly int DEF_COIN_REPLAY_BIT = 0x1000;

	/** リプレイビットマスク */
	public static readonly int DEF_COIN_REPLAY_MASK = 0xF000;

	/** コイン数ビットマスク */
	public static readonly int DEF_COIN_NUM_MASK = 0x0FFF;

	/** 最大獲得数 */
	public static readonly int DEF_COIN_MAX = 15;


}

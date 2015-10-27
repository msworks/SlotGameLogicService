/*
 * 作成日: 2003/08/19
 */


/**
 * デバッグ定義. <br>
 * 全て OFF の状態でしかリリースしてはいけません。 一つのフラグだけをオンにした場合は期待した動作をするでしょう。
 * 複数のフラグをオンにした場合の挙動は不定なので信頼してはいけません。
 * 
 * @author A03605WO
 */
public partial class Defines {

	
	public static readonly bool DEF_IS_DEBUG_MISSION_PARAM = false;
	
	public static readonly bool DEF_IS_PRINT_FREEMEMORY = false;

//	public static readonly bool DEF_IS_DEBUG_FORCE_YAKU = false;
	
	public static readonly bool DEF_IS_DEBUG_AUTO = false;

	/** デバッグ？ */
    public static readonly bool DEF_IS_DEBUG_PARAM = false;
	
    /** デバッグ？ */
    public static readonly bool DEF_IS_DEBUG 	 = false; // TODO trueになってた
    public static readonly bool DEF_IS_DEBUG_POS = false;

    /** Mobile のテスト */
    public static readonly bool DEF_IS_DEBUG_MOBILE = false;
    /** Mobileメニューのテスト */
    public static readonly bool DEF_IS_DEBUG_MENU = false;
    /** Reelsのテスト */
    public static readonly bool DEF_IS_DEBUG_REELS = false;
    /** インフォ画面のテスト */
    public static readonly bool DEF_IS_DEBUG_INFO = false;

    /** process() のテスト */
    public static readonly bool DEF_IS_DEBUG_PROCESS = false;

	/** スロットモードのテスト (カレントモードを確かめたければこれ) */
	public static readonly bool DEF_IS_DEBUG_PRINT_RMODE = false;
//
//
//    /** paintSlot() のテスト */
//    public static readonly bool DEF_IS_DEBUG_PAINT = false;
//
    /** 音のチェック */
    public static readonly bool DEF_IS_DEBUG_SOUND = false;

//
/////////////////////
//
    /** 遊戯状態を表示？ */
    public static readonly bool DEF_IS_PRINT_PLAYSTATE = false; // 表示

	/** 遊戯状態を強制的に */
	public static readonly bool DEF_IS_FORCE_PLAYSTATE = false; // 強制フラグ

	/** 抽選役を表示？ */
	public static readonly bool DEF_IS_PRINT_YAKU = false; // 表示

	/** 遊戯状態を強制的に */
	public static readonly bool DEF_IS_FORCE_YAKU = false; // 強制フラグ

	public static readonly bool DEF_IS_FORCE_BIG = false;
//
//
//
//
//	/** 演出抽選しないフラグ */
//	public static readonly bool DEF_IS_DEBUG_NON_PROBA_EFFECT = false; 
//
//
//	/** 演出抽選途中経過の表示 */
//	public static readonly bool DEF_IS_DEBUG_PRINT_PROBA_EFFECT = false;
//
/////////////////////
//
//
//
//
//    /** メモリ表示 */
//    public static readonly bool DEF_IS_PRINT_FREEMEMORY = false;
//
    /** オートプレイモード */
    public static readonly bool DEF_IS_AUTO_PLAY = false;
//
//    /** リール強制停止？ */
//    public static readonly bool DEF_IS_REEL_STOP_FORCE = false;
//
//    /** リール制御データ非参照？ */
//    public static readonly bool DEF_IS_REEL_STOP_BITA = false;
//
//    /** CT のテスト */
//    public static readonly bool DEF_IS_DEBUG_CT = false;
//
//    /** 演出だけのテスト */
//    public static readonly bool DEF_IS_DEBUG_DIRECT = false;
//
//	/** フラッシュ抽選結果の表示 */
//	public static readonly bool DEF_IS_PRINT_FLASH = false;
//
//
	/** 設定値のテスト */
	public static readonly bool DEF_IS_PRINT_SETUP_VALUE = false;

	/** 小役カウンタのテスト */
	public static readonly bool DEF_IS_PRINT_COIN_COUNTER = false;
//	
//
//	/** BB/RBの消化数を表示 */
//	public static readonly bool DEF_IS_PRINT_BB_RB_GAMES = false;
//
//
//	/** データパネルの更新のテスト */
//	public static readonly bool DEF_IS_DATA_PANEL_TEST = false;
//
//	/** 強制フラッシュなし */
//	public static readonly bool DEF_IS_FORCE_FLASH_NONE = false;
//
//	/** 強制ＷＩＮランプ点灯 */
//	public static readonly bool DEF_IS_FORCE_WIN_ON = false;
//
//	/** ﾌﾗｸﾞ間演出のデバッグ */
//	public static readonly bool DEF_IS_DEBUG_FLAG_EFF = false;
//	
//	/** やっぱりご機嫌葉月ちゃんのテスト */
//	public static readonly bool DEF_IS_DEBUG_YAPPARI_HAZUKICHAN = false;
//
//
//
/////////////////////////////////
//// for 演出
//	
//	
////   public static readonly bool DEF_IS_MOTION_ORDER = true; 
////   public static readonly bool DEF_IS_MOTION    	 !IS_MOTION_ORDER & = false;	
////   public static readonly bool DEF_IS_ANIMATION 	 !IS_MOTION_ORDER & !IS_MOTION & = false;
////   public static readonly bool DEF_IS_SPRITE    	 !IS_MOTION_ORDER & !IS_MOTION & !IS_ANIMATION & = false;
////
////
////	/** サウンド単体テスト */
////	public static readonly bool DEF_IS_SPRITE_SOUND 	 !IS_MOTION & !IS_ANIMATION & !IS_SPRITE & = false;
////
////
////	/** 数字スプライトのテスト */
////	public static readonly bool DEF_IS_SPRITE_NUMBER 	 !IS_MOTION & !IS_ANIMATION & !IS_SPRITE & = false;
////
////	/** 花火スプライトのテスト */
////	public static readonly bool DEF_IS_SPRITE_FW 	 !IS_MOTION & !IS_ANIMATION & !IS_SPRITE & = false;
////
////
////	/** ボーナス間パネルのテスト */
////	public static readonly bool DEF_IS_BONUS_GRAPH_TEST = false;
//
//
//	/**	デバッグ？ */
////	public static readonly bool DEF_IS_DEBUG = false;
//	public static readonly bool DEF_IS_DEBUG_KEY = false;
////	public static readonly bool DEF_IS_DEBUG_PAINT = false;
////	public static readonly bool DEF_IS_DEBUG_RMODE = false;
//
//	public static readonly bool DEF_IS_DEBUG_PRINT_MOTION_FLAG = false; // フラグを立てるモーションナンバーを確認
//
////
//	public static readonly bool DEF_IS_DEBUG_PRINT_ANIM_FRAME = false; // 現在表示中のアニメーションフレームの情報を表示
//	public static readonly bool DEF_IS_DEBUG_PRINT_MOTION_FRAME = false; // 現在表示中のモーションフレームの情報を表示
//
//	public static readonly bool DEF_IS_DEBUG_PRINT_ANIMATION_FLAG = false; // フラグを立てるアニメーションナンバーを確認
//	public static readonly bool DEF_IS_DEBUG_PRINT_FIRE_WORKS = false; // 花火スプライトデバッグ情報の表示
//	public static readonly bool DEF_IS_DEBUG_PRINT_FIRE_WORKS2 = false;
//
//	public static readonly bool DEF_IS_DEBUG_PRINT_DATA_ID = false;	// スプライトの種別ごとの参照データＩＤを表示
//
//	public static readonly bool DEF_IS_DEBUG_PRINT_SPRITE_ID = false;	// スプライトの種別ごとの参照データＩＤを表示
//
////
//	public static readonly bool DEF_IS_DEBUG_PRINT_ANIM_ID = false;	// アニメーションの参照データＩＤを表示
//	public static readonly bool DEF_IS_DEBUG_PRINT_MOTION_ID = false;	// モーションの参照データＩＤを表示
//
//	public static readonly bool DEF_IS_DEBUG_PRINT_SPRITE_NUMBER = false; // 数字スプライトのデバッグ情報を表示
//
//	public static readonly bool DEF_IS_DEBUG_PRINT_SPRITE_SOUND = false; // サウンド(SE/BGM)スプライトの参照データＩＤを表示
//
//
//	public static readonly bool DEF_IS_DEBUG_STEP_MOTION = false; // MOTION TEST時にアニメーション再生時間を勝手に進めない
//
//	public static readonly bool DEF_IS_DEBUG_DIRECTION = false; // DIRECTION(組み合わせ)データに関するデバッグ
//
//	public static readonly bool DEF_IS_DEBUG_DO_DIRECTION = false; // doDirection()を監視
//	
//	public static readonly bool DEF_IS_DEBUG_DO_BONUS = false; // doBonus()を監視
//
//	public static readonly bool DEF_IS_DEBUG_DO_BONUS_END = false; // doBonusEnd()を監視
//
//	public static readonly bool DEF_IS_DEBUG_DRAWGAUGE = false; // drawGaugeWithBonusCoin()を監視
//
//	public static readonly bool DEF_IS_DEBUG_CHALLENGE_EFFECT = false; // チャレンジ演出のデバッグ。１００％チャレンジ演出を引く
//

	public static readonly bool DEF_IS_DEBUG_PULL3 = false;	//第3停止引き込み処理の監視
}
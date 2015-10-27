public partial class Defines {

/*
 * DfMarineBattle.java
 * 
 * Created on 2005/06/02
 */


/**
 * 
 * @author a05229ak
 */

	// //////////////////////////////////////////////////////////////
	// TOBE [カテゴリ]int_value


	// //////////////////////////////
	// int_value(モード)

	/** 現在のゲームモード */
	public const int DEF_INT_CURRENT_MODE = 0;

	/** 変更要求しているゲームモード */
	public const int DEF_INT_REQUEST_MODE = 1;

	// //////////////////////////////
	// int_value(リール)

	/** 左リールの角度 */
	public const int DEF_INT_REEL_ANGLE_R0 = 2;

	/** 中リールの角度 */
	public const int DEF_INT_REEL_ANGLE_R1 = 3;

	/** 右リールの角度 */
	public const int DEF_INT_REEL_ANGLE_R2 = 4;

	/** 左リールの停止角度 */
	public const int DEF_INT_REEL_STOP_R0 = 5;

	/**
	 * 中リールの停止角度
	 */
	public const int DEF_INT_REEL_STOP_R1 = 6;

	/**
	 * 右リールの停止角度
	 */
	public const int DEF_INT_REEL_STOP_R2 = 7;

	/**
	 * リールが物理的に停止したときのフラグ 使用するのは3bit (0/1|0/1|0/1)で 左から順にリール2，リール1，リール0
	 * の停止フラグに対応する． 各bitに1がたっている場合は，そのリールは停止していることを意味する．
	 */
	public const int DEF_INT_IS_REEL_STOPPED = 8;

	/** 遊戯状態 BB & RB 終了ﾌﾗｸﾞ (!=0で終了した) */
	public const int DEF_INT_IS_BB_RB_END = 9;

	// //////////////////////////////
	// int_value(コイン)

	/** 現在の累積コイン枚数 */
	public const int DEF_INT_SLOT_COIN_NUM = 10;

	/** クレジットコイン枚数(0～50) */
	public const int DEF_INT_CREDIT_COIN_NUM = 11;

	/** １ゲーム中の獲得コイン枚数 */
	public const int DEF_INT_WIN_COIN_NUM = 12;

	/** 払い出しコイン枚数表示 */
	public const int DEF_INT_WIN_GET_COIN = 13;

	/** ボーナス獲得数 */
	public const int DEF_INT_BONUS_GOT = 14;

	/** キーを受け付けない回数 */
	public const int DEF_INT_KEY_REJECT = 15;

	/** モード毎のカウンタ（モード変更時に０で初期化） */
	public const int DEF_INT_MODE_COUNTER = 16;

	/** 反復演出更新用カウンタ（０or１） */
	public const int DEF_INT_ON_OFF_EFFECT = 17;

	/**
	 * ﾒｲﾝﾙｰﾌﾟの1ﾀｰﾝのｽﾋﾟｰﾄﾞﾘﾐｯﾄ値[ms] (ADFファイル中の Z-Loop-Time: の値)
	 * 
	 * ﾒｲﾝﾙｰﾌﾟ動作が速い場合は、最低，この値[ms]の待機時間を設ける
	 */
	public const int DEF_INT_LOOP_SPEED = 18; // いろいろな速度調整のベースの値となっています。

	/**
	 * ボーナス入賞時（どんｏｒ７）の状態を保持するフラグ
	 * 
	 * DEF_BB_UNDEF: 通常ゲーム DEF_BB_DON: ドンチャン揃いで入賞 DEF_BB_7: ７揃いで入賞 DEF_RB_IN: ＲＢに入賞
	 * 
	 * ボーナス終了時(DEF_INT_IS_BB_END !	 0)のﾘｰﾙ回転開始時に 通常ゲーム状態に戻す
	 */
	public const int DEF_INT_BB_KIND = 19;

	// //////////////////////////////
	// int_value(ゲーム情報)

	/** BB入賞回数（ゲーム情報） */
	public const int DEF_INT_BIG_COUNT = 20;

	/** RB入賞回数 */
	public const int DEF_INT_REG_COUNT = 21;

	/** ボーナス間の通常ゲーム回数（ゲーム情報） */
	public const int DEF_INT_UNTIL_BONUS_GAMES = 22;

	/** 総回転数（ゲーム情報） */
	public const int DEF_INT_TOTAL_GAMES = 23;

	/** ビッグ最高獲得数（ゲーム情報） */
	public const int DEF_INT_GAME_INFO_MAX_GOT = 24;

	/** コイン補充回数 */
	public const int DEF_INT_COIN_INC_NUM = 25;

	/** ボーナスデータを参照する際の基準点 */
	public const int DEF_INT_BONUS_DATA_BASE = 26;

	/**
	 * ランプの 上位16bitを直前のアクション 下位16bitをアクション
	 */
	public const int DEF_INT_LAMP_1 = 27;

	public const int DEF_INT_LAMP_2 = 28;

	public const int DEF_INT_LAMP_3 = 29;

	/** BETする枚数 */
	public const int DEF_INT_BET_COUNT = 30;

	/** BETした枚数 */
	public const int DEF_INT_BETTED_COUNT = 31;

	/** 貸し出し回数 */
	public const int DEF_INT_NUM_KASIDASI = 32;

	/** 聴牌チェック */
	public const int DEF_INT_IS_TEMPAI = 33;

	/**
	 * 連続演出更新用カウンタ ｹﾞｰﾑのﾓｰﾄﾞが変わっても初期化されたくない
	 */
	public const int DEF_INT_SEQUENCE_EFFECT = 34;

	/** リールの回転速度 */
	public const int DEF_INT_REEL_SPEED = 35;

	/** リールの回転速度パーセント */
	public const int DEF_INT_REEL_SPEED_PER = 36;

	/** デフォルト押し順 */
	public const int DEF_INT_STOP_ORDER = 37;// デフォルト順押し

	/** 前回の停止出目 21(10)=10101(2)なので5bitずつ15bit使います */
	public const int DEF_INT_PREV_GAME = 38;

//	/** 内部的にカウントするｺｲﾝ 実践モードの終了条件で使います */
//	public const int DEF_INT_SLOT_COIN_INNER_COUNT = 39;

	public const int DEF_INT_FLASH_DATA = 40;

	/** 終了条件 */
	public const int DEF_INT_WHAT_EXIT = 41;

	public const int DEF_INT_TOP_LAMP = 42;

	/** 4thリールの角度 */
	public const int DEF_INT_4TH_REEL_ANGLE = 43;

	/** 動作フラグ */
	public const int DEF_INT_4TH_ACTION_FLAG = 44;

	/** アクション */
	public const int DEF_INT_RLPTNDT = 45;

	/** カウンター */
	public const int DEF_INT_4TH_ACTION_COUNTER = 46;

	/** RLPTNDTカウント */
	public const int DEF_INT_RLPTNDT_COUNTER = 47;

	/** RLPTNDTカウント */
	public const int DEF_INT_RLPTNDT_FLAG = 48;
	
	/** RLLカウンター */
	public const int DEF_INT_RLL_COUNTER = 49;

	/** GAMESTを覚えておく */
	public const int DEF_INT_PREV_GAMEST = 50;
	
	/** WINランプ点灯フラグ */
	public const int DEF_INT_WIN_LAMP = 51;
	
	/** 告知するよ */
	public const int DEF_INT_KOKUCHI_X = 52;
	
	public const int DEF_INT_WIN_LAMP_STATUS = 53;

	public const int DEF_INT_KOKUCHI_ID = 54;
	
	public const int DEF_INT_BB_TOTAL_GOT = 55;
	
	public const int DEF_INT_WMLN_HIT = 56;
	
	public const int DEF_INT_WMLN_GOT = 57;
	
	public const int DEF_INT_CHRY_HIT = 58;
	
	public const int DEF_INT_CHRY_GOT = 59;
	
	public const int DEF_INT_FLAG_GAME_COUNT = 60;

	public const int DEF_INT_TOTAL_BET_COUNT = 61;

	public const int DEF_INT_TOTAL_GAIN_COUNT = 62;

	public const int DEF_INT_HAZUSI_COUNT = 63;

	public const int DEF_INT_JAC_HIT = 64;
	public const int DEF_INT_TOTAL_BET = 65;
	public const int DEF_INT_TOTAL_PAY = 66;

	
	/** BB獲得枚数 711枚以上 */
	public const int DEF_INT_BB_GET_OVER711 = 67; // TOBE 個別PARAM

	/**
	 *  BB終了後の１ゲームか？
	 *  
	 *  ボーナス終了ゲームの次ゲームの回転開始(DEF_RMODE_SPIN)で 1
	 *  ↑そのゲームの獲得(DEF_RMODE_RESULT)で 0
	 *  
	 *  @see int_s_value[DEF_INT_IS_BB_RB_END]
	 *  @see int_s_value[DEF_INT_BB_KIND]
	 *  @see DEF_BB_B7
	 *  @see DEF_BB_R7
	 *  
	 */
	public const int DEF_INT_BB_AFTER_1GAME = 68; // TOBE 個別PARAM

	/**
	 * BB終了後の１ゲームでＢＢに入賞したか？（＝揃えたか？）
	 * 
	 * ↑のミッション成立時に1
	 * newSlot()で0
	 */
	public const int DEF_INT_BB_END_1GAME_REGET_BB = 69; // TOBE 個別PARAM

	
	public const int DEF_INT_THIS_FLAG_GAME = 70;

	public const int DEF_INT_BONUS_JAC_GOT = 71;		// GP追加 1回枚のJACゲーム時の払い出し枚数
	
	public const int DEF_INT_SLOT_VALUE_MAX = 72;

	
	
	public const int DEF_EXIT_NON = 0;

	public const int DEF_EXIT_OVER_KASIDASHI = 1;

	public const int DEF_EXIT_OVER_INT = 2;
	
	public const int DEF_EXIT_HALL_NOCOIN = 3;
	
	public const int DEF_EXIT_HALL_CLOSE = 4;

	public const int DEF_EXIT_HALL_HTTP_ERROR = 5;
	
	public const int DEF_EXIT_HALL_SERVER_ERROR = 6;

	public const int DEF_EXIT_HALL_END = 7;
	
	public const int DEF_EXIT_HALL_COIN = 8;

	// スロットゲームモード

	/** 未定義 */
    public const int DEF_RMODE_UNDEF = -1;

	/**
	 * BET待ち
	 */
	public const int DEF_RMODE_WAIT = 0;

	/**
	 * MAXBET
	 */
	public const int DEF_RMODE_BET = 1;

	/**
	 * ﾘｰﾙ回転状態
	 */
	public const int DEF_RMODE_SPIN = 2;
	
	/**
	 * リールフラッシュ
	 */
	public const int DEF_RMODE_FLASH = 3;

	/**
	 * ﾘｰﾙ停止後の結果表示期間 獲得ｺｲﾝ枚数移動処理
	 */
	public const int DEF_RMODE_RESULT = 4;

	/**
	 * BBファンファーレ鳴らす期間
	 */
	public const int DEF_RMODE_BB_FANFARE = 5;

	/**
	 * RBファンファーレ鳴らす期間
	 */
    public const int DEF_RMODE_RB_FANFARE = 6;

    public const int DEF_RMODE_FIN_WAIT = 7;
	
	public const int DEF_RMODE_WIN = 8;

	/**
	 * コイン無しでゲーム終了
	 */
	public const int DEF_RMODE_NO_COIN = 9;

	public const int DEF_RMODE_HTTP = 10;
	
	// //////////////////////////////////////////////////////////////
	// コイン枚数規定値

	/** 開始時の貸し出しコイン枚数(どん２では１０００枚貸し出しスタート) */
	public const int DEF_NUM_START_COIN = 50;

	/** 最大獲得保持コイン枚数 */
	public const int DEF_NUM_MAX_COIN = 99999;

	/** 最大獲得保持コイン枚数 */
	public const int DEF_NUM_MAX_COIN_2 = 999999999;// 下地を書く

	/** クレジット最大保持コイン枚数（５０枚） */
	public const int DEF_NUM_MAX_CREDIT = 50;

	/** 貸し出し回数MAX */
	public const int DEF_NUM_MAX_KASIDASI = 200;

	/** １ゲームで減らすコイン枚数 */
	public const int DEF_NUM_DEC_COIN = 3;

	/** Jacゲームで減らすコイン枚数 */
	public const int DEF_JAC_DEC_COIN = 1;

	/** 実践モードの上限値 */
	public const int DEF_NUM_JISSEN_LIMIT = 99999999;

	/** 分析モードの上限値 */
	public const int DEF_NUM_SIMULATION_LIMIT = 99999;

	/** 分析モードのBB_RB上限値 */
	public const int DEF_NUM_SIMULATION_BB_RB_LIMIT = 99;

	/** ボーナスゲームＭＡＸカウント */
	public const int DEF_NUM_MAX_BONUS_GAMES = 99;

	//
	// /** ボーナスゲーム獲得ＭＡＸ */
	// public const int DEF_NUM_MAX_BONUS_COINS = 999;
	//
	/** 総回転数のＭＡＸ */
	public const int DEF_LIMIT_99999 = 99999;

	/** BB/RB AVERAGEのＭＡＸ値 */
	public const int DEF_BONUS_AVG_MAX = 99999;

	
	/** ボーナス獲得枚数 */
	public const int DEF_BB_GET_711 = 711;	// TOBE 個別PARAM
	
	//	
	// /** ｺｲﾝMAX */
	// public const int DEF_LIMIT_99999999 = 99999999;

	// //////////////////////////////////////////////////////////////
	// リール分割数をかえる（P504とSO504だけ２５分割それ以外は２１分割にする ２００３－０８－０１）

	/** リール回転速度(２５分割)オリジナル */
	// public const int DEF_REEL_MAX_SPEED = 0xA00;
	public const int DEF_REEL_MAX_SPEED = 0xDA7;

	/** リールが1分間に回る回数 */
	public const int DEF_REEL_COUNT_MIN = 80;

	/** リール回転速度(２１分割) */
	// public const int DEF_REEL_MAX_SPEED = 0;xC30
	/** リールすべり最大数 */
	public const int DEF_NUM_SLIDE = 4; //

	/**
	 * アプリでのラインシフト数
	 */
	public const int DEF_NUM_REEL_SHIFT = 7;

	public const int DEF_NUM_4TH_REEL = 6;

	// //////////////////////////////////////////////////////////////
	// ランプ
	/** ランプBIT */
	public const int DEF_LAMP_BIT = 16;

	/** マスク */
    // TODO 不明な定数 LAMP_BIT
    //public const int DEF_LAMP_MASK = (1 << DfOmatsuri_v4.LAMP_BIT) - 1;

	public const int DEF_LAMP_ACTION_OFF = 0;

	public const int DEF_LAMP_ACTION_ON = 1;

	public const int DEF_LAMP_STATUS_OFF = 0;

	// public const int DEF_LAMP_STATUS_MID = 1;

	public const int DEF_LAMP_STATUS_ON = 1;

	// #define DEF_SIDE_LAMP_OFF 	 -1
	//
	// public const int DEF_SIDE_LAMP_ON = 0;

	/**
	 * 筐体左(K2) 0-5
	 */
	public const int DEF_LAMP_BET_1 = 0;
	public const int DEF_LAMP_BET_2 = 1;
	public const int DEF_LAMP_BET_3 = 2;
	public const int DEF_LAMP_BET_4 = 3;
	public const int DEF_LAMP_BET_5 = 4;

	/**
	 * 筐体上(K0) 5-9
	 */
	public const int DEF_LAMP_TOP_1 = 5;
	public const int DEF_LAMP_TOP_2 = 6;
	public const int DEF_LAMP_TOP_3 = 7;
	public const int DEF_LAMP_TOP_4 = 8;
	public const int DEF_LAMP_TOP_5 = 9;

	/**
	 * 筐体右(K2) 10: 確定ランプ上 11:確定ランプ下 12:再遊技 13:スタート 14:インサート
	 */
	public const int DEF_LAMP_WIN = 10;
	public const int DEF_LAMP_BAR = 11;
	public const int DEF_LAMP_FRE = 12;
	public const int DEF_LAMP_STA = 13;
	public const int DEF_LAMP_INS = 14;

	/**
	 * 筐体下(K2) 15:MAXBET 16:チャンス
	 */
	public const int DEF_LAMP_MAXBET = 15;
	public const int DEF_LAMP_CHANCE = 16;

	/**
	 * 筐体ボタン(K3) 17:レバー 18:左 19:中 20:右
	 */
	public const int DEF_LAMP_LEVER = 17;
	public const int DEF_LAMP_BUTTON_L = 18;
	public const int DEF_LAMP_BUTTON_C = 19;
	public const int DEF_LAMP_BUTTON_R = 20;

	/**
	 * 筐体サイドランプ(K1) 21-26
	 */
	public const int DEF_LAMP_S1 = 21;
	public const int DEF_LAMP_S2 = 22;
	public const int DEF_LAMP_S3 = 23;
	public const int DEF_LAMP_S4 = 24;
	public const int DEF_LAMP_S5 = 25;
	public const int DEF_LAMP_S6 = 26;
	/** 4thリール */
	public const int DEF_LAMP_4TH = 27;

	// //////////////////////////////////////////////////////////////
	// 役ビット

	/** ﾁｪﾘｰ(一般) */
	public const int DEF_HITFLAG_NR_CHERRY =	 (1 << 0);

	/** 15枚(BB中) */
	public const int DEF_HITFLAG_BB_15 = 	 (1 << 0);

	/** ﾊﾅﾋﾞ */
	public const int DEF_HITFLAG_NR_HANABI =	 (1 << 1);

	/** 大山 */
	public const int DEF_HITFLAG_NR_OOYAMA =	 (1 << 2);

	/** ﾁｪﾘｰ(BB中) */
	public const int DEF_HITFLAG_BB_CHERRY =	 (1 << 2);

	/** RP */
	public const int DEF_HITFLAG_NR_REPLAY =	 (1 << 3);

	/** RB(BB中) */
	public const int DEF_HITFLAG_BB_RB =	 (1 << 3);

	/** JAC(RB中) */
	public const int DEF_HITFLAG_RB_JAC =	 (1 << 3);

	/** RB */
	public const int DEF_HITFLAG_NR_RB =	 (1 << 4);

	/** BB */
	public const int DEF_HITFLAG_NR_BB =	 (1 << 5);

	// TOBE [カテゴリ]テーブルID
	/** 小役確率テーブル（通常） */
	public const int DEF_PROB_KOYAKU_NORMAL = 0;

	/** 小役確率テーブル（高確率） */
	public const int DEF_PROB_KOYAKU_HIGH = 1;

	// //////////////////////////////////////////////////////////////
	// KEYビット
	/** 生臭キー */
    // TODO 非DEFのKEY_BITが存在しないのでコメントアウト
    //public const int  DEF_KEY_BIT_LAZY = DfKey.KEY_BIT_5 | KEY_BIT_SELECT;

    // TODO 非DEFのKEY_BITが存在しないのでコメントアウト
    //public const int DEF_KEY_START = KEY_BIT_1 | KEY_BIT_2 | KEY_BIT_3 | KEY_BIT_4 | KEY_BIT_5 | KEY_BIT_6 | KEY_BIT_SELECT;

	/**
	 * ボーナスパネルデータ.
	 * 
	 * 配列のインデックスとしても使用するので、負値はダメです.
	 * 
	 * @see Mobile#panel_colors privateだから見えないですが.
	 */
	public const int DEF_GAME_NONE = 0;
	public const int DEF_GAME_NORMAL = 1;
	public const int DEF_GAME_BIG = 2;
	public const int DEF_GAME_REG = 3;
	public const int DEF_GAME_CURRENT = 4;
	public const int DEF_GAME_NUM = 5;

	public const int DEF_INFO_GAME_HISTORY = 10;
	public const int DEF_INFO_GAMES = 8;

	public const int DEF_UNIT_GAMES = 100;

	// //////////////////////////////////////////////////////////////
	// 小役カウンタ

	/** 小役カウンタ制御値 */
	public const int DEF_COYAKU_COUNTER_GRADIENT = 96;

	/** 小役カウンタ固定値 */
	public const int DEF_COYAKU_COUNTER_MAX = 256;

	// //////////////////////////////////////////////////////////////
	// ボーナス入賞種別 (int_s_value[INT_BB_DON_7])

	/** 通常ゲーム */
	public const int DEF_BB_UNDEF = 0;

	/** 赤七揃いで入賞 */
	public const int DEF_BB_R7 = 1;

	/** 青７揃いで入賞 */
	public const int DEF_BB_B7 = 2;

	/** RB入賞 */
	public const int DEF_RB_IN = 3;

	/** BB最大数 */
	public const int DEF_INIT_BIG_GAMES = 30;

	/** Jac-in最大数 */
	public const int DEF_INIT_JAC_IN_GAMES = 3;

	/**
	 * JACゲーム最大数 (JAC-CUT処理では100%あたりなのでこの値を気にすることはない)
	 */
	public const int DEF_INIT_JAC_GAMES = 8;

	// //////////////////////////////////////////////////////////////
	// 時間調整
	/** 点滅ランプスピード調整（ms） */
	public const int DEF_WAIT_ON_OFF_LAMP = 150;

	/** キー受付ない時間調整（ms） */
	public const int DEF_WAIT_KEY_REJECT = 50;

	/** ゲーム間 */
	public const int DEF_WAIT_GAME = 4000;

	/** ランプ */
	public const int DEF_WAIT_LAMP = 30000;

	/** スピン */
	public const int DEF_WAIT_SPIN = 40000;

	/** ＪＡＣカット中のキー受付ない時間調整（ms） */
    // TODO : WAIT_KEY_REJECTが存在しないのでコメントアウト
	//public const int DEF_WAIT_KEY_REJECT_JAC = WAIT_KEY_REJECT * 2;

	/** 上部ランプ点滅スピード調整（ms） */
	public const int DEF_WAIT_UPPER_LAMP = 50 + 40;

	/** リールフラッシュスピード調整（ms） */
	public const int DEF_WAIT_FLASH = 100 + 20;

	/** コイン増加スピード */
	public const int DEF_WAIT_COUNT_UP = 75;

	/** コイン増加スピード */
	public const int DEF_WAIT_BET_UP = 75;

	/** 透過矩形処理方式 */
	public const int DEF_INK_HALF = 0;
	/** 透過矩形処理方式 */
	public const int DEF_INK_ADD = 1;
	/** 透過矩形処理方式 */
	public const int DEF_INK_SUB = 2;

	public const int DEF_TRIAL_GAME = 20;



}

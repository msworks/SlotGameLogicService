public partial class Defines {

/*
 * Created on 2003/08/19
 */

/**
 * モバイルアプリのための定義をします
 * 
 * @author A03605WO
 */

	/** スクロールメニューで描く各上下の数 */
	public const int DEF_MENU_DEPTH = 2;

	/*
	 * モバイルアプリモード
	 */
	/** 未定義 */
    public const int DEF_MODE_UNDEF = -1;

	/** タイトル画面表示 */
    public const int DEF_MODE_TITLE = Defines.DEF__0001B;

	/** スロット実行中 */
    public const int DEF_MODE_RUN = Defines.DEF__0010B;

	/** サーバアクセス中 */
    public const int DEF_MODE_SERVER = Defines.DEF__0011B;

	/** サーバアクセス終了 */
    public const int DEF_MODE_SERVER_DONE = Defines.DEF__0100B;

	/** サーバアクセス失敗 */
    public const int DEF_MODE_SERVER_ERR = Defines.DEF__0101B;

	/** 規定終了 */
    public const int DEF_MODE_OVER = Defines.DEF__0110B;

	/** HALLエラー */
    public const int DEF_MODE_HALL_ERROR = Defines.DEF__0111B;

	/** HALL再起動確認 */
    public const int DEF_MODE_HALL_REPLAY = Defines.DEF__1000B;

	/** HALLコイン貸出確認 */
    public const int DEF_MODE_HALL_COIN = Defines.DEF__1001B;

	/** HALL王国タイムアウト */
    public const int DEF_MODE_HALL_TIMEOUT = Defines.DEF__1010B;

	/** HALL王国初回起動時注意 */
    public const int DEF_MODE_HALL_NOTICE = Defines.DEF__1011B;

	/** HALL王国ﾌﾞﾗｳｻﾞ起動注意 */
    public const int DEF_MODE_HALL_BRAWSER = Defines.DEF__1100B;

	/** maskメニュー以外 (0000 1111) */
    public const int DEF_MODE_NORMAL_BITS = Defines.DEF__00001111B;

	/*
	 * モバイルアプリサブモード
	 */

	/** メニュー表示モード (0001 0000) */
    public const int DEF_MODE_MENU_BIT = Defines.DEF__00010000B;

	/** ヘルプ表示モード (0010 0000) */
    public const int DEF_MODE_HELP_BIT = Defines.DEF__00100000B;

	/** ゲーム情報表示モード (0011 0000) */
    public const int DEF_MODE_INFO_BIT = Defines.DEF__00110000B;

	/** 公式サイト表示モード (0100 0000) */
    public const int DEF_MODE_WEB_BIT = Defines.DEF__01000000B;

	/** タイトル戻る確認表示モード */
    public const int DEF_MODE_TITLE_BIT = Defines.DEF__01010000B;

	/** 終了確認表示モード */
    public const int DEF_MODE_EXIT_BIT = Defines.DEF__01100000B;

	/** 送信確認表示モード */
    public const int DEF_MODE_CAL_BIT = Defines.DEF__01110000B;

	/** 送信確認表示モード */
    public const int DEF_MODE_INIT_BIT = Defines.DEF__10000000B;

	/** maskメニュービット (11110000) */
    public const int DEF_MODE_MENUS_BITS = Defines.DEF__11110000B;

	/*
	 * int変数(int_m_value[])
	 */
	/** コイン枚数 */
	public const int DEF_INT_COIN_NUM = 0;

	/** リクエストされたモード */
	public const int DEF_INT_MODE_REQUEST = 1;

	/** 現在のモード */
	public const int DEF_INT_MODE_CURRENT = 2;

	/** メニュー項目選択 */
	public const int DEF_INT_MENU_ITEM = 3;

	/** ヘルプページ表示カーソル位置 */
	public const int DEF_INT_HELP_PAGE = 4;

	/** TOBE 消す.オフセット基準点 X */
	public const int DEF_INT_BASE_OFFSET_X = 5;

	/** TOBE 消す.オフセット基準点 Y */
	public const int DEF_INT_BASE_OFFSET_Y = 6;

	/** タイトル背景の炎演出 */
	public const int DEF_INT_TITLE_BG_START = 7;

	/**
	 * ゲームモード ↓取りうる値
	 * 
	 * @see GMODE_UNDEF 未定義
	 * @see GMODE_GAME 通常ゲーム
	 * @see GMODE_SIMULATION シミュレーションモード
	 * @see GMODE_BATTLE バトスロ
	 */
	public const int DEF_INT_GMODE = 8;

	/**
	 * サウンド音量
	 * 
	 * @see SELECT_6_0:5
	 * @see SELECT_6_1:4
	 * @see SELECT_6_2:3
	 * @see SELECT_6_3:2
	 * @see SELECT_6_4:1
	 * @see SELECT_6_5:off
	 * 
	 */
	public const int DEF_INT_VOLUME = 10;

    /// <summary>
    /// データパネル
    /// @see SELECT_2_ON:on
    /// @see SELECT_2_OFF:off
    /// </summary>
	public const int DEF_INT_IS_DATAPANEL = 11;

	/**
	 * JACカット
	 *  @see SELECT_2_ON:on
	 *  @see SELECT_2_OFF:off
	 */
	public const int DEF_INT_IS_JACCUT = 12;

	/**
	 *  小役告知
	 *  @see SELECT_3_PREV
	 *  @see SELECT_3_AFTER
	 *  @see SELECT_3_OFF
	 */
	public const int DEF_INT_KOKUCHI = 13;

	/**
	 * 目押し
	 *  @see SELECT_2_ON:on
	 *  @see SELECT_2_OFF:off
	 */
	public const int DEF_INT_IS_MEOSHI = 14;


    /// <summary>
    /// 押し順
    /// @see SELECT_6_0:0-1-2
    /// @see SELECT_6_1:2-1-0
    /// @see SELECT_6_2:0-2-1
    /// @see SELECT_6_3:1-2-0
    /// @see SELECT_6_4:2-0-1
    /// @see SELECT_6_5:1-0-2
    /// </summary>
	public const int DEF_INT_ORDER = 15;

	/**
	 * リールスピード
	 *  @see SELECT_5_0:5
	 *  @see SELECT_5_1:4
	 *  @see SELECT_5_2:3
	 *  @see SELECT_5_3:2
	 *  @see SELECT_5_4:1
	 */
	public const int DEF_INT_SPEED = 16;

	/**
	 *  メニューが押せる
	 *  @see MENU_AVAILABLE
	 *  @see MENU_UNAVAILABLE
     */
	public const int DEF_INT_IS_MENU_AVAILABLE = 17;

	/** 設定値(０～５) */
	public const int DEF_INT_SETUP_VALUE = 18;

	/** 設定値(０～５+?) */
	public const int DEF_INT_SETUP_VALUE_CURSOL = 19;

	/** メニュースクロール角度 */
	public const int DEF_INT_SCROLL = 20;

	/** サブメニュー項目選択 */
	public const int DEF_INT_SUB_MENU_ITEM = 21;
	
	/** サブメニューコントローラ 0で閉じる、５で開く*/
	public const int DEF_INT_SUB_MENU_CTRL = 22;
	
	/** ダイアログとタイトルのカーソル位置 */
	public const int DEF_INT_DIALOG_CURSOL = 24;

	/** 演出ｶｳﾝﾀｰ */
	public const int DEF_INT_COUNTER = 25;

	/** 演出ｶｳﾝﾀｰ */
	public const int DEF_INT_YAJIRUSI_ON_OFF = 26;

	/** 音のON，Off */
	public const int DEF_INT_IS_SOUND = 27;

	/** 音のON，Off */
	public const int DEF_INT_VOLUME_KEEP = 28;

	/** バイ部on/off */
	public const int DEF_INT_IS_VIBRATION = 29;

	/** int変数(int_m_value[])の個数 */
	public const int DEF_INT_M_VALUE_MAX = 30;

	/** メニュースクロール値（θ見たいな感じ） */
    public const int DEF_MENU_SCROLL_UNIT = Defines.DEF_POS_MENU_DY;

	// ヘルプ画面用

	/** ヘルプ画面背景色 R */
	public const int DEF_HELP_BG_R = 0x00;

	/** ヘルプ画面背景色 G */
	public const int DEF_HELP_BG_G = 0x00;

	/** ヘルプ画面背景色 B */
	public const int DEF_HELP_BG_B = 0x00;

	/** ヘルプ画面用設定値 */
	public const int DEF_HELP_CHAR_X_NUM = 8;

	/** ヘルプ画面用設定値 */
	public const int DEF_HELP_CHAR_Y_NUM = 16;

	/** Game Info Data */
	public const int DEF_GAME_INFO_DATA_TITLE = 0;

	/** Game Info Data */
	public const int DEF_GAME_INFO_DATA_UNIT = 1;

	/** Game Info Data */
	public const int DEF_GAME_INFO_DATA_BOX = 2;

	/** ゲームデータＢＢ ＡＶＥＲＡＧＥ */
	public const int DEF_GAME_INFO_BB_AVG = 0;

	/** ゲームデータＲＢ ＡＶＥＲＡＧＥ */
	public const int DEF_GAME_INFO_RB_AVG = 1;

	/** ゲームデータＢＢ最高獲得枚数 */
	public const int DEF_GAME_INFO_BB_GOT_MAX = 2;

	/** 目押しサポート */
	/** データパネル表示 */
	/** JACカット */
	public const int DEF_SELECT_2_ON = 0;

	public const int DEF_SELECT_2_OFF = 1;

	public const int DEF_SELECT_2 = 2;// 項目数

	/** 告知 */
	public const int DEF_SELECT_3_PREV = 0;

	public const int DEF_SELECT_3_AFFTER = 1;

	public const int DEF_SELECT_3_OFF = 2;

	public const int DEF_SELECT_3 = 3;// 項目数

	/** 押し順 別途Slotクラスの配列もこの並びで */
	/** ヴォリューム */
	public const int DEF_SELECT_6_0 = 0;

	public const int DEF_SELECT_6_1 = 1;

	public const int DEF_SELECT_6_2 = 2;

	public const int DEF_SELECT_6_3 = 3;

	public const int DEF_SELECT_6_4 = 4;

	public const int DEF_SELECT_6_5 = 5;

	public const int DEF_SELECT_6 = 6;// 項目数

	public const int DEF_VOLUME_UNIT = 20;// 20%ずつ変化します

	/** スピード */
	public const int DEF_SELECT_5_0 = 0;

	public const int DEF_SELECT_5_1 = 1;

	public const int DEF_SELECT_5_2 = 2;

	public const int DEF_SELECT_5_3 = 3;

	public const int DEF_SELECT_5_4 = 4;

	public const int DEF_SELECT_5 = 5;

	public const int DEF_SPEED_UNIT = 20;// 20%ずつ変化します

	/** メニューボタン動作可否 */
	public const int DEF_MENU_AVAILABLE = 0;

	public const int DEF_MENU_UNAVAILABLE = 1;

	/** 分析モード初期値 */
	public const int DEF_GMODE_UNDEF = 0;

	public const int DEF_GMODE_GAME = 1;

	public const int DEF_GMODE_SIMURATION = 2;

	public const int DEF_GMODE_BATTLE = 3;

	public const int DEF_GMODE_TRIAL = 4;
	
	public const int DEF_GMODE_HALL = 5;

	public const int DEF_GMODE_NUM = 6;

	/** SOFTキー */
	public const int DEF_SOFT_KEY1 = 0;
	public const int DEF_SOFT_KEY2 = 1;
	
	public const string DEF_SOFT_KEY_MENU =	 "ﾒﾆｭｰ";
	public const string DEF_SOFT_KEY_BACK =	 "戻る";
	public const string DEF_SOFT_KEY_NONE =	 "";
	
	/** 設定 */
	public const int DEF_SETUP_MAX = 7;
	
	/** サブメニュー状態 */
	public const int DEF_SUB_MENU_CLOSE = 0;
	public const int DEF_SUB_MENU_OPEN = 5;
	
	/** カーソル位置 */
	public const int DEF_CURSOL_UP = 0;
	public const int DEF_CURSOL_DOWN = 1;

	/** 緑ドンから追加 */
	// スクラッチパッド
	public const int DEF_SAVE_WRITTEN = 0;	// データ保存してあるか
	public const int DEF_SAVE_REEL_SPEED = 1;
	public const int DEF_SAVE_ORDER = 2;
	public const int DEF_SAVE_MEOSHI = 3;
	public const int DEF_SAVE_DATAPANEL = 4;
	public const int DEF_SAVE_VOLUME = 5;
	public const int DEF_SAVE_JACCUT = 6;
	public const int DEF_SAVE_KOKUCHI = 7;
	public const int DEF_SAVE_VIBRATION = 8;
	public const int DEF_SAVE_HTTP_TIME0 = 9;
	public const int DEF_SAVE_HTTP_TIME1 = 10;
	public const int DEF_SAVE_HTTP_TIME2 = 11;
	public const int DEF_SAVE_HTTP_TIME3 = 12;
	public const int DEF_SAVE_KASIDASI_0 = 13;
	public const int DEF_SAVE_KASIDASI_1 = 14;
	public const int DEF_SAVE_KASIDASI_2 = 15;
	public const int DEF_SAVE_KASIDASI_3 = 16;
	public const int DEF_SAVE_HALL_PARAM = 17;

}

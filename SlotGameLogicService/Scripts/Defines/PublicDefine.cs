#if __GP_TRACE__
	#define TRACE_ON
#endif

using System;
using System.Threading;
public class PublicDefine {

	public const int DEBUG_PRI = 57;
#if __GP_TRACE__
	public static void PRINT(object a)      {} //System.out.println(a)
	public static void PRINTLN(object a)    {} //System.out.print(a)
	public static void DEBUGOUT(object a)   {} //setMessageDebug(a)
	public static void PRINT_PRI(int p, object a) {if(p==DEBUG_PRI) Console.WriteLine(a.ToString());}
//	#define SYNCCON_OFF
#else
	public static void PRINT(object a) {}
    public static void PRINTLN(object a) { }
    public static void DEBUGOUT(object a) { }
    public static void PRINT_PRI(object p, object a) { }
#endif

    public static void SLEEP(int a) {
        try { Thread.Sleep(a); } catch (Exception e) { }
    }
	public const int NOVALUE = 0;
	public const int REA_SCENE_MAX = 8;

#if _DOCOMO
	public const string IMAGETYPE = ".gif";
#else
	public const string IMAGETYPE = ".png";
#endif
	public const int TRUE = 1;
	public const int FALSE = 0;

	public const string COPYRIGHT = "(C)UNIVERSAL ENTERTAINMENT";

	// 内部モード
	public const int GP_MODE_INIT      =   0;
	public const int GP_MODE_PRELOAD   =   1;
	public const int GP_MODE_GREEPACHI =   2;
	public const int GP_MODE_RESLOAD   =   3;
	public const int GP_MODE_USEITEM   =   4;
	public const int GP_MODE_ADWAIT    =   5;
	public const int GP_MODE_GAME      =   6;
	public const int GP_MODE_KILL      =   7;

	// 内部キー設定で使う値
#if	_DOCOMO	// {
    // TODO C#移植 一旦コメントアウト
    //public const int GP_KEY_0        = ( Display.KEY_0 );        // 0
    //public const int GP_KEY_1        = ( Display.KEY_1 );        // 1
    //public const int GP_KEY_2        = ( Display.KEY_2 );        // 2
    //public const int GP_KEY_3        = ( Display.KEY_3 );        // 3
    //public const int GP_KEY_4        = ( Display.KEY_4 );        // 4
    //public const int GP_KEY_5        = ( Display.KEY_5 );        // 5
    //public const int GP_KEY_6        = ( Display.KEY_6 );        // 6
    //public const int GP_KEY_7        = ( Display.KEY_7 );        // 7
    //public const int GP_KEY_8        = ( Display.KEY_8 );        // 8
    //public const int GP_KEY_9        = ( Display.KEY_9 );        // 9
    //public const int GP_KEY_SELECT   = ( Display.KEY_SELECT );   // SELECT
    //public const int GP_KEY_UP       = ( Display.KEY_UP );       // UP
    //public const int GP_KEY_DOWN     = ( Display.KEY_DOWN );     // DOWN
    //public const int GP_KEY_LEFT     = ( Display.KEY_LEFT );     // LEFT
    //public const int GP_KEY_RIGHT    = ( Display.KEY_RIGHT );    // RIGHT
    //public const int GP_KEY_SOFT1    = ( Display.KEY_SOFT1 );    // SOFT1
    //public const int GP_KEY_SOFT2    = ( Display.KEY_SOFT2 );    // SOFT2
    //public const int GP_KEY_ASTERISK = ( Display.KEY_ASTERISK ); // ASTERISK
    //public const int GP_KEY_POUND    = ( Display.KEY_POUND );    // POUND

#else // } {
/*
	public const int GP_KEY_0 = 0x00000001; // 〇
	public const int GP_KEY_1 = 0x00000002; // ①
	public const int GP_KEY_2 = 0x00000004; // ②
	public const int GP_KEY_3 = 0x00000008; // ③
	public const int GP_KEY_4 = 0x00000010; // ④
	public const int GP_KEY_5 = 0x00000020; // ⑤
	public const int GP_KEY_6 = 0x00000040; // ⑥
	public const int GP_KEY_7 = 0x00000080; // ⑦
	public const int GP_KEY_8 = 0x00000100; // ⑧
	public const int GP_KEY_9 = 0x00000200; // ⑨
	
	public const int GP_KEY_SELECT = 0x00010000; // ◎
	public const int GP_KEY_UP = 0x00001000; // ↑
	public const int GP_KEY_DOWN = 0x00008000; // ↓
	public const int GP_KEY_LEFT = 0x00002000; // ←
	public const int GP_KEY_RIGHT = 0x00004000; // →
	public const int GP_KEY_SOFT1 = 0x00020000; 
	public const int GP_KEY_SOFT2 = 0x00040000; 
	
	public const int GP_KEY_ASTERISK = 0x00000400; // *
	public const int GP_KEY_POUND = 0x00000800; // #
*/
	public const int GP_KEY_0 = 0x00000000; // 〇
	public const int GP_KEY_1 = 0x00000001; // ①
	public const int GP_KEY_2 = 0x00000002; // ②
	public const int GP_KEY_3 = 0x00000003; // ③
	public const int GP_KEY_4 = 0x00000004; // ④
	public const int GP_KEY_5 = 0x00000005; // ⑤
	public const int GP_KEY_6 = 0x00000006; // ⑥
	public const int GP_KEY_7 = 0x00000007; // ⑦
	public const int GP_KEY_8 = 0x00000008; // ⑧
	public const int GP_KEY_9 = 0x00000009; // ⑨
	
	public const int GP_KEY_SELECT = 0x00000014; // ◎
	public const int GP_KEY_UP = 0x00000011; // ↑
	public const int GP_KEY_DOWN = 0x00000013; // ↓
	public const int GP_KEY_LEFT = 0x00000010; // ←
	public const int GP_KEY_RIGHT = 0x00000012; // →
	public const int GP_KEY_SOFT1 = 0x00000015; 
	public const int GP_KEY_SOFT2 = 0x00000016; 
	
	public const int GP_KEY_ASTERISK = 0x0000000a; // *
	public const int GP_KEY_POUND = 0x0000000b; // #


#endif			// }
	// 画像リソース番号(01)
	public const int GP_OPEN0             =   0; // BG
	public const int GP_OPEN1             =   1; // 下部BAR
	public const int GP_MSG_WINDOW        =   2; // ウィンドウBG
	public const int GP_CR0               =   3; // 時計ｱﾆﾒ
//	public const int GP_CR1               =   4;
//	public const int GP_CR2               =   5;
//	public const int GP_CR3               =   6;
//	public const int GP_CR4               =   7;
	public const int GP_COMM_OFF          =   8;
	public const int GP_COMM_ON           =   9;
	public const int GP_LOADING_OFF       =  10;
	public const int GP_LOADING_CAR       =  11;
	public const int GP_LOADING_MAX       =  12;

	// 画像リソース番号(02)
	public const int GP_DATP_FRM_BONUS_D  =   0; // 
	public const int GP_DATP_FRM_BONUS_S  =   1; // 
	public const int GP_DATP_FRM_ROT      =   2; // 
	public const int GP_DATP_SEG_0        =   3; // 
	public const int GP_DATP_SEG_1        =   4; // 
	public const int GP_DATP_SEG_2        =   5; // 
	public const int GP_DATP_SEG_3        =   6; // 
	public const int GP_DATP_SEG_4        =   7; // 
	public const int GP_DATP_SEG_5        =   8; // 
	public const int GP_DATP_SEG_6        =   9; // 
	public const int GP_DATP_SEG_7        =  10; // 
	public const int GP_DATP_SEG_8        =  11; // 
	public const int GP_DATP_SEG_9        =  12; // 
	public const int GP_DATP_SEG_OFF      =  13; // 
	public const int GP_DATP_TXT_ART      =  14; // 
	public const int GP_DATP_TXT_AT       =  15; // 
	public const int GP_DATP_TXT_ATARI    =  16; // 
	public const int GP_DATP_TXT_BB       =  17; // 
	public const int GP_DATP_TXT_COIN     =  18; // 
	public const int GP_DATP_TXT_CT       =  19; // 
	public const int GP_DATP_TXT_KAKU     =  20; // 
	public const int GP_DATP_TXT_RB       =  21; // 
	public const int GP_DATP_TXT_ROT      =  22; // 
	public const int GP_DATP_TXT_RT       =  23; // 
	public const int GP_ITM_BODY          =  24; // 
	public const int GP_ITM_COL_CUR       =  25; // 
	public const int GP_ITM_COLUMN        =  26; // 
	public const int GP_ITM_ITEM_OFF      =  27; // 
	public const int GP_ITM_ITEM_ON       =  28; // 
	public const int GP_ITM_LIMIT_EMP     =  29; // 
	public const int GP_ITM_LIMIT_FLL     =  30; // 
	public const int GP_ITM_LIMIT_OFF     =  31; // 
	public const int GP_ITM_NAVI_DOWN     =  32; // 
	public const int GP_ITM_NAVI_UP       =  33; // 
	public const int GP_ITM_NUM_0         =  34; // 
	public const int GP_ITM_NUM_1         =  35; // 
	public const int GP_ITM_NUM_2         =  36; // 
	public const int GP_ITM_NUM_3         =  37; // 
	public const int GP_ITM_NUM_4         =  38; // 
	public const int GP_ITM_NUM_5         =  39; // 
	public const int GP_ITM_NUM_6         =  40; // 
	public const int GP_ITM_NUM_7         =  41; // 
	public const int GP_ITM_NUM_8         =  42; // 
	public const int GP_ITM_NUM_9         =  43; // 
	public const int GP_ITM_NUM_SLA       =  44; // 
	public const int GP_LAMP_AUTO_EMP     =  45; // 
	public const int GP_LAMP_AUTO_MAX     =  46; // 
	public const int GP_LAMP_AUTO_OFF     =  47; // 
	public const int GP_LAMP_BCUT_EMP     =  48; // 
	public const int GP_LAMP_BCUT_MA      =  49; // 
	public const int GP_LAMP_BCUT_OFF     =  50; // 
	public const int GP_LAMP_BONUS_NA     =  51; // 
	public const int GP_LAMP_BONUS_OFF    =  52; // 
	public const int GP_LAMP_BONUS_ON     =  53; // 
	public const int GP_LAMP_BRING_OFF    =  54; // 
	public const int GP_LAMP_BRING_BLUE   =  55; // 
	public const int GP_LAMP_BRING_YELLOW =  56; // 
	public const int GP_LAMP_BRING_GREEN  =  57; // 
	public const int GP_LAMP_BRING_RED    =  58; // 
	public const int GP_LAMP_BRING_GOLD   =  59; // 
	public const int GP_LAMP_SET_1_OFF    =  60; // 
	public const int GP_LAMP_SET_1_ON     =  61; // 
	public const int GP_LAMP_SET_2_OFF    =  62; // 
	public const int GP_LAMP_SET_2_ON     =  63; // 
	public const int GP_LAMP_SET_3_OFF    =  64; // 
	public const int GP_LAMP_SET_3_ON     =  65; // 
	public const int GP_LAMP_SET_4_EMP    =  66; // 
	public const int GP_LAMP_SET_4_FULL   =  67; // 
	public const int GP_LAMP_SET_4_OFF    =  68; // 
	public const int GP_LAMP_SET_4_ON     =  69; // 
	public const int GP_LAMP_SET_5_EMP    =  70; // 
	public const int GP_LAMP_SET_5_FULL   =  71; // 
	public const int GP_LAMP_SET_6_EMP    =  72; // 
	public const int GP_LAMP_SET_6_FULL   =  73; // 
	public const int GP_LAMP_SET_7_EMP    =  74; // 
	public const int GP_LAMP_SET_7_FULL   =  75; // 
	public const int GP_LAMP_SET_F_EMP    =  76; // 
	public const int GP_LAMP_SET_F_FULL   =  77; // 
	public const int GP_LAMP_SET_H_EMP    =  78; // 
	public const int GP_LAMP_SET_H_FULL   =  79; // 
	public const int GP_LAMP_SET_RND_NA   =  80; // 
	public const int GP_LAMP_SET_RND_OFF  =  81; // 
	public const int GP_LAMP_SET_RND_ON   =  82; // 
	public const int GP_ADD_COIN          =  83; // 
	public const int GP_MARQ_BG           =  84; // 
	public const int GP_NOW_COMM_0        =  85; // 
	public const int GP_NOW_COMM_1        =  86; // 
	public const int GP_OPT_CUR_LOFF      =  87; // 
	public const int GP_OPT_CUR_LON       =  88; // 
	public const int GP_OPT_CUR_ROFF      =  89; // 
	public const int GP_OPT_CUR_RON       =  90; // 
	public const int GP_OPT_CUR_SEL       =  91; // 
	public const int GP_OPT_CUR_UPD       =  92; // 
	public const int GP_OPT_HIST_BODY     =  93; // 
	public const int GP_OPT_HIST_CUR      =  94; // 
	public const int GP_OPT_ICON_OFF      =  95; // 
	public const int GP_OPT_ICON_ON       =  96; // 
	public const int GP_OPT_NOTE          =  97; // 
	public const int GP_OPT_PSH_ARR       =  98; // 
	public const int GP_OPT_PSH_L         =  99; // 
	public const int GP_OPT_PSH_M         = 100; // 
	public const int GP_OPT_PSH_R         = 101; // 
	public const int GP_OPT_SET_BODY      = 102; // 
	public const int GP_OPT_SLUMP_BODY    = 103; // 
	public const int GP_OPT_TIT_BREW      = 104; // 
	public const int GP_OPT_TIT_DES       = 105; // 
	public const int GP_OPT_TIT_HIS       = 106; // 
	public const int GP_OPT_TIT_ITEM      = 107; // 
	public const int GP_OPT_TIT_PLAY      = 108; // 
	public const int GP_OPT_TIT_SET       = 109; // 
	public const int GP_OPT_TIT_SLUMP     = 110; // 
	public const int GP_OPT_TXT_OFF       = 111; // 
	public const int GP_OPT_TXT_ON        = 112; // 
	public const int GP_OPT_TXT_PUSH_D    = 113; // 
	public const int GP_OPT_TXT_PUSH_E    = 114; // 
	public const int GP_OPT_VOL_0         = 115; // 
	public const int GP_OPT_VOL_1         = 116; // 
	public const int GP_OPT_VOL_2         = 117; // 
	public const int GP_OPT_VOL_3         = 118; // 
	public const int GP_OPT_VOL_4         = 119; // 
	public const int GP_OPT_VOL_5         = 120; // 
	public const int GP_OPT_VOL_M         = 121; // 
	public const int GP_POW_MAX           = 122; // 
	public const int GP_POW_OFF           = 123; // 
	public const int GP_OPT_TXT_BONUS     = 124; // 
	public const int GP_OPT_TXT_JAC_E     = 125; // 
	public const int GP_OPT_TXT_JAC_D     = 126; // 
	public const int GP_LAMP_SET_UP1_OFF  = 127; // 
	public const int GP_LAMP_SET_UP1_ON   = 128; // 
	public const int GP_LAMP_SET_UP2_OFF  = 129; // 
	public const int GP_LAMP_SET_UP2_ON   = 130; // 
	public const int GP_LAMP_SET_UP3_OFF  = 131; // 
	public const int GP_LAMP_SET_UP3_ON   = 132; // 
	public const int GP_LAMP_SET_UP4_OFF  = 133; // 
	public const int GP_LAMP_SET_UP4_ON   = 134; // 
	public const int REEL                 = 135; // 
	public const int BONUS01              = 136; // 

	public const int GP_RES1_MAX          = 137; // 読み込みリソースファイル数
	public const int GP_RES2_MAX          = 204; // 読み込みリソースファイル数

	//--------------------------------------------------------------------------
	// proc define
	//--------------------------------------------------------------------------
	public const int GPH_NOPROCESS      =   0; // 動作に影響を及ぼさない

	// その他
	public const int GPH_01_RESCHECK    =  10; // 01 レスポンスチェック

	// プロセス
	// ↓起動フロー
	public const int GPH_START           =   1; // 
	public const int GPH_PRE_IMAGELOAD   =  20; // バラパーツの読み込み
	public const int GPH_IMAGELOAD       =  30; // バラパーツの読み込み

	public const int GPH_CON_AUTH        =  40; // 認証コマンド
	public const int GPH_CON_HALLDATA    =  60; // 台情報取得
	public const int GPH_CON_ADINFO      =  70; // 広告情報取得
	public const int GPH_CON_USEITEM     =  80; // 使用中アイテムリスト取得通信
	public const int GPH_CON_GETCOIN     = 220; // メダル追加通信
	public const int GPH_CON_ITEMLIST    = 170; // アイテムリスト取得通信
	public const int GPH_CON_ITEMLIST2   = 171; // アイテムリスト取得通信
	public const int GPH_CON_SLEEP_SUB   = 260; // 03 休憩通信準備(先)
	public const int GPH_CON_SLEEP       = 250; // 休憩通信
	public const int GPH_CON_END_SUB     = 280; // 03 精算通信準備(先)
	public const int GPH_CON_END         = 270; // 精算通信
	public const int GPH_CON_2003        = 161; // BB/JACIN/BBEND/通信
	public const int GPH_CON_RESUME_SUB  = 163; // レジューム通信（の依頼）
	public const int GPH_CON_RESUME      = 162; // レジューム通信
	public const int GPH_CON_ITEMSELECT  = 181; // アイテム使用通信
	public const int GPH_CON_SYNC        = 151; // 定期通信
	public const int GPH_CON_DIRECTION   = 152; // レア演出通信

	public const int GPH_VIEW_AUTH          = 310; // 認証画面の描画
	public const int GPH_VIEW_ADINFO        = 330; // AD画面の描画
	public const int GPH_VIEW_RESLOAD       = 331; // リソースロードの描画
	public const int GPH_VIEW_CONNECT       = 332; // 通信中アイコンの描画
	public const int GPH_VIEW_WINDOW        = 333; // ウインドウの描画
	public const int GPH_VIEW_INFOWINDOW    = 334; // お知らせウインドウの描画
	public const int GPH_VIEW_SYNCCON       = 337; // 通信中アイコンの描画
	public const int GPH_VIEW_SYNCCON_AFTER = 338; // 通信中アイコンの描画（後処理）
	public const int GPH_VIEW_DATAWINDOW    = 390; // ゲーム中データの描画
	public const int GPH_VIEW_CREDITWINDOW  = 391; // ゲーム中データの描画
	public const int GPH_VIEW_MARQUEE       = 158; // マーキーの描画
	public const int GPH_VIEW_MESSAGE       = 159; // メッセージの描画
	public const int GPH_VIEW_COINWINDOW    = 360; // コイン補充の描画
	public const int GPH_VIEW_CONNECTWINDOW = 370; // 通信中画面
	public const int GPH_VIEW_ITEMWINDOW    = 180; // アイテムメニュー表示
	public const int GPH_VIEW_MENUWINDOW    = 200; // メニュー表示

	public const int GPH_CTRL_INFOWINDOW    = 335; // お知らせウインドウの入力
	public const int GPH_CTRL_MAIN          = 336; // ゲームメイン
	public const int GPH_CTRL_COINGET       = 210; // ゲームコイン取得
	public const int GPH_CTRL_GAMEWINDOW    = 380; // ゲーム中ウィンドウ
	public const int GPH_CTRL_MENUINIT      = 140; // メニュー初期化
	public const int GPH_CTRL_MENU          = 190; // メニュー制御
	public const int GPH_CTRL_POUNDMENU     = 303; // POUNDメニュー
	public const int GPH_CTRL_POUNDMENU_SUB = 156; // POUNDメニュー（先）
	public const int GPH_CTRL_ITEMLIST      = 160; // アイテムメニュー制御
	public const int GPH_CTRL_ITEM          = 231; // アイテムメニュー制御
	public const int GPH_CTRL_ERR_INIT      = 221; // ゲーム時のエラーを制御
	public const int GPH_CTRL_USEITEM_PRC   = 183; // 05 アイテム使用確認制御

	public const int GPH_END_BEF        =  302; // パチドル不足時の終了確認
	public const int GPH_SLEEP_BEF      =  301; // パチドル不足時の休憩確認

	public const int GPH_JUMP_END       = 9999; // アプリ終了
	public const int GPH_JUMP_VERSIONUP =  230; // バージョンアップ
	public const int GPH_JUMP_ERR       =  292; // サイトへジャンプ(err時)
	public const int GPH_JUMP_SITE      =  240; // サイトへジャンプ
	public const int GPH_JUMP_GACHA     =  290; // サイトへジャンプ
	public const int GPH_JUMP_SHOP      =  291; // サイトへジャンプ(err時)
	public const int GPH_JUMP_RESULT    =  293; // サイトへジャンプ(err時)


	public const int OPT_SOUND_VOL  =  0;
	public const int OPT_AUTO_MEDAL =  1;
	public const int OPT_AUTO_PLAY  =  2;
	public const int OPT_BONUS_CUT  =  3;
	public const int OPT_MEOSHI     =  4;
	public const int OPT_OSHIJUN    =  5;
	public const int OPT_OPT_NUM    =  6;
	public const int OPT_INIT       =  7;
	public const int OPT_SETTING_NOW =  8;
	public const int OPT_MAX        =  9;	// メニューの最大数


	public const int OPT_VOLUME_MAX		= 6;	// 音量ＭＡＸ
	public const int OPT_BONUS_CUT_MAX	= 3;	// ボーナスカット数(BONUS, OFF, JAC)	//_add_mst バグ修正【 No.26 】
	public const int OPT_OSHIJUN_NUM		= 6;	// 押し順数
	public const bool CUR_SELECT_LIGHT =  false;		// カーソルが選択されているところを光らせるか？

	public const int BB = 1;
	public const int RB = 3;
	public const int DRAW_ITEM_MAX = 3;	// 表示する最大アイテム数
	public const int PAGE_MAX      = 20;	// 1ページに表示する最大アイテム数

	public const int GPH_ERR         =  50; // 01 起動エラー
	public const int GPH_AUTH_ERR    =  55; // 01 認証コマンドERR
	public const int GPH_ERR2        =  57; // 01 起動エラー
	public const int GPH_ERR3        =  58; // 01 起動エラー
	public const int GPH_ERR4        =  61; // 01 起動エラー
	public const int GPH_RES_RETRY   =  62; // 01 リソースリトライ処理
	// ↓
	// ↓
	public const int GPH_INFOLAOD    =  90; // 02 広告取得(画像リソース)
	public const int GPH_RES1        = 100; // 02 リソース通信(1)
	public const int GPH_RES2        = 110; // 02 リソース通信(2)
	// ↓
	// ゲーム部分（分岐点）
	public const int GPH_GAMEMODE      = 120; // 03 ゲームモード設定
	public const int GPH_MAIN          = 130; // 03 ゲームメイン
	public const int GPH_RESET         = 150; // 03 ゲーム画面リセット(メニューから戻るなど)
	public const int GPH_SYNCCON_PRC   = 154; // 03 定期通信main
	public const int GPH_SYNCCON_RETRY = 159; // 05 定期通信リトライ部
	public const int GPH_FLASH         = 153; // 03 画面フラッシュ⇒NO PROCESS
	public const int GPH_SYNCCON_DRW   = 155; // 03 定期通信描画
	public const int GPH_SYNCCON_DRW2  = 157; // 03 定期通信描画2(描画だけ)


	// 分岐↓
	public const int GPH_DEBUG_PRC   = 99999; // 05 debug制御
	// 分岐↓
	public const int GPH_SHOPTO_TOMENU  = 201; // 04 ショップへジャンプ
	public const int GPH_GACHATO_TOMENU = 202; // 04 ガチャへジャンプ

	// 分岐↓
	public const int GPH_ERR2_RESUME = 222; // 03 ゲーム時の復帰制御

	public const int GPH_RARE_CON    = 225; // 01 レア演出

	// 分岐↓


	// 描画
	public const int GPH_WINDOW        = 340; // メインウィンドウ
	public const int GPH_INFOWINDOW    = 350; // お知らせ画面
	public const int GPH_DATAWINDOW    = 390; // ゲーム中データ表示

}

public partial class Defines {
    /*
 * 作成日: 2003/09/03
 */


/**
 * Z 用 Df 定義
 *
 * @author Kumita Atsushi
 */


	/** JADファイル内に記載されたプロパティー */
	public const string DEF_Z_JAR_URL = "MIDlet-Jar-URL" ; // midp

	/** 通信用サーバURL */
	public const string DEF_Z_SERVER_URL = "Server-URL" ; // midp

	/** ロード中表示文字列 */
	public const string DEF_Z_NOWLOADING = "Now Loading..."; 

	/** コインアップPHPパス */
	public const string DEF_Z_PHP_COINUP = "coin_up.php?" ; // vodafone doja
	/** ユーザー認証PHPパス */
	public const string DEF_Z_PHP_AUTH = "auth.php?" ; // vodafone doja
	/** ホール通信 本番*/
	public const string DEF_Z_PHP_HALL = "hall.php?" ; // vodafone doja
	
	/** ホールテストディレクトリ sbのみ*/
	public const string DEF_Z_PHP_HALL_DIR = "hall/"; 
//	public const string DEF_Z_PHP_HALL_DIR = "s/hall/"; 

	/** ユーザー認証PHPパス引数：DoCoMo */
	public const string DEF_Z_PHP_AUTH_TAIL_I = "uid=NULLGWDOCOMO"; //仮..
	/** ユーザー認証PHPパス引数：vodafone */
	public const string DEF_Z_PHP_AUTH_TAIL_V = "uid=1&sid=AGT1"; 
	
	/** GETパラメータUID */
	public const string DEF_Z_PARAM_UID = "uid="; 

	/** GETパラメータSID */
	public const string DEF_Z_PARAM_SID = "sid="; 

	/** GETパラメータCOIN imode以外 */
	public const string DEF_Z_PARAM_COIN = "coin="; 

	/** GETパラメータCOIN SoftBank追記 */
	public const string DEF_Z_PARAM_VODAFONE_COIN = "a="; 

	/** GETパラメータCOIN imodeのみ */
	public const string DEF_Z_PARAM_IMODE_COIN = "a="; 

	/** GETパラメータTIME */
	public const string DEF_Z_PARAM_TIME = "time="; 

	/** GETパラメータSLOT */
	public const string DEF_Z_PARAM_SLOT = "slot="; 

	/** GETパラメータAPPLI */
	public const string DEF_Z_PARAM_APPLI = "appli="; 

	/** GETパラメータVER */
	public const string DEF_Z_PARAM_VER = "ver=" ; // TODO アプリバージョンを送信

	/** GETパラメータb */
	public const string DEF_Z_PARAM_KISYU = "b="; 
	
	/* MIDP */
	/** JAD に追い出したメインループ時間 (ms) */
	public const string DEF_Z_JAD_PARAM_LOOP = "Z-Loop-Time"; 

	/** JAD に追い出したリソース名 */
	public const string DEF_Z_JAD_PARAM_RESOURCE = "Z-Resource-Name"; 

	/** JAD に追い出したサーバ名 */
	public const string DEF_Z_JAD_PARAM_SERVER = "Z-Server-Name"; 

	/** JAD に追い出したブラウザ連携フラグ */
	public const string DEF_Z_JAD_PARAM_WEBTO = "Z-CONNECT-WEB"; 

	/***/
	public const string DEF_Z_XSTORAGE_SIZE = "MIDlet-X-Storage-Size" ; // TODO NF

	/***/
	public const string DEF_Z_STORAGE_SIZE = "MIDlet-Data-Size" ; // TODO NF

	/** MIDP: RecordStore名 */
	 public const string DEF_RECORD_STORE_NAME = "D" ; // TODO NF

	/** MIDP: RecordStore ID */
	 public const int DEF_RECORD_ID = 1 ; // TODO NF
	/** MIDP: RecordStore ID ユーザー認証 */
	 public const int DEF_RECORD_ID_AUTH = 0 ; // TODO NF

	/** DoJa: JAM 引数の順序 0:LOOPスピード */
	public const int DEF_JAM_PARAM_LOOP = 0; 

	/** DoJa: JAM 引数の順序 1:ファイル名 */
	public const int DEF_JAM_PARAM_DATA = 1; 

	/** DoJa: JAM 引数の順序 2:launch */
	public const int DEF_JAM_PARAM_SERVER = 2; 








	/** 0-押されたキー */
	public const int DEF_Z_INT_KEYPRESS = 0; 

	/** 1-押されているキー */
	public const int DEF_Z_INT_KEYPRESSING = 1; 

	/** 2-キャンバス幅 */
	public const int DEF_Z_GET_WIDTH = 2; 

	/** 3-キャンバス高さ */
	public const int DEF_Z_GET_HEIGHT = 3; 

	/** DoJa: 4-ゲージの論理幅（正整数） */
	public const int DEF_Z_GAUGE_LENGTH = 4; 

	/** DoJa: 5-ゲージの論理位置（0又は正整数） */
	public const int DEF_Z_GAUGE_POSITION = 5; 

	/** 6-放されたキー */
	public const int DEF_Z_INT_KEYRELEASE = 6; 

	/** 7-変数の数 */
	public const int DEF_Z_INT_MAX = 7; 









	/** コマンドナンバー : UNDEF=-1 */
	public const int DEF_Z_INT_COM_UNDEF = -1; 

	/** コマンドナンバー : MENE=0 */
	public const int DEF_Z_INT_COM_MENU = 0; 

	/** コマンドナンバー : 戻る=1 */
	public const int DEF_Z_INT_COM_BACK = 1; 

	/** コマンドナンバー : 終了=2 */
	public const int DEF_Z_INT_COM_EXIT = 2; 

	/** コマンド表記 : "MENU" */
	public const string DEF_Z_STR_COM_MENU = "MENU"; 

	/** コマンド表記 : "戻る" */
	public const string DEF_Z_STR_COM_BACK = "戻る"; 

	/** コマンド表記 ： "終了" */
	public const string DEF_Z_STR_COM_EXIT = ""; 

	/**
	 * since mobuilder2009
	 * 
	 * to DfRes.java
	 * 
	 * 通信時のサーバーエラーステータスの定義をこちらにしておく
	 */			
//	/** サーバー通信ステータス:初期値 */
//	public const int DEF_Z_SERVER_INI = -2; 
//
//	/** サーバー通信ステータス:アプリ古いバージョン */
//	public const int DEF_Z_SERVER_APP_OLD_VERSION = -3; 
//
//	/** サーバー通信ステータス:エラー */
//	public const int DEF_Z_SERVER_ERR = -4; //-3

	/** サウンドのキューの数 */
	public const int DEF_Z_SOUND_QUE_MAX = 6; 

	/** ダウンロード中文字列 */
	public const string DEF_NOW_DOWNLOAD = "NOW DOWNLOAD..."; 

	/** ロード中文字列 */
	public const string DEF_NOW_LOADING = "NOW LOADING..."; 

	/** 外部化不可文字列リソース */
	public const string DEF_UID_NULLGWDOCOMO = "uid=NULLGWDOCOMO"; //$NON-NLS-1$

	/** 外部化不可文字列リソース */
	public const string DEF_PROTCOL_RESOURCE = "resource: ///";//$NON-NLS-1$

	/** 外部化不可文字列リソース */
	public const string DEF_PROTCOL_SCRATCHPAD = "scratchpad: ///0;pos=";//$NON-NLS-1$

	/** TODO
	 * 認証処理 外部化不可文字列リソース
	 */
	public const string DEF_PROTCOL_SCRATCHPAD1 = "scratchpad: ///1;pos=";//$NON-NLS-1$


	/** 外部化不可文字列リソース */
	public const string DEF_PROTCOL_STORAGE = "storage:"; //$NON-NLS-1$ ez TODO NF

	/** サウンドキュー（再開） */
	public const int DEF_SQ_RESTART = -1; 

	/** サウンドキュー（なにもしない） */
	public const int DEF_SQ_NOP = -2; 

	/** サウンドキュー（リピート） */
	public const int DEF_SQ_REPEAT = -3; 

	/** サウンドキュー（停止） */
	public const int DEF_SQ_STOP = -4; 

	/** サウンドキュー（ポーズ） */
	public const int DEF_SQ_PAUSE = -5; 

	/** サウンドキュー（内部使用：区切り） */
    // TODO C#移植 不明な値 SQ_NOP
	//public const int DEF_SQ_SEPARATSQ_NOPE = SQ_NOP; 

	/** bools ID (リペイント) */
	public const int DEF_IS_REPAINT = 0; 

	/** bools ID (コインアップ) */
	public const int DEF_IS_COIN_UP = 1; 

	/** bools ID (サウンドあり) */
	public const int DEF_IS_SQ_ON = 2; 

	/** bools ID (SE サウンド再生中でない) */
	public const int DEF_IS_SQ_FREE_SE = 3; 

	/** bools ID (メニューモード) */
	public const int DEF_IS_MENU = 4; 

	/** bools ID (ヘルプ) */
	public const int DEF_IS_HELP = 5; 

	/** bools ID (ブラウザ連携使用) */
	public const int DEF_USE_WEBTO = 6; 

	/** bools ID (ブラウザ連携中) */
	public const int DEF_IS_WEBTO = 7; 

	/** bools ID (BGM サウンド再生中でない) */
	public const int DEF_IS_SQ_FREE_BGM = 8; 

	/** bools ID (SE サウンド再生中でない) */
	public const int DEF_IS_SQ_FREE_SE2 = 9 ; // TODO 同時再生音を増やす

	/** bools ID (最大値) */
	public const int DEF_IS_MAX = 10; 

	/** BUFFER SIZE */
	public const int DEF_BUFFER_SIZE = 128; 
	/** BUFFER SIZE */
	public const int DEF_HTTP_BUFFER_SIZE = (12*2 * 3)+(2 * 3) + 1; 

	/** リソースダウンロード済みID */
	public const int DEF_RESOURCE_ID = 0x1D; 
	
	/** リソースダウンロード済みIDをクリア */
	public const int DEF_RESOURCE_NULL = 0x00; 

	/** リトライ回数 */
	public const int DEF_Z_RETRY_TIMES = 3; 

	/** デバッグ用 */
	public const bool DEF_IS_RESOURCE = false; 



	/** 認証用メッセージ */
	public const string DEF_AUTHSTR_YES = "はい"; 
	/** 認証用メッセージ */
	public const string DEF_AUTHSTR_NO = "いいえ"; 
	/** 認証用メッセージ */
	public const string DEF_AUTHSTR_EXIT = "終了"; 
	

	/** 認証用メッセージ */
	public const string DEF_AUTHSTR_INIT_TITLE = "認証確認"; 
	/** 認証用メッセージ */
	public const string DEF_AUTHSTR_INIT_TEXT = "このアプリは\n" + "会員認証を行います。\n"+ "よろしいですか？\n"+ "\n" + DEF_AUTHSTR_YES + ":ＯＫ！\n"+ DEF_AUTHSTR_NO + ":終了"; 
	
	
	/** 認証用メッセージ */
	public const string DEF_AUTHSTR_ERROR_TITLE = "通信エラー"; 
	/** 認証用メッセージ */
	public const string DEF_AUTHSTR_ERROR_TEXT = "電波状態のよいところで\n" + "再接続してください\n"+ "\n" + DEF_AUTHSTR_YES + ":再接続\n"+ DEF_AUTHSTR_NO + ":終了 "; 
	
	/** 認証用メッセージ */
	public const string DEF_AUTHSTR_NOTUSER_TITLE = "有料会員のみプレイ可"; 
	/** 認証用メッセージ */
	public const string DEF_AUTHSTR_NOTUSER_TEXT = "非会員、又はUID通知設定\n"+ "がなされていません。\n"+ "UID通知を確認し、\n"+ "有料会員登録してください\n"+ "\n" + DEF_AUTHSTR_YES + ":公式サイトへ\n" + DEF_AUTHSTR_NO + ":終了 "; 
	
	/** 認証用メッセージ */
	public const string DEF_AUTHSTR_NOTUSER_TEXT_NON_WEBTO = "非会員、又はUID通知設定\n"+ "がなされていません。\n"+ "UID通知を確認し、\n"+ "有料会員登録してください\n"+ "\n"; 
// +
//		AUTHSTR_YES + ":公式サイトへ\n" +
//		AUTHSTR_NO +		":終了 "
		

	// TODO 旧バージョンアプリ
	/** 認証用メッセージ */
	public const string DEF_AUTHSTR_OLDAPPLI_TITLE = "バージョンアップ"; 
	/** 認証用メッセージ */
	public const string DEF_AUTHSTR_OLDAPPLI_TEXT = "このｱﾌﾟﾘは最新版のｱﾌﾟﾘ\n"+ "ﾊﾞｰｼﾞｮﾝではございませ\n"+ "ん。　　　　　　　\n"+ "ｻｲﾄから最新ﾊﾞｰｼﾞｮﾝのｱ\n"+ "ﾌﾟﾘをﾀﾞｳﾝﾛｰﾄﾞしてくださ\n"+ "い\n" + DEF_AUTHSTR_YES + ":ﾀﾞｳﾝﾛｰﾄﾞｻｲﾄへ\n" + DEF_AUTHSTR_NO + ":終了 "; 

	// TODO 旧バージョンアプリ
	/** 認証用メッセージ */
	public const string DEF_AUTHSTR_OLDAPPLI_TEXT_NON_WEBTO = "このｱﾌﾟﾘは最新版のｱﾌﾟﾘ\n"+ "ﾊﾞｰｼﾞｮﾝではございませ\n"+ "ん。　　　　　　　\n"+ "ｻｲﾄから最新ﾊﾞｰｼﾞｮﾝのｱ\n"+ "ﾌﾟﾘをﾀﾞｳﾝﾛｰﾄﾞしてくださ\n"+ "い\n"; 
// +
//		AUTHSTR_YES + ":ﾀﾞｳﾝﾛｰﾄﾞｻｲﾄへ\n" +
//		AUTHSTR_NO + ":終了 "
		

	/** 認証用メッセージ */
	public const string DEF_AUTHSTR_TICKER = "　　 ☆ 認証中 ☆　　 "; 

	/** 認証用通信試行回数 */
	public const int DEF_AUTH_RETRY_COUNT = 5; 

	/** 認証結果：有料会員 */
	public const int DEF_AUTHMEMBER_PAYING = 0; //dues-paying member
	/** 認証結果：無料会員 */
	public const int DEF_AUTHMEMBER_FREE = 1; //free member(←若干疑わしい?)
	/** 認証結果：非会員 */
	public const int DEF_AUTHMEMBER_NON = 2; //nonmember
	/** 認証結果：旧バージョンアプリ*/
	public const int DEF_AUTHMEMBER_OLD_VERSION = 3; //old version // TODO 旧バージョンアプリ
	/** 認証結果：個別課金アプリ*/
	public const int DEF_AUTHMEMBER_KOBETSU = 5; //old version // TODO 個別課金情報

	/** 認証結果：有料会員 */
	public const char DEF_AUTHMEMBERCHAR_PAYING = '0'; //dues-paying member
	/** 認証結果：無料会員 */
    public const char DEF_AUTHMEMBERCHAR_FREE = '1'; //free member(←若干疑わしい?)
	/** 認証結果：非会員 */
    public const char DEF_AUTHMEMBERCHAR_NON = '2'; //nonmember
	/** 認証結果：旧バージョンアプリ*/
    public const char DEF_AUTHMEMBERCHAR_OLD_VERSION = '3'; //old version // TODO 旧バージョンアプリ

	/** 認証結果：個別課金アプリ*/
    public const char DEF_AUTHMEMBERCHAR_KOBETSU = '5'; //old version // TODO 個別課金情報
	
	
	/** 認証用スクラッチパッドorレコードストア配列インデックス */
	public const int DEF_AUTHDAT_MONTH = 0; 
	/** 認証用スクラッチパッドorレコードストア配列インデックス */
	public const int DEF_AUTHDAT_YEAR = 1; 

	/** 非会員用URL */
	public const string DEF_AUTHURL_DOCOMO = "http: //pr.arukin.net/?_fr=ak-mob-ap";
	/** 非会員用URL */
	public const string DEF_AUTHURL_SOFTBANK = "pr.arukin.net/?_fr=ak-mob-ap"; 
//	public const string DEF_AUTHURL_SOFTBANK = "url: //pr.arukin.net/?_fr=ak-mob-ap";

//	/** 旧バージョンアプリ用URL */
//	public const string DEF_AUTHURL_SOFTBANK_OLD_VER = "url:; //js.arukin.net" // TODO 旧バージョンアプリ 機種ＴＯＰへ飛ばすつもり

	/** 
	 * 認証の返り値.
	 * 
	 * 有料会員："0message"
	 * 無料会員："1message\r\nmessage\r\nmessage"
	 * 　非会員："2message\r\nmessage\r\nmessage"
	 * 　VerUP ："3message\r\nmessage\r\nmessage"
	 * 0, 1, 2の文字列長.
	 */
	public const int DEF_AUTH_HEAD_LENGTH = 1; 

	/** @see #AUTH_USER */
	public const int DEF_COINUP_USER = 0; 
	/** @see #AUTH_NOTUSER */
	public const int DEF_COINUP_NOTUSER = -1; 
	/** @see #AUTH_ERROR */
	public const int DEF_COINUP_ERROR = -2; 
	/** @see #AUTH_OLD_Version */
	public const int DEF_COINUP_OLD_VERSION = -3; 

	/** @see #COINUP_USER */
	public const int DEF_AUTH_USER = DEF_COINUP_USER; 
	/** @see #COINUP_NOTUSER */
	public const int DEF_AUTH_NOTUSER = DEF_COINUP_NOTUSER; 
	/** @see #COINUP_ERROR */
	public const int DEF_AUTH_ERROR = DEF_COINUP_ERROR; 
	/** @see #COINUP_OLD_Version */
	public const int DEF_AUTH_OLD_VERSION = DEF_COINUP_OLD_VERSION; 

	/**
	 * 3D:モデル用テクスチャ for Vodafone
	 *
	 * VodafoneとDoCoMoでは逆!!
	 */
    public const bool DEF_TEXTURE_FOR_MODEL_VODAFONE = true; 
    /**
     * 3D:環境用テクスチャ
     *
     * VodafoneとDoCoMoでは逆!!
     */
    public const bool DEF_TEXTURE_FOR_ENV_VODAFONE = !DEF_TEXTURE_FOR_MODEL_VODAFONE; 
    /**
     * 3D:モデル用テクスチャ
     *
     * DoCoMoとVodafoneでは逆!!
     */
    public const bool DEF_TEXTURE_FOR_MODEL_DOCOMO = false; 
    /**
     * 3D:環境用テクスチャ
     *
     * DoCoMoとVodafoneでは逆!!
     */
    public const bool DEF_TEXTURE_FOR_ENV_DOCOMO = !DEF_TEXTURE_FOR_MODEL_DOCOMO; 



    /////////////////////////////////////////////////////

    /** 3D default value **/

    public const int DEF_CONST3D_CENTER_X = 120; 
    /** 3D default value **/
    public const int DEF_CONST3D_CENTER_Y = (DEF_CONST3D_CENTER_X + Defines.GP_DRAW_OFFSET_Y); 
    
//#ifdef	_DOCOMO	// {
//// satoh#位置ずれ問題
//    public const int DEF_CONST3D_CENTER_X = 120; 
//    /** 3D default value **/
//    public const int DEF_CONST3D_CENTER_Y = (DEF_CONST3D_CENTER_X + GP_DRAW_OFFSET_Y); 
//#else			// } {
//	public const int DEF_CONST3D_CENTER_X = 120; 
//	public const int DEF_CONST3D_CENTER_Y = DEF_CONST3D_CENTER_X + 10 + GP_DRAW_OFFSET_Y; 
//#endif			// }

    /** 3D default value **/
    public const int DEF_CONST3D_SCALE_X = 4096; 
    /** 3D default value **/
    public const int DEF_CONST3D_SCALE_Y = DEF_CONST3D_SCALE_X; 
}
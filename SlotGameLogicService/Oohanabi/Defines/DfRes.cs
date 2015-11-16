public partial class Defines {

/** AUTO GENERATED RESOURCE HIERARCHY BY ANT */

	/** 3Dサウンド使うかどうか */
	public const bool DEF_USE_3D_SOUND = false;
	/** 重畳再生するかどうか */
	public const bool DEF_USE_MULTI_SOUND = true;
	/** 重畳再生用 */
	public const int DEF_SOUND_MULTI_SE = 1;
	/** 重畳再生用 */
	public const int DEF_SOUND_MULTI_BGM = 0;

	/** セーブサイズ */
	public const int DEF_SAVE_SIZE = 256;
	/** リソース開始位置 */
	public const int DEF_RESOURCE_START = 2 + DEF_SAVE_SIZE;
	/** リソースを Zip 圧縮するか */
	public const bool DEF_IS_RESOURCE_ZIPPED = false;
	/** リソースを Jar にするか */
	public const bool DEF_IS_RESOURCE_IN_JAR = false;
	/** 動的ロードするかどうか */
	public const bool DEF_IS_DYNAMIC_LOAD = false;

	/** リソースファイル名 */
	public const string DEF_RESOURCE_FILE = "mOmatsuri";
	/** リソースバッファサイズ */
	public const int DEF_RESOURCE_BUFFER_SIZE = 102400;
	/** サブディスプレイを使うかどうか */
	public const bool DEF__DF_SUB_DISPLAY_ = false;
	/** ブラウザ連携するかどうか */
	public const bool DEF__DF_AUTO_CAN_WEBTO_ = true;
	/** ブラウザ連携は機種情報か？ */
	public const bool DEF__DF_AUTO_CAN_WEBTO_KISYU_ = true;
	/** 筐体あり版かどうか */
	public const bool DEF__DF_DRAW_KYOTAI_ = false;
	/** ＡｕｄｉｏＰｒｅｓｅｎｔｅｒ２かどうか
	 * public const bool DEF__DF_USE_AUDIO_PRESENTER_2_ = true;
	 */

	/** アプリシリアル番号 */
//	public const int DEF_APPLI_SERIAL = "1";
			
	/** 機種ＩＤ */			
	public const int DEF_KISYU_ID = 1;
	/** アプリ分類ＩＤ */
	public const int DEF_APPLI_TYPE_ID = 0;
			
	/** アプリバージョン番号 */
    public const string DEF_APPLI_VER = "6.0.2";
			
//	public const string DEF_APPLI_VER = ("im".equals("vo") ) ? "6.0.2" : "6.0.2";
//	public const int DEF_APPLI_VER_docomo = "6.0.2";
//	public const int DEF_APPLI_VER_vodafone = "6.0.2";


	/**
	 * since mobuilder2009
	 *			
	 * from mobuilder2007#DfZ.java			
	 * 通信時のサーバーエラーステータスの定義をこちらにしておく
	 */			
			
	/** サーバー通信ステータス:初期値 */
	public const int DEF_SERVER_INI = -2;

	/** サーバー通信ステータス:アプリ古いバージョン */
	public const int DEF_SERVER_APP_OLD_VERSION = -3;

	/** サーバー通信ステータス:エラー */
	public const int DEF_SERVER_ERR = -4;
			
}

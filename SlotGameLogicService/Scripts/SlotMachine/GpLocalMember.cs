
/// <summary>
/// ほぼスタブ
/// </summary>
using System;
public partial class SlotInterface
{    
	bool end_f = false;

	String[] pngResource; // イメージファイル名のバッファ

    
    // TODO C#移植 GP処理コメントアウト
    //Image[] png01; // アプリ起動画面リソース
    //Image[] png02; // アプリG7画面リソース
    //Image   pnginfo; // 広告画像

	// HOST情報
	public static String gpID;
	public static String gpHOST;
	public static String gpIF_VER;
	public static String gpVER;
	public static String gpRES;
    
	// レスポンスデータ
    public G7NetworkParam param = new G7NetworkParam(); // レスポンスデータ
    public G7NetworkParam param_auth = new G7NetworkParam(); // レスポンスデータ(認証)
    public G7NetworkParam param_hall = new G7NetworkParam(); // レスポンスデータ(台)
    public G7NetworkParam param_info = new G7NetworkParam(); // レスポンスデータ(広告)
    public G7NetworkParam param_itemu = new G7NetworkParam(); // レスポンスデータ(使用中アイテムリスト)
    public G7NetworkParam param_coin = new G7NetworkParam(); // レスポンスデータ(購入)
    public G7NetworkParam param_sleep = new G7NetworkParam(); // レスポンスデータ(休憩)
    public G7NetworkParam param_end = new G7NetworkParam(); // レスポンスデータ(精算)
    public G7NetworkParam param_sync = new G7NetworkParam(); // レスポンスデータ(定期)
    public G7NetworkParam param_iteml = new G7NetworkParam(); // レスポンスデータ(アイテムリスト)
    public G7NetworkParam param_itemg = new G7NetworkParam(); // レスポンスデータ(アイテム使用)
    public G7NetworkParam param_resum = new G7NetworkParam(); // レスポンスデータ(レジューム)

    // TODO C#移植 スタブ
    public class G7NetworkParam { 
    }

    // TODO C#移植 GP処理コメントアウト
    //public G7Network sync_con = null; // 定期通信用スレッド

    //	public bool sync_retry   = false; // 
	public bool sync_reswait = false; // レスポンス待ちの場合true
	public long    sync_next  = 0L;

	public bool seisan = false; // 精算完了後場合true

	// 著作権
	public const String copyright = PublicDefine.COPYRIGHT;

	public long ad_time;
	public int last_sync;

	// 環境メンバ
	public bool auto_coin = false; // 自動でcoinを購入
	public bool data_flag = true; // データパネル表示フラグ

    // TODO C#移植 GP処理コメントアウト
    //public static Graphics g;
    //public static Font     font;
    //public static Canvas   cv;

	public int fontHeight;

	public bool reset = false;
	public bool auto_off_popup = false;
	public bool auto_off = false;
	public bool auto_on = false;
	public int     auto_off_bonus;

	public bool marqueeflg;
	public int     marqueecounted;
	public int     marqueenum;
	public int     marqueecount;
	public String  marqueestr;
	public long    marqueetime;
    // TODO C#移植 GP処理コメントアウト
    //public Image   marqueeimg;

	public bool msgflg;
	public String  msgstr;
	public int     msgsize;
	public long    msgtime;
	public int     msgr;
	public int     msgg;
	public int     msgb;
	public int     msgy;
    // TODO C#移植 GP処理コメントアウト
    //public Image   msgimg;
	
#if	_DOCOMO		//{
	
#else				//}{
		public Font    msgfont = Font.getFont( Font.FACE_SYSTEM, Font.STYLE_PLAIN, Font.SIZE_SMALL );
#endif				// }
	// セーブデータの情報
	enum SAVE {
		SAVE_SOUND_VOL,
		SAVE_AUTO_MEDAL,
		SAVE_AUTO_PLAY,
		SAVE_BONUS_CUT,
		SAVE_MEOSHI,
		SAVE_OSHIJUN,
		SAVE_OPT_NUM,
		SAVE_INIT,
		SAVE_SETTING_NOW,
		SAVE_MAX,
	};
	// セーブデータの情報
    public int[] saveData = new int[(int)SAVE.SAVE_MAX];
	public bool plyer_change_f; // プレイヤーが変わったらtrue
	
	int[] opt_value = new int[PublicDefine.OPT_MAX];				// 設定値
#if __COM_TYPE2__
	// #183対策
	//int opt_value_bak[] = new int[OPT_MAX];		// メニューに入った時の設定値
	bool[] opt_value_up = new bool[PublicDefine.OPT_MAX];	// メニュー内で変更されたかのフラグ,INとOUTが同じ値でも変更すればtrue(true=変更あり)
#endif
	public int     doll_rate = 1000;
	public int     doll_rate_index;
//	public int     dai_bns_rot;
	public bool resumeflg;
	public int[]   locktogame= new int[4];
	public int     lockresult = 0;
	public int     lockresult_next = 0;
	public bool keylock = false; // キー操作をgetKey2でしか受け取れなくなる
	String err_title;
	String err_msg;
	String err_url;
	bool termflg = false;

	bool isBusy = false; // 強制的に(筐体を)停止する

	String dammy_time_str     = "2012/01/23 18:55:32";
	String dammy_srv_time_str = "2012/01/23 18:00:00";

	bool dammy_time_f = true;
	bool sync_err_f = false;

	int[]     srv_time_array = null; // サーバータイム
	String    srv_time_str   = "";   // サーバータイム
	long      srv_time_long  = 0L;   // サーバータイム

//	bool srv_dammy_f = true; // ダミーサーバタイムを利用します。
	bool srv_dammy_f = false; // ダミーサーバタイムを利用します。

	bool   settei_flag = false; // アイテムで設定変更された場合
	int       settei_id   = 0; // 設定変更で使う画像
	long      settei_time; // 終了時間
	long      settei_time_base; // 残り時間ベース

    // TODO C#移植 GP処理コメントアウト
    // ItemParam settei;
	
    bool   bbcut_flag = false; // アイテムで設定変更された場合
	int       bbcut_id   = 0; // 設定変更で使う画像
	long      bbcut_time; // 終了時間
	long      bbcut_time_base; // 残り時間ベース

    // TODO C#移植 GP処理コメントアウト
    // ItemParam bbcut;

	bool   autot_flag = false; // アイテムで設定変更された場合
	int       autot_id   = 0; // 設定変更で使う画像
	long      autot_time; // 終了時間
	long      autot_time_base; // 残り時間ベース

    // TODO C#移植 GP処理コメントアウト
    // ItemParam autot;

	int bufid = 0;

	int retrycount = 0;
	int handle_buf = 0; // 前回していた処理
	int itemNum    = 0;
	public string userDirection = "";
	long auto_time;
	int menu2jump = 0;
	bool no_resume_f = false; // レジュームを受け付けない期間
	bool bonus_seiritsu_f = false; // ボーナス成立メッセージ表示フラグ

	bool menujumpf = false;
	bool menu2jumpf = false;
	public bool l_m_bEyeSupport;
	bool l_v_arBProcessLocalVar_4_;
	int opt_cursol     	= 0;		// オプションのカーソル
	int opt_page       	= 0;		// オプションのページ
	int opt_how_to_page	= 0;		// オプションの遊び方ページ
	int opt_tarm_page	= 0;		// 終了、休憩ページの切り替え
	int opt_tarm_pagebuf= 0;		// 終了、休憩ページの切り替えの戻り位置保持
	int opt_tarm_pagenum= 0;		// 終了、休憩ページの切り替えの戻り位置保持、カーソル位置
	int scratchPad_opt_init       = 0;	// スクラッチパッドでオプションは初期和美
	int drot;
	int dcoin;
	int[] slumpData2; // 通常描画用
	int[] slumpData3; // サーバ送信用
	int[] bonus_history = new int[ 10 * 3 ]; // { game_num, BB, get_coin } × 10
 	int[] historyData = new int[30];
	int[] m_iReelAngle = new int[3]; // リールの角度
	int setting_sub_num = -1;
	int tmp_setting_sub_num = -1;


	bool fItemState = false; //　アイテム状態

	bool menu_bonus_cut_f = false; // ボーナスカット時のメニュー(ボーナスカット)項目の変更用
	
	int itemIndex  = 0; // アイテムインデックス
	int itemCursol = 0; // アイテムカーソル
	int itemPage   = 0; // アイテムページ
	int req_item_page = 0; // アイテムページリクエスト
	int req_item_page_before = 0; // アイテムページリクエストする前のページ

	//ソフトキー関連
	bool softLabel_flag = true;		// ソフトキーフラグ（ON：有効 OFF：無効）

	bool saigoni_flag = false; // 最後に描画したのがON通信だったら
	long    next_time;
	int     use_item_id;
	int     use_item_type;
	int     index_point;
	int     l_m_iSlotFlg_16_;
	int     bonus_cut_num = 0; // JACカット、ボーナスカットの選択の時に使う

	public ZZ mainapp = null;

    // TODO C#移植 GP処理コメントアウト
    //public int before_case = GPH_NOPROCESS;

	bool sync_con_icon = false; // アイコン点灯中
	bool sync_con_flag = false; // 定期通信中
	bool con_flag      = false; // その他通信中

	int errRetry = 0;
	bool startflg = false; // 通常画面まで行ったらtrue

	public int[]       itemSlot = { -1, -1, -1, -1, -1, -1, -1 };
	public int[]       itemType = { -1, -1, -1, -1, -1, -1, -1 };
	//public ItemParam[] usedItem = new ItemParam[ 7 ]; // 設定系 / オート系 / ﾎﾞｰﾅｽｶｯﾄ系

	private int slumpDataStart = 0;
	private int slumpDataEnd   = 0;
	private int slumpDataView  = 0; // 表示開始位置

	// スランプデータ。
	private int[] slumpData = new int[600];

	private int ttl_roll = 10; // 総回転数
	private int init_d_coin = 0; // 台の初期コイン（ゲーム開始時のコイン）
	public int now_ball = 0; // 情報バー「所持玉数」
	private int init_u_coin = 0; // ユーザの初期コイン（ゲーム開始時のコイン）

	bool mim_test = true;

	/** 乱数発生用変数 */
	Random random = new Random(); // 乱数をオブジェクトを生成

}


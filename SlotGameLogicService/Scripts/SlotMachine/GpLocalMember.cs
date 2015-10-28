using System;

public partial class SlotInterface
{    
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

    public class G7NetworkParam
    { 
    }

	public bool sync_reswait = false; // レスポンス待ちの場合true
	public long    sync_next  = 0L;
	public bool seisan = false; // 精算完了後場合true
	public const String copyright = PublicDefine.COPYRIGHT;
	public long ad_time;
	public int last_sync;
	public bool auto_coin = false; // 自動でcoinを購入
	public bool data_flag = true; // データパネル表示フラグ
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
	public bool msgflg;
	public String  msgstr;
	public int     msgsize;
	public long    msgtime;
	public int     msgr;
	public int     msgg;
	public int     msgb;
	public int     msgy;

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
	bool[] opt_value_up = new bool[PublicDefine.OPT_MAX];	// メニュー内で変更されたかのフラグ,INとOUTが同じ値でも変更すればtrue(true=変更あり)
	public int     doll_rate = 1000;
	public int     doll_rate_index;
	public bool resumeflg;
	public int[]   locktogame= new int[4];
	public int     lockresult = 0;
	public int     lockresult_next = 0;
	public bool keylock = false; // キー操作をgetKey2でしか受け取れなくなる
	
	public string userDirection = "";

	public bool l_m_bEyeSupport;

    int[] bonus_history = new int[ 10 * 3 ]; // { game_num, BB, get_coin } × 10
 	int[] historyData = new int[30];
	int[] m_iReelAngle = new int[3]; // リールの角度

	public ZZ mainapp = null;

	public int[]       itemSlot = { -1, -1, -1, -1, -1, -1, -1 };
	public int[]       itemType = { -1, -1, -1, -1, -1, -1, -1 };

	private int[] slumpData = new int[600];
	public int now_ball = 0; // 情報バー「所持玉数」


	/** 乱数発生用変数 */
	Random random = new Random(); // 乱数をオブジェクトを生成

}


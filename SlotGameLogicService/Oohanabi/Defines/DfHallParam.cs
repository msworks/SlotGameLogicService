public partial class Defines {

	/**
	 * 通信情報
	 */
	public static readonly int DEF_H_APPLI_REQ = 0;	//アプリリクエスト
	public static readonly int DEF_H_SERVER_RES = 1;	//サーバーレスポンス
	
	/**
	 * 店及び台の情報
	 */
	public static readonly int DEF_H_HALL_ID = 2;	//ホールID
	public static readonly int DEF_H_LAND_ID = 3;	//島ID
	public static readonly int DEF_H_MACHINE_ID = 4;	//台ID
	public static readonly int DEF_H_LAUNCH_TIME = 5;	//閉店時間
	public static readonly int DEF_H_CLOSE_TIME = 6;	//閉店時間

	/**
	 * コインの情報
	 * オーナー←→プレーヤー間の
	 * コインの移動を行うために必要
	 */
	public static readonly int DEF_H_MEDAL_IN = 7;	//差分投入枚数
	public static readonly int DEF_H_MEDAL_OUT = 8;	//差分払出枚数
	public static readonly int DEF_H_PLAYER_COIN = 9;	//プレーヤーコイン

	/**
	 * 遊技情報
	 */
	public static readonly int DEF_H_GAME_COUNT = 10;	//回転数
	public static readonly int DEF_H_WAVENUM = 11;	//設定
    public static readonly int DEF_H_GAMEST = 12;	//遊戯状態
    public static readonly int DEF_H_BBGMCTR = 13;	//BB中一般遊技可能回数
    public static readonly int DEF_H_BIGBCTR = 14;	//RB作動可能回数
    public static readonly int DEF_H_JAC_CTR = 15;	//役物入賞可能回数
    public static readonly int DEF_H_JACGAME = 16;	//役物遊技可能回数
    public static readonly int DEF_H_PCC_CTR = 17;	//差枚数ｶｳﾝﾀｰ
    public static readonly int DEF_H_HITREQ = 18;	//内部成立
    public static readonly int DEF_H_BNSSYM = 19;	//入賞ボーナス絵柄
    public static readonly int DEF_H_BET = 20; //直前のベット枚数（ﾘﾌﾟﾚｲ時に仕様)
    public static readonly int DEF_H_REEL_L = 21; //リール左
    public static readonly int DEF_H_REEL_C = 22; //リール中
    public static readonly int DEF_H_REEL_R = 23; //リール右
   
    /**
	 * データパネル情報
	 */
	public static readonly int DEF_H_BNS_0 = 24;	//データパネル現在
	public static readonly int DEF_H_BNS_1 = 25;	//データパネル過去
	public static readonly int DEF_H_BNS_2 = 26;
	public static readonly int DEF_H_BNS_3 = 27;
	public static readonly int DEF_H_BNS_4 = 28;
	public static readonly int DEF_H_BNS_5 = 29;
	public static readonly int DEF_H_BNS_6 = 30;
	public static readonly int DEF_H_BNS_7 = 31;
	public static readonly int DEF_H_BNS_8 = 32;
	public static readonly int DEF_H_BNS_9 = 33;
	public static readonly int DEF_H_BB_COUNT = 34;	//BIG回数
	public static readonly int DEF_H_RB_COUNT = 35;	//REG回数

	public static readonly int DEF_H_PARAM_NUM = 36;	//配列最大値

	/**
	 * 文字サイズ
	 */
	public static readonly int DEF_HS_APPLI_REQ = 1;
	public static readonly int DEF_HS_SERVER_RES = 1;
	
	public static readonly int DEF_HS_HALL_ID = 4;
	public static readonly int DEF_HS_LAND_ID = 2;
	public static readonly int DEF_HS_MACHINE_ID = 2;
	public static readonly int DEF_HS_LAUNCH_TIME = 8;
	public static readonly int DEF_HS_CLOSE_TIME = 8;
	
	public static readonly int DEF_HS_MEDAL_IN = 3;
	public static readonly int DEF_HS_MEDAL_OUT = 3;
	public static readonly int DEF_HS_PLAYER_COIN = 5;

	public static readonly int DEF_HS_GAME_COUNT = 5;
	public static readonly int DEF_HS_WAVENUM = 1;
    public static readonly int DEF_HS_GAMEST = 2;
    public static readonly int DEF_HS_BBGMCTR = 2;
    public static readonly int DEF_HS_BIGBCTR = 1;
    public static readonly int DEF_HS_JAC_CTR = 1;
    public static readonly int DEF_HS_JACGAME = 1;
    public static readonly int DEF_HS_PCC_CTR = 4;
    public static readonly int DEF_HS_HITREQ = 2;
    public static readonly int DEF_HS_BNSSYM = 1;
    public static readonly int DEF_HS_BET = 1;
    public static readonly int DEF_HS_REEL_L = 2; //リール左
    public static readonly int DEF_HS_REEL_C = 2; //リール中
    public static readonly int DEF_HS_REEL_R = 2; //リール右
	
	public static readonly int DEF_HS_BNS_0 = 4;
	public static readonly int DEF_HS_BNS_1 = 2;
	public static readonly int DEF_HS_BNS_2 = 2;
	public static readonly int DEF_HS_BNS_3 = 2;
	public static readonly int DEF_HS_BNS_4 = 2;
	public static readonly int DEF_HS_BNS_5 = 2;
	public static readonly int DEF_HS_BNS_6 = 2;
	public static readonly int DEF_HS_BNS_7 = 2;
	public static readonly int DEF_HS_BNS_8 = 2;
	public static readonly int DEF_HS_BNS_9 = 2;
	public static readonly int DEF_HS_BB_COUNT = 2;
	public static readonly int DEF_HS_RB_COUNT = 2;

	public static readonly int DEF_HS_MAX = 90;
	
	public static readonly int DEF_HRQ_INIT = 0;		//起動時
	public static readonly int DEF_HRQ_COIN = 1;		//コイン貸出要求通信
	public static readonly int DEF_HRQ_NORMAL = 2;		//規定G数通信
	public static readonly int DEF_HRQ_BNSIN = 3;		//ボーナスIN通信
	public static readonly int DEF_HRQ_BNSEND = 4;		//ボーナスEND通信
	public static readonly int DEF_HRQ_EXIT = 5;		//終了時通信
	public static readonly int DEF_HRQ_SUCCESS = 6;	//コイン受取成功通信
	
	public static readonly int DEF_HRS_INIT = 0;		//起動時
	public static readonly int DEF_HRS_SUCCESS = 1;	//サーバ処理成功
	public static readonly int DEF_HRS_FAIL = 2;		//サーバ処理失敗
	public static readonly int DEF_HRS_NOCOIN = 3;		//コイン不足
	public static readonly int DEF_HRS_CLOSE = 4;		//閉店確認
	
	public static readonly int DEF_HALL_GAME_SPAN = 50;


}

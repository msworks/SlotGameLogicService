using UnityEngine;
using System;
using System.Collections;

public class SlotInterface
{
	public int bonus_type;    // 獲得種別(BB=1,RB=2)
	public int bonus_incount; // 回転数(ボーナス当選時のhall.dai_bns_rotを入れる）
	public int bonus_getcoin; // 獲得枚数(777時の15枚含む)
	public int gpif_setting; // 台設定
	public int gpif_coin;    // 台総コイン(ユーザー不可視※クレジット表示とは別です)
	public bool gpif_bonuscyu_f; // ボーナス中フラグ
	public bool gpif_naibucyu_f; // ボーナス内部中フラグ
	public bool gpif_flash_f; // 再描画
	public bool gpif_auto_f;        // オートフラグ
	public bool gpif_bonuscut_f;    // ボーナスカットフラグ
	public bool gpif_tatsujin_f;    // 達人オートフラグ
	public bool gpif_triple_f;      // トリプルスリーカード
	public bool gpif_lock_f; // 内部表示ロックフラグ(エラー時など描画だけおこない、内部ステートは変更しないロック状態)
	public bool gpif_nonstop_f;	// ノンストップオート
	public short gpif_bonus_n;		//通常0～設定時BB＝1、RB＝2
	public short gpif_kakuhen_n = 1; // 確率アップ(通常1～設定時33)
	public int auto_num = 0; // オートフラグの復帰に使う

	public short gpif_oshijun_n;	// メニューの押し順(0～5)
	public short[][] gpif_oshijun_list = // メニューの押し順リスト
	{ 
		new short[]{0,1,2},	//0:左、中、右（順押し）
		new short[]{0,2,1},	//1:左、右、中
		new short[]{1,0,2},	//2:中、左、右
		new short[]{1,2,0},	//3:中、右、左
		new short[]{2,0,1},	//4:右、左、中
		new short[]{2,1,0}		//5:右、中、左（逆押し）
	};
		
	// サウンドボリューム
	static int[] vollist = { 0,20,30,50,65,90,100,};
		
	// オートプレイの停止タイミング用
	public bool gpif_auto_stop_f = false;

	// 参照系
	public bool betFlag = false;
	public bool betNow()
    { 
		return betFlag;
	}
	
	public bool bonusNow() { return false; } // ボーナス状態

    Mobile mobile;
    Omatsuri omatsuri;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="mobile"></param>
    public SlotInterface( Mobile mobile, Omatsuri omatsuri )
    {
        this.mobile = mobile;
        this.omatsuri = omatsuri;
    }

	// キー press時にtrue。gpif_lock_f時は常にfalse
	bool getKey( int keycode )
	{
		// キーロック時はfalse
		if( gpif_lock_f ) {
			return false;
		}
        if ((mobile.keyTrigger & (1 << keycode)) != 0)
        {
			return true;
		}
		else {
			return false;
		}
	}

	// キー press時にtrue。gpif_lock_fの状態によらない
	bool getKey2( int keycode )
	{
        if ((mobile.keyTrigger & (1 << keycode)) != 0)
        {
			return true;
		}
		else {
			return false;
		}
	}

	public void resetKey()
	{
        mobile.keyTrigger = 0;
	}
	
	/**
	 * jar/jad内のparamを取得する
	 */
	 string[] getAppParam()
	{
         return null;
	}

	/**
	 * (初回)起動フラグを取得する
	 */
	 bool getInitial()
	{
		return ( opt_value[ (int)SAVE.SAVE_INIT ] == 1 );
	}

	/**
	 * (初回)起動フラグを設定する
	 */
	 void setInitial()
	{
		opt_value[ (int)SAVE.SAVE_INIT ] = 1;
	}

	void setColor( int col_r, int col_g, int col_b )
	{
	}

	void fillRect( int x, int y, int wid, int hei )
	{
	}

	void drawLine( int x1, int y1, int x2, int y2 )
	{
	}

	void drawString( string str, int x, int y )
	{
	}

	void drawString2( string str, int x, int y, Font f )
	{
	}

    class Image {
        public void setAlpha(int a) {}
    }
	
	void setImageAlpah( Image img, int alpha )
	{
		img.setAlpha( alpha );
	}

	int stringWidth( string str )
	{
        return 8;
	}

	void setSoftLabel( int key_pos, string label )
	{
	}

	void setSoftLabel( string leftLabel, string rightLabel )
	{
	}
		
    /// <summary>
    /// ブラウザを開く
    /// </summary>
    /// <param name="url">ブラウザに表示するサイトのURL</param>
	public void goWeb( string url )
	{
        Application.OpenURL(url);
	}

    /// <summary>
    /// バージョン更新処理
    /// </summary>
	public void versionUp()
	{
        // TODO C#移植 バージョン更新処理 機種毎の実装はソース単位で分割したい
        switch (Application.platform) {
            case RuntimePlatform.Android:
                break;
            case RuntimePlatform.IPhonePlayer:
                break;
            default:
                Defines.TRACE("versionUp:想定外のプラットフォーム:"+Application.platform.ToString());
                break;
        }
	}


	// コールバック
	// クレジットが0で投入が必要な時に呼ばれる
	public void onCreditZero()
	{
		Defines.TRACE("call onCreditZero");
	}

	// クレジット加算（コイン＋1時）時に呼ばれる
	public void onCreditUp()
	{
        gpif_coin++;
	}

	// クレジット減算（コイン－1時）時に呼ばれる
	public void onCreditUse()
	{
        gpif_coin--;
	}

	// レアシーンを表示した場合にその演出番号が呼ばれる
	public void onReaSceneGet()
	{
		userDirection = ""; // 安全のため消去
	}

	public bool getBusy()
	{
        return false;
	}
	
	// ボーナス内部当選時に呼ばれる
	public void onBonusNaibu()
	{
        Defines.TRACE("★ボーナス確定演出発生");
	}

	// ボーナス当選BB時に呼ばれる
	public void onBonusBB()
	{
		Defines.TRACE("★ビッグボーナス入賞");
        GameManager.Instance.OnBonusBB();
	}

	// ボーナス当選RB時に呼ばれる
	public void onBonusRB()
	{
		Defines.TRACE("★レギュラーボーナス入賞");
        GameManager.Instance.OnBonusRB();
	}

	// ボーナス中JACIN時に呼ばれる
	public void onBonusJACIN()
	{
	}

	// ボーナス終了時に呼ばれる
	public void onBonusEND()
	{
        // 大当たり間ゲーム数カウントクリア
        GameManager.Instance.OnBonusEnd(bonus_incount);
        bonus_incount = 0;
	}

	public void SettingNow()
	{        
	}
	
	// ボーナス中に精算された場合に呼ばれる
	// ※ボーナスを終了させないといけない
	public void onBonusMiddleEND()
	{
	}
		
	// レバーON（回転数が＋1※リプレイ含む）時に呼ばれる
	public void onCountUp()
	{
        GameManager.Instance.OnCountUp();
        if (!omatsuri.IS_BONUS())
        {
            bonus_incount++;
        }
	}

	// 台内部当選確率変更
	public void setMorningBonus( int bb, int rb, int hazure )
    {
		// 100でくるのを*100
		int r;
		Defines.TRACE("★★★★モーニング");

        //
        // TODO
        // 乱数使っているようだがこれはいいのか？
        //

		r = random.Next(10000) & 0xffff;
		
		if( (bb != 0) && (r < (bb*100)))
		{	// BB当選
			Defines.TRACE("モーニングBB");
			gpif_bonus_n = 1;
			return;
		}
		else if( rb != 0 )
		{	//RB当選
			Defines.TRACE("モーニングRB");
			gpif_bonus_n = 2;
			return;
		}
		Defines.TRACE("モーニング 想定外");
	}

	string telopStr;
	bool telopFg;
	long telopTime;

	// テロップのセット
	public void setTelop(string str)
	{
		telopStr = str;
		telopFg = true;
		telopTime = Util.GetMilliSeconds() + 2000;
	}

	// テロップの描画
	public void drawTelop()
	{
		if( telopFg == true)
		{
			if( Util.GetMilliSeconds() > telopTime )
            {
				telopFg = false;
			}
			else {
				setColor( 0, 0, 0 );
				drawString( telopStr, ( 240 - stringWidth( telopStr ) ) / 2 - 1, ( 240 - fontHeight ) / 2 + 0 - 20);
				drawString( telopStr, ( 240 - stringWidth( telopStr ) ) / 2 + 1, ( 240 - fontHeight ) / 2 + 0 - 20);
				drawString( telopStr, ( 240 - stringWidth( telopStr ) ) / 2 + 0, ( 240 - fontHeight ) / 2 - 1 - 20);
				drawString( telopStr, ( 240 - stringWidth( telopStr ) ) / 2 + 0, ( 240 - fontHeight ) / 2 + 1 - 20);
				setColor( 255, 255, 255 );
				drawString( telopStr, ( 240 - stringWidth( telopStr ) ) / 2, ( 240 - fontHeight ) / 2  - 20);
			}
		}
	}

    // メニュー設定の取得
	public int getOptValue(int index)
	{
		return opt_value[ index ];
	}

    // メニュー設定の書き込み
	public void setOptValue(int index, int val)
	{
		opt_value[ index ] = val;
	}

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


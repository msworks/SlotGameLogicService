using System;

public enum Yaku
{
    None = 0x00,            // 0
    Cherry = 0x01,          // 1
    Bell = 0x02,            // 2
    YAKU_WMLN = 0x04,       // 4
    Replay = 0x08,          // 8  リプレイ
    RegulerBonus = 0x10,    // 16 レギュラーボーナス
    JAC = 0x18,             // 24 ジャック
    BigBonus = 0x20,        // 32 ビッグボーナス
    JACIN = 0x28            // 40 ジャックイン BIGBONUS中のリプレイ
}

public class Mobile
{
    public const bool DEF_IS_DOCOMO = true;
    public int keyTrigger = 0;
    private int keyPressing = 0;
    private int keyPressingCount = 0;

    SlotInterface slotInterface;
    Omatsuri mOmatsuri;
    clOHHB_V23 v23;
    GameManager gameManager;
    public ZZ ZZ;

    PLAYSTATE _state;

    // 押下時リール位置
    int[] ReelIndexies;

    // 押下順
    int[] Order;

    /// <summary>
    /// クライアントが押した内容
    /// </summary>
    /// <param name="reelIndexies">押下時リール位置</param>
    /// <param name="order">押下順</param>
    public void SetClientPressed(int[] reelIndexies, int[] order)
    {
        this.ReelIndexies = reelIndexies;
        this.Order = order;
    }

    public int CoinCount
    {
        get
        {
            return mOmatsuri.int_s_value[Defines.DEF_INT_SLOT_COIN_NUM];
        }
    }

    public PLAYSTATE Playstate
    {
        set { _state = value; }
        get { return _state; }
    }

    public Yaku Yaku
    {
        get { return (Yaku)v23.getWork(Defines.DEF_WAVEBIT); }
    }

    public int WinCoin
    {
        get
        {
            return mOmatsuri.int_s_value[Defines.DEF_INT_WIN_COIN_NUM];
        }
    }

    /// <summary>
    /// コイン投入
    /// </summary>
    /// <param name="coinnum">投入枚数</param>
    public void InsertCoin(int coinnum)
    {
        mOmatsuri.GPW_chgCredit(coinnum);
    }

    /// <summary>
    /// 機械割設定
    /// </summary>
    /// <param name="rate">1～6</param>
    public void SetRate(int rate)
    {
        slotInterface.gpif_setting = rate;
    }

    /// <summary>
    /// リールが停止しているか、状態を取得
    /// </summary>
    /// <returns>true:全停止 false:全停止ではない</returns>
    public bool IsReelsStopped()
    {
        return mOmatsuri.IsReelStopped(0) && 
               mOmatsuri.IsReelStopped(1) && 
               mOmatsuri.IsReelStopped(2);
    }

    /// <summary>
    /// 4thリールが停止しているか状態を取得
    /// </summary>
    /// <returns></returns>
    public bool Is4thReelsStopped()
    {
        return mOmatsuri.int_s_value[Defines.DEF_INT_4TH_ACTION_FLAG] == 0;
    }

    public GameManager GameManager { private set; get; }

    public int Seed {
        get
        {
            return gameManager.Seed;
        }
    }

    public Mobile()
    {
        gameManager = new GameManager(this);
        GameManager = gameManager;
        mOmatsuri = new Omatsuri();
        slotInterface = new SlotInterface(this, mOmatsuri, gameManager);
        ZZ = new ZZ();
        //ZZ.setThreadSpeed(1);
        ZZ.setThreadSpeed(20);
        v23 = new clOHHB_V23(mOmatsuri, ZZ);
        ZZ.SetV23(v23);
        ZZ.SetGameManager(gameManager);

        mOmatsuri.SetSlotInterface(slotInterface);
        mOmatsuri.SetclOHHB_V23(v23);
        mOmatsuri.SetMobile(this);
        mOmatsuri.SetZZ(ZZ);
        mOmatsuri.SetGameManager(gameManager);

        int_m_value[Defines.DEF_INT_MODE_REQUEST] = Defines.DEF_MODE_UNDEF;
        int_m_value[Defines.DEF_INT_MODE_CURRENT] = Defines.DEF_MODE_UNDEF;
        int_m_value[Defines.DEF_INT_BASE_OFFSET_X] = (ZZ.getWidth() - Defines.DEF_POS_WIDTH);
        int_m_value[Defines.DEF_INT_BASE_OFFSET_Y] = (ZZ.getHeight() - Defines.DEF_POS_HEIGHT);
        ZZ.setOrigin(int_m_value[Defines.DEF_INT_BASE_OFFSET_X], int_m_value[Defines.DEF_INT_BASE_OFFSET_Y]);
        int_m_value[Defines.DEF_INT_TITLE_BG_START] = ZZ.getBitRandom(32);
        int_m_value[Defines.DEF_INT_GMODE] = Defines.DEF_GMODE_GAME;
        int_m_value[Defines.DEF_INT_SETUP_VALUE_CURSOL] = 3;// 設定４
        setSetUpValue(3);	// 設定４
        int_m_value[Defines.DEF_INT_SUB_MENU_ITEM] = -1; // 選択メニューアイテムの初期化
        int_m_value[Defines.DEF_INT_IS_SOUND] = 1;// 音鳴るよ

        initConfig();
    }

    public void SetKeyTrigger(int key)
    {
        keyTrigger = key;
    }

    public void SetKeyPressing(int key)
    {
        keyPressing = key;
    }

    public void exec(Action<int> returnCoinAction)
    {
        // キー取得
        keyTrigger = ZZ.getKeyPressed();
        keyPressing = ZZ.getKeyPressing();

        if (keyPressing == 0)
        {
            keyPressingCount = 0;
        }
        else
        {
            keyPressingCount++;
        }
        
        // モード切り替えチェック
        if (int_m_value[Defines.DEF_INT_MODE_CURRENT] != int_m_value[Defines.DEF_INT_MODE_REQUEST])
        {
            int_m_value[Defines.DEF_INT_MODE_CURRENT] = int_m_value[Defines.DEF_INT_MODE_REQUEST];
            int_m_value[Defines.DEF_INT_COUNTER] = 0;
        }

        // モードごとに処理分岐
        switch (int_m_value[Defines.DEF_INT_MODE_CURRENT])
        {
            case Defines.DEF_MODE_UNDEF:
                if (!loadMenuData())
                {
                    initConfig();
                    saveMenuData(false);//初期はホールPは保存しない
                    if (DEF_IS_DOCOMO)
                    {
                        break;
                    }
                }

                setMode(Defines.DEF_MODE_TITLE);
                break;

            /* タイトル */
            case Defines.DEF_MODE_TITLE:
                ctrlTitle();
                break;

            /* ゲーム中 */
            case Defines.DEF_MODE_RUN:
                ctrlRun(returnCoinAction);
                break;
        }

    }

    private void ctrlRun(Action<int> returnCoinAction)
    {
        if (mOmatsuri.process(keyTrigger, returnCoinAction, ReelIndexies, Order))
        {
            mOmatsuri.getExitReason();
        }
        mOmatsuri.restartSlot();
        int pos = (mOmatsuri.int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] % 414) * (2359296 / 414);
        ZZ.dbgDrawAll();
    }

    private void ctrlTitle()
    {
        // ゲームを走らす
        setSetUpValue(slotInterface.gpif_setting);
        int_m_value[Defines.DEF_INT_GMODE] = Defines.DEF_GMODE_SIMURATION;
        mOmatsuri.newSlot();
        setMode(Defines.DEF_MODE_RUN);
    }

    /** Mobile内で使うint配列 */
    public int[] int_m_value = new int[Defines.DEF_INT_M_VALUE_MAX];

    /**
     * 目押しサポートあり？<BR>
     * ﾒﾆｭｰで変更されたﾌﾗｸﾞを渡す<BR>
     * @return true:あり false:なし
     */
    public bool isMeoshi()
    {
        // グリパチではモードがない為
        return slotInterface.l_m_bEyeSupport;
    }

    /**
     * Menuボタンの動作可否を設定する<BR>
     * スロットクラスで使用する。RMODE_BETでfalse,RMODE_WAITでtrue<BR>
     * @param flag true:可動 false:非可動
     */
    public void setMenuAvarable(bool flag)
    {
        var value = (flag) ? Defines.DEF_MENU_AVAILABLE
                           : Defines.DEF_MENU_UNAVAILABLE;

        int_m_value[Defines.DEF_INT_IS_MENU_AVAILABLE] = value;
    }

    /**
     * JACカットするかどうか？<BR>
     * ﾒﾆｭｰで変更されたﾌﾗｸﾞを渡す<BR>
     * @return
     */
    public bool isJacCut()
    {
        // グリパチではモードがない為すべてのボーナスカット時とする
        if (mOmatsuri.cutBonus() != 0)
        {
            return true;
        }

        return false;
    }

    /**
     * 設定値を設定する<BR>
     * @return 設定値0~5
     */
    public void setSetUpValue(int val)
    {
        int_m_value[Defines.DEF_INT_SETUP_VALUE] = val;
        v23.setWork(Defines.DEF_WAVENUM, (ushort)val);
    }

    /**
     * 設定値を取得する<BR>
     * ﾀｲﾄﾙから決定キー押下時に設定されるのでMobileで管理します。<BR>
     * @return 設定値0~5
     */
    public int getSetUpValue()
    {
        return int_m_value[Defines.DEF_INT_SETUP_VALUE];
    }

    /**
     * ゲームモードを取得する。ﾀｲﾄﾙ画面で設定する。
     * @return
     */
    public int getGameMode()
    {
        return int_m_value[Defines.DEF_INT_GMODE];
    }

    /**
     * 告知の状態を返す
     * @return
     */
    public int getKokuchi()
    {
        return int_m_value[Defines.DEF_INT_KOKUCHI];
    }

    private void initConfig()
    {
        int_m_value[Defines.DEF_INT_VOLUME] = 40;// 音量２
        int_m_value[Defines.DEF_INT_VOLUME_KEEP] = 40;// 音量２
        int_m_value[Defines.DEF_INT_ORDER] = Defines.DEF_SELECT_6_0;// 押し順順押し
        int_m_value[Defines.DEF_INT_KOKUCHI] = Defines.DEF_SELECT_3_OFF;// こくちOff
        int_m_value[Defines.DEF_INT_IS_JACCUT] = Defines.DEF_SELECT_2_OFF;// JACCUTオフ
        int_m_value[Defines.DEF_INT_IS_DATAPANEL] = Defines.DEF_SELECT_2_ON;// データパネルOFF
        int_m_value[Defines.DEF_INT_IS_VIBRATION] = Defines.DEF_SELECT_2_ON;// データパネルON
    }

    // アクセス関数の都合上-2しないとこける
    public readonly int SAVE_BUFFER = Defines.DEF_SAVE_SIZE - 2;

    /**
     * メニューデータの書き込み
     */
    public void saveMenuData(bool isHall)
    {
        if (!isHall)
        {
            mOmatsuri.prevHttpTime = 0;
            mOmatsuri.kasidasiMedal = 0;
        }

        sbyte[] buf = new sbyte[SAVE_BUFFER];
        int len;

        len = ZZ.getRecord(ref buf);

        if (len <= 0)
        {
            return;
        }

        // 新規作成
        buf[Defines.DEF_SAVE_ORDER] = (sbyte)int_m_value[Defines.DEF_INT_ORDER];
        buf[Defines.DEF_SAVE_DATAPANEL] = (sbyte)int_m_value[Defines.DEF_INT_IS_DATAPANEL];
        buf[Defines.DEF_SAVE_VOLUME] = (sbyte)int_m_value[Defines.DEF_INT_VOLUME];
        buf[Defines.DEF_SAVE_KOKUCHI] = (sbyte)int_m_value[Defines.DEF_INT_KOKUCHI];
        buf[Defines.DEF_SAVE_JACCUT] = (sbyte)int_m_value[Defines.DEF_INT_IS_JACCUT];
        buf[Defines.DEF_SAVE_VIBRATION] = (sbyte)int_m_value[Defines.DEF_INT_IS_VIBRATION];
        buf[Defines.DEF_SAVE_HTTP_TIME0] = (sbyte)(mOmatsuri.prevHttpTime & 0xff);
        buf[Defines.DEF_SAVE_HTTP_TIME1] = (sbyte)((mOmatsuri.prevHttpTime >> 8) & 0xff);
        buf[Defines.DEF_SAVE_HTTP_TIME2] = (sbyte)((mOmatsuri.prevHttpTime >> 16) & 0xff);
        buf[Defines.DEF_SAVE_HTTP_TIME3] = (sbyte)((mOmatsuri.prevHttpTime >> 24) & 0xff);
        buf[Defines.DEF_SAVE_KASIDASI_0] = (sbyte)(mOmatsuri.kasidasiMedal & 0xff);
        buf[Defines.DEF_SAVE_KASIDASI_1] = (sbyte)((mOmatsuri.kasidasiMedal >> 8) & 0xff);
        buf[Defines.DEF_SAVE_KASIDASI_2] = (sbyte)((mOmatsuri.kasidasiMedal >> 16) & 0xff);
        buf[Defines.DEF_SAVE_KASIDASI_3] = (sbyte)((mOmatsuri.kasidasiMedal >> 24) & 0xff);
        buf[Defines.DEF_SAVE_WRITTEN] = 1;
        ZZ.setRecord(buf);
    }

    /**
     * メニューデータの読込
     * @return
     */
    public bool loadMenuData()
    {
        var buf = new sbyte[SAVE_BUFFER];
        var len = 0;

        len = ZZ.getRecord(ref buf);

        if (len <= 0)
        {
            return false;
        }
        // まだデータが無いとき
        if (buf[Defines.DEF_SAVE_WRITTEN] == 0)
        {
            return false;
        }

        int_m_value[Defines.DEF_INT_ORDER] = buf[Defines.DEF_SAVE_ORDER];
        int_m_value[Defines.DEF_INT_IS_DATAPANEL] = buf[Defines.DEF_SAVE_DATAPANEL];
        int_m_value[Defines.DEF_INT_VOLUME] = buf[Defines.DEF_SAVE_VOLUME];
        int_m_value[Defines.DEF_INT_KOKUCHI] = buf[Defines.DEF_SAVE_KOKUCHI];
        int_m_value[Defines.DEF_INT_IS_JACCUT] = buf[Defines.DEF_SAVE_JACCUT];
        int_m_value[Defines.DEF_INT_IS_VIBRATION] = buf[Defines.DEF_SAVE_VIBRATION];

        mOmatsuri.prevHttpTime = ((buf[Defines.DEF_SAVE_HTTP_TIME0] & 0xff)
                             | ((buf[Defines.DEF_SAVE_HTTP_TIME1] & 0xff) << 8)
                             | ((buf[Defines.DEF_SAVE_HTTP_TIME2] & 0xff) << 16)
                             | ((buf[Defines.DEF_SAVE_HTTP_TIME3] & 0xff) << 24));

        mOmatsuri.kasidasiMedal = ((buf[Defines.DEF_SAVE_KASIDASI_0] & 0xff)
                | ((buf[Defines.DEF_SAVE_KASIDASI_1] & 0xff) << 8)
                | ((buf[Defines.DEF_SAVE_KASIDASI_2] & 0xff) << 16)
                | ((buf[Defines.DEF_SAVE_KASIDASI_3] & 0xff) << 24)
                );

        return true;
    }

    /**
     * アプリモードアクセッサ
     * @param a カレントモード
     * @return ノーマルモード
     */
    private int getNormalMode(int a)
    {
        return Defines.DEF_MODE_NORMAL_BITS & a;
    }

    /**
     * メニューアプリモードアクセッサ
     * @param a カレントモード
     * @return メニューモード
     */
    private int getMenuMode(int a)
    {
        return Defines.DEF_MODE_MENU_BIT | getNormalMode(a);
    }

    /**
     * アプリのイベントモード切替指示
     * @param m 変更要求するアプリモード
     */
    private void setMode(int m)
    {
        int_m_value[Defines.DEF_INT_MODE_REQUEST] = m;
    }

    /**
     * スロットによって出す情報が違うので順番に依存します（汗）
     */
    private readonly int[] infoGameData = { 65536, 65536, 65536, // NULLはだめ
	};

    /**
     * 強制停止. mobuilder と mobuilderA の差異を吸収する
     * @param mode サウンドモード
     */
    public void stopSound(int mode)
    {
        if (Defines.DEF_USE_MULTI_SOUND)
        {
            if (mode == Defines.DEF_SOUND_UNDEF)
            {
                ZZ.stopSound(Defines.DEF_SOUND_MULTI_BGM);
                ZZ.stopSound(Defines.DEF_SOUND_MULTI_SE);
                mOmatsuri.bgm_no = -1;
                mOmatsuri.bgm_loop = false;
            }
            else
            {
                ZZ.stopSound(mode);
                if (Defines.DEF_SOUND_MULTI_BGM == mode)
                {
                    mOmatsuri.bgm_no = -1;
                    mOmatsuri.bgm_loop = false;
                }
            }
        }
    }

    public void playSound(int id, bool isRepeat, int mode)
    {
        if (Defines.DEF_USE_MULTI_SOUND)
        {
            ZZ.playSound(id, isRepeat, mode);
        }
    }

    /**
     * リールスピードを取得します
     */
    public int getReelSpeed()
    {
        return (Defines.GP_DEF_INT_SPEED - 20) * 3 + 100;
    }
}
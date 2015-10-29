using System.Collections;
using System.Text;

public class Mobile
{
    public const bool DEF_IS_DOCOMO = true; // TODO C#移植 DOCOMO準拠と仮定
    public int keyTrigger = 0;

    private bool initModeFlag = false;    // モード初期化フラグ
    private int keyPressing = 0;
    private int keyPressingCount = 0;

    SlotInterface slotInterface;
    Omatsuri mOmatsuri;
    clOHHB_V23 v23;
    ZZ ZZ;

    public Mobile()
    {
        //
        // 相互参照しまくりなのだが解決した方がいいのだろうか
        //
        mOmatsuri = new Omatsuri();
        slotInterface = new SlotInterface(this, mOmatsuri);
        ZZ = new ZZ();
        ZZ.setThreadSpeed(20);
        v23 = new clOHHB_V23(mOmatsuri, ZZ);
        ZZ.SetV23(v23);

        mOmatsuri.SetSlotInterface(slotInterface);
        mOmatsuri.SetclOHHB_V23(v23);
        mOmatsuri.SetMobile(this);

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

    public void exec()
    {
        initModeFlag = false;

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
            initModeFlag = true;

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
                        // satoh
                        //					setMode(DEF_MODE_HALL_NOTICE);
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
                ctrlRun();
                break;
        }

    }

    private void ctrlRun()
    {
        /* 初期化ブロック */
        if (initModeFlag)
        {
        }
        if (mOmatsuri.process(keyTrigger))
        {
            mOmatsuri.getExitReason();
        }
        mOmatsuri.restartSlot();
        // 4TH_REEL
        int pos = (mOmatsuri.int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] % 414) * (2359296 / 414);
        ZZ.dbgDrawAll();
    }

    private void ctrlTitle()
    {
        {
            setSetUpValue(slotInterface.gpif_setting);
            // 分析モード
            int_m_value[Defines.DEF_INT_GMODE] = Defines.DEF_GMODE_SIMURATION;
            mOmatsuri.newSlot();
            setMode(Defines.DEF_MODE_RUN);// ゲームを走らす
        }
    }

    /** 広告文座標X */
    int message_x = 240;// TODO const

    /** 広告文座標 dX */
    readonly int message_d = ZZ.getFontHeight() / 4;

    /** Mobile内で使うint配列 */
    public readonly int[] int_m_value = new int[Defines.DEF_INT_M_VALUE_MAX];

    // デフォルトを変更するにはmenuImages[]の初期値を入れ替える.
    /** ヘルプ文字間隔(切り捨て) */
    public static readonly int HELP_string_H = Defines.DEF_POS_HEIGHT / Defines.DEF_HELP_CHAR_Y_NUM;

    /** ヘルプ文字の表示開始位置（高さ） */
    public static readonly int HELP_WINDOW_Y = (Defines.DEF_POS_HEIGHT - HELP_string_H
            * Defines.DEF_HELP_CHAR_Y_NUM) / 2;

    /** ヘルプ文字の左右の幅 */
    public static readonly int HELP_WINDOW_W = ZZ.stringWidth("あ")
            * Defines.DEF_HELP_CHAR_X_NUM;

    /** ヘルプ文字の左右の位置 */
    public static readonly int HELP_WINDOW_X = (Defines.DEF_POS_WIDTH - HELP_WINDOW_W) / 2;

    /**
     * 目押しサポートあり？<BR>
     * ﾒﾆｭｰで変更されたﾌﾗｸﾞを渡す<BR>
     * 
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
     * 
     * @param flag
     *            true:可動 false:非可動
     */
    public void setMenuAvarable(bool flag)
    {
        int_m_value[Defines.DEF_INT_IS_MENU_AVAILABLE] = (flag) ? Defines.DEF_MENU_AVAILABLE
                : Defines.DEF_MENU_UNAVAILABLE;
        if (flag)
        {
        }
        else
        {
        }
    }

    /**
     * JACカットするかどうか？<BR>
     * ﾒﾆｭｰで変更されたﾌﾗｸﾞを渡す<BR>
     * 
     * @see Defines.DEF_JAC_CUT_ON
     * @see Defines.DEF_JAC_CUT_OFF
     * @return
     */
    public bool isJacCut()
    {
        // グリパチではモードがない為
        // すべてのボーナスカット時とする
        if (mOmatsuri.cutBonus() != 0)
        {
            return true;
        }
        return false;
    }

    /**
     * 設定値を設定する<BR>
     * 
     * @return 設定値0~5
     */
    public void setSetUpValue(int val)
    {

        int_m_value[Defines.DEF_INT_SETUP_VALUE] = val;
        // 内部設定の変更(Z80関係はこっちかな？)
        v23.setWork(Defines.DEF_WAVENUM, (ushort)val);
    }

    /**
     * 設定値を取得する<BR>
     * ﾀｲﾄﾙから決定キー押下時に設定されるのでMobileで管理します。<BR>
     * 
     * @return 設定値0~5
     */
    public int getSetUpValue()
    {
        return int_m_value[Defines.DEF_INT_SETUP_VALUE];
    }

    /**
     * ゲームモードを取得する。<BR>
     * ﾀｲﾄﾙ画面で設定する。
     * 
     * @see DEF_GMODE_GAME
     * @see DEF_GMODE_SIMURATION
     * @see DEF_GMODE_BATTLE
     * @return
     */
    public int getGameMode()
    {
        return int_m_value[Defines.DEF_INT_GMODE];
    }

    /**
     * 告知の状態を返す
     * 
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
        //		setVolume(int_m_value[Defines.DEF_INT_VOLUME]);
        //		int_m_value[Defines.DEF_INT_SPEED] = 20;// スピード３
        //		int_m_value[Defines.DEF_INT_IS_MEOSHI] = DEF_SELECT_2_OFF;// 目押しOff
        int_m_value[Defines.DEF_INT_ORDER] = Defines.DEF_SELECT_6_0;// 押し順順押し
        int_m_value[Defines.DEF_INT_KOKUCHI] = Defines.DEF_SELECT_3_OFF;// こくちOff
        int_m_value[Defines.DEF_INT_IS_JACCUT] = Defines.DEF_SELECT_2_OFF;// JACCUTオフ
        int_m_value[Defines.DEF_INT_IS_DATAPANEL] = Defines.DEF_SELECT_2_ON;// データパネルOFF
        int_m_value[Defines.DEF_INT_IS_VIBRATION] = Defines.DEF_SELECT_2_ON;// データパネルON
    }

    public readonly int SAVE_BUFFER = Defines.DEF_SAVE_SIZE - 2; // アクセス関数の都合上-2しないとこける

    // ///////////////////////////////////////////////////////////////////////
    // メニューデータの管理
    // ///////////////////////////////////////////////////////////////////////
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
     * 
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
     * 
     * @param a
     *            カレントモード
     * @return ノーマルモード
     */
    private int getNormalMode(int a)
    {
        return Defines.DEF_MODE_NORMAL_BITS & a;
    }

    /**
     * メニューアプリモードアクセッサ
     * 
     * @param a
     *            カレントモード
     * @return メニューモード
     */
    private int getMenuMode(int a)
    {
        return Defines.DEF_MODE_MENU_BIT | getNormalMode(a);
    }

    /**
     * アプリのイベントモード切替指示
     * 
     * @param m
     *            変更要求するアプリモード
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
     * 
     * @param mode
     *            サウンドモード
     * @see Df#SOUND_MULTI_SE
     * @see Df#SOUND_MULTI_BGM
     * @see Df#SOUND_UNDEF
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
        else
        {
            ZZ.stopSound();
        }
    }

    public void playSound(int id, bool isRepeat, int mode)
    {
        if (Defines.DEF_USE_MULTI_SOUND)
        {
            ZZ.playSound(id, isRepeat, mode);
        }
        else
        {
            ZZ.playSound(id, isRepeat);
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
using System;

public class Omatsuri
{
    SlotInterface slotInterface = null;
    Mobile mobile;
    ZZ ZZ;
    clOHHB_V23 v23;
    GameManager GameManager;

    public long reelwait = 0;
    public bool reqMenuFg2 = false;	// リール停止時にメニューを表示する
    public bool bgm_resumeFg = false;	// BGMのレジューム再生フラグ
    public int bgm_no = -1;
    public bool bgm_loop = false;
    public int maj_ver;	// メインバージョン
    public int sub_ver;	// サブバージョン
    public int prevHttpTime = 0;
    public int kasidasiMedal = 0;
    public int prevkasidasiMedal = 0;
    public int[] hallData = new int[Defines.DEF_H_PARAM_NUM];

    private bool bigBonusFg = false;
    private bool BonusCutFg = false;
    private bool reelStartFg;	// リールスタートキーがすでに押されている場合
    private bool reqMenuFg = false;	// リール停止時にメニューを表示する

    //int mes_x = 0;
    bool IS_HALL() { return mobile.getGameMode() == Defines.DEF_GMODE_HALL; }

    /** int プール */
    public readonly int[] int_s_value = new int[Defines.DEF_INT_SLOT_VALUE_MAX];

    /** ｻｳﾝﾄﾞ演奏時間 */
    private long _soundTime;

    /** WAIT３０秒でランプが消える */
    private long _lampTime;

    /** ４０秒で回転は止まる */
    private long _spinTime;

    /** ４thリールの一時停止 */
    private long _4thTime;

    /** リールの角度 [1.15.16] の固定少数 */
    private static readonly int ANGLE_2PI_BIT = 16;

    /** リールの角度マスク(0xFFFF)。 */
    private static readonly int ANGLE_2PI_MASK = (1 << ANGLE_2PI_BIT) - 1;

    /** リールの角度が未定義（INDEX2ANGLE では出ない値）)。 */
    private readonly int ANGLE_UNDEF = -1;

    private readonly ushort[,] bonus_Data = new ushort[Defines.DEF_INFO_GAME_HISTORY, Defines.DEF_INFO_GAMES];

    public readonly ushort[][] REELTB = {
		new ushort[]{ Defines.DEF_RPLY, Defines.DEF_BAR_, Defines.DEF_BELL, Defines.DEF_WMLN, Defines.DEF_CHRY, Defines.DEF_BSVN, Defines.DEF_RPLY,
		Defines.DEF_BELL, Defines.DEF_DON_, Defines.DEF_DON_, Defines.DEF_DON_, Defines.DEF_RPLY, Defines.DEF_BELL,
		Defines.DEF_CHRY, Defines.DEF_BSVN, Defines.DEF_WMLN, Defines.DEF_RPLY, Defines.DEF_BELL, Defines.DEF_WMLN,
		Defines.DEF_BELL, Defines.DEF_BSVN },
		new ushort[]{ Defines.DEF_BELL, Defines.DEF_BAR_, Defines.DEF_CHRY, Defines.DEF_BELL, Defines.DEF_RPLY, Defines.DEF_BSVN, Defines.DEF_WMLN,
		Defines.DEF_CHRY, Defines.DEF_BELL, Defines.DEF_RPLY, Defines.DEF_BAR_, Defines.DEF_CHRY, Defines.DEF_BAR_,
		Defines.DEF_BELL, Defines.DEF_RPLY, Defines.DEF_CHRY, Defines.DEF_DON_, Defines.DEF_BELL, Defines.DEF_RPLY,
		Defines.DEF_WMLN, Defines.DEF_RPLY },
		new ushort[]{ Defines.DEF_BELL, Defines.DEF_CHRY, Defines.DEF_RPLY, Defines.DEF_WMLN, Defines.DEF_BELL, Defines.DEF_BSVN, Defines.DEF_CHRY,
		Defines.DEF_RPLY, Defines.DEF_WMLN, Defines.DEF_BELL, Defines.DEF_CHRY, Defines.DEF_RPLY, Defines.DEF_WMLN,
		Defines.DEF_BELL, Defines.DEF_DON_, Defines.DEF_RPLY, Defines.DEF_CHRY, Defines.DEF_BELL, Defines.DEF_BAR_,
		Defines.DEF_RPLY, Defines.DEF_WMLN}
	};

    /**
     * AUTO GENERATED char ARRAY BY compact.CompactClass
     */
    private readonly char[] width4th = "\u0075\u003F\u003B\u0037\u0040\u0038"
            .ToCharArray();

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public Omatsuri(){}

    public void SetSlotInterface(SlotInterface slotInterface)
    {
        this.slotInterface = slotInterface;
    }

    public void SetclOHHB_V23(clOHHB_V23 v23)
    {
        this.v23 = v23;
    }

    public void SetZZ(ZZ ZZ)
    {
        this.ZZ = ZZ;
    }

    public void SetMobile(Mobile mobile)
    {
        this.mobile = mobile;
    }

    public void SetGameManager(GameManager GameManager)
    {
        this.GameManager = GameManager;
    }

    /**
     * データパネル色.
     * 
     * @see df.Df#GAME_NONE なし
     * @see df.Df#GAME_NORMAL 通常
     * @see df.Df#GAME_BIG ＢＩＧ
     * @see df.Df#GAME_REG ＲＥＧ
     * @see df.Df#GAME_CURRENT カレント
     * @see df.Df#GAME_NUM
     */
    private int[] panel_colors = {
	    ZZ.getColor(0x00, 0x00, 0x00),// なし
	    ZZ.getColor(Defines.DEF_POS_CELL_COLOR_ETC_R,
                    Defines.DEF_POS_CELL_COLOR_ETC_G,
			        Defines.DEF_POS_CELL_COLOR_ETC_B),// 通常
	    ZZ.getColor(Defines.DEF_POS_CELL_COLOR_BB_R,
                    Defines.DEF_POS_CELL_COLOR_BB_G,
			        Defines.DEF_POS_CELL_COLOR_BB_B),// BB
	    ZZ.getColor(Defines.DEF_POS_CELL_COLOR_RB_R,
                    Defines.DEF_POS_CELL_COLOR_RB_G,
			        Defines.DEF_POS_CELL_COLOR_RB_B),// RB
	    ZZ.getColor(0xFF, 0x00, 0xFF),
    };

    /**
     * 次のランプ用ステータスを取得する
     * 
     * @param idx
     * @return ランプの次のステータス
     */
    public int getLampStatus(int idx)
    {
        if ((int_s_value[Defines.DEF_INT_LAMP_1 + (idx / 32)] & (1 << (idx % 32))) != 0)
        {
            return Defines.DEF_LAMP_STATUS_ON;
        }
        else
        {
            return Defines.DEF_LAMP_STATUS_OFF;
        }
    }

    /**
     * ランプ用スイッチ
     * 
     * @param idx:index
     * @param act:action
     */
    public void lampSwitch(int idx, int act)
    {
        if (act == Defines.DEF_LAMP_ACTION_ON)
        {
            // ビットを1にする
            int_s_value[Defines.DEF_INT_LAMP_1 + (idx / 32)] |= (1 << (idx % 32));
        }
        else if (act == Defines.DEF_LAMP_ACTION_OFF)
        {
            // ビットを0にする
            int_s_value[Defines.DEF_INT_LAMP_1 + (idx / 32)] &= ~(1 << (idx % 32));
        }
    }

    /**
     * リール位置番号から角度を求める。 <BR>
     * INDEX2ANGLE(n) ((ANGLE_2PI+NUM_REEL-1)/NUM_REEL*((n)%NUM_REEL)) <BR>
     * 切り上げ計算も含む <BR>
     * 
     * @param n:絵柄ｲﾝﾃﾞｯｸｽ
     *            [0, NUM_REEL)
     * @return ﾘｰﾙ角度？
     */
    private int INDEX2ANGLE(int n)
    {
        return 0xC31 * (n);
    }

    /**
     * 角度から次に止まれる絵柄インデックスに変換。 <BR>
     * 端数切り上げ
     * 
     * @param i:角度
     * @return 絵柄ｲﾝﾃﾞｯｸｽ [0, NUM_REEL)
     */
    public int ANGLE2INDEX(int i)
    {
        return ((Defines.DEF_N_FRAME * i + ANGLE_2PI_MASK) >> ANGLE_2PI_BIT)
                % Defines.DEF_N_FRAME;
    }

    /**
     * モードをリクエストする。
     * 
     * @param mod:スロットゲームのモード
     * @return リクエストされたスロットゲームのモード
     */
    private int REQ_MODE(int mod)
    {
        return int_s_value[Defines.DEF_INT_REQUEST_MODE] = mod;
    }

    public void initGameInfo()
    {
        Defines.TRACE("ゲーム情報の初期化");
        int_s_value[Defines.DEF_INT_BIG_COUNT] = 0;
        int_s_value[Defines.DEF_INT_REG_COUNT] = 0;
        int_s_value[Defines.DEF_INT_UNTIL_BONUS_GAMES] = 0;
        int_s_value[Defines.DEF_INT_TOTAL_GAMES] = 0;
        int_s_value[Defines.DEF_INT_GAME_INFO_MAX_GOT] = 0;
        int_s_value[Defines.DEF_INT_SLOT_COIN_NUM] = Defines.DEF_NUM_START_COIN; // コイン数;
        int_s_value[Defines.DEF_INT_NUM_KASIDASI] = 1;
        initDataPaneHistory();
    }

    private void initDataPaneHistory()
    {
        int_s_value[Defines.DEF_INT_BONUS_DATA_BASE] = 0;
        for (int x = 0; x < Defines.DEF_INFO_GAME_HISTORY; x++)
        {
            for (int y = 0; y < Defines.DEF_INFO_GAMES; y++)
            {
                bonus_Data[x, y] = Defines.DEF_GAME_NONE;
            }
        }
    }

    public void newSlot()
    {
        for (int i = 0; i < int_s_value.Length; i++)
        {
            if (i != Defines.DEF_INT_REEL_SPEED)
            {// リールスピードだけ除外
                int_s_value[i] = 0;
            }
        }

        Defines.TRACE("newSlot");

        // メインループスピード
        int_s_value[Defines.DEF_INT_LOOP_SPEED] = ZZ.getThreadSpeed();

        // リールの初期化
        int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM] = 0; // クレジットコイン数（０枚）
        int_s_value[Defines.DEF_INT_WIN_GET_COIN] = 0; // 払い出しコイン枚数
        int_s_value[Defines.DEF_INT_WIN_COIN_NUM] = 0; // １ゲーム中の獲得コイン枚数
        int_s_value[Defines.DEF_INT_BONUS_GOT] = 0; // ボーナス獲得数
        int_s_value[Defines.DEF_INT_BONUS_JAC_GOT] = 0; //JACゲーム中の獲得枚数
        int_s_value[Defines.DEF_INT_BB_KIND] = Defines.DEF_BB_UNDEF; // ＢＢ入賞時の種別
        int_s_value[Defines.DEF_INT_BB_AFTER_1GAME] = 0; // BB 終了後の１ゲーム目？					
        int_s_value[Defines.DEF_INT_BB_END_1GAME_REGET_BB] = 0; // BB 終了後の１ゲーム目でそろえることができた？ミッションパラメータ		
        int_s_value[Defines.DEF_INT_BB_GET_OVER711] = 0; // BB獲得枚数 711枚以上？ミッションミッションパラメータ

        // モードの初期化
        int_s_value[Defines.DEF_INT_CURRENT_MODE] = Defines.DEF_RMODE_UNDEF;
        REQ_MODE(Defines.DEF_RMODE_WAIT);

        // ランプを全て消灯
        int_s_value[Defines.DEF_INT_LAMP_1] = 0;
        int_s_value[Defines.DEF_INT_LAMP_2] = 0;
        int_s_value[Defines.DEF_INT_LAMP_3] = 0;

        int_s_value[Defines.DEF_INT_KOKUCHI_ID] = 0;

        // Topランプアクション初期値
        int_s_value[Defines.DEF_INT_SEQUENCE_EFFECT] = 2;
        int_s_value[Defines.DEF_INT_RLL_COUNTER] = 0;

        int_s_value[Defines.DEF_INT_4TH_ACTION_FLAG] = 0;
        int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] = 0;
        int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] = Defines.DEF_RP19;

        playSE(Defines.DEF_RES_00);
        playBGM(Defines.DEF_RES_00, false);
        mobile.stopSound(Defines.DEF_SOUND_UNDEF);

        // ゲーム情報の初期化
        initGameInfo();

        Defines.TRACE("gp:" + slotInterface);
        Defines.TRACE("initDataPaneHistory");

        initDataPaneHistory();

        Defines.TRACE("Z80移植リールクラスの初期化");

        // /////////////////////////////////////////
        // Z80移植リールクラスの初期化
        // /////////////////////////////////////////

        try
        {
            var seed = GameManager.Seed;
            v23.mInitializaion(seed);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw ex;
        }

        // 内部設定を6にする。
        v23.setWork(Defines.DEF_WAVENUM, (ushort)mobile.getSetUpValue());

        // 停止フラグを立てる
        int_s_value[Defines.DEF_INT_REEL_STOP_R0] = int_s_value[Defines.DEF_INT_REEL_ANGLE_R0];
        int_s_value[Defines.DEF_INT_REEL_STOP_R1] = int_s_value[Defines.DEF_INT_REEL_ANGLE_R1];
        int_s_value[Defines.DEF_INT_REEL_STOP_R2] = int_s_value[Defines.DEF_INT_REEL_ANGLE_R2];
        int_s_value[Defines.DEF_INT_IS_REEL_STOPPED] = 7; // リールストップ

        // 3D関係
        //Mascot3D.initModel();
        ZZ.setLight(0, 0, 100, 2024 * 100 / 4048, 1012 * 100 / 4048);

        Defines.TRACE("全体描画");
        restartSlot();
    }

    /**
     * ゲーム再開。
     * 
     */
    public void restartSlot()
    {
        drawAll(); // 再描画要求
    }

    /**
     * ゲーム終了。 アステカ以降、清算処理失敗でゲームに戻ってきた時に、持ちメダル数として足せなかったクレジット数が残るようにした。
     * 
     * 例）CREDIT = 45, SLOT_COIN_NUM = 99996の状態で清算して失敗したら、CREDIT = 42,
     * SLOT_COIN_NUM = 99999となってゲーム再開
     * 
     * @return 現在の獲得コイン枚数
     */
    public int endSlot()
    {
        mobile.stopSound(Defines.DEF_SOUND_UNDEF); // サウンド停止
        return int_s_value[Defines.DEF_INT_TOTAL_PAY] - int_s_value[Defines.DEF_INT_TOTAL_BET];
    }

    public int getExitReason()
    {
        return int_s_value[Defines.DEF_INT_WHAT_EXIT];
    }

    public void setExitReason(int arg)
    {
        int_s_value[Defines.DEF_INT_WHAT_EXIT] = arg;
    }

    // ======================================================
    // TOBE [メイン制御]
    // ======================================================
    private int pressingSpan = 0;

    /**
     * ここの処理はmobileクラスからゲーム実行中（MODE_RUN）のときだけ呼び出される。
     * 
     * スロットゲームとしての各モードにおける振る舞いを記述
     * 
     * @param keyTrigger
     *            押されたキーを取得
     * @return true=スロット終了,false=スロット継続
     * 
     * @see #int_s_value
     * @see df.Df#INT_CURRENT_MODE
     * @see df.Df#INT_REQUEST_MODE
     */

    // satoh#メニュー割り込みにアイテムを追加
    public int req_code;

    public bool process(int keyTrigger)
    {
        int_s_value[Defines.DEF_INT_REEL_SPEED] =
            ZZ.getThreadSpeed()
            * Defines.DEF_REEL_COUNT_MIN
            * 0x10000
            / 60000
            * mobile.getReelSpeed() / 100;

        //DfMain.TRACE(("オートプレイ:" + gp.gpif_auto_f + ":" + gp.gpif_nonstop_f + ":" + gp.gpif_tatsujin_f + ":" + mobile.isMeoshi());
        if (mobile.isMeoshi() ||
            (slotInterface.gpif_auto_f == true) ||
            (slotInterface.gpif_nonstop_f == true) ||
            (slotInterface.gpif_tatsujin_f == true))
        {	// ｵｰﾄﾌﾟﾚｲ
            if ((slotInterface.gpif_auto_f == true) ||
                (slotInterface.gpif_nonstop_f == true) ||
                (slotInterface.gpif_tatsujin_f == true))
            {	// ｵｰﾄﾌﾟﾚｲの予約
                //DfMain.TRACE(("test:" + mobile.int_m_value[Defines.DEF_INT_IS_MENU_AVAILABLE]);
                // スロットゲーム中WAIT時以外は遷移しない
                if (mobile.int_m_value[Defines.DEF_INT_IS_MENU_AVAILABLE] == Defines.DEF_MENU_UNAVAILABLE)
                {	// メニューが無効の時
                    //DfMain.TRACE(("ここきてる？:" + keyTrigger);
                    if ((keyTrigger & Defines.DEF_KEY_BIT_SOFT1) != 0)
                    {	// ソフトキーがおされたら
                        //DfMain.TRACE(("メニュー予約！！！");
                        req_code = 1;
                        reqMenuFg = true;
                    }
                    if ((keyTrigger & Defines.DEF_KEY_BIT_SOFT2) != 0)
                    {	// ソフトキーがおされたら
                        //DfMain.TRACE(("メニュー予約！！！");
                        req_code = 2;
                        reqMenuFg = true;
                    }
                }
            }
            keyTrigger = Defines.DEF_KEY_BIT_SELECT;
        }

        if (reqMenuFg == true)
        {
            // 自動メニュー画面描画
            if (mobile.int_m_value[Defines.DEF_INT_IS_MENU_AVAILABLE] == Defines.DEF_MENU_AVAILABLE)
            {
                // メニューが無効の時
                reqMenuFg = false;
            }
        }

        if (slotInterface.getBusy())
        {
            // GP用のウインドウが出ているので、筐体キーを無効化する
            keyTrigger = 0;
            return false;
        }

        // コイン枚数の更新
        int_s_value[Defines.DEF_INT_SLOT_COIN_NUM] = slotInterface.gpif_coin;

        Console.WriteLine("[INFO]COIN:" + int_s_value[Defines.DEF_INT_SLOT_COIN_NUM]);

        // 40ms*10毎にタイミングを取ってみる
        pressingSpan++;
        pressingSpan %= 10;
        if (!IS_BONUS() && pressingSpan == 0)
        {
        }

        ////////////////////////////////////
        // このwhile文はループさせるためではなく、違うモードにすぐ遷移(conitnue)するための仕組みです。
        while (int_s_value[Defines.DEF_INT_CURRENT_MODE] != int_s_value[Defines.DEF_INT_REQUEST_MODE])
        {
            if (Defines.DEF_IS_DEBUG_PRINT_RMODE)
            {
                Defines.TRACE("RMODE: " + int_s_value[Defines.DEF_INT_CURRENT_MODE]
                        + " → " + int_s_value[Defines.DEF_INT_REQUEST_MODE]);
            }

            int_s_value[Defines.DEF_INT_CURRENT_MODE] = int_s_value[Defines.DEF_INT_REQUEST_MODE];

            // スロットゲームモード変更時に更新するフラグ
            int_s_value[Defines.DEF_INT_MODE_COUNTER] = 0;
            int_s_value[Defines.DEF_INT_ON_OFF_EFFECT] = 0;
            int_s_value[Defines.DEF_INT_RLPTNDT_COUNTER] = 0;
            int_s_value[Defines.DEF_INT_TOP_LAMP] = 0;

            // 各モードでの初期化
            switch (int_s_value[Defines.DEF_INT_CURRENT_MODE])
            {
                case Defines.DEF_RMODE_WIN:
                    if (int_s_value[Defines.DEF_INT_WIN_LAMP] > 0
                            && int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] == 0)
                    {
                        // た～まや～点灯
                        int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] = 1;
                        // ボーナス当選時
                        slotInterface.onBonusNaibu();
                        // セルフオート停止フラグを立てる
                        GameManager.StopAutoPlay("たまや点灯");
                    }

                    // さらに大当たりで止まっていたらかっきーん！
                    if (int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] == 1)
                    {
                        if (int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] == Defines.DEF_RP08)
                        {
                            playSE(Defines.DEF_SOUND_12);
                            _soundTime = Util.GetMilliSeconds() + Defines.DEF_SOUND_MS_12;
                            int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] = 2;
                            // 以後演出抽選しないようにする
                            v23.setWork(Defines.DEF_FOUT3, (ushort)1);
                        }
                    }
                    break;
                // TOBE [=0.RMODE_WAIT init]137
                case Defines.DEF_RMODE_WAIT: // (モード初期)

                    if (IS_HALL())
                    {
                        mobile.saveMenuData(true);//不正防止用にここで保存
                    }
                    mobile.setMenuAvarable(true);// 押せるようにする

                    if (!IS_BONUS())
                    {
                        lampSwitch(Defines.DEF_LAMP_4TH, Defines.DEF_LAMP_ACTION_OFF);
                    }
                    // コイン７セグ描画用変数を初期化
                    int_s_value[Defines.DEF_INT_BETTED_COUNT] = 0;
                    Defines.TRACE("待機中");

                    // 演出帳のデータを転送する
                    GPW_eventProcess((int)Defines.EVENT_PROC.EVENT_PROC_WEB, -1);
                    break;

                case Defines.DEF_RMODE_BET: // MAXBETﾗﾝﾌﾟ表示期間(モード初期)
                    //DfMain.TRACE(("ベット処理１");
                    // リール全点滅は一回だけ
                    if (int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] == 2)
                    {
                        int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] = 3;
                    }

                    // 払い出しコイン枚数（表示用）
                    int_s_value[Defines.DEF_INT_WIN_GET_COIN] = 0;
                    int_s_value[Defines.DEF_INT_WIN_COIN_NUM] = 0; // １ゲーム中の獲得コイン枚数
                    // ﾃﾝﾊﾟｲ状態
                    int_s_value[Defines.DEF_INT_IS_TEMPAI] = 0;

                    // 告知はBET時にクリア
                    if (!IS_REPLAY())
                    {
                        int_s_value[Defines.DEF_INT_KOKUCHI_X] = 0;
                    }

                    // サウンド
                    if (!IS_BONUS())
                    {
                        // ボーナス消化中以外は毎回停止指示。
                        mobile.stopSound(Defines.DEF_SOUND_UNDEF);
                    }
                    {

                        if (IS_HALL())
                        {/*プレーヤーコイン要求*/
                            if (hallData[Defines.DEF_H_PLAYER_COIN] < int_s_value[Defines.DEF_INT_BET_COUNT])
                            {
                                //鳴らさない
                                //DfMain.TRACE(("ベット音ならさない");
                                break;
                            }
                        }
                        //DfMain.TRACE(("ベット音ならす");
                        switch (int_s_value[Defines.DEF_INT_BET_COUNT])
                        {
                            case 1:
                                playSE(Defines.DEF_SOUND_25);
                                _soundTime = Util.GetMilliSeconds() + Defines.DEF_SOUND_MS_25;
                                break;
                            case 2:
                                playSE(Defines.DEF_SOUND_26);
                                _soundTime = Util.GetMilliSeconds() + Defines.DEF_SOUND_MS_26;
                                break;
                            case 3:
                                playSE(Defines.DEF_SOUND_22);
                                _soundTime = Util.GetMilliSeconds() + Defines.DEF_SOUND_MS_22;
                                break;
                        }
                    }

                    // MENU遷移を禁止する
                    mobile.setMenuAvarable(false);

                    break;// RMODE_BET
                // TOBE [=2.RMODE_SPIN init]
                case Defines.DEF_RMODE_SPIN: // 回転開始(モード初期)
                    if (!IS_REPLAY())
                    {
                        int_s_value[Defines.DEF_INT_TOTAL_BET] += int_s_value[Defines.DEF_INT_BETTED_COUNT];
                        /**/
                        hallData[Defines.DEF_H_MEDAL_IN] += int_s_value[Defines.DEF_INT_BETTED_COUNT];
                        hallData[Defines.DEF_H_PLAYER_COIN] -= int_s_value[Defines.DEF_INT_BETTED_COUNT];
                    }

                    // ///////////////////////////
                    // Z80移植リール部分の初期化
                    // 1プレイ遊技用初期化
                    v23.clearWork(Defines.DEF_CLR_AREA_3);
                    // ///////////////////////////
                    // ///////////////////////////
                    // Z80移植
                    // パチスロ抽選用乱数を取得
                    int rand = v23.mRandomX();

                    // 設定変更チェック
                    chgWaveNum();

                    // 役の変更チェック
                    chgYaku();

                    // 役抽選
                    v23.mReelStart(rand, int_s_value[Defines.DEF_INT_BET_COUNT]);

                    if (!IS_BONUS())
                    {
                        if ((v23.getWork(Defines.DEF_WAVEBIT) & 0x01) != 0)
                        {
                            int_s_value[Defines.DEF_INT_CHRY_HIT]++;
                        }
                        else if ((v23.getWork(Defines.DEF_WAVEBIT) & 0x04) != 0)
                        {
                            int_s_value[Defines.DEF_INT_WMLN_HIT]++;
                        }
                    }
                    else if (IS_BONUS_GAME())
                    {
                        // ビタ外し成功
                        if ((v23.getWork(Defines.DEF_WAVEBIT) & 0x08) != 0)
                        {
                            int_s_value[Defines.DEF_INT_JAC_HIT]++;
                        }
                    }

                    if ((v23.getWork(Defines.DEF_GMLVSTS) & (0x08 | 0x10)) != 0)
                    {
                        int_s_value[Defines.DEF_INT_THIS_FLAG_GAME]++;
                    }

                    // 演出をセット
                    int flash0 = v23.getWork(Defines.DEF_FLASH + 0);

                    // DfMain.TRACE(("******************************************");
                    // DfMain.TRACE(("****FLASH+0 A:\t"+(data/128));
                    // DfMain.TRACE(("****FLASH+0 B:\t"+((data%128)/64));
                    // DfMain.TRACE(("****FLASH+0 C:\t"+((data%128)%64));
                    // ; 回転表示器デモパターン抽選テーブル
                    // ; XXX 確率値 (/128)
                    // ; A*128+B*64+C 演出パラメータ & 演出確率
                    // ; A 当たり確定ランプ点灯フラグ
                    // ; B 遊技開始音選択ビット
                    // ; C 抽選テーブル番号 ( 0~63 )

                    // 演出によるチェック
                    GPW_eventProcess((int)Defines.EVENT_PROC.EVENT_PROC_CHK_LANP, (flash0 / 128));

                    // 確定ランプフラグ
                    int_s_value[Defines.DEF_INT_WIN_LAMP] = flash0 / 128;
                    flash0 %= 128;
                    // 開始音
                    // ; A*32+B 演出パターンデータ
                    // ; A = フラッシュ演出パターンデータ
                    // ; ( 0~7 )
                    // ; B = リール演出パターンデータ
                    // ; ( 0~31 )
                    int flash1 = v23.getWork(Defines.DEF_FLASH + 1);
                    // DfMain.TRACE(("****FLASH+1 A:\t"+(data/32));
                    // DfMain.TRACE(("****FLASH+1 B:\t"+(data%32));
                    // DfMain.TRACE(("******************************************");

                    // 演出によるチェック
                    GPW_eventProcess((int)Defines.EVENT_PROC.EVENT_PROC_CHK_FLASH, flash1);
                    setFlash(flash1 / 32);
                    set4th(flash1 % 32);

                    // 開始音を鳴らす
                    int snd_id = Defines.DEF_SOUND_19;
                    if (flash1 % 32 > 0)
                    {
                        snd_id = Defines.DEF_SOUND_21;
                    }
                    if (flash0 / 64 > 0)
                    {
                        snd_id = Defines.DEF_SOUND_20;
                    }

                    if (mobile.isJacCut() == false)
                    {
                        playSE(snd_id);
                    }

                    // 回転開始時間を記録する
                    _spinTime = Util.GetMilliSeconds() + Defines.DEF_WAIT_SPIN;
                    int_s_value[Defines.DEF_INT_IS_REEL_STOPPED] = 0; // リールストップ

                    // 告知はSPIN時にクリア
                    int_s_value[Defines.DEF_INT_KOKUCHI_X] = 0;

                    // ボーナス（ＢＢ・ＲＢ）終了ですか？
                    if (int_s_value[Defines.DEF_INT_IS_BB_RB_END] > 0)
                    {
                        // ここはボーナス終了ゲームの次ゲームの回転開始です

                        if (!IS_HALL())
                        {
                            // データパネル更新
                            if (int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_B7
                                    || int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_R7)
                            { // ＢＢ終了時のみ
                                shiftDataPanelHistory(
                                        int_s_value[Defines.DEF_INT_UNTIL_BONUS_GAMES],
                                        Defines.DEF_PS_BB_RUN);
                            }
                            else if (int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_RB_IN)
                            { // ＲＢ終了時のみ
                                shiftDataPanelHistory(
                                        int_s_value[Defines.DEF_INT_UNTIL_BONUS_GAMES],
                                        Defines.DEF_PS_RB_RUN);
                            }
                        }

                        // ボーナス関係のフラグたちをクリア
                        int_s_value[Defines.DEF_INT_IS_BB_RB_END] = 0; // ボーナス終了後の次ゲームの回転でこのフラグおろす
                        int_s_value[Defines.DEF_INT_BB_KIND] = Defines.DEF_BB_UNDEF; // ＢＢ入賞時の種別

                        int_s_value[Defines.DEF_INT_BONUS_GOT] = 0; // ボーナス獲得枚数の値をクリア
                        int_s_value[Defines.DEF_INT_BONUS_JAC_GOT] = 0;	// JAC中の獲得枚数をクリア
                        // ボーナス間ゲーム数をボーナス終了時にクリア
                        int_s_value[Defines.DEF_INT_UNTIL_BONUS_GAMES] = 0;
                        /**/
                        hallData[Defines.DEF_H_BNS_0] = 0;


                        // TOBE 個別PARAM　BB 終了後の１ゲーム目ですよフラグを立てる
                        int_s_value[Defines.DEF_INT_BB_AFTER_1GAME] = 1;

                    }

                    if (!IS_BONUS())
                    {
                        // 通常ゲーム中

                        // ボーナス消化中のゲーム数を総回転数としてカウントしない
                        int_s_value[Defines.DEF_INT_TOTAL_GAMES]++;

                        // 総回転数を増やす
                        //DfMain.TRACE(("総回転数のカウントアップ");
                        slotInterface.onCountUp();

                        /**/
                        hallData[Defines.DEF_H_GAME_COUNT]++;

                        // ボーナス間累積（ゲーム情報）
                        int_s_value[Defines.DEF_INT_UNTIL_BONUS_GAMES]++;
                        /**/
                        hallData[Defines.DEF_H_BNS_0]++;
                    }

                    // データパネル情報更新（ゲーム数）
                    // データパネル情報更新（ゲーム数）
                    if (IS_HALL())
                    {
                        setCurrentDataPanel(hallData[Defines.DEF_H_BNS_0]);
                    }
                    else
                    {
                        setCurrentDataPanel(int_s_value[Defines.DEF_INT_UNTIL_BONUS_GAMES]);
                    }

                    // キーリジェクトの値。
                    int_s_value[Defines.DEF_INT_KEY_REJECT] = 5;

                    // 停止フラグ
                    int_s_value[Defines.DEF_INT_REEL_STOP_R0] = ANGLE_UNDEF;
                    int_s_value[Defines.DEF_INT_REEL_STOP_R1] = ANGLE_UNDEF;
                    int_s_value[Defines.DEF_INT_REEL_STOP_R2] = ANGLE_UNDEF;
                    int_s_value[Defines.DEF_INT_REEL_ANGLE_R0] -= int_s_value[Defines.DEF_INT_REEL_SPEED] * 2 / 4;
                    int_s_value[Defines.DEF_INT_REEL_ANGLE_R1] -= int_s_value[Defines.DEF_INT_REEL_SPEED] * 2 / 4;
                    int_s_value[Defines.DEF_INT_REEL_ANGLE_R2] -= int_s_value[Defines.DEF_INT_REEL_SPEED] * 2 / 4;

                    GameManager.OnStartPlay();

                    break;

                case Defines.DEF_RMODE_FLASH: // (モード初期)
                    break;

                case Defines.DEF_RMODE_RESULT: // (モード初期)
                    // RESULTに入った時間を記録
                    _lampTime = Util.GetMilliSeconds() + Defines.DEF_WAIT_LAMP;

                    // 払い出しコイン枚数
                    int_s_value[Defines.DEF_INT_WIN_COIN_NUM] = v23.mPayMedal();

                    int_s_value[Defines.DEF_INT_TOTAL_PAY] += int_s_value[Defines.DEF_INT_WIN_COIN_NUM];
                    /**/
                    hallData[Defines.DEF_H_MEDAL_OUT] += int_s_value[Defines.DEF_INT_WIN_COIN_NUM];


                    if ((v23.getWork(Defines.DEF_HITFLAG) & Defines.DEF_HITFLAG_NR_RB) != 0)
                    {
                        Defines.TRACE("RB入賞時");
                        bigBonusFg = false;
                    }
                    else if ((v23.getWork(Defines.DEF_HITFLAG) & Defines.DEF_HITFLAG_NR_BB) != 0)
                    {
                        Defines.TRACE("BB入賞時");
                        bigBonusFg = true;
                    }
                    if (!IS_BONUS())
                    {
                        if ((v23.getWork(Defines.DEF_HITFLAG) & 0x01) != 0)
                        {
                            int_s_value[Defines.DEF_INT_CHRY_GOT]++;
                        }
                        else if ((v23.getWork(Defines.DEF_HITFLAG) & 0x04) != 0)
                        {
                            int_s_value[Defines.DEF_INT_WMLN_GOT]++;
                        }
                    }
                    else if (IS_BONUS_GAME())
                    {
                        // ビタ外し成功
                        if (v23.getWork(Defines.DEF_HITFLAG) == 0
                                && ((v23.getWork(Defines.DEF_WAVEBIT) & 0x08) != 0)
                                && (v23.getWork(Defines.DEF_ARAY11) == Defines.DEF_BAR_))
                        {
                            int_s_value[Defines.DEF_INT_HAZUSI_COUNT]++;
                        }
                    }

                    int_s_value[Defines.DEF_INT_BONUS_GOT] += int_s_value[Defines.DEF_INT_WIN_COIN_NUM];

                    if ((IS_BONUS_JAC() == true))
                    {
                        int_s_value[Defines.DEF_INT_BONUS_JAC_GOT] += int_s_value[Defines.DEF_INT_WIN_COIN_NUM];
                    }

                    // 払い出し音の発生(獲得枚数がなくても鳴らさなくてはならないので、ここで呼ぶ)
                    playCoinSound();
                    break;
                // TOBE [=6.RMODE_BB_FANFARE init]
                case Defines.DEF_RMODE_BB_FANFARE: // (モード初期)
                    int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] = 0;
                    int_s_value[Defines.DEF_INT_4TH_ACTION_FLAG] = 0;
                    lampSwitch(Defines.DEF_LAMP_4TH, Defines.DEF_LAMP_ACTION_ON);
                    // BBﾌｧﾝﾌｧｰﾚ鳴らすﾓｰﾄﾞ(→Defines.DEF_RMODE_BB_FANFARE_VOICE)
                    if (int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_R7)
                    {
                        _soundTime = Util.GetMilliSeconds() + Defines.DEF_SOUND_MS_03; // ﾌｧﾝﾌｧｰﾚ完奏時間設定
                        playBGM(Defines.DEF_SOUND_03, false); // BBﾌｧﾝﾌｧｰﾚ1(ﾄﾞﾝﾁｬﾝ揃い)
                    }
                    else if (int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_B7)
                    {
                        _soundTime = Util.GetMilliSeconds() + Defines.DEF_SOUND_MS_02; // ﾌｧﾝﾌｧｰﾚ完奏時間設定
                        playBGM(Defines.DEF_SOUND_02, false); // BBﾌｧﾝﾌｧｰﾚ2(7揃い)
                    }
                    set4th(29);
                    break;
                // TOBE [=6.RMODE_BB_FANFARE init]
                case Defines.DEF_RMODE_RB_FANFARE: // (モード初期)
                    int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] = 0;
                    int_s_value[Defines.DEF_INT_4TH_ACTION_FLAG] = 0;
                    lampSwitch(Defines.DEF_LAMP_4TH, Defines.DEF_LAMP_ACTION_ON);
                    // RBﾌｧﾝﾌｧｰﾚ鳴らすﾓｰﾄﾞ(→Defines.DEF_RMODE_BB_FANFARE_VOICE)
                    _soundTime = Util.GetMilliSeconds() + Defines.DEF_SOUND_MS_04; // ﾌｧﾝﾌｧｰﾚ完奏時間設定
                    playBGM(Defines.DEF_SOUND_04, false);
                    set4th(29);
                    break;
                // TOBE [=5.RMODE_FIN_WAIT init]
                case Defines.DEF_RMODE_FIN_WAIT: // (モード初期)
                    int_s_value[Defines.DEF_INT_KEY_REJECT] = 0;
                    // 毎ゲーム終了時にここを通る
#if __BONUS_CUT__
                    //				DfMain.TRACE(("毎ゲーム終了");
                    // ボーナス制御
                    ushort bonusEndFg;

                    //				DfMain.TRACE(("-ST------------------------------------------------------------");
                    //				DfMain.TRACE(("ボーナス制御:" + int_s_value[Defines.DEF_INT_BONUS_GOT]);
                    //				DfMain.TRACE(("遊技ｽﾃｰﾀｽ(Defines.DEF_GMLVSTS):" + (clOHHB_V23.getWork(Defines.DEF_GMLVSTS) & 0xFFFF) );
                    //				DfMain.TRACE(("BB子役(Defines.DEF_BBGMCTR):" + (clOHHB_V23.getWork(Defines.DEF_BBGMCTR)&0xFFFF));
                    //				DfMain.TRACE(("残りJACIN(Defines.DEF_BIGBCTR):" + (clOHHB_V23.getWork(Defines.DEF_BIGBCTR)&0xFFFF));
                    //				DfMain.TRACE(("JAC入賞回数(Defines.DEF_JAC_CTR):" + (clOHHB_V23.getWork(Defines.DEF_JAC_CTR)&0xFFFF));
                    //				DfMain.TRACE(("JAC遊技回数(Defines.DEF_JACGAME):" + (clOHHB_V23.getWork(Defines.DEF_JACGAME)&0xFFFF));
                    //				DfMain.TRACE(("-end-----------------------------------------------------------");

                    bonusEndFg = v23.mBonusCounter();

                    //				DfMain.TRACE(("-ST------------------------------------------------------------");
                    //				DfMain.TRACE(("ボーナス制御2:" + int_s_value[Defines.DEF_INT_BONUS_GOT]);
                    //				DfMain.TRACE(("遊技ｽﾃｰﾀｽ(Defines.DEF_GMLVSTS):" + (clOHHB_V23.getWork(Defines.DEF_GMLVSTS) & 0xFFFF) );
                    //				DfMain.TRACE(("BB子役(Defines.DEF_BBGMCTR):" + (clOHHB_V23.getWork(Defines.DEF_BBGMCTR)&0xFFFF));
                    //				DfMain.TRACE(("残りJACIN(Defines.DEF_BIGBCTR):" + (clOHHB_V23.getWork(Defines.DEF_BIGBCTR)&0xFFFF));
                    //				DfMain.TRACE(("JAC入賞回数(Defines.DEF_JAC_CTR):" + (clOHHB_V23.getWork(Defines.DEF_JAC_CTR)&0xFFFF));
                    //				DfMain.TRACE(("JAC遊技回数(Defines.DEF_JACGAME):" + (clOHHB_V23.getWork(Defines.DEF_JACGAME)&0xFFFF));
                    //				DfMain.TRACE(("bonusEndFg 1:" +(bonusEndFg&0xFFFF));
                    //				DfMain.TRACE(("-end-----------------------------------------------------------");


                    //Defines.DEF_HITFLAG
                    //if(bonusEndFg != Defines.DEF_BBEND_FLX)
                    if (IS_BONUS() == true)
                    {	// ボーナス終了フラグじゃないとき
                        if (cutBonusSystem(0))
                        {	// ﾎﾞｰﾅｽｶｯﾄ処理が必要の場合

                            // カット処理フラグON
                            BonusCutFg = true;

                            if ((int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_B7)
                                || (int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_R7))
                            { // ＢＢ終了判定(RBの場合はRB終了時にメダル加算を行なう)
                                if ((IS_BONUS_JAC() == true))
                                {
                                    int num;
                                    num = 0;
                                    // JACゲームの獲得枚数
                                    if (int_s_value[Defines.DEF_INT_BONUS_JAC_GOT] < Defines.JAC_BONUS_AVENUM)
                                    {	// JACゲームのカット枚数を加算する
                                        num = (Defines.JAC_BONUS_AVENUM - int_s_value[Defines.DEF_INT_BONUS_JAC_GOT]);
                                        int_s_value[Defines.DEF_INT_BONUS_JAC_GOT] = 0;	// JAC中の獲得枚数をクリア
                                        int_s_value[Defines.DEF_INT_BONUS_GOT] += num;
                                    }
                                    Defines.TRACE("JACカット分を追加:" + num);
                                    GPW_chgCredit(num);

                                    BonusCutFg = false;	// JACのカットはここまでなので
                                }
                            }
                            //						DfMain.TRACE(("-ST------------------------------------------------------------");
                            //						DfMain.TRACE(("ボーナスカット前:" + int_s_value[Defines.DEF_INT_BONUS_GOT]);
                            //						DfMain.TRACE(("遊技ｽﾃｰﾀｽ(Defines.DEF_GMLVSTS):" + (clOHHB_V23.getWork(Defines.DEF_GMLVSTS) & 0xFFFF) );
                            //						DfMain.TRACE(("BB子役(Defines.DEF_BBGMCTR):" + (clOHHB_V23.getWork(Defines.DEF_BBGMCTR)&0xFFFF));
                            //						DfMain.TRACE(("残りJACIN(Defines.DEF_BIGBCTR):" + (clOHHB_V23.getWork(Defines.DEF_BIGBCTR)&0xFFFF));
                            //						DfMain.TRACE(("JAC入賞回数(Defines.DEF_JAC_CTR):" + (clOHHB_V23.getWork(Defines.DEF_JAC_CTR)&0xFFFF));
                            //						DfMain.TRACE(("JAC遊技回数(Defines.DEF_JACGAME):" + (clOHHB_V23.getWork(Defines.DEF_JACGAME)&0xFFFF));
                            //						DfMain.TRACE(("-end-----------------------------------------------------------");

                            bonusEndFg = v23.mBonusCounter();

                            //						DfMain.TRACE(("-ST------------------------------------------------------------");
                            //						DfMain.TRACE(("ボーナスカット後:" + int_s_value[Defines.DEF_INT_BONUS_GOT]);
                            //						DfMain.TRACE(("遊技ｽﾃｰﾀｽ(Defines.DEF_GMLVSTS):" + (clOHHB_V23.getWork(Defines.DEF_GMLVSTS) & 0xFFFF) );
                            //						DfMain.TRACE(("BB子役(Defines.DEF_BBGMCTR):" + (clOHHB_V23.getWork(Defines.DEF_BBGMCTR)&0xFFFF));
                            //						DfMain.TRACE(("残りJACIN(Defines.DEF_BIGBCTR):" + (clOHHB_V23.getWork(Defines.DEF_BIGBCTR)&0xFFFF));
                            //						DfMain.TRACE(("JAC入賞回数(Defines.DEF_JAC_CTR):" + (clOHHB_V23.getWork(Defines.DEF_JAC_CTR)&0xFFFF));
                            //						DfMain.TRACE(("JAC遊技回数(Defines.DEF_JACGAME):" + (clOHHB_V23.getWork(Defines.DEF_JACGAME)&0xFFFF));
                            //						DfMain.TRACE(("bonusEndFg 2:" +(bonusEndFg&0xFFFF));
                            //						DfMain.TRACE(("-end-----------------------------------------------------------");
                        }
                    }

                    if (bonusEndFg != 0)
                    {
#else
#endif

                        BonusEnd(0);
                    }
                    break; // 抜ける
                // TOBE [=RMODE_NO_COIN init}
                case Defines.DEF_RMODE_NO_COIN: // (モード初期)
                    return true; // コインなしで終了通知
            } // E-O-各モードでの初期化
            break;
        } // end of while

        if (int_s_value[Defines.DEF_INT_PREV_GAMEST] != v23.getWork(Defines.DEF_GAMEST))
        {
            int_s_value[Defines.DEF_INT_PREV_GAMEST] = v23.getWork(Defines.DEF_GAMEST);
            if ((int_s_value[Defines.DEF_INT_PREV_GAMEST] & 0x01) != 0)
            {
                // TODO JACBGMを鳴らす
                playBGM(Defines.DEF_SOUND_05, true);
            }
            else if ((int_s_value[Defines.DEF_INT_PREV_GAMEST] & 0x80) != 0)
            {
                // TODO BIGBGMを鳴らす
                if (int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_R7)
                {
                    playBGM(Defines.DEF_SOUND_07, true);
                }
                else if (int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_B7)
                {
                    playBGM(Defines.DEF_SOUND_06, true);
                }
            }
        }

        int_s_value[Defines.DEF_INT_MODE_COUNTER]++; // モードが切り替わってからの累積カウンタ

        ctrlTopLamp();

        int_s_value[Defines.DEF_INT_ON_OFF_EFFECT] = Util.GetMilliSeconds() % 1000 > 500 ? 1 : 0;

        ctrlLamp();
        ctrlBetLamp();

        DoModeAction(keyTrigger);
        return false;
    } // process()

    private void DoModeAction(int keyTrigger)
    {
        // ======================================
        // 各モードにおける毎回の処理
        // ======================================
        //DfMain.TRACE(("リプレイある(毎回)？" + IS_REPLAY());
        //DfMain.TRACE(("Defines.DEF_INT_CURRENT_MODE:" + int_s_value[Defines.DEF_INT_CURRENT_MODE] + ":" + (clOHHB_V23.getWork(Defines.DEF_GAMEST)&0xFFFF));
        switch (int_s_value[Defines.DEF_INT_CURRENT_MODE])
        {
            // TOBE [=0.RMODE_WAIT rp]
            case Defines.DEF_RMODE_WAIT: // （毎回処理）
                // 直前の停止音の完奏を待つ
                if (_soundTime < Util.GetMilliSeconds())
                {
                    // ﾘﾌﾟﾚｲが揃っているときは、自動的にRMODE_BETまで遷移する
                    if (bgm_resumeFg == true)
                    {	// 休憩中からの復帰
                        if (bgm_no != -1)
                        {
                            // サウンド
                            //if (!IS_BONUS()) {
                            //	// ボーナス消化中以外は毎回停止指示。
                            //	mobile.stopSound(Defines.DEF_SOUND_UNDEF);
                            //}
                            if (IS_BONUS())
                            {	// ボーナス時限定
                                Defines.TRACE("復帰サウンドの再生");
                                playBGM(bgm_no, bgm_loop); // 復帰サウンドの再生
                                bgm_resumeFg = false;
                            }
                            else
                            {
                                // ボーナス消化中以外は毎回停止指示。
                                mobile.stopSound(Defines.DEF_SOUND_UNDEF);
                            }
                        }
                    }

                    slotInterface.betFlag = false;
                    if (IS_REPLAY())
                    {
                        slotInterface.betFlag = true;
                        REQ_MODE(Defines.DEF_RMODE_BET); // MAXBETへ遷移
                    }
                    else
                    {
                        // BET開始
                        if ((keyTrigger & (Defines.DEF_KEY_BIT_SELECT | Defines.DEF_KEY_BIT_5)) != 0)
                        {
                            if (IS_BONUS_JAC())
                            {
                                int_s_value[Defines.DEF_INT_BET_COUNT] = 1;
                            }
                            else
                            {
                                int_s_value[Defines.DEF_INT_BET_COUNT] = 3;
                            }

                            //DfMain.TRACE(("枚数チェック:" + int_s_value[Defines.DEF_INT_SLOT_COIN_NUM]+ ":" + int_s_value[Defines.DEF_INT_BETTED_COUNT] + ":" + int_s_value[Defines.DEF_INT_BET_COUNT]);
                            if (int_s_value[Defines.DEF_INT_SLOT_COIN_NUM] < int_s_value[Defines.DEF_INT_BET_COUNT])
                            {	// コインがないからBETさせない
                                slotInterface.onCreditZero();
                            }
                            else
                            {
                                REQ_MODE(Defines.DEF_RMODE_BET);
                                slotInterface.betFlag = true;
                            }
                        }
                    }
                }
                break;
            // TOBE [=1.RMODE_BET rp]
            case Defines.DEF_RMODE_BET: // MAXBETﾗﾝﾌﾟ表示期間（毎回処理）
                #region DEF_RMODE_BET
                //DfMain.TRACE(("ベット処理２");
                int betMax = Math.Min(3, int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM]
                        + int_s_value[Defines.DEF_INT_SLOT_COIN_NUM]
                        + int_s_value[Defines.DEF_INT_BETTED_COUNT]);
                // サウンドの終わりを待つ
                // 描画を増やす
                if (int_s_value[Defines.DEF_INT_BETTED_COUNT] < int_s_value[Defines.DEF_INT_BET_COUNT])
                {
                    // ココのタイミングの取り方は40msでループするのが前提
                    if (int_s_value[Defines.DEF_INT_MODE_COUNTER] % 2 == 0)
                    {
                        if (!IS_REPLAY())
                        {
                            // クレジットから減らす
                            if (int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM] > 0)
                            {
                                int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM]--;
                            }
                            int_s_value[Defines.DEF_INT_SLOT_COIN_NUM]--;

                            // BETした分だけ減算する
                            GPW_chgCredit(-1);

                            // コイン投入時処理
                            GameManager.OnCoinInsert();
                        }
                        int_s_value[Defines.DEF_INT_BONUS_GOT]--;
                        int_s_value[Defines.DEF_INT_BETTED_COUNT]++;


                        //GPW_chgCredit(0 - int_s_value[Defines.DEF_INT_BETTED_COUNT]);
                    }
                }
                // 減らす
                else if (int_s_value[Defines.DEF_INT_BETTED_COUNT] > int_s_value[Defines.DEF_INT_BET_COUNT])
                {
                    // ココのタイミングの取り方は40msでループするのが前提
                    if (int_s_value[Defines.DEF_INT_MODE_COUNTER] % 2 == 0)
                    {
                        if (!IS_REPLAY())
                        {
                            if (int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM] < Defines.DEF_NUM_MAX_CREDIT)
                            {
                                int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM]++;
                            }
                            int_s_value[Defines.DEF_INT_SLOT_COIN_NUM]++;
                        }
                        int_s_value[Defines.DEF_INT_BONUS_GOT]++;
                        int_s_value[Defines.DEF_INT_BETTED_COUNT]--;
                    }
                }
                else
                {
                    if (_soundTime < Util.GetMilliSeconds())
                    {
                        // 回転開始
                        //DfMain.TRACE(("回転開始チェック:" + int_s_value[Defines.DEF_INT_BETTED_COUNT]);
                        if ((int_s_value[Defines.DEF_INT_BETTED_COUNT] > 0)
                            && (((keyTrigger & (Defines.DEF_KEY_BIT_SELECT | Defines.DEF_KEY_BIT_5)) != 0)
                            || (reelStartFg == true)))
                        {
                            //DfMain.TRACE(("回転開始ウェイト");
                            reelStartFg = true;
                            lampSwitch(Defines.DEF_LAMP_LEVER, Defines.DEF_LAMP_ACTION_ON);
                            if (mobile.isJacCut() == true)
                            {	// ボーナスカットの場合
                                reelwait = -3200;
                            }
                            if ((reelwait + 3200) < Util.GetMilliSeconds())
                            {	// リールウェイト
                                //DfMain.TRACE(("回転開始");
                                REQ_MODE(Defines.DEF_RMODE_SPIN);
                                //DfMain.TRACE(("リールウェイト:" + (Util.GetMilliSeconds() - reelwait));
                                reelwait = Util.GetMilliSeconds();//リール全体用
                                reelStartFg = false;
                            }

                        }
                    }
                }
                break;
                #endregion
            // TOBE [=2.RMODE_SPIN rp]
            case Defines.DEF_RMODE_SPIN: // 回転中（毎回処理）
                // 4thをまわす。
                action4th();
                // 全部止まったらモード変わる
                if (int_s_value[Defines.DEF_INT_IS_REEL_STOPPED] == 7)
                {
                    // 停止音 完奏を待って次のモードへ遷移
                    if (_soundTime < Util.GetMilliSeconds())
                    {
                        // 4thが止まったら
                        if (int_s_value[Defines.DEF_INT_4TH_ACTION_FLAG] == 0)
                        {
                            if (isPlay())
                            {
                                REQ_MODE(Defines.DEF_RMODE_FLASH);
                            }
                            else
                            {
                                REQ_MODE(Defines.DEF_RMODE_WIN);
                            }
                        }
                    }
                    break;
                }

                // 前に鳴らしたサウンド待ち
                if (_soundTime < Util.GetMilliSeconds() ||
                     int_s_value[Defines.DEF_INT_IS_REEL_STOPPED] != 0)
                {
                    if (int_s_value[Defines.DEF_INT_KEY_REJECT] > 0)
                    {
                        // 一定ターン待つ
                        int_s_value[Defines.DEF_INT_KEY_REJECT]--;
                    }
                    else
                    {
                        // 同時押しは出来ない ワンボタン(KEY_5)操作あり
                        bool isSpinning = true;
                        bool isLimitStop = false; // 自動停止の場合用
                        //DfMain.TRACE(("ここから開始");

                        if (_spinTime < Util.GetMilliSeconds())
                        {
                            Defines.TRACE(Util.GetMilliSeconds() - _spinTime);
                            isLimitStop = true;
                        }
                        // 自動停止もあり
                        if ((keyTrigger & (Defines.DEF_KEY_BIT_SELECT | Defines.DEF_KEY_BIT_5)) != 0
                            || (isLimitStop == true))
                        {
                            int tmp;
                            for (int i = 0; i < Defines.DEF_N_REELS; i++)
                            {
                                if (isSpinning)
                                {
                                    // 押し順の変更
                                    tmp = getStopReel(i, isLimitStop);
                                    isSpinning = setReelStopAngle(tmp);
                                    //DfMain.TRACE(("停止フラグ:" + isSpinning);
                                }
                            }
                        }
                        else
                        {
                            if (isSpinning
                                    && (keyTrigger & (Defines.DEF_KEY_BIT_1)) != 0)
                            {
                                isSpinning = setReelStopAngle(0);
                            }
                            if (isSpinning
                                    && (keyTrigger & (Defines.DEF_KEY_BIT_2)) != 0)
                            {
                                isSpinning = setReelStopAngle(1);
                            }
                            if (isSpinning
                                    && (keyTrigger & (Defines.DEF_KEY_BIT_3)) != 0)
                            {
                                isSpinning = setReelStopAngle(2);
                            }
                        }
                    }
                }

                // 停止ボタンを点灯
                ctrlButtonLamp();

                // リールを進める。
                for (int i = 0; i < 3; i++)
                {
                    // 止まっていたら次
                    if ((int_s_value[Defines.DEF_INT_IS_REEL_STOPPED] & BIT(i)) != 0)
                        continue;

                    if (int_s_value[Defines.DEF_INT_REEL_STOP_R0 + i] != ANGLE_UNDEF
                            && (((int_s_value[Defines.DEF_INT_REEL_STOP_R0 + i] - int_s_value[Defines.DEF_INT_REEL_ANGLE_R0 + i])
                                & ANGLE_2PI_MASK) <= int_s_value[Defines.DEF_INT_REEL_SPEED] ||
                                (mobile.isJacCut() == true)))
                    {
                        // 止めにかかる
                        int_s_value[Defines.DEF_INT_REEL_ANGLE_R0 + i] = int_s_value[Defines.DEF_INT_REEL_STOP_R0 + i];
                        // 止まった
                        int_s_value[Defines.DEF_INT_IS_REEL_STOPPED] |= BIT(i);

                        int_s_value[Defines.DEF_INT_KEY_REJECT] = 1;
                        // 次のボタンが押せるようにする
                        int stop_snd_id = Defines.DEF_SOUND_23;

                        _soundTime = Util.GetMilliSeconds() + Defines.DEF_SOUND_MS_23 / 2;
                        if (!IS_BONUS())
                        {
                            int_s_value[Defines.DEF_INT_IS_TEMPAI] = 0;
                            int[] tempai = isTempai();
                            switch (v23.getWork(Defines.DEF_PUSHCTR))
                            {
                                case 0x02:// 第1停止
                                    if (i == 0)
                                    {	// 停止リールが左リールの時
                                        GPW_eventProcess((int)Defines.EVENT_PROC.EVENT_PROC_CHK_REEL, (int)Defines.EVENT.EVENT_NO1);
                                    }
                                    break;
                                case 0x01:// 第2停止
                                    if (tempai[1] == 3)
                                    {
                                        //ﾄﾘﾌﾟﾙﾃﾝﾊﾟｲ音
                                        // (トリプルテンパイ（BIG確）)
                                        GPW_eventProcess((int)Defines.EVENT_PROC.EVENT_PROC_CHK_REEL, (int)Defines.EVENT.EVENT_NO2);
                                        stop_snd_id = Defines.DEF_SOUND_15;
                                        _soundTime = Util.GetMilliSeconds() + Defines.DEF_SOUND_MS_15;
                                        int_s_value[Defines.DEF_INT_IS_TEMPAI] = 1;
                                    }
                                    else if (tempai[0] != Defines.DEF_BB_UNDEF)
                                    {
                                        stop_snd_id = Defines.DEF_SOUND_14;
                                        _soundTime = Util.GetMilliSeconds() + Defines.DEF_SOUND_MS_14;
                                        int_s_value[Defines.DEF_INT_IS_TEMPAI] = 1;
                                    }
                                    break;
                                case 0x00:// 第三停止
                                    if (tempai[0] == Defines.DEF_BB_B7)
                                    {
                                        int_s_value[Defines.DEF_INT_BB_KIND] = Defines.DEF_BB_B7;
                                        //TOBE 個別PARAM ＢＢ揃ったかの判定									
                                        if (int_s_value[Defines.DEF_INT_BB_AFTER_1GAME] > 0)
                                        {
                                            int_s_value[Defines.DEF_INT_BB_END_1GAME_REGET_BB] = 1; // 揃えた
                                            if (Defines.DEF_IS_DEBUG_MISSION_PARAM) { Defines.TRACE("1ゲーム目で青七をそろえた"); }
                                        }
                                        slotInterface.onBonusBB();
                                    }
                                    else if (tempai[0] == Defines.DEF_BB_R7)
                                    {
                                        int_s_value[Defines.DEF_INT_BB_KIND] = Defines.DEF_BB_R7;

                                        //TOBE 個別PARAM ＢＢ揃ったかの判定									
                                        if (int_s_value[Defines.DEF_INT_BB_AFTER_1GAME] > 0)
                                        {
                                            int_s_value[Defines.DEF_INT_BB_END_1GAME_REGET_BB] = 1; // 揃えた									
                                            if (Defines.DEF_IS_DEBUG_MISSION_PARAM) { Defines.TRACE("1ゲーム目で赤ﾄﾞﾝをそろえた"); }
                                        }
                                        slotInterface.onBonusBB();
                                    }

                                    // ゲチェナ
                                    GPW_eventProcess((int)Defines.EVENT_PROC.EVENT_PROC_CHK_REEL, (int)Defines.EVENT.EVENT_NO3);
                                    int_s_value[Defines.DEF_INT_BB_AFTER_1GAME] = 0;// TOBE 個別PARAM用フラグで使うフラグを必ず下ろす
                                    break;
                            }
                        }

                        if ((mobile.isJacCut() == false))
                        {
                            playSE(stop_snd_id);
                        }

                    }
                    else
                    {
                        int_s_value[Defines.DEF_INT_REEL_ANGLE_R0 + i] =
                            (int_s_value[Defines.DEF_INT_REEL_ANGLE_R0 + i] + int_s_value[Defines.DEF_INT_REEL_SPEED]) & ANGLE_2PI_MASK;
                    }
                }

                break;
            // TOBE [=3.RMODE_FLASH rp]
            case Defines.DEF_RMODE_FLASH: // 結果（毎回処理）
                //スピード調整
                if (ZZ.getThreadSpeed() < 40
                        && int_s_value[Defines.DEF_INT_MODE_COUNTER] % 2 == 0)
                {
                    break;
                }
                if (isPlay())
                {
                    int_s_value[Defines.DEF_INT_FLASH_DATA] = getNext();
                    // リールフラッシュ以外の部分
                    if ((int_s_value[Defines.DEF_INT_FLASH_DATA] & (1 << 10)) != 0)
                    {
                        lampSwitch(Defines.DEF_LAMP_4TH, Defines.DEF_LAMP_ACTION_ON);
                    }
                    else
                    {
                        lampSwitch(Defines.DEF_LAMP_4TH, Defines.DEF_LAMP_ACTION_OFF);
                    }
                }
                else
                {
                    REQ_MODE(Defines.DEF_RMODE_WIN);
                }
                break;
            case Defines.DEF_RMODE_WIN:
                if (_soundTime < Util.GetMilliSeconds())
                {
                    REQ_MODE(Defines.DEF_RMODE_RESULT);
                }
                break;
            // TOBE [=4.RMODE_RESULT rp]
            case Defines.DEF_RMODE_RESULT: // 結果（毎回処理）
#if __COM_TYPE__
                if ((mobile.isJacCut() == true))
#else
//			if (mobile.isJacCut() && IS_BONUS_JAC())
#endif
                {
                    // 内部でカウント
                    int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM] += int_s_value[Defines.DEF_INT_WIN_COIN_NUM];
                    int_s_value[Defines.DEF_INT_SLOT_COIN_NUM] += int_s_value[Defines.DEF_INT_WIN_COIN_NUM];
                    // 表示用は個々で増やす
                    int_s_value[Defines.DEF_INT_WIN_GET_COIN] += int_s_value[Defines.DEF_INT_WIN_COIN_NUM];
                    /**/
                    hallData[Defines.DEF_H_PLAYER_COIN] += int_s_value[Defines.DEF_INT_WIN_COIN_NUM];


                    // ５０枚まではクレジットへ貯めるぅ
                    if (int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM] > Defines.DEF_NUM_MAX_CREDIT)
                    {
                        int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM] = Defines.DEF_NUM_MAX_CREDIT;
                    }
                    // ＭＡＸを超えないように。
                    if (int_s_value[Defines.DEF_INT_SLOT_COIN_NUM] > Defines.DEF_NUM_MAX_COIN)
                    {
                        int_s_value[Defines.DEF_INT_SLOT_COIN_NUM] = Defines.DEF_NUM_MAX_COIN;
                    }
                    _soundTime = 0;

                    // 払い出し分加算
#if __ERR_MSG__
				if( (int_s_value[Defines.DEF_INT_WIN_COIN_NUM] < 0) || (int_s_value[Defines.DEF_INT_WIN_COIN_NUM] > 800))
				{	// 0以下ならば
					SET_ERR_CODE(ERR_CODE_PAY_UP2);
					SET_ERR_OPTION(int_s_value[Defines.DEF_INT_WIN_COIN_NUM]);
				}
#endif
                    GPW_chgCredit(int_s_value[Defines.DEF_INT_WIN_COIN_NUM]);
                }
                else
                {
                    // 一枚一枚移す
                    // satoh#暫定
                    if (int_s_value[Defines.DEF_INT_WIN_GET_COIN] < int_s_value[Defines.DEF_INT_WIN_COIN_NUM])
                    {
                        if ((int_s_value[Defines.DEF_INT_MODE_COUNTER] % (Defines.DEF_WAIT_COUNT_UP / int_s_value[Defines.DEF_INT_LOOP_SPEED] + 1)) == 0)
                        {
                            //				if (int_s_value[Defines.DEF_INT_WIN_GET_COIN] < int_s_value[Defines.DEF_INT_WIN_COIN_NUM]) {
                            //					if ((int_s_value[Defines.DEF_INT_MODE_COUNTER] % (Defines.DEF_WAIT_COUNT_UP
                            //							/ 1 + 1 )) == 0) {

#if __ERR_MSG__
					if( (int_s_value[Defines.DEF_INT_WIN_COIN_NUM] < 0) || (int_s_value[Defines.DEF_INT_WIN_COIN_NUM] > 20))
					{	// 0以下ならば
						SET_ERR_CODE(ERR_CODE_PAY_UP);
						SET_ERR_OPTION(int_s_value[Defines.DEF_INT_WIN_COIN_NUM]);
					}
#endif
                            // 払い出し分加算
                            GPW_chgCredit(1);
                            // ５０枚まではクレジットへ貯めるぅ
                            if (int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM] < Defines.DEF_NUM_MAX_CREDIT)
                            {
                                int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM]++;
                            }
                            int_s_value[Defines.DEF_INT_SLOT_COIN_NUM]++;
                            /**/
                            hallData[Defines.DEF_H_PLAYER_COIN]++;
                            // 表示用は個々で増やす
                            int_s_value[Defines.DEF_INT_WIN_GET_COIN]++;
                            // ＭＡＸを超えないように。
                            if (int_s_value[Defines.DEF_INT_SLOT_COIN_NUM] > Defines.DEF_NUM_MAX_COIN)
                            {
                                int_s_value[Defines.DEF_INT_SLOT_COIN_NUM] = Defines.DEF_NUM_MAX_COIN;
                            }
                        }
                        break;
                    }
                }

                // 払い出し音を待つ
                if (_soundTime < Util.GetMilliSeconds()
                        || (int_s_value[Defines.DEF_INT_WIN_COIN_NUM] <= int_s_value[Defines.DEF_INT_WIN_GET_COIN] && !IS_REPLAY()))
                {
#if __COM_TYPE__
#else
//				// 後告知なら、ここで出現
//				if (mobile.getKokuchi() == Defines.DEF_SELECT_3_AFFTER) {
//					m_nYokoku2[1] = -1;
//					ForwordYokoku();
//				}
#endif
                    mobile.stopSound(Defines.DEF_SOUND_MULTI_SE);
                    _soundTime = Util.GetMilliSeconds();
                    // REG入賞
                    if ((v23.getWork(Defines.DEF_HITFLAG) & Defines.DEF_HITFLAG_NR_RB) != 0)
                    {
                        int_s_value[Defines.DEF_INT_BONUS_GOT] = 15;
                        int_s_value[Defines.DEF_INT_REG_COUNT]++;
                        int_s_value[Defines.DEF_INT_FLAG_GAME_COUNT] += int_s_value[Defines.DEF_INT_THIS_FLAG_GAME];
                        int_s_value[Defines.DEF_INT_THIS_FLAG_GAME] = 0;
                        hallData[Defines.DEF_H_RB_COUNT]++;/*HALL*/

                        // Jac-in 突入
                        int_s_value[Defines.DEF_INT_BB_KIND] = Defines.DEF_RB_IN;
                        REQ_MODE(Defines.DEF_RMODE_RB_FANFARE); // ＲＢファンファーレへ遷移

                        Defines.TRACE("REG入賞処理");
                        slotInterface.onBonusRB();
                        break;
                    }
                    // BIG入賞
                    else if ((v23.getWork(Defines.DEF_HITFLAG) & Defines.DEF_HITFLAG_NR_BB) != 0)
                    {
                        Defines.TRACE("BB入賞処理");
                        int_s_value[Defines.DEF_INT_BONUS_GOT] = 15;
                        int_s_value[Defines.DEF_INT_FLAG_GAME_COUNT] += int_s_value[Defines.DEF_INT_THIS_FLAG_GAME];
                        int_s_value[Defines.DEF_INT_THIS_FLAG_GAME] = 0;
                        int_s_value[Defines.DEF_INT_BIG_COUNT]++;
                        hallData[Defines.DEF_H_BB_COUNT]++;/*HALL*/
                        REQ_MODE(Defines.DEF_RMODE_BB_FANFARE); // ＢＢファンファーレへ遷移

                        // セルフオート停止フラグを立てる
                        GameManager.StopAutoPlay("BB入賞");
                        break;
                    }
                    REQ_MODE(Defines.DEF_RMODE_FIN_WAIT);
                    break;
                }
                break;
            // TOBE [=6.RMODE_BB_FANFARE rp]
            case Defines.DEF_RMODE_BB_FANFARE:
            case Defines.DEF_RMODE_RB_FANFARE:
                // if (int_s_value[Defines.DEF_INT_4TH_ACTION_FLAG] < 1) {
                // int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] += 20 + Defines.DEF_POS_4TH_TOTAL_W;
                // int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] %= Defines.DEF_POS_4TH_TOTAL_W;
                // }
                // if (int_s_value[Defines.DEF_INT_MODE_COUNTER] > 10) {
                // if (int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] < 20 + Defines.DEF_RP08) {
                // int_s_value[Defines.DEF_INT_4TH_ACTION_FLAG] = 1;
                // int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] = Defines.DEF_RP08;
                // }
                // }
                action4th();
                if (_soundTime < Util.GetMilliSeconds()
                        && int_s_value[Defines.DEF_INT_4TH_ACTION_FLAG] == 0)
                {
                    REQ_MODE(Defines.DEF_RMODE_FIN_WAIT);
                }
                break;
            // TOBE [=5.RMODE_FIN_WAIT rp]
            case Defines.DEF_RMODE_FIN_WAIT:
                if (int_s_value[Defines.DEF_INT_IS_BB_RB_END] > 0
                        && int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_RB_IN)
                {
                    int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] += Defines.DEF_POS_4TH_TOTAL_W - 20;
                    int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] %= Defines.DEF_POS_4TH_TOTAL_W;
                    if (Defines.DEF_RP19 - 20 < int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE]
                            && int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] <= Defines.DEF_RP19 + 20)
                    {
                        int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] = Defines.DEF_RP19;
                        //					REQ_MODE(Defines.DEF_RMODE_WAIT); // 回転待ちへ遷移
                    }
                    else
                    {
                        break;
                    }
                }
                // 直前の音の完奏を待つ
                if (_soundTime < Util.GetMilliSeconds())
                {
                    if (IS_HALL())
                    {
                        //ﾎﾞｰﾅｽ終了時通信
                        if (int_s_value[Defines.DEF_INT_IS_BB_RB_END] == 1)
                        {
                            hallData[Defines.DEF_H_APPLI_REQ] = Defines.DEF_HRQ_BNSEND;
                            REQ_MODE(Defines.DEF_RMODE_HTTP);
                            // REG入賞時通信
                        }
                        else if ((v23.getWork(Defines.DEF_HITFLAG) & Defines.DEF_HITFLAG_NR_RB) != 0)
                        {
                            hallData[Defines.DEF_H_APPLI_REQ] = Defines.DEF_HRQ_BNSIN;
                            REQ_MODE(Defines.DEF_RMODE_HTTP);
                            // BIG入賞時通信
                        }
                        else if ((v23.getWork(Defines.DEF_HITFLAG) & Defines.DEF_HITFLAG_NR_BB) != 0)
                        {
                            hallData[Defines.DEF_H_APPLI_REQ] = Defines.DEF_HRQ_BNSIN;
                            REQ_MODE(Defines.DEF_RMODE_HTTP);
                            //						//規定ゲーム通信
                            //						}else if(hallData[Defines.DEF_H_GAME_COUNT] - lastHttpGame >= Defines.DEF_HALL_GAME_SPAN){
                            //							hallData[Defines.DEF_H_APPLI_REQ] = Defines.DEF_HRQ_NORMAL;
                            //							REQ_MODE(Defines.DEF_RMODE_HTTP);
                            //10分経過後の最初のゲームで一応通信
                        }
                        else if (prevHttpTime + (5 * 60) < Util.GetMilliSeconds() / 1000)
                        {
                            hallData[Defines.DEF_H_APPLI_REQ] = Defines.DEF_HRQ_NORMAL;
                            REQ_MODE(Defines.DEF_RMODE_HTTP);
                        }
                        else
                        {
                            REQ_MODE(Defines.DEF_RMODE_WAIT); // 回転待ちへ遷移
                        }
                    }
                    else
                    {
                        REQ_MODE(Defines.DEF_RMODE_WAIT); // 回転待ちへ遷移
                    }
                }
                break;
        }
    }

    /**
     * クリップが使えるのでisRepaintを使用しない方向で
     * 
     */
    private void drawAll()
    {
        //DfMain.TRACE(("★★★★drawAll★★★★");
        // クリッピング領域の解除
        ZZ.setClip(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ.getHeight());
        // 画面を白く塗りつぶす
        ZZ.setColor(ZZ.getColor(0, 0, 0));
        ZZ.fillRect(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ.getHeight());
        // 筐体背景部分
        ZZ.drawImage(Defines.DEF_RES_K1, Defines.DEF_POS_K1_X, Defines.DEF_POS_K1_Y);
        ZZ.drawImage(Defines.DEF_RES_K2, Defines.DEF_POS_K2_X, Defines.DEF_POS_K2_Y);
        ZZ.drawImage(Defines.DEF_RES_K3, Defines.DEF_POS_K3_X, Defines.DEF_POS_K3_Y);
        ZZ.drawImage(Defines.DEF_RES_K4, Defines.DEF_POS_K4_X, Defines.DEF_POS_K4_Y);
        ZZ.drawImage(Defines.DEF_RES_K5, Defines.DEF_POS_K5_X, Defines.DEF_POS_K5_Y);
        ZZ.drawImage(Defines.DEF_RES_K6, Defines.DEF_POS_K6_X, Defines.DEF_POS_K6_Y);
        ZZ.drawImage(Defines.DEF_RES_K7, Defines.DEF_POS_K7_X, Defines.DEF_POS_K7_Y);

        // ランプ
        {	// drawK1 4thリールの左右のランプ
            int[] x = { Defines.DEF_POS_S1_X, Defines.DEF_POS_S2_X, Defines.DEF_POS_S3_X, Defines.DEF_POS_S4_X, Defines.DEF_POS_S5_X, Defines.DEF_POS_S6_X };
            int[] y = { Defines.DEF_POS_S1_Y, Defines.DEF_POS_S2_Y, Defines.DEF_POS_S3_Y, Defines.DEF_POS_S4_Y, Defines.DEF_POS_S5_Y, Defines.DEF_POS_S6_Y };
            // ランプの画像がなぜかでかすぎる為、いれとかないと枠外にでてしまう。
            ZZ.setClip(Defines.DEF_POS_K1_X, Defines.DEF_POS_K1_Y, Defines.DEF_POS_K1_W, Defines.DEF_POS_K1_H);
            for (int i = 0; i < 6; i++)
            {
                if (getLampStatus(Defines.DEF_LAMP_S1 + i) == Defines.DEF_LAMP_STATUS_ON)
                {
                    ZZ.drawImage(Defines.DEF_RES_S1_B + i, x[i], y[i]);
                }
            }
            // クリッピング領域の解除
            ZZ.setClip(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ.getHeight());
        }
        {	// drawK3 BETランプ
            int[] x = { Defines.DEF_POS_B1_X, Defines.DEF_POS_B2_X, Defines.DEF_POS_B3_X, Defines.DEF_POS_B4_X, Defines.DEF_POS_B5_X };
            int[] y = { Defines.DEF_POS_B1_Y, Defines.DEF_POS_B2_Y, Defines.DEF_POS_B3_Y, Defines.DEF_POS_B4_Y, Defines.DEF_POS_B5_Y };
            for (int i = 0; i < 5; i++)
            {
                if (getLampStatus(Defines.DEF_LAMP_BET_1 + i) == Defines.DEF_LAMP_STATUS_ON)
                {
                    ZZ.drawImage(Defines.DEF_RES_B1_B + i, x[i], y[i]);
                }
            }
        }

        {	// drawK4 筐体左のかぎやランプやリプレイランプ
            int[] x = { Defines.DEF_POS_C1_X, Defines.DEF_POS_C2_X, Defines.DEF_POS_C3_X, Defines.DEF_POS_C4_X, Defines.DEF_POS_C5_X };
            int[] y = { Defines.DEF_POS_C1_Y, Defines.DEF_POS_C2_Y, Defines.DEF_POS_C3_Y, Defines.DEF_POS_C4_Y, Defines.DEF_POS_C5_Y };
            for (int i = 0; i < 5; i++)
            {
                if (getLampStatus(Defines.DEF_LAMP_WIN + i) == Defines.DEF_LAMP_STATUS_ON)
                {
                    ZZ.drawImage(Defines.DEF_RES_C1_B + i, x[i], y[i]);
                }
            }
        }

        // 4th
        draw4th();
        // リール
        drawSlot();

        // drawK7関数を分解してみた
        ZZ.drawImage(Defines.DEF_RES_K7, Defines.DEF_POS_K7_X, Defines.DEF_POS_K7_Y);
        if (getLampStatus(Defines.DEF_LAMP_CHANCE) == Defines.DEF_LAMP_STATUS_ON)
        {
            ZZ.drawImage(Defines.DEF_RES_D1_B, Defines.DEF_POS_CHANCE_X, Defines.DEF_POS_CHANCE_Y);
        }
        // ボーナスかうんと描画
        drawBonusCount();
        // クレジット描画
        drawCredit();
        // 払い出し描画
        drawPay();

    }

    /**
     * 光どころ描画
     * 
     */
    private void drawK3()
    {
        ZZ.setClip(Defines.DEF_POS_K3_X, Defines.DEF_POS_K3_Y, Defines.DEF_POS_K3_W, Defines.DEF_POS_K3_H);
        int[] x = { Defines.DEF_POS_B1_X, Defines.DEF_POS_B2_X, Defines.DEF_POS_B3_X, Defines.DEF_POS_B4_X, Defines.DEF_POS_B5_X };
        int[] y = { Defines.DEF_POS_B1_Y, Defines.DEF_POS_B2_Y, Defines.DEF_POS_B3_Y, Defines.DEF_POS_B4_Y, Defines.DEF_POS_B5_Y };
        for (int i = 0; i < 5; i++)
        {
            if (getLampStatus(Defines.DEF_LAMP_BET_1 + i) == Defines.DEF_LAMP_STATUS_ON)
            {
                ZZ.drawImage(Defines.DEF_RES_B1_B + i, x[i], y[i]);
            }
        }
        // クリッピング領域の解除
        ZZ.setClip(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ.getHeight());
    }

    private void drawK1()
    {
        ZZ.setClip(Defines.DEF_POS_K1_X, Defines.DEF_POS_K1_Y, Defines.DEF_POS_K1_W, Defines.DEF_POS_K1_H);

        int[] x = { Defines.DEF_POS_S1_X, Defines.DEF_POS_S2_X, Defines.DEF_POS_S3_X, Defines.DEF_POS_S4_X, Defines.DEF_POS_S5_X, Defines.DEF_POS_S6_X };
        int[] y = { Defines.DEF_POS_S1_Y, Defines.DEF_POS_S2_Y, Defines.DEF_POS_S3_Y, Defines.DEF_POS_S4_Y, Defines.DEF_POS_S5_Y, Defines.DEF_POS_S6_Y };
        for (int i = 0; i < 6; i++)
        {
            if (getLampStatus(Defines.DEF_LAMP_S1 + i) == Defines.DEF_LAMP_STATUS_ON)
            {
                ZZ.drawImage(Defines.DEF_RES_S1_B + i, x[i], y[i]);
            }
        }
        // クリッピング領域の解除
        ZZ.setClip(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ.getHeight());
    }

    /**
     * 光どころ描画
     * 
     */
    private void drawK4()
    {
        ZZ.setClip(Defines.DEF_POS_K4_X, Defines.DEF_POS_K4_Y, Defines.DEF_POS_K4_W, Defines.DEF_POS_K4_H);
        int[] x = { Defines.DEF_POS_C1_X, Defines.DEF_POS_C2_X, Defines.DEF_POS_C3_X, Defines.DEF_POS_C4_X, Defines.DEF_POS_C5_X };
        int[] y = { Defines.DEF_POS_C1_Y, Defines.DEF_POS_C2_Y, Defines.DEF_POS_C3_Y, Defines.DEF_POS_C4_Y, Defines.DEF_POS_C5_Y };
        for (int i = 0; i < 5; i++)
        {
            if (getLampStatus(Defines.DEF_LAMP_WIN + i) == Defines.DEF_LAMP_STATUS_ON)
            {
                ZZ.drawImage(Defines.DEF_RES_C1_B + i, x[i], y[i]);
            }
        }
        // クリッピング領域の解除
        ZZ.setClip(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ.getHeight());
    }

    /**
     * 4thリールの描画
     * 
     */
    private void draw4th()
    {
        if (getLampStatus(Defines.DEF_LAMP_4TH) == Defines.DEF_LAMP_STATUS_ON)
        {
            GameManager.Set4thReelTexture(true);
        }
        else
        {
            GameManager.Set4thReelTexture(false);
        }
    }

    /**
     * スロット回転部の描画。
     */
    private void drawSlot()
    {
        // １リールずつ処理
        int[] x = { 25, 92, 159 };
        for (int i = 0; i < 3; i++)
        { // 左のリールから描画しています
            // リール部分クリッピング
            ZZ.setClip(25, 114 + Defines.GP_DRAW_OFFSET_Y, 215 - 25, 96);
            // 消灯
            int[] state = { 0, 0, 0, 0, 0 };// 消灯
            if (int_s_value[Defines.DEF_INT_CURRENT_MODE] == Defines.DEF_RMODE_FLASH
                    || (int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] == 2 && int_s_value[Defines.DEF_INT_CURRENT_MODE] == Defines.DEF_RMODE_WAIT))
            {
                // 点灯
                // 枠下&下段
                if ((int_s_value[Defines.DEF_INT_FLASH_DATA] & (1 << (i * 3 + 0))) != 0)
                {
                    state[0] = state[1] = 1;
                }
                // 中段
                if ((int_s_value[Defines.DEF_INT_FLASH_DATA] & (1 << (i * 3 + 1))) != 0)
                {
                    state[2] = 1;
                }
                // 上段&枠上
                if ((int_s_value[Defines.DEF_INT_FLASH_DATA] & (1 << (i * 3 + 2))) != 0)
                {
                    state[3] = state[4] = 1;
                }
            }
            else
            {
                state[0] = state[1] = state[2] = state[3] = state[4] = 1;
            }
            // ブラー
            if ((int_s_value[Defines.DEF_INT_IS_REEL_STOPPED] & BIT(i)) == 0)
            {
                // リールスピードが２以下の場合ブラー画像は使わない！
                if (mobile.getReelSpeed() >= 80)
                {
                    // ブラー
                    state[0] = state[1] = state[2] = state[3] = state[4] = 2;
                }
                ZZ.drawImage(Defines.DEF_RES_BACK_B, x[i], 114 + Defines.GP_DRAW_OFFSET_Y);
            }
            else
            {
                ZZ.drawImage(Defines.DEF_RES_BACK, x[i], 114 + Defines.GP_DRAW_OFFSET_Y);
            }

            // 例:1周21コマのうち15.75コマ回転していたら、per21=15.75<<16
            int per21 = Defines.DEF_N_FRAME
                    * (int_s_value[Defines.DEF_INT_REEL_ANGLE_R0 + i] & ANGLE_2PI_MASK);

            // 例:15.75コマ回転していたら15番を取得
            int mid = ((per21 >> 16) % Defines.DEF_N_FRAME); // 中段番号番号。
            // 各y座標に0.75コマ分下に
            int[] y = { 204, 176, 148, 120, 92 };
            // // リールの描画
            for (int j = 0; j < 5; j++)
            {// 0:枠下 1:下段 2:中段 3:上段 4:枠上
                int h = 28;
                y[j] += (/* １コマに満たない分 */(per21 & ANGLE_2PI_MASK) * h) >> 16;

                int rlnum = (mid + j - 2 + Defines.DEF_N_FRAME) % Defines.DEF_N_FRAME;// リール位置番号
                int sym = getReelId(REELTB[i][rlnum]);// 絵柄ID
                int id = 0;

                // soy TODO リールランプ処理
                GameManager.SetReelTexture(rlnum, i, state[j] == 1);

                id = Defines.DEF_RES_R1_01 + (Defines.DEF_RES_R1_02 - Defines.DEF_RES_R1_01) * sym + state[j];
                ZZ.drawImage(id, x[i], y[j] + Defines.GP_DRAW_OFFSET_Y);

                id = Defines.DEF_RES_R2_01 + (Defines.DEF_RES_R2_02 - Defines.DEF_RES_R2_01) * sym + state[j];
                ZZ.drawImage(id, x[i], y[j] + Defines.GP_DRAW_OFFSET_Y);

                id = Defines.DEF_RES_R3_01 + (Defines.DEF_RES_R3_02 - Defines.DEF_RES_R3_01) * sym + state[j];
                ZZ.drawImage(id, x[i], y[j] + Defines.GP_DRAW_OFFSET_Y);
            }
        }

        ZZ.setClip(25, 114 + Defines.GP_DRAW_OFFSET_Y, 215 - 25, 96);
        ZZ.scale3D(100);// スケール弄るよ
        int[] xx = { 25, 92, 159 };
        for (int i = 0; i < 3; i++)
        {
            // 上の左影
            _drawEffect(xx[i], 114, 56, 2, 61, 61, 61, Defines.DEF_INK_SUB);
            _drawEffect(xx[i], 116, 56, 4, 36, 36, 36, Defines.DEF_INK_SUB);
            _drawEffect(xx[i], 120, 56, 3, 18, 18, 18, Defines.DEF_INK_SUB);
            // 下の左影
            _drawEffect(xx[i], 208, 56, 2, 61, 61, 61, Defines.DEF_INK_SUB);
            _drawEffect(xx[i], 204, 56, 4, 36, 36, 36, Defines.DEF_INK_SUB);
            _drawEffect(xx[i], 201, 56, 3, 18, 18, 18, Defines.DEF_INK_SUB);
        }
        ZZ.scale3D(50);// 戻すよ～

        // クリッピング領域の解除
        ZZ.setClip(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ.getHeight());

    } // End of drawSlot()

    /**
     * TODO クレジット描画
     * 
     */
    private void drawCredit()
    {
        int xx = Defines.DEF_POS_CREDIT_X;
        int val = int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM];

        for (int i = 0; i < Defines.DEF_POS_CREDIT_D; i++)
        {
            if (val > 0)
            {
                ZZ.drawImage(Defines.DEF_RES_SEG_R0 + (val % 10), xx, Defines.DEF_POS_CREDIT_Y);
            }
            else
            {
                if (i == 0)
                {
                    ZZ.drawImage(Defines.DEF_RES_SEG_R0, xx, Defines.DEF_POS_CREDIT_Y);
                }
            }
            val /= 10;
            xx -= Defines.DEF_POS_CREDIT_W;
        }
    }

    /**
     * TODO 払い出し描画
     * 
     */
    private void drawPay()
    {
        // ZZ.setClip(Defines.DEF_POS_PAY_X - Defines.DEF_POS_PAY_W * 2, Defines.DEF_POS_PAY_Y,
        // Defines.DEF_POS_PAY_W * 3, Defines.DEF_POS_PAY_H);
        // ZZ.drawImage(Defines.DEF_RES_K2, Defines.DEF_POS_K2_X, Defines.DEF_POS_K2_Y);
        int xx = Defines.DEF_POS_PAY_X;
        int val = int_s_value[Defines.DEF_INT_WIN_GET_COIN];
        for (int i = 0; i < Defines.DEF_POS_PAY_D; i++)
        {
            if (val > 0)
            {
                ZZ.drawImage(Defines.DEF_RES_SEG_R0 + (val % 10), xx, Defines.DEF_POS_PAY_Y);
            }
            val /= 10;
            xx -= Defines.DEF_POS_PAY_W;
        }
    }

    /**
     * TODO ボーナスかうんと描画
     * 
     */
    private void drawBonusCount()
    {
        // ZZ.setClip(Defines.DEF_POS_BONUS_X - Defines.DEF_POS_BONUS_W * 2, Defines.DEF_POS_BONUS_Y,
        // Defines.DEF_POS_BONUS_W * 3, Defines.DEF_POS_BONUS_H);
        // ZZ.drawImage(Defines.DEF_RES_K2, Defines.DEF_POS_K2_X, Defines.DEF_POS_K2_Y);
        // ボーナスカウンタ表示領域
        if (IS_BONUS_JAC())
        { // Jac 中
            int i = v23.getWork(Defines.DEF_BIGBCTR);
            i = (i == 0) ? 1 : i;
            // (JACイン中)残り回数(3～1)
            ZZ.drawImage(Defines.DEF_RES_SEG_G0 + i,
                    Defines.DEF_POS_BONUS_X - Defines.DEF_POS_BONUS_W * 2, Defines.DEF_POS_BONUS_Y);
            // -[ﾊｲﾌﾝ]
            ZZ.drawImage(Defines.DEF_RES_SEG_GB, Defines.DEF_POS_BONUS_X - Defines.DEF_POS_BONUS_W,
                    Defines.DEF_POS_BONUS_Y);
            // ボーナスカウント(JACイン中)を表示します(8～1)
            ZZ.drawImage(Defines.DEF_RES_SEG_G0 + v23.getWork(Defines.DEF_JAC_CTR),
                    Defines.DEF_POS_BONUS_X, Defines.DEF_POS_BONUS_Y);
        }
        else if (IS_BONUS_GAME())
        {
            // ＢＢ残り回数を表示
            int val = v23.getWork(Defines.DEF_BBGMCTR);
            int xx = Defines.DEF_POS_BONUS_X;
            for (int i = 0; i < 3; i++)
            {
                if (val > 0)
                {
                    ZZ
                            .drawImage(Defines.DEF_RES_SEG_G0 + (val % 10), xx,
                                    Defines.DEF_POS_BONUS_Y);
                }
                val /= 10;
                xx -= Defines.DEF_POS_BONUS_W;
            }
        }
    }

    /// <summary>
    /// ボーナスカウンタ表示取得
    /// </summary>
    /// <returns></returns>
    public string GetBonusCount()
    {
        if (IS_BONUS_JAC())
        { // Jac 中
            int i = v23.getWork(Defines.DEF_BIGBCTR);
            i = (i == 0) ? 1 : i;
            return i + "-" + v23.getWork(Defines.DEF_JAC_CTR);
        }
        else if (IS_BONUS_GAME())
        {
            // ＢＢ残り回数を表示
            return v23.getWork(Defines.DEF_BBGMCTR).ToString();
        }
        return "";
    }

    /**
     * ゼロ埋めせずに数字を描く。（データパネル専用）
     * @param val 値
     * @param xx  X座標（１桁目の左上）
     * @param yy  Y座標（左上）
     * @param max 表示値の最大値
     * @see #printNumber(int, int, int)
     */
    private void printPanelCounts(int val, int xx, int yy, int max, int col)
    {
        printPanelCounts(val, xx, yy, max, col, -1);
    }

    private void printPanelCounts(int val, int xx, int yy, int max, int col, int keta)
    {
        // 桁チェック
        if (val < 0)
        {
            val = 0;
        }
        if (val > max)
        {
            val = max;
        }

        do
        {
            // 一桁は必ず描く
            // 下（右）から描いて行く
            switch (col)
            {
                case 3:
                    ZZ.setColor(ZZ.getColor(0x00, 0x00, 0xff));
                    break;
                case 2:
                    ZZ.setColor(ZZ.getColor(0x00, 0xff, 0x00));
                    break;
                case 1:
                    ZZ.setColor(ZZ.getColor(0xff, 0x00, 0x00));
                    break;
                case 0:
                default:
                    ZZ.setColor(ZZ.getColor(0xff, 0xff, 0xff));
                    break;
            }
            ZZ.fillRect(xx, yy, Defines.DEF_POS_NAVI_BB_W, Defines.DEF_POS_NAVI_BB_H);
            ZZ.drawImage(Defines.DEF_RES_NA0 + (val % 10), xx, yy);
            val /= 10;
            xx -= Defines.DEF_POS_NAVI_BB_W;
            keta--;
        } while (val > 0 || keta > 0);
    }

    private void set4th(int id)
    {
        // 待ちから動作状態へ
        if (int_s_value[Defines.DEF_INT_4TH_ACTION_FLAG] == 0 && id > 0)
        {
            int_s_value[Defines.DEF_INT_RLPTNDT] = id - 1;
            int_s_value[Defines.DEF_INT_RLPTNDT_COUNTER] = 0;
            int_s_value[Defines.DEF_INT_RLPTNDT_FLAG] = 0;// 0:回転開始可1:回転中2:回転終了
            int_s_value[Defines.DEF_INT_4TH_ACTION_FLAG] = 1;// セット完了（動作待ち）
            isCanStop = false;
        }
    }

    bool isCanStop = false;//センサー通過フラグ

    private void action4th()
    {
        // 動作状態でなければ飛ばす
        if (int_s_value[Defines.DEF_INT_4TH_ACTION_FLAG] != 1)
        {
            return;
        }
        // 一時停止中は何もしない
        if (_4thTime > Util.GetMilliSeconds())
        {
            return;
        }
        // 読込
        int[] data = RLPTNDT[int_s_value[Defines.DEF_INT_RLPTNDT]];
        // 回転タイミングがまだの時
        if (v23.getWork(Defines.DEF_PUSHCTR) > data[int_s_value[Defines.DEF_INT_RLPTNDT_COUNTER]] % 8)
        {
            return;
        }
        // 回転パラ夢
        int dir = data[int_s_value[Defines.DEF_INT_RLPTNDT_COUNTER] + 1] / 4;
        int spe = data[int_s_value[Defines.DEF_INT_RLPTNDT_COUNTER] + 1] % 4;
        int pos = data[int_s_value[Defines.DEF_INT_RLPTNDT_COUNTER] + 2];
        dir = (dir == 0) ? -1 : 1;
        spe = ((ZZ.getThreadSpeed() >= 40) ? 20 : 10) / spe;
        bool snd4th = false;
        if (ZZ.getThreadSpeed() >= 40)
        {
            if (spe == 20)
            {
                snd4th = true;
            }
        }
        else
        {
            if (spe == 10)
            {
                snd4th = true;
            }
        }
        bool isNext = false;
        for (int i = 0; i < spe; i++)
        {
            // 回転停止タイミングか？
            if (data[int_s_value[Defines.DEF_INT_RLPTNDT_COUNTER]] / 8 == 0
                    || v23.getWork(Defines.DEF_PUSHCTR) == 0)
            {
                //センサーは通過しているか?
                if (isCanStop)
                {
                    //さらに少なくとも赤ドン←大当り分以上は回らないといけない。
                    if (int_s_value[Defines.DEF_INT_RLPTNDT_FLAG] > 100 / spe)
                    {
                        if (int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] == pos)
                        {
                            isNext = true;
                            break;
                        }
                    }
                }
            }

            // 回す
            int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] += dir + 414;
            int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] %= 414;
            //センサーチェック
            if (dir < 0)
            {//正回転のセンサー位置:はずれ～青ドンの間にある
                if (int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] == 270)
                {
                    isCanStop = true;
                }
            }
            else
            {//逆回転のセンサー位置:大当り～赤ドンの間にある
                if (int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] == 60)
                {
                    isCanStop = true;
                }
            }

        }
        // DfMain.TRACE((int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE]);
        if (isNext)
        {
            isCanStop = false;
            int_s_value[Defines.DEF_INT_TOP_LAMP] = 0;
            if (int_s_value[Defines.DEF_INT_RLPTNDT_COUNTER] + 3 < data.Length)
            {
                // 次のデータへ
                int_s_value[Defines.DEF_INT_RLPTNDT_COUNTER] += 3;
                int_s_value[Defines.DEF_INT_RLPTNDT_FLAG] = 0;
                _4thTime = Util.GetMilliSeconds() + 1000;
            }
            else
            {
                // 演出完了
                int_s_value[Defines.DEF_INT_4TH_ACTION_FLAG] = 0;
            }
            if (!IS_BONUS()
                    && int_s_value[Defines.DEF_INT_CURRENT_MODE] != Defines.DEF_RMODE_BB_FANFARE
                    && int_s_value[Defines.DEF_INT_CURRENT_MODE] != Defines.DEF_RMODE_RB_FANFARE)
            {
                ZZ.stopSound(Defines.DEF_SOUND_MULTI_BGM);
            }
            return;
        }

        lampSwitch(Defines.DEF_LAMP_4TH, Defines.DEF_LAMP_ACTION_ON);

        // 回転
        if (int_s_value[Defines.DEF_INT_RLPTNDT_FLAG] == 0 && !IS_BONUS()
                && int_s_value[Defines.DEF_INT_CURRENT_MODE] != Defines.DEF_RMODE_BB_FANFARE
                && int_s_value[Defines.DEF_INT_CURRENT_MODE] != Defines.DEF_RMODE_RB_FANFARE)
        {
            if (snd4th)
            {
                playBGM(Defines.DEF_SOUND_10, true);
            }
            else
            {
                playSE(Defines.DEF_SOUND_11);
            }
        }
        int_s_value[Defines.DEF_INT_RLPTNDT_FLAG]++;
        int_s_value[Defines.DEF_INT_TOP_LAMP] = (dir == -1) ? 4 : 5;
    }

    /**
     * 動く上部ランプ
     * 
     * 演出ＩＤ
     * 
     * @param isRepaint
     * @see #drawUpperLamp(int)
     */
    private void ctrlTopLamp()
    {
        if (IS_BONUS_JAC()
                || int_s_value[Defines.DEF_INT_CURRENT_MODE] == Defines.DEF_RMODE_RB_FANFARE)
        {
            int_s_value[Defines.DEF_INT_TOP_LAMP] = 1;
        }
        else if (IS_BONUS_GAME()
              || int_s_value[Defines.DEF_INT_CURRENT_MODE] == Defines.DEF_RMODE_BB_FANFARE)
        {
            int_s_value[Defines.DEF_INT_TOP_LAMP] = 2;
        }
        else if (IS_REPLAY())
        {
            int_s_value[Defines.DEF_INT_TOP_LAMP] = 3;
        }

        // satoh#暫定
        // 0: 点滅スピード(ms)

        var v001 = int_s_value[Defines.DEF_INT_MODE_COUNTER];
        var v002 = FLLXX[int_s_value[Defines.DEF_INT_TOP_LAMP]][0];
        var v003 = int_s_value[Defines.DEF_INT_LOOP_SPEED];


        if ((int_s_value[Defines.DEF_INT_MODE_COUNTER] % (FLLXX[int_s_value[Defines.DEF_INT_TOP_LAMP]][0]
                / int_s_value[Defines.DEF_INT_LOOP_SPEED] + 1)) == 0)
        {
            //		if ((int_s_value[Defines.DEF_INT_MODE_COUNTER] % (FLLXX[int_s_value[Defines.DEF_INT_TOP_LAMP]][0]
            //				/ 1 + 1 )) == 0) {
            // ボーナス上部ランプの点滅スピード調整
            int_s_value[Defines.DEF_INT_SEQUENCE_EFFECT]++;
        }

        // Defines.DEF_INT_SEQUENCE_EFFECTを使いまわしているので、注意
        if (int_s_value[Defines.DEF_INT_SEQUENCE_EFFECT] >= FLLXX[int_s_value[Defines.DEF_INT_TOP_LAMP]].Length - 1)
        {
            int_s_value[Defines.DEF_INT_SEQUENCE_EFFECT] = 1;
        }

        //DfMain.TRACE(("Defines.DEF_INT_TOP_LAMP:" + int_s_value[Defines.DEF_INT_TOP_LAMP] + ":" + int_s_value[Defines.DEF_INT_SEQUENCE_EFFECT]);
        int data = FLLXX[int_s_value[Defines.DEF_INT_TOP_LAMP]][int_s_value[Defines.DEF_INT_SEQUENCE_EFFECT]];
        for (int i = 0; i < 8; i++)
        {
            int action = ((data & (1 << i)) != 0) ? Defines.DEF_LAMP_ACTION_ON : Defines.DEF_LAMP_ACTION_OFF;
            if (i < 5)
            {
                lampSwitch(Defines.DEF_LAMP_TOP_1 + i, action);
            }
            else
            {
                lampSwitch(Defines.DEF_LAMP_S3 - (i - 5), action);
                lampSwitch(Defines.DEF_LAMP_S4 + (i - 5), action);
            }
        }
    }

    // ======================================================
    // TOBE [スイッチメソッド]
    // ======================================================
    /**
     * ボタンのスイッチ
     * 
     */
    private void ctrlButtonLamp()
    {
        for (int i = 0; i < 3; i++)
        {
            if (int_s_value[Defines.DEF_INT_REEL_STOP_R0 + i] != ANGLE_UNDEF
                    || (int_s_value[Defines.DEF_INT_KEY_REJECT] > 0))
            {
                // 止められている又はキーリジェクト前
                lampSwitch(Defines.DEF_LAMP_BUTTON_L + i, Defines.DEF_LAMP_ACTION_OFF);
            }
            else
            {
                // リールが回ってるところだけ着色
                lampSwitch(Defines.DEF_LAMP_BUTTON_L + i, Defines.DEF_LAMP_ACTION_ON);
            }
        }
    }

    /**
     * BETランプのスイッチ
     * 
     */
    private void ctrlBetLamp()
    {
        switch (int_s_value[Defines.DEF_INT_CURRENT_MODE])
        {
            case Defines.DEF_RMODE_BET:
            case Defines.DEF_RMODE_SPIN:
                for (int i = 0; i < 3; i++)
                {
                    if (int_s_value[Defines.DEF_INT_BETTED_COUNT] > i)
                    {
                        lampSwitch(Defines.DEF_LAMP_BET_3 + i, Defines.DEF_LAMP_ACTION_ON);
                        lampSwitch(Defines.DEF_LAMP_BET_3 - i, Defines.DEF_LAMP_ACTION_ON);
                    }
                    else
                    {
                        lampSwitch(Defines.DEF_LAMP_BET_3 + i, Defines.DEF_LAMP_ACTION_OFF);
                        lampSwitch(Defines.DEF_LAMP_BET_3 - i, Defines.DEF_LAMP_ACTION_OFF);
                    }
                }
                break;
            case Defines.DEF_RMODE_WAIT:
            case Defines.DEF_RMODE_RESULT:
            case Defines.DEF_RMODE_BB_FANFARE:
            case Defines.DEF_RMODE_RB_FANFARE:
                // そろったラインを光らす
                for (int i = 0; i < 5; i++)
                {
                    if (_lampTime < Util.GetMilliSeconds())
                    {
                        lampSwitch(Defines.DEF_LAMP_BET_1 + i, Defines.DEF_LAMP_ACTION_OFF);
                    }
                    else
                    {
                        if ((v23.getWork(Defines.DEF_HITLINE) & (Defines.DEF__00001000B << i)) != 0)
                        {
                            int id = 0;
                            switch (i)
                            {
                                case 0:// センター
                                    id = Defines.DEF_LAMP_BET_3;
                                    break;
                                case 1:// トップ
                                    id = Defines.DEF_LAMP_BET_2;
                                    break;
                                case 2:// ボトム
                                    id = Defines.DEF_LAMP_BET_4;
                                    break;
                                case 3:// クロスダウン
                                    id = Defines.DEF_LAMP_BET_1;
                                    break;
                                case 4:// クロスｱｯﾌﾟ
                                    id = Defines.DEF_LAMP_BET_5;
                                    break;
                            }
                            if (int_s_value[Defines.DEF_INT_ON_OFF_EFFECT] > 0)
                            {
                                lampSwitch(id, Defines.DEF_LAMP_ACTION_OFF);
                            }
                            else
                            {
                                lampSwitch(id, Defines.DEF_LAMP_ACTION_ON);
                            }
                        }
                    }
                }
                break;
        }
    }

    private void ctrlLamp()
    {
        if (int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] > 0)
        {
            if (int_s_value[Defines.DEF_INT_ON_OFF_EFFECT] > 0)
            {
                lampSwitch(Defines.DEF_LAMP_WIN, Defines.DEF_LAMP_ACTION_ON);
                lampSwitch(Defines.DEF_LAMP_BAR, Defines.DEF_LAMP_ACTION_ON);
            }
            else
            {
                lampSwitch(Defines.DEF_LAMP_WIN, Defines.DEF_LAMP_ACTION_OFF);
                lampSwitch(Defines.DEF_LAMP_BAR, Defines.DEF_LAMP_ACTION_OFF);
            }
            if (int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] > 1)
            {
                if (int_s_value[Defines.DEF_INT_ON_OFF_EFFECT] > 0)
                {
                    lampSwitch(Defines.DEF_LAMP_4TH, Defines.DEF_LAMP_ACTION_OFF);
                }
                else
                {
                    lampSwitch(Defines.DEF_LAMP_4TH, Defines.DEF_LAMP_ACTION_ON);
                }
            }
        }
        else
        {
            lampSwitch(Defines.DEF_LAMP_WIN, Defines.DEF_LAMP_ACTION_OFF);
            lampSwitch(Defines.DEF_LAMP_BAR, Defines.DEF_LAMP_ACTION_OFF);
        }
        switch (int_s_value[Defines.DEF_INT_CURRENT_MODE])
        {
            case Defines.DEF_RMODE_RESULT:
                if (IS_REPLAY())
                {
                    lampSwitch(Defines.DEF_LAMP_FRE, Defines.DEF_LAMP_ACTION_ON);
                }
                break;
            case Defines.DEF_RMODE_WAIT:
            case Defines.DEF_RMODE_BET:
                // TODO C#移植 フォールスルーしていたのでC#で動くように変更
                if (int_s_value[Defines.DEF_INT_CURRENT_MODE] == Defines.DEF_RMODE_WAIT)
                {
                    if (int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] == 2)
                    {
                        if (int_s_value[Defines.DEF_INT_ON_OFF_EFFECT] > 0)
                        {
                            int_s_value[Defines.DEF_INT_FLASH_DATA] = 0x1ff;
                        }
                        else
                        {
                            int_s_value[Defines.DEF_INT_FLASH_DATA] = 0;
                        }
                    }
                }

                // ｽﾀｰﾄランプを点滅させる
                if (int_s_value[Defines.DEF_INT_BETTED_COUNT] > 0)
                {
                    if (int_s_value[Defines.DEF_INT_ON_OFF_EFFECT] > 0)
                    {
                        lampSwitch(Defines.DEF_LAMP_STA, Defines.DEF_LAMP_ACTION_OFF);
                    }
                    else
                    {
                        lampSwitch(Defines.DEF_LAMP_STA, Defines.DEF_LAMP_ACTION_ON);
                    }
                }
                else
                {
                    lampSwitch(Defines.DEF_LAMP_STA, Defines.DEF_LAMP_ACTION_OFF);
                }
                // インサートコインランプを点滅させる
                if (!IS_REPLAY()
                        && ((IS_BONUS_JAC() && int_s_value[Defines.DEF_INT_BETTED_COUNT] < 1) || (!IS_BONUS_JAC() && (int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM] < Defines.DEF_NUM_MAX_CREDIT || int_s_value[Defines.DEF_INT_BETTED_COUNT] < 3))))
                {
                    if (int_s_value[Defines.DEF_INT_ON_OFF_EFFECT] > 0)
                    {
                        lampSwitch(Defines.DEF_LAMP_INS, Defines.DEF_LAMP_ACTION_ON);
                    }
                    else
                    {
                        lampSwitch(Defines.DEF_LAMP_INS, Defines.DEF_LAMP_ACTION_OFF);
                    }
                }
                else
                {
                    lampSwitch(Defines.DEF_LAMP_INS, Defines.DEF_LAMP_ACTION_OFF);
                }

                // BETボタンを点滅
                if (int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM] > 0)
                {
                    if ((IS_BONUS_JAC() && int_s_value[Defines.DEF_INT_BETTED_COUNT] < 1)
                            || (!IS_BONUS_JAC() && int_s_value[Defines.DEF_INT_BETTED_COUNT] < 3))
                    {
                        if (int_s_value[Defines.DEF_INT_ON_OFF_EFFECT] > 0)
                        {
                            lampSwitch(Defines.DEF_LAMP_MAXBET, Defines.DEF_LAMP_ACTION_OFF);
                        }
                        else
                        {
                            lampSwitch(Defines.DEF_LAMP_MAXBET, Defines.DEF_LAMP_ACTION_ON);
                        }
                    }
                    else
                    {
                        lampSwitch(Defines.DEF_LAMP_MAXBET, Defines.DEF_LAMP_ACTION_OFF);
                    }
                }
                else
                {
                    lampSwitch(Defines.DEF_LAMP_MAXBET, Defines.DEF_LAMP_ACTION_OFF);
                }

                break;
            case Defines.DEF_RMODE_SPIN:
                lampSwitch(Defines.DEF_LAMP_STA, Defines.DEF_LAMP_ACTION_OFF);
                lampSwitch(Defines.DEF_LAMP_INS, Defines.DEF_LAMP_ACTION_OFF);
                lampSwitch(Defines.DEF_LAMP_MAXBET, Defines.DEF_LAMP_ACTION_OFF);
                lampSwitch(Defines.DEF_LAMP_FRE, Defines.DEF_LAMP_ACTION_OFF);
                // ﾃﾝﾊﾟｲランプの点滅
                if (int_s_value[Defines.DEF_INT_IS_TEMPAI] == 1)
                {
                    if (int_s_value[Defines.DEF_INT_ON_OFF_EFFECT] > 0)
                    {
                        lampSwitch(Defines.DEF_LAMP_CHANCE, Defines.DEF_LAMP_ACTION_OFF);
                    }
                    else
                    {
                        lampSwitch(Defines.DEF_LAMP_CHANCE, Defines.DEF_LAMP_ACTION_ON);
                    }
                }
                else
                {
                    lampSwitch(Defines.DEF_LAMP_CHANCE, Defines.DEF_LAMP_ACTION_OFF);
                }
                break;
        }
    }

    /// <summary>
    /// 指定したリールの停止状態を取得する。
    /// </summary>
    /// <param name="stopNum">第？回胴停止(左=0, 中=1, 右=2)</param>
    /// <returns>既に止まっていたら true</returns>
    public bool IsReelStopped(int stopNum)
    {
        if ((int_s_value[Defines.DEF_INT_IS_REEL_STOPPED] & BIT(stopNum)) != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // ======================================================
    // TOBE [メイン制御内部メソッド]
    // ======================================================
    /**
     * リールを止めるべき所に止める。
     * 
     * @param stopNum
     *            第？回胴停止(左=0, 中=1, 右=2)
     * @return 既に止まっていたら true
     */
    private bool setReelStopAngle(int stopNum)
    {
        if (IsReelStopped(stopNum))
        {
            return true; // 止まってる
        }

        // 止まる場所
        int result_index;
        // ////////////////
        // 目押サポート付ボーナスイン
        result_index = ANGLE2INDEX(int_s_value[Defines.DEF_INT_REEL_ANGLE_R0 + stopNum]);

        result_index = EyeSniper(stopNum);

        // 停止角度を決める
        int_s_value[Defines.DEF_INT_REEL_STOP_R0 + stopNum] = INDEX2ANGLE(result_index);

        // 停止出目を覚えておく
        int_s_value[Defines.DEF_INT_PREV_GAME] &= ~(0x1F << stopNum * 5);// 対象BITをクリア
        int_s_value[Defines.DEF_INT_PREV_GAME] |= (result_index << (stopNum * 5));// 記憶

        if (mobile.isJacCut() == true)
        {
            return true;
        }
        //		if (mobile.isJacCut() && IS_BONUS_JAC()) {
        //			DfMain.TRACE(("JACkカットになっている？");
        //			return true;
        //		}

        return false;
    } // End of setReelStopAngle() Method

    /**
     * 各種払い出し音セット
     */
    private void playCoinSound()
    {
        int snd_id = Defines.DEF_SOUND_UNDEF;
        _soundTime = Util.GetMilliSeconds() + 0;
        if (int_s_value[Defines.DEF_INT_WIN_COIN_NUM] <= 0)
        {
            if (IS_REPLAY())
            {
                snd_id = Defines.DEF_SOUND_24;
                _soundTime = Util.GetMilliSeconds() + Defines.DEF_SOUND_MS_24;
            }
        }
        else if (int_s_value[Defines.DEF_INT_WIN_COIN_NUM] < 15)
        {
            snd_id = Defines.DEF_SOUND_16;
            _soundTime = Util.GetMilliSeconds()
                    + (120 * int_s_value[Defines.DEF_INT_WIN_COIN_NUM]);
        }
        else
        {
            if (IS_BONUS_JAC())
            {
                snd_id = Defines.DEF_SOUND_18;
                _soundTime = Util.GetMilliSeconds() + Defines.DEF_SOUND_MS_18;
            }
            else
            {
                snd_id = Defines.DEF_SOUND_17;
                _soundTime = Util.GetMilliSeconds() + Defines.DEF_SOUND_MS_17;
            }
        }
        playSE(snd_id);
    }

    /**
     * グラフ更新(シフト)
     * 
     * ボーナス終了後の次ゲームのリール回転開始のタイミングで呼ぶ.
     * 
     * @param current =
     *            現在の回転数
     * @param game_kind =
     *            BIG/REG/NORMAL
     * 
     * @see bonus_Data[][]
     * @see df.Df#UNIT_GAMES
     * @see df.Df#INFO_GAMES
     * @see df.Df#GAME_BIG
     * @see df.Df#GAME_REG
     * @see df.Df#GAME_NONE
     * 
     */
    private void shiftDataPanelHistory(int current, int game_kind)
    {
        // Y軸 高さ.
        int idx = current / Defines.DEF_UNIT_GAMES;
        if (idx >= Defines.DEF_INFO_GAMES)
        { // == bonus_Data[x].Length
            idx = Defines.DEF_INFO_GAMES - 1;
        }

        // --- set --
        if (game_kind == Defines.DEF_PS_BB_RUN)
        {
            bonus_Data[int_s_value[Defines.DEF_INT_BONUS_DATA_BASE], idx] = Defines.DEF_GAME_BIG;
        }
        else if (game_kind == Defines.DEF_PS_RB_RUN)
        {
            bonus_Data[int_s_value[Defines.DEF_INT_BONUS_DATA_BASE], idx] = Defines.DEF_GAME_REG;
        }

        // -- shift --
        int_s_value[Defines.DEF_INT_BONUS_DATA_BASE]--;
        if (int_s_value[Defines.DEF_INT_BONUS_DATA_BASE] < 0)
        {
            int_s_value[Defines.DEF_INT_BONUS_DATA_BASE] = Defines.DEF_INFO_GAME_HISTORY - 1;
        }
        for (int i = 0; i < Defines.DEF_INFO_GAMES; i++)
        {
            bonus_Data[int_s_value[Defines.DEF_INT_BONUS_DATA_BASE], i] = Defines.DEF_GAME_NONE;
        }
    }

    /**
     * 進行中のゲーム情報を蓄積していく
     * 
     * 毎ゲーム呼ぶ.
     * 
     * @param current =
     *            現在のボーナス間回転数
     * 
     * @see bonus_Data[][]
     * @see df.Df#UNIT_GAMES
     * @see df.Df#INFO_GAMES
     * @see df.Df#GAME_NONE
     * @see df.Df#GAME_NORMAL
     * 
     */
    private void setCurrentDataPanel(int current)
    {
        // Y軸 高さ.
        int idx = (current - 1) / Defines.DEF_UNIT_GAMES;
        if (idx >= Defines.DEF_INFO_GAMES)
        { // == bonus_Data[x].Length
            idx = Defines.DEF_INFO_GAMES - 1;
        }

        // 通常.
        if (bonus_Data[int_s_value[Defines.DEF_INT_BONUS_DATA_BASE], idx] == Defines.DEF_GAME_NONE)
        {
            bonus_Data[int_s_value[Defines.DEF_INT_BONUS_DATA_BASE], idx] = Defines.DEF_GAME_NORMAL;
        }
    }

    /**
     * ＢＢ作動中ですか？
     * 
     * @return true=ＢＢ作動中
     */
    public bool IS_BONUS_GAME()
    {
        return (v23.getWork(Defines.DEF_GAMEST) & 0x80) != 0 && !IS_BONUS_JAC();
    }

    /**
     * BONUS中ですか？
     * 
     * @return
     */
    public bool IS_BONUS()
    {
        return IS_BONUS_GAME() || IS_BONUS_JAC();
    }

    /**
     * ＲＢ作動中ですか？
     * 
     * @return true=ＲＢ作動中
     */
    public bool IS_BONUS_JAC()
    {
        return (v23.getWork(Defines.DEF_GAMEST) & 0x01) != 0;
    }

    /**
     * 
     */
    public bool IS_REPLAY()
    {
        return v23.getWork(Defines.DEF_GAMEST) == 0x4
                || (!IS_BONUS() && v23.getWork(Defines.DEF_HITFLAG) == 0x8);
    }

    public int getReelId(int reelBit)
    {
        switch (reelBit)
        {
            case Defines.DEF_BAR_:
                return Defines.DEF_ID_REEL_BAR_;
            case Defines.DEF_BELL:
                return Defines.DEF_ID_REEL_BELL;
            case Defines.DEF_BSVN:
                return Defines.DEF_ID_REEL_BSVN;
            case Defines.DEF_CHRY:
                return Defines.DEF_ID_REEL_CHRY;
            case Defines.DEF_RPLY:
                return Defines.DEF_ID_REEL_RPLY;
            case Defines.DEF_DON_:
                return Defines.DEF_ID_REEL_RSVN;
            case Defines.DEF_WMLN:
                return Defines.DEF_ID_REEL_WMLN;
        }
        return 0;
    }

    private int[] isTempai()
    {
        int[] tempai = { Defines.DEF_BB_UNDEF, 0 };
        int yukou = 5;// 有効ライン
        if (int_s_value[Defines.DEF_INT_BET_COUNT] == 1)
        {
            yukou = 1;
        }
        else if (int_s_value[Defines.DEF_INT_BET_COUNT] == 2)
        {
            yukou = 3;
        }
        for (int line = 0; line < yukou; line++)
        {
            int tmp = Defines.DEF_ARAY;
            for (int reel = 0; reel < 3; reel++)
            {
                tmp &= v23.getWork(Defines.DEF_ARAY11 + (line * 3) + reel);
            }
            if ((tmp & Defines.DEF_BSVN) != 0)
            {
                tempai[0] = Defines.DEF_BB_B7;
            }
            else if ((tmp & Defines.DEF_DON_) != 0)
            {
                tempai[0] = Defines.DEF_BB_R7;
                tempai[1]++;
            }
        }
        return tempai;
    }

    /// <summary>
    /// ｎビット左シフト。
    /// </summary>
    /// <param name="n">nﾋﾞｯﾄ左ｼﾌﾄ</param>
    /// <returns>ビット</returns>
    //[Obsolete("削除予定とのコメントあり")]
    public int BIT(int n)
    {
        return 1 << n;
    }

    /// <summary>
    /// SE を再生(Director.directionデータから選ばず、直接鳴動を指定するときに使う)
    /// </summary>
    /// <param name="id"></param>
    public void playSE(int id)
    {
        if (Defines.DEF_USE_MULTI_SOUND)
        {
            // 重畳の場合
            mobile.playSound(id, false, Defines.DEF_SOUND_MULTI_SE);
        }
        else
        {
            //// 非重畳の場合はボーナスゲームのＢＧＭを優先して鳴らす（＝ボーナスゲームでないときだけ鳴らせる）
            //if (!IS_BONUS_GAME())
            //{
            //    // 非重畳の場合は第三引数に意味はない！！
            //    mobile.playSound(id, false, Defines.DEF_SOUND_MULTI_SE);
            //}
        }
    }

    /**
     * BGM を再生(Director.directionデータから選ばず、直接鳴動を指定するときに使う)
     * 
     * @param id
     *            サウンドＩＤ
     * @param loop
     *            ループ再生
     */
    public void playBGM(int id, bool loop)
    {
        // 非重畳の場合は第３引数は無視されるのよ
#if __COM_TYPE__
        if (loop == true)
        {
            bgm_no = id;
            bgm_loop = loop;
        }
        else
        {
            bgm_no = -1;
            bgm_loop = false;
        }
#endif
        GameManager.PlayBGM(id, loop);
        //mobile.playSound(id, loop, Defines.DEF_SOUND_MULTI_BGM);
    }

    // ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
    // もともとDirector.classだったがプログラムサイズが大きすぎてこちらに移動
    // ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
    // /**
    // * 現在のフラッシュデータ
    // */
    // private  char[] flash = { 0 };

    /** 現在位置 */
    private int current = 0;

    /** 繰り返し回数 */
    private int repeat = 0;

    /** 終了判定 */
    private bool play = false;

    /**
     * フラッシュを設定、各種初期化
     */
    public void setFlash(int idx)
    {
        if (idx > 0)
        {
            flash = FLASHTBL[idx - 1];
            current = -2;
            repeat = 0;
            play = true;
        }
    }

    /**
     * @return 次のフラッシュ位置
     */
    public int getNext()
    {
        if (repeat <= 0)
        {
            current += 2;
            if (current < flash.Length)
            {
                repeat = flash[current];
                if (repeat != Defines.DEF_FEND)
                {
                    repeat = repeat * 3 / 5;
                    if (repeat == 0)
                    {
                        repeat = 1;
                    }
                    return flash[current + 1];
                }
            }
        }
        else
        {
            repeat--;
            return flash[current + 1];
        }

        play = false;
        return 0x1ff;
    }

    /**
     * 終了判定
     * 
     * @return
     */
    public bool isPlay()
    {
        return play;
    }

    public readonly int[][] RLPTNDT = {
	// RLPTNDT01
			new int[] { Defines.DEF_R_ST3 + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_RVS, Defines.DEF_RP13, // 1
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_SLW + Defines.DEF_R_NRL, Defines.DEF_RP13, // 2
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_SLW + Defines.DEF_R_RVS, Defines.DEF_RP13, // 3
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_SLW + Defines.DEF_R_NRL, Defines.DEF_RP08, // 4
			},
			// RLPTNDT02
			new int[] { Defines.DEF_R_ST3 + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_RVS, Defines.DEF_RP13, // 1
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_SLW + Defines.DEF_R_NRL, Defines.DEF_RP13, // 2
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_SLW + Defines.DEF_R_RVS, Defines.DEF_RP01, // 3
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_SLW + Defines.DEF_R_NRL, Defines.DEF_RP08, // 4
			},
			// RLPTNDT03
			new int[] { Defines.DEF_R_ST3 + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP13, // 1
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_NRS + Defines.DEF_R_RVS, Defines.DEF_RP01, // 2
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_SLW + Defines.DEF_R_NRL, Defines.DEF_RP08, // 3
			},
			// RLPTNDT04
			new int[] { Defines.DEF_R_ST3 + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP13, // 1
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_NRS + Defines.DEF_R_RVS, Defines.DEF_RP01, // 2
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_SLW + Defines.DEF_R_NRL, Defines.DEF_RP19, // 3
			},
			// RLPTNDT05
			new int[] { Defines.DEF_R_ST3 + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_RVS, Defines.DEF_RP13, // 1
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_NRS + Defines.DEF_R_RVS, Defines.DEF_RP16, // 2
			},
			// RLPTNDT06
			new int[] { Defines.DEF_R_ST3 + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP01, // 1
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_NRS + Defines.DEF_R_RVS, Defines.DEF_RP01, // 2
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_SLW + Defines.DEF_R_NRL, Defines.DEF_RP01, // 3
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_SLW + Defines.DEF_R_NRL, Defines.DEF_RP08, // 4
			},
			// RLPTNDT07
			new int[] { Defines.DEF_R_ST3 + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP01, // 1
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP01, // 2
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_SLW + Defines.DEF_R_RVS, Defines.DEF_RP13, // 3
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_SLW + Defines.DEF_R_RVS, Defines.DEF_RP08, // 4
			},
			// RLPTNDT08
			new int[] { Defines.DEF_R_ST3 + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_RVS, Defines.DEF_RP01, // 1
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_SLW + Defines.DEF_R_NRL, Defines.DEF_RP13, // 2
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_SLW + Defines.DEF_R_NRL, Defines.DEF_RP19, // 3
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_SLW + Defines.DEF_R_RVS, Defines.DEF_RP08, // 4
			},
			// RLPTNDT09
			new int[] { Defines.DEF_R_ST3 + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_RVS, Defines.DEF_RP01, // 1
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_SLW + Defines.DEF_R_NRL, Defines.DEF_RP13, // 2
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_SLW + Defines.DEF_R_NRL, Defines.DEF_RP19, // 3
			},
			// RLPTNDT10
			new int[] { Defines.DEF_R_ST3 + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP01, // 1
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_NRS + Defines.DEF_R_RVS, Defines.DEF_RP16, // 2
			},
			// RLPTNDT11
			new int[] { Defines.DEF_R_ST2 + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_RVS, Defines.DEF_RP13, // 1
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_NRS + Defines.DEF_R_RVS, Defines.DEF_RP16, // 2
			},
			// RLPTNDT12
			new int[] { Defines.DEF_R_ST2 + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_RVS, Defines.DEF_RP01, // 1
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_NRS + Defines.DEF_R_RVS, Defines.DEF_RP16, // 2
			},
			// RLPTNDT13
			new int[] { Defines.DEF_R_ST2 + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP13, // 1
			},
			// RLPTNDT14
			new int[] { Defines.DEF_R_ST2 + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP01, // 1
			},
			// RLPTNDT15
			new int[] { Defines.DEF_R_ST2 + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP16, // 1
			},
			// RLPTNDT16
			new int[] { Defines.DEF_R_ST2 + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP04, // 1
			},
			// RLPTNDT17
			new int[] { Defines.DEF_R_ST1 + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_RVS, Defines.DEF_RP13, // 1
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP04, // 2
			},
			// RLPTNDT18
			new int[] { Defines.DEF_R_ST1 + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_RVS, Defines.DEF_RP01, // 1
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP04, // 2
			},
			// RLPTNDT19
			new int[] { Defines.DEF_R_ST1 + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP13, // 1
			},
			// RLPTNDT20
			new int[] { Defines.DEF_R_ST1 + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP01, // 1
			},
			// RLPTNDT21
			new int[] { Defines.DEF_R_ST1 + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP16, // 1
			},
			// RLPTNDT22
			new int[] { Defines.DEF_R_ST1 + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP04, // 1
			},
			// RLPTNDT23
			new int[] { Defines.DEF_R_STS + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_RVS, Defines.DEF_RP08, // 1
			},
			// RLPTNDT24
			new int[] { Defines.DEF_R_STS + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP19, // 1
			},
			// RLPTNDT25
			new int[] { Defines.DEF_R_STS + Defines.DEF_ST_T3, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP04, // 1
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP05, // 2
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP06, // 3
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP07, // 4
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_SLW + Defines.DEF_R_NRL, Defines.DEF_RP19, // 5
			},
			// RLPTNDT26
			new int[] { Defines.DEF_R_STS + Defines.DEF_ST_T3, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP04, // 1
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP05, // 2
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP06, // 3
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP07, // 4
					Defines.DEF_R_STE + Defines.DEF_ST_TE, Defines.DEF_R_SLW + Defines.DEF_R_NRL, Defines.DEF_RP08, // 5
			},
			// RLPTNDT27
			new int[] { Defines.DEF_R_STS + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP13, // 1
			},
			// RLPTNDT28
			new int[] { Defines.DEF_R_STS + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_RVS, Defines.DEF_RP01, // 1
			},
			// RLPTNDT29
			new int[] { Defines.DEF_R_STS + Defines.DEF_ST_TN, Defines.DEF_R_NRS + Defines.DEF_R_NRL, Defines.DEF_RP08, // 1
			}, 
			};

    // private  final char[][] FLASHTBL = {
    // // FLASH_01[] = { // ﾌﾗｯｼｭ演出ﾃﾞｰﾀ 01
    // { 3, F2 | F3 | F5 | F6 | F8 | F9 | FNON, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 01
    // 3, F1 | F3 | F4 | F6 | F7 | F9 | FNON, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ
    // // ﾊﾟﾀｰﾝ 02
    // 3, F1 | F2 | F4 | F5 | F7 | F8 | FNON, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ
    // // ﾊﾟﾀｰﾝ 03
    // FEND // ｴﾝﾄﾞｺｰﾄﾞ
    // },
    // // FLASH_02[] = { // ﾌﾗｯｼｭ演出ﾃﾞｰﾀ 02
    // { 3, F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 01
    // 3, F4 | F7 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 02
    // 3, F1 | F2 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 03
    // 3, F3 | F6 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 04
    // 3, F8 | F9 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 05
    // 3, F5 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 06
    // 3, F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 07
    // FEND // ｴﾝﾄﾞｺｰﾄﾞ
    // },
    // // FLASH_03[] = { // ﾌﾗｯｼｭ演出ﾃﾞｰﾀ 03
    // { 3, F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 01
    // 3, F8 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 02
    // 3, F5 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 03
    // 3, F2 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 04
    // 9, F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 05
    // 6, F5 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 06
    // 3, F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 07
    // 3, F5 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 08
    // 6, F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 09
    // FEND // ｴﾝﾄﾞｺｰﾄﾞ
    // },
    // // FLASH_04[] = { // ﾌﾗｯｼｭ演出ﾃﾞｰﾀ 04
    // { 3, F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 01
    // 3, F8 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 02
    // 3, F5 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 03
    // 3, F2 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 04
    // 9, F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 05
    // 3, F5 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 06
    // 3, F2 | F4 | F6 | F8 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ
    // // ﾊﾟﾀｰﾝ 07
    // 4, F1 | F2 | F3 | F4 | F6 | F7 | F8 | F9 | F10 | F11 | F12, // ﾘｰﾙ
    // // ﾗﾝﾌﾟ
    // // ﾃﾞﾓ
    // // ﾊﾟﾀｰﾝ
    // // 08
    // 6, F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 09
    // FEND // ｴﾝﾄﾞｺｰﾄﾞ
    // },
    // // FLASH_05[] = { // ﾌﾗｯｼｭ演出ﾃﾞｰﾀ 05
    // { 8, F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 01
    // 3, F7 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 02
    // 3, F4 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 03
    // 3, F1 | F4 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 04
    // 3, F1 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 05
    // 3, F9 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 06
    // 3, F6 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 07
    // 3, F3 | F6 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 08
    // 3, F3 | F8 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 09
    // 3, F5 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 10
    // 3, F2 | F5 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 11
    // 3, F2 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 12
    // 15, F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 13
    // 3, F4 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 14
    // 3, F1 | F5 | F7 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 15
    // 10, F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 16
    // 3, F6 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 17
    // 3, F3 | F5 | F9 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 18
    // 15, F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 19
    // 3, F5 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 20
    // 3, F2 | F4 | F6 | F8 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ
    // // ﾊﾟﾀｰﾝ 21
    // 4, F1 | F3 | F7 | F9 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ
    // // ﾊﾟﾀｰﾝ 22
    // 10, F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 23
    // FEND // ｴﾝﾄﾞｺｰﾄﾞ
    // },
    // // FLASH_06[] = { // ﾌﾗｯｼｭ演出ﾃﾞｰﾀ 06
    // {
    // 8,
    // F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 01
    // 3,
    // F8 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 02
    // 3,
    // F5 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 03
    // 4,
    // F2 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 04
    // 10,
    // FNON, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 05
    // 2,
    // F1 | F2 | F3 | F4 | F5 | F6 | F7 | F8 | F9 | F10 | F11
    // | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 06
    // 15, FNON, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 07
    // 3, F2 | F6 | F7 | F10 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 08
    // 3, F3 | F4 | F8 | F10, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 09
    // 3, F5 | F7 | F9 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 10
    // 3, F1 | F2 | F6 | F7 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 11
    // 3, F5 | F9 | F10 | F11, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 12
    // 3, F2 | F6 | F8 | F11, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 13
    // 3, F3 | F4 | F8 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 14
    // 4, F1 | F5 | F7 | F9 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 15
    // 4, F2 | F4 | F6 | F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 16
    // 15, F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 17
    // 5, F1 | F3 | F4 | F6 | F8 | FNON, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 18
    // 8, F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 19
    // 5, F1 | F3 | F4 | F6 | F8 | FNON, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 20
    // 8, F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 21
    // 5, F1 | F3 | F4 | F6 | F8 | FNON, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 22
    // 10, F10 | F11 | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 23
    // FEND // ｴﾝﾄﾞｺｰﾄﾞ
    // },
    // // FLASH_07[] = { // ﾌﾗｯｼｭ演出ﾃﾞｰﾀ 07
    // {
    // 2,
    // F1 | F2 | F3 | F4 | F5 | F6 | F7 | F8 | F9 | F10 | F11
    // | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 01
    // 2,
    // F1 | F2 | F3 | F4 | F5 | F6 | F7 | F8 | F9 | FNON, // ﾘｰﾙ
    // // ﾗﾝﾌﾟ
    // // ﾃﾞﾓ
    // // ﾊﾟﾀｰﾝ
    // // 02
    // 2,
    // F1 | F2 | F3 | F4 | F5 | F6 | F7 | F8 | F9 | F10 | F11
    // | F12, // ﾘｰﾙ ﾗﾝﾌﾟ ﾃﾞﾓ ﾊﾟﾀｰﾝ 03
    // FEND // ｴﾝﾄﾞｺｰﾄﾞ
    // }, };

    // // //
    // //
    // ----------------------------------------------------------------------------------------------
    // // // 遊技状態表示ＬＥＤデータテーブル
    // //
    // //----------------------------------------------------------------------------------------------
    // public  final char[][] FLLXX = {
    // //ダミー
    // {100,0x00,0x00,0x00,0x00},
    // // RB作動時の点灯
    // { 120, 0x91, 0x4E, 0x24, 0x4E },
    // // BB作動時の点灯
    // { 80, 0x35, 0x7B, 0xE4, 0x0A },
    // // 再遊技作動時の点灯
    // { 200, 0x11, 0x0E, 0x11, 0x0E },
    // // 正回転時の点灯
    // { 80, 0x92, 0x49, 0x24, 0x00 },
    // // 逆回転時の点灯
    // { 80, 0x89, 0x52, 0x24, 0x00 }, };

    // //////////////////////////////////////////////////////////////
    // compact.CompactClass char[]

    /*------------------ class Director ------------------*/

    /** AUTO GENERATED char ARRAY BY compact.CompactClass */
    private char[] flash = "\u0000".ToCharArray();

    /** AUTO GENERATED char ARRAY BY compact.CompactClass */
    private char[][] FLASHTBL = {
			"\u0003\u01F8\u0003\u01C7\u0003\u003F\uFFFF".ToCharArray(),
			"\u0003\u0E00\u0003\u0E03\u0003\u0E24\u0003\u0F80\u0003\u0E48\u0003\u0E10\u0003\u0E00\uFFFF"
					.ToCharArray(),
			"\u0003\u0E00\u0003\u0E08\u0003\u0E10\u0003\u0E20\u0009\u0E00\u0006\u0E10\u0003\u0E00\u0003\u0E10\u0006\u0E00\uFFFF"
					.ToCharArray(),
			"\u0003\u0E00\u0003\u0E08\u0003\u0E10\u0003\u0E20\u0009\u0E00\u0003\u0E10\u0003\u0EAA\u0004\u0FEF\u0006\u0E00\uFFFF"
					.ToCharArray(),
			"\u0008\u0E00\u0003\u0E01\u0003\u0E02\u0003\u0E06\u0003\u0E04\u0003\u0E40\u0003\u0E80\u0003\u0F80\u0003\u0F08\u0003\u0E10\u0003\u0E30\u0003\u0E20\u000F\u0E00\u0003\u0E02\u0003\u0E15\n\u0E00\u0003\u0E80\u0003\u0F50\u000F\u0E00\u0003\u0E10\u0003\u0EAA\u0004\u0F45\n\u0E00\uFFFF"
					.ToCharArray(),
			"\u0008\u0E00\u0003\u0E08\u0003\u0E10\u0004\u0E20\n\u0000\u0002\u0FFF\u000F\u0000\u0003\u0AA1\u0003\u030A\u0003\u0E51\u0003\u0CA5\u0003\u0650\u0003\u04A8\u0003\u0F0A\u0004\u0855\u0004\u0EA2\u000F\u0E00\u0005\u018E\u0008\u0E00\u0005\u018E\u0008\u0E00\u0005\u018E\n\u0E00\uFFFF"
					.ToCharArray(),
			"\u0002\u0FFF\u0002\u01FF\u0002\u0FFF\uFFFF".ToCharArray(), };

    /** AUTO GENERATED char ARRAY BY compact.CompactClass */
    public char[][] FLLXX = {
			"\u0064\u0000\u0000\u0000\u0000\u0000".ToCharArray(),
			"\u0078\u0091\u004E\u0024\u004E\u0000".ToCharArray(),
			"\u0050\u0035\u007B\u00E4\n\u0000".ToCharArray(),
			"\u00C8\u0011\u000E\u0011\u000E\u0000".ToCharArray(),
			"\u0050\u0092\u0049\u0024\u0000\u0000".ToCharArray(),
			"\u0050\u0089\u0052\u0024\u0000\u0000".ToCharArray(), };

    // //////////////////////////////////////////////////////////////
    // compact.CompactClass byte[]

    /*------------------ class Director ------------------*/
    // /////////////////////////////////
    // BREWからの告知移植
    /** 3D Rect用 */
    private int[] polygon = new int[4 * 3];// xyz*4

    /** 3D Rect用 */
    private int[] polygon_color = new int[3];// rgb

    /**
     * 透過矩形描画.
     * 
     * @param x0
     * @param y0
     * @param w
     * @param h
     * @param r
     * @param g
     * @param b
     * @param ink
     */
    public void _drawEffect(int x0, int y0, int w, int h, int r, int g,
            int b, int ink)
    {

        const int bias_x = 120;// ZZ.centerX;
        const int bias_y = 120;// ZZ.centerY;


        // x0, y0, z0
        polygon[0] = x0 - bias_x;
        polygon[1] = -y0 - bias_y;
        polygon[2] = 0;

        // x1, y1, z1
        polygon[3] = x0 + w - bias_x;
        polygon[4] = -y0 + 0 - bias_y;
        polygon[5] = 0;

        // x2, y2, z2
        polygon[6] = x0 + w - bias_x;
        polygon[7] = -y0 + -h - bias_y;
        polygon[8] = 0;

        // x3, y3, z3
        polygon[9] = x0 + 0 - bias_x;
        polygon[10] = -y0 + -h - bias_y;
        polygon[11] = 0;

        int x = x0 - bias_x;
        int y = y0 - bias_y;

        // x0, y0, z0
        polygon[0] = x;
        polygon[1] = y;
        polygon[2] = 0;

        // x1, y1, z1
        polygon[3] = x + w;
        polygon[4] = y;
        polygon[5] = 0;

        // x2, y2, z2
        polygon[6] = x + w;
        polygon[7] = y + h;
        polygon[8] = 0;

        // x3, y3, z3
        polygon[9] = x + 0;
        polygon[10] = y + h;
        polygon[11] = 0;

        // color
        polygon_color[0] = r;
        polygon_color[1] = g;
        polygon_color[2] = b;

        if (ink == Defines.DEF_INK_SUB)
        {
            ZZ.drawPolygonRectSub(polygon, polygon_color);
        }
        else if (ink == Defines.DEF_INK_ADD)
        {
            ZZ.drawPolygonRectAdd(polygon, polygon_color);
        }
        else
        {
            ZZ.drawPolygonRect(polygon, polygon_color);
        }
        ZZ.flush3D();
    }

    public int GET_INT_TO_STR(int str) { return str; }
    public int GET_STR_TO_INT(string str) { return (int)long.Parse(str); } 	// 符号付きで変換されるので一つ上の型からキャストする
    public ushort GET_STR_TO_CHAR(string str) { return (ushort)long.Parse(str); } 	// 符号付きで変換されるので一つ上の型からキャストする

    // 基本がこれ
    enum EYE_TYPE
    {
        EYE_TYPE_NONE,		// 目押しなし
        EYE_TYPE_BB,		// BB当選時
        EYE_TYPE_RB,		// RB当選時
        EYE_TYPE_BONUS,		// ボーナス時の３連どんちゃんねらい
        EYE_TYPE_SP_NO,		// 達人オートの通常時
        EYE_TYPE_SP_REP,	// 達人オートのリプレイ外し
        EYE_TYPE_TIMEOUT,	// 自動停止
    };

    // 目押し制御用
    // 引数
    //        stopNum	第？回胴停止(左=0, 中=1, 右=2)
    // 戻り値 true		目押し制御あり
    //        false		目押し制御なし
    public int EyeSniper(int stopNum)
    {
        int result_index;
        int tmp;
        ushort pos = 0;
        int[][] meoshi = { 
		    new int[]{ 9, 15, 13 }, // BB図柄
		    new int[]{ 13, 3, 3 }, // BB図柄
		    new int[]{ 0, 9, 17 }, // RB図柄
		    new int[]{ 2, 4, 11} // 通常時の達人オート
	    };
        EYE_TYPE EyeType = EYE_TYPE.EYE_TYPE_NONE;

        if (mobile.isMeoshi())
        {	// メニューからの目押しフラグ
            if (this.int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] > 0
                    && v23.getWork(Defines.DEF_HITREQ) == Defines.DEF_HITFLAG_NR_BB
                    && (v23.getWork(Defines.DEF_WAVEBIT) & Defines.DEF__00001110B) == 0)
            {	// DfOHHB_V23_DEF.DEF__00001110B　ﾍﾞﾙとｽｲｶとﾘﾌﾟが当選してないかﾁｪｯｸしている
                //DFMain.APP_TRACE("目押し　ビッグボーナス");
                EyeType = EYE_TYPE.EYE_TYPE_BB;
            }
            else if (this.int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] > 0
                        && v23.getWork(Defines.DEF_HITREQ) == Defines.DEF_HITFLAG_NR_RB
                        && (v23.getWork(Defines.DEF_WAVEBIT) & Defines.DEF__00001110B) == 0)
            {
                //DFMain.APP_TRACE("目押し　レギュラーボーナス");
                EyeType = EYE_TYPE.EYE_TYPE_RB;
            }
        }

        Defines.APP_TRACE("目押しタイプ:" + EyeType);
        switch (EyeType)
        {
            case EYE_TYPE.EYE_TYPE_BB:
                // BB
                pos = (ushort)meoshi[this.int_s_value[Defines.DEF_INT_BIG_COUNT] % 2][stopNum];
                break;
            case EYE_TYPE.EYE_TYPE_RB:
                // RB
                pos = (ushort)meoshi[2][stopNum];
                break;
            case EYE_TYPE.EYE_TYPE_BONUS:
                // 三連ドン狙い
                if (stopNum == 0)
                {
                    pos = (ushort)9;
                }
                break;
            case EYE_TYPE.EYE_TYPE_SP_NO:
            //// 達人オート通常時
            //pos = (ushort) meoshi[3][stopNum];
            // TODO C#移植 ここでフォールスルー
            case EYE_TYPE.EYE_TYPE_SP_REP:
                if (EyeType == EYE_TYPE.EYE_TYPE_SP_NO)
                {
                    // 達人オート通常時
                    pos = (ushort)meoshi[3][stopNum];
                }
                // ボーナス中リプレイ外し
                if (stopNum == 0)
                {
                    pos = (ushort)1;
                }
                break;
            case EYE_TYPE.EYE_TYPE_TIMEOUT:
                // 自動停止
                tmp = 0x1F & (this.int_s_value[Defines.DEF_INT_PREV_GAME] >> (stopNum * 5));
                tmp = (tmp + Defines.DEF_N_FRAME + 2) % Defines.DEF_N_FRAME;
                pos = (ushort)tmp;
                break;
            default:
                // 目押しサポートなしの通常リール停止テーブル参照。
                pos = (ushort)ANGLE2INDEX(this.int_s_value[Defines.DEF_INT_REEL_ANGLE_R0 + stopNum]);
                break;
        }

        //pos = (ushort) ANGLE2INDEX(this.int_s_value[DfOHHB_V23_DEF.DEF_INT_REEL_ANGLE_R0 + stopNum]);
        result_index = v23.mReelStop(stopNum, pos);
        Defines.APP_TRACE("ﾘｰﾙ[" + stopNum + "] = " + (pos & 0xFFFF) + " 停止位置:" + result_index);
        ZZ.reelStopStatus[stopNum] = "ﾘｰﾙ[" + stopNum + "] = " + (pos & 0xFFFF) + " 停止位置:" + result_index;
        return result_index;


    }

    int getStopReel(int index, bool limit)
    {
        int ret;
        int num = 0;
        //int stop[][] = {{0,1,2}, {2,1,0}};// 順押しと逆押し

        if (limit == true)
        {	// 自動停止の場合は順押しにする
            //ret = gp.gpif_oshijun_list[0][index];
            num = 0;
        }
        ret = slotInterface.gpif_oshijun_list[num][index];

        //DFMain.APP_TRACE("押し順: gpif_oshijun_list["+num+"]["+index+"]=" + ret);
        return ret;
    }

    // 役の変更
    void chgYaku()
    {
        Defines.ForceYakuFlag flag = Defines.ForceYakuFlag.NONE;

        if (slotInterface.gpif_bonus_n == 1)
        {	// BB強制

            flag = Defines.ForceYakuFlag.BIG;
            slotInterface.gpif_bonus_n = 0;
        }
        else if (slotInterface.gpif_bonus_n == 2)
        {	// RB強制
            flag = Defines.ForceYakuFlag.REG;
            slotInterface.gpif_bonus_n = 0;
        }

        flag = GameManager.forceYakuValue;

        if (flag != 0)
        {// 強制役のセット
            v23.mSetForceFlag(flag);
        }
    }

    // 設定変更が必要ならば、変更する
    void chgWaveNum()
    {
        mobile.setSetUpValue(slotInterface.gpif_setting);
    }

    // コイン数が変化した時に加算、減算を行なう
    public void GPW_chgCredit(int num)
    {
        int i;

        for (i = 0; i < Math.Abs(num); i++)
        {
            if (num > 0)
            {	// 換算
                slotInterface.onCreditUp();
            }
            else if (num < 0)
            {	// 減算
                slotInterface.onCreditUse();
            }
        }
    }

    // コイン枚数の変化(ボーナス)
    public void GPW_chgCreditBonus()
    {
    }

    public int GPW_chgProba()
    {
        if (slotInterface.gpif_triple_f == true)
        {	//トリプルセブンアイテム使用時
            //(num * gpif_kakuhen_n)
            Defines.APP_TRACE("確率アップ:" + slotInterface.gpif_kakuhen_n);
            return slotInterface.gpif_kakuhen_n;
        }
        return 1;
    }

    // JACIN時に呼ばれる
    public void JacIn()
    {
        this.int_s_value[Defines.DEF_INT_BONUS_JAC_GOT] = 0;
        slotInterface.onBonusJACIN();
    }

    // ボーナスカット関係
    public int cutBonus()
    {
        // num=1 JACゲームのみ
        // num=2 ボーナス全部
        int num;

        Defines.APP_TRACE("gp.gpif_bonuscut_f:" + slotInterface.gpif_bonuscut_f);
        if (slotInterface.gpif_bonuscut_f == true)
        {	// ボーナスカットON
            // 0ボーナスカット
            // 1 OFF
            // 2 JACカット
            num = slotInterface.getOptValue(PublicDefine.OPT_BONUS_CUT);

            Defines.APP_TRACE("ボーナスカットフラグ:" + slotInterface.gpif_bonuscut_f);
            if ((this.int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_B7)
                || (this.int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_R7)) // ＢＢ終了判定
            //if ((clOHHB_V23.getWork(DfOHHB_V23_DEF.DEF_GMLVSTS) & 0x01) != 0)
            {	// ビッグボーナス中
                Defines.APP_TRACE("ここまできてる？：" + num);

                if (IS_BONUS_JAC() == true)
                {
                    Defines.APP_TRACE("ボーナスJAC中:" + num);
                    if ((num == 0) || (num == 2))
                    {	// オール指定の時のみカット
                        Defines.APP_TRACE("JACカット");
                        return 2;
                    }
                }
                else
                {
                    Defines.APP_TRACE("test");
                    if (num == 0)
                    {	// オール指定の時のみカット
                        Defines.APP_TRACE("オールカット");
                        return 1;
                    }
                }

            }
            else if (this.int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_RB_IN)
            {	// レギュラーボーナス
                if ((IS_BONUS_JAC() == true))
                {	// レギュラーボーナス中
                    Defines.APP_TRACE("レギュラーボーナス中:" + num);
                    if ((num == 0) || (num == 2))
                    {	// JAC&ｵｰﾙ指定の時にJACゲームのカット
                        Defines.APP_TRACE("レギュラーカット");
                        return 2;
                    }
                }
            }
        }
        return 0;
    }

    // ボーナスカット処理
    // 引数
    // type		0=通常,1=離席や別ユーザーの為の強制ボーナスクリア
    bool cutBonusSystem(int type)
    {
        // 0 カットなし
        // 1 ボーナスカット
        // 2 JACカット
        int cutType;

        if (type == 1)
        {	// 強制的にカットしたい場合
            cutType = 1;
        }
        else
        {	// 通常はカットフラグを見て動作させる
            cutType = cutBonus();
        }
        Defines.APP_TRACE(" ﾎﾞｰﾅｽ:" + this.int_s_value[Defines.DEF_INT_BB_KIND] + " cutType:" + cutType);

        if (cutType == 1)
        {	// ﾎﾞｰﾅｽｹﾞｰﾑ
            Defines.APP_TRACE("ボーナスオールカットだよ");
            // 遊技状態 ｽﾃｰﾀｽをRB or JAC 中へ
            v23.setWork(Defines.DEF_GMLVSTS, (ushort)1);
            // 残りJACIN可能回数（0～3）
            v23.setWork(Defines.DEF_BIGBCTR, (ushort)1);
            // JACゲーム 遊技可能回数（0～12）
            v23.setWork(Defines.DEF_JACGAME, (ushort)0);
            // ヒット役を消す
            v23.setWork(Defines.DEF_HITFLAG, (ushort)0);
            return true;
        }
        else if (cutType == 2)
        {	// JACｹﾞｰﾑ of RB
            Defines.APP_TRACE("JACカットだよ");
            // 遊技状態 ｽﾃｰﾀｽをRB or JAC 中へ
            v23.setWork(Defines.DEF_GMLVSTS, (ushort)1);
            // JACゲーム 遊技可能回数（0～12）
            v23.setWork(Defines.DEF_JACGAME, (ushort)0);
            // ヒット役を消す
            v23.setWork(Defines.DEF_HITFLAG, (ushort)0);
            return true;
        }

        return false;
    }

    bool[] eventFlagList = new bool[(int)Defines.EVENT.EVENT_NO_MAX];

    // イベントフラグセット用
    public void GPW_SET_EVENT(int n)
    {
        eventFlagList[n] = true;
        eventFlagList[(int)Defines.EVENT.EVENT_WEB] = true;
    }

    // 演出帳のチェック用
    void GPW_eventProcess(int type, int flash)
    {
        int tmp;

        switch (type)
        {
            case (int)Defines.EVENT_PROC.EVENT_PROC_CHK_REEL:
                // リールの停止位置によるチェック
                if ((v23.getWork(Defines.DEF_HITREQ) == Defines.DEF_HITFLAG_NR_BB) ||
                    (v23.getWork(Defines.DEF_HITREQ) == Defines.DEF_HITFLAG_NR_RB))
                {	// ボーナス内部中ならば
                    if (flash == (int)Defines.EVENT.EVENT_NO1)
                    {	// 3連ドン（1確)
                        tmp = ANGLE2INDEX(this.int_s_value[Defines.DEF_INT_REEL_ANGLE_R0]);
                        // 角度補正で+1されてしまうので-1する
                        if ((tmp - 1) == 9)
                        {	// 3連ドン（1確)
                            Defines.APP_TRACE("演出１：3連ドン（1確)");
                            GPW_SET_EVENT((int)Defines.EVENT.EVENT_NO1);
                        }
                    }
                    else if (flash == (int)Defines.EVENT.EVENT_NO2)
                    {	// トリプルテンパイの場合
                        Defines.APP_TRACE("演出２：トリプルテンパイ（BIG確）");
                        GPW_SET_EVENT((int)Defines.EVENT.EVENT_NO2);
                    }
                    else if (flash == (int)Defines.EVENT.EVENT_NO3)
                    {
                        // TODO ZZ.hexShortが__DEBUG__でないと使えない？
                        Defines.TRACE("★停止図柄1:" + ZZ.hexShort((short)(v23.getWork(Defines.DEF_ARAY21) & 0xFFFF)) + ":" +
                                                ZZ.hexShort((short)(v23.getWork(Defines.DEF_ARAY22) & 0xFFFF)) + ":" +
                                                ZZ.hexShort((short)(v23.getWork(Defines.DEF_ARAY23) & 0xFFFF)));
                        Defines.TRACE("★停止図柄2:" + ZZ.hexShort((short)(v23.getWork(Defines.DEF_ARAY11) & 0xFFFF)) + ":" +
                                                ZZ.hexShort((short)(v23.getWork(Defines.DEF_ARAY12) & 0xFFFF)) + ":" +
                                                ZZ.hexShort((short)(v23.getWork(Defines.DEF_ARAY13) & 0xFFFF)));
                        Defines.TRACE("★停止図柄3:" + ZZ.hexShort((short)(v23.getWork(Defines.DEF_ARAY31) & 0xFFFF)) + ":" +
                                                ZZ.hexShort((short)(v23.getWork(Defines.DEF_ARAY32) & 0xFFFF)) + ":" +
                                                ZZ.hexShort((short)(v23.getWork(Defines.DEF_ARAY33) & 0xFFFF)));

                        if (Defines.CHECK_FLAG(v23.getWork(Defines.DEF_ARAY33), Defines.DEF_BSVN) &&
                            Defines.CHECK_FLAG(v23.getWork(Defines.DEF_ARAY13), Defines.DEF_CHRY))
                        {	// 右リール下段にﾁｪﾘｰ付き青七図柄
                            if (Defines.CHECK_FLAG(v23.getWork(Defines.DEF_ARAY21), (Defines.DEF_BSVN | Defines.DEF_DON_ | Defines.DEF_BAR_)))
                            {	// 左リール上段にボーナス図柄
                                Defines.TRACE("演出３：上段タイプのゲチェナ");
                                Defines.APP_TRACE("演出３：上段タイプのゲチェナ");
                                GPW_SET_EVENT((int)Defines.EVENT.EVENT_NO3);
                            }
                            else if (Defines.CHECK_FLAG(v23.getWork(Defines.DEF_ARAY31), (Defines.DEF_BSVN | Defines.DEF_DON_ | Defines.DEF_BAR_)))
                            {	// 左リール下段にボーナス図柄
                                Defines.TRACE("演出３：上段タイプのゲチェナ");
                                Defines.APP_TRACE("演出３：下段タイプのゲチェナ");
                                GPW_SET_EVENT((int)Defines.EVENT.EVENT_NO3);
                            }
                        }
                    }
                }
                break;
            case (int)Defines.EVENT_PROC.EVENT_PROC_CHK_FLASH:
                // 演出によるチェック

                //////////////////////////////////////////////////////////////////////////////////
                // あまりタイプ
                tmp = (flash % 32);
                // 演出チェック
                if (tmp == 1)
                {
                    Defines.APP_TRACE("演出６：鉢巻リールアクション「赤ドン」３回停止");
                    //enshutu += "6,";
                    GPW_SET_EVENT((int)Defines.EVENT.EVENT_NO6);
                }
                else if (tmp == 6)
                {
                    Defines.APP_TRACE("演出７：鉢巻リールアクション「青ドン」３回停止");
                    //enshutu += "7,";
                    GPW_SET_EVENT((int)Defines.EVENT.EVENT_NO7);
                }
                else if (tmp == 23 || tmp == 29)
                {
                    Defines.APP_TRACE("演出５：レバーオン鉢巻リール始動からの「大当たり」");
                    //enshutu += "5,";
                    GPW_SET_EVENT((int)Defines.EVENT.EVENT_NO5);
                }

                //////////////////////////////////////////////////////////////////////////////////
                // 割り算タイプ
                tmp = (int)(flash / 32);
                if (tmp == 7)
                {
                    Defines.APP_TRACE("演出４：真・線香花火");
                    //enshutu += "4,";
                    GPW_SET_EVENT((int)Defines.EVENT.EVENT_NO4);
                }

                break;
            case (int)Defines.EVENT_PROC.EVENT_PROC_CHK_LANP:
                // 確定ランプフラグ
                if ((this.int_s_value[Defines.DEF_INT_WIN_LAMP] == 0) && (flash != 0))
                {	// まだ確定ランプが点等していなく、演出番号が点等命令の場合
                    Defines.APP_TRACE("演出８：「か～ぎや～」ランプ点灯");
                    GPW_SET_EVENT((int)Defines.EVENT.EVENT_NO8);
                }
                break;
            case (int)Defines.EVENT_PROC.EVENT_PROC_WEB:
                // イベント情報の送信
                if (eventFlagList[(int)Defines.EVENT.EVENT_WEB] == true)
                {
                    int i;
                    String strTmp = "";
                    // ＠＠＠
                    // グローバルの文字列い値を入れる
                    for (i = (int)Defines.EVENT.EVENT_NO1; i < (int)Defines.EVENT.EVENT_NO_MAX; i++)
                    {
                        if (eventFlagList[i] == true)
                        {
                            if (strTmp != "")
                            {
                                strTmp = strTmp + ",";
                            }
                            strTmp = strTmp + i;
                            eventFlagList[i] = false;

                        }
                    }

                    eventFlagList[(int)Defines.EVENT.EVENT_WEB] = false;

                    Defines.APP_TRACE("演出帳:" + strTmp);

                    slotInterface.userDirection = strTmp;

                    // 通信を行なう
                    // TODO C#移植 一旦コメントアウト
                    slotInterface.onReaSceneGet();
                }
                break;
        }
    }

    // 大祭り用のサーバー文字列の作成
    public String OmatsuriToString(bool intfg)
    {	// リール停止の初期値
        Defines.APP_TRACE("★リール位置の初期化★");
        this.int_s_value[Defines.DEF_INT_REEL_ANGLE_R0] = INDEX2ANGLE(9);
        this.int_s_value[Defines.DEF_INT_REEL_ANGLE_R1] = INDEX2ANGLE(16);
        this.int_s_value[Defines.DEF_INT_REEL_ANGLE_R2] = INDEX2ANGLE(14);

        // 停止フラグを立てる
        this.int_s_value[Defines.DEF_INT_REEL_STOP_R0] = this.int_s_value[Defines.DEF_INT_REEL_ANGLE_R0];
        this.int_s_value[Defines.DEF_INT_REEL_STOP_R1] = this.int_s_value[Defines.DEF_INT_REEL_ANGLE_R1];
        this.int_s_value[Defines.DEF_INT_REEL_STOP_R2] = this.int_s_value[Defines.DEF_INT_REEL_ANGLE_R2];
        this.int_s_value[Defines.DEF_INT_IS_REEL_STOPPED] = 7; // リールストップ

        return OmatsuriToString();
    }

    public String OmatsuriToString()
    {
        int i;
        String str;
        int tmp;

        str = "";

        Defines.APP_TRACE("★乱数用シードの登録★:" + clRAND8.mRndbuf.Length);
        // 乱数用シードの登録
        for (i = 0; i < Defines.RAND_SEED_SIZE; i++)
        {
            //DFMain.APP_TRACE("["+i+"]"+  (clRAND8.mRndbuf[i] & 0xFFFF) );
            str = str + GET_INT_TO_STR((clRAND8.mRndbuf[i] & 0xFFFF));

            if (i != (Defines.RAND_SEED_SIZE - 1))
            {
                str = str + ",";
            }
        }

        str = str + ",";

        Defines.APP_TRACE("★ランダムの参照先の登録★");
        str = str + GET_INT_TO_STR(clRAND8.mRndwl & 0xFFFF) + ",";
        str = str + GET_INT_TO_STR(clRAND8.mRndwh & 0xFFFF);

        str = str + ",";

        // バージョン11以上からサブのバージョンを付与
        Defines.APP_TRACE("★サウンドの再生登録★[" + this.bgm_no + "]");
        str = str + this.bgm_no + ",";
        if (this.bgm_loop == true)
        {
            tmp = 1;
        }
        else
        {
            tmp = 0;
        }
        str = str + tmp;
        /////////////////////////////////////////////

        str = str + ",";
        Defines.APP_TRACE("★グローバル変数の登録★:" + Defines.DEF_INT_SLOT_VALUE_MAX);
        for (i = 0; i < Defines.DEF_INT_SLOT_VALUE_MAX; i++)
        {
            if ((i == Defines.DEF_INT_REEL_ANGLE_R0) ||
                (i == Defines.DEF_INT_REEL_ANGLE_R1) ||
                (i == Defines.DEF_INT_REEL_ANGLE_R2))
            {	// リールの位置
                tmp = ANGLE2INDEX(this.int_s_value[i]);
                // 角度補正で+1されてしまうので-1する
                //DFMain.APP_TRACE("["+i+"]:"+(tmp-1));
                //str = str + GpHandler.PadLeft( Integer.toString((tmp-1)), 2, "0");
                str = str + GET_INT_TO_STR((tmp - 1));
            }
            else
            {
                //str = str + Integer.toString(this.int_s_value[i]);
                str = str + GET_INT_TO_STR(this.int_s_value[i]);
            }
            if (i != (Defines.DEF_INT_SLOT_VALUE_MAX - 1))
            {
                str = str + ",";
            }
            //ofset++;
        }

        //DFMain.APP_TRACE("OmatsuriToString:"+str);
        return str;
    }

    // 大祭り用のサーバー文字列の復元
    public int StringToOmatsuri(string[] appData, int top)
    {
        int i;
        int ofset = 0;
        int tmp;

        //DFMain.APP_TRACE("★乱数用シードの登録★:" + clRAND8.mRndbuf.length);
        // 乱数用シードの登録
        for (i = 0; i < Defines.RAND_SEED_SIZE; i++)
        {
            tmp = GET_STR_TO_INT(appData[top + ofset]);

            clRAND8.mRndbuf[i] = (ushort)tmp;
            //DFMain.APP_TRACE("mRndbuf復元["+i+"]=" + clRAND8.mRndbuf[i]);
            ofset++;
        }
        //DFMain.APP_TRACE("★ランダムの参照先の登録★");
        clRAND8.mRndwl = GET_STR_TO_CHAR(appData[top + ofset]); ofset++;
        clRAND8.mRndwh = GET_STR_TO_CHAR(appData[top + ofset]); ofset++;

        if (this.maj_ver >= 12 && this.sub_ver >= 0)
        {// バージョン11以上からサブのバージョンを付与
            //DFMain.APP_TRACE("★サウンドの再生登録★");
            this.bgm_no = GET_STR_TO_INT(appData[top + ofset]); ofset++;
            tmp = GET_STR_TO_INT(appData[top + ofset]); ofset++;
            if (tmp == 1)
            {
                this.bgm_loop = true;
            }
            else
            {
                this.bgm_loop = false;
            }
            if (this.bgm_no > -1) bgm_resumeFg = true;// アプリ復帰時に
        }

        Defines.APP_TRACE("★グローバル変数の登録★:" + Defines.DEF_INT_SLOT_VALUE_MAX);
        for (i = 0; i < Defines.DEF_INT_SLOT_VALUE_MAX; i++)
        {
            //DFMain.APP_TRACE("["+i+"]=" + Integer.parseInt(appData[top + ofset]));
            tmp = GET_STR_TO_INT(appData[top + ofset]);

            this.int_s_value[i] = tmp;

            if ((i == Defines.DEF_INT_REEL_ANGLE_R0) ||
                (i == Defines.DEF_INT_REEL_ANGLE_R1) ||
                (i == Defines.DEF_INT_REEL_ANGLE_R2))
            {	// リールの位置
                if (tmp != 0)
                {	// tmp=0の場合は初期値になる為、代入しない
                    //DFMain.APP_TRACE("リール位置["+i+"]:" + tmp + ":" + INDEX2ANGLE(tmp));
                    this.int_s_value[i] = INDEX2ANGLE(tmp);
                }
            }

            ofset++;
        }

        return (top + ofset);
    }

    // プレイヤーの変更時
    public void chgPrayer()
    {
        if (slotInterface.gpif_bonuscyu_f)
        {
            // ボーナス中だった場合
            Defines.TRACE("★別ユーザーのボーナス");

            if (cutBonusSystem(1))
            {	// ボーナス状態を強制クリア
                int bonusEndFg;

                Defines.TRACE("★ボーナス中を消す");
                // ボーナスを終わらせる
                bonusEndFg = v23.mBonusCounter();
                if (bonusEndFg != 0)
                {
                    Defines.TRACE("終了処理");

                    this.int_s_value[Defines.DEF_INT_CURRENT_MODE] = 0;
                    this.int_s_value[Defines.DEF_INT_REQUEST_MODE] = 0;
                    this.int_s_value[Defines.DEF_INT_WIN_COIN_NUM] = 0;
                    this.int_s_value[Defines.DEF_INT_BET_COUNT] = 0;
                    this.int_s_value[Defines.DEF_INT_BETTED_COUNT] = 0;
                    this.int_s_value[Defines.DEF_INT_NUM_KASIDASI] = 0;
                    this.int_s_value[Defines.DEF_INT_WIN_LAMP] = 0;
                    this.int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] = 0;

                    // ヒット役を消す
                    v23.setWork(Defines.DEF_HITFLAG, (ushort)0);
                    // 獲得枚数を消す
                    v23.setWork(Defines.DEF_INT_WIN_GET_COIN, (ushort)0);

                    //ここでランプを消す
                    this.int_s_value[Defines.DEF_INT_TOP_LAMP] = 0;
                    // 上部の左右ランプフラグ更新
                    ctrlTopLamp();
                    // 4THのランプフラグ更新
                    lampSwitch(Defines.DEF_LAMP_4TH, Defines.DEF_LAMP_ACTION_OFF);

                    BonusEnd(1);
                    this.int_s_value[Defines.DEF_INT_BB_TOTAL_GOT] = 0;
                }
            }
        }
        else if (IS_REPLAY())
        {
            Defines.TRACE("★リプレイを消す:");
            v23.setWork(Defines.DEF_GAMEST, (ushort)0);
            v23.setWork(Defines.DEF_HITFLAG, (ushort)0);
            lampSwitch(Defines.DEF_LAMP_FRE, Defines.DEF_LAMP_ACTION_OFF);
            this.int_s_value[Defines.DEF_INT_BETTED_COUNT] = 0;
        }
    }

    void BonusEnd(int type)
    {
        // ボーナス終了！！
        Defines.TRACE("ボーナス終了！！:" + this.int_s_value[Defines.DEF_INT_BONUS_GOT]);
        // mBonusCounter()内部でclearWork(DfOHHB_V23_DEF.DEF_CLR_AREA_2)を実行！

        // ＪＡＣ ＆ ＢＢ終了時にここを通る
        // ＢＢ・ＲＢが終了したことにするためここでフラグを初期化処理する
        this.int_s_value[Defines.DEF_INT_IS_BB_RB_END] = 1;

        // セルフオート停止フラグを立てる
        GameManager.StopAutoPlay("ボーナス終了");

        mobile.stopSound(Defines.DEF_SOUND_MULTI_BGM); // BGMを止める

        if ((this.int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_B7)
                || (this.int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_R7))
        { // ＢＢ終了判定
            Defines.TRACE("BBボーナス終了:" + this.int_s_value[Defines.DEF_INT_BONUS_GOT] + "枚:" + type);
            if (type == 1)
            {	// ボーナス状態を強制クリア

            }
            else
            {	// 通常はこっち
                _soundTime = Util.GetMilliSeconds() + Defines.DEF_SOUND_MS_09; // ﾌｧﾝﾌｧｰﾚ完奏時間設定
                playBGM(Defines.DEF_SOUND_09, false); // BBEND音

                if (BonusCutFg == true)
                {	// ボーナスカットオールの場合限定
                    BonusCutFg = false;
                    if (this.int_s_value[Defines.DEF_INT_BONUS_GOT] < Defines.BIG_BONUS_AVENUM)
                    {
                        int num;
                        num = (Defines.BIG_BONUS_AVENUM - this.int_s_value[Defines.DEF_INT_BONUS_GOT]);
                        Defines.TRACE("BBカット分を追加:" + Defines.BIG_BONUS_AVENUM + "-" + this.int_s_value[Defines.DEF_INT_BONUS_GOT] + "=" + num);

                        this.int_s_value[Defines.DEF_INT_BONUS_GOT] += num;
                        GPW_chgCredit(num);
                    }
                }

                this.int_s_value[Defines.DEF_INT_BB_TOTAL_GOT] += this.int_s_value[Defines.DEF_INT_BONUS_GOT];
            }

            // ボーナス
            slotInterface.onBonusEND();

            // 消化中の使用コイン数があるため、－枚は０枚にしておく
            this.int_s_value[Defines.DEF_INT_BONUS_GOT] = Math.Max(0, this.int_s_value[Defines.DEF_INT_BONUS_GOT]);

        }
        else if (this.int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_RB_IN)
        {
            Defines.TRACE("RBボーナス終了:" + this.int_s_value[Defines.DEF_INT_BONUS_GOT] + "枚:" + type);
            if (type == 1)
            {	// ボーナス状態を強制クリア

            }
            else
            {	// 通常はこっち
                Defines.TRACE("RBカット:" + cutBonus());
                if (BonusCutFg == true)
                {	// カット処理フラグON
                    BonusCutFg = false;
                    if (this.int_s_value[Defines.DEF_INT_BONUS_GOT] < Defines.REG_BONUS_AVENUM)
                    {
                        int num;
                        num = (Defines.REG_BONUS_AVENUM - this.int_s_value[Defines.DEF_INT_BONUS_GOT]);
                        Defines.TRACE("RBカット分を追加:" + Defines.REG_BONUS_AVENUM + "-" + this.int_s_value[Defines.DEF_INT_BONUS_GOT] + "=" + num);
                        this.int_s_value[Defines.DEF_INT_BONUS_GOT] += num;
                        GPW_chgCredit(num);
                    }
                }
            }
            // ボーナス
            slotInterface.onBonusEND();
            // 消化中の使用コイン数があるため、－枚は０枚にしておく
            this.int_s_value[Defines.DEF_INT_BONUS_GOT] = Math.Max(0, this.int_s_value[Defines.DEF_INT_BONUS_GOT]);
        }
    }
}

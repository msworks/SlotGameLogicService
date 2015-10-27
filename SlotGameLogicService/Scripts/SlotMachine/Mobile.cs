using UnityEngine;
using System.Collections;
using System.Text;

/*
 * 作成日: 2003/08/25
 * 更新： 2006/02/28
 */

//#include "DfMain.h"
//import docomo_DF905.*;

/**
 * スロット以外の親クラス 主にメニューを担当する 起動ﾓｰﾄﾞ等各種設定等を管理します。
 * 
 * @author A05229Ak
 */
//#include "DfImport.h"
//import java.io.*;
//import java.util.*;
//import javax.microedition.io.*;
//import com.nttdocomo.ui.*;
//import com.nttdocomo.io.*;
//import com.nttdocomo.device.*;
//import com.nttdocomo.opt.ui.*;
//import com.nttdocomo.opt.ui.j3d.*;

/*
 * メニューの背景が黒なので、ヘルプの絵文字を一部変更。
 */
public class Mobile {

    public const bool DEF_IS_DOCOMO = true; // TODO C#移植 DOCOMO準拠と仮定

    // モード初期化フラグ
    private static bool initModeFlag = false;

    // キー
    // satoh
    //	private static int keyTrigger = 0;
    public static int keyTrigger = 0;

    private static int keyPressing = 0;

    private static int keyPressingCount = 0;

    private static string spHallParam = null;

    /**
     * Ｚ（エントリポイントのある）クラスから呼ばれるメソッドです。 このクラスが呼ばれるときは、リソースはすでに読み込まれているものとします。
     * 旧型のMiniSlot.j (Canvasクラス)内のdoPaint()メソッドを抜き出したものです。
     * 
     * ここに,モバイルアプリとして各モードにおける振る舞いを記述します。
     * 
     * @see #int_m_value
     * @see df.Df#INT_MODE_CURRENT
     * @see df.Df#INT_MODE_REQUEST
     */

    //	static Canvas maincv = null;
    //	GpHandler proc = null;
    /*	
        public Canvas getCanvas() {
            return maincv;
        }
	
        int gpHandlerIRQProcess;
        public int V_switchProcess( ) throws Exception
        {
            return 0;
        }

        public void initGp( ) {
            proc = new GpHandler();
        }
    */
    public mOmatsuri mo = new mOmatsuri();
    public static SlotInterface gp = null;

    public void SetKeyTrigger(int key) {
        keyTrigger = key;
    }
    public void SetKeyPressing(int key) {
        keyPressing = key;
    }

    public void exec() {
        if (gp == null) {
            Defines.TRACE("gpがないよ");
            gp = new SlotInterface(); // TODO C#移植 ここでGP作ってみる
        }
        if (mOmatsuri.gp == null) {
            Defines.TRACE("gpの登録");
            mOmatsuri.gp = gp;
#if	__DEBUG_MENU__
			Debug.mo = mo;
			Debug.gp = gp;
#endif
        }

        initModeFlag = false;
        // キー取得
        keyTrigger = ZZ.getKeyPressed();
        keyPressing = ZZ.getKeyPressing();
        //Debug.Log("keyTrigger:" + keyTrigger);
        //Debug.Log("keyPressing:" + keyPressing);
#if __ERR_MSG__
		if( ZZ.errCode != 0)
		{	// エラーコードがあれば
			keyTrigger = 0;
		}
#endif

        if (keyPressing == 0) {
            keyPressingCount = 0;
        } else {
            keyPressingCount++;
        }
        // モード切り替えチェック
        if (int_m_value[Defines.DEF_INT_MODE_CURRENT] != int_m_value[Defines.DEF_INT_MODE_REQUEST]) {
            int_m_value[Defines.DEF_INT_MODE_CURRENT] = int_m_value[Defines.DEF_INT_MODE_REQUEST];
            int_m_value[Defines.DEF_INT_COUNTER] = 0;
            initModeFlag = true;

            // TODO 
            //// 黒で全体クリア
            //ZZ.setColor(ZZ.getColor(0, 0, 0));
            //ZZ.fillRect(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ.getHeight());
            //// 音を鳴らすモードは初期化フェーズで設定
            //// 音量を0にする
            //ZZ.setVolume(0, DEF_SOUND_MULTI_BGM);
            //ZZ.setVolume(0, DEF_SOUND_MULTI_SE);
        }
        // satoh
        //		DfMain.TRACE("case="+int_m_value[Defines.DEF_INT_MODE_CURRENT]);
        //		DfMain.TRACE("case2="+(int_m_value[Defines.DEF_INT_MODE_CURRENT] & DEF_MODE_MENUS_BITS));

        // モードごとに処理分岐
        switch (int_m_value[Defines.DEF_INT_MODE_CURRENT]) {
            case Defines.DEF_MODE_UNDEF:
                // スクラッチパッドアクセス

                //gp.outSavaData();
                //gp.inSavaData();
                if (!loadMenuData()) {
                    initConfig();
                    saveMenuData(false);//初期はホールPは保存しない
                    if (DEF_IS_DOCOMO) {
                        // satoh
                        //					setMode(DEF_MODE_HALL_NOTICE);
                        break;
                    }
                }
#if __COM_TYPE__
                setMode(Defines.DEF_MODE_TITLE);
#else
#endif
                break;
#if __COM_TYPE__
            /* タイトル */
            case Defines.DEF_MODE_TITLE:
                // satoh
                //			mo.int_s_value[Defines.DEF_INT_SLOT_COIN_NUM] = gp.param_hall.usr_coin;
                //gp.gpLocal2Gp();
                //			DfMain.TRACE( "設定値=" + gp.m_iSetting );
                ctrlTitle();
                break;
            /* ゲーム中 */
            case Defines.DEF_MODE_RUN:
                ctrlRun();
                break;
#else
#endif
        }
        // //////////////////////////////////////
        // 共通の処理。
#if __COM_TYPE__
#else
//		int_m_value[Defines.DEF_INT_COUNTER]++;
#endif

#if __ERR_MSG__
		if( ZZ.errCode != 0)
		{	// エラーコードがあれば
			ZZ.drawErrMsg();
		}
#endif
    }

    //	G7Network param = new G7Network(); 

    private void ctrlRun() {

        /* 初期化ブロック */
        if (initModeFlag) {
            // ソフトキー設定
            //			//ZZ.addSoftKey(DEF_SOFT_KEY_MENU);// MENU
            //			// ゲームの初期化
            //			mOmatsuri.newSlot();
#if __SON_VOLUME__
#else
			// 音量を設定
            ZZ.setVolume(volume, Defines.DEF_SOUND_MULTI_BGM);
            ZZ.setVolume(volume, Defines.DEF_SOUND_MULTI_SE);
#endif
            // 再描画要求
            //mo.restartSlot();
        }
#if __COM_TYPE__
        if (mo.process(keyTrigger)) {
            mOmatsuri.getExitReason();
        }
#if __DRAW_SLOT2__
        //		if(gp.gpif_flash_f == true)
        //		{
        //			//DfMain.TRACE("★★★★gpif_flash_f★★★★");
        //			//drawAll();
        //			gp.gpif_flash_f = false;
        //		}
        //		else
        //		{
        //			DfMain.TRACE("★★★★paintSlot★★★★");
        //			//mo.restartSlot(); // スロット画面描画
        //		}

        mo.restartSlot();
        // 4TH_REEL
        int pos = (mOmatsuri.int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] % 414) * (2359296 / 414);
        // TODO C#移植 描画処理？
        //Mascot3D.draw3Dtest(pos);
#if __DEBUG__
        ZZ.dbgDrawAll();
#endif
        //mo.paintSlot(); // スロット画面描画
#else
		mo.paintSlot(); // スロット画面描画
#endif
#else

#endif
    }

    //satoh
    private void ctrlTitle() {
        // siomoto
        /* 初期化ブロック */
        //		if (initModeFlag) {
        //			// ソフトキー設定
        ////			//ZZ.addSoftKey(DEF_SOFT_KEY_MENU);// MENU
        //			// ゲームの初期化
        //			mo.newSlot();
        //			// カーソル初期値
        //			int_m_value[Defines.DEF_INT_GMODE] = DEF_GMODE_GAME;
        //			int_m_value[Defines.DEF_INT_SETUP_VALUE_CURSOL] = 4;
        //		}

        // satoh
        {
            //int_m_value[Defines.DEF_INT_SETUP_VALUE] = gp.m_iSetting;
            setSetUpValue(gp.gpif_setting);
            // 分析モード
            int_m_value[Defines.DEF_INT_GMODE] = Defines.DEF_GMODE_SIMURATION;
            mo.newSlot();
            setMode(Defines.DEF_MODE_RUN);// ゲームを走らす
        }

    }

    /** 広告文座標X */
    static int message_x = 240;// TODO const

    /** 広告文座標 dX */
    static readonly int message_d = ZZ.getFontHeight() / 4;

#if __DRAW_MENU_ICON__
	// //////////////////////////////////////////////////////////////
	// アイコン表示
	public static void drawIcon(bool isTitle) {
		int dy = 0;
		if (isTitle) {
			dy = 0;
		}
		ZZ.setClip(DEF_POS_MENU_ICONE_GMODE_X - 1, DEF_POS_MENU_ICONE_GMODE_Y - 1
				+ dy, DEF_POS_MENU_ICONE_MEOSHI_X + DEF_POS_MENU_ICONE_MEOSHI_W
				+ 2, DEF_POS_MENU_ICONE_MEOSHI_H + 2);
		// ゲーム状態を表示します
		if (int_m_value[Defines.DEF_INT_GMODE] == DEF_GMODE_HALL) {
			ZZ.setColor(ZZ.getColor(0x5b,0x5b,0x5b));
		}else{
			ZZ.setColor(ZZ.getColor(DEF_POS_DEFAULT_COLOR_1_R,
					DEF_POS_DEFAULT_COLOR_1_G, DEF_POS_DEFAULT_COLOR_1_B));
		}
		ZZ.fillRect(DEF_POS_MENU_ICONE_GMODE_X - 1, DEF_POS_MENU_ICONE_GMODE_Y - 1
				+ dy, DEF_POS_MENU_ICONE_GMODE_W + 2,
				DEF_POS_MENU_ICONE_GMODE_H + 2);
		

		if (int_m_value[Defines.DEF_INT_GMODE] == DEF_GMODE_GAME) {
			if(ZZ.getAuthResult() == 0){
				ZZ.drawImage(DEF_RES_MENU_ICON_DOL, DEF_POS_MENU_ICONE_GMODE_X,
						DEF_POS_MENU_ICONE_GMODE_Y + dy);
			}else{
				ZZ.drawImage(DEF_RES_MENU_ICON_TRY, DEF_POS_MENU_ICONE_GMODE_X,
						DEF_POS_MENU_ICONE_GMODE_Y + dy);
			}
		} else if (int_m_value[Defines.DEF_INT_GMODE] == DEF_GMODE_SIMURATION) {
			ZZ.drawImage(DEF_RES_MENU_ICON_SIM, DEF_POS_MENU_ICONE_GMODE_X,
					DEF_POS_MENU_ICONE_GMODE_Y + dy);
		} else if (int_m_value[Defines.DEF_INT_GMODE] == DEF_GMODE_HALL) {
			ZZ.drawImage(DEF_RES_MENU_ICON_HALL, DEF_POS_MENU_ICONE_GMODE_X,
					DEF_POS_MENU_ICONE_GMODE_Y + dy);
		}

		// 目押しON／Offを表示します
		if (isMeoshi()) {
			ZZ.fillRect(DEF_POS_MENU_ICONE_MEOSHI_X - 1,
					DEF_POS_MENU_ICONE_MEOSHI_Y - 1 + dy,
					DEF_POS_MENU_ICONE_MEOSHI_W + 2,
					DEF_POS_MENU_ICONE_MEOSHI_H + 2);
			ZZ.drawImage(DEF_RES_MENU_ICON_EYE, DEF_POS_MENU_ICONE_MEOSHI_X,
					DEF_POS_MENU_ICONE_MEOSHI_Y + dy);
		}
		if (int_m_value[Defines.DEF_INT_GMODE] == DEF_GMODE_HALL) {
			// サウンドON／OFFを表示します
			ZZ.fillRect(DEF_POS_MENU_ICONE_SOUND_X - 1, DEF_POS_MENU_ICONE_SOUND_Y - 1
					+ dy, DEF_POS_MENU_ICONE_SOUND_W + 2,
					DEF_POS_MENU_ICONE_SOUND_H + 2);
			ZZ.drawImage(DEF_RES_MENU_ICON_HALL_SOU, DEF_POS_MENU_ICONE_SOUND_X,
					DEF_POS_MENU_ICONE_SOUND_Y + dy);
			if (int_m_value[Defines.DEF_INT_IS_SOUND] == 0) {
				ZZ.drawImage(DEF_RES_MENU_ICON_HALL_SOU_OFF, DEF_POS_MENU_ICONE_MUTE_X,
						DEF_POS_MENU_ICONE_MUTE_Y + dy);
			}
		}else{
			// サウンドON／OFFを表示します
			ZZ.fillRect(DEF_POS_MENU_ICONE_SOUND_X - 1, DEF_POS_MENU_ICONE_SOUND_Y - 1
					+ dy, DEF_POS_MENU_ICONE_SOUND_W + 2,
					DEF_POS_MENU_ICONE_SOUND_H + 2);
			ZZ.drawImage(DEF_RES_MENU_ICON_SOU, DEF_POS_MENU_ICONE_SOUND_X,
					DEF_POS_MENU_ICONE_SOUND_Y + dy);
			if (int_m_value[Defines.DEF_INT_IS_SOUND] == 0) {
				ZZ.drawImage(DEF_RES_MENU_ICON_SOU_OFF, DEF_POS_MENU_ICONE_MUTE_X,
						DEF_POS_MENU_ICONE_MUTE_Y + dy);
			}
		}

	}
#endif

    /** Mobile内で使うint配列 */
    public static readonly int[] int_m_value = new int[Defines.DEF_INT_M_VALUE_MAX];

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

    // ///////////////////////////////////////////////////////////////////////
    // アクセサメソッド
    // ///////////////////////////////////////////////////////////////////////
    /**
     * 目押しサポートあり？<BR>
     * ﾒﾆｭｰで変更されたﾌﾗｸﾞを渡す<BR>
     * 
     * @return true:あり false:なし
     */
    public static bool isMeoshi() {

#if __COM_TYPE__
        // グリパチではモードがない為
        return gp.l_m_bEyeSupport;
#else
//		if (int_m_value[Defines.DEF_INT_GMODE] == DEF_GMODE_SIMURATION) {
//			return int_m_value[Defines.DEF_INT_IS_MEOSHI] == Defines.DEF_SELECT_2_ON;
//		}else{
//			return false;
//		}
#endif
    }
#if __COM_TYPE__

#else
//	/**
//	 * データパネル表示？<BR>
//	 * ﾒﾆｭｰで変更されたﾌﾗｸﾞを渡す<BR>
//	 * 
//	 * @return true:表示 false:非表示
//	 */
//	public static bool isDataPanel() {
//		return int_m_value[Defines.DEF_INT_IS_DATAPANEL] == Defines.DEF_SELECT_2_ON;
//	}
//
//	/**
//	 * バイブ<BR>
//	 */
//	public static bool isVaib() {
//		return int_m_value[Defines.DEF_INT_IS_VIBRATION] == Defines.DEF_SELECT_2_ON;
//	}
#endif
    /**
	 * Menuボタンの動作可否を設定する<BR>
	 * スロットクラスで使用する。RMODE_BETでfalse,RMODE_WAITでtrue<BR>
	 * 
	 * @param flag
	 *            true:可動 false:非可動
	 */
    public static void setMenuAvarable(bool flag) {
        int_m_value[Defines.DEF_INT_IS_MENU_AVAILABLE] = (flag) ? Defines.DEF_MENU_AVAILABLE
                : Defines.DEF_MENU_UNAVAILABLE;
        if (flag) {
            //ZZ.addSoftKey(Defines.DEF_SOFT_KEY1, Defines.DEF_SOFT_KEY_MENU);// MENU
        } else {
            //ZZ.addSoftKey(Defines.DEF_SOFT_KEY1, Defines.DEF_SOFT_KEY_NONE);// なし
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
    public static bool isJacCut() {
#if __COM_TYPE__
        // グリパチではモードがない為
        // すべてのボーナスカット時とする
        if (mOmatsuri.cutBonus() != 0) {
            return true;
        }
        //		if( gp.getOptValue(gp.OPT_BONUS_CUT) == 1)
        //		{
        //			return true;
        //		}
        return false;
#else
        if (int_m_value[Defines.DEF_INT_GMODE] == Defines.DEF_GMODE_HALL) {
			return false;
		}else{
            return int_m_value[Defines.DEF_INT_IS_JACCUT] == Defines.DEF_SELECT_2_ON;
		}
#endif
    }

    /**
     * 設定値を設定する<BR>
     * 
     * @return 設定値0~5
     */
    public static void setSetUpValue(int val) {

        int_m_value[Defines.DEF_INT_SETUP_VALUE] = val;
        // 内部設定の変更(Z80関係はこっちかな？)
        clOHHB_V23.setWork(Defines.DEF_WAVENUM, (ushort)val);
    }

    /**
     * 設定値を取得する<BR>
     * ﾀｲﾄﾙから決定キー押下時に設定されるのでMobileで管理します。<BR>
     * 
     * @return 設定値0~5
     */
    public static int getSetUpValue() {
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
    public static int getGameMode() {
        return int_m_value[Defines.DEF_INT_GMODE];
    }
#if __COM_TYPE__
    // 元のはもう使わない
#else
	/**
	 * 押し順の定義 並びはサブメニューの並びで
	 */
	private static readonly int[,] _order = { { 0, 1, 2 }, { 0, 2, 1 },
			{ 1, 0, 2 }, { 1, 2, 0 }, { 2, 0, 1 }, { 2, 1, 0 } };

	/**
	 * 停止ボタンの場所を返す.
	 * 
	 * @param num
	 *            第？停止か
	 * @return
	 */
	public static int getOrder(int num) {
//		for (int i = 0; i < 3; i++) {
//			if (num == _order[int_m_value[Defines.DEF_INT_ORDER]][i]) {
//				return i;
//			}
//		}
//		// ここにはこないけど
//		return num;
        return _order[int_m_value[Defines.DEF_INT_ORDER]][num];
	}
#endif
    /**
	 * 告知の状態を返す
	 * 
	 * @return
	 */
    public static int getKokuchi() {
        return int_m_value[Defines.DEF_INT_KOKUCHI];
    }

    /**
     * 初期化ブロックです、 ロードは既に終わっているはずなのでタイトルモードから開始するようにアプリモードを初期化。
     */
    static Mobile() {

        int_m_value[Defines.DEF_INT_MODE_REQUEST] = Defines.DEF_MODE_UNDEF;
        int_m_value[Defines.DEF_INT_MODE_CURRENT] = Defines.DEF_MODE_UNDEF;
#if __COM_TYPE__
        // GPでは下詰めで描画する為
        // センター
        int_m_value[Defines.DEF_INT_BASE_OFFSET_X] = (ZZ.getWidth() - Defines.DEF_POS_WIDTH);
        // センター
        int_m_value[Defines.DEF_INT_BASE_OFFSET_Y] = (ZZ.getHeight() - Defines.DEF_POS_HEIGHT);
#else
		// センター
		int_m_value[Defines.DEF_INT_BASE_OFFSET_X] = (ZZ.getWidth() - Defines.DEF_POS_WIDTH) / 2;
		// センター
		int_m_value[Defines.DEF_INT_BASE_OFFSET_Y] = (ZZ.getHeight() - Defines.DEF_POS_HEIGHT) / 2;
#endif
        ZZ.setOrigin(int_m_value[Defines.DEF_INT_BASE_OFFSET_X], int_m_value[Defines.DEF_INT_BASE_OFFSET_Y]);

        int_m_value[Defines.DEF_INT_TITLE_BG_START] = ZZ.getBitRandom(32);

        // 設定初期値
        int_m_value[Defines.DEF_INT_GMODE] = Defines.DEF_GMODE_GAME;
        int_m_value[Defines.DEF_INT_SETUP_VALUE_CURSOL] = 3;// 設定４
        //int_m_value[Defines.DEF_INT_SETUP_VALUE] = 3;// 設定４
        setSetUpValue(3);	// 設定４
        int_m_value[Defines.DEF_INT_SUB_MENU_ITEM] = -1; // 選択メニューアイテムの初期化
        int_m_value[Defines.DEF_INT_IS_SOUND] = 1;// 音鳴るよ
        initConfig();
    }

    private static void initConfig() {
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

    public static readonly int SAVE_BUFFER = Defines.DEF_SAVE_SIZE - 2; // アクセス関数の都合上-2しないとこける

    // ///////////////////////////////////////////////////////////////////////
    // メニューデータの管理
    // ///////////////////////////////////////////////////////////////////////
    /**
     * メニューデータの書き込み
     */
    public static void saveMenuData(bool isHall)
    {
        if (!isHall) {
            mOmatsuri.prevHttpTime = 0;
            mOmatsuri.kasidasiMedal = 0;
        }

        sbyte[] buf = new sbyte[SAVE_BUFFER];
        int len;

        len = ZZ.getRecord(ref buf);

        if (len <= 0) {
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

#if __COM_TYPE__
#else
		string hp = new string(new sbyte[DEF_HS_MAX]);
		if(isHall){
			hp = mOmatsuri.getHallParam();
		}
		System.arraycopy(hp.getBytes(),0,buf,Defines.DEF_SAVE_HALL_PARAM,hp.getBytes().length);
#endif
        buf[Defines.DEF_SAVE_WRITTEN] = 1;
        ZZ.setRecord(buf);

    }

    /**
     * メニューデータの読込
     * 
     * @return
     */
    public static bool loadMenuData()
    {
        var buf = new sbyte[SAVE_BUFFER];
        var len = 0;

        len = ZZ.getRecord(ref buf);

        if (len <= 0) {
            return false;
        }
        // まだデータが無いとき
        if (buf[Defines.DEF_SAVE_WRITTEN] == 0) {
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

        // TODO C#移植 要確認(hall要る？) => spHallParamはorg内のソースで参照されていないのでコメントアウト
        //spHallParam = new string(Encoding.Default.GetString((byte[])buf)).ToCharArray(), Defines.DEF_SAVE_HALL_PARAM, Defines.DEF_HS_MAX);
        //if (spHallParam == (new string(new char[Defines.DEF_HS_MAX]))) {
        //    spHallParam = null;
        //}

        //setVolume(int_m_value[Defines.DEF_INT_VOLUME]);

        return true;
    }

    /**
     * アプリモードアクセッサ
     * 
     * @param a
     *            カレントモード
     * @return ノーマルモード
     */
    private static int getNormalMode(int a)
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
    private static int getMenuMode(int a) {
        return Defines.DEF_MODE_MENU_BIT | getNormalMode(a);
    }

    /**
     * アプリのイベントモード切替指示
     * 
     * @param m
     *            変更要求するアプリモード
     */
    private static void setMode(int m) {
        int_m_value[Defines.DEF_INT_MODE_REQUEST] = m;
    }

    // //////////////////////////////////////////////////////////////
    // タイトル

    // //////////////////////////////////////////////////////////////
    // メニュー

    // /**
    // * メニュー項目(表示画像の順番)を定義
    // *
    // */
    // private static final int[] menuImages = { DEF_RES_ME_L_01, // ヘルプ
    // DEF_RES_ME_L_09, // メダル精算
    // DEF_RES_ME_L_04, // サウンド設定
    // DEF_RES_ME_L_02, // リールスピード
    // DEF_RES_ME_L_03, // ボーナス目押し ＯＦＦ
    // DEF_RES_ME_L_02, // 押し順
    // DEF_RES_ME_L_02, // 小役告知
    // DEF_RES_ME_L_02, // JACカット
    // DEF_RES_ME_L_08, // 公式サイト
    // DEF_RES_ME_L_05, // データパネル ＯＮ
    // DEF_RES_ME_L_06, // ゲームデータ
    // DEF_RES_ME_L_07, // 終了
    // };

    /**
     * 起動モードによってメニューが違うのでココで定義 モード初期化で変更 MENU_IDは問題なければMENUの画像IDにしようかな？
     */
    private static readonly int[][] menuDefine = {
	// ﾀｲﾄﾙからﾒﾆｭｰ
			new int[]{ Defines.DEF_MENU_ID_CONFIG,// ゲーム設定
// satoh
//					Defines.DEF_MENU_ID_WEB,// 機種攻略
					Defines.DEF_MENU_ID_HELP,// ヘルプ
					Defines.DEF_MENU_ID_EXIT,// 終了
			},
			// 実践ﾓｰﾄﾞﾒﾆｭｰ
			new int[]{ Defines.DEF_MENU_ID_CONFIG,// ゲーム設定
					Defines.DEF_MENU_ID_INFO,// ゲームデータ
// satoh
//					Defines.DEF_MENU_ID_CAL,// 情報送信
//					Defines.DEF_MENU_ID_WEB,// 機種攻略
					Defines.DEF_MENU_ID_HELP,// ヘルプ
					Defines.DEF_MENU_ID_TITLE,// タイトルへ戻る
					Defines.DEF_MENU_ID_EXIT,// 終了
			},
			// 分析ﾓｰﾄﾞ
			new int[]{ Defines.DEF_MENU_ID_CONFIG,// ゲーム設定
					Defines.DEF_MENU_ID_INFO,// ゲームデータ
// satoh
//					Defines.DEF_MENU_ID_WEB,// 機種攻略
					Defines.DEF_MENU_ID_HELP,// ヘルプ
					Defines.DEF_MENU_ID_TITLE,// タイトルへ戻る
					Defines.DEF_MENU_ID_EXIT,// 終了
			},
			// ﾎｰﾙﾓｰﾄﾞ
			new int[]{ Defines.DEF_MENU_ID_CONFIG,// ゲーム設定
					Defines.DEF_MENU_ID_INFO,// ゲームデータ
//					Defines.DEF_MENU_ID_WEB,// 機種攻略
					Defines.DEF_MENU_ID_HELP,// ヘルプ
//					Defines.DEF_MENU_ID_TITLE,// タイトルへ戻る
					Defines.DEF_MENU_ID_EXIT,// 終了
			},
			};

    /**
     * 起動モードによってメニューが違うのでココで定義 モード初期化で変更 MENU_IDは問題なければMENUの画像IDにしようかな？
     */
    private static readonly int[][] configDefine = {
			// ﾀｲﾄﾙからﾒﾆｭｰ
			new int[]{ Defines.DEF_MENU_ID_VOLUME,// 音量設定
					Defines.DEF_MENU_ID_SPEED,// リール速度
					Defines.DEF_MENU_ID_JACCUT,// JACｶｯﾄ
					Defines.DEF_MENU_ID_DATAPANEL,// データパネル
					Defines.DEF_MENU_ID_VAIB,//バイブ
					Defines.DEF_MENU_ID_ORDER,// 押し順
					Defines.DEF_MENU_ID_INIT,// 設定初期化
			},
			// 実践ﾓｰﾄﾞﾒﾆｭｰ
			new int[]{ Defines.DEF_MENU_ID_VOLUME,// 音量設定
					Defines.DEF_MENU_ID_SPEED,// リール速度
					Defines.DEF_MENU_ID_JACCUT,// JACｶｯﾄ
					Defines.DEF_MENU_ID_DATAPANEL,// データパネル
					Defines.DEF_MENU_ID_VAIB,//バイブ
					Defines.DEF_MENU_ID_ORDER,// 押し順
					Defines.DEF_MENU_ID_INIT,// 設定初期化
			},
			// 分析ﾓｰﾄﾞ
			new int[]{ Defines.DEF_MENU_ID_VOLUME,// 音量設定
					Defines.DEF_MENU_ID_SPEED,// リール速度
					Defines.DEF_MENU_ID_MEOSHI,// ボーナス目押し
					Defines.DEF_MENU_ID_JACCUT,// JACｶｯﾄ
					Defines.DEF_MENU_ID_DATAPANEL,// データパネル
					Defines.DEF_MENU_ID_KOKUCHI,// 小役告知
					Defines.DEF_MENU_ID_VAIB,//バイブ
					Defines.DEF_MENU_ID_ORDER,// 押し順
					Defines.DEF_MENU_ID_INIT,// 設定初期化
			},
			// ﾎｰﾙﾓｰﾄﾞ
			new int[]{ Defines.DEF_MENU_ID_VOLUME,// 音量設定
					Defines.DEF_MENU_ID_SPEED,// リール速度
					Defines.DEF_MENU_ID_DATAPANEL,// データパネル
					Defines.DEF_MENU_ID_VAIB,//バイブ
					Defines.DEF_MENU_ID_ORDER,// 押し順
					Defines.DEF_MENU_ID_INIT,// 設定初期化
			},
			};

    /**
     * 現在のモードを入れる場所
     */
    private static int[] selectedMenu;
#if __COM_TYPE__

#else
//	/**
//	 * 精算画面クリア＆背景画像する。
//	 */
//	private static void clearDisp() {
//		ZZ.setClip(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ.getHeight());
//		ZZ.setColor(ZZ.getColor(Defines.DEF_MENU_BG_R, Defines.DEF_MENU_BG_G,Defines.DEF_MENU_BG_B));
//		ZZ.fillRect(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ.getHeight());
//		ZZ.drawImage(DEF_RES_MENU_BG, 0, 240 - 62);
//		ZZ.drawImage(DEF_RES_MENU_BG, 120, 240 - 62);
//	}
//
//	/**
//	 * ｹﾞｰﾑ情報表示。
//	 * 
//	 * @param coin
//	 *            コイン数
//	 */
//	private void drawInfo(final int coin) {
//		clearDisp();
//		mo.drawDataPanel(false);
//		ZZ.setClip(0, 0, 240, 240);
//		int y0 = DEF_POS_NAVI_BOT_Y + DEF_POS_NAVI_BOT_H;
//		ZZ.setColor(ZZ.getColor(DEF_TXT_R,DEF_TXT_G, DEF_TXT_B));
//		ZZ.drawstring("所持枚数", 60, y0 += HELP_string_H);
//		drawstringR((coin + mOmatsuri.getKasidasiValue()) + "", 180, y0);
//		ZZ.drawstring("貸出枚数", 60, y0 += HELP_string_H);
//		drawstringR(mOmatsuri.getKasidasiValue() + "", 180, y0);
//		ZZ.fillRect(0, y0 += ZZ.getFontHeight() / 6, DEF_POS_WIDTH, 1);
//		ZZ.drawstring("獲得枚数", 60, y0 += HELP_string_H);
//		drawstringR(coin + "", 180, y0);
//
//		if(int_m_value[Defines.DEF_INT_GMODE] == DEF_GMODE_SIMURATION){
//			ZZ.drawstringCenter("●設定 " + (getSetUpValue() + 1)//5行目
//					+ "でした●", y0 += HELP_string_H);
//		}else{
//			y0 += HELP_string_H;
//		}
//		
//		int[] param = mOmatsuri.getSendParams();
//		ZZ.drawstring("山獲得率", 4, y0 += HELP_string_H);
//		drawstringR(getPercent(param[DEF_PARAM_WMLN_GOT], param[DEF_PARAM_WMLN_HIT]) + "%", 116, y0);
//		ZZ.drawstring("ﾁｪﾘｰ獲得率", 124, y0);
//		drawstringR(getPercent(param[DEF_PARAM_CHRY_GOT], param[DEF_PARAM_CHRY_HIT]) + "%", 236, y0);
//		ZZ.drawstring("BB平均獲得", 4, y0 += HELP_string_H);
//		drawstringR(getAve(param[DEF_PARAM_BB_TOTAL_COIN], param[DEF_PARAM_BB_COUNT]) + "枚", 116, y0);
//		ZZ.drawstring("ﾌﾗｸﾞ間平均", 124, y0);
//		drawstringR(getAve(param[DEF_PARAM_FLAG_GAME], param[DEF_PARAM_BB_COUNT]+param[DEF_PARAM_RB_COUNT]) + "G", 236, y0);
//		ZZ.drawstring("ﾋﾞﾀ外し", 4, y0 += HELP_string_H);
//		drawstringR(param[DEF_PARAM_HAZUSHI] + "回", 116, y0);
//		ZZ.drawstring("機械割", 124, y0);
//		drawstringR(getPercent(param[DEF_PARAM_TOTAL_OUT], param[DEF_PARAM_TOTAL_IN]) + "%",236, y0);
//	}
#endif
    private static void drawstringR(string str, int rx, int y) {
        ZZ.drawstring(str, rx - ZZ.stringWidth(str), y);
    }

    private static string getAve(int bunsi, int bunbo) {
        string res = "";
        if (bunbo != 0) {
            int val = bunsi * 1000 / bunbo;
            res += (val / 1000) + "." + shosu2(val % 1000);
            return res;
        } else {
            return "--.--";
        }
    }

    private static string shosu2(int sho) {
        string res = "";
        if (sho < 100) {
            res += "0";
        } else {
            res += "";
        }
        if (sho % 10 < 5) {
            return res += sho / 10;
        } else {
            return res += (sho / 10) + 1;
        }
    }

    private static string getPercent(int bunsi, int bunbo) {
        return getAve(bunsi * 100, bunbo);
    }
#if __COM_TYPE__
#else
//	/**
//	 * タイトル戻る確認画面 チェック●
//	 */
//	private static void drawPrevTitle() {
//		clearDisp();
//		int y0 = HELP_string_H;//1行目
//		ZZ.setColor(ZZ.getColor(DEF_TITLE_TXT_R,DEF_TITLE_TXT_G, DEF_TITLE_TXT_B));
//		ZZ.drawstringCenter("●ﾀｲﾄﾙへ戻る●", y0 += HELP_string_H);//2行目
//		y0 += HELP_string_H;//3行目
//		ZZ.setColor(ZZ.getColor(DEF_TXT_R,DEF_TXT_G, DEF_TXT_B));
//		ZZ.drawstringCenter("ﾀｲﾄﾙへ戻ります。", y0 += HELP_string_H);//4行目
//		ZZ.drawstringCenter("よろしいですか？", y0 += HELP_string_H);//5行目
//		ZZ.drawstringCenter("※現在のｹﾞｰﾑ内容は破棄されます。", y0 += HELP_string_H);//6行目
//		y0 += HELP_string_H;//7行目
//		y0 += HELP_string_H;//8行目
//		y0 += HELP_string_H;//9行目
//		y0 += HELP_string_H;//10行目
//		y0 += HELP_string_H;//11行目
//		y0 += HELP_string_H;//12行目
//		y0 += HELP_string_H;//13行目
//		if (int_m_value[Defines.DEF_INT_DIALOG_CURSOL] == DEF_CURSOL_UP) {
//			ZZ.setColor(ZZ.getColor(DEF_SELECT_BG_R,DEF_SELECT_BG_G, DEF_SELECT_BG_B));
//			ZZ.fillRect(HELP_WINDOW_X, y0 + 2, HELP_WINDOW_W, HELP_string_H);
//			ZZ.setColor(ZZ.getColor(DEF_TXT_R,DEF_TXT_G, DEF_TXT_B));
//		}else{
//			ZZ.setColor(ZZ.getColor(DEF_UNSELECT_TXT_R,DEF_UNSELECT_TXT_G, DEF_UNSELECT_TXT_B));
//		}
//		ZZ.drawstringCenter("はい", y0 += HELP_string_H);//14行目
//		if (int_m_value[Defines.DEF_INT_DIALOG_CURSOL] == DEF_CURSOL_DOWN) {
//			ZZ.setColor(ZZ.getColor(DEF_SELECT_BG_R,DEF_SELECT_BG_G, DEF_SELECT_BG_B));
//			ZZ.fillRect(HELP_WINDOW_X, y0 + 2, HELP_WINDOW_W, HELP_string_H);
//			ZZ.setColor(ZZ.getColor(DEF_TXT_R,DEF_TXT_G, DEF_TXT_B));
//		}else{
//			ZZ.setColor(ZZ.getColor(DEF_UNSELECT_TXT_R,DEF_UNSELECT_TXT_G, DEF_UNSELECT_TXT_B));
//		}
//		ZZ.drawstringCenter("いいえ", y0 += HELP_string_H);//15行目
//	}
//
//	/**
//	 * 初期化確認画面 チェック●
//	 */
//	private static void drawPrevInit() {
//		clearDisp();
//		int y0 = HELP_string_H;//1行目
//		ZZ.setColor(ZZ.getColor(DEF_TITLE_TXT_R,DEF_TITLE_TXT_G, DEF_TITLE_TXT_B));
//		ZZ.drawstringCenter("●設定初期化●", y0 += HELP_string_H);//2行目
//		y0 += HELP_string_H;//3行目
//		ZZ.setColor(ZZ.getColor(DEF_TXT_R,DEF_TXT_G, DEF_TXT_B));
//		ZZ.drawstringCenter("ｹﾞｰﾑ設定の各種設定を", y0 += HELP_string_H);//4行目
//		ZZ.drawstringCenter("ｱﾌﾟﾘの初期設定状態に", y0 += HELP_string_H);//5行目
//		ZZ.drawstringCenter("戻しますか?", y0 += HELP_string_H);//6行目
//		y0 += HELP_string_H;//7行目
//		y0 += HELP_string_H;//8行目
//		y0 += HELP_string_H;//9行目
//		y0 += HELP_string_H;//10行目
//		y0 += HELP_string_H;//11行目
//		y0 += HELP_string_H;//12行目
//		y0 += HELP_string_H;//13行目
//		if (int_m_value[Defines.DEF_INT_DIALOG_CURSOL] == DEF_CURSOL_UP) {
//			ZZ.setColor(ZZ.getColor(DEF_SELECT_BG_R,DEF_SELECT_BG_G, DEF_SELECT_BG_B));
//			ZZ.fillRect(HELP_WINDOW_X, y0 + 2, HELP_WINDOW_W, HELP_string_H);
//			ZZ.setColor(ZZ.getColor(DEF_TXT_R,DEF_TXT_G, DEF_TXT_B));
//		}else{
//			ZZ.setColor(ZZ.getColor(DEF_UNSELECT_TXT_R,DEF_UNSELECT_TXT_G, DEF_UNSELECT_TXT_B));
//		}
//		ZZ.drawstringCenter("ｹﾞｰﾑ設定の初期化", y0 += HELP_string_H);//14行目
//		if (int_m_value[Defines.DEF_INT_DIALOG_CURSOL] == DEF_CURSOL_DOWN) {
//			ZZ.setColor(ZZ.getColor(DEF_SELECT_BG_R,DEF_SELECT_BG_G, DEF_SELECT_BG_B));
//			ZZ.fillRect(HELP_WINDOW_X, y0 + 2, HELP_WINDOW_W, HELP_string_H);
//			ZZ.setColor(ZZ.getColor(DEF_TXT_R,DEF_TXT_G, DEF_TXT_B));
//		}else{
//			ZZ.setColor(ZZ.getColor(DEF_UNSELECT_TXT_R,DEF_UNSELECT_TXT_G, DEF_UNSELECT_TXT_B));
//		}
//		ZZ.drawstringCenter("初期化せずに戻る", y0 += HELP_string_H);//15行目
//	}
//	/**
//	 * 終了確認画面 チェック●
//	 */
//	private static void drawPrevExit() {
//		clearDisp();
//		int y0 = HELP_string_H;//1行目
//		ZZ.setColor(ZZ.getColor(DEF_TITLE_TXT_R,DEF_TITLE_TXT_G, DEF_TITLE_TXT_B));
//		ZZ.drawstringCenter("●終了●", y0 += HELP_string_H);//2行目
//		y0 += HELP_string_H;//3行目
//		ZZ.setColor(ZZ.getColor(DEF_TXT_R,DEF_TXT_G, DEF_TXT_B));
//		if(int_m_value[Defines.DEF_INT_GMODE] == DEF_GMODE_HALL){
//			ZZ.drawstringCenter("ﾌﾟﾚｲ中のﾒﾀﾞﾙを精算して", y0 += HELP_string_H);//4行目
//		}
//		ZZ.drawstringCenter("ｱﾌﾟﾘを終了します。", y0 += HELP_string_H);//4行目
//		ZZ.drawstringCenter("よろしいですか？", y0 += HELP_string_H);//5行目
//		y0 += HELP_string_H;//6行目
//		y0 += HELP_string_H;//7行目
//		y0 += HELP_string_H;//8行目
//		y0 += HELP_string_H;//9行目
//		y0 += HELP_string_H;//10行目
//		y0 += HELP_string_H;//11行目
//		y0 += HELP_string_H;//12行目
//		if(int_m_value[Defines.DEF_INT_GMODE] != DEF_GMODE_HALL){
//			y0 += HELP_string_H;//13行目
//		}
//		if (int_m_value[Defines.DEF_INT_DIALOG_CURSOL] == DEF_CURSOL_UP) {
//			ZZ.setColor(ZZ.getColor(DEF_SELECT_BG_R,DEF_SELECT_BG_G, DEF_SELECT_BG_B));
//			ZZ.fillRect(HELP_WINDOW_X, y0 + 2, HELP_WINDOW_W, HELP_string_H);
//			ZZ.setColor(ZZ.getColor(DEF_TXT_R,DEF_TXT_G, DEF_TXT_B));
//		}else{
//			ZZ.setColor(ZZ.getColor(DEF_UNSELECT_TXT_R,DEF_UNSELECT_TXT_G, DEF_UNSELECT_TXT_B));
//		}
//		ZZ.drawstringCenter("はい", y0 += HELP_string_H);//14行目
//		if (int_m_value[Defines.DEF_INT_DIALOG_CURSOL] == DEF_CURSOL_DOWN) {
//			ZZ.setColor(ZZ.getColor(DEF_SELECT_BG_R,DEF_SELECT_BG_G, DEF_SELECT_BG_B));
//			ZZ.fillRect(HELP_WINDOW_X, y0 + 2, HELP_WINDOW_W, HELP_string_H);
//			ZZ.setColor(ZZ.getColor(DEF_TXT_R,DEF_TXT_G, DEF_TXT_B));
//		}else{
//			ZZ.setColor(ZZ.getColor(DEF_UNSELECT_TXT_R,DEF_UNSELECT_TXT_G, DEF_UNSELECT_TXT_B));
//		}
//		ZZ.drawstringCenter("いいえ", y0 += HELP_string_H);//15行目
//	}
//
//	/**
//	 * Web確認画面
//	 * 
//	 */
//	private static void drawPrevWeb() {
//		clearDisp();
//		int y0 = HELP_string_H;	//1行目
//		ZZ.setColor(ZZ.getColor(DEF_TITLE_TXT_R,DEF_TITLE_TXT_G, DEF_TITLE_TXT_B));
//		ZZ.drawstringCenter("●機種攻略●", y0 += HELP_string_H);//2行目
//		y0 += HELP_string_H;//3行目
//		ZZ.setColor(ZZ.getColor(DEF_TXT_R,DEF_TXT_G, DEF_TXT_B));
//		ZZ.drawstringCenter("機種攻略ﾍﾟｰｼﾞへｱｸｾｽします。", y0 += HELP_string_H);//4行目
//		ZZ.drawstringCenter("よろしいですか？", y0 += HELP_string_H);//5行目
//		ZZ.drawstringCenter("※現在のｹﾞｰﾑ内容は破棄されます。", y0 += HELP_string_H);//6行目
//		y0 += HELP_string_H;//7行目
//		y0 += HELP_string_H;//8行目
//		y0 += HELP_string_H;//9行目
//		y0 += HELP_string_H;//10行目
//		y0 += HELP_string_H;//11行目
//		y0 += HELP_string_H;//12行目
//		y0 += HELP_string_H;//13行目
//		if (int_m_value[Defines.DEF_INT_DIALOG_CURSOL] == DEF_CURSOL_UP) {
//			ZZ.setColor(ZZ.getColor(DEF_SELECT_BG_R,DEF_SELECT_BG_G, DEF_SELECT_BG_B));
//			ZZ.fillRect(HELP_WINDOW_X, y0 + 2, HELP_WINDOW_W, HELP_string_H);
//			ZZ.setColor(ZZ.getColor(DEF_TXT_R,DEF_TXT_G, DEF_TXT_B));
//		}else{
//			ZZ.setColor(ZZ.getColor(DEF_UNSELECT_TXT_R,DEF_UNSELECT_TXT_G, DEF_UNSELECT_TXT_B));
//		}
//		ZZ.drawstringCenter("公式ｻｲﾄへｱｸｾｽ", y0 += HELP_string_H);//14行目
//
//		if (int_m_value[Defines.DEF_INT_DIALOG_CURSOL] == DEF_CURSOL_DOWN) {
//			ZZ.setColor(ZZ.getColor(DEF_SELECT_BG_R,DEF_SELECT_BG_G, DEF_SELECT_BG_B));
//			ZZ.fillRect(HELP_WINDOW_X, y0 + 2, HELP_WINDOW_W, HELP_string_H);
//			ZZ.setColor(ZZ.getColor(DEF_TXT_R,DEF_TXT_G, DEF_TXT_B));
//		}else{
//			ZZ.setColor(ZZ.getColor(DEF_UNSELECT_TXT_R,DEF_UNSELECT_TXT_G, DEF_UNSELECT_TXT_B));
//		}
//		ZZ.drawstringCenter("ﾒﾆｭｰへ戻る", y0 += HELP_string_H);//15行目
//	}
#endif
    // //////////////////////////////////////////////////////////////
    // ゲームインフォ

    /**
     * スロットによって出す情報が違うので順番に依存します（汗）
     */
    private static readonly int[] infoGameData = { 65536, 65536, 65536, // NULLはだめ
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
    public static void stopSound(int mode) {
        if (Defines.DEF_USE_MULTI_SOUND) {
            if (mode == Defines.DEF_SOUND_UNDEF) {
                ZZ.stopSound(Defines.DEF_SOUND_MULTI_BGM);
                ZZ.stopSound(Defines.DEF_SOUND_MULTI_SE);
                mOmatsuri.bgm_no = -1;
                mOmatsuri.bgm_loop = false;
            } else {
                ZZ.stopSound(mode);
                if (Defines.DEF_SOUND_MULTI_BGM == mode) {
                    mOmatsuri.bgm_no = -1;
                    mOmatsuri.bgm_loop = false;
                }
            }
        } else {
            ZZ.stopSound();
        }
    }

    /**
     * 再生
     * 
     * @param id
     *            サウンドID
     * @param isRepeat
     *            繰り返し演奏するかどうか
     * @param mode
     *            サウンドモード
     * @see Df#SOUND_MULTI_SE
     * @see Df#SOUND_MULTI_BGM
     */
    public static void playSound(int id, bool isRepeat, int mode) {
        if (Defines.DEF_USE_MULTI_SOUND) {
            ZZ.playSound(id, isRepeat, mode);
        } else {
            ZZ.playSound(id, isRepeat);
        }
    }
#if __SON_VOLUME__
#else
	static int volume;

	/**
	 * ボリューム設定する
	 * 
	 * @param val
	 *            はサブメニューのインデックスなので音量(%)に変換
	 */
	public static void setVolume(int val) {
		if (val == 0) {
			if (int_m_value[Defines.DEF_INT_IS_SOUND] == 1) {
				int_m_value[Defines.DEF_INT_IS_SOUND] = 0;// Z.changeSound() ? 1 : 0;
			}
		} else {
			if (int_m_value[Defines.DEF_INT_IS_SOUND] == 0) {
				int_m_value[Defines.DEF_INT_IS_SOUND] = 1;// Z.changeSound() ? 1 : 0;
			}
		}
		// サブメニューの並びによって変化します
		volume = int_m_value[Defines.DEF_INT_VOLUME] * 25 / 10;
		if (volume >= 100) {
			volume = 99;
		}
		if (volume <= 0) {
			volume = 0;
		}
	}
#endif
    /**
	 * リールスピードを取得します
	 */
    public static int getReelSpeed() {
        // サブメニューの並びによって変化します
        //return (int_m_value[Defines.DEF_INT_SPEED] - 20) * 3 + 100;
        return (Defines.GP_DEF_INT_SPEED - 20) * 3 + 100;
    }

}
//using UnityEngine;
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
public class Mobile
{
    public mOmatsuri mo = new mOmatsuri();
    public static SlotInterface gp = null;
    public const bool DEF_IS_DOCOMO = true; // TODO C#移植 DOCOMO準拠と仮定
    public static int keyTrigger = 0;

    private static bool initModeFlag = false;    // モード初期化フラグ
    private static int keyPressing = 0;
    private static int keyPressingCount = 0;

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
        if (gp == null)
        {
            Defines.TRACE("gpがないよ");
            gp = new SlotInterface(); // TODO C#移植 ここでGP作ってみる
        }

        if (mOmatsuri.gp == null)
        {
            Defines.TRACE("gpの登録");
            mOmatsuri.gp = gp;
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

        }

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
        if (mo.process(keyTrigger))
        {
            mOmatsuri.getExitReason();
        }
        mo.restartSlot();
        // 4TH_REEL
        int pos = (mOmatsuri.int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] % 414) * (2359296 / 414);
        ZZ.dbgDrawAll();
    }

    private void ctrlTitle()
    {
        {
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

    /**
     * 目押しサポートあり？<BR>
     * ﾒﾆｭｰで変更されたﾌﾗｸﾞを渡す<BR>
     * 
     * @return true:あり false:なし
     */
    public static bool isMeoshi()
    {
        // グリパチではモードがない為
        return gp.l_m_bEyeSupport;
    }

    /**
	 * Menuボタンの動作可否を設定する<BR>
	 * スロットクラスで使用する。RMODE_BETでfalse,RMODE_WAITでtrue<BR>
	 * 
	 * @param flag
	 *            true:可動 false:非可動
	 */
    public static void setMenuAvarable(bool flag)
    {
        int_m_value[Defines.DEF_INT_IS_MENU_AVAILABLE] = (flag) ? Defines.DEF_MENU_AVAILABLE
                : Defines.DEF_MENU_UNAVAILABLE;
        if (flag)
        {
        } else {
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
    public static bool isJacCut()
    {
        // グリパチではモードがない為
        // すべてのボーナスカット時とする
        if (mOmatsuri.cutBonus() != 0) {
            return true;
        }
        return false;
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
    public static int getGameMode()
    {
        return int_m_value[Defines.DEF_INT_GMODE];
    }

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
    static Mobile()
    {
        int_m_value[Defines.DEF_INT_MODE_REQUEST] = Defines.DEF_MODE_UNDEF;
        int_m_value[Defines.DEF_INT_MODE_CURRENT] = Defines.DEF_MODE_UNDEF;

        // GPでは下詰めで描画する為
        // センター
        int_m_value[Defines.DEF_INT_BASE_OFFSET_X] = (ZZ.getWidth() - Defines.DEF_POS_WIDTH);
        // センター
        int_m_value[Defines.DEF_INT_BASE_OFFSET_Y] = (ZZ.getHeight() - Defines.DEF_POS_HEIGHT);

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

    /**
     * 起動モードによってメニューが違うのでココで定義 モード初期化で変更 MENU_IDは問題なければMENUの画像IDにしようかな？
     */
    private static readonly int[][] menuDefine = {
	// ﾀｲﾄﾙからﾒﾆｭｰ
			new int[]{ Defines.DEF_MENU_ID_CONFIG,// ゲーム設定
					Defines.DEF_MENU_ID_HELP,// ヘルプ
					Defines.DEF_MENU_ID_EXIT,// 終了
			},
			// 実践ﾓｰﾄﾞﾒﾆｭｰ
			new int[]{ Defines.DEF_MENU_ID_CONFIG,// ゲーム設定
					Defines.DEF_MENU_ID_INFO,// ゲームデータ
					Defines.DEF_MENU_ID_HELP,// ヘルプ
					Defines.DEF_MENU_ID_TITLE,// タイトルへ戻る
					Defines.DEF_MENU_ID_EXIT,// 終了
			},
			// 分析ﾓｰﾄﾞ
			new int[]{ Defines.DEF_MENU_ID_CONFIG,// ゲーム設定
					Defines.DEF_MENU_ID_INFO,// ゲームデータ
					Defines.DEF_MENU_ID_HELP,// ヘルプ
					Defines.DEF_MENU_ID_TITLE,// タイトルへ戻る
					Defines.DEF_MENU_ID_EXIT,// 終了
			},
			// ﾎｰﾙﾓｰﾄﾞ
			new int[]{ Defines.DEF_MENU_ID_CONFIG,// ゲーム設定
					Defines.DEF_MENU_ID_INFO,// ゲームデータ
					Defines.DEF_MENU_ID_HELP,// ヘルプ
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
    public static void stopSound(int mode)
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

    public static void playSound(int id, bool isRepeat, int mode)
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
    public static int getReelSpeed()
    {
        return (Defines.GP_DEF_INT_SPEED - 20) * 3 + 100;
    }

}
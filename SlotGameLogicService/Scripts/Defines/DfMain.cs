#define __COM_TYPE__	// 可変かけた場所

#define __COM_TYPE2__	// 可変かけた場所

//=============================================================
// リリース時には基本的にOFFにする
#define __DEBUG__	// デバッグ用
//#define __DRAW_DATA_PANEL2__	// 元々あったスランプグラフの描画(有効で描画)
//#define __DRAW_MENU_ICON__	// 元々あったメニューアイコンの描画(有効で描画)
//#define __TRY_FALSE__	// メイン関数全体のtryを無効化
//=============================================================
// リリース時には基本的にONにする
#define __BONUS_CUT__	// ボーナスゲームカット(ゲーム数スキップ版)
#define __DRAW_SLOT2__	// 描画のタイミングを変更
#define __FPS_TYPE2__	//FPSの調整を行なった
#define __CLIP_FALSE__	// 出来るだけsetClip処理を外す
#define __SON_VOLUME__	// サウンドのボリューム変更の整理
//=============================================================

#region __DEBUG__
#if	__DEBUG__
#define __GP_TRACE__	// GP側のトレース情報

//	#define __TRACE__	// トレース出力

//	#define __DEBUG_MENU__	// デバッグメニュー
//	#define __DEBUG_DRAW_YAKU__	// デバッグ用 内部当選役の描画

//#define __DEBUG_APP_REFRESH__	// app_dataの削除

//	#define __ERR_MSG__	// エラーメッセージ描画用

//#define __AUTO_STOCK__ //オート補充

// #define __OHANA_DEBUG__	// 元の大花用デバッグ

//#define _SOUND_OFF_			// サウンド再生OFF　＠＠＠ソフトバンクのサウンドがまだ

//#define __DRAW_CREDIT_UP__	// クレジットアップ描画
//#define __REEL_WEIT_SKIP__	// リールウェイトの無効化

//	#define __REEL_ID_CHECK__	// リールIDとRAM内のデータチェック
#if	_DOCOMO	// {
#else
//	#define __CHANGE_FPS__	//FPSのリアルタイム変更
#endif
//	#define __DRAW_REEL_WAIT__ // リールウェイト時間の描画

#endif
#endregion

using System;
using UnityEngine;

/*__
 * Created on 2003/07/11
 */
public partial class Defines {

    ///////////////////////////////////////////////
    //
    //	トレース関係
    //
    ///////////////////////////////////////////////
    //#define __TRACE_PROCESS__	// プロセス名のトレース
    //#define __RAM_TRACE__	// RAMチェック用トレース
    //#define __APP_TRACE__	// RAMチェック用トレース

    public static void TRACE(object x) {
#if __TRACE__
	    ZZ.TRACE1(x)
#else
        //TRACE( x );
        //Debug.Log(DateTime.Now.ToString("HH:mm:ss.fff")+ ":DebugTrace:" + x.ToString());
#endif
    }

    public static void RAM_TRACE(object x) {
#if __RAM_TRACE__
	    ZZ.TRACE1( x )
#else
        //RAM_TRACE( x );
        //Debug.Log(DateTime.Now.ToString("HH:mm:ss.fff") + ":DebugRamTrace:" + x.ToString());
#endif
    }

    public static void APP_TRACE(object x) {
#if __APP_TRACE__
	    ZZ.TRACE1( x )
#else
        //APP_TRACE( x );
        //Debug.Log(DateTime.Now.ToString("HH:mm:ss.fff") + ":DebugAppTrace:" + x.ToString());
    }
#endif


    ////////////////////////////////////////////////////////////////////////////////////////////////

    //#define CHECK_FLAG( flg, value )					( (flg & (value)) != 0 )
    public static bool CHECK_FLAG(int flg, int value) {
        return ((flg & (value)) != 0);
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////
#if __ERR_MSG__
    public static void SET_ERR_CODE(int x)      {ZZ.errCode=x;}
	public static void SET_ERR_OPTION(int x)	{ZZ.errOption1=x;}
	public static void SET_ERR_OPTION2(int x)	{ZZ.errOption2=x;}
	public static void SET_ERR_OPTION3(int x)	{ZZ.errOption3=x;}

	// エラーコード
	public const int ERR_CODE_JAC_CUT = 1000;	    // jacカット分の追加時
	public static reaconly int ERR_CODE_BONUS_CUT = 1001;	// ボーナスカット時
	public static reaconly int ERR_CODE_PAY_UP = 1002;	    // 払い出し時 通常
	public static reaconly int ERR_CODE_PAY_UP2 = 1003;	    // 払い出し時 ｶｯﾄ
	public static reaconly int ERR_CODE_CREDIT_UP = 1004;	// 加算時のMath.abs
	public static reaconly int ERR_CODE_PAY_UP_RAM = 1005;	// 払い出し時 RAM
#endif

#if	__DEBUG__
    enum DBG {	//debug_cnf[]
        DBG_DRWF,		//00 描画ON/OFF
        DBG_CURSORX,	//   ページ番号
        DBG_CURSORY,	//   行番号
        //		DBG_DRWF2,		//01 デバッグモードのサブメニュー
        //		DBG_MODE,		//02 モード
        DBG_YAKUN,		//03 デバッグ役番号
        DBG_YAKV,		//04 デバッグ役
        DBG_YAKV_LOCK,	//04 デバッグ役
        //		DBG_YAKUN_BNS,	//05 デバッグ役同時当選役番号
        //		DBG_YAKV_BNS,	//05 デバッグ役同時当選役
        //		DBG_NSMOD,		//06 次のサブモード
        //		DBG_ANNO,		//07 デバッグ用アニメーション番号
        //		DBG_SCNO,		//08 強制演出番号
        DBG_MANUAL,		//09 マニュアルリール
        //		DBG_FLY,		//10 強制飛翔ゾーン当選
        //		DBG_FREEZE,		//11 強制フリーズ当選
        //		DBG_KEY,		//12 図柄点滅用

        //		DBG_ARTNUM,		//13 ART抽選の固定

        //		DBG_FRX_SC,		// ﾌﾘｰｽﾞｽｸﾘﾌﾟﾄ番号
        //		DBG_REEL_SC,	// ｱﾆﾒﾘｰﾙｽｸﾘﾌﾟﾄ番号
        //		DBG_CUT_SC,		// ｶｯﾄｽｸﾘﾌﾟﾄ番号

        //		DBG_LOAD_TEST,	// ロード成功
        //		DBG_LOAD_TEST2,	// ロード成功

        DBG_AUTO_TYPE,	// オートプレイのタイプ(0=なし,1=ﾉｰﾏﾙ,2=ﾉﾝｽﾄｯﾌﾟ,3=達人)
        DBG_CUT_BONUS_TYPE,	// ボーナスカットのタイプ(0=なし,1=JACのみ,2=全部)
        DBG_VOLUME,		// サウンドボリューム
        DBG_SETTING,	// 設定の変更
        DBG_KAKUHEN,	// 確率変更
        DBG_OSIJUN,		// 押し順
        DBG_FLASH,		// 演出適応
        DBG_FLASH0,		// フラッシュ演出番号
        DBG_FLASH1,		// リール演出番号
        DBG_APP_REFRESH,		// リール演出番号
        DBG_MAX
    };
    // デバッグモードのON/OFF
    enum DBG_MODE {
        DBG_MODE_OFF,
        DBG_MODE_ON,
        DBG_MODE_EXIT
    };
#endif
    ////////////////////////////////////////////////////////////////////////////////////////////////

#if	_DOCOMO		//{
#else				//}{
    //	ソフトバンクのセーブデータの保存名
    public const string STR_APP_NAME = "az004004";
#endif				//}
    // グリパチ用に画面全体をずらす為用
    // + GP_DRAW_OFFSET_Y
    public const int GP_DRAW_OFFSET_Y = -15;

    public const int LOT_YAKU_CHRY = 0x01;
    public const int LOT_YAKU_BELL = 0x02;	// 三尺玉
    public const int LOT_YAKU_WMLN = 0x04;	// 山
    public const int LOT_YAKU_REP = 0x08;
    public const int LOT_YAKU_RB = 0x10;
    public const int LOT_YAKU_BB = 0x20;

    // メニュー設定の代り
    public const int GP_DEF_INT_SPEED = 20;

    // 格納サイズ
    public const int SVR_DATA_MAJ_ERSION = 12; // サーバー用バージョンデータ
    public const int SVR_DATA_SUB_VERSION = 0; // サーバー用バージョンデータ


    public const int SVR_DATA_SIZE = 2; // バージョン情報

    // RAM関係
    public const int RAM_SIZE = (Defines.DEF_WORKEND + 1); // clZ80RAM.mWorkRamの配列サイズ
    public const int REG_SIZE = (6 * 2); // レジストリデータ
    public const int SYSTEM_SIZE = ((RAM_SIZE) + (REG_SIZE));
    // ランド関係(子役抽選の復帰の為)
    public const int RAND_SEED_SIZE = (256); // mRndbuf用
    public const int RAND_SEED_INDEX_SIZE = (2); // mRndbufの参照先
    public const int RAND_SEED_MAX_SIZE = (RAND_SEED_SIZE + RAND_SEED_INDEX_SIZE);
    // 状態復帰用
    public const int SOUND_DATA_SIZE = 2; // サウンドデータ(no,loop)
    public const int APP_WORK_SIZE = (SOUND_DATA_SIZE + Defines.DEF_INT_SLOT_VALUE_MAX);
    // GPフレームワーク用
    public const int GP_APP_DATA_SIZE = 30; // GPデータ用
    public const int GP_APP_DATA_SIZE_M = 6; // GPデータ用

    // サーバーと送受信を行なうアプリデータ
    public const int APP_SERVER_DATA_SIZE = (SVR_DATA_SIZE + SYSTEM_SIZE + RAND_SEED_MAX_SIZE + APP_WORK_SIZE + GP_APP_DATA_SIZE + GP_APP_DATA_SIZE_M);


    // ボーナス時のカット枚数
    public const int BIG_BONUS_AVENUM = (584); //平均獲得枚数
    public const int REG_BONUS_AVENUM = (127); //平均獲得枚数
    public const int JAC_BONUS_AVENUM = (112); //平均獲得枚数

    // 演出関係
    public enum EVENT_PROC {	// 演出チェックのタイプ
        EVENT_PROC_CHK_REEL,	// リールの停止位置によるチェック
        EVENT_PROC_CHK_FLASH,	// 演出によるチェック
        EVENT_PROC_CHK_LANP,	// 確定ランプチェック
        EVENT_PROC_WEB			// イベント情報の送信
    };
    public enum EVENT {
        EVENT_WEB,	// 通信許可フラグ
        EVENT_NO1,	// 3連ドン（1確)
        EVENT_NO2,	// トリプルテンパイ（BIG確）
        EVENT_NO3,	// ゲチェナ
        EVENT_NO4,	// 真・線香花火
        EVENT_NO5,	// レバーオン鉢巻リール始動からの「大当たり」
        EVENT_NO6,	// 鉢巻リールアクション「赤ドン」3回停止
        EVENT_NO7,	// 鉢巻リールアクション「青ドン」3回停止
        EVENT_NO8,	// 「か～ぎや～」ランプ点灯

        EVENT_NO_MAX	// 

    };

    ////////////////////////////////////////////////////////////////////////////////////////////////

    //#include "df\DfKey.h"

    //#include "df\DfZDebug.h"

    //#include "df\DfRes.h"

    //#include "df\DfPos_240x240.h"

    //#if	_DOCOMO	// {
    //    #include "df\DfRes_240x240im.h"
    //    #include "df\DfCarrier_im.h"
    //#else			// } {
    //    #include "df\DfRes_240x240vo.h"
    //    #include "df\DfCarrier_vo.h"
    //#endif			// }

    //#include "df\DfZ.h"

    //#include "df\DfOmatsuri_v4.h"
    //#include "df\DfKey.h"
    //#include "df\DfDebug.h"
    //#include "df\DfSound.h"
    //#include "df\DfMobile.h"
    //#include "df\DfMenu.h"
    //#include "df\DfUnicode.h"
    //#include "df\DfBinaryDigit.h"
    //#include "df\DfDirector.h"
    //#include "df\DfOHHB_V23_DEF.h"
    //#include "df\DfSendParam.h"
    //#include "df\DfHallParam.h"

    //#include "df\DfSlotState.h"
    //#include "df\DfSoundFull.h"

}


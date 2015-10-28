using System;

public partial class Defines
{
    public static void TRACE(object x)
    {
        Console.WriteLine(x);
    }

    public static void RAM_TRACE(object x)
    {
    }

    public static void APP_TRACE(object x)
    {
    }

    public static bool CHECK_FLAG(int flg, int value)
    {
        return ((flg & (value)) != 0);
    }

    enum DBG {
        DBG_DRWF,		//00 描画ON/OFF
        DBG_CURSORX,	//   ページ番号
        DBG_CURSORY,	//   行番号
        DBG_YAKUN,		//03 デバッグ役番号
        DBG_YAKV,		//04 デバッグ役
        DBG_YAKV_LOCK,	//04 デバッグ役
        DBG_MANUAL,		//09 マニュアルリール
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
    public enum EVENT_PROC
    {
        EVENT_PROC_CHK_REEL,	// リールの停止位置によるチェック
        EVENT_PROC_CHK_FLASH,	// 演出によるチェック
        EVENT_PROC_CHK_LANP,	// 確定ランプチェック
        EVENT_PROC_WEB			// イベント情報の送信
    };

    public enum EVENT
    {
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
}


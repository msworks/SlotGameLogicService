using System;
//using UnityEngine;
public partial class ZZ{

#if __REEL_ID_CHECK__
	public static int drawReel[] = new int[3];
	public static string drawReelStr;
	
	public static void checkReel()
	{
		int i;
		int defAray = DEF_ARAY11;
		
		int reel;
		int work;
		
		//	図柄コード
		//#define DEF_BSVN         	     0x01                // 青７
		//#define DEF_DON_         	     0x02                // ﾄﾞﾝ
		//#define DEF_BAR_         	     0x04                // BAR
		//#define DEF_RPLY         	     0x08                // ｾﾝｽ
		//#define DEF_WMLN         	     0x10                // 大山
		//#define DEF_BELL         	     0x20                // ﾊﾅﾋﾞ
		//#define DEF_CHRY         	     0x40                // ﾁｪﾘｰ
		//#define DEF_ANY_         	     0x00                // －
		
		drawReelStr = "";
		for(i = 0; i < 3; i++)
		{
			reel = mOmatsuri.REELTB[i][drawReel[i]];
			work = (clOHHB_V23.getWork(defAray+i) & 0xFFFF);
			
			drawReelStr = drawReelStr + "re:" + reel + " wo:" + work + "/";
			
			if( (reel != work) && (work != 0) )
			{
				drawReelStr = drawReelStr + "アウト！！";
				TRACE("リールの値がおかしいよ！:" + i + ":0x" + ZZ.hexInt(reel) + ":0x" + ZZ.hexInt(work));
			}
		}
		

		
	}
	
	public static void dbgReelMsg()
	{
#if	_DOCOMO	// {
#else			// } {
		ZZ.grp.setFont(Font.getFont( Font.FACE_SYSTEM, Font.STYLE_PLAIN, Font.SIZE_SMALL ) );
#endif			// }
		//ZZ.setColor(ZZ.getColor(0x00,0x00,0x00));
		//ZZ.fillRect(0 - ZZ.ofX, 0 - ZZ.ofY, 50, 20);
		
		ZZ.setColor(ZZ.getColor(0xFF,0xFF,0xFF));
		ZZ.drawstring("" + drawReelStr, 0, 220);
		
		int i;
		string tmp = "";
		for (i = 0; i < 6; i++) {
			tmp = tmp + mOmatsuri.getLampStatus(DEF_LAMP_S1 + i) + ":";
		}
		
		tmp = tmp + mOmatsuri.int_s_value[DEF_INT_LAMP_1] + "/" + mOmatsuri.int_s_value[DEF_INT_LAMP_2] + "/" + mOmatsuri.int_s_value[DEF_INT_LAMP_3];
		
		tmp = tmp + "/" + mOmatsuri.int_s_value[DEF_INT_TOP_LAMP] + "/" + mOmatsuri.int_s_value[DEF_INT_SEQUENCE_EFFECT];
		
		ZZ.drawstring("" + tmp, 0, 200);
	}


#endif
#if __ERR_MSG__
	  
	public static int errCode=0;
	public static int errOption1=0;
	public static string errOption2="";
	public static string errOption3="";
	
	public static void drawErrMsg()
	{
		//ZZ.grp.setFont(Font.getFont( Font.FACE_SYSTEM, Font.STYLE_PLAIN, Font.SIZE_SMALL ) );
		
		ZZ.setColor(ZZ.getColor(0x00,0x00,0x00));
		ZZ.fillRect(0, 0, 240, 240);
	
		ZZ.setColor(ZZ.getColor(0xFF,0xFF,0xFF));
		ZZ.drawstring("ｴﾗｰｺｰﾄﾞ:" + errCode, 10, 100);
		ZZ.drawstring("1:" + errOption1, 10, 125);
		ZZ.drawstring("2:" + errOption2, 10, 150);
		ZZ.drawstring("3:" + errOption3, 10, 175);
	}
#endif


#if __CHANGE_FPS__
	public static int appSecLog;
	
	public static void dbgFpsProc(int keyTrigger,int kePressing)
	{
		int ofset = 0;
		if( CHECK_FLAG(keyTrigger, DEF_KEY_BIT_UP) )
		{
			ofset = -1;
		}
		else if( CHECK_FLAG(keyTrigger, DEF_KEY_BIT_DOWN) )
		{
			ofset = 1;
		}
		else if( CHECK_FLAG(keyTrigger, DEF_KEY_BIT_LEFT) )
		{
			ofset = -10;
		}
		else if( CHECK_FLAG(keyTrigger, DEF_KEY_BIT_RIGHT) )
		{
			ofset = 10;
		}
		
		ZZ.appSec += ofset;
		ZZ.appSec = ZZ.dbg_Range(ZZ.appSec, 0, 500);
	}
	public static void dbgFpsuDraw()
	{
#if	_DOCOMO	// {
#else			// } {
		ZZ.grp.setFont(Font.getFont( Font.FACE_SYSTEM, Font.STYLE_PLAIN, Font.SIZE_SMALL ) );
#endif			// }
		
		//ZZ.setColor(ZZ.getColor(0x00,0x00,0x00));
		//ZZ.fillRect(0 - ZZ.ofX, 0 - ZZ.ofY, 50, 20);
	
		ZZ.setColor(ZZ.getColor(0xFF,0xFF,0xFF));
		ZZ.drawstring("FPS:" + ZZ.appSec + ":" + (appSecLog), 0, 0 );
		//System.out.println("ofX:"+ ZZ.ofX + " ofY:" +ZZ.ofY);
	}
#endif

#if __DRAW_CREDIT_UP__
	public static int cregitUpLog;
	
	public static void dbgCreditUp()
	{
#if	_DOCOMO	// {
#else			// } {
		ZZ.grp.setFont(Font.getFont( Font.FACE_SYSTEM, Font.STYLE_PLAIN, Font.SIZE_SMALL ) );
#endif			// }
		//ZZ.setColor(ZZ.getColor(0x00,0x00,0x00));
		//ZZ.fillRect(0 - ZZ.ofX, 0 - ZZ.ofY, 50, 20);
		
		ZZ.setColor(ZZ.getColor(0xFF,0xFF,0xFF));
		ZZ.drawstring("cUp:" + ZZ.cregitUpLog, 0, 220);
		//System.out.println("ofX:"+ ZZ.ofX + " ofY:" +ZZ.ofY);
	}
#endif

#if __DRAW_REEL_WAIT__
	public static int reelWaitLog;
	
	public static void dbgReelWait()
	{
#if	_DOCOMO	// {
#else			// } {
		ZZ.grp.setFont(Font.getFont( Font.FACE_SYSTEM, Font.STYLE_PLAIN, Font.SIZE_SMALL ) );
#endif			// }
		
		//ZZ.setColor(ZZ.getColor(0x00,0x00,0x00));
		//ZZ.fillRect(0 - ZZ.ofX, 0 - ZZ.ofY, 50, 20);
		
		ZZ.setColor(ZZ.getColor(0xFF,0xFF,0xFF));
		ZZ.drawstring("rw:" + (System.currentTimeMillis() - mOmatsuri.reelwait), 50, 220);
		//System.out.println("ofX:"+ ZZ.ofX + " ofY:" +ZZ.ofY);
	}
#endif

#if __DEBUG_DRAW_YAKU__
	public static void dbgYakuDraw()
	{
		int i;
		// 内部当選役の描画
		int[] yakuID = {
                           0x00, 
                           Defines.LOT_YAKU_CHRY, 
                           Defines.LOT_YAKU_BELL, 
                           Defines.LOT_YAKU_WMLN, 
                           Defines.LOT_YAKU_REP, 
                           Defines.LOT_YAKU_RB, 
                           Defines.LOT_YAKU_BB};
		int yaku;
        yaku = clOHHB_V23.getWork(Defines.DEF_WAVEBIT);

		for( i = 0; i < 7;i++)
		{
			if(yakuID[i] == yaku)
			{
                innerHitPattern = "内部当選役:" + getYakuTxt(i + 1) + System.Environment.NewLine;
				break;
			}
		}
	}

    /// <summary>
    /// 内部当選役
    /// </summary>
    public static string innerHitPattern;

    /// <summary>
    /// リール停止位置
    /// </summary>
    public static string[] reelStopStatus = new string[3];

   	public static String getYakuTxt(int index)
	{
		String[] str = {
			"ﾗﾝﾀﾞﾑ (NO)|(BB)|(JAC)",
			"(ﾊｽﾞﾚ)|(ﾊｽﾞﾚ)|(ﾊｽﾞﾚ)",
			"(ﾁｪﾘｰ)|(ﾄﾞﾝﾍﾞﾙ15)|(無効)",
			"(ﾍﾞﾙ)|(ﾍﾞﾙ)|(無効)",
			"(ｽｲｶ)|(ｽｲｶorﾁｪ)|(無効)",
			"(ﾘﾌﾟﾚｲ)|(JACIN)|(ﾘﾌﾟﾘﾌﾟ15)",
			"(ﾚｷﾞｭﾗ)|(無効)|(無効)",
			"(ﾋﾞｯｸﾞ)|(無効)|(無効)"
		};
		//TRACE("ﾃｷｽﾄId:" + debug_cnf[DBG_YAKUN]);
		return str[index];
	}

#endif

#if __DEBUG__
	//------------------------------------------------------------------
	//		デバッグ用
	//------------------------------------------------------------------
	
	// デバッグ描画用
	public static void dbgDrawAll()
	{
#if __CHANGE_FPS__
		dbgFpsuDraw();
#endif
#if __DRAW_CREDIT_UP__
		dbgCreditUp();
#endif
#if __DRAW_REEL_WAIT__
		dbgReelWait();
#endif
#if __REEL_ID_CHECK__
		dbgReelMsg();
#endif
#if __DEBUG_DRAW_YAKU__
		dbgYakuDraw();
#endif
#if	__DEBUG_MENU__
		Debug.drawDebug(); // デバッグ画面の描画
#endif
	}
	
#endif

    private static string @hex2str(int d, int x) {
        string s;
        int l;

        s = "00000000" + Convert.ToString(d, 16);
        l = s.Length;
        return s.Substring(l - x, x);
    }

    // TODO C#移植 未使用につきコメントアウト
    //public static string hexByte(sbyte d) {
    //    int dd = (int)d;
    //    return @hex2str(dd & 0x000000ff, 2);
    //}
    public static string hexShort(short d) {
        int dd = (int)d;
        return @hex2str(dd & 0x0000ffff, 4);
    }
    public static string hexInt(int d) {
        return @hex2str(d, 8);
    }

    // 範囲で折り返す処理
    public static int dbg_Range(int now, int min, int max) {
        if (now < min) {
            now = max;
        } else if (now > max) {
            now = min;
        }

        return now;
    }


#if __TRACE__


 	public static void	TRACE1(string s)
	{
		System.out.println(s);
	}
	
 	public static void	TRACE2(string s)
	{	// 改行なし
		System.out.print(s);
	}
#endif



#if __TRACE_PROCESS__
	public static void testProses(int index)
	{
		int ProcessIndex[] = 
		{
			G7Network.GPH_NOPROCESS	,
			G7Network.GPH_01_RESCHECK	,
			G7Network.GPH_START	,
			G7Network.GPH_PRE_IMAGELOAD	,
			G7Network.GPH_IMAGELOAD	,
			G7Network.GPH_CON_AUTH	,
			G7Network.GPH_CON_HALLDATA	,
			G7Network.GPH_CON_ADINFO	,
			G7Network.GPH_CON_USEITEM	,
			G7Network.GPH_CON_GETCOIN	,
			G7Network.GPH_CON_ITEMLIST	,
			G7Network.GPH_CON_ITEMLIST2	,
			G7Network.GPH_CON_SLEEP_SUB	,
			G7Network.GPH_CON_SLEEP	,
			G7Network.GPH_CON_END_SUB	,
			G7Network.GPH_CON_END	,
			G7Network.GPH_CON_2003	,
			G7Network.GPH_CON_RESUME	,
			G7Network.GPH_CON_ITEMSELECT	,
			G7Network.GPH_CON_SYNC	,
			G7Network.GPH_CON_DIRECTION	,
			G7Network.GPH_VIEW_AUTH	,
			G7Network.GPH_VIEW_ADINFO	,
			G7Network.GPH_VIEW_RESLOAD	,
			G7Network.GPH_VIEW_CONNECT	,
			G7Network.GPH_VIEW_WINDOW	,
			G7Network.GPH_VIEW_INFOWINDOW	,
			G7Network.GPH_VIEW_SYNCCON	,
			G7Network.GPH_VIEW_DATAWINDOW	,
			G7Network.GPH_VIEW_MARQUEE	,
			G7Network.GPH_VIEW_COINWINDOW	,
			G7Network.GPH_VIEW_CONNECTWINDOW	,
			G7Network.GPH_VIEW_ITEMWINDOW	,
			G7Network.GPH_VIEW_MENUWINDOW	,
			G7Network.GPH_CTRL_INFOWINDOW	,
			G7Network.GPH_CTRL_MAIN	,
			G7Network.GPH_CTRL_COINGET	,
			G7Network.GPH_CTRL_GAMEWINDOW	,
			G7Network.GPH_CTRL_MENUINIT	,
			G7Network.GPH_CTRL_MENU	,
			G7Network.GPH_CTRL_POUNDMENU	,
			G7Network.GPH_CTRL_POUNDMENU_SUB	,
			G7Network.GPH_CTRL_ITEMLIST	,
			G7Network.GPH_CTRL_ITEM	,
			G7Network.GPH_CTRL_ERR_INIT	,
			G7Network.GPH_CTRL_USEITEM_PRC	,
			G7Network.GPH_END	,
			G7Network.GPH_END_BEF	,
			G7Network.GPH_SLEEP_BEF	,
			G7Network.GPH_VERSIONUP	,
			G7Network.GPH_ERR_WEBTO	,
			G7Network.GPH_SITE_WEBTO	,
			G7Network.GPH_GACHA	,
			G7Network.GPH_SHOP	,
			G7Network.GPH_ERR_WEBTO	,
			G7Network.GPH_SITE_WEBTO	,
			G7Network.GPH_GACHA	,
			G7Network.GPH_SHOP	
		};
		string ProsesName[] = 
		{
			"動作に影響を及ぼさない",
			"01 レスポンスチェック",
			"",
			"バラパーツの読み込み",
			"バラパーツの読み込み",
			"認証コマンド",
			"台情報取得",
			"広告情報取得",
			"使用中アイテムリスト取得通信",
			"メダル追加通信",
			"アイテムリスト取得通信",
			"アイテムリスト取得通信",
			"03 休憩通信準備(先)",
			"休憩通信",
			"03 精算通信準備(先)",
			"精算通信",
			"BB/JACIN/BBEND/通信",
			"レジューム通信",
			"アイテム使用通信",
			"定期通信",
			"レア演出通信",
			"認証画面の描画",
			"AD画面の描画",
			"リソースロードの描画",
			"通信中アイコンの描画",
			"ウインドウの描画",
			"お知らせウインドウの描画",
			"通信中アイコンの描画",
			"ゲーム中データの描画",
			"マーキーの描画",
			"コイン補充の描画",
			"通信中画面",
			"アイテムメニュー表示",
			"メニュー表示",
			"お知らせウインドウの入力",
			"ゲームメイン",
			"ゲームコイン取得",
			"ゲーム中ウィンドウ",
			"メニュー初期化",
			"メニュー制御",
			"POUNDメニュー",
			"POUNDメニュー（先）",
			"アイテムメニュー制御",
			"アイテムメニュー制御",
			"ゲーム時のエラーを制御",
			"アイテム使用確認制御",
			"アプリ終了",
			"パチドル不足時の終了確認",
			"パチドル不足時の休憩確認",
			"バージョンアップ",
			"サイトへジャンプ(err時)",
			"サイトへジャンプ",
			"サイトへジャンプ",
			"サイトへジャンプ(err時)",
			"サイトへジャンプ(err時)",
			"サイトへジャンプ",
			"サイトへジャンプ",
			"サイトへジャンプ(err時)"
		};
		
		int i;
		for(i = 0; i < ProcessIndex.length;i ++)
		{
			if( index == ProcessIndex[i])
			TRACE( "----gpHandlerProcess:" + index + " Name:"+ ProsesName[i] + "----");
		}
	}
#endif

}

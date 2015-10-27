using System;

public partial class mOmatsuri {
    public static int GET_INT_TO_STR(int str) { return str;}
    public static int GET_STR_TO_INT(string str) { return (int)long.Parse(str);} 	// 符号付きで変換されるので一つ上の型からキャストする
    public static ushort GET_STR_TO_CHAR(string str) { return (ushort)long.Parse(str); } 	// 符号付きで変換されるので一つ上の型からキャストする

	// 基本がこれ
	enum EYE_TYPE {
		EYE_TYPE_NONE,		// 目押しなし
		EYE_TYPE_BB,		// BB当選時
		EYE_TYPE_RB,		// RB当選時
		EYE_TYPE_BONUS,		// ボーナス時の３連どんちゃんねらい
		EYE_TYPE_SP_NO,		// 達人オートの通常時
		EYE_TYPE_SP_REP,	// 達人オートのリプレイ外し
		
		EYE_TYPE_TIMEOUT	// 自動停止
	};

    // mOmatsuri.javaに結合する
		
    // 目押し制御用
    // 引数
    //        stopNum	第？回胴停止(左=0, 中=1, 右=2)
    // 戻り値 true		目押し制御あり
    //        false		目押し制御なし
    public static int EyeSniper(int stopNum) {
    
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


        //DFMain.APP_TRACE("isMeoshi:" + Mobile.isMeoshi());
        //DFMain.APP_TRACE("WIN_LAMP_STATUS:" + mOmatsuri.int_s_value[DfOHHB_V23_DEF.DEF_INT_WIN_LAMP_STATUS] );
        //DFMain.APP_TRACE("HITREQ:" + (clOHHB_V23.getWork(DfOHHB_V23_DEF.DEF_HITREQ) & 0xFFFF) + "=" + DfOHHB_V23_DEF.DEF_HITFLAG_NR_BB);
        //DFMain.APP_TRACE("当選ﾁｪｯｸ(WAVEBIT):" + (clOHHB_V23.getWork(DfOHHB_V23_DEF.DEF_WAVEBIT) & 0xFFFF));


	    if( Mobile.isMeoshi() )
	    {	// メニューからの目押しフラグ
		    if( mOmatsuri.int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] > 0
				    && clOHHB_V23.getWork(Defines.DEF_HITREQ) == Defines.DEF_HITFLAG_NR_BB
				    && (clOHHB_V23.getWork(Defines.DEF_WAVEBIT) & Defines.DEF__00001110B) == 0)
		    {	// DfOHHB_V23_DEF.DEF__00001110B　ﾍﾞﾙとｽｲｶとﾘﾌﾟが当選してないかﾁｪｯｸしている
			    //DFMain.APP_TRACE("目押し　ビッグボーナス");
			    EyeType = EYE_TYPE.EYE_TYPE_BB;
		    }
		    else if (mOmatsuri.int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] > 0
					    && clOHHB_V23.getWork(Defines.DEF_HITREQ) == Defines.DEF_HITFLAG_NR_RB
					    && (clOHHB_V23.getWork(Defines.DEF_WAVEBIT) & Defines.DEF__00001110B) == 0)
		    {
			    //DFMain.APP_TRACE("目押し　レギュラーボーナス");
			    EyeType = EYE_TYPE.EYE_TYPE_RB;
		    }
		// TODO C#移植 GP処理のコメントアウト
        //} else if( (gp.gpif_auto_f == true) ||
        //    (gp.gpif_nonstop_f == true) ||
        //    (gp.gpif_tatsujin_f == true) )
        //{	// 目押しフラグON時 & ｵｰﾄﾌﾟﾚｲ時
        //    if( IS_BONUS() )
        //    {	// ボーナス中
        //        // ボーナス中は基本的に３連ドンを狙う
        //        EyeType = EYE_TYPE.EYE_TYPE_BONUS;
        //        if( gp.gpif_tatsujin_f == true )
        //        {// 達人オート
        //            if( Defines.CHECK_FLAG(clOHHB_V23.getWork(Defines.DEF_WAVEBIT), Defines.LOT_YAKU_REP) )
        //            {	// リプレイだったら
        //                if( (clOHHB_V23.getWork(Defines.DEF_BBGMCTR) > 8) && 
        //                     (clOHHB_V23.getWork(Defines.DEF_BIGBCTR) == 1) )
        //                {	//残り8ゲームまでにJAC回数が残り１の時
        //                    //DFMain.APP_TRACE("目押し　達人　ボーナス中リプレイ g=" + clOHHB_V23.getWork(DfOHHB_V23_DEF.DEF_BBGMCTR) + " j=" + clOHHB_V23.getWork(DfOHHB_V23_DEF.DEF_BIGBCTR));
        //                    EyeType = EYE_TYPE.EYE_TYPE_SP_REP;
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {	// 通常時
        //        if( mOmatsuri.int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] > 0
        //                && clOHHB_V23.getWork(Defines.DEF_HITREQ) == Defines.DEF_HITFLAG_NR_BB
        //                && (clOHHB_V23.getWork(Defines.DEF_WAVEBIT) & Defines.DEF__00001110B) == 0)
        //        {	// DfOHHB_V23_DEF.DEF__00001110B　ﾍﾞﾙとｽｲｶとﾘﾌﾟが当選してないかﾁｪｯｸしている
        //            //DFMain.APP_TRACE("目押し　ビッグボーナス");
        //            EyeType = EYE_TYPE.EYE_TYPE_BB;
        //        } else if (mOmatsuri.int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] > 0
        //                && clOHHB_V23.getWork(Defines.DEF_HITREQ) == Defines.DEF_HITFLAG_NR_RB
        //                && (clOHHB_V23.getWork(Defines.DEF_WAVEBIT) & Defines.DEF__00001110B) == 0)
        //        {
        //            //DFMain.APP_TRACE("目押し　レギュラーボーナス");
        //            EyeType = EYE_TYPE.EYE_TYPE_RB;
        //        } else
        //        {		
        //            if( gp.gpif_tatsujin_f == true )
        //            {// 達人オート
        //                Defines.APP_TRACE("目押し　通常時");
        //                EyeType = EYE_TYPE.EYE_TYPE_SP_NO;
        //            }
        //            else if (_spinTime < Util.GetMilliSeconds())
        //            {	// 自動停止は前回の停止出目をビタ押し
        //                EyeType = EYE_TYPE.EYE_TYPE_TIMEOUT;
        //            }
        //        }
        //    }
	    }
	
	    Defines.APP_TRACE("目押しタイプ:" + EyeType);
	    switch(EyeType)
	    {
	    //case EYE_TYPE.EYE_TYPE_NONE:
	    //	// 何もなし
	    //	break;
	    case EYE_TYPE.EYE_TYPE_BB:
		    // BB
                pos = (ushort)meoshi[mOmatsuri.int_s_value[Defines.DEF_INT_BIG_COUNT] % 2][stopNum];
		    break;
	    case EYE_TYPE.EYE_TYPE_RB:
		    // RB
            pos = (ushort)meoshi[2][stopNum];
		    break;
	    case EYE_TYPE.EYE_TYPE_BONUS:
		    // 三連ドン狙い
		    if( stopNum == 0)
		    {
                pos = (ushort)9;
		    }
		    break;
	    case EYE_TYPE.EYE_TYPE_SP_NO:
            //// 達人オート通常時
            //pos = (ushort) meoshi[3][stopNum];
                // TODO C#移植 ここでフォールスルー
	    case EYE_TYPE.EYE_TYPE_SP_REP:
            if (EyeType == EYE_TYPE.EYE_TYPE_SP_NO) {
                // 達人オート通常時
                pos = (ushort)meoshi[3][stopNum];
            }
		    // ボーナス中リプレイ外し
		    if( stopNum == 0)
		    {
                pos = (ushort)1;
		    }
		    break;
	    case EYE_TYPE.EYE_TYPE_TIMEOUT:
		    // 自動停止
			    tmp = 0x1F & (mOmatsuri.int_s_value[Defines.DEF_INT_PREV_GAME] >> (stopNum * 5));
			    tmp = (tmp + Defines.DEF_N_FRAME + 2) % Defines.DEF_N_FRAME;
                pos = (ushort)tmp;
		    break;
	    default:
		    // 目押しサポートなしの通常リール停止テーブル参照。
		    pos = (ushort) ANGLE2INDEX(mOmatsuri.int_s_value[Defines.DEF_INT_REEL_ANGLE_R0 + stopNum]);
		    break;
	    }

	    //pos = (ushort) ANGLE2INDEX(mOmatsuri.int_s_value[DfOHHB_V23_DEF.DEF_INT_REEL_ANGLE_R0 + stopNum]);
	    result_index = clOHHB_V23.mReelStop(stopNum, pos);
	    Defines.APP_TRACE("ﾘｰﾙ["+ stopNum+ "] = "+(pos&0xFFFF)+" 停止位置:" + result_index);
        ZZ.reelStopStatus[stopNum] = "ﾘｰﾙ[" + stopNum + "] = " + (pos & 0xFFFF) + " 停止位置:" + result_index;
	    return result_index;


    }

// ストップさせるリール番号選択
// 引数
// 	index	ストップさせるリール番号(0-2)
// 	ohijun	特定の押し順で押したい場合
// 戻り値
// 	ストップさせるリール番号(0-2)
static int getStopReel(int index, bool limit )
{
	int ret;
	int num = 0;
	//int stop[][] = {{0,1,2}, {2,1,0}};// 順押しと逆押し
	
	if( limit == true )
	{	// 自動停止の場合は順押しにする
		//ret = gp.gpif_oshijun_list[0][index];
		num = 0;
	}
    // TODO C#移植 GP処理コメントアウト
    //else
    //{	// 基本はこっち
    //    // 基本はメニューから情報をもってきておく
    //    //ret = Mobile.getOrder(index);
    //    //ret = gp.gpif_oshijun_list[gp.gpif_oshijun_n][index];
    //    num = gp.gpif_oshijun_n;
		
    //    if( (gp.gpif_auto_f == true) ||
    //         (gp.gpif_tatsujin_f == true))
    //    {	// ｵｰﾄﾌﾟﾚｲ時
    //        if( IS_BONUS() )
    //        {	// ボーナス中
    //            // 強制順押し
    //            //ret = Mobile.getOrder(gpif_oshijun_list[0][index]);
    //            //ret = gpif_oshijun_list[0][index];
    //            num = 0;
				
    //            //DFMain.APP_TRACE("WAVEBIT:" + ((clOHHB_V23.getWork(DfOHHB_V23_DEF.DEF_WAVEBIT)) & 0xFFFF));
    //            //DFMain.APP_TRACE("BIGBCTR:" + ((clOHHB_V23.getWork(DfOHHB_V23_DEF.DEF_BIGBCTR)) & 0xFFFF));
    //            //DFMain.APP_TRACE("BBGMCTR:" + ((clOHHB_V23.getWork(DfOHHB_V23_DEF.DEF_BBGMCTR)) & 0xFFFF));
				
    //            if( (gp.gpif_tatsujin_f == true) && 
    //                Defines.CHECK_FLAG(clOHHB_V23.getWork(Defines.DEF_WAVEBIT), Defines.LOT_YAKU_REP) && 
    //                (clOHHB_V23.getWork(Defines.DEF_BIGBCTR) == 1) && 
    //                (clOHHB_V23.getWork(Defines.DEF_BBGMCTR) > 8)
    //            )
    //            {	// 達人ｵｰﾄ & リプレイ & 残りJAC1ｹﾞｰﾑ & 8ｹﾞｰﾑ以上の場合
    //                // 逆押しにする
    //                //ret = Mobile.getOrder(gpif_oshijun_list[5][index]);
    //                //ret = gpif_oshijun_list[5][index];
    //                num = 5;
    //            }
    //        }
    //    }
    //}
    ret = gp.gpif_oshijun_list[num][index];
	
	//DFMain.APP_TRACE("押し順: gpif_oshijun_list["+num+"]["+index+"]=" + ret);
	return ret;
}

// 役の変更
static void chgYaku()
{
    Defines.ForceYakuFlag flag = Defines.ForceYakuFlag.NONE;
#if __DEBUG_MENU__
	if(Debug.debug_cnf[DBG_YAKV] != 0)
	{
		if( (Debug.debug_cnf[DBG_YAKV] > 0) && (Debug.debug_cnf[DBG_YAKV] < DfOHHB_V23_DEF.DEF_FORCE_MAX))
		{
			flagIndex = Debug.debug_cnf[DBG_YAKV];
			//clOHHB_V23.mSetForceFlag(debug_cnf[DBG_YAKV]);
			if( Debug.debug_cnf[DBG_YAKV_LOCK] == 0)
			{	// ロック指定ならば消さない
				Debug.debug_cnf[DBG_YAKV] = 0;
			}
		}
	}
#endif
	
	//if(gpif_bonus_n==0)return;
	
	// #define DfOHHB_V23_DEF.DEF_FORCE_HAZURE 		1			//（ﾊｽﾞﾚ）			｜（ﾊｽﾞﾚ）					｜（ﾊｽﾞﾚ）		
	// #define DfOHHB_V23_DEF.DEF_FORCE_CHERY 			2			//（ﾁｪﾘｰ）			｜（15枚役）ﾄﾞﾝ・ﾍﾞﾙ・ﾍﾞﾙ	｜（無効）	
	// #define DfOHHB_V23_DEF.DEF_FORCE_BELL 			3			//（ﾍﾞﾙ）			｜（ﾍﾞﾙ）					｜（無効）		
	// #define DfOHHB_V23_DEF.DEF_FORCE_SUIKA 			4			//（ｽｲｶ）			｜（ｽｲｶ　or ﾁｪﾘｰ）			｜（無効）
	// #define DfOHHB_V23_DEF.DEF_FORCE_REPLAY 		5			//（ﾘﾌﾟﾚｲ）			｜（JACIN）					｜（15枚役）ﾘﾌﾟ・ﾘﾌﾟ・ﾘﾌﾟ		
	// #define DfOHHB_V23_DEF.DEF_FORCE_REG 			6			//（ﾚｷﾞｭﾗｰﾎﾞｰﾅｽ）	｜（無効）					｜（無効）
	// #define DfOHHB_V23_DEF.DEF_FORCE_BIG 			7			//（ﾋﾞｯｸﾞﾎﾞｰﾅｽ）	｜（無効）					｜（無効）
	// #define DfOHHB_V23_DEF.DEF_FORCE_MAX 			8			// フラグ数

	if( gp.gpif_bonus_n == 1)
	{	// BB強制

        flag = Defines.ForceYakuFlag.BIG;
		gp.gpif_bonus_n = 0;
	}
	else if( gp.gpif_bonus_n == 2)
	{	// RB強制
        flag = Defines.ForceYakuFlag.REG;
		gp.gpif_bonus_n = 0;
	}

    flag = GameManager.forceYakuValue;

	if( flag != 0 )
	{// 強制役のセット
		clOHHB_V23.mSetForceFlag(flag);
	}
}
// 設定変更が必要ならば、変更する
static void chgWaveNum()
{
//	if(gp.gpif_setting > -1 )
//	{
		Mobile.setSetUpValue(gp.gpif_setting);
//		gp.gpif_setting = -1;
//	}
}

//// 再描画
//void drawFlash()
//{
//	if(gp.gpif_flash_f == true)
//	{	// 再描画
//		restartSlot();
//	}
//}

// コイン数が変化した時に加算、減算を行なう
public static void GPW_chgCredit(int num)
{
	int i;
	//int count = 0;
#if __DRAW_CREDIT_UP__
	ZZ.cregitUpLog = num;
#endif
#if __ERR_MSG__
	if( Math.abs(num) > 600)
	{
		SET_ERR_CODE(ERR_CODE_CREDIT_UP);
		SET_ERR_OPTION(num);
	}
#endif
	for(i=0;i<Math.Abs(num);i++)
	{
		if(num>0)
		{	// 換算
			gp.onCreditUp();
		}
		else if(num < 0)
		{	// 減算
			gp.onCreditUse();
		}
	}
}
// コイン枚数の変化(ボーナス)
public static void GPW_chgCreditBonus()
{
	
	
}
public static int GPW_chgProba()
{	// ボーナスの確率をアップさせる
	//int 
	
	if(gp.gpif_triple_f == true)
	{	//トリプルセブンアイテム使用時
		//(num * gpif_kakuhen_n)
		Defines.APP_TRACE("確率アップ:" + gp.gpif_kakuhen_n);
		return gp.gpif_kakuhen_n;
	}
	return 1;
	
}

// JACIN時に呼ばれる
public static void JacIn()
{
//	TRACE("JAC　IN");
	mOmatsuri.int_s_value[Defines.DEF_INT_BONUS_JAC_GOT] = 0;
	gp.onBonusJACIN();
}
// ボーナスカット関係
public static int cutBonus()
{	// isJacCut
	//DFMain.APP_TRACE("ボーナスカットチェック:" + gp.getOptValue(gp.OPT_BONUS_CUT));
	
	// num=1 JACゲームのみ
	// num=2 ボーナス全部
	int num;
	
	Defines.APP_TRACE("gp.gpif_bonuscut_f:" + gp.gpif_bonuscut_f);
	if( gp.gpif_bonuscut_f == true)
	{	// ボーナスカットON
		// 0ボーナスカット
		// 1 OFF
		// 2 JACカット
		num = gp.getOptValue(PublicDefine.OPT_BONUS_CUT);
		
		Defines.APP_TRACE("ボーナスカットフラグ:" + gp.gpif_bonuscut_f);
		if ((mOmatsuri.int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_B7)
			|| (mOmatsuri.int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_R7)) // ＢＢ終了判定
		//if ((clOHHB_V23.getWork(DfOHHB_V23_DEF.DEF_GMLVSTS) & 0x01) != 0)
		{	// ビッグボーナス中
			Defines.APP_TRACE("ここまできてる？：" + num);
//			if( (IS_BONUS_GAME() == true) )
//			{	// ボーナス子役ゲーム中
//				DFMain.APP_TRACE("test");
//				if( num == 0 )
//				{	// オール指定の時のみカット
//					DFMain.APP_TRACE("オールカット");
//					return 1;
//				}
//			}
//			else if( IS_BONUS_JAC() == true )
//			{
//				DFMain.APP_TRACE("ボーナスJAC中:" + num);
//				if( (num == 0) || (num == 2))
//				{	// オール指定の時のみカット
//					DFMain.APP_TRACE("JACカット");
//					return 2;
//				}
//			}
			
			if( IS_BONUS_JAC() == true )
			{
				Defines.APP_TRACE("ボーナスJAC中:" + num);
				if( (num == 0) || (num == 2))
				{	// オール指定の時のみカット
					Defines.APP_TRACE("JACカット");
					return 2;
				}
			}
			else
			{
				Defines.APP_TRACE("test");
				if( num == 0 )
				{	// オール指定の時のみカット
					Defines.APP_TRACE("オールカット");
					return 1;
				}
			}
			
		}
		else if (mOmatsuri.int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_RB_IN)
		//else if ((clOHHB_V23.getWork(DfOHHB_V23_DEF.DEF_GMLVSTS) & 0x02) != 0)
		{	// レギュラーボーナス
			if( (IS_BONUS_JAC() == true) )
			{	// レギュラーボーナス中
				Defines.APP_TRACE("レギュラーボーナス中:" + num);
				if( (num == 0) || (num == 2))
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
static bool cutBonusSystem(int type)
{
//	if( cutBonus() == true)
//	{	// ボーナスカットあり
//		if ((clOHHB_V23.getWork(DfOHHB_V23_DEF.DEF_HITFLAG) & (DfOHHB_V23_DEF.DEF_HITFLAG_NR_BB | DfOHHB_V23_DEF.DEF_HITFLAG_NR_RB))!=0)
//		{	// ボーナス入賞時
//			
//		}
//	}
	
	// 0 カットなし
	// 1 ボーナスカット
	// 2 JACカット
	int cutType;
	
	if( type == 1 )
	{	// 強制的にカットしたい場合
		cutType = 1;
	}
	else
	{	// 通常はカットフラグを見て動作させる
		cutType = cutBonus();
	}
	Defines.APP_TRACE(" ﾎﾞｰﾅｽ:" + mOmatsuri.int_s_value[Defines.DEF_INT_BB_KIND] + " cutType:" + cutType);
	
	if( cutType == 1 )
	{	// ﾎﾞｰﾅｽｹﾞｰﾑ
		Defines.APP_TRACE("ボーナスオールカットだよ");
		// 遊技状態 ｽﾃｰﾀｽをRB or JAC 中へ
		clOHHB_V23.setWork(Defines.DEF_GMLVSTS, (ushort)1);
		// 残りJACIN可能回数（0～3）
		clOHHB_V23.setWork(Defines.DEF_BIGBCTR, (ushort)1);
		// JACゲーム 遊技可能回数（0～12）
		clOHHB_V23.setWork(Defines.DEF_JACGAME, (ushort)0);
		// ヒット役を消す
		clOHHB_V23.setWork(Defines.DEF_HITFLAG, (ushort)0);
		return true;
	}
	else if( cutType == 2 )
	{	// JACｹﾞｰﾑ of RB
		Defines.APP_TRACE("JACカットだよ");
		// 遊技状態 ｽﾃｰﾀｽをRB or JAC 中へ
		clOHHB_V23.setWork(Defines.DEF_GMLVSTS, (ushort)1);
		// JACゲーム 遊技可能回数（0～12）
		clOHHB_V23.setWork(Defines.DEF_JACGAME, (ushort)0);
		// ヒット役を消す
		clOHHB_V23.setWork(Defines.DEF_HITFLAG, (ushort)0);
		return true;
	}
	
	
//	if( Mobile.isJacCut() == true)
//	{	// ボーナスカットあり
//		// 変化　 : DfOHHB_V23_DEF.DEF_GAMEST   主にボーナス中とリプレイ中の状態変化に使用。※下記のビットでチェック可能。
//		//		  : DfOHHB_V23_DEF.DEF_BBGMCTR  BB子役ゲーム中カウンター（0～30）
//		//		  : DfOHHB_V23_DEF.DEF_BIGBCTR  残りJACIN可能回数（0～3）
//		//		  : DfOHHB_V23_DEF.DEF_JAC_CTR  JACゲーム 入賞可能回数（0～8）
//		//		  : DfOHHB_V23_DEF.DEF_JACGAME: JACゲーム 遊技可能回数（0～12）
//
//		// num=1 JACゲームのみ
//		// num=2 ボーナス全部
//		int num;
//		// ﾎﾞｰﾅｽﾀｲﾑ
//		// bbtype=1 ﾋﾞｯﾌﾞﾎﾞｰﾅｽ
//		// bbtype=2 ﾚｷﾞｭﾗｰﾎﾞｰﾅｽ
//		int bbtype;
//		num = gp.getOptValue(gp.OPT_BONUS_CUT);
//		
//		// ﾎﾞｰﾅｽのﾀｲﾌﾟを取得
//		bbtype = (clOHHB_V23.getWork(DfOHHB_V23_DEF.DEF_GMLVSTS) & 0x03);
//		
//		DFMain.APP_TRACE("ｶｯﾄの種類:" + num + " ﾎﾞｰﾅｽ:" + mOmatsuri.int_s_value[DfOHHB_V23_DEF.DEF_INT_BB_KIND]);
//		
//
//		
//		if ((mOmatsuri.int_s_value[DfOHHB_V23_DEF.DEF_INT_BB_KIND] == DfOHHB_V23_DEF.DEF_BB_B7)
//							|| (mOmatsuri.int_s_value[DfOHHB_V23_DEF.DEF_INT_BB_KIND] == DfOHHB_V23_DEF.DEF_BB_R7)) // ＢＢ終了判定
//		{	// ビッグボーナス中
//			if( num == 1)
//			{	// JACゲームのみ
//				if( IS_BONUS_JAC() == true)
//				{
//					DFMain.APP_TRACE("JAC中だよ");
//					type = 1;
//					// 遊技状態 ｽﾃｰﾀｽをRB or JAC 中へ
//					bbtype = 1;
//				}
//			}
//			else if( num == 2)
//			{	// ﾎﾞｰﾅｽ全部
//				type = 0;
//				// 遊技状態 ｽﾃｰﾀｽをRB or JAC 中へ
//				bbtype = 1;
//			}
//		}
//		else if (mOmatsuri.int_s_value[DfOHHB_V23_DEF.DEF_INT_BB_KIND] == DfOHHB_V23_DEF.DEF_RB_IN)
//		{	// レギュラーボーナス
//			if( (IS_BONUS_JAC() == true) )
//			{	// JACゲーム中
//				type = 1;
//			}
//		}
//		if( type == 0 )
//		{	// ﾎﾞｰﾅｽｹﾞｰﾑ
//			// 遊技状態 ｽﾃｰﾀｽをRB or JAC 中へ
//			clOHHB_V23.setWork(DfOHHB_V23_DEF.DEF_GMLVSTS, (ushort)bbtype);
//			// 残りJACIN可能回数（0～3）
//			clOHHB_V23.setWork(DfOHHB_V23_DEF.DEF_BIGBCTR, (ushort)1);
//			// JACゲーム 遊技可能回数（0～12）
//			clOHHB_V23.setWork(DfOHHB_V23_DEF.DEF_JACGAME, (ushort)0);
//			// ヒット役を消す
//			clOHHB_V23.setWork(DfOHHB_V23_DEF.DEF_HITFLAG, (ushort)0);
//			return true;
//		}
//		else if( type == 1)
//		{	// JACｹﾞｰﾑ of RB
//			// 遊技状態 ｽﾃｰﾀｽをRB or JAC 中へ
//			clOHHB_V23.setWork(DfOHHB_V23_DEF.DEF_GMLVSTS, (ushort)bbtype);
//			// JACゲーム 遊技可能回数（0～12）
//			clOHHB_V23.setWork(DfOHHB_V23_DEF.DEF_JACGAME, (ushort)0);
//			// ヒット役を消す
//			clOHHB_V23.setWork(DfOHHB_V23_DEF.DEF_HITFLAG, (ushort)0);
//			return true;
//		}
//	}
	
	return false;
}


/////////////////////////////////////////////////////////////////
//
// サーバーデータ操作
//
/////////////////////////////////////////////////////////////////

	static bool[] eventFlagList = new bool[(int)Defines.EVENT.EVENT_NO_MAX];
	// イベントフラグセット用
	public static void GPW_SET_EVENT(int n) {
        eventFlagList[n] = true; 
        eventFlagList[(int)Defines.EVENT.EVENT_WEB] = true;
    }
	// 演出帳のチェック用
	void GPW_eventProcess(int type, int flash)
	{
		int eventNo = -1;
		int tmp;
		
		switch(type)
		{
		case (int)Defines.EVENT_PROC.EVENT_PROC_CHK_REEL:
			// リールの停止位置によるチェック
			if( (clOHHB_V23.getWork(Defines.DEF_HITREQ) == Defines.DEF_HITFLAG_NR_BB) ||
				(clOHHB_V23.getWork(Defines.DEF_HITREQ) == Defines.DEF_HITFLAG_NR_RB))
			{	// ボーナス内部中ならば
				if(flash == (int)Defines.EVENT.EVENT_NO1)
				{	// 3連ドン（1確)
					tmp = ANGLE2INDEX(mOmatsuri.int_s_value[Defines.DEF_INT_REEL_ANGLE_R0]);
					// 角度補正で+1されてしまうので-1する
					if( (tmp-1) == 9)
					{	// 3連ドン（1確)
						Defines.APP_TRACE("演出１：3連ドン（1確)");
						GPW_SET_EVENT((int)Defines.EVENT.EVENT_NO1);
					}
				}
				else if(flash == (int)Defines.EVENT.EVENT_NO2)
				{	// トリプルテンパイの場合
					Defines.APP_TRACE("演出２：トリプルテンパイ（BIG確）");
					GPW_SET_EVENT((int)Defines.EVENT.EVENT_NO2);
				}
				else if(flash == (int)Defines.EVENT.EVENT_NO3)
				{
					// これで図柄をとれるのかな？
					//	図柄コード
					//#define DfOHHB_V23_DEF.DEF_BSVN         	     0x01                // 青７
					//#define DfOHHB_V23_DEF.DEF_DON_         	     0x02                // ﾄﾞﾝ
					//#define DfOHHB_V23_DEF.DEF_BAR_         	     0x04                // BAR
					//#define DfOHHB_V23_DEF.DEF_RPLY         	     0x08                // ｾﾝｽ
					//#define DfOHHB_V23_DEF.DEF_WMLN         	     0x10                // 大山
					//#define DfOHHB_V23_DEF.DEF_BELL         	     0x20                // ﾊﾅﾋﾞ
					//#define DfOHHB_V23_DEF.DEF_CHRY         	     0x40                // ﾁｪﾘｰ
					//#define DfOHHB_V23_DEF.DEF_ANY_         	     0x00                // －
                    
                    // TODO ZZ.hexShortが__DEBUG__でないと使えない？
                    Defines.TRACE("★停止図柄1:" + ZZ.hexShort((short)(clOHHB_V23.getWork(Defines.DEF_ARAY21) & 0xFFFF)) + ":" +
                                            ZZ.hexShort((short)(clOHHB_V23.getWork(Defines.DEF_ARAY22) & 0xFFFF)) + ":" +
                                            ZZ.hexShort((short)(clOHHB_V23.getWork(Defines.DEF_ARAY23) & 0xFFFF)));
                    Defines.TRACE("★停止図柄2:" + ZZ.hexShort((short)(clOHHB_V23.getWork(Defines.DEF_ARAY11) & 0xFFFF)) + ":" +
                                            ZZ.hexShort((short)(clOHHB_V23.getWork(Defines.DEF_ARAY12) & 0xFFFF)) + ":" +
                                            ZZ.hexShort((short)(clOHHB_V23.getWork(Defines.DEF_ARAY13) & 0xFFFF)));
                    Defines.TRACE("★停止図柄3:" + ZZ.hexShort((short)(clOHHB_V23.getWork(Defines.DEF_ARAY31) & 0xFFFF)) + ":" +
                                            ZZ.hexShort((short)(clOHHB_V23.getWork(Defines.DEF_ARAY32) & 0xFFFF)) + ":" +
                                            ZZ.hexShort((short)(clOHHB_V23.getWork(Defines.DEF_ARAY33) & 0xFFFF)));
					
                    if( Defines.CHECK_FLAG(clOHHB_V23.getWork(Defines.DEF_ARAY33), Defines.DEF_BSVN) &&
						Defines.CHECK_FLAG(clOHHB_V23.getWork(Defines.DEF_ARAY13), Defines.DEF_CHRY))
					{	// 右リール下段にﾁｪﾘｰ付き青七図柄
						if( Defines.CHECK_FLAG(clOHHB_V23.getWork(Defines.DEF_ARAY21), (Defines.DEF_BSVN | Defines.DEF_DON_ | Defines.DEF_BAR_)) )
						{	// 左リール上段にボーナス図柄
							Defines.TRACE("演出３：上段タイプのゲチェナ");
							Defines.APP_TRACE("演出３：上段タイプのゲチェナ");
							GPW_SET_EVENT((int)Defines.EVENT.EVENT_NO3);
						}
						else if( Defines.CHECK_FLAG( clOHHB_V23.getWork(Defines.DEF_ARAY31), (Defines.DEF_BSVN | Defines.DEF_DON_ | Defines.DEF_BAR_) ) )
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
			if (tmp == 1) {
				Defines.APP_TRACE("演出６：鉢巻リールアクション「赤ドン」３回停止");
				//enshutu += "6,";
				GPW_SET_EVENT((int)Defines.EVENT.EVENT_NO6);
			}
			else if (tmp == 6) {
				Defines.APP_TRACE("演出７：鉢巻リールアクション「青ドン」３回停止");
				//enshutu += "7,";
				GPW_SET_EVENT((int)Defines.EVENT.EVENT_NO7);
			}
			else if (tmp == 23 || tmp == 29) {
				Defines.APP_TRACE("演出５：レバーオン鉢巻リール始動からの「大当たり」");
				//enshutu += "5,";
				GPW_SET_EVENT((int)Defines.EVENT.EVENT_NO5);
			}
			
			//////////////////////////////////////////////////////////////////////////////////
			// 割り算タイプ
			tmp = (int)(flash / 32);
			if (tmp == 7) {
				Defines.APP_TRACE("演出４：真・線香花火");
				//enshutu += "4,";
				GPW_SET_EVENT((int)Defines.EVENT.EVENT_NO4);
			}

			break;
		case (int)Defines.EVENT_PROC.EVENT_PROC_CHK_LANP:
			// 確定ランプフラグ
			if( (mOmatsuri.int_s_value[Defines.DEF_INT_WIN_LAMP] == 0) && (flash != 0) )
			{	// まだ確定ランプが点等していなく、演出番号が点等命令の場合
				Defines.APP_TRACE("演出８：「か～ぎや～」ランプ点灯");
				GPW_SET_EVENT((int)Defines.EVENT.EVENT_NO8);
			}
			break;
		case (int)Defines.EVENT_PROC.EVENT_PROC_WEB:
			// イベント情報の送信
			if( eventFlagList[(int)Defines.EVENT.EVENT_WEB] == true )
			{
				int i;
				String strTmp="";
				// ＠＠＠
				// グローバルの文字列い値を入れる
				for(i = (int)Defines.EVENT.EVENT_NO1; i < (int)Defines.EVENT.EVENT_NO_MAX; i++)
				{
					if( eventFlagList[i] == true)
					{
						if( strTmp != "")
						{
							strTmp = strTmp + ",";
						}
						strTmp = strTmp + i;
						eventFlagList[i] = false;
						
					}
				}
				
				eventFlagList[(int)Defines.EVENT.EVENT_WEB] = false;
				
				Defines.APP_TRACE("演出帳:" + strTmp);
				
				gp.userDirection = strTmp;
				
				// 通信を行なう
                // TODO C#移植 一旦コメントアウト
                gp.onReaSceneGet();
			}
			break;
		}
	}
	
	// 大祭り用のサーバー文字列の作成
	public static String OmatsuriToString(bool intfg)
	{	// リール停止の初期値
		Defines.APP_TRACE("★リール位置の初期化★");
		mOmatsuri.int_s_value[Defines.DEF_INT_REEL_ANGLE_R0] = INDEX2ANGLE(9);
		mOmatsuri.int_s_value[Defines.DEF_INT_REEL_ANGLE_R1] = INDEX2ANGLE(16);
		mOmatsuri.int_s_value[Defines.DEF_INT_REEL_ANGLE_R2] = INDEX2ANGLE(14);
		
		// 停止フラグを立てる
		mOmatsuri.int_s_value[Defines.DEF_INT_REEL_STOP_R0] = mOmatsuri.int_s_value[Defines.DEF_INT_REEL_ANGLE_R0];
		mOmatsuri.int_s_value[Defines.DEF_INT_REEL_STOP_R1] = mOmatsuri.int_s_value[Defines.DEF_INT_REEL_ANGLE_R1];
		mOmatsuri.int_s_value[Defines.DEF_INT_REEL_STOP_R2] = mOmatsuri.int_s_value[Defines.DEF_INT_REEL_ANGLE_R2];
		mOmatsuri.int_s_value[Defines.DEF_INT_IS_REEL_STOPPED] = 7; // リールストップ
		
		return OmatsuriToString();
	}
	public static String OmatsuriToString()
	{
		int i;
		String str;
		String str2;
		//int ofset;
		int tmp;
		
		str = "";
		str2 = "";
		
		Defines.APP_TRACE("★乱数用シードの登録★:" + clRAND8.mRndbuf.Length);
		// 乱数用シードの登録
		for( i=0; i < Defines.RAND_SEED_SIZE; i++)
		{
			//DFMain.APP_TRACE("["+i+"]"+  (clRAND8.mRndbuf[i] & 0xFFFF) );
			str = str + GET_INT_TO_STR( (clRAND8.mRndbuf[i] & 0xFFFF) );
			
			if( i != (Defines.RAND_SEED_SIZE-1))
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
		Defines.APP_TRACE("★サウンドの再生登録★["+mOmatsuri.bgm_no+"]");
		str = str + mOmatsuri.bgm_no + ",";
		if( mOmatsuri.bgm_loop == true)
		{
			tmp = 1;
		} else {
			tmp = 0;
		}
		str = str + tmp;
		/////////////////////////////////////////////
		
		str = str + ",";
		Defines.APP_TRACE("★グローバル変数の登録★:" + Defines.DEF_INT_SLOT_VALUE_MAX);
		for( i=0; i < Defines.DEF_INT_SLOT_VALUE_MAX; i++)
		{
			if( (i == Defines.DEF_INT_REEL_ANGLE_R0) ||
				(i == Defines.DEF_INT_REEL_ANGLE_R1) ||
				(i == Defines.DEF_INT_REEL_ANGLE_R2))
			{	// リールの位置
				tmp = ANGLE2INDEX(mOmatsuri.int_s_value[i]);
				// 角度補正で+1されてしまうので-1する
				//DFMain.APP_TRACE("["+i+"]:"+(tmp-1));
				//str = str + GpHandler.PadLeft( Integer.toString((tmp-1)), 2, "0");
				str = str + GET_INT_TO_STR((tmp-1));
//				System.out.println(">>"+i+"|"+GET_INT_TO_STR((tmp-1)));
			}
			else
			{
				//str = str + Integer.toString(mOmatsuri.int_s_value[i]);
				str = str + GET_INT_TO_STR(mOmatsuri.int_s_value[i]);
//				System.out.println(" >"+i+"|"+GET_INT_TO_STR(mOmatsuri.int_s_value[i]));
			}
			if( i != (Defines.DEF_INT_SLOT_VALUE_MAX-1))
			{
				str = str + ",";
			}
			//ofset++;
		}
		
		//DFMain.APP_TRACE("OmatsuriToString:"+str);
		return str;
	}
	
	// 大祭り用のサーバー文字列の復元
	public static int StringToOmatsuri(string[] appData, int top)
	{
		int i;
		int ofset = 0;
		int tmp;
		
//		// リールの出目
//		for(i = 0; i < 3; i++)
//		{
//			tmp = Integer.parseInt(appData[top + ofset]);
//			if( tmp != 0)
//			{	// tmp=0の場合は初期値になる為、代入しない
//				mOmatsuri.int_s_value[DfOHHB_V23_DEF.DEF_INT_REEL_ANGLE_R0 + ofset] = INDEX2ANGLE(tmp);
//			}
//			ofset++;
//		}
		
		//DFMain.APP_TRACE("★乱数用シードの登録★:" + clRAND8.mRndbuf.length);
		// 乱数用シードの登録
		for( i=0; i < Defines.RAND_SEED_SIZE; i++)
		{
			tmp = GET_STR_TO_INT(appData[top + ofset]);
			
			clRAND8.mRndbuf[i] = (ushort)tmp;
			//DFMain.APP_TRACE("mRndbuf復元["+i+"]=" + clRAND8.mRndbuf[i]);
			ofset++;
		}
		//DFMain.APP_TRACE("★ランダムの参照先の登録★");
		clRAND8.mRndwl = GET_STR_TO_CHAR(appData[top + ofset]); ofset++;
		clRAND8.mRndwh = GET_STR_TO_CHAR(appData[top + ofset]); ofset++;
		
		if( mOmatsuri.maj_ver >= 12 && mOmatsuri.sub_ver >= 0)
		{// バージョン11以上からサブのバージョンを付与
			//DFMain.APP_TRACE("★サウンドの再生登録★");
			mOmatsuri.bgm_no = GET_STR_TO_INT(appData[top + ofset]); ofset++;
			tmp = GET_STR_TO_INT(appData[top + ofset]); ofset++;
			if( tmp == 1)
			{
				mOmatsuri.bgm_loop = true;
			} else {
				mOmatsuri.bgm_loop = false;
			}
			if( mOmatsuri.bgm_no > -1)bgm_resumeFg = true;// アプリ復帰時に
		}
		
		Defines.APP_TRACE("★グローバル変数の登録★:" + Defines.DEF_INT_SLOT_VALUE_MAX);
		for( i=0; i < Defines.DEF_INT_SLOT_VALUE_MAX; i++)
		{
			//DFMain.APP_TRACE("["+i+"]=" + Integer.parseInt(appData[top + ofset]));
			tmp = GET_STR_TO_INT(appData[top + ofset]);
			
			mOmatsuri.int_s_value[i] = tmp;
			
			if( (i == Defines.DEF_INT_REEL_ANGLE_R0) ||
				(i == Defines.DEF_INT_REEL_ANGLE_R1) ||
				(i == Defines.DEF_INT_REEL_ANGLE_R2))
			{	// リールの位置
				if( tmp != 0)
				{	// tmp=0の場合は初期値になる為、代入しない
					//DFMain.APP_TRACE("リール位置["+i+"]:" + tmp + ":" + INDEX2ANGLE(tmp));
					mOmatsuri.int_s_value[i] = INDEX2ANGLE(tmp);
				}
			}
			
			ofset++;
		}
		
/*
		TRACE("★usr_ply_rot:" + gp.param_hall.usr_ply_rot);
		TRACE("★usr_ply_bns_rot:" + gp.param_hall.usr_ply_bns_rot);
		if( (gp.param_hall.usr_ply_rot == 0) &&
			(gp.param_hall.usr_ply_bns_rot == 0))
		{	// 別ユーザーの時
			// プレイヤーの変更時
			//chgPrayer();
			gp.onBonusMiddleEND();
		}
*/		
		return (top +ofset);
	}
	
// プレイヤーの変更時
public static void chgPrayer()
{
//	if( IS_BONUS() == true)
	if( gp.gpif_bonuscyu_f )
	{	// ボーナス中だった場合
		Defines.TRACE("★別ユーザーのボーナス");
		if( cutBonusSystem(1) )
		{	// ボーナス状態を強制クリア
			int bonusEndFg;
			
			Defines.TRACE("★ボーナス中を消す");
			// ボーナスを終わらせる
			bonusEndFg = clOHHB_V23.mBonusCounter();
			if (bonusEndFg != 0)
			{
				Defines.TRACE("終了処理");
				
		mOmatsuri.int_s_value[Defines.DEF_INT_CURRENT_MODE] = 0;
		mOmatsuri.int_s_value[Defines.DEF_INT_REQUEST_MODE] = 0;
		mOmatsuri.int_s_value[Defines.DEF_INT_WIN_COIN_NUM] = 0;
		mOmatsuri.int_s_value[Defines.DEF_INT_BET_COUNT] = 0;
		mOmatsuri.int_s_value[Defines.DEF_INT_BETTED_COUNT] = 0;
		mOmatsuri.int_s_value[Defines.DEF_INT_NUM_KASIDASI] = 0;
		mOmatsuri.int_s_value[Defines.DEF_INT_WIN_LAMP] = 0;
		mOmatsuri.int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] = 0;

//System.out.println("★別ユーザーのボーナス");

		// ヒット役を消す
		clOHHB_V23.setWork(Defines.DEF_HITFLAG, (ushort)0);
		// 獲得枚数を消す
		clOHHB_V23.setWork(Defines.DEF_INT_WIN_GET_COIN, (ushort)0);

				//ここでランプを消す
				mOmatsuri.int_s_value[Defines.DEF_INT_TOP_LAMP] = 0;
				// 上部の左右ランプフラグ更新
				ctrlTopLamp();
				// 4THのランプフラグ更新
				lampSwitch(Defines.DEF_LAMP_4TH, Defines.DEF_LAMP_ACTION_OFF);
//				// ランプを全て消灯
//				mOmatsuri.int_s_value[DfOHHB_V23_DEF.DEF_INT_LAMP_1] = 0;
//				mOmatsuri.int_s_value[DfOHHB_V23_DEF.DEF_INT_LAMP_2] = 0;
//				mOmatsuri.int_s_value[DfOHHB_V23_DEF.DEF_INT_LAMP_3] = 0;
				
				BonusEnd(1);
				mOmatsuri.int_s_value[Defines.DEF_INT_BB_TOTAL_GOT] = 0;
			}
		}
	}
	else if( IS_REPLAY() )
	{
		Defines.TRACE("★リプレイを消す:");
		// リプレイを消す
		
		// 状態を通常に戻す
		clOHHB_V23.setWork(Defines.DEF_GAMEST, (ushort)0);
		// ヒット役を消す
		clOHHB_V23.setWork(Defines.DEF_HITFLAG, (ushort)0);
	
		// ランプのビットを0にする
		//mOmatsuri.int_s_value[DfOHHB_V23_DEF.DEF_INT_LAMP_1 + (DfOHHB_V23_DEF.DEF_LAMP_FRE / 32)] &= ~(1 << (DfOHHB_V23_DEF.DEF_LAMP_FRE % 32));
		lampSwitch(Defines.DEF_LAMP_FRE, Defines.DEF_LAMP_ACTION_OFF);
		
		// ベットした枚数
		mOmatsuri.int_s_value[Defines.DEF_INT_BETTED_COUNT] = 0;
		//mOmatsuri.int_s_value[DfOHHB_V23_DEF.DEF_INT_BETTED_COUNT] = 0;
	}
}

#if __COM_TYPE__
// ボーナス終了処理
// 引数
// type		0=通常,1=離席や別ユーザーの為の強制ボーナスクリア
static void BonusEnd(int type)
{
	// ボーナス終了！！
	Defines.TRACE("ボーナス終了！！:" + mOmatsuri.int_s_value[Defines.DEF_INT_BONUS_GOT]);
	// mBonusCounter()内部でclearWork(DfOHHB_V23_DEF.DEF_CLR_AREA_2)を実行！

	// ＪＡＣ ＆ ＢＢ終了時にここを通る
	// ＢＢ・ＲＢが終了したことにするためここでフラグを初期化処理する
	mOmatsuri.int_s_value[Defines.DEF_INT_IS_BB_RB_END] = 1;

    // セルフオート停止フラグを立てる
    GameManager.Instance.StopAutoPlay("ボーナス終了");

	Mobile.stopSound(Defines.DEF_SOUND_MULTI_BGM); // BGMを止める

	if ((mOmatsuri.int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_B7)
			|| (mOmatsuri.int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_R7)) { // ＢＢ終了判定
		Defines.TRACE("BBボーナス終了:" + mOmatsuri.int_s_value[Defines.DEF_INT_BONUS_GOT] + "枚:" + type);
		if( type == 1)
		{	// ボーナス状態を強制クリア
			
		}
		else
		{	// 通常はこっち
			_soundTime = Util.GetMilliSeconds() + Defines.DEF_SOUND_MS_09; // ﾌｧﾝﾌｧｰﾚ完奏時間設定
			playBGM(Defines.DEF_SOUND_09, false); // BBEND音
#if __BONUS_CUT__
			if( BonusCutFg == true)
			{	// ボーナスカットオールの場合限定
				BonusCutFg = false;
				if( mOmatsuri.int_s_value[Defines.DEF_INT_BONUS_GOT] < Defines.BIG_BONUS_AVENUM)
				{
					int num;
					num = (Defines.BIG_BONUS_AVENUM - mOmatsuri.int_s_value[Defines.DEF_INT_BONUS_GOT]);
					Defines.TRACE("BBカット分を追加:" + Defines.BIG_BONUS_AVENUM + "-" + mOmatsuri.int_s_value[Defines.DEF_INT_BONUS_GOT] + "=" + num);
					
					mOmatsuri.int_s_value[Defines.DEF_INT_BONUS_GOT] += num;
#if __ERR_MSG__
				if( (num < 0) || (num > BIG_BONUS_AVENUM))
				{	// 0以下ならば
					SET_ERR_CODE(ERR_CODE_BONUS_CUT);
					SET_ERR_OPTION(num);
				}
#endif
					GPW_chgCredit(num);
				}
			}
#endif
			mOmatsuri.int_s_value[Defines.DEF_INT_BB_TOTAL_GOT] += mOmatsuri.int_s_value[Defines.DEF_INT_BONUS_GOT];
		}
		
		// ボーナス
		gp.onBonusEND();
		
		// 消化中の使用コイン数があるため、－枚は０枚にしておく
		mOmatsuri.int_s_value[Defines.DEF_INT_BONUS_GOT] = Math.Max(0,mOmatsuri.int_s_value[Defines.DEF_INT_BONUS_GOT]);
				
	}
	else if (mOmatsuri.int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_RB_IN)
	{
		Defines.TRACE("RBボーナス終了:" + mOmatsuri.int_s_value[Defines.DEF_INT_BONUS_GOT] + "枚:" + type);
		if( type == 1)
		{	// ボーナス状態を強制クリア
			
		}
		else
		{	// 通常はこっち
#if __BONUS_CUT__
			Defines.TRACE("RBカット:" + cutBonus());
			if( BonusCutFg == true)
			{	// カット処理フラグON
				BonusCutFg = false;
				if( mOmatsuri.int_s_value[Defines.DEF_INT_BONUS_GOT] < Defines.REG_BONUS_AVENUM)
				{
					int num;
					num = (Defines.REG_BONUS_AVENUM - mOmatsuri.int_s_value[Defines.DEF_INT_BONUS_GOT]);
					Defines.TRACE("RBカット分を追加:" + Defines.REG_BONUS_AVENUM + "-" + mOmatsuri.int_s_value[Defines.DEF_INT_BONUS_GOT] + "=" + num);
#if __ERR_MSG__
				if( (num < 0) || (num > Defines.REG_BONUS_AVENUM))
				{	// 0以下ならば
					SET_ERR_CODE(ERR_CODE_BONUS_CUT);
					SET_ERR_OPTION(num);
				}
#endif
					mOmatsuri.int_s_value[Defines.DEF_INT_BONUS_GOT] += num;
					GPW_chgCredit(num);
				}
			}
#endif
		}
		// ボーナス
		gp.onBonusEND();
		// 消化中の使用コイン数があるため、－枚は０枚にしておく
		mOmatsuri.int_s_value[Defines.DEF_INT_BONUS_GOT] = Math.Max(0,mOmatsuri.int_s_value[Defines.DEF_INT_BONUS_GOT]);
	}
}
#endif

}

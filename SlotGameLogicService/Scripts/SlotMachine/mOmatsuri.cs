//#include "DfMain.h"

/*
 * B_Max.java
 * 
 * Created on 2005/10/24
 */

/**
 * B-Max
 * 
 * @author a05229ak
 */
using System;
public partial class mOmatsuri {

// satoh
	public static SlotInterface gp = null;
	
#if __COM_TYPE__
	//private static int debug_cnf[DBG_YAKV] = 0;
	//private static int debug_cnf[DBG_YAKUN] = 0;
	
	
	//$debug_cnf[DBG_MANUAL] ^= 0x01;
	
	private static bool bigBonusFg = false;
	private static bool BonusCutFg = false;
	
	public static long reelwait=0;
	private static bool reelStartFg;	// リールスタートキーがすでに押されている場合
	private static bool reqMenuFg = false;	// リール停止時にメニューを表示する
	public static bool reqMenuFg2 = false;	// リール停止時にメニューを表示する
	//public static bool reqMenu
	public static bool	bgm_resumeFg = false;	// BGMのレジューム再生フラグ
	public static int		bgm_no = -1;
	public static bool	bgm_loop = false;
	
	public static int		maj_ver;	// メインバージョン
	public static int		sub_ver;	// サブバージョン
	
#else
	static int debugHallParamReqX = 0;
	static int debugHallParamResX = 0;
    [Obsolete]
	static void debugHallParam(){
        //ZZ.setClip(0, 0, 240,240);
        //int reqln = ZZ.stringWidth(debughallreq)+240;
        //int resln = ZZ.stringWidth(debughallres)+240;
        //ZZ.drawString(debughallreq,240-debugHallParamReqX,228);
        //ZZ.drawString(debughallres,240-debugHallParamResX,240);
        //debugHallParamReqX++;
        //debugHallParamResX++;
        //debugHallParamReqX %= reqln;
        //debugHallParamResX %= resln;
	}
#endif
	
	
	static int mes_x = 0;

	
#if __COM_TYPE__

#else
	/**
	 * データパネルの左側
	 */
//	private static void drawDataPanelHall() {
//		ZZ.setClip(0, 0, 240,240);
//		//台番号
//		ZZ.drawImage(Defines.DEF_RES_HALL_NAVI_NUM, 7, 2);
//		printPanelCounts(hallData[Defines.DEF_H_MACHINE_ID], 24,9, 999,0);
//
//		//メッセージ
//		ZZ.setClip(51, 3, 181,22);
//		ZZ.drawImage(Defines.DEF_RES_HALL_NAVI_MES, 50, 2);
//		ZZ.setColor(ZZ.getColor(0xff, 0xff, 0xff));
//		int ln = 181+ZZ.stringWidth(hallMes);
//		if(mes_x < ln * 3){//3周ぐらいしてみようか
//			int xxx = mes_x % (232 + ZZ.stringWidth(hallMes));
//			ZZ.drawString(hallMes, 232 - xxx, 3+6+12);
//			mes_x ++;
//		}
//
//		ZZ.setClip(0, 0, 240,240);
//		
//		//閉店時間
//		ZZ.drawImage(Defines.DEF_RES_HALL_NAVI_TIME, 189, 212);
//		int hhh = (int)((hallData[Defines.DEF_H_CLOSE_TIME]/3600)%24+9);//GMTなので時差9時間
//		int mmm = (int)((hallData[Defines.DEF_H_CLOSE_TIME]/60  )%60);
//		printPanelCounts(hhh, 200,226, 99,0);
//		printPanelCounts(mmm, 221,226, 99,0,2);
//	}
#endif

    public static int prevHttpTime = 0;
	public static int kasidasiMedal = 0;
    public static int prevkasidasiMedal = 0;
	static bool IS_HALL(){
		return Mobile.getGameMode() == Defines.DEF_GMODE_HALL;
	}
	
#if __COM_TYPE__
	public static int[] hallData = new int[Defines.DEF_H_PARAM_NUM];
#else
//	static String debughallreq = "";
//	static String debughallres = "";
//	static String debugStr = "";
//	static bool debugHall = false;
//	
//	//最後に通信したｹﾞｰﾑ数;
//	public static int lastHttpGame;
//	
//	public static final int[] hallDataSize = {
//		Defines.DEF_HS_APPLI_REQ 	,//	1;
//		Defines.DEF_HS_SERVER_RES	,//	1;
//		
//		Defines.DEF_HS_HALL_ID		,//	4;
//		Defines.DEF_HS_LAND_ID		,//	2;
//		Defines.DEF_HS_MACHINE_ID	,//	2;
//		Defines.DEF_HS_LAUNCH_TIME	,//	8;
//		Defines.DEF_HS_CLOSE_TIME	,//	8;
//		
//		Defines.DEF_HS_MEDAL_IN		,//	3;
//		Defines.DEF_HS_MEDAL_OUT	,//	3;
//		Defines.DEF_HS_PLAYER_COIN	,//	5;
//
//		Defines.DEF_HS_GAME_COUNT	,//	5;
//		Defines.DEF_HS_WAVENUM		,//	1;
//	    Defines.DEF_HS_GAMEST		,//	2;
//	    Defines.DEF_HS_BBGMCTR		,//	2;
//	    Defines.DEF_HS_BIGBCTR		,//	1;
//	    Defines.DEF_HS_JAC_CTR		,//	1;
//	    Defines.DEF_HS_JACGAME		,//	1;
//	    Defines.DEF_HS_PCC_CTR		,//	4;
//	    Defines.DEF_HS_HITREQ		,//	2;
//	    Defines.DEF_HS_BNSSYM		,//	1;
//	    Defines.DEF_HS_BET		,//	1;
//	    Defines.DEF_HS_REEL_L		,//2;
//		Defines.DEF_HS_REEL_C		,//2;
//		Defines.DEF_HS_REEL_R		,//2;
//		
//		Defines.DEF_HS_BNS_0		,//	4;
//		Defines.DEF_HS_BNS_1		,//	2;
//		Defines.DEF_HS_BNS_2		,//	2;
//		Defines.DEF_HS_BNS_3		,//	2;
//		Defines.DEF_HS_BNS_4		,//	2;
//		Defines.DEF_HS_BNS_5		,//	2;
//		Defines.DEF_HS_BNS_6		,//	2;
//		Defines.DEF_HS_BNS_7		,//	2;
//		Defines.DEF_HS_BNS_8		,//	2;
//		Defines.DEF_HS_BNS_9		,//	2;
//		Defines.DEF_HS_BB_COUNT		,//	2;
//		Defines.DEF_HS_RB_COUNT		,//	2;
//	};
//	
//	public static String hallMes = "";
//	public static void setHallParam(String arg) throws Exception{
//		debughallres = arg;
//		int offset = 0;
//		int[] tmp = new int[Defines.DEF_H_PARAM_NUM];
//		try{
//			for(int i = 0;i<Defines.DEF_H_PARAM_NUM;i++){
//				tmp[i] = Integer.parseInt(arg.substring(offset,offset+hallDataSize[i]),16);
//				offset += hallDataSize[i];
//			}
//			if(offset<arg.Length()){
//				hallMes = arg.substring(offset);
//				mes_x = 0;
//			}
//		}catch (Exception e) {
//			throw e;
//		}
//		for(int i = 0;i<Defines.DEF_H_PARAM_NUM;i++){
//			hallData[i] = tmp[i];
//		}
//		lastHttpGame = hallData[Defines.DEF_H_GAME_COUNT];
//		
//		debugStr = "GAMEST:"+(int)clOHHB_V23.getWork(Defines.DEF_GAMEST)+"->"+hallData[Defines.DEF_H_GAMEST];
//		
//		//メインの情報を更新する
//		clOHHB_V23.clearWork(Defines.DEF_CLR_AREA_1);
//		clOHHB_V23.setWork(Defines.DEF_WAVENUM,(char)hallData[Defines.DEF_H_WAVENUM]);
//		clOHHB_V23.setWork(Defines.DEF_GAMEST,(char)hallData[Defines.DEF_H_GAMEST]);
//		clOHHB_V23.setWork(Defines.DEF_BBGMCTR,(char)hallData[Defines.DEF_H_BBGMCTR]);
//		clOHHB_V23.setWork(Defines.DEF_BIGBCTR,(char)hallData[Defines.DEF_H_BIGBCTR]);
//		clOHHB_V23.setWork(Defines.DEF_JAC_CTR,(char)hallData[Defines.DEF_H_JAC_CTR]);
//		clOHHB_V23.setWork(Defines.DEF_JACGAME,(char)hallData[Defines.DEF_H_JACGAME]);
////		clOHHB_V23.setWork(Defines.DEF_PCC_CTR,(char)(0xff & hallData[Defines.DEF_H_PCC_CTR]));
////		clOHHB_V23.setWork(Defines.DEF_PCC_CTR+1,(char)((0xff00 & hallData[Defines.DEF_H_PCC_CTR])>>8));
//		clOHHB_V23.setWork(Defines.DEF_HITREQ,(char)hallData[Defines.DEF_H_HITREQ]);
//		int_s_value[Defines.DEF_INT_BB_KIND] = (char)hallData[Defines.DEF_H_BNSSYM];
//		int_s_value[Defines.DEF_INT_BET_COUNT] = (char)hallData[Defines.DEF_H_BET];
//		
//		int_s_value[Defines.DEF_INT_REEL_ANGLE_R0] = INDEX2ANGLE(hallData[Defines.DEF_H_REEL_L]);
//		int_s_value[Defines.DEF_INT_REEL_ANGLE_R1] = INDEX2ANGLE(hallData[Defines.DEF_H_REEL_C]);
//		int_s_value[Defines.DEF_INT_REEL_ANGLE_R2] = INDEX2ANGLE(hallData[Defines.DEF_H_REEL_R]);
//		int_s_value[Defines.DEF_INT_REEL_STOP_R0] = int_s_value[Defines.DEF_INT_REEL_ANGLE_R0];
//		int_s_value[Defines.DEF_INT_REEL_STOP_R1] = int_s_value[Defines.DEF_INT_REEL_ANGLE_R1];
//		int_s_value[Defines.DEF_INT_REEL_STOP_R2] = int_s_value[Defines.DEF_INT_REEL_ANGLE_R2];
//
//		int_s_value[Defines.DEF_INT_SLOT_COIN_NUM] = hallData[Defines.DEF_H_PLAYER_COIN];
//
//		int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] = Defines.DEF_RP19;
//		switch(hallData[Defines.DEF_H_PCC_CTR]){
//		case Defines.DEF_RP01:
//		case Defines.DEF_RP04:
//		case Defines.DEF_RP08:
//		case Defines.DEF_RP13:
//		case Defines.DEF_RP16:
//		case Defines.DEF_RP19:
//			int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] = hallData[Defines.DEF_H_PCC_CTR];//4thリールの位置覚えるよ
//			break;
//		}
//
//		//データパネルを更新する
//		initDataPaneHistory();
//		for(int i = Defines.DEF_H_BNS_9; i >= Defines.DEF_H_BNS_1 ;i--){
//			for(int j = 0;j<(hallData[i] & 0xf)+1;j++){
//				setCurrentDataPanel((j+1) * 100);
//			}
//			if((hallData[i] & 0x10)!=0){//bb
//				shiftDataPanelHistory((hallData[i] & 0xf) * 100, Defines.DEF_PS_BB_RUN);
//			}else if((hallData[i] & 0x20)!=0){
//				shiftDataPanelHistory((hallData[i] & 0xf) * 100, Defines.DEF_PS_RB_RUN);
//			}
//		}
//		for(int i = 0;i<(hallData[Defines.DEF_H_BNS_0] / 100)+1;i++){
//			setCurrentDataPanel((i+1) * 100);
//		}
//	}
//	
//	public static String getHallParam(){
//		hallData[Defines.DEF_H_WAVENUM] = clOHHB_V23.getWork(Defines.DEF_WAVENUM);
//		hallData[Defines.DEF_H_GAMEST] = clOHHB_V23.getWork(Defines.DEF_GAMEST);
//		hallData[Defines.DEF_H_BBGMCTR] = clOHHB_V23.getWork(Defines.DEF_BBGMCTR);
//		hallData[Defines.DEF_H_BIGBCTR] = clOHHB_V23.getWork(Defines.DEF_BIGBCTR);
//		hallData[Defines.DEF_H_JAC_CTR] = clOHHB_V23.getWork(Defines.DEF_JAC_CTR);
//		hallData[Defines.DEF_H_JACGAME] = clOHHB_V23.getWork(Defines.DEF_JACGAME);
////		hallData[Defines.DEF_H_PCC_CTR] = (clOHHB_V23.getWork(Defines.DEF_PCC_CTR)|(clOHHB_V23.getWork(DATA[Defines.DEF_PCC_CTR+1])<<8));
//		hallData[Defines.DEF_H_HITREQ] = clOHHB_V23.getWork(Defines.DEF_HITREQ);
//		hallData[Defines.DEF_H_BNSSYM] = int_s_value[Defines.DEF_INT_BB_KIND];
//		hallData[Defines.DEF_H_BET] = int_s_value[Defines.DEF_INT_BET_COUNT];
//
//		hallData[Defines.DEF_H_REEL_L] = int_s_value[Defines.DEF_INT_REEL_ANGLE_R0]/0xC31;
//		hallData[Defines.DEF_H_REEL_C] = int_s_value[Defines.DEF_INT_REEL_ANGLE_R1]/0xC31;
//		hallData[Defines.DEF_H_REEL_R] = int_s_value[Defines.DEF_INT_REEL_ANGLE_R2]/0xC31;
//
//		DfMain.TRACE(("Defines.DEF_H_REEL_L:" + hallData[Defines.DEF_H_REEL_L]);
//		hallData[Defines.DEF_H_PCC_CTR] = int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE];//4thリールの位置覚えるよ
//
//		String res = "";
//		for(int i = 0;i<hallData.Length;i++){
//			res += toHexString(hallData[i],hallDataSize[i]);
//		}
//		debughallreq = res;
//		return res;
//	}
//	public static String toHexString(int arg,int size){
//		String res = Integer.toHexString(arg);
//		if(res.Length()>size){
//			res = "";//はみ出たら消そうか
//		}
//		while(size-res.Length()>0){
//			res="0"+res;
//		}
//		return res;
//	}
#endif
	// ======================================================
	// TOBE [変数定義]
	// ======================================================
	/** int プール */
	public static readonly int[] int_s_value = new int[Defines.DEF_INT_SLOT_VALUE_MAX];

	/** ｻｳﾝﾄﾞ演奏時間 */
	private static long _soundTime;

	/** WAIT３０秒でランプが消える */
	private static long _lampTime;

	/** ４０秒で回転は止まる */
	private static long _spinTime;

	/** ４thリールの一時停止 */
	private static long _4thTime;

	/** リールの角度 [1.15.16] の固定少数 */
	private static readonly int ANGLE_2PI_BIT = 16;

	/** リールの角度マスク(0xFFFF)。 */
	private static readonly int ANGLE_2PI_MASK = (1 << ANGLE_2PI_BIT) - 1;

	/** リールの角度が未定義（INDEX2ANGLE では出ない値）)。 */
	private static readonly int ANGLE_UNDEF = -1;

	/**
	 * ボーナス間情報グラフ作成データ.
	 * 
	 * bonus_Data[x(?回前)][y(100回転刻み)]
	 * 
	 * @see df.Df#INFO_GAMES
	 * @see df.Df#INFO_GAME_HISTORY
	 * 
	 */
    private static readonly ushort[,] bonus_Data = new ushort[Defines.DEF_INFO_GAME_HISTORY, Defines.DEF_INFO_GAMES];

    //private static final ushort[][] REELTB = {
#if __COM_TYPE__
#if __REEL_ID_CHECK__
	public static final char[][] REELTB = {
#else
    public static readonly ushort[][] REELTB = {
#endif
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
#else
#if __REEL_ID_CHECK__
	public static final char[][] REELTB = {
#else
	private static char[][] REELTB = {
#endif
			"\u0008\u0004\u0020\u0010\u0040\u0001\u0008\u0020\u0002\u0002\u0002\u0008\u0020\u0040\u0001\u0010\u0008\u0020\u0010\u0020\u0001"
					.ToCharArray(),
			"\u0020\u0004\u0040\u0020\u0008\u0001\u0010\u0040\u0020\u0008\u0004\u0040\u0004\u0020\u0008\u0040\u0002\u0020\u0008\u0010\u0008"
					.ToCharArray(),
			"\u0020\u0040\u0008\u0010\u0020\u0001\u0040\u0008\u0010\u0020\u0040\u0008\u0010\u0020\u0002\u0008\u0040\u0020\u0004\u0008\u0010"
					.ToCharArray() };
#endif
	// private static final char[][] REELTB = {
	// { Defines.DEF_RPLY, Defines.DEF_BAR_, Defines.DEF_BELL, Defines.DEF_WMLN, Defines.DEF_CHRY, Defines.DEF_BSVN, Defines.DEF_RPLY,
	// Defines.DEF_BELL, Defines.DEF_DON_, Defines.DEF_DON_, Defines.DEF_DON_, Defines.DEF_RPLY, Defines.DEF_BELL,
	// Defines.DEF_CHRY, Defines.DEF_BSVN, Defines.DEF_WMLN, Defines.DEF_RPLY, Defines.DEF_BELL, Defines.DEF_WMLN,
	// Defines.DEF_BELL, Defines.DEF_BSVN },
	// { Defines.DEF_BELL, Defines.DEF_BAR_, Defines.DEF_CHRY, Defines.DEF_BELL, Defines.DEF_RPLY, Defines.DEF_BSVN, Defines.DEF_WMLN,
	// Defines.DEF_CHRY, Defines.DEF_BELL, Defines.DEF_RPLY, Defines.DEF_BAR_, Defines.DEF_CHRY, Defines.DEF_BAR_,
	// Defines.DEF_BELL, Defines.DEF_RPLY, Defines.DEF_CHRY, Defines.DEF_DON_, Defines.DEF_BELL, Defines.DEF_RPLY,
	// Defines.DEF_WMLN, Defines.DEF_RPLY },
	// { Defines.DEF_BELL, Defines.DEF_CHRY, Defines.DEF_RPLY, Defines.DEF_WMLN, Defines.DEF_BELL, Defines.DEF_BSVN, Defines.DEF_CHRY,
	// Defines.DEF_RPLY, Defines.DEF_WMLN, Defines.DEF_BELL, Defines.DEF_CHRY, Defines.DEF_RPLY, Defines.DEF_WMLN,
	// Defines.DEF_BELL, Defines.DEF_DON_, Defines.DEF_RPLY, Defines.DEF_CHRY, Defines.DEF_BELL, Defines.DEF_BAR_,
	// Defines.DEF_RPLY, Defines.DEF_WMLN
	//
	// }, };

	/**
	 * AUTO GENERATED char ARRAY BY compact.CompactClass
	 */
	private static readonly char[] width4th = "\u0075\u003F\u003B\u0037\u0040\u0038"
			.ToCharArray();

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
	private static int[] panel_colors = {
			ZZ.getColor(0x00, 0x00, 0x00),// なし
			ZZ.getColor(Defines.DEF_POS_CELL_COLOR_ETC_R, Defines.DEF_POS_CELL_COLOR_ETC_G,
					Defines.DEF_POS_CELL_COLOR_ETC_B),// 通常
			ZZ.getColor(Defines.DEF_POS_CELL_COLOR_BB_R, Defines.DEF_POS_CELL_COLOR_BB_G,
					Defines.DEF_POS_CELL_COLOR_BB_B),// BB
			ZZ.getColor(Defines.DEF_POS_CELL_COLOR_RB_R, Defines.DEF_POS_CELL_COLOR_RB_G,
					Defines.DEF_POS_CELL_COLOR_RB_B),// RB
			ZZ.getColor(0xFF, 0x00, 0xFF), };// カレント

	// ======================================================
	// TOBE [メソッド]
	// ======================================================
	/**
	 * 次のランプ用ステータスを取得する
	 * 
	 * @param idx
	 * @return ランプの次のステータス
	 */
	public static int getLampStatus(int idx) {
	//private static int getLampStatus(int idx) {
		if ((int_s_value[Defines.DEF_INT_LAMP_1 + (idx / 32)] & (1 << (idx % 32))) != 0) {
			return Defines.DEF_LAMP_STATUS_ON;
		} else {
			return Defines.DEF_LAMP_STATUS_OFF;
		}
	}

	/**
	 * ランプ用スイッチ
	 * 
	 * @param idx:index
	 * @param act:action
	 */
#if __COM_TYPE__
	public static void lampSwitch(int idx, int act) {
#else
	private static void lampSwitch(int idx, int act) {
#endif
		if (act == Defines.DEF_LAMP_ACTION_ON) {
			// ビットを1にする
			int_s_value[Defines.DEF_INT_LAMP_1 + (idx / 32)] |= (1 << (idx % 32));
		} else if (act == Defines.DEF_LAMP_ACTION_OFF) {
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
	private static int INDEX2ANGLE(int n) {
		return 0xC31 * (n);
	}

	/**
	 * 角度から次に止まれる絵柄インデックスに変換。 <BR>
	 * 端数切り上げ
	 * 
	 * @param i:角度
	 * @return 絵柄ｲﾝﾃﾞｯｸｽ [0, NUM_REEL)
	 */
	public static int ANGLE2INDEX(int i) {
		return ((Defines.DEF_N_FRAME * i + ANGLE_2PI_MASK) >> ANGLE_2PI_BIT)
				% Defines.DEF_N_FRAME;
	}

	/**
	 * モードをリクエストする。
	 * 
	 * @param mod:スロットゲームのモード
	 * @return リクエストされたスロットゲームのモード
	 */
	private static int REQ_MODE(int mod) {
		return int_s_value[Defines.DEF_INT_REQUEST_MODE] = mod;
	}

	// ======================================================
	// TOBE [アクセッサ]
	// ======================================================

	/**
	 * ゲーム情報の初期化。
	 * 
	 * @see #int_s_value
	 * @see df.Df#INT_BIG_COUNT
	 * @see df.Df#INT_UNTIL_BONUS_GAMES
	 * @see df.Df#INT_TOTAL_GAMES
	 * @see df.Df#INT_GAME_INFO_MAX_GOT
	 * @see df.Df#INT_SLOT_COIN_NUM
	 * 
	 */
	public static void initGameInfo() {
		
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

	/**
	 * 新規ゲーム開始時に情報の履歴を初期化する
	 * 
	 * @see bonus_Data[][]
	 * @see df.Df#GAME_NONE
	 * @see df.Df#INFO_GAMES
	 * @see df.Df#INFO_GAME_HISTORY
	 * 
	 */
	private static void initDataPaneHistory() {
		int_s_value[Defines.DEF_INT_BONUS_DATA_BASE] = 0;
		for (int x = 0; x < Defines.DEF_INFO_GAME_HISTORY; x++) {
			for (int y = 0; y < Defines.DEF_INFO_GAMES; y++) {
				bonus_Data[x,y] = Defines.DEF_GAME_NONE;
			}
		}
	}

#if __COM_TYPE__

#else
//	//
//	// 貸し出し枚数を知る
//	// 
//	// @return 貸し出し枚数
//	//
//	public static int getKasidasiValue() {
//		if(IS_HALL()){
//			return kasidasiMedal;
//		}else{
//			return int_s_value[Defines.DEF_INT_NUM_KASIDASI] * Defines.DEF_NUM_START_COIN;
//		}
//	}
	//
//	// ゲーム情報取得。
//	// 
//	// @param gameInfo[]
//	// 
//	// @see #int_s_value
//	// @see df.Df#INT_BIG_COUNT
//	// @see df.Df#INT_TOTAL_GAMES
//	// @see df.Df#INT_GAME_INFO_MAX_GOT
//	// 
//	public static void getGameInfo(final int[] gameInfo) {
//		gameInfo[Defines.DEF_GAME_INFO_BB_AVG] = 0; // ＢＢ ＡＶＥＲＡＧＥ １０進固定小数一桁（0又は正）で渡します
//		gameInfo[Defines.DEF_GAME_INFO_RB_AVG] = 0; // ＲＢ ＡＶＥＲＡＧＥ １０進固定小数一桁（0又は正）で渡します
//
//		if (int_s_value[Defines.DEF_INT_BIG_COUNT] > 0) {
//			gameInfo[Defines.DEF_GAME_INFO_BB_AVG] = int_s_value[Defines.DEF_INT_TOTAL_GAMES]
//					/ int_s_value[Defines.DEF_INT_BIG_COUNT];
//		}
//
//		// ＢＢ最高獲得枚数 １０進整数（0又は正）で渡します
//		gameInfo[Defines.DEF_GAME_INFO_BB_GOT_MAX] = int_s_value[Defines.DEF_INT_GAME_INFO_MAX_GOT];
//	}
//
//	public static void setReelSpeed(int per) {
//		int_s_value[Defines.DEF_INT_REEL_SPEED_PER] = per;
//		int_s_value[Defines.DEF_INT_REEL_SPEED] = ZZ.getThreadSpeed()
//				* Defines.DEF_REEL_COUNT_MIN * 0x10000 / 60000 * per / 100;
//	}
#endif

	// ======================================================
	// TOBE [スロット開始再開終了]
	// ======================================================
	/**
	 * ゲーム開始時に各値を初期化。 0:通常1:シミュレーション
	 * 
	 * @see #int_s_value
	 * @see #initGameInfo()
	 */
	public void newSlot() {
	
		for (int i = 0; i < int_s_value.Length; i++) {
			if (i != Defines.DEF_INT_REEL_SPEED) {// リールスピードだけ除外
				int_s_value[i] = 0;
			}
		}
Defines.TRACE("newSlot");
		// メインループスピード
		int_s_value[Defines.DEF_INT_LOOP_SPEED] = ZZ.getThreadSpeed();

		
		// リールの初期化
//		// ランダムに並べる
//#if __COM_TYPE__
//		int_s_value[Defines.DEF_INT_REEL_ANGLE_R0] = INDEX2ANGLE(8);
//		int_s_value[Defines.DEF_INT_REEL_ANGLE_R1] = INDEX2ANGLE(15);
//		int_s_value[Defines.DEF_INT_REEL_ANGLE_R2] = INDEX2ANGLE(13);
//#else
//		int_s_value[Defines.DEF_INT_REEL_ANGLE_R0] = INDEX2ANGLE(ZZ.getRandom(Defines.DEF_N_FRAME));
//		int_s_value[Defines.DEF_INT_REEL_ANGLE_R1] = INDEX2ANGLE(ZZ.getRandom(Defines.DEF_N_FRAME));
//		int_s_value[Defines.DEF_INT_REEL_ANGLE_R2] = INDEX2ANGLE(ZZ.getRandom(Defines.DEF_N_FRAME));
//#endif
//		
//		// 停止フラグを立てる
//		int_s_value[Defines.DEF_INT_REEL_STOP_R0] = int_s_value[Defines.DEF_INT_REEL_ANGLE_R0];
//		int_s_value[Defines.DEF_INT_REEL_STOP_R1] = int_s_value[Defines.DEF_INT_REEL_ANGLE_R1];
//		int_s_value[Defines.DEF_INT_REEL_STOP_R2] = int_s_value[Defines.DEF_INT_REEL_ANGLE_R2];
//		int_s_value[Defines.DEF_INT_IS_REEL_STOPPED] = 7; // リールストップ

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
		Mobile.stopSound(Defines.DEF_SOUND_UNDEF);

		// ゲーム情報の初期化
		initGameInfo();
// satoh
		Defines.TRACE("gp:" + gp);
		
        // TODO C#移植 GP処理コメントアウト
//        Defines.TRACE("gp.param_hall:チェック");
//        if( gp.param_hall == null)
//        {
//            Defines.TRACE("gp.param_hall:ないよ！");
//        }
//Defines.TRACE("mOmatsuri.newSlot():" +  gp.param_hall.usr_coin);
//        int_s_value[Defines.DEF_INT_SLOT_COIN_NUM] = gp.param_hall.usr_coin;

Defines.TRACE("initDataPaneHistory");
		initDataPaneHistory();
#if __COM_TYPE__
#else
//DfMain.TRACE(("全体描画");
//		// 全体再描画要求
//		//drawAll();
//		restartSlot();
#endif
Defines.TRACE("Z80移植リールクラスの初期化");
		// /////////////////////////////////////////
		// Z80移植リールクラスの初期化
		// /////////////////////////////////////////
		// システムの初期化（携帯電話から現在時刻を取得）
//		clOHHB_V23.mInitializaion((int) (Util.GetMilliSeconds() / 1000));
// #none/satoh ランダム生成をミリ秒単位で
		clOHHB_V23.mInitializaion(GameManager.GetRandomSeed());
		// 内部設定を6にする。
        clOHHB_V23.setWork(Defines.DEF_WAVENUM, (ushort)Mobile.getSetUpValue());
		
#if	__DEBUG_MENU__
		if( Debug.debug_cnf[DBG_APP_REFRESH] == 1)
		{
			gp.appData_str = null;
			Debug.debug_cnf[DBG_APP_REFRESH] = 0;
		}
#endif

        // TODO C#移植 GP処理コメントアウト
		//Defines.TRACE("appData:" + gp.appData_str);
		
		// ここでゲーム情報の復元をおこなっていみる
//System.out.println("★★★★ newSlot ★★★★");

        // TODO C#移植 GP処理コメントアウト
		//gp.getAppDataString(gp.appData_str);
		
		//int_s_value[Defines.DEF_INT_REEL_ANGLE_R0] = INDEX2ANGLE(9);
		//int_s_value[Defines.DEF_INT_REEL_ANGLE_R1] = INDEX2ANGLE(16);
		//int_s_value[Defines.DEF_INT_REEL_ANGLE_R2] = INDEX2ANGLE(14);
		
		// 停止フラグを立てる
		int_s_value[Defines.DEF_INT_REEL_STOP_R0] = int_s_value[Defines.DEF_INT_REEL_ANGLE_R0];
		int_s_value[Defines.DEF_INT_REEL_STOP_R1] = int_s_value[Defines.DEF_INT_REEL_ANGLE_R1];
		int_s_value[Defines.DEF_INT_REEL_STOP_R2] = int_s_value[Defines.DEF_INT_REEL_ANGLE_R2];
		int_s_value[Defines.DEF_INT_IS_REEL_STOPPED] = 7; // リールストップ
		
//System.out.println("★★★★ usr_ply_rot ★★★★ "+gp.param_hall.usr_ply_rot);
//System.out.println("★★★★ dai_bns_rot ★★★★ "+gp.param_hall.dai_bns_rot);
		
		// 3D関係
		Mascot3D.initModel();
		// ZZ.setLight(0, 0, 100,2772*100/4048,1896*100/4048);
		ZZ.setLight(0, 0, 100, 2024 * 100 / 4048, 1012 * 100 / 4048);

#if __COM_TYPE__
#else
//		InitYokoku();
#endif
		

		

		
#if __COM_TYPE__
Defines.TRACE("全体描画");
		// 全体再描画要求
		//drawAll();
		restartSlot();
#endif
#if __REEL_ID_CHECK__
		ZZ.checkReel();
#endif
	}


	/**
	 * ゲーム再開。
	 * 
	 */
	public void restartSlot() {
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
	public static int endSlot() {
		Mobile.stopSound(Defines.DEF_SOUND_UNDEF); // サウンド停止
//		return int_s_value[Defines.DEF_INT_SLOT_COIN_INNER_COUNT]
//				- (int_s_value[Defines.DEF_INT_NUM_KASIDASI] * Defines.DEF_NUM_START_COIN);
		return int_s_value[Defines.DEF_INT_TOTAL_PAY] - int_s_value[Defines.DEF_INT_TOTAL_BET];
	}

	public static int getExitReason() {
		return int_s_value[Defines.DEF_INT_WHAT_EXIT];
	}

	public static void setExitReason(int arg) {
		int_s_value[Defines.DEF_INT_WHAT_EXIT] = arg;
	}
#if __COM_TYPE__
#else
//	public static int getCoinNum() {
//		if(IS_HALL()){
//			return hallData[Defines.DEF_H_PLAYER_COIN]- getKasidasiValue();
//		}else{
//			return int_s_value[Defines.DEF_INT_TOTAL_PAY] - int_s_value[Defines.DEF_INT_TOTAL_BET];
//		}
//	}
//
//	public static bool isHallHttp = false; 
//	public static void hallHttp(int req){
//		hallData[Defines.DEF_H_APPLI_REQ] = req;
//		isHallHttp = true;
//	}
#endif
	// ======================================================
	// TOBE [メイン制御]
	// ======================================================
	private static int pressingSpan = 0;

	/**
	 * ここの処理はMobileクラスからゲーム実行中（MODE_RUN）のときだけ呼び出される。
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

	public bool process(int keyTrigger) {
		int_s_value[Defines.DEF_INT_REEL_SPEED] =
			ZZ.getThreadSpeed()
			* Defines.DEF_REEL_COUNT_MIN
			* 0x10000
			/ 60000
			* Mobile.getReelSpeed() / 100;
#if __COM_TYPE__
//		DfMain.TRACE(("keyTrigger process:" + keyTrigger);
#else
//		if(isHallHttp){
//			REQ_MODE(Defines.DEF_RMODE_HTTP);
//			isHallHttp = false;
//		}
#endif
#if __DEBUG_MENU__
		// ﾃﾞﾊﾞｯｸﾞ用
		// デバッグモード用
		Debug.procDebug(keyTrigger, ZZ.getKeyPressing());
#endif

#if __CHANGE_FPS__
		ZZ.dbgFpsProc(keyTrigger, ZZ.getKeyPressing());
#endif
		
#if __COM_TYPE__
		
		//DfMain.TRACE(("オートプレイ:" + gp.gpif_auto_f + ":" + gp.gpif_nonstop_f + ":" + gp.gpif_tatsujin_f + ":" + Mobile.isMeoshi());
		if( Mobile.isMeoshi() ||
			(gp.gpif_auto_f == true) ||
			(gp.gpif_nonstop_f == true) ||
			(gp.gpif_tatsujin_f == true) )
		{	// ｵｰﾄﾌﾟﾚｲ
			if( (gp.gpif_auto_f == true) ||
				(gp.gpif_nonstop_f == true) ||
				(gp.gpif_tatsujin_f == true) )
			{	// ｵｰﾄﾌﾟﾚｲの予約
				//DfMain.TRACE(("test:" + Mobile.int_m_value[Defines.DEF_INT_IS_MENU_AVAILABLE]);
				// スロットゲーム中WAIT時以外は遷移しない
				if (Mobile.int_m_value[Defines.DEF_INT_IS_MENU_AVAILABLE] == Defines.DEF_MENU_UNAVAILABLE)
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
		
		if( reqMenuFg == true)
		{	// 自動メニュー画面描画
			if (Mobile.int_m_value[Defines.DEF_INT_IS_MENU_AVAILABLE] == Defines.DEF_MENU_AVAILABLE)
			{	// メニューが無効の時
				//DfMain.TRACE(("自動メニュー画面描画");
				//Mobile.keyTrigger |= Defines.DEF_KEY_BIT_SOFT1;
				////gp.gph_ctrl_count = 1;
				////gp.callINTR( gp.GPH_CTRL_MENUINIT );
				////gp.softLabel_flag = true;
				reqMenuFg = false;
				//reqMenuFg2 = true;
				////return false;			
                // TODO C#移植 GP処理コメントアウト
                //gp.gph_ctrl_count = req_code;
                //gp.callINTR( gp.GPH_CTRL_MENUINIT );
			}
		}
		
		
#if __BONUS_CUT__
		
#else
		if( Mobile.isJacCut() == true)
		{	// ボーナスカットの場合
			keyTrigger = Defines.DEF_KEY_BIT_SELECT;
		}
#endif
#else
//		if (IS_BONUS_JAC() && Mobile.isJacCut()) {
//			//keyTrigger = Defines.DEF_KEY_BIT_SELECT;
//		}
#endif

#if __DEBUG_MENU__
		if( CHECK_FLAG(keyTrigger, Defines.DEF_KEY_BIT_7) )
		{	// 7ｷｰで再ｾｯﾄ
			// リピートテロップ
			gp.setTelop(Debug.getYakuTxt());
			Debug.debug_cnf[DBG_YAKV] = Debug.debug_cnf[DBG_YAKUN];
		}
		if( Debug.debug_cnf[DBG_DRWF] != DBG_MODE_OFF)
		{	// デバッグウインドウが出ていればキーを無効化する
			keyTrigger = 0;
		}
#endif

#if __AUTO_STOCK__
		if( int_s_value[Defines.DEF_INT_SLOT_COIN_NUM] < 5)
		{
			int_s_value[Defines.DEF_INT_SLOT_COIN_NUM] = 999;
		}
#endif
		
#if __DRAW_SLOT2__
#else
// ここじゃなくていいかも		
//		if(gp.gpif_flash_f == true)
//		{
//			DfMain.TRACE(("★★★★再描画★★★★");
//			drawAll();
//			gp.gpif_flash_f = false;
//		}
#endif
// satoh 02/02 07:55
//		if( gp.intr_ch != GpHandler.GPH_NOPROCESS ) {
		if( gp.getBusy() ) {
			// GP用のウインドウが出ているので、筐体キーを無効化する
			keyTrigger = 0;
			return false;
		}
		// コイン枚数の更新
		int_s_value[Defines.DEF_INT_SLOT_COIN_NUM] = gp.gpif_coin;
		//DfMain.TRACE(("gp.gpif_coin:" + gp.gpif_coin);
		
		// 40ms*10毎にタイミングを取ってみる
		pressingSpan++;
		pressingSpan %= 10;
		if (!IS_BONUS() && pressingSpan == 0) {
//			keyTrigger |= ZZ.getKeyPressing();
		}
		
	////////////////////////////////////
		// このwhile文はループさせるためではなく、違うモードにすぐ遷移(conitnue)するための仕組みです。
		while (int_s_value[Defines.DEF_INT_CURRENT_MODE] != int_s_value[Defines.DEF_INT_REQUEST_MODE]) {
			
			
			
			if (Defines.DEF_IS_DEBUG_PRINT_RMODE) {
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
			switch (int_s_value[Defines.DEF_INT_CURRENT_MODE]) {
			case Defines.DEF_RMODE_WIN:
				if (int_s_value[Defines.DEF_INT_WIN_LAMP] > 0
						&& int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] == 0) {
					// た～まや～点灯
					int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] = 1;
					// ボーナス当選時
					gp.onBonusNaibu();
                    // セルフオート停止フラグを立てる
                    GameManager.Instance.StopAutoPlay("たまや点灯");
				}
				
				// さらに大当たりで止まっていたらかっきーん！
				if (int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] == 1) {
					if (int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] == Defines.DEF_RP08) {
						playSE(Defines.DEF_SOUND_12);
						_soundTime = Util.GetMilliSeconds() + Defines.DEF_SOUND_MS_12;
						int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] = 2;
						// 以後演出抽選しないようにする
                        clOHHB_V23.setWork(Defines.DEF_FOUT3, (ushort)1);
					}
				}
				break;
			// TOBE [=0.RMODE_WAIT init]137
			case Defines.DEF_RMODE_WAIT: // (モード初期)
				
				if(IS_HALL()){
					Mobile.saveMenuData(true);//不正防止用にここで保存
				}
				Mobile.setMenuAvarable(true);// 押せるようにする

				if (!IS_BONUS()) {
					lampSwitch(Defines.DEF_LAMP_4TH, Defines.DEF_LAMP_ACTION_OFF);
				}
				// コイン７セグ描画用変数を初期化
				int_s_value[Defines.DEF_INT_BETTED_COUNT] = 0;
				Defines.TRACE("待機中");

#if __REEL_ID_CHECK__
				ZZ.checkReel();
#endif
                // TODO C#移植 GP処理コメントアウト
				// 定期通信用データの作成
				//gp.setAppDataString();

				// 演出帳のデータを転送する
				GPW_eventProcess((int)Defines.EVENT_PROC.EVENT_PROC_WEB, -1);
				break;
			// TOBE [=1.RMODE_BET init]
			case Defines.DEF_RMODE_BET: // MAXBETﾗﾝﾌﾟ表示期間(モード初期)
				//DfMain.TRACE(("ベット処理１");
				// リール全点滅は一回だけ
				if (int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] == 2) {
					int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] = 3;
				}
				
				// 払い出しコイン枚数（表示用）
				int_s_value[Defines.DEF_INT_WIN_GET_COIN] = 0;
				int_s_value[Defines.DEF_INT_WIN_COIN_NUM] = 0; // １ゲーム中の獲得コイン枚数
				// ﾃﾝﾊﾟｲ状態
				int_s_value[Defines.DEF_INT_IS_TEMPAI] = 0;

				// 告知はBET時にクリア
				if (!IS_REPLAY()) {
					int_s_value[Defines.DEF_INT_KOKUCHI_X] = 0;
				}
#if __COM_TYPE__
	
#else
//				if (checkLimit()) {
//					if(IS_HALL()){
//						hallData[Defines.DEF_H_APPLI_REQ] = Defines.DEF_HRQ_EXIT;
//						REQ_MODE(Defines.DEF_RMODE_HTTP);
//						return false;
//					}
//					return true;// GAMEOVER
//				}
#endif
				// if (int_s_value[Defines.DEF_INT_BET_COUNT] == 0) {
				// int_s_value[Defines.DEF_INT_WHAT_EXIT] = Defines.DEF_EXIT_OVER_KASIDASHI;
				// return true;// GAMEOVER
				// }

				// サウンド
				if (!IS_BONUS()) {
					// ボーナス消化中以外は毎回停止指示。
					Mobile.stopSound(Defines.DEF_SOUND_UNDEF);
				}
#if __BONUS_CUT__
				
#else
	#if __COM_TYPE__
				if( Mobile.isJacCut() == false)
	#else
	//				if (!(Mobile.isJacCut() && IS_BONUS_JAC()))
	#endif
#endif
				{
					
					if(IS_HALL()){/*プレーヤーコイン要求*/
						if(hallData[Defines.DEF_H_PLAYER_COIN] < int_s_value[Defines.DEF_INT_BET_COUNT]) {
							//鳴らさない
							//DfMain.TRACE(("ベット音ならさない");
							break;
						}
					}
					//DfMain.TRACE(("ベット音ならす");
					switch (int_s_value[Defines.DEF_INT_BET_COUNT]) {
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
				Mobile.setMenuAvarable(false);
				
				break;// RMODE_BET
			// TOBE [=2.RMODE_SPIN init]
			case Defines.DEF_RMODE_SPIN: // 回転開始(モード初期)
				if (!IS_REPLAY()) {
					int_s_value[Defines.DEF_INT_TOTAL_BET] += int_s_value[Defines.DEF_INT_BETTED_COUNT];
/**/				hallData[Defines.DEF_H_MEDAL_IN] += int_s_value[Defines.DEF_INT_BETTED_COUNT];
					hallData[Defines.DEF_H_PLAYER_COIN] -= int_s_value[Defines.DEF_INT_BETTED_COUNT];
				}

				// ///////////////////////////
				// Z80移植リール部分の初期化
				// 1プレイ遊技用初期化
				clOHHB_V23.clearWork(Defines.DEF_CLR_AREA_3);
				// ///////////////////////////
				// ///////////////////////////
				// Z80移植
				// パチスロ抽選用乱数を取得
				int rand = clOHHB_V23.mRandomX();
				
				// 設定変更チェック
				chgWaveNum();

				// 役の変更チェック
				chgYaku();
				
				// 役抽選
				clOHHB_V23.mReelStart(rand, int_s_value[Defines.DEF_INT_BET_COUNT]);
				// ///////////////////////////
				// // ///////////////////////////
#if __COM_TYPE__
#else
//				// 告知
//				SetYokoku();
//				// 告知はここで帰る
//				if (Mobile.getKokuchi() == Defines.DEF_SELECT_3_AFFTER) {
//					BackYokoku();
//				}
//
//				// 前告知なら
//				if (Mobile.getKokuchi() == Defines.DEF_SELECT_3_PREV) {
//					// 前ゲームの小役と異なっていたら帰る。
//					if (m_nYokoku2[0] != m_nYokoku2[1]) {
//						BackYokoku();
//					}
//					// 出現
//					ForwordYokoku();
//				}
#endif
				if (!IS_BONUS()) {
					if ((clOHHB_V23.getWork(Defines.DEF_WAVEBIT) & 0x01) != 0) {
						int_s_value[Defines.DEF_INT_CHRY_HIT]++;
					} else if ((clOHHB_V23.getWork(Defines.DEF_WAVEBIT) & 0x04) != 0) {
						int_s_value[Defines.DEF_INT_WMLN_HIT]++;
					}
				} else if (IS_BONUS_GAME()) {
					// ビタ外し成功
					if ((clOHHB_V23.getWork(Defines.DEF_WAVEBIT) & 0x08) != 0) {
						int_s_value[Defines.DEF_INT_JAC_HIT]++;
					}
				}

				if ((clOHHB_V23.getWork(Defines.DEF_GMLVSTS) & (0x08 | 0x10)) != 0) {
//					int_s_value[Defines.DEF_INT_FLAG_GAME_COUNT]++;
					int_s_value[Defines.DEF_INT_THIS_FLAG_GAME]++;
				}
				
#if __COM_TYPE__
#else
//				// 告知をセット
//				setKokuchiId();
#endif
				// 演出をセット
				int flash0 = clOHHB_V23.getWork(Defines.DEF_FLASH + 0);

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
#if __COM_TYPE__
				// 演出によるチェック
				GPW_eventProcess((int)Defines.EVENT_PROC.EVENT_PROC_CHK_LANP, (flash0 / 128));
#endif
				// 確定ランプフラグ
				int_s_value[Defines.DEF_INT_WIN_LAMP] = flash0 / 128;
				flash0 %= 128;
				// 開始音
				// ; A*32+B 演出パターンデータ
				// ; A = フラッシュ演出パターンデータ
				// ; ( 0~7 )
				// ; B = リール演出パターンデータ
				// ; ( 0~31 )
				int flash1 = clOHHB_V23.getWork(Defines.DEF_FLASH + 1);
				// DfMain.TRACE(("****FLASH+1 A:\t"+(data/32));
				// DfMain.TRACE(("****FLASH+1 B:\t"+(data%32));
				// DfMain.TRACE(("******************************************");
#if __COM_TYPE__
#if	__DEBUG_MENU__
				if( Debug.debug_cnf[DBG_FLASH] == 1)
				{
					Debug.debug_cnf[DBG_FLASH] = 0;
					flash1 = ((Debug.debug_cnf[DBG_FLASH0] << 5) | Debug.debug_cnf[DBG_FLASH1]);
				}
#endif
				// 演出によるチェック
				GPW_eventProcess((int)Defines.EVENT_PROC.EVENT_PROC_CHK_FLASH, flash1);
#endif
				setFlash(flash1 / 32);
				set4th(flash1 % 32);
	
				// 開始音を鳴らす
				int snd_id = Defines.DEF_SOUND_19;
				if (flash1 % 32 > 0) {
					snd_id = Defines.DEF_SOUND_21;
				}
				if (flash0 / 64 > 0) {
					snd_id = Defines.DEF_SOUND_20;
				}


#if __COM_TYPE__
				if( Mobile.isJacCut() == false)
#else
//				if(Mobile.isVaib()){
//					if(IS_BONUS_GAME()){
//						if(flash1 % 32 == 28){
//							ZZ.setVibrator(true);
//						}
//					}else if(!IS_BONUS_JAC()){
//						if(snd_id != Defines.DEF_SOUND_19){
//							ZZ.setVibrator(true);
//						}
//					}
//				}
//				
//				if (!(Mobile.isJacCut() && IS_BONUS_JAC()))
#endif
				{
					playSE(snd_id);
				}
				
				// 回転開始時間を記録する
				_spinTime = Util.GetMilliSeconds() + Defines.DEF_WAIT_SPIN;
				int_s_value[Defines.DEF_INT_IS_REEL_STOPPED] = 0; // リールストップ

				// 告知はSPIN時にクリア
				int_s_value[Defines.DEF_INT_KOKUCHI_X] = 0;

				// ボーナス（ＢＢ・ＲＢ）終了ですか？
				if (int_s_value[Defines.DEF_INT_IS_BB_RB_END] > 0) {
					// ここはボーナス終了ゲームの次ゲームの回転開始です

					if(!IS_HALL()){
						// データパネル更新
						if (int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_B7
								|| int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_R7) { // ＢＢ終了時のみ
							shiftDataPanelHistory(
									int_s_value[Defines.DEF_INT_UNTIL_BONUS_GAMES],
									Defines.DEF_PS_BB_RUN);
						} else if (int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_RB_IN) { // ＲＢ終了時のみ
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
/**/				hallData[Defines.DEF_H_BNS_0] = 0;

					
					// TOBE 個別PARAM　BB 終了後の１ゲーム目ですよフラグを立てる
					int_s_value[Defines.DEF_INT_BB_AFTER_1GAME] = 1;
					
				}

				if (!IS_BONUS()) {
					// 通常ゲーム中

					// ボーナス消化中のゲーム数を総回転数としてカウントしない
					int_s_value[Defines.DEF_INT_TOTAL_GAMES]++;
					
					// 総回転数を増やす
					//DfMain.TRACE(("総回転数のカウントアップ");
					gp.onCountUp();
					
/**/				hallData[Defines.DEF_H_GAME_COUNT]++;

					// ボーナス間累積（ゲーム情報）
					int_s_value[Defines.DEF_INT_UNTIL_BONUS_GAMES]++;
/**/				hallData[Defines.DEF_H_BNS_0]++;
				}

				// データパネル情報更新（ゲーム数）
				// データパネル情報更新（ゲーム数）
				if(IS_HALL()){
					setCurrentDataPanel(hallData[Defines.DEF_H_BNS_0]);
				}else{
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

                // TODO C#移植 ホール処理も不要なのでコメントアウト
                //if(IS_HALL() && !Defines.DEF_IS_VODA){
                //    Mobile.saveMenuData(true);//不正防止用にここで保存
                //}

                GameManager.Instance.OnStartPlay();

				break;
			// TOBE [=4.RMODE_FLASH init]
			case Defines.DEF_RMODE_FLASH: // (モード初期)
				break;
			// TOBE [=4.RMODE_RESULT init]
			case Defines.DEF_RMODE_RESULT: // (モード初期)
				// RESULTに入った時間を記録
				_lampTime = Util.GetMilliSeconds() + Defines.DEF_WAIT_LAMP;

				// 払い出しコイン枚数
				int_s_value[Defines.DEF_INT_WIN_COIN_NUM] = clOHHB_V23.mPayMedal();

				int_s_value[Defines.DEF_INT_TOTAL_PAY] += int_s_value[Defines.DEF_INT_WIN_COIN_NUM];
/**/			hallData[Defines.DEF_H_MEDAL_OUT] += int_s_value[Defines.DEF_INT_WIN_COIN_NUM];
				

				if ((clOHHB_V23.getWork(Defines.DEF_HITFLAG) & Defines.DEF_HITFLAG_NR_RB)!=0)
				{
					Defines.TRACE("RB入賞時");
					bigBonusFg = false;
// satoh BB
//					gp.onBonusRB();
				}
				else if ((clOHHB_V23.getWork(Defines.DEF_HITFLAG) & Defines.DEF_HITFLAG_NR_BB)!=0) {
					Defines.TRACE("BB入賞時");
					bigBonusFg = true;
// satoh BB
//					gp.onBonusBB();
				}
				//DfMain.TRACE(("払い出し分加算");
				if (!IS_BONUS()) {
					if ((clOHHB_V23.getWork(Defines.DEF_HITFLAG) & 0x01) != 0) {
						int_s_value[Defines.DEF_INT_CHRY_GOT]++;
					} else if ((clOHHB_V23.getWork(Defines.DEF_HITFLAG) & 0x04) != 0) {
						int_s_value[Defines.DEF_INT_WMLN_GOT]++;
					}
				} else if (IS_BONUS_GAME()) {
					// ビタ外し成功
					if (clOHHB_V23.getWork(Defines.DEF_HITFLAG) == 0
							&& ((clOHHB_V23.getWork(Defines.DEF_WAVEBIT) & 0x08) != 0)
							&& (clOHHB_V23.getWork(Defines.DEF_ARAY11) == Defines.DEF_BAR_)) {
						int_s_value[Defines.DEF_INT_HAZUSI_COUNT]++;
					}
				}

				int_s_value[Defines.DEF_INT_BONUS_GOT] += int_s_value[Defines.DEF_INT_WIN_COIN_NUM];
				
				if( (IS_BONUS_JAC() == true) )
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
				if (int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_R7) {
					_soundTime = Util.GetMilliSeconds() + Defines.DEF_SOUND_MS_03; // ﾌｧﾝﾌｧｰﾚ完奏時間設定
					playBGM(Defines.DEF_SOUND_03, false); // BBﾌｧﾝﾌｧｰﾚ1(ﾄﾞﾝﾁｬﾝ揃い)
				} else if (int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_B7) {
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

				bonusEndFg = clOHHB_V23.mBonusCounter();

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
				if( IS_BONUS() == true)
				{	// ボーナス終了フラグじゃないとき
					if( cutBonusSystem(0) )
					{	// ﾎﾞｰﾅｽｶｯﾄ処理が必要の場合
						
						// カット処理フラグON
						BonusCutFg = true;
						
						if ((int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_B7)
							|| (int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_R7))
						{ // ＢＢ終了判定(RBの場合はRB終了時にメダル加算を行なう)
							if( (IS_BONUS_JAC() == true) )
							{
								int num;
								num = 0;
								// JACゲームの獲得枚数
								if(int_s_value[Defines.DEF_INT_BONUS_JAC_GOT] < Defines.JAC_BONUS_AVENUM)
								{	// JACゲームのカット枚数を加算する
									num = (Defines.JAC_BONUS_AVENUM - int_s_value[Defines.DEF_INT_BONUS_JAC_GOT]);
									int_s_value[Defines.DEF_INT_BONUS_JAC_GOT] = 0;	// JAC中の獲得枚数をクリア
									int_s_value[Defines.DEF_INT_BONUS_GOT] += num;
								}
								Defines.TRACE("JACカット分を追加:" + num);
#if __ERR_MSG__
								if( (num < 0) || (num > JAC_BONUS_AVENUM))
								{	// 0以下ならば
									SET_ERR_CODE(ERR_CODE_JAC_CUT);
									SET_ERR_OPTION(num);
								}
#endif
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
						
						bonusEndFg = clOHHB_V23.mBonusCounter();
						
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
				
				if (bonusEndFg != 0) {
#else
				if (clOHHB_V23.mBonusCounter() != 0) {
#endif
					
#if __COM_TYPE__
					BonusEnd(0);
#else
//					// ボーナス終了！！
//					DfMain.TRACE(("ボーナス終了！！:" + int_s_value[Defines.DEF_INT_BONUS_GOT]);
//					// mBonusCounter()内部でclearWork(Defines.DEF_CLR_AREA_2)を実行！
//
//					// ＪＡＣ ＆ ＢＢ終了時にここを通る
//					// ＢＢ・ＲＢが終了したことにするためここでフラグを初期化処理する
//					int_s_value[Defines.DEF_INT_IS_BB_RB_END] = 1;
//
//					Mobile.stopSound(Defines.DEF_SOUND_MULTI_BGM); // BGMを止める
//
//					if ((int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_B7)
//							|| (int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_R7)) { // ＢＢ終了判定
//
//						_soundTime = Util.GetMilliSeconds() + Defines.DEF_SOUND_MS_09; // ﾌｧﾝﾌｧｰﾚ完奏時間設定
//						playBGM(Defines.DEF_SOUND_09, false); // BBEND音
//
//#if __COM_TYPE__
//	#if __BONUS_CUT__
//						if( cutBonus() == 1)
//						{	// ボーナスカットオールの場合限定
//							if( int_s_value[Defines.DEF_INT_BONUS_GOT] < BIG_BONUS_AVENUM)
//							{
//								int num;
//								num = (BIG_BONUS_AVENUM - int_s_value[Defines.DEF_INT_BONUS_GOT]);
//								int_s_value[Defines.DEF_INT_BONUS_GOT] += num;
//								GPW_chgCredit(num);
//							}
//						}
//	#else
//
//	#endif
//#endif
//						// 消化中の使用コイン数があるため、－枚は０枚にしておく
//						int_s_value[Defines.DEF_INT_BONUS_GOT] = Math.max(0,int_s_value[Defines.DEF_INT_BONUS_GOT]);
//
//						int_s_value[Defines.DEF_INT_BB_TOTAL_GOT] += int_s_value[Defines.DEF_INT_BONUS_GOT];
//#if __COM_TYPE__
//#else
////						// ＢＢ最高獲得数を更新
////						int_s_value[Defines.DEF_INT_GAME_INFO_MAX_GOT] = Math.max(
////								int_s_value[Defines.DEF_INT_GAME_INFO_MAX_GOT],
////								int_s_value[Defines.DEF_INT_BONUS_GOT]);
////#if __OHANA_DEBUG__
////						// TOBE 個別PARAM デバッグ　７１１枚超えの獲得にする
////						if(Defines.DEF_IS_DEBUG_MISSION_PARAM){if(is711){int_s_value[Defines.DEF_INT_GAME_INFO_MAX_GOT] += Defines.DEF_BB_GET_711;}}
////#endif
////						// TOBE 個別PARAM　BB獲得枚数が711枚以上か？
////						if(int_s_value[Defines.DEF_INT_GAME_INFO_MAX_GOT] >= Defines.DEF_BB_GET_711){
////							int_s_value[Defines.DEF_INT_BB_GET_OVER711] = 1;
////						}
//#endif
//
//						DfMain.TRACE(("BBボーナス終了？");
//						// ボーナス
//						gp.onBonusEND();
//					}
//					else if (int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_RB_IN)
//					{
//						DfMain.TRACE(("RBボーナス終了？");
//	#if __BONUS_CUT__
//						if( int_s_value[Defines.DEF_INT_BONUS_GOT] < REG_BONUS_AVENUM)
//						{
//							int_s_value[Defines.DEF_INT_BONUS_GOT] = REG_BONUS_AVENUM;
//							GPW_chgCredit(REG_BONUS_AVENUM);
//						}
//	#else
//
//	#endif
//						// ボーナス
//						gp.onBonusEND();
//					}
//					break;
#endif
				}
				break; // 抜ける
			// TOBE [=RMODE_NO_COIN init}
			case Defines.DEF_RMODE_NO_COIN: // (モード初期)
				return true; // コインなしで終了通知
			} // E-O-各モードでの初期化
			break;
		} // end of while

		if (int_s_value[Defines.DEF_INT_PREV_GAMEST] != clOHHB_V23.getWork(Defines.DEF_GAMEST)) {
			int_s_value[Defines.DEF_INT_PREV_GAMEST] = clOHHB_V23.getWork(Defines.DEF_GAMEST);
			if ((int_s_value[Defines.DEF_INT_PREV_GAMEST] & 0x01) != 0) {
				// TODO JACBGMを鳴らす
				playBGM(Defines.DEF_SOUND_05, true);
			} else if ((int_s_value[Defines.DEF_INT_PREV_GAMEST] & 0x80) != 0) {
				// TODO BIGBGMを鳴らす
				if (int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_R7) {
					playBGM(Defines.DEF_SOUND_07, true);
				} else if (int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_BB_B7) {
					playBGM(Defines.DEF_SOUND_06, true);
				}
			}
		}
	
		int_s_value[Defines.DEF_INT_MODE_COUNTER]++; // モードが切り替わってからの累積カウンタ

		ctrlTopLamp();
		// // 演出ＯＮ・ＯＦＦフラグ切り替え（ここで演出の更新タイミングを調整する）
		// if ((int_s_value[Defines.DEF_INT_MODE_COUNTER] % (Defines.DEF_WAIT_ON_OFF_LAMP
		// / int_s_value[Defines.DEF_INT_LOOP_SPEED] + 1)) == 0) {
		// int_s_value[Defines.DEF_INT_ON_OFF_EFFECT] =
		// (int_s_value[Defines.DEF_INT_ON_OFF_EFFECT] > 0) ? 0
		// : 1;
		// }
		int_s_value[Defines.DEF_INT_ON_OFF_EFFECT] = Util.GetMilliSeconds() % 1000 > 500 ? 1
				: 0;

		ctrlLamp();
		ctrlBetLamp();

        DoModeAction(keyTrigger);
		return false;
	} // process()

    private void DoModeAction(int keyTrigger) {

        // ======================================
        // 各モードにおける毎回の処理
        // ======================================
        //DfMain.TRACE(("リプレイある(毎回)？" + IS_REPLAY());
        //DfMain.TRACE(("Defines.DEF_INT_CURRENT_MODE:" + int_s_value[Defines.DEF_INT_CURRENT_MODE] + ":" + (clOHHB_V23.getWork(Defines.DEF_GAMEST)&0xFFFF));
        switch (int_s_value[Defines.DEF_INT_CURRENT_MODE]) {
            // TOBE [=0.RMODE_WAIT rp]
            case Defines.DEF_RMODE_WAIT: // （毎回処理）
                // 直前の停止音の完奏を待つ
                if (_soundTime < Util.GetMilliSeconds()) {
                    // ﾘﾌﾟﾚｲが揃っているときは、自動的にRMODE_BETまで遷移する
#if __COM_TYPE__

                    if (bgm_resumeFg == true) {	// 休憩中からの復帰
                        if (bgm_no != -1) {
                            // サウンド
                            //if (!IS_BONUS()) {
                            //	// ボーナス消化中以外は毎回停止指示。
                            //	Mobile.stopSound(Defines.DEF_SOUND_UNDEF);
                            //}
                            if (IS_BONUS()) {	// ボーナス時限定
                                Defines.TRACE("復帰サウンドの再生");
                                playBGM(bgm_no, bgm_loop); // 復帰サウンドの再生
                                bgm_resumeFg = false;
                            } else {
                                // ボーナス消化中以外は毎回停止指示。
                                Mobile.stopSound(Defines.DEF_SOUND_UNDEF);
                            }
                        }
                    }

                    gp.betFlag = false;
                    if (IS_REPLAY()) {
                        gp.betFlag = true;
                        REQ_MODE(Defines.DEF_RMODE_BET); // MAXBETへ遷移
                    } else {
                        // BET開始
                        if ((keyTrigger & (Defines.DEF_KEY_BIT_SELECT | Defines.DEF_KEY_BIT_5)) != 0) {
                            if (IS_BONUS_JAC()) {
                                int_s_value[Defines.DEF_INT_BET_COUNT] = 1;
                            } else {
                                int_s_value[Defines.DEF_INT_BET_COUNT] = 3;
                            }

                            //DfMain.TRACE(("枚数チェック:" + int_s_value[Defines.DEF_INT_SLOT_COIN_NUM]+ ":" + int_s_value[Defines.DEF_INT_BETTED_COUNT] + ":" + int_s_value[Defines.DEF_INT_BET_COUNT]);
                            if (int_s_value[Defines.DEF_INT_SLOT_COIN_NUM] < int_s_value[Defines.DEF_INT_BET_COUNT]) {	// コインがないからBETさせない
                                gp.onCreditZero();
                            } else {
                                //							if (int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM] < int_s_value[Defines.DEF_INT_BET_COUNT])
                                //							{	// 筐体内クレジット描画
                                //								int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM] = int_s_value[Defines.DEF_INT_SLOT_COIN_NUM];
                                //								if( int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM] > Defines.DEF_NUM_MAX_CREDIT)
                                //								{	// 最大数に丸め込む
                                //									int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM] = Defines.DEF_NUM_MAX_CREDIT;
                                //								}
                                //							}

                                REQ_MODE(Defines.DEF_RMODE_BET);
                                gp.betFlag = true;
                            }
                        }
                    }
#else
				if (IS_REPLAY()) {
					REQ_MODE(Defines.DEF_RMODE_BET); // MAXBETへ遷移
				} else {
					// BET開始
					if ((keyTrigger & (Defines.DEF_KEY_BIT_SELECT | Defines.DEF_KEY_BIT_5)) != 0) {
						if (IS_BONUS_JAC()) {
							int_s_value[Defines.DEF_INT_BET_COUNT] = 1;
						} else {
							int_s_value[Defines.DEF_INT_BET_COUNT] = 3;
						}
					}
					else if ((keyTrigger & (Defines.DEF_KEY_BIT_RIGHT)) != 0) {
						int_s_value[Defines.DEF_INT_BET_COUNT] = 1;
						REQ_MODE(Defines.DEF_RMODE_BET);
					}
				}
#endif
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
#if __COM_TYPE__

#else
//			if (_soundTime < Util.GetMilliSeconds()) {
//				// BET枚数変更ができる
//				if (!IS_REPLAY() && !IS_BONUS_JAC()) {
//					if ((keyTrigger & (Defines.DEF_KEY_BIT_RIGHT)) != 0) {
//						int_s_value[Defines.DEF_INT_BET_COUNT]++;
//						gp.onCreditUp();
//						if (int_s_value[Defines.DEF_INT_BET_COUNT] > betMax) {
//							int_s_value[Defines.DEF_INT_BET_COUNT] = betMax;
//							gp.onCreditUse();
//						} else {
//							playSE(Defines.DEF_SOUND_25);
//							_soundTime = Util.GetMilliSeconds()
//									+ Defines.DEF_SOUND_MS_25;
//						}
//					} else if ((keyTrigger & (Defines.DEF_KEY_BIT_LEFT)) != 0) {
//						int_s_value[Defines.DEF_INT_BET_COUNT]--;
//						gp.onCreditUse();
//						if (int_s_value[Defines.DEF_INT_BET_COUNT] < 1) {
//							int_s_value[Defines.DEF_INT_BET_COUNT] = 1;
//							gp.onCreditUp();
//						} else {
//							playSE(Defines.DEF_SOUND_25);
//							_soundTime = Util.GetMilliSeconds()
//									+ Defines.DEF_SOUND_MS_25;
//						}
//					}
//				}
//				// ↑ココマデでBET枚数が確定
//			}
//
//			if (!IS_REPLAY()) {
//				// 特定の枚数以上あるかどうか
//				//DfMain.TRACE(("枚数チェック:" + int_s_value[Defines.DEF_INT_SLOT_COIN_NUM]+ ":" + int_s_value[Defines.DEF_INT_BETTED_COUNT] + ":" + int_s_value[Defines.DEF_INT_BET_COUNT]);
//				if (int_s_value[Defines.DEF_INT_SLOT_COIN_NUM]
//						+ int_s_value[Defines.DEF_INT_BETTED_COUNT] < int_s_value[Defines.DEF_INT_BET_COUNT]) {
//							DfMain.TRACE(("コインがある");
//					int_s_value[Defines.DEF_INT_NUM_KASIDASI]++;
//					int_s_value[Defines.DEF_INT_SLOT_COIN_NUM] += Defines.DEF_NUM_START_COIN;
//				}
//				
//				if(IS_HALL()){/*プレーヤーコイン要求*/
//					if(hallData[Defines.DEF_H_PLAYER_COIN] < int_s_value[Defines.DEF_INT_BET_COUNT]) {
//						prevkasidasiMedal = hallData[Defines.DEF_H_PLAYER_COIN];
//						DfMain.TRACE(("コインがない");
//						//貸し出し処理
//						//int_s_value[Defines.DEF_INT_WHAT_EXIT] = Defines.DEF_EXIT_HALL_COIN;
//						return true;
//					}
//				}
//			}
#endif
                // 描画を増やす
                if (int_s_value[Defines.DEF_INT_BETTED_COUNT] < int_s_value[Defines.DEF_INT_BET_COUNT]) {
                    // ココのタイミングの取り方は40msでループするのが前提
                    if (int_s_value[Defines.DEF_INT_MODE_COUNTER] % 2 == 0) {
                        if (!IS_REPLAY()) {
                            // クレジットから減らす
                            if (int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM] > 0) {
                                int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM]--;
                            }
                            int_s_value[Defines.DEF_INT_SLOT_COIN_NUM]--;

                            // BETした分だけ減算する
                            GPW_chgCredit(-1);

                            // コイン投入時処理
                            GameManager.Instance.OnCoinInsert();
                        }
                        int_s_value[Defines.DEF_INT_BONUS_GOT]--;
                        int_s_value[Defines.DEF_INT_BETTED_COUNT]++;


                        //GPW_chgCredit(0 - int_s_value[Defines.DEF_INT_BETTED_COUNT]);
                    }
                }
                    // 減らす
                else if (int_s_value[Defines.DEF_INT_BETTED_COUNT] > int_s_value[Defines.DEF_INT_BET_COUNT]) {
                    // ココのタイミングの取り方は40msでループするのが前提
                    if (int_s_value[Defines.DEF_INT_MODE_COUNTER] % 2 == 0) {
                        if (!IS_REPLAY()) {
                            if (int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM] < Defines.DEF_NUM_MAX_CREDIT) {
                                int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM]++;
                            }
                            int_s_value[Defines.DEF_INT_SLOT_COIN_NUM]++;
                        }
                        int_s_value[Defines.DEF_INT_BONUS_GOT]++;
                        int_s_value[Defines.DEF_INT_BETTED_COUNT]--;
                    }
                } else {
                    if (_soundTime < Util.GetMilliSeconds()) {
                        // 回転開始
#if __COM_TYPE__
                        //DfMain.TRACE(("回転開始チェック:" + int_s_value[Defines.DEF_INT_BETTED_COUNT]);
                        if ((int_s_value[Defines.DEF_INT_BETTED_COUNT] > 0)
                            && (((keyTrigger & (Defines.DEF_KEY_BIT_SELECT | Defines.DEF_KEY_BIT_5)) != 0)
                            || (reelStartFg == true))) {
                            //DfMain.TRACE(("回転開始ウェイト");
                            reelStartFg = true;
                            lampSwitch(Defines.DEF_LAMP_LEVER, Defines.DEF_LAMP_ACTION_ON);
#if __REEL_WEIT_SKIP__
						reelwait = -3200;
#else
                            if (Mobile.isJacCut() == true) {	// ボーナスカットの場合
                                reelwait = -3200;
                            }
                            if ((reelwait + 3200) < Util.GetMilliSeconds()) {	// リールウェイト
                                //DfMain.TRACE(("回転開始");
                                REQ_MODE(Defines.DEF_RMODE_SPIN);
                                //DfMain.TRACE(("リールウェイト:" + (Util.GetMilliSeconds() - reelwait));
                                reelwait = Util.GetMilliSeconds();//リール全体用
                                reelStartFg = false;
                            }
#endif

                        }
#else
					if ( (int_s_value[Defines.DEF_INT_BETTED_COUNT] > 0)
						&& ((keyTrigger & (Defines.DEF_KEY_BIT_SELECT | Defines.DEF_KEY_BIT_5 | Defines.DEF_KEY_BIT_DOWN)) != 0)) {
						lampSwitch(Defines.DEF_LAMP_LEVER, Defines.DEF_LAMP_ACTION_ON);
						REQ_MODE(Defines.DEF_RMODE_SPIN);
					}
#endif
                    }
                }
                break;
                #endregion
            // TOBE [=2.RMODE_SPIN rp]
            case Defines.DEF_RMODE_SPIN: // 回転中（毎回処理）
                // 4thをまわす。
                action4th();
                // 全部止まったらモード変わる
                if (int_s_value[Defines.DEF_INT_IS_REEL_STOPPED] == 7) {
                    // 停止音 完奏を待って次のモードへ遷移
                    if (_soundTime < Util.GetMilliSeconds()) {
                        // 4thが止まったら
                        if (int_s_value[Defines.DEF_INT_4TH_ACTION_FLAG] == 0) {
                            if (isPlay()) {
                                REQ_MODE(Defines.DEF_RMODE_FLASH);
                            } else {
                                REQ_MODE(Defines.DEF_RMODE_WIN);
                            }
                        }
                    }
                    break;
                }

                // 前に鳴らしたサウンド待ち
                if (_soundTime < Util.GetMilliSeconds()
                        || int_s_value[Defines.DEF_INT_IS_REEL_STOPPED] != 0) {
                    if (int_s_value[Defines.DEF_INT_KEY_REJECT] > 0) {
                        int_s_value[Defines.DEF_INT_KEY_REJECT]--; // 一定ターン待つ
                    } else {
#if __COM_TYPE__

#else
					if(Mobile.isVaib()){
						ZZ.setVibrator(false);
					}
#endif
                        // 同時押しは出来ない ワンボタン(KEY_5)操作あり
                        bool isSpinning = true;
                        bool isLimitStop = false; // 自動停止の場合用
                        //DfMain.TRACE(("ここから開始");
#if __COM_TYPE__
                        if (_spinTime < Util.GetMilliSeconds()) {
                            Defines.TRACE(Util.GetMilliSeconds() - _spinTime);
                            isLimitStop = true;
                        }
                        // 自動停止もあり
                        if ((keyTrigger & (Defines.DEF_KEY_BIT_SELECT | Defines.DEF_KEY_BIT_5)) != 0
                            || (isLimitStop == true)) {
                            int tmp;
                            for (int i = 0; i < Defines.DEF_N_REELS; i++) {
                                if (isSpinning) {
                                    // 押し順の変更
                                    tmp = getStopReel(i, isLimitStop);
                                    isSpinning = setReelStopAngle(tmp);
                                    //DfMain.TRACE(("停止フラグ:" + isSpinning);
                                }
                            }
                        } else {
                            if (isSpinning
                                    && (keyTrigger & (Defines.DEF_KEY_BIT_1)) != 0) {
                                isSpinning = setReelStopAngle(0);
                            }
                            if (isSpinning
                                    && (keyTrigger & (Defines.DEF_KEY_BIT_2)) != 0) {
                                isSpinning = setReelStopAngle(1);
                            }
                            if (isSpinning
                                    && (keyTrigger & (Defines.DEF_KEY_BIT_3)) != 0) {
                                isSpinning = setReelStopAngle(2);
                            }
                        }
#else
/////////////////////
//					if ((keyTrigger & (Defines.DEF_KEY_BIT_SELECT | Defines.DEF_KEY_BIT_5)) != 0
//							|| _spinTime < Util.GetMilliSeconds() )
//					{
//						int tmp;
//						for (int i = 0; i < Defines.DEF_N_REELS; i++) {
//							if (isSpinning) {
//								if(debugAuto){
//									if(IS_BONUS_GAME() && (clOHHB_V23.getWork(Defines.DEF_WAVEBIT)&0x08)!=0
//										
//											&& clOHHB_V23.getWork(Defines.DEF_BIGBCTR) == 1 && clOHHB_V23.getWork(Defines.DEF_BBGMCTR) > 8){
//										isSpinning = setReelStopAngle(new int[]{2,1,0}[i]);
//									}else{
//										isSpinning = setReelStopAngle(Mobile.getOrder(i));
//									}
//								}else{
//									isSpinning = setReelStopAngle(Mobile.getOrder(i));
//								}
//							}
//						}
//					} else {
//						if (isSpinning
//								&& (keyTrigger & (Defines.DEF_KEY_BIT_1)) != 0) {
//							isSpinning = setReelStopAngle(0);
//						}
//						if (isSpinning
//								&& (keyTrigger & (Defines.DEF_KEY_BIT_2)) != 0) {
//							isSpinning = setReelStopAngle(1);
//						}
//						if (isSpinning
//								&& (keyTrigger & (Defines.DEF_KEY_BIT_3)) != 0) {
//							isSpinning = setReelStopAngle(2);
//						}
//					}
////
#endif

                    }
                }

                // 停止ボタンを点灯
                ctrlButtonLamp();

                // リールを進める。
                for (int i = 0; i < 3; i++) {
                    // 止まっていたら次
                    if ((int_s_value[Defines.DEF_INT_IS_REEL_STOPPED] & BIT(i)) != 0)
                        continue;
#if __COM_TYPE__
                    if (int_s_value[Defines.DEF_INT_REEL_STOP_R0 + i] != ANGLE_UNDEF
                            && (((int_s_value[Defines.DEF_INT_REEL_STOP_R0 + i] - int_s_value[Defines.DEF_INT_REEL_ANGLE_R0 + i])
                                & ANGLE_2PI_MASK) <= int_s_value[Defines.DEF_INT_REEL_SPEED] ||
                                (Mobile.isJacCut() == true)))
#else
//				if (int_s_value[Defines.DEF_INT_REEL_STOP_R0 + i] != ANGLE_UNDEF
//						&& (((int_s_value[Defines.DEF_INT_REEL_STOP_R0 + i] - int_s_value[Defines.DEF_INT_REEL_ANGLE_R0 + i]) 
//							& ANGLE_2PI_MASK) <= int_s_value[Defines.DEF_INT_REEL_SPEED] ||
//							(Mobile.isJacCut() && IS_BONUS_JAC())))
#endif
 {
                        // 止めにかかる
                        int_s_value[Defines.DEF_INT_REEL_ANGLE_R0 + i] = int_s_value[Defines.DEF_INT_REEL_STOP_R0 + i];
                        // 止まった
                        int_s_value[Defines.DEF_INT_IS_REEL_STOPPED] |= BIT(i);

                        int_s_value[Defines.DEF_INT_KEY_REJECT] = 1;
                        // 次のボタンが押せるようにする
                        int stop_snd_id = Defines.DEF_SOUND_23;

                        _soundTime = Util.GetMilliSeconds() + Defines.DEF_SOUND_MS_23 / 2;
                        if (!IS_BONUS()) {
                            int_s_value[Defines.DEF_INT_IS_TEMPAI] = 0;
                            int[] tempai = isTempai();
                            switch (clOHHB_V23.getWork(Defines.DEF_PUSHCTR)) {
                                case 0x02:// 第1停止
#if __COM_TYPE__
                                    if (i == 0) {	// 停止リールが左リールの時
                                        GPW_eventProcess((int)Defines.EVENT_PROC.EVENT_PROC_CHK_REEL, (int)Defines.EVENT.EVENT_NO1);
                                    }
#endif



                                    break;
                                case 0x01:// 第2停止
                                    if (tempai[1] == 3) {
                                        //ﾄﾘﾌﾟﾙﾃﾝﾊﾟｲ音
#if __COM_TYPE__
                                        // (トリプルテンパイ（BIG確）)
                                        GPW_eventProcess((int)Defines.EVENT_PROC.EVENT_PROC_CHK_REEL, (int)Defines.EVENT.EVENT_NO2);
#endif
                                        stop_snd_id = Defines.DEF_SOUND_15;
                                        _soundTime = Util.GetMilliSeconds() + Defines.DEF_SOUND_MS_15;
                                        int_s_value[Defines.DEF_INT_IS_TEMPAI] = 1;
                                    } else if (tempai[0] != Defines.DEF_BB_UNDEF) {
                                        stop_snd_id = Defines.DEF_SOUND_14;
                                        _soundTime = Util.GetMilliSeconds() + Defines.DEF_SOUND_MS_14;
                                        int_s_value[Defines.DEF_INT_IS_TEMPAI] = 1;
                                    }
                                    break;
                                case 0x00:// 第三停止
                                    if (tempai[0] == Defines.DEF_BB_B7) {
                                        int_s_value[Defines.DEF_INT_BB_KIND] = Defines.DEF_BB_B7;
                                        //TOBE 個別PARAM ＢＢ揃ったかの判定									
                                        if (int_s_value[Defines.DEF_INT_BB_AFTER_1GAME] > 0) {
                                            int_s_value[Defines.DEF_INT_BB_END_1GAME_REGET_BB] = 1; // 揃えた
                                            if (Defines.DEF_IS_DEBUG_MISSION_PARAM) { Defines.TRACE("1ゲーム目で青七をそろえた"); }
                                        }
                                        gp.onBonusBB();
                                    } else if (tempai[0] == Defines.DEF_BB_R7) {
                                        int_s_value[Defines.DEF_INT_BB_KIND] = Defines.DEF_BB_R7;

                                        //TOBE 個別PARAM ＢＢ揃ったかの判定									
                                        if (int_s_value[Defines.DEF_INT_BB_AFTER_1GAME] > 0) {
                                            int_s_value[Defines.DEF_INT_BB_END_1GAME_REGET_BB] = 1; // 揃えた									
                                            if (Defines.DEF_IS_DEBUG_MISSION_PARAM) { Defines.TRACE("1ゲーム目で赤ﾄﾞﾝをそろえた"); }
                                        }
                                        gp.onBonusBB();
                                    }
#if __COM_TYPE__
                                    // ゲチェナ
                                    GPW_eventProcess((int)Defines.EVENT_PROC.EVENT_PROC_CHK_REEL, (int)Defines.EVENT.EVENT_NO3);
#endif
                                    int_s_value[Defines.DEF_INT_BB_AFTER_1GAME] = 0;// TOBE 個別PARAM用フラグで使うフラグを必ず下ろす
                                    break;
                            }
                        }
#if __COM_TYPE__
                        if ((Mobile.isJacCut() == false))
#else
//					if (!(Mobile.isJacCut() && IS_BONUS_JAC()))
#endif
 {
                            playSE(stop_snd_id);
                        }

                    } else {
#if __DEBUG_MENU__
					if( Debug.debug_cnf[DBG_MANUAL] == 1)
					{
						//40秒停止を無効化する
						// 回転開始時間を記録する
						_spinTime = Util.GetMilliSeconds() + Defines.DEF_WAIT_SPIN;
						if( CHECK_FLAG(keyTrigger, Defines.DEF_KEY_BIT_UP) )
						{
							int_s_value[Defines.DEF_INT_REEL_ANGLE_R0 + i] = 
								(int_s_value[Defines.DEF_INT_REEL_ANGLE_R0 + i] + int_s_value[Defines.DEF_INT_REEL_SPEED]) & ANGLE_2PI_MASK;
						}
					}
					else
					{
						int_s_value[Defines.DEF_INT_REEL_ANGLE_R0 + i] = 
						(int_s_value[Defines.DEF_INT_REEL_ANGLE_R0 + i] + int_s_value[Defines.DEF_INT_REEL_SPEED]) & ANGLE_2PI_MASK;
					}
#else
                        int_s_value[Defines.DEF_INT_REEL_ANGLE_R0 + i] =
                            (int_s_value[Defines.DEF_INT_REEL_ANGLE_R0 + i] + int_s_value[Defines.DEF_INT_REEL_SPEED]) & ANGLE_2PI_MASK;
#endif
                    }
                }

                break;
            // TOBE [=3.RMODE_FLASH rp]
            case Defines.DEF_RMODE_FLASH: // 結果（毎回処理）
                //スピード調整
                if (ZZ.getThreadSpeed() < 40
                        && int_s_value[Defines.DEF_INT_MODE_COUNTER] % 2 == 0) {
                    break;
                }
                if (isPlay()) {
                    int_s_value[Defines.DEF_INT_FLASH_DATA] = getNext();
                    // リールフラッシュ以外の部分
                    if ((int_s_value[Defines.DEF_INT_FLASH_DATA] & (1 << 10)) != 0) {
                        lampSwitch(Defines.DEF_LAMP_4TH, Defines.DEF_LAMP_ACTION_ON);
                    } else {
                        lampSwitch(Defines.DEF_LAMP_4TH, Defines.DEF_LAMP_ACTION_OFF);
                    }
                } else {
                    REQ_MODE(Defines.DEF_RMODE_WIN);
                }
                break;
            case Defines.DEF_RMODE_WIN:
                if (_soundTime < Util.GetMilliSeconds()) {
                    REQ_MODE(Defines.DEF_RMODE_RESULT);
                }
                break;
            // TOBE [=4.RMODE_RESULT rp]
            case Defines.DEF_RMODE_RESULT: // 結果（毎回処理）
#if __COM_TYPE__
                if ((Mobile.isJacCut() == true))
#else
//			if (Mobile.isJacCut() && IS_BONUS_JAC())
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
                    if (int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM] > Defines.DEF_NUM_MAX_CREDIT) {
                        int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM] = Defines.DEF_NUM_MAX_CREDIT;
                    }
                    // ＭＡＸを超えないように。
                    if (int_s_value[Defines.DEF_INT_SLOT_COIN_NUM] > Defines.DEF_NUM_MAX_COIN) {
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
                } else {
                    // 一枚一枚移す
                    // satoh#暫定
                    if (int_s_value[Defines.DEF_INT_WIN_GET_COIN] < int_s_value[Defines.DEF_INT_WIN_COIN_NUM]) {
                        if ((int_s_value[Defines.DEF_INT_MODE_COUNTER] % (Defines.DEF_WAIT_COUNT_UP / int_s_value[Defines.DEF_INT_LOOP_SPEED] + 1)) == 0) {
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
                            if (int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM] < Defines.DEF_NUM_MAX_CREDIT) {
                                int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM]++;
                            }
                            int_s_value[Defines.DEF_INT_SLOT_COIN_NUM]++;
                            /**/
                            hallData[Defines.DEF_H_PLAYER_COIN]++;
                            // 表示用は個々で増やす
                            int_s_value[Defines.DEF_INT_WIN_GET_COIN]++;
                            // ＭＡＸを超えないように。
                            if (int_s_value[Defines.DEF_INT_SLOT_COIN_NUM] > Defines.DEF_NUM_MAX_COIN) {
                                int_s_value[Defines.DEF_INT_SLOT_COIN_NUM] = Defines.DEF_NUM_MAX_COIN;
                            }
                        }
                        break;
                    }
                }

                // 払い出し音を待つ
                if (_soundTime < Util.GetMilliSeconds()
                        || (int_s_value[Defines.DEF_INT_WIN_COIN_NUM] <= int_s_value[Defines.DEF_INT_WIN_GET_COIN] && !IS_REPLAY())) {
#if __COM_TYPE__
#else
//				// 後告知なら、ここで出現
//				if (Mobile.getKokuchi() == Defines.DEF_SELECT_3_AFFTER) {
//					m_nYokoku2[1] = -1;
//					ForwordYokoku();
//				}
#endif
                    Mobile.stopSound(Defines.DEF_SOUND_MULTI_SE);
                    _soundTime = Util.GetMilliSeconds();
                    // REG入賞
                    if ((clOHHB_V23.getWork(Defines.DEF_HITFLAG) & Defines.DEF_HITFLAG_NR_RB) != 0) {
                        int_s_value[Defines.DEF_INT_BONUS_GOT] = 15;
                        int_s_value[Defines.DEF_INT_REG_COUNT]++;
                        int_s_value[Defines.DEF_INT_FLAG_GAME_COUNT] += int_s_value[Defines.DEF_INT_THIS_FLAG_GAME];
                        int_s_value[Defines.DEF_INT_THIS_FLAG_GAME] = 0;
                        hallData[Defines.DEF_H_RB_COUNT]++;/*HALL*/

                        // Jac-in 突入
                        int_s_value[Defines.DEF_INT_BB_KIND] = Defines.DEF_RB_IN;
                        REQ_MODE(Defines.DEF_RMODE_RB_FANFARE); // ＲＢファンファーレへ遷移

                        Defines.TRACE("REG入賞処理");
                        gp.onBonusRB();
                        break;
                    }
                        // BIG入賞
                    else if ((clOHHB_V23.getWork(Defines.DEF_HITFLAG) & Defines.DEF_HITFLAG_NR_BB) != 0) {
                        Defines.TRACE("BB入賞処理");
                        int_s_value[Defines.DEF_INT_BONUS_GOT] = 15;
                        int_s_value[Defines.DEF_INT_FLAG_GAME_COUNT] += int_s_value[Defines.DEF_INT_THIS_FLAG_GAME];
                        int_s_value[Defines.DEF_INT_THIS_FLAG_GAME] = 0;
                        int_s_value[Defines.DEF_INT_BIG_COUNT]++;
                        hallData[Defines.DEF_H_BB_COUNT]++;/*HALL*/
                        REQ_MODE(Defines.DEF_RMODE_BB_FANFARE); // ＢＢファンファーレへ遷移

                        // セルフオート停止フラグを立てる
                        GameManager.Instance.StopAutoPlay("BB入賞");
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
                        && int_s_value[Defines.DEF_INT_4TH_ACTION_FLAG] == 0) {
                    REQ_MODE(Defines.DEF_RMODE_FIN_WAIT);
                }
                break;
            // TOBE [=5.RMODE_FIN_WAIT rp]
            case Defines.DEF_RMODE_FIN_WAIT:
                if (int_s_value[Defines.DEF_INT_IS_BB_RB_END] > 0
                        && int_s_value[Defines.DEF_INT_BB_KIND] == Defines.DEF_RB_IN) {
                    int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] += Defines.DEF_POS_4TH_TOTAL_W - 20;
                    int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] %= Defines.DEF_POS_4TH_TOTAL_W;
                    if (Defines.DEF_RP19 - 20 < int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE]
                            && int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] <= Defines.DEF_RP19 + 20) {
                        int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] = Defines.DEF_RP19;
                        //					REQ_MODE(Defines.DEF_RMODE_WAIT); // 回転待ちへ遷移
                    } else {
                        break;
                    }
                }
                // 直前の音の完奏を待つ
                if (_soundTime < Util.GetMilliSeconds()) {
                    if (IS_HALL()) {
                        //ﾎﾞｰﾅｽ終了時通信
                        if (int_s_value[Defines.DEF_INT_IS_BB_RB_END] == 1) {
                            hallData[Defines.DEF_H_APPLI_REQ] = Defines.DEF_HRQ_BNSEND;
                            REQ_MODE(Defines.DEF_RMODE_HTTP);
                            // REG入賞時通信
                        } else if ((clOHHB_V23.getWork(Defines.DEF_HITFLAG) & Defines.DEF_HITFLAG_NR_RB) != 0) {
                            hallData[Defines.DEF_H_APPLI_REQ] = Defines.DEF_HRQ_BNSIN;
                            REQ_MODE(Defines.DEF_RMODE_HTTP);
                            // BIG入賞時通信
                        } else if ((clOHHB_V23.getWork(Defines.DEF_HITFLAG) & Defines.DEF_HITFLAG_NR_BB) != 0) {
                            hallData[Defines.DEF_H_APPLI_REQ] = Defines.DEF_HRQ_BNSIN;
                            REQ_MODE(Defines.DEF_RMODE_HTTP);
                            //						//規定ゲーム通信
                            //						}else if(hallData[Defines.DEF_H_GAME_COUNT] - lastHttpGame >= Defines.DEF_HALL_GAME_SPAN){
                            //							hallData[Defines.DEF_H_APPLI_REQ] = Defines.DEF_HRQ_NORMAL;
                            //							REQ_MODE(Defines.DEF_RMODE_HTTP);
                            //10分経過後の最初のゲームで一応通信
                        } else if (prevHttpTime + (5 * 60) < Util.GetMilliSeconds() / 1000) {
                            hallData[Defines.DEF_H_APPLI_REQ] = Defines.DEF_HRQ_NORMAL;
                            REQ_MODE(Defines.DEF_RMODE_HTTP);
                        } else {
                            REQ_MODE(Defines.DEF_RMODE_WAIT); // 回転待ちへ遷移
                        }
                    } else {
                        REQ_MODE(Defines.DEF_RMODE_WAIT); // 回転待ちへ遷移
                    }
                }
                break;
        }    
    }

#if __DRAW_SLOT2__
#else
//	public void paintSlot() {
//		DfMain.TRACE(("★★★★paintSlot★★★★");
//		ZZ.setColor(ZZ.getColor(0, 0, 0));
//		ZZ.fillRect(Defines.DEF_POS_NAVI_C_X, Defines.DEF_POS_NAVI_C_Y + 26 + Defines.GP_DRAW_OFFSET_Y, Defines.DEF_POS_NAVI_C_W,
//				Defines.DEF_POS_NAVI_C_H);
//#if __COM_TYPE__
//#else
//		drawK0();
//#endif
//		drawK1();
//		draw4th();
//		drawK3();
//		if(int_s_value[Defines.DEF_INT_MODE_COUNTER] < 2
//				|| int_s_value[Defines.DEF_INT_CURRENT_MODE] ==Defines.DEF_RMODE_SPIN
//				|| int_s_value[Defines.DEF_INT_CURRENT_MODE] ==Defines.DEF_RMODE_FLASH
//		){
//			//DfMain.TRACE(("drawSlot");
//			drawSlot();
//		}
//		drawK7();
//		// drawKokuchi();
//#if __DRAW_MENU_ICON__
//		Mobile.drawIcon(false);
//#endif
//		if (Mobile.getKokuchi() != Defines.DEF_SELECT_3_OFF) {
//			// drawKokuchi();
//			DrawYokoku();
//		}else{
//			drawK4();
//		}
//
////		if (Defines.DEF_IS_DEBUG_AUTO) {
////		}
//
//		int pos = (int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] % 414)
//		* (2359296 / 414);
//		Mascot3D.draw3Dtest(pos);
//		// }
//		// スロットの描画
//		// if (Defines.DEF_IS_DEBUG_POS) {
//		// // デバック
//		// clearClip();
//		// // Z.setColor(Z.getColor(0, 0, 0));
//		// Z.setColor(Z.getColor(255, 0, 0));
//		// int y = 0;
//		// // Z.drawString("左 押:" + debugPos[0] + " 止:" + debugStop[0], 10, y
//		// // +=
//		// // 12);
//		// Z.drawString("HITLINE :"
//		// + Integer.toHexString(clOHHB_V23.getWork(Defines.DEF_HITLINE)), 10,
//		// y += 12);
//		// Z.drawString("WAVENUM :"
//		// + Integer.toHexString(clOHHB_V23.getWork(Defines.DEF_WAVENUM)), 130,
//		// y);
//		// // Z.drawString("中 押:" + debugPos[1] + " 止:" + debugStop[1], 10, y
//		// // +=
//		// // 12);
//		// // Z.drawString("JAC_CTR :" + (int) clOHHB_V23.getWork(Defines.DEF_JAC_CTR),
//		// // 10, y += 12);
//		// Z.drawString("FLASH0 :"
//		// + Integer.toHexString(clOHHB_V23.getWork(Defines.DEF_FLASH)), 10,
//		// y += 12);
//		// Z.drawString("GAMEST :0x"
//		// + Integer.toHexString(clOHHB_V23.getWork(Defines.DEF_GAMEST)), 130,
//		// y);
//		//
//		// // Z.drawString("右 押:" + debugPos[2] + " 止:" + debugStop[2], 10, y
//		// // +=
//		// // 12);
//		// // Z.drawString("BIGBCTR:" + (int) clOHHB_V23.getWork(Defines.DEF_BIGBCTR),
//		// // 10,
//		// // y += 12);
//		// Z.drawString("FLASH1:"
//		// + Integer.toHexString(clOHHB_V23.getWork(Defines.DEF_FLASH + 1)),
//		// 10, y += 12);
//		// Z.drawString("GMLVSTS :0x"
//		// + Integer.toHexString(clOHHB_V23.getWork(Defines.DEF_GMLVSTS)), 130,
//		// y);
//		//
//		// Z
//		// .drawString("MEDAL :" + int_s_value[Defines.DEF_INT_BET_COUNT], 10,
//		// y += 12);
//		// Z.drawString("HITREQ :0x"
//		// + Integer.toHexString(clOHHB_V23.getWork(Defines.DEF_HITREQ)), 130,
//		// y);
//		//
//		// Z.drawString("RANDOMX :" + (int) clOHHB_V23.getWork(Defines.DEF_RANDOMX),
//		// 10, y += 12);
//		// Z.drawString("WAVEBIT :0x"
//		// + Integer.toHexString(clOHHB_V23.getWork(Defines.DEF_WAVEBIT)), 130,
//		// y);
//		//
//		// Z.drawString("SLAMPBIT :0x"
//		// + Integer.toHexString(clOHHB_V23.getWork(Defines.DEF_SLAMPBIT)), 10,
//		// y += 12);
//		// Z.drawString("HITFLAG :0x"
//		// + Integer.toHexString(clOHHB_V23.getWork(Defines.DEF_HITFLAG)), 130,
//		// y);
//		//
//		// Z.drawString("PUSHCTR :0x"
//		// + Integer.toHexString(clOHHB_V23.getWork(Defines.DEF_PUSHCTR)), 10,
//		// y += 12);
//		// Z.drawString("PayMedal :" + int_s_value[Defines.DEF_INT_WIN_COIN_NUM], 130,
//		// y);
//		//
//		// Z.drawString("STOPBIT :0x"
//		// + Integer.toHexString(clOHHB_V23.getWork(Defines.DEF_STOPBIT)), 10,
//		// y += 12);
//		// if (Defines.DEF_IS_DEBUG_FORCE_YAKU) {
//		// Z.drawString("debugForceYaku:" + debugForceYaku, 130, y);
//		// }
//		// }
//
//		drawDataPanel(true);
//#if __COM_TYPE__
//
//#else
////		if(IS_HALL()){
////			if(Mobile.isDataPanel()){
////				drawDataPanelHall();
////			}
////		}
//#endif
//		if (Defines.DEF_IS_PRINT_FREEMEMORY) {
//			ZZ.setClip(0, 0, 240, 24);
//			ZZ.setColor(ZZ.getColor(0xff, 0xff, 0xff));
//			ZZ.fillRect(0, 0, 240, 24);
//			int xx = 200;
//			int yy = 0;
//			long val = Runtime.getRuntime().freeMemory();
//			do {
//				// 一桁は必ず描く
//				// 下（右）から描いて行く
//				ZZ.drawImage(Defines.DEF_RES_NA0 + (int) (val % 10), xx, yy);
//				val /= 10;
//				xx -= Defines.DEF_POS_NAVI_BB_W;
//			} while (val > 0);
//		}
//		
//		if(int_s_value[Defines.DEF_INT_CURRENT_MODE] == Defines.DEF_RMODE_HTTP){
//			ZZ.setClip(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ
//					.getHeight());
//			ZZ.drawImage(Defines.DEF_RES_TUSHIN_BACK,23,113);
//			ZZ.setColor(ZZ.getColor(0xff, 0xff, 0xff));
//			ZZ.drawString("通信中...", 36,127+12);
//			
//		}
//#if __COM_TYPE__
//		// クリッピング領域の解除
//		ZZ.setClip(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ.getHeight());
//#endif
//	}
#endif
	/**
	 * クリップが使えるのでisRepaintを使用しない方向で
	 * 
	 */
	private void drawAll() {
		//DfMain.TRACE(("★★★★drawAll★★★★");
		// クリッピング領域の解除
		ZZ.setClip(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ.getHeight());
		// 画面を白く塗りつぶす
		ZZ.setColor(ZZ.getColor(0, 0, 0));
		ZZ.fillRect(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ.getHeight());

#if __COM_TYPE__
		
		// 筐体背景部分
		ZZ.drawImage(Defines.DEF_RES_K1, Defines.DEF_POS_K1_X, Defines.DEF_POS_K1_Y);
		ZZ.drawImage(Defines.DEF_RES_K2, Defines.DEF_POS_K2_X, Defines.DEF_POS_K2_Y);
		ZZ.drawImage(Defines.DEF_RES_K3, Defines.DEF_POS_K3_X, Defines.DEF_POS_K3_Y);
		ZZ.drawImage(Defines.DEF_RES_K4, Defines.DEF_POS_K4_X, Defines.DEF_POS_K4_Y);
		ZZ.drawImage(Defines.DEF_RES_K5, Defines.DEF_POS_K5_X, Defines.DEF_POS_K5_Y);
		ZZ.drawImage(Defines.DEF_RES_K6, Defines.DEF_POS_K6_X, Defines.DEF_POS_K6_Y);
		ZZ.drawImage(Defines.DEF_RES_K7, Defines.DEF_POS_K7_X, Defines.DEF_POS_K7_Y);
		
		// ランプ
#if __CLIP_FALSE__
		
		{	// drawK1 4thリールの左右のランプ
			int[] x = { Defines.DEF_POS_S1_X, Defines.DEF_POS_S2_X, Defines.DEF_POS_S3_X, Defines.DEF_POS_S4_X, Defines.DEF_POS_S5_X, Defines.DEF_POS_S6_X };
			int[] y = { Defines.DEF_POS_S1_Y, Defines.DEF_POS_S2_Y, Defines.DEF_POS_S3_Y, Defines.DEF_POS_S4_Y, Defines.DEF_POS_S5_Y, Defines.DEF_POS_S6_Y };
			// ランプの画像がなぜかでかすぎる為、いれとかないと枠外にでてしまう。
			ZZ.setClip(Defines.DEF_POS_K1_X, Defines.DEF_POS_K1_Y, Defines.DEF_POS_K1_W, Defines.DEF_POS_K1_H);
			for (int i = 0; i < 6; i++) {
				if (getLampStatus(Defines.DEF_LAMP_S1 + i) == Defines.DEF_LAMP_STATUS_ON) {
					ZZ.drawImage(Defines.DEF_RES_S1_B + i, x[i], y[i]);
				}
			}
			// クリッピング領域の解除
			ZZ.setClip(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ.getHeight());
		}
		{	// drawK3 BETランプ
			int[] x = { Defines.DEF_POS_B1_X, Defines.DEF_POS_B2_X, Defines.DEF_POS_B3_X, Defines.DEF_POS_B4_X, Defines.DEF_POS_B5_X };
			int[] y = { Defines.DEF_POS_B1_Y, Defines.DEF_POS_B2_Y, Defines.DEF_POS_B3_Y, Defines.DEF_POS_B4_Y, Defines.DEF_POS_B5_Y };
			for (int i = 0; i < 5; i++) {
				if (getLampStatus(Defines.DEF_LAMP_BET_1 + i) == Defines.DEF_LAMP_STATUS_ON) {
					ZZ.drawImage(Defines.DEF_RES_B1_B + i, x[i], y[i]);
				}
			}
		}
		
		{	// drawK4 筐体左のかぎやランプやリプレイランプ
            int[] x = { Defines.DEF_POS_C1_X, Defines.DEF_POS_C2_X, Defines.DEF_POS_C3_X, Defines.DEF_POS_C4_X, Defines.DEF_POS_C5_X };
			int[] y = { Defines.DEF_POS_C1_Y, Defines.DEF_POS_C2_Y, Defines.DEF_POS_C3_Y, Defines.DEF_POS_C4_Y, Defines.DEF_POS_C5_Y };
			for (int i = 0; i < 5; i++) {
				if (getLampStatus(Defines.DEF_LAMP_WIN + i) == Defines.DEF_LAMP_STATUS_ON) {
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
		if (getLampStatus(Defines.DEF_LAMP_CHANCE) == Defines.DEF_LAMP_STATUS_ON) {
			ZZ.drawImage(Defines.DEF_RES_D1_B, Defines.DEF_POS_CHANCE_X, Defines.DEF_POS_CHANCE_Y);
		}
		// ボーナスかうんと描画
		drawBonusCount();
		// クレジット描画
		drawCredit();
		// 払い出し描画
		drawPay();
#else
		// 4thリールの左右のランプ
		drawK1();
		// BETランプ
		drawK3();
		// 筐体左のかぎやランプやリプレイランプ
		drawK4();
		// 4th
		draw4th();
		// リール
		drawSlot();
		
		drawK7();
		
		// クリッピング領域の解除
		ZZ.setClip(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ.getHeight());
#endif

#else
//		ZZ.drawImage(Defines.DEF_RES_K0, Defines.DEF_POS_K0_X, Defines.DEF_POS_K0_Y);
//		ZZ.drawImage(Defines.DEF_RES_K1, Defines.DEF_POS_K1_X, Defines.DEF_POS_K1_Y);
//		ZZ.drawImage(Defines.DEF_RES_K2, Defines.DEF_POS_K2_X, Defines.DEF_POS_K2_Y);
//		// ZZ.drawImage(Defines.DEF_RES_K3, Defines.DEF_POS_K3_X, Defines.DEF_POS_K3_Y);
//		// 筐体＆ランプ
//		drawK0();
//
//		drawK1();
//		drawK3();
//		drawK4();
//		ZZ.drawImage(Defines.DEF_RES_K3, Defines.DEF_POS_K3_X, Defines.DEF_POS_K3_Y);
//		// 4th
//		draw4th();
//		// リール
//		drawSlot();
//		// 払い出し
//		drawPay();	// drawK7内でもよばれている
//		// クレジット
//		drawCredit();
//		// ボーナスカウント
//		drawBonusCount();
//		
//		drawK7();
//		drawDataPanel(true);
//		if(IS_HALL()){
//			if(Mobile.isDataPanel()){
//				drawDataPanelHall();
//			}
//		}
//		// クリッピング領域の解除
//		ZZ.setClip(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ.getHeight());
#endif
		


//		ZZ.userRepaint();
	}

	/**
	 * 光どころ描画
	 * 
	 */
	private static void drawK3() {
		ZZ.setClip(Defines.DEF_POS_K3_X, Defines.DEF_POS_K3_Y, Defines.DEF_POS_K3_W, Defines.DEF_POS_K3_H);
#if __COM_TYPE__
#else
//		ZZ.drawImage(Defines.DEF_RES_K3, Defines.DEF_POS_K3_X, Defines.DEF_POS_K3_Y);
#endif
		int[] x = { Defines.DEF_POS_B1_X, Defines.DEF_POS_B2_X, Defines.DEF_POS_B3_X, Defines.DEF_POS_B4_X, Defines.DEF_POS_B5_X };
		int[] y = { Defines.DEF_POS_B1_Y, Defines.DEF_POS_B2_Y, Defines.DEF_POS_B3_Y, Defines.DEF_POS_B4_Y, Defines.DEF_POS_B5_Y };
		for (int i = 0; i < 5; i++) {
			if (getLampStatus(Defines.DEF_LAMP_BET_1 + i) == Defines.DEF_LAMP_STATUS_ON) {
				ZZ.drawImage(Defines.DEF_RES_B1_B + i, x[i], y[i]);
			}
		}
#if __COM_TYPE__
		// クリッピング領域の解除
		ZZ.setClip(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ.getHeight());
#endif

	}
	
#if __COM_TYPE__
#else
//	/**
//	 * 光どころ描画
//	 * 
//	 */
//	private static void drawK0() {
//		ZZ.setClip(Defines.DEF_POS_K0_X, Defines.DEF_POS_K0_Y, Defines.DEF_POS_K0_W, Defines.DEF_POS_K0_H);
//		ZZ.drawImage(Defines.DEF_RES_K0, Defines.DEF_POS_K0_X, Defines.DEF_POS_K0_Y);
//		final int[] x = { Defines.DEF_POS_A1_X, Defines.DEF_POS_A2_X, Defines.DEF_POS_A3_X, Defines.DEF_POS_A4_X,
//				Defines.DEF_POS_A5_X };
//		final int[] y = { Defines.DEF_POS_A1_Y, Defines.DEF_POS_A2_Y, Defines.DEF_POS_A3_Y, Defines.DEF_POS_A4_Y,
//				Defines.DEF_POS_A5_Y };
//		for (int i = 0; i < 5; i++) {
//			if (getLampStatus(Defines.DEF_LAMP_TOP_1 + i) == Defines.DEF_LAMP_STATUS_ON) {
//				ZZ.drawImage(Defines.DEF_RES_A1_B + i, x[i], y[i]);
//			}
//		}
//#if __COM_TYPE__
//		// クリッピング領域の解除
//		ZZ.setClip(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ.getHeight());
//#endif
//	}
#endif
	private static void drawK1() {
		ZZ.setClip(Defines.DEF_POS_K1_X, Defines.DEF_POS_K1_Y, Defines.DEF_POS_K1_W, Defines.DEF_POS_K1_H);
#if __COM_TYPE__
#else
//		ZZ.drawImage(Defines.DEF_RES_K1, Defines.DEF_POS_K1_X, Defines.DEF_POS_K1_Y);
#endif
		int[] x = { Defines.DEF_POS_S1_X, Defines.DEF_POS_S2_X, Defines.DEF_POS_S3_X, Defines.DEF_POS_S4_X, Defines.DEF_POS_S5_X, Defines.DEF_POS_S6_X };
		int[] y = { Defines.DEF_POS_S1_Y, Defines.DEF_POS_S2_Y, Defines.DEF_POS_S3_Y, Defines.DEF_POS_S4_Y, Defines.DEF_POS_S5_Y, Defines.DEF_POS_S6_Y };
		for (int i = 0; i < 6; i++) {
			if (getLampStatus(Defines.DEF_LAMP_S1 + i) == Defines.DEF_LAMP_STATUS_ON) {
				ZZ.drawImage(Defines.DEF_RES_S1_B + i, x[i], y[i]);
			}
		}
#if __COM_TYPE__
		// クリッピング領域の解除
		ZZ.setClip(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ.getHeight());
#endif
	}

	/**
	 * 光どころ描画
	 * 
	 */
	private static void drawK4() {
		ZZ.setClip(Defines.DEF_POS_K4_X, Defines.DEF_POS_K4_Y, Defines.DEF_POS_K4_W, Defines.DEF_POS_K4_H);
#if __COM_TYPE__
#else
		ZZ.drawImage(Defines.DEF_RES_K4, Defines.DEF_POS_K4_X, Defines.DEF_POS_K4_Y);
#endif
		int[] x = { Defines.DEF_POS_C1_X, Defines.DEF_POS_C2_X, Defines.DEF_POS_C3_X, Defines.DEF_POS_C4_X, Defines.DEF_POS_C5_X };
		int[] y = { Defines.DEF_POS_C1_Y, Defines.DEF_POS_C2_Y, Defines.DEF_POS_C3_Y, Defines.DEF_POS_C4_Y, Defines.DEF_POS_C5_Y };
		for (int i = 0; i < 5; i++) {
			if (getLampStatus(Defines.DEF_LAMP_WIN + i) == Defines.DEF_LAMP_STATUS_ON) {
				ZZ.drawImage(Defines.DEF_RES_C1_B + i, x[i], y[i]);
			}
		}
#if __COM_TYPE__
		// クリッピング領域の解除
		ZZ.setClip(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ.getHeight());
#endif
	}
#if __CLIP_FALSE__
#else
	private static void drawK7() {
#if __CLIP_FALSE__
#else
		ZZ.setClip(Defines.DEF_POS_K7_X, Defines.DEF_POS_K7_Y, Defines.DEF_POS_K7_W, Defines.DEF_POS_K7_H);
#endif
#if __COM_TYPE__
#else
		ZZ.drawImage(Defines.DEF_RES_K7, Defines.DEF_POS_K7_X, Defines.DEF_POS_K7_Y);
#endif
		if (getLampStatus(Defines.DEF_LAMP_CHANCE) == Defines.DEF_LAMP_STATUS_ON) {
			ZZ.drawImage(Defines.DEF_RES_D1_B, Defines.DEF_POS_CHANCE_X, Defines.DEF_POS_CHANCE_Y);
		}
		drawBonusCount();
		drawCredit();
		drawPay();
#if __COM_TYPE__
		// クリッピング領域の解除
		ZZ.setClip(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ.getHeight());
#endif
	}
#endif
	/**
	 * 4thリールの描画
	 * 
	 */
	private static void draw4th() {
		// ZZ.setClip(Defines.DEF_POS_4TH_X, Defines.DEF_POS_4TH_Y, Defines.DEF_POS_4TH_W, Defines.DEF_POS_4TH_H);
		// int x = int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE];
		// for (int i = 0; i < Defines.DEF_NUM_4TH_REEL; i++) {
		// if (x < width4th[i]) {
		// for (int j = 0; j < 3; j++) {
		// final int id = (i + j) % Defines.DEF_NUM_4TH_REEL;
		// // 画像ID
		// int img_id = 0;
		if (getLampStatus(Defines.DEF_LAMP_4TH) == Defines.DEF_LAMP_STATUS_ON) {
			// img_id = Defines.DEF_RES_R08_B + (Defines.DEF_RES_R09_B - Defines.DEF_RES_R08_B)
			// * id;

            // TODO soy 4thリールランプ処理
            GameManager.Instance.Set4thReelTexture(true);

			Mascot3D.setTexture(0);
		} else {
            // TODO soy 4thリールランプ処理
            GameManager.Instance.Set4thReelTexture(false);
            
            // img_id = Defines.DEF_RES_R08 + (Defines.DEF_RES_R09 - Defines.DEF_RES_R08) * id;
			Mascot3D.setTexture(1);
		}
		// ZZ.drawImage(img_id, Defines.DEF_POS_4TH_X - x, Defines.DEF_POS_4TH_Y);
		// x -= width4th[id];
		// }
		// return;
		// } else {
		// x -= width4th[i];
		// }
		// }
	}

	/**
	 * スロット回転部の描画。
	 */
	private static void drawSlot() {
		// // リール部分クリッピング
		// ZZ.setClip(Defines.DEF_POS_REEL_WINDOW_X, Defines.DEF_POS_REEL_WINDOW_Y,
		// Defines.DEF_POS_REEL_WINDOW_DX * 2 + Defines.DEF_POS_REEL_WINDOW_W,
		// Defines.DEF_POS_REEL_WINDOW_H);
		// // １リールずつ処理
		// int x = Defines.DEF_POS_REEL_WINDOW_X; // リール書き出し位置
		// int bg_1 = 1;
		// for (int i = 0; i < 3; i++) { // 左のリールから描画しています
		// // 背景
		// ZZ.drawImage(Defines.DEF_RES_BACK, x, Defines.DEF_POS_REEL_WINDOW_Y);
		// int y = Defines.DEF_N_FRAME * int_s_value[Defines.DEF_INT_REEL_ANGLE_R0 + i];
		// // あとでｙになります。
		// int num = ((y >> 16) % Defines.DEF_N_FRAME);
		// // 上にくる画像の番号。
		// y = Defines.DEF_POS_REEL_WINDOW_Y - Defines.DEF_POS_REEL_H + Defines.DEF_POS_REEL_WINDOW_H
		// + (((y & ANGLE_2PI_MASK) * Defines.DEF_POS_REEL_H) >> 16);
		// // // リールの描画
		// for (int j = 3; j >= 0; j--) {
		// int sym = getReelId(REELTB[i][(num + 2 + Defines.DEF_N_FRAME - j)
		// % Defines.DEF_N_FRAME]);
		// // 明るい
		// int id = Defines.DEF_RES_R01_B + (Defines.DEF_RES_R02_B - Defines.DEF_RES_R01_B) * sym;
		// if (int_s_value[Defines.DEF_INT_CURRENT_MODE] == Defines.DEF_RMODE_FLASH
		// || (int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] == 2 &&
		// int_s_value[Defines.DEF_INT_CURRENT_MODE] == Defines.DEF_RMODE_WAIT)) {
		// if ((int_s_value[Defines.DEF_INT_FLASH_DATA] & bg_1) == 0) {
		// // 暗い
		// id = Defines.DEF_RES_R01 + (Defines.DEF_RES_R02 - Defines.DEF_RES_R01) * sym;
		// }
		// } else {
		// if ((int_s_value[Defines.DEF_INT_IS_REEL_STOPPED] & BIT(i)) == 0) {
		// // 暗い
		// id = Defines.DEF_RES_R01_BB + (Defines.DEF_RES_R02_BB - Defines.DEF_RES_R01_BB)
		// * sym;
		// }
		// }
		// ZZ.drawImage(id, x, y);
		// // リール絵
		// y -= Defines.DEF_POS_REEL_H;
		//
		// if (j > 0) {
		// bg_1 <<= 1;
		// }
		// }
		// x += Defines.DEF_POS_REEL_WINDOW_DX; // 右へずらす
		// }
		//

#if __COM_TYPE__
#else
//		ZZ.setClip(25, 114 + Defines.GP_DRAW_OFFSET_Y, 215 - 25, 96);
//		ZZ.drawImage(Defines.DEF_RES_K5, Defines.DEF_POS_K5_X, Defines.DEF_POS_K5_Y);
//		ZZ.drawImage(Defines.DEF_RES_K6, Defines.DEF_POS_K6_X, Defines.DEF_POS_K6_Y);
#endif
		// １リールずつ処理
		int[] x = { 25, 92, 159 };
		for (int i = 0; i < 3; i++) { // 左のリールから描画しています
			// リール部分クリッピング
			ZZ.setClip(25, 114 + Defines.GP_DRAW_OFFSET_Y, 215 - 25, 96);
			// 消灯
			int[] state = { 0, 0, 0, 0, 0 };// 消灯
			if (int_s_value[Defines.DEF_INT_CURRENT_MODE] == Defines.DEF_RMODE_FLASH
					|| (int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] == 2 && int_s_value[Defines.DEF_INT_CURRENT_MODE] == Defines.DEF_RMODE_WAIT)) {
				// 点灯
				// 枠下&下段
				if ((int_s_value[Defines.DEF_INT_FLASH_DATA] & (1 << (i * 3 + 0))) != 0) {
					state[0] = state[1] = 1;
				}
				// 中段
				if ((int_s_value[Defines.DEF_INT_FLASH_DATA] & (1 << (i * 3 + 1))) != 0) {
					state[2] = 1;
				}
				// 上段&枠上
				if ((int_s_value[Defines.DEF_INT_FLASH_DATA] & (1 << (i * 3 + 2))) != 0) {
					state[3] = state[4] = 1;
				}
			} else {
				state[0] = state[1] = state[2] = state[3] = state[4] = 1;
			}
			// ブラー
			if ((int_s_value[Defines.DEF_INT_IS_REEL_STOPPED] & BIT(i)) == 0) {
				// リールスピードが２以下の場合ブラー画像は使わない！
				if (Mobile.getReelSpeed() >= 80) {
					// ブラー
					state[0] = state[1] = state[2] = state[3] = state[4] = 2;
				}
				ZZ.drawImage(Defines.DEF_RES_BACK_B, x[i], 114 + Defines.GP_DRAW_OFFSET_Y);
			} else {
				ZZ.drawImage(Defines.DEF_RES_BACK, x[i], 114 + Defines.GP_DRAW_OFFSET_Y);
			}

//			System.out.println("time="+ZZ.getThreadSpeed());
//			System.out.println(""+int_s_value[2 + 0]+","+int_s_value[2 + 1]+","+int_s_value[2 + i]+"&"+ANGLE_2PI_MASK);
			// 例:1周21コマのうち15.75コマ回転していたら、per21=15.75<<16
			int per21 = Defines.DEF_N_FRAME
					* (int_s_value[Defines.DEF_INT_REEL_ANGLE_R0 + i] & ANGLE_2PI_MASK);
			
			// 例:15.75コマ回転していたら15番を取得
			int mid = ((per21 >> 16) % Defines.DEF_N_FRAME); // 中段番号番号。
			// 各y座標に0.75コマ分下に
			int[] y = { 204, 176, 148, 120, 92 };
			// // リールの描画
			for (int j = 0; j < 5; j++) {// 0:枠下 1:下段 2:中段 3:上段 4:枠上
				int h = 28;
				y[j] += (/* １コマに満たない分 */(per21 & ANGLE_2PI_MASK) * h) >> 16;

				int rlnum = (mid + j - 2 + Defines.DEF_N_FRAME) % Defines.DEF_N_FRAME;// リール位置番号
				int sym = getReelId(REELTB[i][rlnum]);// 絵柄ID
				int id = 0;
				
                // soy TODO リールランプ処理
                GameManager.Instance.SetReelTexture(rlnum, i, state[j] == 1);

#if __REEL_ID_CHECK__
				if(j == 2)
				{	// センター
					
					//	図柄コード
					//#define Defines.DEF_BSVN         	     0x01                // 青７
					//#define Defines.DEF_DON_         	     0x02                // ﾄﾞﾝ
					//#define Defines.DEF_BAR_         	     0x04                // BAR
					//#define Defines.DEF_RPLY         	     0x08                // ｾﾝｽ
					//#define Defines.DEF_WMLN         	     0x10                // 大山
					//#define Defines.DEF_BELL         	     0x20                // ﾊﾅﾋﾞ
					//#define Defines.DEF_CHRY         	     0x40                // ﾁｪﾘｰ
					//#define Defines.DEF_ANY_         	     0x00                // －
					
					ZZ.drawReel[i] = rlnum;
//					if(i == 0)
//					{
//						
//						if( REELTB[i][rlnum] != (clOHHB_V23.getWork(Defines.DEF_ARAY11) & 0xFFFF))
//						{
//							DfMain.TRACE(("左リールの値がおかしいよ！:0x" + ZZ.hexInt(REELTB[i][rlnum]) + ":0x" + ZZ.hexInt((clOHHB_V23.getWork(Defines.DEF_ARAY11) & 0xFFFF)));
//						}
//					}
//					else if(i == 1)
//					{
//						if( REELTB[i][rlnum] != (clOHHB_V23.getWork(Defines.DEF_ARAY12) & 0xFFFF))
//						{
//							DfMain.TRACE(("中リールの値がおかしいよ！:0x" + ZZ.hexInt(REELTB[i][rlnum]) + ":0x" + ZZ.hexInt((clOHHB_V23.getWork(Defines.DEF_ARAY12) & 0xFFFF)));
//						}
//					}
//					else if(i == 2)
//					{
//						if( REELTB[i][rlnum] != (clOHHB_V23.getWork(Defines.DEF_ARAY13) & 0xFFFF))
//						{
//							DfMain.TRACE(("右リールの値がおかしいよ！:0x" + ZZ.hexInt(REELTB[i][rlnum]) + ":0x" + ZZ.hexInt((clOHHB_V23.getWork(Defines.DEF_ARAY13) & 0xFFFF)));
//						}
//					}
					//DfMain.TRACE(("★停止図柄:" +   + ":" + (clOHHB_V23.getWork(Defines.DEF_ARAY12) & 0xFFFF) + ":" + (clOHHB_V23.getWork(Defines.DEF_ARAY13) & 0xFFFF));
				}
#endif

#if __CLIP_FALSE__
				id = Defines.DEF_RES_R1_01 + (Defines.DEF_RES_R1_02 - Defines.DEF_RES_R1_01) * sym + state[j];
				ZZ.drawImage(id, x[i], y[j] + Defines.GP_DRAW_OFFSET_Y);
				
				id = Defines.DEF_RES_R2_01 + (Defines.DEF_RES_R2_02 - Defines.DEF_RES_R2_01) * sym + state[j];
				ZZ.drawImage(id, x[i], y[j] + Defines.GP_DRAW_OFFSET_Y);
				
				id = Defines.DEF_RES_R3_01 + (Defines.DEF_RES_R3_02 - Defines.DEF_RES_R3_01) * sym + state[j];
				ZZ.drawImage(id, x[i], y[j] + Defines.GP_DRAW_OFFSET_Y);
#else
				id = Defines.DEF_RES_R1_01 + (Defines.DEF_RES_R1_02 - Defines.DEF_RES_R1_01) * sym + state[j];
				ZZ.setClip(25, 114 + Defines.GP_DRAW_OFFSET_Y, 190, 34);
				ZZ.drawImage(id, x[i], y[j] + Defines.GP_DRAW_OFFSET_Y);
				
				id = Defines.DEF_RES_R2_01 + (Defines.DEF_RES_R2_02 - Defines.DEF_RES_R2_01) * sym + state[j];
				ZZ.setClip(25, 148 + Defines.GP_DRAW_OFFSET_Y, 190, 28);
				ZZ.drawImage(id, x[i], y[j] + Defines.GP_DRAW_OFFSET_Y);
				
				id = Defines.DEF_RES_R3_01 + (Defines.DEF_RES_R3_02 - Defines.DEF_RES_R3_01) * sym + state[j];
				ZZ.setClip(25, 176 + Defines.GP_DRAW_OFFSET_Y, 190, 34);
				ZZ.drawImage(id, x[i], y[j] + Defines.GP_DRAW_OFFSET_Y);
#endif
			}
		}

		ZZ.setClip(25, 114 + Defines.GP_DRAW_OFFSET_Y, 215 - 25, 96);
		ZZ.scale3D(100);// スケール弄るよ
		int[] xx = { 25, 92, 159 };
		for (int i = 0; i < 3; i++) {
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
		
#if __COM_TYPE__
		// クリッピング領域の解除
		ZZ.setClip(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ.getHeight());
#endif

	} // End of drawSlot()

	/**
	 * TODO クレジット描画
	 * 
	 */
	private static void drawCredit() {
		// ZZ.setClip(Defines.DEF_POS_CREDIT_X - Defines.DEF_POS_CREDIT_W * 2, Defines.DEF_POS_CREDIT_Y,
		// Defines.DEF_POS_CREDIT_W * 3, Defines.DEF_POS_CREDIT_H);
		// ZZ.drawImage(Defines.DEF_RES_K2, Defines.DEF_POS_K2_X, Defines.DEF_POS_K2_Y);
		int xx = Defines.DEF_POS_CREDIT_X;
		int val = int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM];
		
		for (int i = 0; i < Defines.DEF_POS_CREDIT_D; i++) {
			if (val > 0) {
				ZZ.drawImage(Defines.DEF_RES_SEG_R0 + (val % 10), xx, Defines.DEF_POS_CREDIT_Y);
			} else {
				if (i == 0) {
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
	private static void drawPay() {
		// ZZ.setClip(Defines.DEF_POS_PAY_X - Defines.DEF_POS_PAY_W * 2, Defines.DEF_POS_PAY_Y,
		// Defines.DEF_POS_PAY_W * 3, Defines.DEF_POS_PAY_H);
		// ZZ.drawImage(Defines.DEF_RES_K2, Defines.DEF_POS_K2_X, Defines.DEF_POS_K2_Y);
		int xx = Defines.DEF_POS_PAY_X;
		int val = int_s_value[Defines.DEF_INT_WIN_GET_COIN];
		for (int i = 0; i < Defines.DEF_POS_PAY_D; i++) {
			if (val > 0) {
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
	private static void drawBonusCount() {
		// ZZ.setClip(Defines.DEF_POS_BONUS_X - Defines.DEF_POS_BONUS_W * 2, Defines.DEF_POS_BONUS_Y,
		// Defines.DEF_POS_BONUS_W * 3, Defines.DEF_POS_BONUS_H);
		// ZZ.drawImage(Defines.DEF_RES_K2, Defines.DEF_POS_K2_X, Defines.DEF_POS_K2_Y);
		// ボーナスカウンタ表示領域
		if (IS_BONUS_JAC()) { // Jac 中
			int i = clOHHB_V23.getWork(Defines.DEF_BIGBCTR);
			i = (i == 0) ? 1 : i;
			// (JACイン中)残り回数(3～1)
			ZZ.drawImage(Defines.DEF_RES_SEG_G0 + i,
					Defines.DEF_POS_BONUS_X - Defines.DEF_POS_BONUS_W * 2, Defines.DEF_POS_BONUS_Y);
			// -[ﾊｲﾌﾝ]
			ZZ.drawImage(Defines.DEF_RES_SEG_GB, Defines.DEF_POS_BONUS_X - Defines.DEF_POS_BONUS_W,
					Defines.DEF_POS_BONUS_Y);
			// ボーナスカウント(JACイン中)を表示します(8～1)
			ZZ.drawImage(Defines.DEF_RES_SEG_G0 + clOHHB_V23.getWork(Defines.DEF_JAC_CTR),
					Defines.DEF_POS_BONUS_X, Defines.DEF_POS_BONUS_Y);
		} else if (IS_BONUS_GAME()) {
			// ＢＢ残り回数を表示
			int val = clOHHB_V23.getWork(Defines.DEF_BBGMCTR);
			int xx = Defines.DEF_POS_BONUS_X;
			for (int i = 0; i < 3; i++) {
				if (val > 0) {
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
    public static string GetBonusCount() {
        if (IS_BONUS_JAC()) { // Jac 中
            int i = clOHHB_V23.getWork(Defines.DEF_BIGBCTR);
            i = (i == 0) ? 1 : i;
            return i + "-" + clOHHB_V23.getWork(Defines.DEF_JAC_CTR);
        } else if (IS_BONUS_GAME()) {
            // ＢＢ残り回数を表示
            return clOHHB_V23.getWork(Defines.DEF_BBGMCTR).ToString();
        }
        return "";
    }
#if __COM_TYPE__
	
#else
//	/**
//	 * 小役告知描画
//	 * 
//	 */
//	private static void drawKokuchi() {
//		ZZ.setClip(Defines.DEF_POS_KOKUCHI_X, Defines.DEF_POS_KOKUCHI_Y, Defines.DEF_POS_KOKUCHI_W,
//				Defines.DEF_POS_KOKUCHI_DY + Defines.DEF_POS_KOKUCHI_H * 2);
//		ZZ.drawImage(Defines.DEF_RES_K2, Defines.DEF_POS_K2_X, Defines.DEF_POS_K2_Y);
//		ZZ.drawImage(Defines.DEF_RES_K3, Defines.DEF_POS_K3_X, Defines.DEF_POS_K3_Y);
//		if (int_s_value[Defines.DEF_INT_CURRENT_MODE] == Defines.DEF_RMODE_SPIN) {
//			if (Mobile.getKokuchi() == Defines.DEF_SELECT_3_PREV) {
//				int_s_value[Defines.DEF_INT_KOKUCHI_X]++;
//			}
//		} else if (int_s_value[Defines.DEF_INT_CURRENT_MODE] != Defines.DEF_RMODE_BET) {
//			int_s_value[Defines.DEF_INT_KOKUCHI_X]++;
//		}
//
//		int xx = Defines.DEF_POS_WIDTH - (int_s_value[Defines.DEF_INT_KOKUCHI_X] * 10);
//
//		if (xx < Defines.DEF_POS_KOKUCHI_X) {
//			xx = Defines.DEF_POS_KOKUCHI_X;
//		}
//
//		if (xx != Defines.DEF_POS_WIDTH && int_s_value[Defines.DEF_INT_KOKUCHI_ID] != 0) {
//			// 成立時でなく内部フラグ間は表示しつづける。
//			if (Mobile.getKokuchi() != Defines.DEF_SELECT_3_OFF) {
//				if (int_s_value[Defines.DEF_INT_KOKUCHI_ID] > 1000) {
//					ZZ.drawImage(int_s_value[Defines.DEF_INT_KOKUCHI_ID] / 1000, xx,
//							Defines.DEF_POS_KOKUCHI_Y);
//				}
//				if (int_s_value[Defines.DEF_INT_KOKUCHI_ID] % 1000 > 0) {
//					ZZ.drawImage(int_s_value[Defines.DEF_INT_KOKUCHI_ID] % 1000, xx,
//							Defines.DEF_POS_KOKUCHI_Y + Defines.DEF_POS_KOKUCHI_DY);
//				}
//			}
//		}
//#if __COM_TYPE__
//		// クリッピング領域の解除
//		ZZ.setClip(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ.getHeight());
//#endif
//	}
//
//	private static void setKokuchiId() {
//		int_s_value[Defines.DEF_INT_KOKUCHI_ID] = 0;
//		// switch (clOHHB_V23.getWork(Defines.DEF_GMLVSTS)) {
//		// case Defines.DEF_GMLVSTS_RB_FLAG:// RB内部当たり中
//		// int_s_value[Defines.DEF_INT_KOKUCHI_ID] = Defines.DEF_RES_KOKU_02 * 1000;
//		// case Defines.DEF_GMLVSTS_BB_FLAG:// BB内部当たり中
//		// int_s_value[Defines.DEF_INT_KOKUCHI_ID] = Defines.DEF_RES_KOKU_01 * 1000;
//		// }
//		char wavebit = clOHHB_V23.getWork(Defines.DEF_WAVEBIT);
//		if ((wavebit & Defines.DEF__00000001B) != 0) {
//			if (IS_BONUS_JAC()) {
//				int_s_value[Defines.DEF_INT_KOKUCHI_ID] += Defines.DEF_RES_KOKU_08;
//			} else if (IS_BONUS_GAME()) {
//				int_s_value[Defines.DEF_INT_KOKUCHI_ID] += Defines.DEF_RES_KOKU_03;
//			} else {
//				int_s_value[Defines.DEF_INT_KOKUCHI_ID] += Defines.DEF_RES_KOKU_06;
//			}
//		} else if ((wavebit & Defines.DEF__00000010B) != 0) {
//			int_s_value[Defines.DEF_INT_KOKUCHI_ID] += Defines.DEF_RES_KOKU_04;
//		} else if ((wavebit & Defines.DEF__00000100B) != 0) {
//			if (IS_BONUS_GAME()) {
//				int_s_value[Defines.DEF_INT_KOKUCHI_ID] += Defines.DEF_RES_KOKU_06;
//			} else {
//				int_s_value[Defines.DEF_INT_KOKUCHI_ID] += Defines.DEF_RES_KOKU_05;
//			}
//		} else if ((wavebit & Defines.DEF__00001000B) != 0) {
//			if (IS_BONUS_JAC()) {
//				int_s_value[Defines.DEF_INT_KOKUCHI_ID] += Defines.DEF_RES_KOKU_08;
//			} else {
//				int_s_value[Defines.DEF_INT_KOKUCHI_ID] += Defines.DEF_RES_KOKU_07;
//			}
//		}
//
//		if ((wavebit & Defines.DEF__00010000B) != 0) {
//			int_s_value[Defines.DEF_INT_KOKUCHI_ID] += Defines.DEF_RES_KOKU_02 * 1000;
//		} else if ((wavebit & Defines.DEF__00100000B) != 0) {
//			int_s_value[Defines.DEF_INT_KOKUCHI_ID] += Defines.DEF_RES_KOKU_01 * 1000;
//		}
//	}
//	public void drawDataPanel(bool isGame) {
//		
//	}
//	/**
//	 * TODO データパネル表示
//	 * 
//	 * @param dy
//	 */
//	public void drawDataPanel(bool isGame) {
//		if (Mobile.isDataPanel() || !isGame) {
//			gp.param_hall.usr_coin = int_s_value[Defines.DEF_INT_SLOT_COIN_NUM]; // getCoinNum();
//			DfMain.TRACE(("gp.param_hall.usr_coin="+gp.param_hall.usr_coin);
//			gp.gpHandler(G7Network.GP_HANDLER_03_DATAWINDOW);
//			drawDataPanelLeft(isGame);
//			drawDataPanelRight(isGame);
//#if __DRAW_DATA_PANEL2__
//			if (!isGame || 
//					(int_s_value[Defines.DEF_INT_CURRENT_MODE] == Defines.DEF_RMODE_WAIT
//							&& int_s_value[Defines.DEF_INT_MODE_COUNTER] * ZZ.getThreadSpeed() >= 10000)) {
//				drawDataPanelCenter(isGame);
//			}
//#endif
//			if (!isGame) {
//				drawDataPanelBottom(isGame);
//			}
//		}
//	}
//
//	/**
//	 * データパネルの左側
//	 */
//	private static void drawDataPanelLeft(bool isGame) {
//		int dy = 0;
//		if (isGame) {
//			dy = 28 - Defines.DEF_POS_NAVI_DY;
//		}
//		ZZ.setClip(Defines.DEF_POS_NAVI_L_X, Defines.DEF_POS_NAVI_L_Y + dy, Defines.DEF_POS_NAVI_L_W,
//				Defines.DEF_POS_NAVI_L_H);
//		// TOTAL
//		if(IS_HALL()){
//			ZZ.drawImage(Defines.DEF_RES_HALL_NAVI_L, Defines.DEF_POS_NAVI_L_X, Defines.DEF_POS_NAVI_L_Y + dy);
//			printPanelCounts(hallData[Defines.DEF_H_GAME_COUNT], Defines.DEF_POS_NAVI_TOTAL_X,
//					Defines.DEF_POS_NAVI_TOTAL_Y + dy, 99999,0);
//			// COUNT
//			printPanelCounts(hallData[Defines.DEF_H_BNS_0], Defines.DEF_POS_NAVI_COUNT_X,
//					Defines.DEF_POS_NAVI_COUNT_Y + dy, 99999,0);
//		}else{
//			ZZ.drawImage(Defines.DEF_RES_NAVI_L, Defines.DEF_POS_NAVI_L_X, Defines.DEF_POS_NAVI_L_Y + dy);
//			printPanelCounts(int_s_value[Defines.DEF_INT_TOTAL_GAMES], Defines.DEF_POS_NAVI_TOTAL_X,
//					Defines.DEF_POS_NAVI_TOTAL_Y + dy, 99999,0);
//			// COUNT
//			printPanelCounts(int_s_value[Defines.DEF_INT_UNTIL_BONUS_GAMES], Defines.DEF_POS_NAVI_COUNT_X,
//					Defines.DEF_POS_NAVI_COUNT_Y + dy, 99999,0);
//		}
//	}
//
//	private static void drawDataPanelRight(bool isGame) {
//		int dy = 0;
//		if (isGame) {
//			dy = 28 - Defines.DEF_POS_NAVI_DY;
//		}
//		ZZ.setClip(Defines.DEF_POS_NAVI_R_X, Defines.DEF_POS_NAVI_R_Y + dy, Defines.DEF_POS_NAVI_R_W,
//				Defines.DEF_POS_NAVI_R_H);
////		ZZ.drawImage(Defines.DEF_RES_NAVI_R, Defines.DEF_POS_NAVI_R_X, Defines.DEF_POS_NAVI_R_Y + dy);
////		int col = (getCoinNum() < 0) ? 1 : 0;
////		// MEDAL
////		printPanelCounts(int_s_value[Defines.DEF_INT_SLOT_COIN_NUM], Defines.DEF_POS_NAVI_COIN_X,
////				Defines.DEF_POS_NAVI_COIN_Y + dy, Defines.DEF_POS_NAVI_COIN_MAX, col);
////
////		// BB
////		printPanelCounts(int_s_value[Defines.DEF_INT_BIG_COUNT], Defines.DEF_POS_NAVI_BB_X,
////				Defines.DEF_POS_NAVI_BB_Y + dy, Defines.DEF_POS_NAVI_BB_MAX, 0);
////
////		ZZ.setColor(ZZ.getColor(0xff, 0xff, 0xff));
////
////		ZZ.fillRect(Defines.DEF_POS_NAVI_BB_X + Defines.DEF_POS_NAVI_BB_W, Defines.DEF_POS_NAVI_BB_Y + dy,
////				Defines.DEF_POS_NAVI_BB_W, Defines.DEF_POS_NAVI_BB_H);
////
////		ZZ.drawImage(Defines.DEF_RES_NAB, Defines.DEF_POS_NAVI_BB_X + Defines.DEF_POS_NAVI_BB_W,
////				Defines.DEF_POS_NAVI_BB_Y + dy);
////
////		// RB
////		printPanelCounts(int_s_value[Defines.DEF_INT_REG_COUNT], Defines.DEF_POS_NAVI_RB_X,
////				Defines.DEF_POS_NAVI_RB_Y + dy, Defines.DEF_POS_NAVI_RB_MAX, 0);
//		// MEDAL
//		if(IS_HALL()){
//			ZZ.drawImage(Defines.DEF_RES_HALL_NAVI_R, Defines.DEF_POS_NAVI_R_X, Defines.DEF_POS_NAVI_R_Y + dy);
////			printPanelCounts(int_s_value[Defines.DEF_INT_SLOT_COIN_NUM], Defines.DEF_POS_COIN_X,
////					Defines.DEF_POS_COIN_Y + dy, 99999,0);
//		}else{
//			ZZ.drawImage(Defines.DEF_RES_NAVI_R, Defines.DEF_POS_NAVI_R_X, Defines.DEF_POS_NAVI_R_Y + dy);
//		}
//			int col = (getCoinNum() < 0) ? 1 : 0;
//			if(isGame){
//				printPanelCounts(int_s_value[Defines.DEF_INT_SLOT_COIN_NUM], Defines.DEF_POS_NAVI_COIN_X,
//						Defines.DEF_POS_NAVI_COIN_Y + dy, 99999,col);
//			}else{
//				printPanelCounts(getCoinNum() + getKasidasiValue(),
//						Defines.DEF_POS_NAVI_COIN_X, Defines.DEF_POS_NAVI_COIN_Y + dy, 99999, col);
//			}
////		}
//
//		if(IS_HALL()){
//			// BB
//			printPanelCounts(hallData[Defines.DEF_H_BB_COUNT], Defines.DEF_POS_NAVI_BB_X,
//					Defines.DEF_POS_NAVI_BB_Y + dy, 99,0);
//		}else{
//			// BB
//			printPanelCounts(int_s_value[Defines.DEF_INT_BIG_COUNT], Defines.DEF_POS_NAVI_BB_X,
//					Defines.DEF_POS_NAVI_BB_Y + dy, 99,0);
//		}
//
//		ZZ.setColor(ZZ.getColor(0xff, 0xff, 0xff));
//		ZZ.fillRect(Defines.DEF_POS_NAVI_BB_X + Defines.DEF_POS_NAVI_BB_W, Defines.DEF_POS_NAVI_BB_Y + dy, Defines.DEF_POS_NAVI_BB_W,
//				Defines.DEF_POS_NAVI_BB_H);
//		ZZ.drawImage(Defines.DEF_RES_NAB, Defines.DEF_POS_NAVI_BB_X + Defines.DEF_POS_NAVI_BB_W, Defines.DEF_POS_NAVI_BB_Y + dy);
//
//		if(IS_HALL()){
//			// RB
//			printPanelCounts(hallData[Defines.DEF_H_RB_COUNT], Defines.DEF_POS_NAVI_RB_X,
//					Defines.DEF_POS_NAVI_RB_Y + dy, 99,0);
//		}else{
//			// RB
//			printPanelCounts(int_s_value[Defines.DEF_INT_REG_COUNT], Defines.DEF_POS_NAVI_RB_X,
//					Defines.DEF_POS_NAVI_RB_Y + dy, 99,0);
//		}
//	}
//	/**
//	 * TODO データパネル表示その2
//	 */
//	private static void drawDataPanelCenter(bool isGame) {
//		int dy = 0;
//		if (isGame) {
//			dy = 28 - Defines.DEF_POS_NAVI_DY;
//		}
//		ZZ.setClip(Defines.DEF_POS_NAVI_C_X, Defines.DEF_POS_NAVI_C_Y + dy, Defines.DEF_POS_NAVI_C_W,
//				Defines.DEF_POS_NAVI_C_H);
//		if(IS_HALL()){
//			ZZ.drawImage(Defines.DEF_RES_HALL_NAVI_C, Defines.DEF_POS_NAVI_C_X, Defines.DEF_POS_NAVI_C_Y + dy);
//		}else{
//			ZZ.drawImage(Defines.DEF_RES_NAVI_C, Defines.DEF_POS_NAVI_C_X, Defines.DEF_POS_NAVI_C_Y + dy);
//		}
//		ZZ.setColor(ZZ.getColor(Defines.DEF_POS_CELL_COLOR_ETC_R,
//				Defines.DEF_POS_CELL_COLOR_ETC_G, Defines.DEF_POS_CELL_COLOR_ETC_B));
//		ZZ.fillRect(Defines.DEF_POS_CELL_ETC_X, Defines.DEF_POS_CELL_ETC_Y + dy,
//				Defines.DEF_POS_CELL_ETC_W, Defines.DEF_POS_CELL_ETC_H);
//		ZZ.setColor(ZZ.getColor(Defines.DEF_POS_CELL_COLOR_BB_R, Defines.DEF_POS_CELL_COLOR_BB_G,
//				Defines.DEF_POS_CELL_COLOR_BB_B));
//		ZZ.fillRect(Defines.DEF_POS_CELL_BB_X, Defines.DEF_POS_CELL_BB_Y + dy, Defines.DEF_POS_CELL_BB_W,
//				Defines.DEF_POS_CELL_BB_H);
//		ZZ.setColor(ZZ.getColor(Defines.DEF_POS_CELL_COLOR_RB_R, Defines.DEF_POS_CELL_COLOR_RB_G,
//				Defines.DEF_POS_CELL_COLOR_RB_B));
//		ZZ.fillRect(Defines.DEF_POS_CELL_RB_X, Defines.DEF_POS_CELL_RB_Y + dy, Defines.DEF_POS_CELL_RB_W,
//				Defines.DEF_POS_CELL_RB_H);
//		/** パネル表示領域を定義 */
//		int x0 = Defines.DEF_POS_CELL_X;
//		int y0 = Defines.DEF_POS_CELL_Y + dy;
//
//		/***/
//		int tmp = 0;
//		int col = 0;
//
//		// セルの色を決めて描画
//		for (int i = 0; i < Defines.DEF_INFO_GAME_HISTORY; i++) { // W(10)
//			for (int j = 0; j < Defines.DEF_INFO_GAMES; j++) { // H(8)
//				tmp = bonus_Data[(int_s_value[Defines.DEF_INT_BONUS_DATA_BASE] + i)
//						% bonus_Data.Length][j];
//				// TOBE tmpが保証されていればこのチェックは不要だす..
//				if (tmp < 0 || tmp >= Defines.DEF_GAME_NUM) {
//					col = panel_colors[Defines.DEF_GAME_NONE];
//				} else {
//					col = panel_colors[tmp];
//				}
//
//				ZZ.setColor(col);
//				// １セル描画
//				ZZ.fillRect(x0 + i * (Defines.DEF_POS_CELL_W + 1),// comment
//						y0 + ((Defines.DEF_INFO_GAMES - 1) - j) * (Defines.DEF_POS_CELL_H + 1),
//						// comment
//						Defines.DEF_POS_CELL_W, // comment
//						Defines.DEF_POS_CELL_H);
//			}
//		}
//	}
//	private static void drawDataPanelBottom(bool isGame) {
//		int dy = 0;
//		if (isGame) {
//			dy = 28 - Defines.DEF_POS_NAVI_DY;
//		}
//		ZZ.setClip(Defines.DEF_POS_NAVI_B_X, Defines.DEF_POS_NAVI_B_Y + dy, Defines.DEF_POS_NAVI_B_W,
//				Defines.DEF_POS_NAVI_B_H);
//		if(IS_HALL()){
//			ZZ.drawImage(Defines.DEF_RES_HALL_NAVI_BOT, Defines.DEF_POS_NAVI_BOT_X, Defines.DEF_POS_NAVI_BOT_Y + dy);
//		}else{
//			ZZ.drawImage(Defines.DEF_RES_NAVI_BOT, Defines.DEF_POS_NAVI_BOT_X, Defines.DEF_POS_NAVI_BOT_Y + dy);
//		}
//		// MAX MEDAL(BB最高獲得)
//		printPanelCounts(int_s_value[Defines.DEF_INT_GAME_INFO_MAX_GOT],
//				Defines.DEF_POS_NAVI_MAX_X, Defines.DEF_POS_NAVI_MAX_Y + dy, Defines.DEF_POS_NAVI_MAX_MAX,
//				0);
//		if(IS_HALL()){
//			// BB_AVE
//			if (hallData[Defines.DEF_H_BB_COUNT] > 0) {
//				printPanelCounts(hallData[Defines.DEF_H_GAME_COUNT]
//						/ hallData[Defines.DEF_H_BB_COUNT], Defines.DEF_POS_NAVI_BBAVG_X,
//						Defines.DEF_POS_NAVI_BBAVG_Y + dy, 999999999,0);
//			}
//			// RB_AVE
//			if (hallData[Defines.DEF_H_RB_COUNT] > 0) {
//				printPanelCounts(hallData[Defines.DEF_H_GAME_COUNT]
//						/ hallData[Defines.DEF_H_RB_COUNT], Defines.DEF_POS_NAVI_RBAVG_X,
//						Defines.DEF_POS_NAVI_RBAVG_Y + dy, 999999999,0);
//			}
//		}else{
//			// BB_AVE
//			if (int_s_value[Defines.DEF_INT_BIG_COUNT] > 0) {
//				printPanelCounts(int_s_value[Defines.DEF_INT_TOTAL_GAMES]
//						/ int_s_value[Defines.DEF_INT_BIG_COUNT], Defines.DEF_POS_NAVI_BBAVG_X,
//						Defines.DEF_POS_NAVI_BBAVG_Y + dy, Defines.DEF_POS_NAVI_BBAVG_MAX, 0);
//			}
//			// RB_AVE
//			if (int_s_value[Defines.DEF_INT_REG_COUNT] > 0) {
//				printPanelCounts(int_s_value[Defines.DEF_INT_TOTAL_GAMES]
//						/ int_s_value[Defines.DEF_INT_REG_COUNT], Defines.DEF_POS_NAVI_RBAVG_X,
//						Defines.DEF_POS_NAVI_RBAVG_Y + dy, Defines.DEF_POS_NAVI_RBAVG_MAX, 0);
//			}
//		}
//#if __COM_TYPE__
//		// クリッピング領域の解除
//		ZZ.setClip(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ.getHeight());
//#endif
//	}
#endif
	/**
	 * ゼロ埋めせずに数字を描く。（データパネル専用）
	 * 
	 * @param val
	 *            値
	 * @param xx
	 *            X座標（１桁目の左上）
	 * @param yy
	 *            Y座標（左上）
	 * @param max
	 *            表示値の最大値
	 * 
	 * @see #printNumber(int, int, int)
	 */
	private static void printPanelCounts(int val, int xx, int yy, int max,int col) {
		printPanelCounts(val, xx, yy, max, col, -1);
	}
	private static void printPanelCounts(int val, int xx, int yy, int max,int col,int keta) {
		// 桁チェック
		if (val < 0) {
			val = 0;
		}
		if (val > max) {
			val = max;
		}

		do {
			// 一桁は必ず描く
			// 下（右）から描いて行く
			switch (col) {
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

	private static void set4th(int id) {
		// 待ちから動作状態へ
		if (int_s_value[Defines.DEF_INT_4TH_ACTION_FLAG] == 0 && id > 0) {
			int_s_value[Defines.DEF_INT_RLPTNDT] = id - 1;
			int_s_value[Defines.DEF_INT_RLPTNDT_COUNTER] = 0;
			int_s_value[Defines.DEF_INT_RLPTNDT_FLAG] = 0;// 0:回転開始可1:回転中2:回転終了
			int_s_value[Defines.DEF_INT_4TH_ACTION_FLAG] = 1;// セット完了（動作待ち）
			isCanStop = false;
		}
	}

	static bool isCanStop = false;//センサー通過フラグ

	private static void action4th() {
		// 動作状態でなければ飛ばす
		if (int_s_value[Defines.DEF_INT_4TH_ACTION_FLAG] != 1) {
			return;
		}
		// 一時停止中は何もしない
		if (_4thTime > Util.GetMilliSeconds()) {
			return;
		}
		// 読込
		int[] data = RLPTNDT[int_s_value[Defines.DEF_INT_RLPTNDT]];
		// 回転タイミングがまだの時
		if (clOHHB_V23.getWork(Defines.DEF_PUSHCTR) > data[int_s_value[Defines.DEF_INT_RLPTNDT_COUNTER]] % 8) {
			return;
		}
		// 回転パラ夢
		int dir = data[int_s_value[Defines.DEF_INT_RLPTNDT_COUNTER] + 1] / 4;
		int spe = data[int_s_value[Defines.DEF_INT_RLPTNDT_COUNTER] + 1] % 4;
		int pos = data[int_s_value[Defines.DEF_INT_RLPTNDT_COUNTER] + 2];
		dir = (dir == 0) ? -1 : 1;
		spe = ((ZZ.getThreadSpeed()>=40)?20:10) / spe;
		bool snd4th = false;
		if(ZZ.getThreadSpeed()>=40){
			if(spe == 20){
				snd4th = true;
			}
		}else{
			if(spe == 10){
				snd4th = true;
			}
		}
		bool isNext = false;
		for (int i = 0; i < spe; i++) {
			// 回転停止タイミングか？
			if (data[int_s_value[Defines.DEF_INT_RLPTNDT_COUNTER]] / 8 == 0
					|| clOHHB_V23.getWork(Defines.DEF_PUSHCTR) == 0) {
				//センサーは通過しているか?
				if (isCanStop) {
					//さらに少なくとも赤ドン←大当り分以上は回らないといけない。
					if (int_s_value[Defines.DEF_INT_RLPTNDT_FLAG] > 100 / spe) {
						if (int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] == pos) {
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
			if(dir<0){//正回転のセンサー位置:はずれ～青ドンの間にある
				if(int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] == 270){
					isCanStop = true;
				}
			}else{//逆回転のセンサー位置:大当り～赤ドンの間にある
				if(int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE] == 60){
					isCanStop = true;
				}
			}

		}
		// DfMain.TRACE((int_s_value[Defines.DEF_INT_4TH_REEL_ANGLE]);
		if (isNext) {
			isCanStop = false;
			int_s_value[Defines.DEF_INT_TOP_LAMP] = 0;
			if (int_s_value[Defines.DEF_INT_RLPTNDT_COUNTER] + 3 < data.Length) {
				// 次のデータへ
				int_s_value[Defines.DEF_INT_RLPTNDT_COUNTER] += 3;
				int_s_value[Defines.DEF_INT_RLPTNDT_FLAG] = 0;
				_4thTime = Util.GetMilliSeconds() + 1000;
			} else {
				// 演出完了
				int_s_value[Defines.DEF_INT_4TH_ACTION_FLAG] = 0;
			}
			if (!IS_BONUS()
					&& int_s_value[Defines.DEF_INT_CURRENT_MODE] != Defines.DEF_RMODE_BB_FANFARE
					&& int_s_value[Defines.DEF_INT_CURRENT_MODE] != Defines.DEF_RMODE_RB_FANFARE) {
				ZZ.stopSound(Defines.DEF_SOUND_MULTI_BGM);
			}
			return;
		}

		lampSwitch(Defines.DEF_LAMP_4TH, Defines.DEF_LAMP_ACTION_ON);

		// 回転
		if (int_s_value[Defines.DEF_INT_RLPTNDT_FLAG] == 0 && !IS_BONUS()
				&& int_s_value[Defines.DEF_INT_CURRENT_MODE] != Defines.DEF_RMODE_BB_FANFARE
				&& int_s_value[Defines.DEF_INT_CURRENT_MODE] != Defines.DEF_RMODE_RB_FANFARE) {
			if(snd4th){
				playBGM(Defines.DEF_SOUND_10, true);
			}else{
				playSE(Defines.DEF_SOUND_11);
			}
//			switch (spe) {
//			case 20:
//				playBGM(Defines.DEF_SOUND_10, true);
//				break;
//			case 10:
//				playSE(Defines.DEF_SOUND_11);
//				break;
//			}
		}
		int_s_value[Defines.DEF_INT_RLPTNDT_FLAG]++;
		int_s_value[Defines.DEF_INT_TOP_LAMP] = (dir == -1) ? 4 : 5;
//		DfMain.TRACE(("******"+int_s_value[Defines.DEF_INT_RLPTNDT_FLAG]);
	}

	/**
	 * 動く上部ランプ
	 * 
	 * 演出ＩＤ
	 * 
	 * @param isRepaint
	 * @see #drawUpperLamp(int)
	 */
	private static void ctrlTopLamp() {
		if (IS_BONUS_JAC()
				|| int_s_value[Defines.DEF_INT_CURRENT_MODE] == Defines.DEF_RMODE_RB_FANFARE) {
			int_s_value[Defines.DEF_INT_TOP_LAMP] = 1;
		} else if (IS_BONUS_GAME()
				|| int_s_value[Defines.DEF_INT_CURRENT_MODE] == Defines.DEF_RMODE_BB_FANFARE) {
			int_s_value[Defines.DEF_INT_TOP_LAMP] = 2;
		} else if (IS_REPLAY()) {
			int_s_value[Defines.DEF_INT_TOP_LAMP] = 3;
		}

// satoh#暫定
		// 0: 点滅スピード(ms)
		if ((int_s_value[Defines.DEF_INT_MODE_COUNTER] % (FLLXX[int_s_value[Defines.DEF_INT_TOP_LAMP]][0]
				/ int_s_value[Defines.DEF_INT_LOOP_SPEED] + 1)) == 0) {
//		if ((int_s_value[Defines.DEF_INT_MODE_COUNTER] % (FLLXX[int_s_value[Defines.DEF_INT_TOP_LAMP]][0]
//				/ 1 + 1 )) == 0) {
			// ボーナス上部ランプの点滅スピード調整
			int_s_value[Defines.DEF_INT_SEQUENCE_EFFECT]++;
		}

		// Defines.DEF_INT_SEQUENCE_EFFECTを使いまわしているので、注意
		if (int_s_value[Defines.DEF_INT_SEQUENCE_EFFECT] >= FLLXX[int_s_value[Defines.DEF_INT_TOP_LAMP]].Length - 1) {
			int_s_value[Defines.DEF_INT_SEQUENCE_EFFECT] = 1;
		}

		//DfMain.TRACE(("Defines.DEF_INT_TOP_LAMP:" + int_s_value[Defines.DEF_INT_TOP_LAMP] + ":" + int_s_value[Defines.DEF_INT_SEQUENCE_EFFECT]);
		int data = FLLXX[int_s_value[Defines.DEF_INT_TOP_LAMP]][int_s_value[Defines.DEF_INT_SEQUENCE_EFFECT]];
		for (int i = 0; i < 8; i++) {
			int action = ((data & (1 << i)) != 0) ? Defines.DEF_LAMP_ACTION_ON: Defines.DEF_LAMP_ACTION_OFF;
			if (i < 5) {
				lampSwitch(Defines.DEF_LAMP_TOP_1 + i, action);
			} else {
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
	private static void ctrlButtonLamp() {
		for (int i = 0; i < 3; i++) {
			if (int_s_value[Defines.DEF_INT_REEL_STOP_R0 + i] != ANGLE_UNDEF
					|| (int_s_value[Defines.DEF_INT_KEY_REJECT] > 0)) {
				// 止められている又はキーリジェクト前
				lampSwitch(Defines.DEF_LAMP_BUTTON_L + i, Defines.DEF_LAMP_ACTION_OFF);
			} else {
				// リールが回ってるところだけ着色
				lampSwitch(Defines.DEF_LAMP_BUTTON_L + i, Defines.DEF_LAMP_ACTION_ON);
			}
		}
	}

	/**
	 * BETランプのスイッチ
	 * 
	 */
	private static void ctrlBetLamp() {
		switch (int_s_value[Defines.DEF_INT_CURRENT_MODE]) {
		case Defines.DEF_RMODE_BET:
		case Defines.DEF_RMODE_SPIN:
			for (int i = 0; i < 3; i++) {
				if (int_s_value[Defines.DEF_INT_BETTED_COUNT] > i) {
					lampSwitch(Defines.DEF_LAMP_BET_3 + i, Defines.DEF_LAMP_ACTION_ON);
					lampSwitch(Defines.DEF_LAMP_BET_3 - i, Defines.DEF_LAMP_ACTION_ON);
				} else {
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
			for (int i = 0; i < 5; i++) {
				if (_lampTime < Util.GetMilliSeconds()) {
					lampSwitch(Defines.DEF_LAMP_BET_1 + i, Defines.DEF_LAMP_ACTION_OFF);
				} else {
					if ((clOHHB_V23.getWork(Defines.DEF_HITLINE) & (Defines.DEF__00001000B << i)) != 0) {
						int id = 0;
						switch (i) {
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
						if (int_s_value[Defines.DEF_INT_ON_OFF_EFFECT] > 0) {
							lampSwitch(id, Defines.DEF_LAMP_ACTION_OFF);
						} else {
							lampSwitch(id, Defines.DEF_LAMP_ACTION_ON);
						}
					}
				}
			}
			break;
		}
	}

	private static void ctrlLamp() {
		if (int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] > 0) {
			if (int_s_value[Defines.DEF_INT_ON_OFF_EFFECT] > 0) {
				lampSwitch(Defines.DEF_LAMP_WIN, Defines.DEF_LAMP_ACTION_ON);
				lampSwitch(Defines.DEF_LAMP_BAR, Defines.DEF_LAMP_ACTION_ON);
			} else {
				lampSwitch(Defines.DEF_LAMP_WIN, Defines.DEF_LAMP_ACTION_OFF);
				lampSwitch(Defines.DEF_LAMP_BAR, Defines.DEF_LAMP_ACTION_OFF);
			}
			if (int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] > 1) {
				if (int_s_value[Defines.DEF_INT_ON_OFF_EFFECT] > 0) {
					lampSwitch(Defines.DEF_LAMP_4TH, Defines.DEF_LAMP_ACTION_OFF);
				} else {
					lampSwitch(Defines.DEF_LAMP_4TH, Defines.DEF_LAMP_ACTION_ON);
				}
			}
		} else {
			lampSwitch(Defines.DEF_LAMP_WIN, Defines.DEF_LAMP_ACTION_OFF);
			lampSwitch(Defines.DEF_LAMP_BAR, Defines.DEF_LAMP_ACTION_OFF);
		}
		switch (int_s_value[Defines.DEF_INT_CURRENT_MODE]) {
		case Defines.DEF_RMODE_RESULT:
			if (IS_REPLAY()) {
				lampSwitch(Defines.DEF_LAMP_FRE, Defines.DEF_LAMP_ACTION_ON);
			}
			break;
		case Defines.DEF_RMODE_WAIT:
		case Defines.DEF_RMODE_BET:
            // TODO C#移植 フォールスルーしていたのでC#で動くように変更
            if (int_s_value[Defines.DEF_INT_CURRENT_MODE] == Defines.DEF_RMODE_WAIT) {
                if (int_s_value[Defines.DEF_INT_WIN_LAMP_STATUS] == 2) {
                    if (int_s_value[Defines.DEF_INT_ON_OFF_EFFECT] > 0) {
                        int_s_value[Defines.DEF_INT_FLASH_DATA] = 0x1ff;
                    } else {
                        int_s_value[Defines.DEF_INT_FLASH_DATA] = 0;
                    }
                }
            }

			// ｽﾀｰﾄランプを点滅させる
			if (int_s_value[Defines.DEF_INT_BETTED_COUNT] > 0) {
				if (int_s_value[Defines.DEF_INT_ON_OFF_EFFECT] > 0) {
					lampSwitch(Defines.DEF_LAMP_STA, Defines.DEF_LAMP_ACTION_OFF);
				} else {
					lampSwitch(Defines.DEF_LAMP_STA, Defines.DEF_LAMP_ACTION_ON);
				}
			} else {
				lampSwitch(Defines.DEF_LAMP_STA, Defines.DEF_LAMP_ACTION_OFF);
			}
			// インサートコインランプを点滅させる
			if (!IS_REPLAY()
					&& ((IS_BONUS_JAC() && int_s_value[Defines.DEF_INT_BETTED_COUNT] < 1) || (!IS_BONUS_JAC() && (int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM] < Defines.DEF_NUM_MAX_CREDIT || int_s_value[Defines.DEF_INT_BETTED_COUNT] < 3)))) {
				if (int_s_value[Defines.DEF_INT_ON_OFF_EFFECT] > 0) {
					lampSwitch(Defines.DEF_LAMP_INS, Defines.DEF_LAMP_ACTION_ON);
				} else {
					lampSwitch(Defines.DEF_LAMP_INS, Defines.DEF_LAMP_ACTION_OFF);
				}
			} else {
				lampSwitch(Defines.DEF_LAMP_INS, Defines.DEF_LAMP_ACTION_OFF);
			}

			// BETボタンを点滅
			if (int_s_value[Defines.DEF_INT_CREDIT_COIN_NUM] > 0) {
				if ((IS_BONUS_JAC() && int_s_value[Defines.DEF_INT_BETTED_COUNT] < 1)
						|| (!IS_BONUS_JAC() && int_s_value[Defines.DEF_INT_BETTED_COUNT] < 3)) {
					if (int_s_value[Defines.DEF_INT_ON_OFF_EFFECT] > 0) {
						lampSwitch(Defines.DEF_LAMP_MAXBET, Defines.DEF_LAMP_ACTION_OFF);
					} else {
						lampSwitch(Defines.DEF_LAMP_MAXBET, Defines.DEF_LAMP_ACTION_ON);
					}
				} else {
					lampSwitch(Defines.DEF_LAMP_MAXBET, Defines.DEF_LAMP_ACTION_OFF);
				}
			} else {
				lampSwitch(Defines.DEF_LAMP_MAXBET, Defines.DEF_LAMP_ACTION_OFF);
			}

			break;
		case Defines.DEF_RMODE_SPIN:
			lampSwitch(Defines.DEF_LAMP_STA, Defines.DEF_LAMP_ACTION_OFF);
			lampSwitch(Defines.DEF_LAMP_INS, Defines.DEF_LAMP_ACTION_OFF);
			lampSwitch(Defines.DEF_LAMP_MAXBET, Defines.DEF_LAMP_ACTION_OFF);
			lampSwitch(Defines.DEF_LAMP_FRE, Defines.DEF_LAMP_ACTION_OFF);
			// ﾃﾝﾊﾟｲランプの点滅
			if (int_s_value[Defines.DEF_INT_IS_TEMPAI] == 1) {
				if (int_s_value[Defines.DEF_INT_ON_OFF_EFFECT] > 0) {
					lampSwitch(Defines.DEF_LAMP_CHANCE, Defines.DEF_LAMP_ACTION_OFF);
				} else {
					lampSwitch(Defines.DEF_LAMP_CHANCE, Defines.DEF_LAMP_ACTION_ON);
				}
			} else {
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
    public static bool IsReelStopped(int stopNum) {
        if ((int_s_value[Defines.DEF_INT_IS_REEL_STOPPED] & BIT(stopNum)) != 0) {
            return true;
        } else {
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
	private static bool setReelStopAngle(int stopNum) {
        if (IsReelStopped(stopNum)) {
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
		
		if( Mobile.isJacCut() == true)
		{
			return true;
		}
//		if (Mobile.isJacCut() && IS_BONUS_JAC()) {
//			DfMain.TRACE(("JACkカットになっている？");
//			return true;
//		}
		
		return false;
	} // End of setReelStopAngle() Method

	/**
	 * 各種払い出し音セット
	 */
	private static void playCoinSound() {
		int snd_id = Defines.DEF_SOUND_UNDEF;
		_soundTime = Util.GetMilliSeconds() + 0;
		if (int_s_value[Defines.DEF_INT_WIN_COIN_NUM] <= 0) {
			if (IS_REPLAY()) {
				snd_id = Defines.DEF_SOUND_24;
				_soundTime = Util.GetMilliSeconds() + Defines.DEF_SOUND_MS_24;
			}
		} else if (int_s_value[Defines.DEF_INT_WIN_COIN_NUM] < 15) {
			snd_id = Defines.DEF_SOUND_16;
			_soundTime = Util.GetMilliSeconds()
					+ (120 * int_s_value[Defines.DEF_INT_WIN_COIN_NUM]);
		} else {
			if (IS_BONUS_JAC()) {
				snd_id = Defines.DEF_SOUND_18;
				_soundTime = Util.GetMilliSeconds() + Defines.DEF_SOUND_MS_18;
			} else {
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
	private static void shiftDataPanelHistory(int current, int game_kind) {
		// Y軸 高さ.
		int idx = current / Defines.DEF_UNIT_GAMES;
		if (idx >= Defines.DEF_INFO_GAMES) { // == bonus_Data[x].Length
			idx = Defines.DEF_INFO_GAMES - 1;
		}

		// --- set --
		if (game_kind == Defines.DEF_PS_BB_RUN) {
			bonus_Data[int_s_value[Defines.DEF_INT_BONUS_DATA_BASE],idx] = Defines.DEF_GAME_BIG;
		} else if (game_kind == Defines.DEF_PS_RB_RUN) {
			bonus_Data[int_s_value[Defines.DEF_INT_BONUS_DATA_BASE],idx] = Defines.DEF_GAME_REG;
		}

		// -- shift --
		int_s_value[Defines.DEF_INT_BONUS_DATA_BASE]--;
		if (int_s_value[Defines.DEF_INT_BONUS_DATA_BASE] < 0) {
            int_s_value[Defines.DEF_INT_BONUS_DATA_BASE] = Defines.DEF_INFO_GAME_HISTORY - 1; 
		}
        for (int i = 0; i < Defines.DEF_INFO_GAMES; i++) {
			bonus_Data[int_s_value[Defines.DEF_INT_BONUS_DATA_BASE],i] = Defines.DEF_GAME_NONE;
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
	private static void setCurrentDataPanel(int current) {
		// Y軸 高さ.
		int idx = (current - 1) / Defines.DEF_UNIT_GAMES;
		if (idx >= Defines.DEF_INFO_GAMES) { // == bonus_Data[x].Length
			idx = Defines.DEF_INFO_GAMES - 1;
		}

		// 通常.
		if (bonus_Data[int_s_value[Defines.DEF_INT_BONUS_DATA_BASE],idx] == Defines.DEF_GAME_NONE) {
			bonus_Data[int_s_value[Defines.DEF_INT_BONUS_DATA_BASE],idx] = Defines.DEF_GAME_NORMAL;
		}
	}

	/**
	 * ＢＢ作動中ですか？
	 * 
	 * @return true=ＢＢ作動中
	 */
	public static bool IS_BONUS_GAME() {
		return (clOHHB_V23.getWork(Defines.DEF_GAMEST) & 0x80) != 0 && !IS_BONUS_JAC();
	}

	/**
	 * BONUS中ですか？
	 * 
	 * @return
	 */
    public static bool IS_BONUS() {
		return IS_BONUS_GAME() || IS_BONUS_JAC();
	}

	/**
	 * ＲＢ作動中ですか？
	 * 
	 * @return true=ＲＢ作動中
	 */
    public static bool IS_BONUS_JAC() {
		return (clOHHB_V23.getWork(Defines.DEF_GAMEST) & 0x01) != 0;
	}

	/**
	 * 
	 */
	public static bool IS_REPLAY() {
		return clOHHB_V23.getWork(Defines.DEF_GAMEST) == 0x4
				|| (!IS_BONUS() && clOHHB_V23.getWork(Defines.DEF_HITFLAG) == 0x8);
	}

	public static int getReelId(int reelBit) {
		switch (reelBit) {
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
#if __COM_TYPE__
	
#else
//	private static bool checkLimit() {
//		// bool flag = false;
//		int_s_value[Defines.DEF_INT_WHAT_EXIT] = Defines.DEF_EXIT_NON;
//		// if (!IS_BONUS()) {
//		// if (Mobile.getGameMode() == Defines.DEF_GMODE_SIMURATION) {
//		// if (int_s_value[Defines.DEF_INT_BIG_COUNT] > Defines.DEF_NUM_SIMULATION_BB_RB_LIMIT)
//		// flag = true;
//		// if (int_s_value[Defines.DEF_INT_REG_COUNT] > Defines.DEF_NUM_SIMULATION_BB_RB_LIMIT)
//		// flag = true;
//		// } else if (Mobile.getGameMode() == Defines.DEF_GMODE_GAME) {
//		// if (int_s_value[Defines.DEF_INT_BIG_COUNT] > Defines.DEF_NUM_JISSEN_LIMIT)
//		// flag = true;
//		// if (int_s_value[Defines.DEF_INT_REG_COUNT] > Defines.DEF_NUM_JISSEN_LIMIT)
//		// flag = true;
//		// }
//		// }
//		// if (Mobile.getGameMode() == Defines.DEF_GMODE_SIMURATION) {
//		// if (int_s_value[Defines.DEF_INT_TOTAL_GAMES] > Defines.DEF_NUM_SIMULATION_LIMIT)
//		// flag = true;
//		// if (int_s_value[Defines.DEF_INT_SLOT_COIN_INNER_COUNT] >
//		// Defines.DEF_NUM_SIMULATION_LIMIT)
//		// flag = true;
//		// } else if (Mobile.getGameMode() == Defines.DEF_GMODE_GAME) {
//		// if (int_s_value[Defines.DEF_INT_TOTAL_GAMES] > Defines.DEF_NUM_JISSEN_LIMIT)
//		// flag = true;
//		// if (int_s_value[Defines.DEF_INT_SLOT_COIN_INNER_COUNT] > Defines.DEF_NUM_JISSEN_LIMIT)
//		// flag = true;
//		// }
//		if(ZZ.getAuthResult() == 0 || IS_HALL()){
//			int[] param = getSendParams();
//			for (int i = 0; i < param.Length; i++) {
//				if (param[i] >= 99999 || param[i] <= -99999) {
//					int_s_value[Defines.DEF_INT_WHAT_EXIT] = Defines.DEF_EXIT_OVER_INT;
//					return true;
//				}
//			}
//		}else{
//			if(int_s_value[Defines.DEF_INT_TOTAL_GAMES] >= Defines.DEF_TRIAL_GAME){
//				int_s_value[Defines.DEF_INT_WHAT_EXIT] = Defines.DEF_EXIT_OVER_INT;
//				return true;
//			}
//		}
//		// if (flag)
//		// int_s_value[Defines.DEF_INT_WHAT_EXIT] = Defines.DEF_EXIT_OVER_INT;
//		// return flag;
//		return false;
//	}
#endif
	private static int[] isTempai() {
		int[] tempai = { Defines.DEF_BB_UNDEF, 0 };
		int yukou = 5;// 有効ライン
		if (int_s_value[Defines.DEF_INT_BET_COUNT] == 1) {
			yukou = 1;
		} else if (int_s_value[Defines.DEF_INT_BET_COUNT] == 2) {
			yukou = 3;
		}
		for (int line = 0; line < yukou; line++) {
			int tmp = Defines.DEF_ARAY;
			for (int reel = 0; reel < 3; reel++) {
				tmp &= clOHHB_V23.getWork(Defines.DEF_ARAY11 + (line * 3) + reel);
			}
			if ((tmp & Defines.DEF_BSVN) != 0) {
				tempai[0] = Defines.DEF_BB_B7;
			} else if ((tmp & Defines.DEF_DON_) != 0) {
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
    [Obsolete("削除予定とのコメントあり")]
	public static int BIT(int n) {
		return 1 << n;
	}

	/// <summary>
    /// SE を再生(Director.directionデータから選ばず、直接鳴動を指定するときに使う)
	/// </summary>
	/// <param name="id"></param>
	public static void playSE(int id) {
		if (Defines.DEF_USE_MULTI_SOUND) {
			// 重畳の場合
			Mobile.playSound(id, false, Defines.DEF_SOUND_MULTI_SE);
		} else {
			// 非重畳の場合はボーナスゲームのＢＧＭを優先して鳴らす（＝ボーナスゲームでないときだけ鳴らせる）
			if (!IS_BONUS_GAME()) {
				// 非重畳の場合は第三引数に意味はない！！
				Mobile.playSound(id, false, Defines.DEF_SOUND_MULTI_SE);
			}
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
	public static void playBGM(int id, bool loop) {
		// 非重畳の場合は第３引数は無視されるのよ
#if __COM_TYPE__
		if( loop == true)
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
		//Mobile.playSound(id, loop, Defines.DEF_SOUND_MULTI_BGM);
	}

	// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
	// もともとDirector.classだったがプログラムサイズが大きすぎてこちらに移動
	// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
	// /**
	// * 現在のフラッシュデータ
	// */
	// private static char[] flash = { 0 };

	/** 現在位置 */
	private static int current = 0;

	/** 繰り返し回数 */
	private static int repeat = 0;

	/** 終了判定 */
	private static bool play = false;

	/**
	 * フラッシュを設定、各種初期化
	 */
	public static void setFlash(int idx) {
		if (idx > 0) {
			flash = FLASHTBL[idx - 1];
			current = -2;
			repeat = 0;
			play = true;
		}
	}

	/**
	 * @return 次のフラッシュ位置
	 */
	public static int getNext() {
		if (repeat <= 0) {
			current += 2;
			if (current < flash.Length) {
				repeat = flash[current];
				if (repeat != Defines.DEF_FEND) {
					repeat = repeat * 3 / 5;
					if (repeat == 0) {
						repeat = 1;
					}
					return flash[current + 1];
				}
			}
		} else {
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
	public static bool isPlay() {
		return play;
	}

	public static readonly int[][] RLPTNDT = {
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

	// private static final char[][] FLASHTBL = {
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
	// public static final char[][] FLLXX = {
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
	private static char[] flash = "\u0000".ToCharArray();

	// /** AUTO GENERATED char ARRAY BY compact.CompactClass */
	// public static final char[][] RLPTNDT = {
	// "\u0000\u0005\\\u0020\u0002\\\u0020\u0006\\\u0020\u0002\u0002"
	// .ToCharArray(),
	// "\u0000\u0005\\\u0020\u0002\\\u0020\u0006\u010D\u0020\u0002\u0002"
	// .ToCharArray(),
	// "\u0000\u0001\\\u0020\u0005\u010D\u0020\u0002\u0002".ToCharArray(),
	// "\u0000\u0001\\\u0020\u0005\u010D\u0020\u0002\u00D2".ToCharArray(),
	// "\u0000\u0005\\\u0020\u0005\u0099".ToCharArray(),
	// "\u0000\u0001\u010D\u0020\u0005\u010D\u0020\u0002\u010D\u0020\u0002\u0002"
	// .ToCharArray(),
	// "\u0000\u0001\u010D\u0020\u0001\u010D\u0020\u0006\\\u0020\u0006\u0002"
	// .ToCharArray(),
	// "\u0000\u0005\u010D\u0020\u0002\\\u0020\u0002\u00D2\u0020\u0006\u0002"
	// .ToCharArray(),
	// "\u0000\u0005\u010D\u0020\u0002\\\u0020\u0002\u00D2".ToCharArray(),
	// "\u0000\u0001\u010D\u0020\u0005\u0099".ToCharArray(),
	// "\u0001\u0005\\\u0020\u0005\u0099".ToCharArray(),
	// "\u0001\u0005\u010D\u0020\u0005\u0099".ToCharArray(),
	// "\u0001\u0001\\".ToCharArray(),
	// "\u0001\u0001\u010D".ToCharArray(),
	// "\u0001\u0001\u0099".ToCharArray(),
	// "\u0001\u0001\u0149".ToCharArray(),
	// "\u0002\u0005\\\u0020\u0001\u0149".ToCharArray(),
	// "\u0002\u0005\u010D\u0020\u0001\u0149".ToCharArray(),
	// "\u0002\u0001\\".ToCharArray(),
	// "\u0002\u0001\u010D".ToCharArray(),
	// "\u0002\u0001\u0099".ToCharArray(),
	// "\u0002\u0001\u0149".ToCharArray(),
	// "\u0003\u0005\u0002".ToCharArray(),
	// "\u0003\u0001\u00D2".ToCharArray(),
	// "\u0013\u0001\u0149\u0020\u0001\u015F\u0020\u0001\u0175\u0020\u0001\u018B\u0020\u0002\u00D2"
	// .ToCharArray(),
	// "\u0013\u0001\u0149\u0020\u0001\u015F\u0020\u0001\u0175\u0020\u0001\u018B\u0020\u0002\u0002"
	// .ToCharArray(), "\u0003\u0001\\".ToCharArray(),
	// "\u0003\u0005\u010D".ToCharArray(), };

	/** AUTO GENERATED char ARRAY BY compact.CompactClass */
	private static char[][] FLASHTBL = {
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
	public static char[][] FLLXX = {
			"\u0064\u0000\u0000\u0000\u0000\u0000".ToCharArray(),
			"\u0078\u0091\u004E\u0024\u004E\u0000".ToCharArray(),
			"\u0050\u0035\u007B\u00E4\n\u0000".ToCharArray(),
			"\u00C8\u0011\u000E\u0011\u000E\u0000".ToCharArray(),
			"\u0050\u0092\u0049\u0024\u0000\u0000".ToCharArray(),
			"\u0050\u0089\u0052\u0024\u0000\u0000".ToCharArray(), };

	// //////////////////////////////////////////////////////////////
	// compact.CompactClass byte[]

	/*------------------ class Director ------------------*/
#if __COM_TYPE__
	
#else
//	public static int[] getSendParams() {
//		int[] param = new int[Defines.DEF_PARAM_NUM];
//		param[Defines.DEF_PARAM_COIN_NUM] = getCoinNum() < 0 ? 0 : getCoinNum();// 獲得枚数
//		param[Defines.DEF_PARAM_KISYU_ID] = Defines.DEF_KISYU_ID;// 機種ID 大祭り
//		param[Defines.DEF_PARAM_TOTAL_IN] = int_s_value[Defines.DEF_INT_TOTAL_BET];// 投入総数
//		param[Defines.DEF_PARAM_TOTAL_OUT] = int_s_value[Defines.DEF_INT_TOTAL_PAY];// 払出総数
//		param[Defines.DEF_PARAM_TOTAL_GAME] = int_s_value[Defines.DEF_INT_TOTAL_GAMES];// 総ｹﾞｰﾑ数
//		param[Defines.DEF_PARAM_SETTEI] = Mobile.getSetUpValue();// 設定値
////----------------------------------ココまで固定
//		param[Defines.DEF_PARAM_BB_COUNT] = int_s_value[Defines.DEF_INT_BIG_COUNT];// BB入賞回数
//		param[Defines.DEF_PARAM_RB_COUNT] = int_s_value[Defines.DEF_INT_REG_COUNT];// RB入賞回数
//		param[Defines.DEF_PARAM_BB_MAX_COIN] = int_s_value[Defines.DEF_INT_GAME_INFO_MAX_GOT];// BB最大獲得枚数
//		param[Defines.DEF_PARAM_BB_TOTAL_COIN] = int_s_value[Defines.DEF_INT_BB_TOTAL_GOT];// BBトータル獲得枚数
//		param[Defines.DEF_PARAM_CHRY_HIT] = int_s_value[Defines.DEF_INT_CHRY_HIT];// チェリー当選回数
//		param[Defines.DEF_PARAM_CHRY_GOT] = int_s_value[Defines.DEF_INT_CHRY_GOT];// チェリー入賞回数
//		param[Defines.DEF_PARAM_WMLN_HIT] = int_s_value[Defines.DEF_INT_WMLN_HIT];// ｽｲｶ当選回数
//		param[Defines.DEF_PARAM_WMLN_GOT] = int_s_value[Defines.DEF_INT_WMLN_GOT];// ｽｲｶ入賞回数
//		param[Defines.DEF_PARAM_FLAG_GAME] = int_s_value[Defines.DEF_INT_FLAG_GAME_COUNT];// フラグ間ｹﾞｰﾑ回数
//		param[Defines.DEF_PARAM_JAC_HIT] = int_s_value[Defines.DEF_INT_JAC_HIT];// JAC抽選回数
//		param[Defines.DEF_PARAM_HAZUSHI] = int_s_value[Defines.DEF_INT_HAZUSI_COUNT];// ビタは図示
//		param[Defines.DEF_PARAM_BB_GET_OVER711] = int_s_value[Defines.DEF_INT_BB_GET_OVER711];// BB獲得枚数 711枚以上？
//		param[Defines.DEF_PARAM_BB_END_1GAME_REGET_BB] = int_s_value[Defines.DEF_INT_BB_END_1GAME_REGET_BB];// BB終了後の１ゲームでＢＢに入賞したか？（＝揃えたか？）
//
////----------------------------------// TOBE 後に追加PARAM
////		param[Defines.DEF_PAMRAM_VER] = Mobile.getAppVersion(); // アプリバージョン
//
//				
//		// for(int i = 0;i<param.Length;i++){
//		// param[i] = 99999;
//		// }
//		return param;
//	}
#endif
	// /////////////////////////////////
	// BREWからの告知移植
#if __COM_TYPE__
#else
//
//	private static int m_nYokokuBonus = 0;// 告知画像(ボーナス)
//
//	private static int m_nYokokuBonusPosX = 0;// ボーナス絵柄のX座標
//
//	private static bool m_fYokokuPaint = false;
//
//	private static bool[] m_nYokokuMove = new bool[2];// 0：現ゲームの役、//1:前ゲームの役
//
//	private static int[] m_nYokokuPosX = new int[2];// 0：現ゲームの役、//1:前ゲームの役
//
//	private static int[] m_nYokoku2 = new int[2];// 0：現ゲームの役、//1:前ゲームの役
//
//	private static void InitYokoku() {
//		m_nYokoku2[0] = m_nYokoku2[1] = m_nYokokuBonus = -1;
//		m_nYokokuPosX[0] = m_nYokokuPosX[1] = m_nYokokuBonusPosX = 240;
//		m_nYokokuMove[0] = m_nYokokuMove[1] = m_fYokokuPaint = false;
//	}
//
//	// 出現
//	private static void ForwordYokoku() {
//		m_fYokokuPaint = true;
//		m_nYokokuMove[0] = true;
//	}
//
//	// 帰る
//	private static void BackYokoku() {
//		m_fYokokuPaint = true;
//		m_nYokokuMove[0] = false;// 出現をクリア
//		m_nYokokuMove[1] = true;// 帰る。
//	}
//
//	private static void SetYokoku() {
//		if (Mobile.getKokuchi() == Defines.DEF_SELECT_3_OFF) {
//			// オフなら全てクリア
//			InitYokoku();
//			return;
//		}
//
//		int wavebit = clOHHB_V23.getWork(Defines.DEF_WAVEBIT);
//
//		m_nYokoku2[1] = m_nYokoku2[0];
//		m_nYokoku2[0] = m_nYokokuBonus = -1;
//
//		m_nYokokuPosX[1] = m_nYokokuPosX[0];
//		m_nYokokuPosX[0] = m_nYokokuBonusPosX = 240;
//		// HFLGTBL EQU $
//		// ; BB未作動時 | BB作動時
//		// DB 000H,000H ; [ 0 ] はずれ | はずれ
//		// DB 000H,001H ; [ 1 ] ﾁｪﾘｰ | 15枚
//		// DB 000H,002H ; [ 2 ] ﾊﾅﾋﾞ | ﾊﾅﾋﾞ
//		// DB 000H,004H ; [ 3 ] 大山 | 大山 or ﾁｪﾘｰ
//		// DB 000H,008H ; [ 4 ] 再遊技 | BB中のRB
//		// DB 010H,010H ; [ 5 ] RB | なし
//		// DB 020H,020H ; [ 6 ] BB | なし
//
//		if ((wavebit & 0x01) != 0) {
//			if (IS_BONUS()) {
//				m_nYokoku2[0] = Defines.DEF_RES_KOKU_03;
//			} else {
//				m_nYokoku2[0] = Defines.DEF_RES_KOKU_06;
//			}
//		} else if ((wavebit & 0x02) != 0) {
//			m_nYokoku2[0] = Defines.DEF_RES_KOKU_04;
//		} else if ((wavebit & 0x04) != 0) {
//			m_nYokoku2[0] = Defines.DEF_RES_KOKU_05;
//		} else if ((wavebit & 0x8) != 0) {
//			if (IS_BONUS_JAC()) {
//				m_nYokoku2[0] = Defines.DEF_RES_KOKU_08;
//			} else {
//				m_nYokoku2[0] = Defines.DEF_RES_KOKU_07;
//			}
//		}
//
//		if ((wavebit & 0x10) != 0) {// RB内部中
//			m_nYokokuBonus = Defines.DEF_RES_KOKU_02;
//			m_nYokokuBonusPosX = Defines.DEF_POS_BONUS_X;
//		} else if ((wavebit & 0x20) != 0) {// BB内部中
//			m_nYokokuBonus = Defines.DEF_RES_KOKU_01;
//			m_nYokokuBonusPosX = Defines.DEF_POS_BONUS_X;
//		}
//
//		// // REG確定中なら
//		// if ((Babl_v32.DATA[DfBabl_v32_DEF.GMLVSTS] & 0x08) != 0) {
//		// // BB内部当たり
//		// m_nYokokuBonus = Defines.DEF_RES_KOYAKU_02;// 下
//		// m_nYokokuBonusPosX = Defines.DEF_POS_BONUS_X;
//		// }
//		// // BIG確定中なら
//		// if ((Babl_v32.DATA[DfBabl_v32_DEF.GMLVSTS] & 0x10) != 0) {
//		// // BB内部当たり
//		// m_nYokokuBonus = Defines.DEF_RES_KOYAKU_01;// 下
//		// m_nYokokuBonusPosX = Defines.DEF_POS_BONUS_X;
//		// }
//		// }
//	}
#endif
#if __COM_TYPE__
#else
//	private static void DrawYokoku() {
//		drawK4();
//		ZZ.setClip(Defines.DEF_POS_KOKU_B_X, Defines.DEF_POS_KOKU_B_Y, 240 - Defines.DEF_POS_KOKU_B_X, 30);
//		ZZ.drawImage(Defines.DEF_RES_K1, Defines.DEF_POS_K1_X, Defines.DEF_POS_K1_Y);
//		ZZ.drawImage(Defines.DEF_RES_K2, Defines.DEF_POS_K2_X, Defines.DEF_POS_K2_Y);
////		ZZ.drawImage(Defines.DEF_RES_K4, Defines.DEF_POS_K4_X, Defines.DEF_POS_K4_Y);
//		// ジッセンモード時
//		if (Mobile.getGameMode() == Defines.DEF_GMODE_GAME)
//			return;
//		// 告知オフ時
//		if (Mobile.getKokuchi() == Defines.DEF_SELECT_3_OFF)
//			return;
//
//		// 出現を優先する。
//		if (m_nYokokuMove[0]) {
//			m_fYokokuPaint = true;
//			// ボーナス絵柄
//			if (m_nYokokuBonus != -1) {
//				m_nYokokuBonusPosX -= 20;
//				if (m_nYokokuBonusPosX < Defines.DEF_POS_KOKU_B_X)// > POS_YOKOKU_X )
//				{
//					m_nYokokuBonusPosX = Defines.DEF_POS_KOKU_B_X;
//				}
//			}
//
//			// 小役絵柄
//			if (m_nYokoku2[0] > 0) {
//				if (m_nYokoku2[0] != m_nYokoku2[1]) {
//					// 前ゲームと異なる役の場合は引っ込む
//					m_nYokokuPosX[0] -= 20;
//					if (m_nYokokuPosX[0] < Defines.DEF_POS_KOKU_K_X)// > POS_YOKOKU_X )
//					{
//						m_nYokokuPosX[0] = Defines.DEF_POS_KOKU_K_X;
//						m_nYokokuMove[0] = false;
//					}
//				} else {
//					// 前ゲームと同じ役の場合はそのまま(帰らない)
//					m_nYokokuPosX[0] = Defines.DEF_POS_KOKU_K_X;
//					m_nYokokuMove[0] = m_nYokokuMove[1] = false;
//
//				}
//			}
//		}
//		// 帰る
//		if (m_nYokokuMove[1]) {
//			if (m_nYokoku2[1] > 0) {
//				m_fYokokuPaint = true;
//				m_nYokokuPosX[1] += 20;
//				if (m_nYokokuPosX[1] >= 240) {
//					m_nYokokuPosX[1] = 240;
//					m_nYokokuMove[1] = false;
//				}
//			}
//		}
//
//		if (!m_fYokokuPaint)
//			return;
//
//		// 小役絵柄
//		if (m_nYokoku2[0] != -1) {
//			ZZ.drawImage(m_nYokoku2[0], m_nYokokuPosX[0], Defines.DEF_POS_KOKU_K_Y);
//		}
//		if (m_nYokoku2[1] != -1) {
//			ZZ.drawImage(m_nYokoku2[1], m_nYokokuPosX[1], Defines.DEF_POS_KOKU_K_Y);
//		}
//
//		// ボーナス絵柄
//		if (m_nYokokuBonus == Defines.DEF_RES_KOKU_01
//				|| m_nYokokuBonus == Defines.DEF_RES_KOKU_02) {
//			ZZ.drawImage(m_nYokokuBonus, m_nYokokuBonusPosX, Defines.DEF_POS_KOKU_B_Y);
//		}
//		
//#if __COM_TYPE__
//		// クリッピング領域の解除
//		ZZ.setClip(-ZZ.getOffsetX(), -ZZ.getOffsetY(), ZZ.getWidth(), ZZ.getHeight());
//#endif
//	}
#endif
	/** 3D Rect用 */
	private static int[] polygon = new int[4 * 3];// xyz*4

	/** 3D Rect用 */
	private static int[] polygon_color = new int[3];// rgb

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
	public static void _drawEffect(int x0, int y0, int w, int h, int r, int g,
			int b, int ink) {

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

		if (ink == Defines.DEF_INK_SUB) {
			ZZ.drawPolygonRectSub(polygon, polygon_color);
		} else if (ink == Defines.DEF_INK_ADD) {
			ZZ.drawPolygonRectAdd(polygon, polygon_color);
		} else {
			ZZ.drawPolygonRect(polygon, polygon_color);
		}
		ZZ.flush3D();
	}

}

//using UnityEngine;
using System.Collections;

public partial class Defines {
/*
 * 作成日: 2006/02/05
 */


/**
 * 240 サイズの位置定義
 * 
 * @author A05229AK
 */

	/** 会員ﾒｯｾｰｼﾞ */
	public const int DEF_POS_HTTP_CONTENT_Y = 150;
	public const int DEF_POS_HTTP_CONTENT_H = 240;

	// 共通
	// //////////////////////////////////////////////////////////////
	// 画面サイズ
	// [ 可能：再設定 ]

	/**
	 * 定義横サイズ <br>[ 可能：再設定 ]
	 */
	public const int DEF_POS_WIDTH = 240;

	/**
	 * 定義縦サイズ <br>[ 可能：再設定 ]
	 */
	public const int DEF_POS_HEIGHT = 240;

	/**
	 * 筐体以外定義横位置 <br>[ 可能：再設定 ]
	 */
	public const int DEF_OF_X = 0;

	/**
	 * 筐体以外定義縦位置 <br>[ 可能：再設定 ]
	 */
	public const int DEF_OF_Y = 0;

	/**
	 * 筐体含む定義横サイズ <br>[ 可能：再設定 ]
	 */
	public const int DEF_POS_WIDTH_LARGE = DEF_POS_WIDTH;

	/**
	 * 筐体含む定義縦サイズ <br>[ 可能：再設定 ]
	 */
	public const int DEF_POS_HEIGHT_LARGE = DEF_POS_HEIGHT;

	// //////////////////////////////////////////////////////////////
	// タイトル
	/** タイトル画像 */
	public const int DEF_POS_TITLE_X = 0;
	public const int DEF_POS_TITLE_Y = 6;
	public const int DEF_POS_TITLE_W = 240;
	public const int DEF_POS_TITLE_H = 140;
	
	public const int DEF_POS_MENU_X = 15;
	public const int DEF_POS_MENU_Y = 51;
	public const int DEF_POS_MENU_DY = 24;

	// //////////////////////////////////////////////////////////////
	// メニューの基本色
	public const int DEF_POS_DEFAULT_COLOR_1_R = 0x33;
	public const int DEF_POS_DEFAULT_COLOR_1_G = 0x41;
	public const int DEF_POS_DEFAULT_COLOR_1_B = 0xA9;

	public const int DEF_POS_DEFAULT_COLOR_2_R = 0x77;
	public const int DEF_POS_DEFAULT_COLOR_2_G = 0x82;
	public const int DEF_POS_DEFAULT_COLOR_2_B = 0xD0;

	public const int DEF_POS_DEFAULT_COLOR_3_R = 0xC8;
	public const int DEF_POS_DEFAULT_COLOR_3_G = 0xCF;
	public const int DEF_POS_DEFAULT_COLOR_3_B = 0xFE;

	public const int DEF_POS_DEFAULT_COLOR_4_R = 0xFF;
	public const int DEF_POS_DEFAULT_COLOR_4_G = 0xFF;
	public const int DEF_POS_DEFAULT_COLOR_4_B = 0xFF;

	// //////////////////////////////////////////////////////////////
	// タイトルメニュー
	/** 実践モード */
	public const int DEF_POS_TITLE_MODE_JISEN_X = 75;
	public const int DEF_POS_TITLE_MODE_JISEN_Y = 154;
	public const int DEF_POS_TITLE_MODE_JISEN_W = 90;
	public const int DEF_POS_TITLE_MODE_JISEN_H = 13;

	/** 分析モード */
	public const int DEF_POS_TITLE_MODE_KOU_X = 76;
	public const int DEF_POS_TITLE_MODE_KOU_Y = 172;
	public const int DEF_POS_TITLE_MODE_KOU_W = 89;
	public const int DEF_POS_TITLE_MODE_KOU_H = 14;

	/** モード選択(塗り) */
	public const int DEF_POS_MODE_SELECT_L_X = 58;
	public const int DEF_POS_MODE_SELECT_R_X = 169;
	public const int DEF_POS_MODE_SELECT_Y = 156;
	public const int DEF_POS_MODE_SELECT_W = 13;
	public const int DEF_POS_MODE_SELECT_H = 11;
	public const int DEF_POS_MODE_SELECT_DY = 18;
	public const int DEF_POS_MODE_SELECT_COLOR_R = DEF_POS_DEFAULT_COLOR_2_R;
	public const int DEF_POS_MODE_SELECT_COLOR_G = DEF_POS_DEFAULT_COLOR_2_G;
	public const int DEF_POS_MODE_SELECT_COLOR_B = DEF_POS_DEFAULT_COLOR_2_B;

	/** 設定って画像 */
	public const int DEF_POS_TITLE_LEVEL_X = 47;
	public const int DEF_POS_TITLE_LEVEL_Y = 202;
	public const int DEF_POS_TITLE_LEVEL_W = 30;
	public const int DEF_POS_TITLE_LEVEL_H = 15;

	/** 設定 数字 */
	public const int DEF_POS_MENU_BOX_NO_X = 81;
	public const int DEF_POS_MENU_BOX_NO_Y = 202;
	public const int DEF_POS_MENU_BOX_NO_W = 15;
	public const int DEF_POS_MENU_BOX_NO_1 = 16;
	public const int DEF_POS_MENU_BOX_NO_DX = 16;
	public const int DEF_POS_MENU_BOX_NO_NUM = 7;

	/** 設定選択(塗り) */
	public const int DEF_POS_SELECT_LEVEL_X = 81;
	public const int DEF_POS_SELECT_LEVEL_Y = 201;
	public const int DEF_POS_SELECT_LEVEL_W = 15;
	public const int DEF_POS_SELECT_LEVEL_H = 18;
	public const int DEF_POS_SELECT_LEVEL_DX = 16;
	public const int DEF_POS_SELECT_LEVEL_COLOR_R = 0xff;
	public const int DEF_POS_SELECT_LEVEL_COLOR_G = 0xff;
	public const int DEF_POS_SELECT_LEVEL_COLOR_B = 0xff;

	/** 設定 背景 中 塗り */
	public const int DEF_POS_SELECT_LEVEL_BG_X = 39;
	public const int DEF_POS_SELECT_LEVEL_BG_Y = 200;
	public const int DEF_POS_SELECT_LEVEL_BG_W = 162;
	public const int DEF_POS_SELECT_LEVEL_BG_H = 20;
	public const int DEF_POS_SELECT_LEVEL_BG_COLOR_R = DEF_POS_DEFAULT_COLOR_3_R;
	public const int DEF_POS_SELECT_LEVEL_BG_COLOR_G = DEF_POS_DEFAULT_COLOR_3_G;
	public const int DEF_POS_SELECT_LEVEL_BG_COLOR_B = DEF_POS_DEFAULT_COLOR_3_B;

	/** 設定 背景 左右グラデ↓ */
	public const int DEF_POS_SELECT_LEVEL_BG_L_X = 0;
	public const int DEF_POS_SELECT_LEVEL_BG_R_X = 201;
	public const int DEF_POS_SELECT_LEVEL_BG_L_Y = 200;
	public const int DEF_POS_SELECT_LEVEL_BG_L_W = 39;
	public const int DEF_POS_SELECT_LEVEL_BG_L_H = 20;
	public const int DEF_POS_SELECT_LEVEL_BG_COLOR_NUM = 13;
	public const int DEF_POS_SELECT_LEVEL_BG_DX = DEF_POS_SELECT_LEVEL_BG_L_W/ DEF_POS_SELECT_LEVEL_BG_COLOR_NUM;
	public const int DEF_POS_SELECT_LEVEL_BG_L_COLOR_SR = 0x00;
	public const int DEF_POS_SELECT_LEVEL_BG_L_COLOR_SG = 0x00;
	public const int DEF_POS_SELECT_LEVEL_BG_L_COLOR_SB = 0x00;
	public const int DEF_POS_SELECT_LEVEL_BG_L_COLOR_FR = DEF_POS_DEFAULT_COLOR_3_R;
	public const int DEF_POS_SELECT_LEVEL_BG_L_COLOR_FG = DEF_POS_DEFAULT_COLOR_3_G;
	public const int DEF_POS_SELECT_LEVEL_BG_L_COLOR_FB = DEF_POS_DEFAULT_COLOR_3_B;
	public const int DEF_POS_SELECT_LEVEL_BG_L_COLOR_DR = (DEF_POS_SELECT_LEVEL_BG_L_COLOR_FR - DEF_POS_SELECT_LEVEL_BG_L_COLOR_SR)	/ DEF_POS_SELECT_LEVEL_BG_COLOR_NUM;
	public const int DEF_POS_SELECT_LEVEL_BG_L_COLOR_DG = (DEF_POS_SELECT_LEVEL_BG_L_COLOR_FG - DEF_POS_SELECT_LEVEL_BG_L_COLOR_SG)/ DEF_POS_SELECT_LEVEL_BG_COLOR_NUM;
	public const int DEF_POS_SELECT_LEVEL_BG_L_COLOR_DB = (DEF_POS_SELECT_LEVEL_BG_L_COLOR_FB - DEF_POS_SELECT_LEVEL_BG_L_COLOR_SB)/ DEF_POS_SELECT_LEVEL_BG_COLOR_NUM;
	public const int DEF_POS_SELECT_LEVEL_BG_R_COLOR_SR = DEF_POS_SELECT_LEVEL_BG_L_COLOR_FR;
	public const int DEF_POS_SELECT_LEVEL_BG_R_COLOR_SG = DEF_POS_SELECT_LEVEL_BG_L_COLOR_FG;
	public const int DEF_POS_SELECT_LEVEL_BG_R_COLOR_SB = DEF_POS_SELECT_LEVEL_BG_L_COLOR_FB;

	/** サウンド ON/OFF txt */
	public const int DEF_POS_TITLE_SOU_ON_OFF_X = 154;
	public const int DEF_POS_TITLE_SOU_ON_OFF_Y = 226;
	public const int DEF_POS_TITLE_SOU_ON_OFF_W = 76;
	public const int DEF_POS_TITLE_SOU_ON_OFF_H = 13;

	/** 背景 塗り */
	public const int DEF_POS_TITLE_BG_R = 0x00;
	public const int DEF_POS_TITLE_BG_G = 0x00;
	public const int DEF_POS_TITLE_BG_B = 0x00;

	/** タイトル */
	public const int DEF_POS_MENU_BG_TITLE_X = 4;
	public const int DEF_POS_MENU_BG_TITLE_Y = 3;
	public const int DEF_POS_MENU_BG_TITLE_W = 46;
	public const int DEF_POS_MENU_BG_TITLE_H = 13;

	/** 背景 上 塗り */
	public const int DEF_POS_MENU_UP_X = 0;
	public const int DEF_POS_MENU_UP_Y = 0;
	public const int DEF_POS_MENU_UP_W = 240;
	public const int DEF_POS_MENU_UP_H = 19;

	/** 背景 下 塗り */
	public const int DEF_POS_MENU_DOWN_X = 0;
	public const int DEF_POS_MENU_DOWN_Y = 221;
	public const int DEF_POS_MENU_DOWN_W = 240;
	public const int DEF_POS_MENU_DOWN_H = 19;

	/** 矢印 右 ① loop アニメ 1 */
	public const int DEF_POS_MENU_YAJI_RIG_X = 122;
	public const int DEF_POS_MENU_YAJI_RIG_Y = 77;
	public const int DEF_POS_MENU_YAJI_RIG_W = 10;
	public const int DEF_POS_MENU_YAJI_RIG_H = 11;
	public const int DEF_POS_MENU_YAJI_RIG_DX = 3;

	/** 矢印 上 ② loop アニメ 1 */
	public const int DEF_POS_MENU_YAJI_TOP_X = 1;
	public const int DEF_POS_MENU_YAJI_TOP_Y = 21;
	public const int DEF_POS_MENU_YAJI_TOP_W = 12;
	public const int DEF_POS_MENU_YAJI_TOP_H = 9;
	public const int DEF_POS_MENU_YAJI_TOP_DY = -2;

	/** 矢印 下 ① loop アニメ 1 */
	public const int DEF_POS_MENU_YAJI_BOT_X = 1;
	public const int DEF_POS_MENU_YAJI_BOT_Y = 211;
	public const int DEF_POS_MENU_YAJI_BOT_W = 12;
	public const int DEF_POS_MENU_YAJI_BOT_H = 8;
	public const int DEF_POS_MENU_YAJI_BOT_DY = 2;

	/** メニューフォーカス */
	public const int DEF_POS_MENU_FOCUS_FRAME_X = 124;
	public const int DEF_POS_MENU_FOCUS_FRAME_Y = 70;

	public const int DEF_POS_MENU_FOCUS_FRAME_2_X = 0;
	public const int DEF_POS_MENU_FOCUS_FRAME_2_Y = 70;
	public const int DEF_POS_MENU_FOCUS_FRAME_2_W = 124;
	public const int DEF_POS_MENU_FOCUS_FRAME_2_H = 2;
	public const int DEF_POS_MENU_FOCUS_FRAME_2_DY = 23;
	public const int DEF_POS_MENU_FOCUS_FRAME_COLOR_R = DEF_POS_DEFAULT_COLOR_4_R;
	public const int DEF_POS_MENU_FOCUS_FRAME_COLOR_G = DEF_POS_DEFAULT_COLOR_4_G;
	public const int DEF_POS_MENU_FOCUS_FRAME_COLOR_B = DEF_POS_DEFAULT_COLOR_4_B;

//	/** アイコン */
    // TODO C#移植　宣言重複分をコメントアウト
////	public const int DEF_POS_MENU_ICONE_GMODE_X = 6;
//    public const int DEF_POS_MENU_ICONE_GMODE_Y = 1;
//    // TODO エラーになるのでコメントアウト
////	#define DEF_//POS_MENU_ICONE_GMODE_W 	 19
//    public const int DEF_POS_MENU_ICONE_GMODE_H = 15;
////
////	public const int DEF_POS_MENU_ICONE_MEOSHI_X = 54;
//    public const int DEF_POS_MENU_ICONE_MEOSHI_Y = 1;
//    // TODO エラーになるのでコメントアウト
////	#define DEF_//POS_MENU_ICONE_MEOSHI_W 	 19
//    public const int DEF_POS_MENU_ICONE_MEOSHI_H = 15;
////
////	public const int DEF_POS_MENU_ICONE_SOUND_X = 30;
//    public const int DEF_POS_MENU_ICONE_SOUND_Y = 1;
//    // TODO エラーになるのでコメントアウト
////	#define DEF_//POS_MENU_ICONE_SOUND_W 	 19
//    public const int DEF_POS_MENU_ICONE_SOUND_H = 15;
////
////	public const int DEF_POS_MENU_ICONE_MUTE_X = 31;
//    public const int DEF_POS_MENU_ICONE_MUTE_Y = 1;
//    // TODO エラーになるのでコメントアウト
////	#define DEF_//POS_MENU_ICONE_MUTE_W 	 18 
//    public const int DEF_POS_MENU_ICONE_MUTE_H = 14;
	/** アイコン */
	public const int DEF_POS_MENU_ICONE_GMODE_X = 6;
	public const int DEF_POS_MENU_ICONE_GMODE_Y = 223;
	public const int DEF_POS_MENU_ICONE_GMODE_W = 19;
	public const int DEF_POS_MENU_ICONE_GMODE_H = 15;

	public const int DEF_POS_MENU_ICONE_MEOSHI_X = 54;
	public const int DEF_POS_MENU_ICONE_MEOSHI_Y = 223;
	public const int DEF_POS_MENU_ICONE_MEOSHI_W = 19;
	public const int DEF_POS_MENU_ICONE_MEOSHI_H = 15;

	public const int DEF_POS_MENU_ICONE_SOUND_X = 30;
	public const int DEF_POS_MENU_ICONE_SOUND_Y = 223;
	public const int DEF_POS_MENU_ICONE_SOUND_W = 19;
	public const int DEF_POS_MENU_ICONE_SOUND_H = 15;

	public const int DEF_POS_MENU_ICONE_MUTE_X = 31;
	public const int DEF_POS_MENU_ICONE_MUTE_Y = 224;
	public const int DEF_POS_MENU_ICONE_MUTE_W = 18;
	public const int DEF_POS_MENU_ICONE_MUTE_H = 14;

	/** サブメニュー */
	// 背景書き出し位置
	public const int DEF_POS_SUB_MENU_BG_X = 147;
	public const int DEF_POS_SUB_MENU_BG_Y = 70;
	public const int DEF_POS_SUB_MENU_BG_W = 93;
	public const int DEF_POS_SUB_MENU_BG_H = 28;
	public const int DEF_POS_SUB_MENU_BG_DY = 20;

	// 左上丸め
	public const int DEF_POS_SUB_MENU_LT_X = 147;
	public const int DEF_POS_SUB_MENU_LT_Y = 70;
	public const int DEF_POS_SUB_MENU_LT_W = 5;
	public const int DEF_POS_SUB_MENU_LT_H = 5;

	// 左下丸め
	public const int DEF_POS_SUB_MENU_BT_X = 147;
	public const int DEF_POS_SUB_MENU_BT_Y = 93;
	public const int DEF_POS_SUB_MENU_BT_W = 5;
	public const int DEF_POS_SUB_MENU_BT_H = 5;

	// 選択塗りつぶし
	public const int DEF_POS_SUB_MENU_TG_X = 147;
	public const int DEF_POS_SUB_MENU_TG_Y = 75;
	public const int DEF_POS_SUB_MENU_TG_W = 93;
	public const int DEF_POS_SUB_MENU_TG_H = 18;
	public const int DEF_POS_SUB_MENU_TG_DY = 20;

	// ON_OFF
	public const int DEF_POS_SUB_MENU_X = 161;
	public const int DEF_POS_SUB_MENU_Y = 77;

	// セル
	public const int DEF_POS_SUB_MENU_CELL_X = 186;
	public const int DEF_POS_SUB_MENU_CELL_Y = 76;
	public const int DEF_POS_SUB_MENU_CELL_W = 7;
	public const int DEF_POS_SUB_MENU_CELL_H = 16;
	public const int DEF_POS_SUB_MENU_CELL_DX = 10;

	// 押し順
	public const int DEF_POS_SUB_MENU_ORDER_LX = 161;
	public const int DEF_POS_SUB_MENU_ORDER_Y = 76;
	public const int DEF_POS_SUB_MENU_ORDER_CX = 186;
	public const int DEF_POS_SUB_MENU_ORDER_RX = 211;

	/** 告知 */
	public const int DEF_POS_KOKUCHI_X = 174;
	public const int DEF_POS_KOKUCHI_Y = (184 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_KOKUCHI_W = 57;
	public const int DEF_POS_KOKUCHI_H = 30;
	public const int DEF_POS_KOKUCHI_DY = 209 - DEF_POS_KOKUCHI_Y;

	// //////////////////////////////////////////////////////////////
	// スロット（リール）
	/** リール絵柄１つのサイズ */
	public const int DEF_POS_REEL_W = 40;
	public const int DEF_POS_REEL_H = 20;

	/** リール枠の左上の位置 */
	public const int DEF_POS_REEL_WINDOW_X = 49;
    public const int DEF_POS_REEL_WINDOW_Y = (107 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_REEL_WINDOW_W = 40;
	public const int DEF_POS_REEL_WINDOW_H = 60;

	/** 隣のリールまでの幅 */
	public const int DEF_POS_REEL_WINDOW_DX = 51;

	// ////////////////////////////////////////////////////////////////////
	// ゲームパネル関係の座標値

	/** データパネル左 */
	public const int DEF_POS_NAVI_L_X = 7;
	public const int DEF_POS_NAVI_L_Y = 2;
	public const int DEF_POS_NAVI_L_W = 44;
	public const int DEF_POS_NAVI_L_H = 50;

	/** TOTALの数字1桁目 */
	public const int DEF_POS_NAVI_TOTAL_X = 39;
	public const int DEF_POS_NAVI_TOTAL_Y = 14;
	public const int DEF_POS_NAVI_TOTAL_W = 7;
	public const int DEF_POS_NAVI_TOTAL_H = 9;
	public const int DEF_POS_NAVI_TOTAL_MAX = 99999;

	/** COUNTの数字1桁目	 */
	public const int DEF_POS_NAVI_COUNT_X = 39;
	public const int DEF_POS_NAVI_COUNT_Y = 40;
	public const int DEF_POS_NAVI_COUNT_W = 7;
	public const int DEF_POS_NAVI_COUNT_H = 9;
	public const int DEF_POS_NAVI_COUNT_MAX = 99999;

	/** データパネル右 */
	public const int DEF_POS_NAVI_R_X = 190;
	public const int DEF_POS_NAVI_R_Y = 2;
	public const int DEF_POS_NAVI_R_W = 44;
	public const int DEF_POS_NAVI_R_H = 50;

	/** BBの数字1桁目 */
	public const int DEF_POS_NAVI_BB_X = 201;
	public const int DEF_POS_NAVI_BB_Y = 14;
	public const int DEF_POS_NAVI_BB_W = 7;
	public const int DEF_POS_NAVI_BB_H = 9;
	public const int DEF_POS_NAVI_BB_MAX = 99;

	/** RBの数字1桁目 */
	public const int DEF_POS_NAVI_RB_X = 222;
	public const int DEF_POS_NAVI_RB_Y = 14;
	public const int DEF_POS_NAVI_RB_W = 7;
	public const int DEF_POS_NAVI_RB_H = 9;
	public const int DEF_POS_NAVI_RB_MAX = 99;

	/** COINの数字1桁目 */
	public const int DEF_POS_NAVI_COIN_X = 222;
	public const int DEF_POS_NAVI_COIN_Y = 40;
	public const int DEF_POS_NAVI_COIN_W = 7;
	public const int DEF_POS_NAVI_COIN_H = 9;
	public const int DEF_POS_NAVI_COIN_MAX = 99999;

	/** データパネル中 */
	public const int DEF_POS_NAVI_C_X = 55;
	public const int DEF_POS_NAVI_C_Y = 2;
	public const int DEF_POS_NAVI_C_W = 131;
	public const int DEF_POS_NAVI_C_H = 50;

	/** データパネル下 */
	public const int DEF_POS_NAVI_B_X = 7;
	public const int DEF_POS_NAVI_B_Y = 54;
	public const int DEF_POS_NAVI_B_W = 227;
	public const int DEF_POS_NAVI_B_H = 24;

	/** BB_AVGの数字1桁目 */
	public const int DEF_POS_NAVI_BBAVG_X = 69;
	public const int DEF_POS_NAVI_BBAVG_Y = 66;
	public const int DEF_POS_NAVI_BBAVG_W = 7;
	public const int DEF_POS_NAVI_BBAVG_H = 9;
	public const int DEF_POS_NAVI_BBAVG_MAX = 99999;

	/** RB_AVGの数字1桁目 */
	public const int DEF_POS_NAVI_RBAVG_X = 146;
	public const int DEF_POS_NAVI_RBAVG_Y = 66;
	public const int DEF_POS_NAVI_RBAVG_W = 7;
	public const int DEF_POS_NAVI_RBAVG_H = 9;
	public const int DEF_POS_NAVI_RBAVG_MAX = 99999;

	/** MAX COINの数字1桁目 */
	public const int DEF_POS_NAVI_MAX_X = 222;
	public const int DEF_POS_NAVI_MAX_Y = 66;
	public const int DEF_POS_NAVI_MAX_W = 7;
	public const int DEF_POS_NAVI_MAX_H = 9;
	public const int DEF_POS_NAVI_MAX_MAX = 999999999;

	/** メニューから表示する時下げる */
	public const int DEF_POS_NAVI_DY = 2;

	/** ﾌﾟﾚｲ数ﾏｽ左上 縦8ﾏｽ横10ﾏｽ next縦4ﾄﾞｯﾄ横12ﾄﾞｯﾄ */
	public const int DEF_POS_CELL_X = 61;
	public const int DEF_POS_CELL_Y = 6;
	public const int DEF_POS_CELL_W = 11;
	public const int DEF_POS_CELL_H = 3;

	/** ﾏｽの色:BB */
	public const int DEF_POS_CELL_COLOR_BB_R = 0xFF;
	public const int DEF_POS_CELL_COLOR_BB_G = 0x00;
	public const int DEF_POS_CELL_COLOR_BB_B = 0x54;
	public const int DEF_POS_CELL_BB_X = 97;
	public const int DEF_POS_CELL_BB_Y = 46;
	public const int DEF_POS_CELL_BB_W = 11;
	public const int DEF_POS_CELL_BB_H = 3;
	
	/** ﾏｽの色:RB */
	public const int DEF_POS_CELL_COLOR_RB_R = 0x58;
	public const int DEF_POS_CELL_COLOR_RB_G = 0x7B;
	public const int DEF_POS_CELL_COLOR_RB_B = 0xFF;
	public const int DEF_POS_CELL_RB_X = 124;
	public const int DEF_POS_CELL_RB_Y = 46;
	public const int DEF_POS_CELL_RB_W = 11;
	public const int DEF_POS_CELL_RB_H = 3;

	/** ﾏｽの色:経過 */
	public const int DEF_POS_CELL_COLOR_ETC_R = 0xAA;
	public const int DEF_POS_CELL_COLOR_ETC_G = 0xC9;
	public const int DEF_POS_CELL_COLOR_ETC_B = 0xE4;
	public const int DEF_POS_CELL_ETC_X = 151;
	public const int DEF_POS_CELL_ETC_Y = 46;
	public const int DEF_POS_CELL_ETC_W = 11;
	public const int DEF_POS_CELL_ETC_H = 3;

	/** ボーナスランプ */
	public const int DEF_POS_A1_X = 0;
    public const int DEF_POS_A1_Y = (0 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_A1_W = 79;
	public const int DEF_POS_A1_H = 12;
	public const int DEF_POS_A2_X = 79;
    public const int DEF_POS_A2_Y = (0 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_A2_W = 27;
	public const int DEF_POS_A2_H = 7;
	public const int DEF_POS_A3_X = 106;
    public const int DEF_POS_A3_Y = (0 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_A3_W = 28;
	public const int DEF_POS_A3_H = 7;
	public const int DEF_POS_A4_X = 134;
    public const int DEF_POS_A4_Y = (0 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_A4_W = 27;
	public const int DEF_POS_A4_H = 7;
	public const int DEF_POS_A5_X = 161;
    public const int DEF_POS_A5_Y = (0 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_A5_W = 79;
	public const int DEF_POS_A5_H = 12;
	
	/** BETランプ */
	public const int DEF_POS_B1_X = 0;
    public const int DEF_POS_B1_Y = (106 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_B1_W = 15;
	public const int DEF_POS_B1_H = 22;
	public const int DEF_POS_B2_X = 0;
    public const int DEF_POS_B2_Y = (128 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_B2_W = 15;
	public const int DEF_POS_B2_H = 22;
	public const int DEF_POS_B3_X = 0;
    public const int DEF_POS_B3_Y = (150 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_B3_W = 15;
	public const int DEF_POS_B3_H = 23;
	public const int DEF_POS_B4_X = 0;
    public const int DEF_POS_B4_Y = (173 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_B4_W = 15;
	public const int DEF_POS_B4_H = 22;
	public const int DEF_POS_B5_X = 0;
    public const int DEF_POS_B5_Y = (195 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_B5_W = 14;
	public const int DEF_POS_B5_H = 22;

	/** 状態ランプ */
	public const int DEF_POS_C1_X = 227;
    public const int DEF_POS_C1_Y = (94 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_C1_W = 13;
	public const int DEF_POS_C1_H = 28;
	public const int DEF_POS_C2_X = 227;
    public const int DEF_POS_C2_Y = (122 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_C2_W = 13;
	public const int DEF_POS_C2_H = 28;
	public const int DEF_POS_C3_X = 226;
    public const int DEF_POS_C3_Y = (151 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_C3_W = 14;
	public const int DEF_POS_C3_H = 21;
	public const int DEF_POS_C4_X = 226;
    public const int DEF_POS_C4_Y = (172 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_C4_W = 14;
	public const int DEF_POS_C4_H = 23;
	public const int DEF_POS_C5_X = 226;
    public const int DEF_POS_C5_Y = (195 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_C5_W = 14;
	public const int DEF_POS_C5_H = 23;

	/** 7SEG */
	public const int DEF_POS_CREDIT_X = 68;
    public const int DEF_POS_CREDIT_Y = (224 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_CREDIT_W = 9;
	public const int DEF_POS_CREDIT_H = 14;
	public const int DEF_POS_CREDIT_D = 2;
	public const int DEF_POS_BONUS_X = 140;
    public const int DEF_POS_BONUS_Y = (224 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_BONUS_W = 9;
	public const int DEF_POS_BONUS_H = 14;
	public const int DEF_POS_BONUS_D = 3;
	public const int DEF_POS_PAY_X = 174;
    public const int DEF_POS_PAY_Y = (224 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_PAY_W = 9;
	public const int DEF_POS_PAY_H = 14;
	public const int DEF_POS_PAY_D = 2;
	public const int DEF_POS_CHANCE_X = 89;
    public const int DEF_POS_CHANCE_Y = (225 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_CHANCE_W = 21;
	public const int DEF_POS_CHANCE_H = 10;
	
	/** MAX_BET */
	public const int DEF_POS_BET_X = 54;
    public const int DEF_POS_BET_Y = (193 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_BET_W = 23;
	public const int DEF_POS_BET_H = 20;
	
	/** レバー */
	public const int DEF_POS_REV_X = 28;
    public const int DEF_POS_REV_Y = (213 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_REV_W = 54;
	public const int DEF_POS_REV_H = 27;
	
	/** ストップランプ */
	public const int DEF_POS_E1_X = 84;
    public const int DEF_POS_E1_Y = (222 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_E1_W = 16;
	public const int DEF_POS_E1_H = 16;
	public const int DEF_POS_E2_X = 112;
    public const int DEF_POS_E2_Y = (222 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_E2_W = 16;
	public const int DEF_POS_E2_H = 16;
	public const int DEF_POS_E3_X = 140;
    public const int DEF_POS_E3_Y = (222 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_E3_W = 16;
	public const int DEF_POS_E3_H = 16;
	
	/** 筐体 */
	public const int DEF_POS_K0_X = 0;
    public const int DEF_POS_K0_Y = (0 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_K0_W = 240;
	public const int DEF_POS_K0_H = 15;
	public const int DEF_POS_K1_X = 0;
    public const int DEF_POS_K1_Y = (15 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_K1_W = 240;
	public const int DEF_POS_K1_H = 73;
	public const int DEF_POS_K2_X = 25;
    public const int DEF_POS_K2_Y = (88 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_K2_W = 190;
	public const int DEF_POS_K2_H = 26;
	public const int DEF_POS_K3_X = 0;
    public const int DEF_POS_K3_Y = (88 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_K3_W = 25;
	public const int DEF_POS_K3_H = 152;
	public const int DEF_POS_K4_X = 215;
    public const int DEF_POS_K4_Y = (88 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_K4_W = 25;
	public const int DEF_POS_K4_H = 152;
	public const int DEF_POS_K5_X = 81;
    public const int DEF_POS_K5_Y = (114 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_K5_W = 11;
	public const int DEF_POS_K5_H = 96;
	public const int DEF_POS_K6_X = 148;
    public const int DEF_POS_K6_Y = (114 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_K6_W = 11;
	public const int DEF_POS_K6_H = 96;
	public const int DEF_POS_K7_X = 25;
    public const int DEF_POS_K7_Y = (210 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_K7_W = 190;
	public const int DEF_POS_K7_H = 30;
	
	/** サイドランプ */
	public const int DEF_POS_S1_X = 0;
    public const int DEF_POS_S1_Y = (13 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_S1_W = 7;
	public const int DEF_POS_S1_H = 61;
	public const int DEF_POS_S2_X = 6;
    public const int DEF_POS_S2_Y = (12 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_S2_W = 22;
	public const int DEF_POS_S2_H = 62;
	public const int DEF_POS_S3_X = 27;
    public const int DEF_POS_S3_Y = (11 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_S3_W = 33;
	public const int DEF_POS_S3_H = 65;
	public const int DEF_POS_S4_X = 180;
    public const int DEF_POS_S4_Y = (11 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_S4_W = 33;
	public const int DEF_POS_S4_H = 65;
	public const int DEF_POS_S5_X = 212;
    public const int DEF_POS_S5_Y = (12 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_S5_W = 22;
	public const int DEF_POS_S5_H = 51;
	public const int DEF_POS_S6_X = 233;
    public const int DEF_POS_S6_Y = (13 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_S6_W = 7;
	public const int DEF_POS_S6_H = 61;
	
//	/** 4thリール */
//	public const int DEF_POS_4TH_X = 63;
    public const int DEF_POS_4TH_Y = (26 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_4TH_W = 113;
	public const int DEF_POS_4TH_H = 44;
//	public const int DEF_POS_4TH_1_W = 117;
	public const int DEF_POS_4TH_2_W = 63;
	public const int DEF_POS_4TH_3_W = 59;
	public const int DEF_POS_4TH_4_W = 55;
	public const int DEF_POS_4TH_5_W = 64;
	public const int DEF_POS_4TH_6_W = 56;
	public const int DEF_POS_4TH_TOTAL_W = 414;
	public const int DEF_POS_4TH_STOP_N = 21;
//	
//	public const int DEF_POS_4TH_REEL_1_W = 117;
	public const int DEF_POS_4TH_REEL_2_W = 63;
	public const int DEF_POS_4TH_REEL_3_W = 59;
	public const int DEF_POS_4TH_REEL_4_W = 55;
	public const int DEF_POS_4TH_REEL_5_W = 64;
	public const int DEF_POS_4TH_REEL_6_W = 56;
	
	public const int DEF_POS_KOKU_B_X = 123;
    public const int DEF_POS_KOKU_B_Y = (78 + Defines.GP_DRAW_OFFSET_Y);
	public const int DEF_POS_KOKU_K_X = 181;
    public const int DEF_POS_KOKU_K_Y = (78 + Defines.GP_DRAW_OFFSET_Y);
	

	/** パネル　AVG (ゲームデータ用)navi_bot*/
	public const int DEF_POS_NAVI_BOT_X = 7;
	public const int DEF_POS_NAVI_BOT_Y = 54;
	public const int DEF_POS_NAVI_BOT_W = 227;
	public const int DEF_POS_NAVI_BOT_H = 24;

	public const int DEF_RECT_MENU_BG01_R = 29;
	public const int DEF_RECT_MENU_BG01_G = 29;
	public const int DEF_RECT_MENU_BG01_B = 87;
	public const int DEF_MENU_ITEM_POS_START_X = 10;
	public const int DEF_MENU_ITEM_POS_START_Y = 20;
	public const int DEF_SIM_ITEM_LINE_SPACE = 18;

	//メーター枠背景色外
	public const int DEF_WAKU_HAIKEI_SOTO_R = 0xff ;
	public const int DEF_WAKU_HAIKEI_SOTO_G = 0x00;
	public const int DEF_WAKU_HAIKEI_SOTO_B = 0x00;
	//メーター枠背景色中
	public const int DEF_WAKU_HAIKEI_NAKA_R = 0x00 ;
	public const int DEF_WAKU_HAIKEI_NAKA_G = 0x00;
	public const int DEF_WAKU_HAIKEI_NAKA_B = 0x00;
	//パラメータ色
	public const int DEF_PARAMERTER_R = 0xff;
	public const int DEF_PARAMERTER_G = 0xff;
	public const int DEF_PARAMERTER_B = 0x00;
	//パラメータ下地
	public const int DEF_PARAMERTER_SITA_R = 0x4d;
	public const int DEF_PARAMERTER_SITA_G = 0x4d;
	public const int DEF_PARAMERTER_SITA_B = 0x4d;
	//ﾒﾆｭｰ選択
	public const int DEF_MENU_SELECT_R = 0xff;
	public const int DEF_MENU_SELECT_G = 0xff;
	public const int DEF_MENU_SELECT_B = 0xff;
	public const int DEF_MENU_SELECT2_R = 0xff;
	public const int DEF_MENU_SELECT2_G = 0xff;
	public const int DEF_MENU_SELECT2_B = 0xff;
	//ﾒﾆｭｰ非選択
	public const int DEF_MENU_UNSELECT_R = 0x80;
	public const int DEF_MENU_UNSELECT_G = 0x80;
	public const int DEF_MENU_UNSELECT_B = 0x80;
	//ﾒﾆｭｰ背景色
	public const int DEF_MENU_BG_R = 0x04;
	public const int DEF_MENU_BG_G = 0x08;
	public const int DEF_MENU_BG_B = 0x20;
	//タイトルテキスト
	public const int DEF_TITLE_TXT_R = 0xd8;
	public const int DEF_TITLE_TXT_G = 0xb3;
	public const int DEF_TITLE_TXT_B = 0x5a;
	//テキスト
	public const int DEF_TXT_R = 0xff;
	public const int DEF_TXT_G = 0xff;
	public const int DEF_TXT_B = 0xff;
	//非選択テキスト
	public const int DEF_UNSELECT_TXT_R = 0x80;
	public const int DEF_UNSELECT_TXT_G = 0x80;
	public const int DEF_UNSELECT_TXT_B = 0x80;
	//選択背景色
	public const int DEF_SELECT_BG_R = 0x07;
	public const int DEF_SELECT_BG_G = 0x64;
	public const int DEF_SELECT_BG_B = 0xaa;
}

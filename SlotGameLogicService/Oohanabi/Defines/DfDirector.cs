public partial class Defines {

	// ----------------------------------------------------------------------------------------------
	// 回転表示器回転要求ﾃﾞｰﾀﾃｰﾌﾞﾙ
	// R4DAT.TRG:
	// A*128+B*64+C,D*16+E*8+F*4+G 回転開始ﾀｲﾐﾝｸﾞﾋﾞｯﾄ
	// A 第一停止時回転開始
	// B 第二停止時回転開始
	// C 第三停止時回転開始
	// D 最終停止後回転開始
	// E 第三停止時回転停止
	// F 最終停止後回転停止
	// G 回転開始有効ﾌﾗｸﾞ
	// ( 0 	 無し, 1 	 有り )
	// R4DAT.CMD:
	// A*64+B*32+C 回転ｽﾃｰﾀｽﾃﾞｰﾀ ＆ 停止位置
	// A 回転方向制御ﾌﾗｸﾞ
	// ( 0 	 正回転要求, 1 	 逆回転要求 )
	// B 回転速度制御ﾌﾗｸﾞ
	// ( 0 	 通常回転速度, 1 	 低速回転速度 )
	// C 停止ﾎﾟｼﾞｼｮﾝﾃﾞｰﾀ
	// ( 0~20 	 停止ﾎﾟｼﾞｼｮﾝﾃﾞｰﾀ )
	// ----------------------------------------------------------------------------------------------

	// TRG

	 public static readonly int DEF_R_STS = 0x03; // 回転許可ﾀｲﾐﾝｸﾞ

	// ｽﾀｰﾄ時及び連続回転有効(bit7～bit4が0)

	 public static readonly int DEF_R_ST1 = 0x02; // 第一停止後有効

	 public static readonly int DEF_R_ST2 = 0x01; // 第二停止後有効

	 public static readonly int DEF_R_ST3 = 0x00; // 第三停止後有効

	 public static readonly int DEF_R_STE = 0x00; // 最終停止後有

	 public static readonly int DEF_ST_TN = 0x00; // 回転停止要求ﾀｲﾐﾝｸﾞ 即停止(bit3～bit2が0)

	 public static readonly int DEF_ST_T3 = 0x10; // 第三停止時

	 public static readonly int DEF_ST_TE = 0x20; // 最終停止後

	// CMD
	 public static readonly int DEF_R_NRL = 0x00; // 正回転（左方向）bit2=0

	 public static readonly int DEF_R_RVS = 0x04; // 逆回転（右方向）bit2=1

	 public static readonly int DEF_R_NRS = 0x01; // 通常回転 (60/min)

	 public static readonly int DEF_R_SLW = 0x02; // 低速回転 (30/min)

     public static readonly int DEF_RP01 = 237;// :青ドン:

	 public static readonly int DEF_RP04 = 182;// :一尺玉:

	 public static readonly int DEF_RP05 = 105+57;// :大当たり手前０:

	 public static readonly int DEF_RP06 = 105+38;// :大当たり手前１:

	 public static readonly int DEF_RP07 = 105+19;// :大当たり手前２:

	 public static readonly int DEF_RP08 = 105;// :大当たり:

	 public static readonly int DEF_RP13 = 15;// :赤ドン:

	 public static readonly int DEF_RP16 = 362;// :三尺玉:

	 public static readonly int DEF_RP19 = 300;// :はずれ:
		// ----------------------------------------------------------------------------------------------
		// ﾘｰﾙﾗﾝﾌﾟﾃﾞﾓ点滅ﾊﾟﾀｰﾝﾃｰﾌﾞﾙ
		// 101112
		// 一二三
		// 上 １２３
		// 中 ４５６
		// 下 ７８９
		// 一二三
		// 上 ３６９
		// 中 ２５８ ←書く順番があるので変更
		// 下 １４７
		// ----------------------------------------------------------------------------------------------
		 public static readonly int DEF_FEND = 0xFFFF; // ｴﾝﾄﾞ ｺｰﾄﾞ

		 public static readonly int DEF_FNON = 0; // 真っ暗

		 public static readonly int DEF_F1 = 1 << 2; // ﾘｰﾙ ﾗﾝﾌﾟ1 ﾋﾞｯﾄ

		 public static readonly int DEF_F2 = 1 << 5; // ﾘｰﾙ ﾗﾝﾌﾟ2 ﾋﾞｯﾄ

		 public static readonly int DEF_F3 = 1 << 8; // ﾘｰﾙ ﾗﾝﾌﾟ3 ﾋﾞｯﾄ

		 public static readonly int DEF_F4 = 1 << 1; // ﾘｰﾙ ﾗﾝﾌﾟ4 ﾋﾞｯﾄ

		 public static readonly int DEF_F5 = 1 << 4; // ﾘｰﾙ ﾗﾝﾌﾟ5 ﾋﾞｯﾄ

		 public static readonly int DEF_F6 = 1 << 7; // ﾘｰﾙ ﾗﾝﾌﾟ6 ﾋﾞｯﾄ

		 public static readonly int DEF_F7 = 1 << 0; // ﾘｰﾙ ﾗﾝﾌﾟ7 ﾋﾞｯﾄ

		 public static readonly int DEF_F8 = 1 << 3; // ﾘｰﾙ ﾗﾝﾌﾟ8 ﾋﾞｯﾄ

		 public static readonly int DEF_F9 = 1 << 6; // ﾘｰﾙ ﾗﾝﾌﾟ9 ﾋﾞｯﾄ

		 public static readonly int DEF_F10 = 1 << 9; // ﾘｰﾙ ﾗﾝﾌﾟ10 ﾋﾞｯﾄ

		 public static readonly int DEF_F11 = 1 << 10; // ﾘｰﾙ ﾗﾝﾌﾟ11 ﾋﾞｯﾄ

		 public static readonly int DEF_F12 = 1 << 11; // ﾘｰﾙ ﾗﾝﾌﾟ12 ﾋﾞｯﾄ

}

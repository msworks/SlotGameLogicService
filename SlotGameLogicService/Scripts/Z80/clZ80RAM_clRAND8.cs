﻿using System.Collections;

///////////////////////////////////////////////
// 擬似乱数クラス
///////////////////////////////////////////////
public class clRAND8 {

    static ushort mBak;
    public static ushort mRndwl;
    public static ushort mRndwh;
    public static ushort[] mRndbuf = new ushort[256];
    static ushort mRandomA;


	public static void mInitializaion(int count)
	{
        Defines.TRACE("★mInitializaion:seed=" + count);

        ushort[] initdat =
		{
			0x82,0x7B,0x24,0xCA,0x0D,0x7A,0x0F,0x0C,0x48,0xCC,0x0B,0x29,0x42,0xC3,0xC4,0x3F,
			0xA8,0x33,0x47,0x89,0x4C,0xFC,0x4D,0x00,0xE6,0xFA,0x04,0xB7,0x7C,0xF8,0xEA,0x4F,
			0xDA,0x87,0x49,0x20,0x2C,0xB2,0xB3,0x22,0xAC,0xE9,0xB4,0x9C,0xDF,0xB8,0xCD,0x77,
			0xA6,0x16,0x17,0x68,0x15,0xBA,0x1D,0x19,0xC9,0x6D,0x0E,0xAD,0x2F,0x06,0x12,0x1F,
			0x14,0xF0,0x72,0x25,0x05,0xD6,0x8D,0x80,0x2A,0x78,0x28,0xDD,0xBF,0xD9,0x88,0x8F,
			0x60,0x51,0x59,0xE7,0x55,0xEE,0x52,0x26,0x18,0xED,0x11,0xC1,0xFF,0x43,0x21,0x5F,
			0xD4,0xF6,0xBC,0xF3,0x70,0xDE,0xAE,0xA9,0x3A,0x36,0xDB,0xD7,0x9F,0x6C,0xD1,0x3E,
			0x71,0xA7,0x08,0xC6,0x97,0xBB,0xCE,0xC8,0x56,0xE3,0x01,0x0A,0x3C,0x92,0x41,0xFE,
			0x64,0x6A,0x40,0xFB,0xF4,0x5D,0x9A,0xF1,0xBE,0x83,0xC0,0xF2,0xD0,0x4B,0x3D,0xFD,
			0x90,0x34,0x8A,0x84,0x7E,0xCF,0x75,0x95,0x45,0x7D,0x03,0x09,0x07,0xF9,0x4E,0x79,
			0x23,0xA4,0xD8,0x37,0xDC,0x93,0xF7,0xD5,0x50,0x81,0xE4,0xD3,0xB1,0x8C,0x2E,0x46,
			0x61,0x5B,0x57,0x31,0xEC,0x5C,0xA2,0xE2,0x62,0xE8,0x13,0xEB,0x66,0x94,0x1E,0xEF,
			0x74,0xAB,0xE5,0x02,0xE0,0x2D,0xBD,0xB5,0x85,0xB6,0xD2,0xB0,0x2B,0xE1,0x8E,0xB9,
			0xA1,0x1B,0x1A,0x67,0x65,0x1C,0x6E,0x69,0x58,0xC2,0x53,0x6B,0x8B,0x86,0x5E,0x6F,
			0x5A,0x76,0x54,0xA0,0x9E,0x39,0x98,0xA5,0x4A,0x91,0x27,0xA3,0xAA,0xF5,0xAF,0x99,
			0xC7,0x10,0x96,0x32,0x38,0x9D,0x73,0x9B,0xC5,0x30,0xCB,0x44,0x3B,0x63,0x7F,0x35,
		};
		
		int	i;

		mBak = 0xFFFF;
		mRndwl = 0;
		mRndwh = 31;

        // TODO soy この行があると乱数が固定になる
//count = 30568;

		for(i=0;i<256;i++) mRndbuf[i] = initdat[i];
		for(i=0;i<count;i++) {
			mGetRnd8();
		}
	}

	public static int mGetRnd8()
	{
        ushort l;

		if(++mRndwl>=55) mRndwl = 0;
        l = (ushort)(mRndbuf[mRndwl] & 0x00FF);

		if(++mRndwh>=55) mRndwh = 0;
		mRndbuf[mRndwh] ^= l;

		return (mRndbuf[mRndwh] & 0x00FF);
	}
};
///////////////////////////////////////////////
// Z80用レジスタークラス
///////////////////////////////////////////////
public class clREG {
      // TODO C#移植 とりあえずintとしたもののビットシフト演算で使われる可能性を考えるとushortにした方が良さそう
      public ushort AF;
      public ushort BC;
      public ushort DE;
      public ushort HL;
      public ushort IX;
      public ushort IY;
};
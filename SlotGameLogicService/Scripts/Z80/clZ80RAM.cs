//*******************************************************
//	【C++言語ソース　→　JAVAソース出力】
//		このファイルはZ80C2J.exeで出力されています。
//*******************************************************


//#include "DfMain.h"

using System;
public class clZ80RAM {

	//Z80ワークRAMの実態
    public static readonly ushort[] mWorkRam = new ushort[Defines.DEF_WORKEND + 1];

	public static clREG front;		//表レジスタ
	public static clREG back;		//裏レジスタ

    public static clRAND8 random;	//乱数クラス

	//初期化
	static clZ80RAM(){

	    front = new clREG();
	    back = new clREG();
		//表ﾚｼﾞｽﾀのみ0クリア。
		//（本当はやらなくてもいいはずだが念のため）
		front.AF = back.AF = 0;
		front.BC = back.BC = 0;
		front.DE = back.DE = 0;
		front.HL = back.HL = 0;
		front.IX = back.IX = 0;
		front.IY = back.IY = 0;
		//ワークRAMのクリア
		for(int i=0; i < Defines.DEF_WORKEND; i++ )
			mWorkRam[i] = 0;
	}

	//ワークRAM 取得(仮想1バイト)
    public static ushort getWork(int index) { return mWorkRam[index]; }
	//ワークRAM 設定(仮想1バイト)
    public static void setWork(int index, ushort data) { Defines.RAM_TRACE("setWork:[" + index + "]" + (data & 0xFFFF)); mWorkRam[index] = data; }

	//ワークRAM 取得(仮想2バイト)
    public static ushort getWork16(int index) { return (ushort)((mWorkRam[index] | (mWorkRam[index + 1] << 8))); }
	//ワークRAM 設定(仮想2バイト)
    public static void setWork16(int index, ushort data) { mWorkRam[index] = (ushort)(data & 0x00FF); mWorkRam[index + 1] = (ushort)((data & 0xFF00) >> 8); }

    public static void clearWork(int topIndex)
	{
        int n = topIndex;
		while(n < (Defines.DEF_WORKEND) ) mWorkRam[n++] = 0;	
	}


	/*
		Z80 レジスターアクセス関数
	*/
    public static ushort getAF() { return front.AF; }
    public static ushort getBC() { return front.BC; }
    public static ushort getDE() { return front.DE; }
    public static ushort getHL() { return front.HL; }
    public static ushort getIX() { return front.IX; }
    public static ushort getIY() { return front.IY; }

	//ワークRAM内取得
	public static ushort getBCm(){ return mWorkRam[getBC()];}
	public static ushort getDEm(){ return mWorkRam[getDE()];}
	public static ushort getHLm(){ return mWorkRam[getHL()];}
	public static ushort getIXm(){ return mWorkRam[getIX()];}
	public static ushort getIYm(){ return mWorkRam[getIY()];}

	//静的データ内取得
	public static ushort getBCt(){ return mDataTable[getBC()];}
	public static ushort getDEt(){ return mDataTable[getDE()];}
    public static ushort getHLt(){ return mDataTable[getHL()];}
	public static ushort getIXt(){ return mDataTable[getIX()];}
	public static ushort getIYt(){ return mDataTable[getIY()];}

	
	//1バイト取得。
    public static ushort getA() { return (ushort)(front.AF & 0x00FF); }
    public static ushort getB() { return (ushort)((front.BC & 0xFF00) >> 8); }
    public static ushort getC() { return (ushort)(front.BC & 0x00FF); }
    public static ushort getD() { return (ushort)((front.DE & 0xFF00) >> 8); }
    public static ushort getE() { return (ushort)(front.DE & 0x00FF); }
    public static ushort getH() { return (ushort)((front.HL & 0xFF00) >> 8); }
    public static ushort getL() { return (ushort)(front.HL & 0x00FF); }

	//2バイト設定
	public static void setAF(int data){ front.AF = (ushort)data; }
    public static void setBC(int data){ front.BC = (ushort)data; }
    public static void setDE(int data){ front.DE = (ushort)data; }
    public static void setHL(int data){ front.HL = (ushort)data; }
    public static void setIX(int data){ front.IX = (ushort)data; }
    public static void setIY(int data){ front.IY = (ushort)data; }

    //ワークRAM内設定（1バイト）
    public static void setBCm(int data){ mWorkRam[getBC()] = (ushort)data; }
    public static void setDEm(int data){ mWorkRam[getDE()] = (ushort)data; }
    public static void setHLm(int data){ mWorkRam[getHL()] = (ushort)data; }
    public static void setIXm(int data){ mWorkRam[getIX()] = (ushort)data; }
    public static void setIYm(int data){ mWorkRam[getIY()] = (ushort)data; }

	//ワークRAM内設定（2バイト）
	public static void mLD_Nm_A(int index){mWorkRam[index] = getA();}
//	public static void mLD_Nm_DE(int index){mWorkRam[index] = getD();mWorkRam[index+1] = getE();}
//	public static void mLD_Nm_HL(int index){mWorkRam[index] = getH();mWorkRam[index+1] = getL();}

	public static void mLD_Nm_DE(int index){mWorkRam[index] = getE();mWorkRam[index+1] = getD();}
	public static void mLD_Nm_HL(int index){mWorkRam[index] = getL();mWorkRam[index+1] = getH();}

	//1バイト設定
    public static void setA(int data)
	{
		front.AF &= 0xFF00;
        front.AF |= (ushort)((((ushort)data) & 0x00FF));
	}
	public static void setB(int data)
	{
		front.BC &= 0x00FF;
        front.BC |= (ushort)((((ushort)data) & 0x00FF) << 8);
	}
	public static void setC(int data)
	{
		front.BC &= 0xFF00;
        front.BC |= (ushort)(((ushort)data) & 0x00FF);
	}
	public static void setD(int data)
	{
		front.DE &= 0x00FF;
        front.DE |= (ushort)((((ushort)data) & 0x00FF) << 8);
	}
	public static void setE(int data)
	{
		front.DE &= 0xFF00;
        front.DE |= (ushort)(((ushort)data) & 0x00FF);
	}
	
	public static void setH(int data)
	{
		front.HL &= 0x00FF;
        front.HL |= (ushort)((((ushort)data) & 0x00FF) << 8);
	}
	public static void setL(int data)
	{
		front.HL &= 0xFF00;
        front.HL |= (ushort)(((ushort)data) & 0x00FF);
	}

	/*	Z80のEX AF,AF' 命令と同じ動作。
		AFの内容をAF'と交換
	*/
	public static void mEX_AF(){
        ushort tmp;		
		//アホな入れ替え処理
		tmp = front.AF;
		front.AF = back.AF;
		back.AF = tmp;
	}
	
	/*	Z80のEX DE,HL 命令と同じ動作。
		DEの内容をHLと交換
	*/
	public static void mEX_DE_HL(){
        ushort tmp;		
		//アホな入れ替え処理
		tmp = front.DE;
		front.DE = front.HL;
		front.HL = tmp;
	}

	//＠＠＠ppincでおかしくなる為
	//	Z80のEXX命令と同じ動作。
	//	BC/DE/HLの内容をBC'/DE'/HL'と交換
	//
	public static void mEXX(){
        ushort[] tmp = new ushort[3];
		//アホな入れ替え処理
		tmp[0] = front.BC;
		tmp[1] = front.DE;
		tmp[2] = front.HL;
		front.BC = back.BC;
		front.DE = back.DE;
		front.HL = back.HL;
		back.BC = tmp[0];
		back.DE = tmp[1];
		back.HL = tmp[2];
	}

//	public static int getLD_Am(int index){
    public static ushort mLD_A_Nm(int index) {
		setA(mWorkRam[index]);
		return mWorkRam[index];
	}

    public static ushort mLD_BC_Nm(int index) {
		setBC((mWorkRam[index] | (mWorkRam[index + 1]<<8)));
		return mWorkRam[index];
	}

    public static ushort mLD_DE_Nm(int index) {
		setDE((mWorkRam[index] | (mWorkRam[index + 1]<<8)));
		return mWorkRam[index];
	}

    public static ushort mLD_HL_Nm(int index) {
		setHL((mWorkRam[index] | (mWorkRam[index + 1]<<8)));
		return mWorkRam[index];
	}

    public static ushort mLD_BC_Nt(int index) {
		setBC((mDataTable[index] | (mDataTable[index + 1]<<8)));
		return mDataTable[index];
	}

    public static ushort mLD_DE_Nt(int index) {
		setDE((mDataTable[index] | (mDataTable[index + 1]<<8)));
		return mDataTable[index];
	}

    //	public static ushort getLD_HLm(int index){
    public static ushort mLD_HL_Nt(int index) {
		setHL((mWorkRam[index] | (mWorkRam[index + 1]<<8)));
		return mDataTable[index];
	}

    public static void mINC_BC(int i) { setBC((ushort)(getBC() + i)); }
    public static void mINC_DE(int i) { setDE((ushort)(getDE() + i)); }
    public static void mINC_HL(int i) { setHL((ushort)(getHL() + i)); }
    public static void mINC_IX(int i) { setIX((ushort)(getIX() + i)); }
    public static void mINC_IY(int i) { setIY((ushort)(getIY() + i)); }

    public static void mDEC_BC(int i) { setBC((ushort)(getBC() - i)); }
    public static void mDEC_DE(int i) { setDE((ushort)(getDE() - i)); }
    public static void mDEC_HL(int i) { setHL((ushort)(getHL() - i)); }
    public static void mDEC_IX(int i) { setIX((ushort)(getIX() - i)); }
    public static void mDEC_IY(int i) { setIY((ushort)(getIY() - i)); }

	public static bool mDJNZ()
	{
		bool ret = false;
        ushort tmpB = getB();
		if( --tmpB == 0 ) 
		{
			tmpB = 0;
			ret = true;
		}
		setB(tmpB);
		return ret;
	}

	public static void mLDI()
	{
		setDEm(getHLm());
		mINC_HL(1);
		mINC_DE(1);
		mDEC_BC(1);
	}
	public static void mLDIR()
	{	
		while(true)
		{
			mLDI();
			if(getBC() == 0)break;
		}
	}

	///////////////////////////////////////////////
	// 静的データ領域
	///////////////////////////////////////////////

	public static ushort[] mDataTable;

};



//#include "clZ80RAM_clRAND8.cpp"





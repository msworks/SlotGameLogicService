﻿public class clZ80RAM
{
    //Z80ワークRAMの実態
    public readonly ushort[] mWorkRam = new ushort[Defines.DEF_WORKEND + 1];
    public clREG front;		//表レジスタ
    public clREG back;		//裏レジスタ
    public clRAND8 random;	//乱数クラス

    //初期化
    public clZ80RAM()
    {
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
        for (int i = 0; i < Defines.DEF_WORKEND; i++)
            mWorkRam[i] = 0;
    }

    //ワークRAM 取得(仮想1バイト)
    public ushort getWork(int index) { return mWorkRam[index]; }

    //ワークRAM 設定(仮想1バイト)
    public void setWork(int index, ushort data) {
        Defines.RAM_TRACE("setWork:[" + index + "]" + (data & 0xFFFF)); mWorkRam[index] = data; }

    //ワークRAM 取得(仮想2バイト)
    public ushort getWork16(int index) { return (ushort)((mWorkRam[index] | (mWorkRam[index + 1] << 8))); }
    //ワークRAM 設定(仮想2バイト)
    public void setWork16(int index, ushort data) { mWorkRam[index] = (ushort)(data & 0x00FF); mWorkRam[index + 1] = (ushort)((data & 0xFF00) >> 8); }

    public void clearWork(int topIndex)
    {
        int n = topIndex;
        while (n < (Defines.DEF_WORKEND)) mWorkRam[n++] = 0;
    }


    /*
        Z80 レジスターアクセス関数
    */
    public ushort getAF() { return front.AF; }
    public ushort getBC() { return front.BC; }
    public ushort getDE() { return front.DE; }
    public ushort getHL() { return front.HL; }
    public ushort getIX() { return front.IX; }
    public ushort getIY() { return front.IY; }

    //ワークRAM内取得
    public ushort getBCm() { return mWorkRam[getBC()]; }
    public ushort getDEm() { return mWorkRam[getDE()]; }
    public ushort getHLm() { return mWorkRam[getHL()]; }
    public ushort getIXm() { return mWorkRam[getIX()]; }
    public ushort getIYm() { return mWorkRam[getIY()]; }

    //静的データ内取得
    public ushort getBCt() { return mDataTable[getBC()]; }
    public ushort getDEt() { return mDataTable[getDE()]; }
    public ushort getHLt() { return mDataTable[getHL()]; }
    public ushort getIXt() { return mDataTable[getIX()]; }
    public ushort getIYt() { return mDataTable[getIY()]; }


    //1バイト取得。
    public ushort getA() { return (ushort)(front.AF & 0x00FF); }
    public ushort getB() { return (ushort)((front.BC & 0xFF00) >> 8); }
    public ushort getC() { return (ushort)(front.BC & 0x00FF); }
    public ushort getD() { return (ushort)((front.DE & 0xFF00) >> 8); }
    public ushort getE() { return (ushort)(front.DE & 0x00FF); }
    public ushort getH() { return (ushort)((front.HL & 0xFF00) >> 8); }
    public ushort getL() { return (ushort)(front.HL & 0x00FF); }

    //2バイト設定
    public void setAF(int data) { front.AF = (ushort)data; }
    public void setBC(int data) { front.BC = (ushort)data; }
    public void setDE(int data) { front.DE = (ushort)data; }
    public void setHL(int data) { front.HL = (ushort)data; }
    public void setIX(int data) { front.IX = (ushort)data; }
    public void setIY(int data) { front.IY = (ushort)data; }

    //ワークRAM内設定（1バイト）
    public void setBCm(int data) { mWorkRam[getBC()] = (ushort)data; }
    public void setDEm(int data) { mWorkRam[getDE()] = (ushort)data; }
    public void setHLm(int data) { mWorkRam[getHL()] = (ushort)data; }
    public void setIXm(int data) { mWorkRam[getIX()] = (ushort)data; }
    public void setIYm(int data) { mWorkRam[getIY()] = (ushort)data; }

    //ワークRAM内設定（2バイト）
    public void mLD_Nm_A(int index) { mWorkRam[index] = getA(); }
    public void mLD_Nm_DE(int index) { mWorkRam[index] = getE(); mWorkRam[index + 1] = getD(); }
    public void mLD_Nm_HL(int index) { mWorkRam[index] = getL(); mWorkRam[index + 1] = getH(); }

    //1バイト設定
    public void setA(int data)
    {
        front.AF &= 0xFF00;
        front.AF |= (ushort)((((ushort)data) & 0x00FF));
    }
    public void setB(int data)
    {
        front.BC &= 0x00FF;
        front.BC |= (ushort)((((ushort)data) & 0x00FF) << 8);
    }
    public void setC(int data)
    {
        front.BC &= 0xFF00;
        front.BC |= (ushort)(((ushort)data) & 0x00FF);
    }
    public void setD(int data)
    {
        front.DE &= 0x00FF;
        front.DE |= (ushort)((((ushort)data) & 0x00FF) << 8);
    }
    public void setE(int data)
    {
        front.DE &= 0xFF00;
        front.DE |= (ushort)(((ushort)data) & 0x00FF);
    }

    public void setH(int data)
    {
        front.HL &= 0x00FF;
        front.HL |= (ushort)((((ushort)data) & 0x00FF) << 8);
    }
    public void setL(int data)
    {
        front.HL &= 0xFF00;
        front.HL |= (ushort)(((ushort)data) & 0x00FF);
    }

    /*	Z80のEX AF,AF' 命令と同じ動作。
        AFの内容をAF'と交換
    */
    public void mEX_AF()
    {
        ushort tmp;
        //アホな入れ替え処理
        tmp = front.AF;
        front.AF = back.AF;
        back.AF = tmp;
    }

    /*	Z80のEX DE,HL 命令と同じ動作。
        DEの内容をHLと交換
    */
    public void mEX_DE_HL()
    {
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
    public void mEXX()
    {
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

    //	public  int getLD_Am(int index){
    public ushort mLD_A_Nm(int index)
    {
        setA(mWorkRam[index]);
        return mWorkRam[index];
    }

    public ushort mLD_BC_Nm(int index)
    {
        setBC((mWorkRam[index] | (mWorkRam[index + 1] << 8)));
        return mWorkRam[index];
    }

    public ushort mLD_DE_Nm(int index)
    {
        setDE((mWorkRam[index] | (mWorkRam[index + 1] << 8)));
        return mWorkRam[index];
    }

    public ushort mLD_HL_Nm(int index)
    {
        setHL((mWorkRam[index] | (mWorkRam[index + 1] << 8)));
        return mWorkRam[index];
    }

    public ushort mLD_BC_Nt(int index)
    {
        setBC((mDataTable[index] | (mDataTable[index + 1] << 8)));
        return mDataTable[index];
    }

    public ushort mLD_DE_Nt(int index)
    {
        setDE((mDataTable[index] | (mDataTable[index + 1] << 8)));
        return mDataTable[index];
    }

    //	public  ushort getLD_HLm(int index){
    public ushort mLD_HL_Nt(int index)
    {
        setHL((mWorkRam[index] | (mWorkRam[index + 1] << 8)));
        return mDataTable[index];
    }

    public void mINC_BC(int i) { setBC((ushort)(getBC() + i)); }
    public void mINC_DE(int i) { setDE((ushort)(getDE() + i)); }
    public void mINC_HL(int i) { setHL((ushort)(getHL() + i)); }
    public void mINC_IX(int i) { setIX((ushort)(getIX() + i)); }
    public void mINC_IY(int i) { setIY((ushort)(getIY() + i)); }

    public void mDEC_BC(int i) { setBC((ushort)(getBC() - i)); }
    public void mDEC_DE(int i) { setDE((ushort)(getDE() - i)); }
    public void mDEC_HL(int i) { setHL((ushort)(getHL() - i)); }
    public void mDEC_IX(int i) { setIX((ushort)(getIX() - i)); }
    public void mDEC_IY(int i) { setIY((ushort)(getIY() - i)); }

    public bool mDJNZ()
    {
        bool ret = false;
        ushort tmpB = getB();
        if (--tmpB == 0)
        {
            tmpB = 0;
            ret = true;
        }
        setB(tmpB);
        return ret;
    }

    public void mLDI()
    {
        setDEm(getHLm());
        mINC_HL(1);
        mINC_DE(1);
        mDEC_BC(1);
    }
    public void mLDIR()
    {
        while (true)
        {
            mLDI();
            if (getBC() == 0) break;
        }
    }

    public ushort[] mDataTable;
}

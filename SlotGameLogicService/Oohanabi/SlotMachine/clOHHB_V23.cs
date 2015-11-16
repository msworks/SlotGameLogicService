using System;

public class clOHHB_V23 : clZ80RAM
{
    Omatsuri omatsuri;
    ZZ ZZ;

    public clOHHB_V23(Omatsuri omatsuri, ZZ ZZ)
    {
        this.omatsuri = omatsuri;
        this.ZZ = ZZ;

        loadRAM();
    }

    void mSB_DTST_00(int mode)
    {
        if (mode == 0)
        {
            setE(getHLt());
            mINC_HL(1);

            setD(getHLt());
            setDE(getDE() - Defines.DEF_TOPADRS);
        }
        else
        {
            setE(getHLm());
            mINC_HL(1);
            setD(getHLm());
            setDE(getDE() - Defines.DEF_TOPADRS);
        }
        mEX_DE_HL();
    }

    ushort mSB_BNUM_00()
    {
        ushort tmpA = getA();
        setB(8);
        while (true)
        {
            tmpA = (ushort)(tmpA << 1);
            if (tmpA >= 0x0100) break;
            if (mDJNZ()) break;
        }
        setA(getB());

        return getB();
    }

    ushort mSB_ADRA_00(int Areg)
    {
        ushort tmpHL = getHL();
        Defines.RAM_TRACE("mSB_ADRA_00:" + (tmpHL & 0xFFFF) + " Areg:" + Areg);
        setHL(tmpHL + Areg);
        setA(getHLt());
        return getA();
    }

    void mSB_DIVD_00()
    {
        setE(0xFF);
        while (true)
        {

            setE(getE() + 1);
            int SUB = (int)(getA() - getD());

            if (SUB < 0)
            {
                setA(SUB + getD());
                break;
            }
            else setA(SUB);
        }
    }

    bool mSB_DDEC_00(int mode)
    {
        bool ret = true;
        if (mode == 0)
        {
            if (getHLm() == 0) ret = false;
            else
            {
                setHLm(getHLm() - 1);
                if (getHLm() == 0) ret = false;
            }
        }
        else
        {

            if (getHLt() == 0) ret = false;
        }
        return ret;
    }

    void mSTSB_GPCX_00(int mode)
    {
        ushort tmpIX = getIX();
        mLD_A_Nm((ushort)(tmpIX + Defines.DEF_CCCPIC));

        if (mode == 0)
        {
            mSTSB_GPCY_00();
        }
        else
        {
            if (getA() >= 21)
                setA(getA() - 21);

            setE(getA());
            setHL(getHL() + getA());
            setA(getHLm());
        }
    }

    void mSTSB_GPCY_00()
    {
        if (getA() >= 21)
            setA(getA() - 21);

        mSB_ADRA_00(getA());
    }

    void mSSEL_TPNT_00()
    {
        setB(getA());
        setDE(21);
        while (true)
        {
            setHL(getHL() + getDE());
            if (mDJNZ() == true) break;
        }
    }

    bool mSSEL_SPNT_00()
    {
        setD(getB());
        setE(getWork((ushort)(getIX() + Defines.DEF_CCCPIC)));

        while (true)
        {
            setA(getE());
            ushort pushHL = getHL();
            mSTSB_GPCY_00();
            setHL(pushHL);
            setA(getA() & getC());
            if (getA() > 0) break;

            setE(getE() + 1);
            if (mDJNZ() == true) break;
        }

        setA(getD());
        setA(getA() - getB());

        bool ret = true;
        if (getA() != 0) ret = false;

        return ret;
    }

    void mSCHK_SCHG_00()
    {
        bool ret = false;

        setHL(Defines.DEF_PCHGTB1);
        setA(getE());
        setA(getA() - 1);
        if (getA() != 0)
        {
            setA(getC() & Defines.DEF_FRUITFLG);
            if (getA() == 0)
            {
                setA(getD());
                if (getA() == 0x61)
                {
                    ushort pushDE = getDE();
                    setHL(Defines.DEF_PCHGTB5 + Defines.DEF_PCTCKNUM2);
                    setB(Defines.DEF_PCTCKNUM);
                    setDE(-8);
                    setA(getC());

                    ushort RLCA = getA();
                    while (true)
                    {
                        //マイナスになるか少し心配だ・・・。
                        setHL(getHL() + getDE());
                        RLCA = (ushort)(RLCA << 1);
                        if ((RLCA & 0xFF00) > 0) break;
                        if (mDJNZ() == true) break;
                    }

                    setDE(pushDE);
                    mLD_A_Nm(Defines.DEF_STOPRND);

                    setA(getA() & getE());
                    setA(getA() & 0x03);
                    setA(getA() * 2);
                    mSB_ADRA_00(getA());
                    mSB_DTST_00(0);

                    mLD_A_Nm((ushort)(getIX() + Defines.DEF_CCCPOS));
                    mSTSB_GPCY_00();

                    mSTSB_LPN3_00();
                    ret = true;
                }
            }

            if (ret == false)
                setHL(Defines.DEF_PCHGTB2);
        }

        if (ret == false)
        {
            while (true)
            {
                setA(getHLt());
                if (getA() != 0xFF)
                {
                    mINC_HL(1);
                    if (getA() == getD())
                    {
                        setA(getHLt() & getC());
                        mINC_HL(1);
                        if (getA() != 0)
                        {
                            mLD_A_Nm(Defines.DEF_GMLVSTS);

                            setA(getA() & getHLt());
                            if (getA() != 0)
                            {
                                mINC_HL(1);
                                mSB_DTST_00(0);
                                break;
                            }
                        }
                    }
                    else
                        mINC_HL(1);
                    //_SCHK_SCHG_05:
                    mINC_HL(3);
                }
                else
                {
                    ret = true;
                    break;
                }
            }
            //_SCHK_SCHG_07:
            if (ret == false)
            {
                while (true)
                {
                    mINC_HL(1);

                    setA(getHLt() + 1);
                    if (getA() != 0)
                    {
                        setA(getA() + 1);
                        if (getA() == 0)
                        {
                            mINC_HL(1);

                            setB(getHLt());
                            mINC_HL(1);
                        }
                    }
                    else
                    {
                        ret = true;
                        break;
                    }
                    //_SCHK_SCHG_08:

                    setA(getHLt());
                    setD(21);
                    mSB_DIVD_00();
                    if (getA() == getWork((ushort)(getIX() + (Defines.DEF_CCCPOS)))) break;
                }

                if (ret == false)
                {

                    setHL(Defines.DEF_CHGLINE);
                    setA(getE());
                    setA(getA() * 2);
                    setA(getA() + getE());
                    mSB_ADRA_00(getA());
                    setD(getA());
                    setA(128 - getD());
                    setE(getA());
                    mINC_HL(1);

                    mLD_A_Nm(Defines.DEF_GMLVSTS);
                    setA(getA() & 0x07);
                    if (getA() == 0)
                    {
                        mINC_HL(1);
                        setD(getE());
                    }
                    //_SCHK_SCHG_09:
                    mLD_A_Nm(Defines.DEF_MEDLCTR);

                    bool CY = true;
                    if (getA() != 2)
                    {
                        mLD_A_Nm(Defines.DEF_STOPRND);
                        if (getA() >= getD()) CY = false;
                    }
                    //_SCHK_SCHG_10:
                    //※PUSH AFはキャリーフラグの保存なので記述しない。

                    setA(getHLt());
                    setD(16);
                    mSB_DIVD_00();
                    setH(getE());
                    setL(getA());
                    //PUSHがないので当然POPもしない。
                    if (CY == false)
                    {
                        setH(getL());
                    }
                    //_SCHK_SCHG_11:
                    setL(getB());
                    mLD_Nm_HL(Defines.DEF_TBLNUM);
                }
            }
        }
    }

    ushort MN_CKLN;

    void mMN_CKLN_00()
    {
        //MN_CKLN_00:
        setC(0x08);
        setDE(Defines.DEF_ARAY11);
        //JAVA圧縮ツールのバグのため外に出します。
        //		  ushort MN_CKLN = 0x00;
        MN_CKLN = 0x00;

        ushort pushDE = 0;
        ushort pushHL = 0;

        //
        //=== 有効 ﾗｲﾝ の ﾁｪｯｸ
        //
        //MN_CKLN_01:
        while (true)
        {
            if (MN_CKLN == 0x00)
            {
                mLD_A_Nm(Defines.DEF_MLAMPST);
                setA(getA() & getC());
                if (getA() == 0) break;

                setHL(Defines.DEF_COMPTBL);
            }

            MN_CKLN = 0x00;
            //
            //=== 検索制御 ｺｰﾄﾞ の ﾁｪｯｸ
            //
            //MN_CKLN_02:

            setA(getHLt());
            if (getA() != 0)
            {
                pushDE = getDE();
                pushHL = getHL();

                mLD_A_Nm(Defines.DEF_GMLVSTS);

                setA(getA() & getHLt());
                if (getA() == 0)
                {
                    MN_CKLN = 0x07;
                    //					break;
                }
                else
                {
                    mINC_HL(1);

                    setA(getHLt() & getC());
                    if (getA() == 0)
                    {
                        MN_CKLN = 0x07;
                        //						break;
                    }
                }

                if (MN_CKLN != 0x07)
                {
                    mEX_DE_HL();
                    setB(Defines.DEF_REELNUM);

                    while (true)
                    {
                        mINC_DE(1);
                        setA(getDEt());
                        if (getA() != 0)
                        {
                            setA(getA() & getHLm());
                            if (getA() == 0)
                            {
                                MN_CKLN = 0x07;
                                break;
                            }
                        }

                        mINC_HL(1);
                        if (mDJNZ() == true) break;
                    }

                    if (MN_CKLN != 0x07)
                    {
                        setHL(Defines.DEF_HITFLAG);
                        mINC_DE(1);
                        setA(getDEt());
                        setA(getA() | getHLm());
                        setHLm(getA());

                        mINC_HL(1);
                        mINC_DE(1);
                        setA(getDEt());
                        setA(getA() >> 4);
                        setA(getA() & 0x0F);

                        mINC_HL(1);
                        setA(getDEt());
                        setA(getA() & 0x0F);
                        setA(getA() + getHLm());
                        if (getA() >= (Defines.DEF_PAYMAX + 1))
                        {
                            setA(Defines.DEF_PAYMAX);
                        }
                        //MN_CKLN_05:
                        setHLm(getA());
                        mINC_HL(1);
                        setA(getHLm() | getC());
                        setHLm(getA());

                        setHL(pushHL);
                        setDE(pushDE);

                    }
                }
            }
            else MN_CKLN = 0x00;

            if (MN_CKLN != 0x07)
            {

                mINC_DE(3);
                setC(getC() << 1);
                MN_CKLN = 0x00;
            }

            if (MN_CKLN == 0x07)
            {
                setHL(pushHL);
                setDE(Defines.DEF_CMPTLNG);
                setHL(getHL() + getDE());
                setDE(pushDE);
            }
        }
    }

    void mSTSB_STRE_00()
    {
        setE(getWork(Defines.DEF_MEDLCTR));
        setD(getWork(Defines.DEF_STOPBIT));
        setB(getWork(Defines.DEF_GMLVSTS));
        setC(getWork(Defines.DEF_WAVEBIT));
    }

    void mSTOP_SSEL_00()
    {
        int _STOP_SSEL = 0x02;

        // 使用 ｽﾃｰﾀｽ ﾚｼﾞｽﾀ 格納処理
        mSTSB_STRE_00();

        setA(getB() & 0x01);
        if (getA() == 0)
        {
            setA(getD());
            if (getA() == 0x61)
            {
                setA(getE());
                setA(getA() - 1);
                if (getA() != 0)
                {
                    // ﾗｲﾝ ﾏｽｸ ﾃﾞｰﾀ 変更処理 2
                    mSTSB_LPN2_00();
                    setHL(Defines.DEF_P1STTBL - 21);
                    mLD_A_Nm(Defines.DEF_FLGCTR);
                    if (getA() >= Defines.DEF_BNSFLGC)
                    {
                        setA(0);
                    }

                    setA(getA() + 1);
                    _STOP_SSEL = 0x04;
                }
            }

        }
        if (_STOP_SSEL == 0x02)
        {
            //_STOP_SSEL_02:
            mLD_A_Nm(Defines.DEF_LINENUM);
            mSTSB_LPN1_00();

            setHL(Defines.DEF_ST101TBL - 21);
            mLD_A_Nm(Defines.DEF_STOPBIT);
            ushort RRCA = (ushort)(getA() << 8);
            RRCA = (ushort)(RRCA >> 1);
            if ((RRCA & 0x00FF) == 0)
            {
                setHL(Defines.DEF_ST201TBL - 21);
                RRCA = (ushort)(RRCA >> 1);
                if ((RRCA & 0x00FF) == 0)
                {
                    setHL(Defines.DEF_ST301TBL - 21);
                }
            }
            //_STOP_SSEL_03:
            mLD_A_Nm(Defines.DEF_TBLNUM);
        }
        //_STOP_SSEL_04:

        // 停止 ﾃｰﾌﾞﾙ 検索処理
        mSSEL_TPNT_00();

        setB(5);
        mSSEL_SPNT_00();

        //Aﾚｼﾞｽﾀｰに滑り値を格納。
    }

    void mSTOP_RECH_00()
    {
        //_STOP_RECH_00:
        mLD_A_Nm(Defines.DEF_PUSHCTR);
        setA(getA() - 1);
        if (getA() == 0)
        {
            mLD_A_Nm(Defines.DEF_GMLVSTS);
            setA(getA() & 0x03);
            if (getA() == 0)
            {
                setD(getA());
                mLD_A_Nm(Defines.DEF_MEDLCTR);
                setA((getA() * 2) - 1);
                setB(getA());
                setHL(Defines.DEF_ARAY11 - 1);

                while (true)
                {
                    mINC_HL(1);
                    setA(getHLm());
                    mINC_HL(1);
                    setA(getA() & getHLm());
                    mINC_HL(1);
                    setA(getA() & getHLm());
                    setA(getA() & Defines.DEF_REACHDAT);
                    if (getA() != 0)
                    {
                        setD(getD() + 1);
                    }

                    if (mDJNZ() == true) break;
                }
                setA(getD());
                if (getA() != 0)
                {
                    //実機ではここでBB入賞期待音とチャンスLEDを設定していますが、
                    //アプリでは現状使用しませんのでここまでしておきます。
                    //この場所の情報をアプリ用ワークでフラグ化すれば、アプリ側でも
                    //このタイミングは拾えるようになります。（要相談）
                }
            }
        }
    }

    void mSTOP_SCHK_00()
    {
        mLD_A_Nm(Defines.DEF_PUSHCTR);
        if (getA() != 0)
        {
            mLD_A_Nm(Defines.DEF_GMLVSTS);
            setA(getA() - 1);
            if (getA() != 0)
            {
                // 使用 ｽﾃｰﾀｽ ﾚｼﾞｽﾀ 格納処理
                mSTSB_STRE_00();
                // 回胴停止後の変更処理
                mSCHK_SCHG_00();
            }
        }
    }

    void mSTSB_ARYS_00()
    {

        setDE(Defines.DEF_STOPBIT);
        setA(getDEm() & 0x07);
        mSB_BNUM_00();


        setHL(Defines.DEF_RCB_POS - 1);


        setHL(getHL() + getA());
        setA(getHLm());

        setC(getA());
        setB(5);
        setA(getDEm());

        ushort RRCA = (ushort)(getA() << 8);

        RRCA = (ushort)(RRCA >> 1);
        setDE(Defines.DEF_ARAY51);
        setH(0);
        if ((RRCA & 0x00FF) == 0)
        {

            RRCA = (ushort)(RRCA >> 1);
            mINC_DE(1);
            setH(getB());
            if ((RRCA & 0x00FF) == 0)
            {
                mINC_DE(1);
                setH(10);
            }
        }


        setA(getC());
        setC(getH());
        setHL(Defines.DEF_REELTB1);
        mSTSB_GPCY_00();


        while (true)
        {
            ushort pushHL = getHL();
            setHL(Defines.DEF_ARSETTBL - 1);
            setA(getB() + getC());
            mSB_ADRA_00(getA());
            setHL(pushHL);
            mSB_ADRA_00(getA());
            setDEm(getA());
            mDEC_DE(3);
            setHL(pushHL);
            if (mDJNZ() == true) break;
        }
    }

    void mSTSB_LPN1_00()
    {
        setHL(Defines.DEF_LINEDAT1);
        mSB_ADRA_00(getA());
        setC(getA());
    }

    void mSTSB_LPN2_00()
    {
        //_STSB_LPN2_00:
        setA(getC() & Defines.DEF_FRUITFLG2);
        if (getA() == 0)
        {
            setA(getA() | 0x80);
        }
        //_STSB_LPN2_01:
        setC(getA());
        setHL(Defines.DEF_FRSTTBL - 8);
        setA(getB());
        mSB_BNUM_00();
        setA(getA() * 4);
        mSB_ADRA_00(getA());
        mSB_DTST_00(0);
        mSTSB_GPCX_00(0);
        setA(getA() & getC());
        mINC_DE(1);
        if (getA() != 0)
        {
            mINC_DE(1);
        }
        setA(getDEt());
        setC(getA());
    }

    void mSTSB_LPN3_00()
    {
        setD(8);
        mSB_DIVD_00();
        setH(getA());
        setL(getE());
        mLD_Nm_HL(Defines.DEF_TBLNUM);
    }

    bool mMN_ILCK_00()
    {
        bool ret = false;
        mLD_A_Nm(Defines.DEF_WAVEBIT);
        setHL(Defines.DEF_HITFLAG);

        setA((getA() ^ getHLm()) & getHLm());
        if (getA() != 0)
        {
            setA(0);
            mLD_Nm_A(Defines.DEF_HITFLAG);
            mLD_Nm_A(Defines.DEF_HITSND);
            mLD_Nm_A(Defines.DEF_HITCTR);
            mLD_Nm_A(Defines.DEF_HITLINE);
            ret = true; //エラー
        }

        return ret;
    }

    void mMN_FSEL_00()
    {
        ushort pushAF = 0;
        setAF(0);

        setB(0);
        //アプリ用に変更！（4thリールのランプ点灯中は０以上にしてください。）メソッド作るか・・・。
        mLD_A_Nm(Defines.DEF_FOUT3);
        if (getA() == 0)
        {
            ushort tmp;
            Defines.RAM_TRACE("抽選開始？");
            mLD_A_Nm(Defines.DEF_GMLVSTS);
            mSB_BNUM_00();
            setHL(Defines.DEF_RLSLTBL - 1);
            //mSB_ADRA_00:2159 Areg:3
            // 演出テーブル
            tmp = mSB_ADRA_00(getA());
            Defines.RAM_TRACE("A=" + (tmp & 0xFFFF));
            // A=0A　HL=2162
            setA(getA() + getC());
            // A=14
            tmp = mSB_ADRA_00(getA());
            Defines.RAM_TRACE("A=" + (tmp & 0xFFFF));
            // A=2　HL=2176
            setHL(Defines.DEF_SELECTTBL);

            tmp = mSB_ADRA_00(getA());
            Defines.RAM_TRACE("A=" + (tmp & 0xFFFF));
            tmp = mSB_ADRA_00(getA());
            Defines.RAM_TRACE("A=" + (tmp & 0xFFFF));

            int rand = clRAND8.mGetRnd8();
            Defines.RAM_TRACE("抽選時 rand:" + rand);
            Defines.RAM_TRACE("抽選時 rand&0x7F:" + (rand & 0x7F));
            setA((ushort)(clRAND8.mGetRnd8() & 0x7F));
            Defines.RAM_TRACE("抽選時 getA2:" + (getA() & 0xFFFF));

            setB(0);

            Defines.RAM_TRACE("抽選時 getA:" + (getA() & 0xFFFF));
            Defines.RAM_TRACE("抽選時 getHL:" + (getHL() & 0xFFFF));
            Defines.RAM_TRACE("抽選時 getHLt:" + (getHLt() & 0xFFFF));

            if (getA() < getHLt())
            {
                //=== 演出当選時
                Defines.RAM_TRACE("演出当選時");

                mINC_HL(1);
                mEX_DE_HL();
                mLD_A_Nm(Defines.DEF_RANDOMY);
                //MN_FSEL_01:
                while (true)
                {
                    pushAF = getAF();
                    setHL(Defines.DEF_AVGTBL);
                    setA(getDEt());
                    setA(getA() & 0x3F);
                    mSB_ADRA_00(getA());
                    setAF(pushAF);

                    setAF(getAF() - getHLt());
                    if ((getAF() & 0xFF00) > 0) break;
                    mINC_DE(2);
                }
                //MN_FSEL_02:
                setHL(Defines.DEF_FLASH);
                setA(getDEt());
                setA(getA() & 0xC0);
                setHLm(getA());
                mINC_DE(1);
                mINC_HL(1);

                //アプリ用に全ての情報をDfOHHB_V23_DEF.DEF_FLASH+1に格納。
                setA(getDEt());
                setHLm(getA());

                //回転表示器回転要求ﾃﾞｰﾀﾃｰﾌﾞﾙ（DfOHHB_V23_DEF.DEF_RLPTNTBLはアプリ側で用意しているので転送しない）

                //
                ///=== 遊技開始音選択
                //スタート音はDfOHHB_V23_DEF.DEF_FLASH+0にビット格納しているので、アプリ側で分解


            }

        }

    }

    void mSB_RTCK_00()
    {
        //この機種はＲＴはありません。
    }

    bool mMN_GCCK_00()
    {
        bool CY = false;
        //MN_GCCK_00:


        //TRACE("ＢＢ、ＲＢ遊技数チェック処理:" + (getA()&0xFFFF));

        if ((getA() & 0x01) == 0)
        {
            //
            //=== ＢＢ遊技数チェック処理
            //
            setHL(Defines.DEF_BBGMCTR);
            mSB_DDEC_00(0);

            mLD_A_Nm(Defines.DEF_HITFLAG);
            if ((getA() & (0x01 << Defines.DEF_RJAC_BITN)) > 0)
            {
                //TRACE("JACあたり？");
                // CALL    MN_GMIT_03          ; RB 初期化処理
                // 上記関数内で必要そうな処理のみを直接やってみる。
                setA(0);
                mLD_Nm_A(Defines.DEF_HITREQ);
                setA(8);
                mLD_Nm_A(Defines.DEF_JAC_CTR);
                setA(12);
                mLD_Nm_A(Defines.DEF_JACGAME);
                setHL(Defines.DEF_GAMEST);
                setHLm(getHLm() | (0x01 << Defines.DEF_RBGC_FLN));

                omatsuri.JacIn();
                return CY;
                //				mLD_A_Nm(BIGGCTR);	
            }

            //
            //=== BB 作動中の遊技数 ﾁｪｯｸ
            //
            //MN_GCCK_01:
            //TRACE("BB中チェック？" + (getHLm() & 0xFFFF));
            setA(getHLm());
            if (getA() != 0)
                return CY;
            //
            //=== 役物入賞チェック処理
            //
            //MN_GCCK_02:
        }
        bool jac_ctr_bool = false;
        mLD_A_Nm(Defines.DEF_HITFLAG);
        if (getA() != 0)
        {
            setHL(Defines.DEF_JAC_CTR);
            setHLm(getHLm() - 1);
            if (getHLm() == 0)
                jac_ctr_bool = true;
        }
        //else{
        //
        //=== RB 作動中の遊技数 ﾁｪｯｸ
        //
        //MN_GCCK_03:
        // JACゲーム 遊技可能回数
        setHL(Defines.DEF_JACGAME);
        //TRACE("RB or JAC 中チェック？" + (getHLm() & 0xFFFF) + ":" + jac_ctr_bool);
        if ((mSB_DDEC_00(0) == true) && (jac_ctr_bool == false))
        {
            //				setA(0);
            //				mLD_Nm_A(DfOHHB_V23_DEF.DEF_JAC_CTR);
            return CY;
        }
        else
        {
            //
            //=== ＲＢ終了時の処理
            //TRACE("RB or JAC 終了時の処理");
            //MN_GCCK_04:
            setA(0);
            mLD_Nm_A(Defines.DEF_HITREQ);
            mLD_Nm_A(Defines.DEF_JACGAME);
            mLD_Nm_A(Defines.DEF_JAC_CTR);

            setHL(Defines.DEF_GAMEST);
            setHLm(getHLm() & ~(0x01 << Defines.DEF_RBGC_FLN));
            //
            //=== 遊技復帰条件の ﾁｪｯｸ
            //		
            if ((getHLm() & (0x1 << Defines.DEF_BBGC_FLN)) == 0)
            {
                CY = true;
                return CY;
            }

        }
        //		}

        //MN_GCCK_05:
        //  残りJACIN可能回数

        setHL(Defines.DEF_BIGBCTR);
        setHLm(getHLm() - 1);
        //TRACE("残りJACIN可能回数の処理:" + (getHLm()&0xFFFF));
        if (getHLm() != 0)
        {
            mLD_A_Nm(Defines.DEF_BBGMCTR);
            if (getA() > 0)
                return CY;
        }
        //
        //=== ＢＢ終了時の処理
        //TRACE("ＢＢ終了時の処理");
        //MN_GCCK_06:
        setA(0);
        mLD_Nm_A(Defines.DEF_GAMEST);

        CY = true;
        return CY;
    }
    //========================================================================================
    //== (4-09)MN_GMIT_00 特賞作動チェック処理
    //========================================================================================
    void mMN_GMIT_00()
    {
        //MN_GMIT_00:
        ushort MN_GMIT = 0x00;
        mLD_A_Nm(Defines.DEF_HITFLAG);
        if ((getA() & 0x20) > 0) MN_GMIT = 0x01;
        if ((getA() & 0x10) > 0) MN_GMIT = 0x02;

        switch (MN_GMIT)
        {
            case 0x00:
                if ((getA() & 0x08) > 0)
                {
                    //
                    //=== 再遊技初期設定処理
                    //
                    setHL(Defines.DEF_GAMEST);
                    setHLm(getHLm() | (0x1 << Defines.DEF_RPLC_FLN));
                }
                break;
            case 0x01:
                //
                //=== ＢＢ初期設定処理
                //
                //MN_GMIT_01:
                setA(0);
                mLD_Nm_A(Defines.DEF_HITREQ);
                mLD_Nm_A(Defines.DEF_FOUT3);
                setA(0x80);
                mLD_Nm_A(Defines.DEF_GAMEST);
                setA(Defines.DEF_RBHMAX);
                mLD_Nm_A(Defines.DEF_BIGBCTR);
                setA(Defines.DEF_BBGMAX);
                mLD_Nm_A(Defines.DEF_BBGMCTR);

                break;
            case 0x02:
                setA(0);
                mLD_Nm_A(Defines.DEF_HITREQ);
                mLD_Nm_A(Defines.DEF_FOUT3);
                setA(8);
                mLD_Nm_A(Defines.DEF_JAC_CTR);
                setA(12);
                mLD_Nm_A(Defines.DEF_JACGAME);
                setHL(Defines.DEF_GAMEST);
                setHLm(getHLm() | (0x01 << Defines.DEF_RBGC_FLN));
                break;
        }
    }

    void mSB_RRND_00()
    {

    }


    void mMN_PRND_00()
    {

        setA((ushort)clRAND8.mGetRnd8());
        setL(getA());
        setA((ushort)clRAND8.mGetRnd8());
        setA(getA() & Defines.DEF_RNDMASK);
        setH(getA());
        mLD_Nm_HL(Defines.DEF_RANDOMX);

        setA((ushort)clRAND8.mGetRnd8());
        mLD_Nm_A(Defines.DEF_RANDOMY);
    }

    public void mMN_GLCK_00()
    {
        setA(0x01);
        setHL(Defines.DEF_GAMEST);
        if ((getHLm() & (0x01 << Defines.DEF_RBGC_FLN)) == 0)
        {
            setA(getA() * 2);
            if ((getHLm() & (0x01 << Defines.DEF_BBGC_FLN)) == 0)
            {
                setA(getA() * 4);
                setHL(Defines.DEF_HITREQ);
                if ((getHLm() & (0x01 << Defines.DEF_RBGL_BITN)) == 0)
                {
                    setA(getA() * 2);
                    if ((getHLm() & (0x01 << Defines.DEF_BBGL_BITN)) == 0)
                    {
                        setA(0x04);
                    }
                }
            }
        }
        //アプリで追加。
        mLD_Nm_A(Defines.DEF_GMLVSTS);

        //アプリで追加。
        setHL(Defines.DEF_GAMEST);
        setHLm((getHLm() & ~(0x01 << Defines.DEF_RPLC_FLN)));
        //デバッグ用フラグをクリア
        setHLm((getHLm() & ~(0x01 << Defines.DEF_STOPRND_FLN)));
    }

    public void mMN_WCAL_00()
    {

        ushort MN_WCAL = 0x02;

        mLD_HL_Nm(Defines.DEF_RANDOMX);			// 乱数値 X [ 0-16383 ]
        setC(1);
        mLD_A_Nm(Defines.DEF_GMLVSTS);
        ushort tmpAF = (ushort)(getA() << 8);
        tmpAF = (ushort)(tmpAF >> 1);

        // RB 作動中か ?
        if ((tmpAF & 0x00FF) > 0)
        {//RB作動中！
            //強制フラグ処理。(JAC中はﾊｽﾞﾚとﾘﾌﾟﾚｲ以外は処理しない。)
            ushort flag = getWork(Defines.DEF_FORCE_FLAG);
            if ((flag > 0) && ((flag == (int)Defines.ForceYakuFlag.HAZURE) || (flag == (int)Defines.ForceYakuFlag.REPLAY)))
            {//強制フラグの指定だった！
                setA(flag - 1);
                setC(getA());
                mLD_Nm_A(Defines.DEF_WAVEBIT);
                MN_WCAL = 0x01;

            }
            else
            {
                mLD_DE_Nt(Defines.DEF_PRB_JHTBL);	// JAC の確率抽選値 ﾃﾞｰﾀ
                setA(Defines.DEF_JACHITF);
                int ADD = (int)(getHL() + getDE());
                //当選か ?
                if (ADD > 0xFFFF)
                {//不当選（16/16383）
                    setA(0);
                    setC(getA());
                }

                mLD_Nm_A(Defines.DEF_WAVEBIT);
                MN_WCAL = 0x01;
            }
        }

        if (MN_WCAL == 0x02)
        {

            setDE(Defines.DEF_PRB_BBTBL);
            setB(Defines.DEF_ETC_NUM);
            tmpAF = (ushort)(tmpAF >> 1);
            if ((tmpAF & 0x00FF) == 0)
            {
                tmpAF = (ushort)(tmpAF >> 1);
                if ((tmpAF & 0x00FF) > 0)
                {
                    setB(Defines.DEF_FLAGNUM);
                }

                setDE(Defines.DEF_PRB_FLTBL);
            }

            while (true)
            {
                //強制フラグ処理。(BB中のボーナス指定は無効。)
                ushort flag = getWork(Defines.DEF_FORCE_FLAG);

                if (flag > 0)
                {//強制ワークに値がある！
                    Defines.RAM_TRACE("強制フラグ" + (flag & 0xFFFF));
                    if (((int)(flag - 1)) <= getB())
                    {//状態別のフラグ数以下なら強制フラグをセットする。
                        setC((ushort)(flag - 1));
                        break;
                    }
                }

                ushort pushDE = getDE();		// 期待値 ﾃｰﾌﾞﾙ を退避
                ushort pushHL = getHL();		// 乱数値を退避
                //				ushort pushDE1 = getDE();		// 期待値 ﾃｰﾌﾞﾙ を退避

                Defines.RAM_TRACE("子役抽選開始！！！");

                setHL(Defines.DEF_SATDAT - 1);
                Defines.RAM_TRACE("抽選1:mDataTable:" + (getHL() & 0xFFFF));
                setA(getC());
                mSB_ADRA_00(getA());
                setE(getA());
                Defines.RAM_TRACE("抽選2:mDataTable:" + (getHL() & 0xFFFF));
                //アプリでは使用しない。
                //********************************************************
                //		          CALL    SB_MDCK_00				// ﾒﾀﾞﾙ 投入枚数 ﾁｪｯｸ 処理
                //		          JR      Z,$						// 0 枚なら ｼﾞｬﾝﾌﾟ
                //********************************************************

                //アプリ用の処理とする。
                //※（危険そうなので後で枚数チェックをすること！！）

                mLD_A_Nm(Defines.DEF_MEDLCTR);
                Defines.RAM_TRACE("抽選3:mDataTable:" + (getHL() & 0xFFFF));
                //ﾒﾀﾞﾙ ｶｳﾝﾀｰ 格納領域 ｾｯﾄ
                // BET別抽選確率のトップ位置
                setHL(pushDE);				// 期待値 ﾃｰﾌﾞﾙ を復帰
                Defines.RAM_TRACE("抽選4:mDataTable:" + (getHL() & 0xFFFF));
                //ＣＹ判定用にローカルワークを準備
                ushort lCY = (ushort)(getE() << 1);
                setE(getE() << 1);			// ﾃﾞｰﾀ 構成用に調整
                setD(0);
                Defines.RAM_TRACE("抽選5:mDataTable:" + (getHL() & 0xFFFF));
                setHL(getHL() + getDE());
                Defines.RAM_TRACE("抽選6:mDataTable:" + (getHL() & 0xFFFF));
                setA(getA() - 1);

                Defines.RAM_TRACE("抽選7:mDataTable:" + (getHL() & 0xFFFF));
                // 設定別の判断が不要なら ｼﾞｬﾝﾌﾟ
                if ((lCY & 0xFF00) > 0)
                {

                    setA(getA() * 12);
                    mSB_ADRA_00(getA());
                    //SB_WVCK_00の代わり
                    //---------------------
                    mLD_A_Nm(Defines.DEF_WAVENUM);
                    Defines.RAM_TRACE("設定別に内容変更！！:" + (getA() & 0xFFFF));
                    if (getA() > Defines.DEF_WAVEMAX)
                    {
                        setA(Defines.DEF_SET_INIT);
                        mLD_Nm_A(Defines.DEF_WAVENUM);
                    }
                    //---------------------
                }
                Defines.RAM_TRACE("抽選8:mDataTable:" + (getHL() & 0xFFFF));
                //MN_WCAL_05:
                setA(getA() * 2);
                mSB_ADRA_00(getA());
                //
                //=== 抽選の実行
                //
                Defines.RAM_TRACE("子役確率1:mDataTable[" + (getHL() & 0xFFFF) + "]=0x" + ZZ.hexShort((short)(getHLt() & 0xFFFF)));
                setE(getHLt());
                mINC_HL(1);

                Defines.RAM_TRACE("子役確率2:mDataTable[" + (getHL() & 0xFFFF) + "]=0x" + ZZ.hexShort((short)(getHLt() & 0xFFFF)));
                setD(getHLt());

                // 乱数のセット
                setHL(pushHL);

                Defines.RAM_TRACE("確率？: 乱数:" + (getHL() & 0xFFFF) + " ﾃｰﾌﾞﾙﾃﾞｰﾀ？:0x" + ZZ.hexShort((short)(getDE() & 0xFFFF)));

                int SBC;
                if ((lCY & 0xFF00) > 0)
                {	// 設定別＝ボーナスしかなかったのでこれで仕分けする
                    Defines.RAM_TRACE("確率変更:" + getDE() + "→" + (getDE() * omatsuri.GPW_chgProba()));
                    SBC = (int)((getHL() & 0xFFFF) - (getDE() * omatsuri.GPW_chgProba()));
                }
                else
                {
                    SBC = (int)((getHL() & 0xFFFF) - (getDE()));
                }
                setHL((ushort)SBC);

                setDE(pushDE);

                Defines.RAM_TRACE("当選ﾁｪｯｸ？" + SBC);
                // 当選か ?
                if (SBC < 0) break;


                setC(getC() + 1);
                if (mDJNZ() == true)
                {
                    setC(getB());
                    break;
                }
            }
            //MN_WCAL_06:

            setHL(Defines.DEF_HFLGTBL);				// 当たり要求 ﾌﾗｸﾞ ﾃｰﾌﾞﾙ
            setA(getC());
            setA(getA() * 2);
            mSB_ADRA_00(getA());
            mINC_HL(1);

            setD(getHLt());

            setHL(Defines.DEF_HITREQ);				// 内部当たり ﾌﾗｸﾞ 格納領域
            setA(getA() | getHLm());
            setHLm(getA());					// 内部当たり ﾌﾗｸﾞ ｾｯﾄ
            Defines.RAM_TRACE("内部当たり:" + (getHLm() & 0xFFFF));
            mINC_HL(1);						// [ DfOHHB_V23_DEF.DEF_WAVEBIT ]
            setA(getA() | getD());
            setHLm(getA());					// 当たり要求 ﾌﾗｸﾞ ｾｯﾄ


            Defines.RAM_TRACE("当たり要求:" + (getHLm() & 0xFFFF));
        }

        // アプリ用処理。MAIN_04の処理から移動。
        //-----------------------------------
        // 当選番号をｽﾄｯﾌﾟ 用 ｾﾚｸﾄ ｶｳﾝﾀｰに格納。
        setA(getC());
        mLD_Nm_A(Defines.DEF_FLGCTR);

        //4th演出抽選
        mMN_FSEL_00();

        setA(0);
        mLD_Nm_A(Defines.DEF_RCB_ST + 0);		// [ DfOHHB_V23_DEF.DEF_RCB_ST+0 ]   1stﾘｰﾙｽﾃｰﾀｽｸﾘｱ
        mLD_Nm_A(Defines.DEF_RCB_ST + 1);		// [ DfOHHB_V23_DEF.DEF_RCB_ST+1 ]   2ndﾘｰﾙｽﾃｰﾀｽｸﾘｱ
        mLD_Nm_A(Defines.DEF_RCB_ST + 2);		// [ DfOHHB_V23_DEF.DEF_RCB_ST+2 ]   3rdﾘｰﾙｽﾃｰﾀｽｸﾘｱ
        setA(0x07);
        mLD_Nm_A(Defines.DEF_SLAMPBIT);		// [ DfOHHB_V23_DEF.DEF_SLAMPBIT ]   ｽﾄｯﾌﾟﾎﾞﾀﾝLED状態に全ﾘｰﾙ回転ｾｯﾄ
        setA(Defines.DEF_REELNUM);
        mLD_Nm_A(Defines.DEF_PUSHCTR);		// [ DfOHHB_V23_DEF.DEF_PUSHCTR ]    ｽﾄｯﾌﾟﾎﾞﾀﾝ作動ｶｳﾝﾀｰ全ﾘｰﾙ回転ｾｯﾄ

        setHL(Defines.DEF_REELST);
        setA(getHLm() | 0x07);
        setHLm(getA());				// [ DfOHHB_V23_DEF.DEF_REELST ]     ﾘｰﾙ制御ﾌﾗｸﾞに全ﾘｰﾙ回転要求ｾｯﾄ

        //-----------------------------------
    }

    ushort mDbSdatAdr;
    public void mMN_SDAT_00()
    {
        //MN_SDAT_00:
        setHL(Defines.DEF_GMLVSTS);
        setA(getHLm());
        mSB_BNUM_00();
        mINC_HL(1);
        setA(getHLm());
        setB(getB() - 1);
        setHL(Defines.DEF_STPTBL1);
        if (getB() != 0)
        {
            setC(getA());
            setHL(Defines.DEF_STPTBL0);
            mSB_ADRA_00(getA());

            setHL(Defines.DEF_STPTBL2 - 1);
            mSB_ADRA_00(getA());

            mLD_A_Nm(Defines.DEF_MEDLCTR);
            if (Defines.DEF_FRTFLGC < getC())
            {
                setB(0);
            }
            else
            {
                //MN_SDAT_01:
                setA(getA() - 1);
                setA(getA() * 4);
            }
        }
        //MN_SDAT_02:
        setA(getA() + getB());
        mSB_ADRA_00(getA());
        setD(23);
        mSB_DIVD_00();
        setHL(Defines.DEF_LINEDAT2);
        mSB_ADRA_00(getA());
        mSB_ADRA_00(getA());
        setA(getE());
        mLD_Nm_A(Defines.DEF_TBLNUM);

        //		  ushort R = 0;
        ushort R = (ushort)clRAND8.mGetRnd8();
        //デバッグ用にHLアドレスを保存
        mDbSdatAdr = (ushort)getHL();


        setA(R & 0x0F);
        setD(getHLt());
        mINC_HL(1);
        mSB_DIVD_00();
        mSB_ADRA_00(getA());
        mLD_Nm_A(Defines.DEF_LINENUM);
    }

    public void mMN_STOP_00()
    {
        //mDebugStopRndが呼ばれている
        if ((getWork(Defines.DEF_GAMEST) & (0x01 << Defines.DEF_STOPRND_FLN)) > 0)
        {//デバッグフラグが立っていたら処理する
            if ((mDbSdatAdr != 0))
            {

                setHL((int)mDbSdatAdr);

                setD(getHLt());
                mINC_HL(1);
                mLD_A_Nm(Defines.DEF_STOPRND);
                mSB_ADRA_00(getA() % getD());
                mLD_Nm_A(Defines.DEF_LINENUM);
                mDbSdatAdr = 0;
            }
        }

        setHL(Defines.DEF_SLAMPBIT);		//ｽﾄｯﾌﾟﾎﾞﾀﾝ LED 状態格納領域 ｾｯﾄ
        setA(getHLm());			//ｽﾄｯﾌﾟﾎﾞﾀﾝ LED 状態 取得 ( 点灯で回転中 )
        setD(~getA());		//反転 ( 消灯で回転中 )

        mLD_A_Nm(Defines.DEF_INBUFF0);		//入力 ﾎﾟｰﾄ 0 ( ｽﾄｯﾌﾟﾎﾞﾀﾝ 入力 ) の状態 取得
        setE(getA());
        setA(getA() & getD());	//有効な ( 回転中の ) ｽﾄｯﾌﾟﾎﾞﾀﾝ 以外の操作か ?

        //有効なストップボタン以外は処理しない
        if (getA() == 0)
        {
            //入力 ﾎﾟｰﾄ 0 ( ｽﾄｯﾌﾟﾎﾞﾀﾝ 入力 ) の状態 取得
            mLD_A_Nm(Defines.DEF_INBUFF0);
            //有効な ( 回転中の ) ｽﾄｯﾌﾟﾎﾞﾀﾝ 入力か ?
            if ((getA() & getHLm()) > 0)
            {
                setIX(Defines.DEF_RCB_ST);				//  ﾘｰﾙ ｽﾃｰﾀｽ 格納領域
                setBC((2 * 256) + 0x01);		// ﾘｰﾙ 数 ､ ｽﾄｯﾌﾟﾎﾞﾀﾝ 作動 ﾋﾞｯﾄ

                ushort RRCA = (ushort)((getA() & 0x00FF) << 8);

                while (true)
                {
                    RRCA = (ushort)(RRCA >> 1);
                    if ((RRCA & 0x0080) > 0) break;
                    mINC_IX(1);
                    setC((getC() << 1));
                    if (mDJNZ() == true) break;
                }

                // ｽﾄｯﾌﾟﾎﾞﾀﾝ LED 状態 ｾｯﾄ
                // *   ｽﾄｯﾌﾟﾎﾞﾀﾝ 作動 ﾋﾞｯﾄ 反転 (停止対象を'0')
                // *   現在の状態を加味して更新
                setA((~getC() & getHLm()));
                setHLm(getA());

                // ｽﾄｯﾌﾟﾎﾞﾀﾝ 作動状態 ｾｯﾄ
                // ( DfOHHB_V23_DEF.DEF_STOPBIT に ｾｯﾄ するため上位 4ﾋﾞｯﾄ へ )
                setA((getA() << 4) | getC());
                // [ DfOHHB_V23_DEF.DEF_STOPBIT ] ｽﾄｯﾌﾟﾎﾞﾀﾝ 作動状態 格納領域
                mINC_HL(1);
                setHLm(getA());

                // [ DfOHHB_V23_DEF.DEF_PUSHCTR ] ｽﾄｯﾌﾟﾎﾞﾀﾝ 作動 ｶｳﾝﾀｰ 格納領域
                // ｽﾄｯﾌﾟﾎﾞﾀﾝ 作動 ｶｳﾝﾀｰ を更新
                mINC_HL(1);
                setHLm(getHLm() - 1);

                ushort R = (ushort)(clRAND8.mGetRnd8() & 0x7F);
                //デバッグフラグは立っているか？
                if ((getWork(Defines.DEF_GAMEST) & (0x01 << Defines.DEF_STOPRND_FLN)) == 0)
                {//立っていないの通常処理。
                    setA(R);
                    mLD_Nm_A(Defines.DEF_STOPRND);
                }//else フラグが立っていたので既にセットされているDfOHHB_V23_DEF.DEF_STOPRNDを使用。

                ushort tmpIX = (ushort)(getIX() + (Defines.DEF_CCCPIC));

                mLD_A_Nm(tmpIX);

                mLD_Nm_A(Defines.DEF_STP_INPT);

                // 使用 ｽﾃｰﾀｽ ﾚｼﾞｽﾀ 格納処理
                mSTSB_STRE_00();
                // 回胴停止選択処理（Aﾚｼﾞｽﾀｰに滑り値を格納）

                mSTOP_SSEL_00();

                ushort tmpA = getA();
                mLD_A_Nm(tmpIX);
                setA(getA() + tmpA);

                if (getA() >= 21)
                {
                    setA(getA() - 21);
                }

                tmpIX = (ushort)(getIX() + Defines.DEF_CCCPOS);
                mLD_Nm_A(tmpIX);
                mSTSB_ARYS_00();
                mSTOP_RECH_00();
                mSTOP_SCHK_00();
            }
        }
    }

    public void mInitializaion(int n)
    {
        clRAND8.mInitializaion(n);
        clearWork(Defines.DEF_CLR_AREA_1);
    }

    public void mRandomRefresh()
    {
        mSB_RRND_00();
    }

    public int mRandomX()
    {
        mMN_PRND_00();
        return getWork16(Defines.DEF_RANDOMX);
    }

    public void mReelStart(int random, int medal)
    {
        setWork(Defines.DEF_MEDLCTR, (ushort)medal);

        ushort[] MLAMPTBL ={           // ﾒﾀﾞﾙﾗｲﾝﾗﾝﾌﾟの点灯ﾊﾟﾀｰﾝﾃｰﾌﾞﾙ
             0x08,                    // 1ﾒﾀﾞﾙ投入時(ﾒﾀﾞﾙﾗｲﾝﾗﾝﾌﾟ1点灯)
             0x38,                    // 2ﾒﾀﾞﾙ投入時(ﾒﾀﾞﾙﾗｲﾝﾗﾝﾌﾟ1､2A､2B点灯)
             0xF8,                    // 3ﾒﾀﾞﾙ投入時(全ﾒﾀﾞﾙﾗｲﾝﾗﾝﾌﾟ点灯)
		};
        mLD_A_Nm(Defines.DEF_MEDLCTR);
        if (getA() > 0)
        {
            setA(MLAMPTBL[getA() - 1]);
            mLD_Nm_A(Defines.DEF_MLAMPST);
        }
        setWork((int)Defines.DEF_RANDOMX, (ushort)random);

        mMN_GLCK_00();
        mMN_WCAL_00();
        mMN_SDAT_00();

        setHL(Defines.DEF_ARAY11);
        setBC(15 * 256 + Defines.DEF_ARAY);

        while (true)
        {
            setHLm(getC());
            mINC_HL(1);
            if (mDJNZ() == true) break;
        }

    }

    public void mSetForceFlag(Defines.ForceYakuFlag flagIndex)
    {
        Defines.RAM_TRACE("mSetForceFlag:[" + Defines.DEF_FORCE_FLAG + "]" + flagIndex);
        setWork(Defines.DEF_FORCE_FLAG, (ushort)((int)flagIndex & (Enum.GetNames(typeof(Defines.ForceYakuFlag)).Length - 1)));
    }

    public ushort mReelStop(int reel, ushort pos)
    {
        ushort[] REEL_PIC = { (Defines.DEF_RCB_PIC + 0), (Defines.DEF_RCB_PIC + 1), (Defines.DEF_RCB_PIC + 2) };
        ushort[] REEL_STP = { (Defines.DEF_RCB_POS + 0), (Defines.DEF_RCB_POS + 1), (Defines.DEF_RCB_POS + 2) };
        ushort bit = 0x01;
        int i = reel;

        for (int j = 0; j < i; j++) bit = (ushort)(bit << 1);
        //有効なボタンが押されたよ！
        setWork(Defines.DEF_INBUFF0, bit);
        //その時のリール位置
        setWork(REEL_PIC[i], pos);

        mMN_STOP_00();

        return getWork(REEL_STP[i]);
    }

    public void mSetDebugStopRnd(int random)
    {
        setHL(Defines.DEF_GAMEST);
        setHLm(getHLm() | (0x01 << Defines.DEF_STOPRND_FLN));
        setWork(Defines.DEF_STOPRND, (ushort)(random & 0xFF));
    }

    public ushort mPayMedal()
    {
        ushort ret = 0xFFFF;

        setA(0);
        mLD_Nm_A(Defines.DEF_HITFLAG);
        mLD_Nm_A(Defines.DEF_HITSND);
        mLD_Nm_A(Defines.DEF_HITCTR);
        mLD_Nm_A(Defines.DEF_HITLINE);

        mMN_CKLN_00();

        if (mMN_ILCK_00() == false)
        {
            ret = getWork(Defines.DEF_HITCTR);
        }

        if (ret > 15)
        {
            ret = 0;
        }

        return ret;
    }

    public ushort mBonusCounter()
    {
        ushort ret = 0;
        mLD_A_Nm(Defines.DEF_GMLVSTS);

        if ((getA() & 0x03) != 0)
        {	// ボーナス限定処理
            if (mMN_GCCK_00() == true)
            {
                clearWork(Defines.DEF_CLR_AREA_2);
                ret = Defines.DEF_BBEND_FLX;
            }
            else
            {
                return ret;
            }
        }

        mMN_GMIT_00();

        return ret;
    }

    public void loadRAM()
    {
        int i;
        int j;
        sbyte[] ramData;

        ramData = SlotInterfaceResource.getResourceData("ClZ80RAM_big.dat");
        mDataTable = new ushort[ramData.Length / 2];

        for (i = 0, j = 0; i < ramData.Length; i += 2, j++)
        {
            mDataTable[j] = (ushort)((ramData[i] << 8) & 0xFF);
            mDataTable[j] |= (ushort)(ramData[i + 1] & 0xFF);
        }
    }
};

//*******************************************************
//	【C++言語ソース　→　JAVAソース出力】
//		このファイルはZ80C2J.exeで出力されています。
//*******************************************************



/////////////////////////////////////////
//QTのZ80メイン関連クラス。
/////////////////////////////////////////
using System;
public class clOHHB_V23 : clZ80RAM {

	//========================================================================================
	//== (6-14)SB_DTST_00 データセット処理
	//========================================================================================
	//
	//入力パラメータ
	//            HL ﾚｼﾞｽﾀ  :  ｱﾄﾞﾚｽ ﾃﾞｰﾀ ﾃｰﾌﾞﾙ 番地
	//
	//出力パラメータ
	//            HL ﾚｼﾞｽﾀ  :  新 ﾃﾞｰﾀ ﾃｰﾌﾞﾙ 番地
	//
	//使用レジスタ
	//            DE ﾚｼﾞｽﾀ  :  汎用
	//            HL        :  汎用
	//
	//========================================================================================
	static void mSB_DTST_00(int mode)
	{
		//この分岐処理を何とかせねば・・・。
        // TODO C#移植 呼ぶ時に0しか使ってない
		if(mode == 0)
		{
			
			setE(getHLt());
			mINC_HL(1);
			
			setD(getHLt());
            setDE(getDE() - Defines.DEF_TOPADRS);
		}else{
			setE(getHLm());
			mINC_HL(1);
			setD(getHLm());
            setDE(getDE() - Defines.DEF_TOPADRS);
		}
		mEX_DE_HL();
	}
	//========================================================================================
	//== (6-15)SB_BNUM_00 番号化処理
	//========================================================================================
	static ushort mSB_BNUM_00()
	{
		//RLCAでキャリーを拾うためキャストしておく。(  charは２バイト)
        ushort tmpA = getA();

		setB(8);

		//DJNZを実現するため妙なfor文にしておく
	//	for(int Breg=8; Breg==0; Breg--)
		while(true)
		{
			//1ビットづつ左へシフト。
			//RLCA

	         //RLCA                        ; '1' の ﾋﾞｯﾄ を探す
			//JR      C,SB_BNUM_02
            tmpA = (ushort)(tmpA << 1);
			if(tmpA >= 0x0100) break;
			if( mDJNZ() )break;
		}
		//キャリーの見つかった番号を返す。
		//恐らく
		//8 7 6 5 4 3 2 1	(番号化)
		//0 0 0 0 0 0 0 1	(対象ビット)
		//	(0ビット目から1,2,3,4,5,6,7,8)
		setA( getB() );

		return getB();
	}
	//========================================================================================
	//== (6-06)SB_ADRA_00 番地加算処理
	//========================================================================================
    static ushort mSB_ADRA_00(int Areg)
	{
        //	  ushort Areg = getA();
        ushort tmpHL = getHL();
		Defines.RAM_TRACE("mSB_ADRA_00:" + (tmpHL & 0xFFFF) + " Areg:" + Areg);
		//2162+14
		setHL(tmpHL + Areg);
		
		setA( getHLt() );
		return getA();
	}
	//========================================================================================
	//== (6-08)SB_DIVD_00 割算処理
	//========================================================================================
	//
	//入力パラメータ
	//            A  ﾚｼﾞｽﾀ  :  割られる数
	//            D         :  割る数
	//
	//出力パラメータ
	//            A  ﾚｼﾞｽﾀ  :  余り
	//            D         :  割る数
	//            E         :  商
	//            ZR ﾌﾗｸﾞ   :  [ ｾｯﾄ  ] 商 ＝ 0 ｽﾃｰﾀｽ
	//                      :  [ ﾘｾｯﾄ ] 商 ≠ 0 ｽﾃｰﾀｽ
	//========================================================================================
	static void mSB_DIVD_00()
	{
/*
		setE(0xFF);
		while(true)
		{
			setE(getE()+1);
			if( (getA() - getD() ) < 0 )
			{
				setA( getA() );
				setE(getE()+1);
				setE(getE()-1);
				break;

			}else setA(getA() - getD());		
		}
*/	
		setE(0xFF);
		while(true)
		{
			
			setE(getE()+1);		
			int SUB = (int)(getA() - getD());
			
			if( SUB < 0 )
			{
				setA( SUB + getD() );
				break;
			}else setA(SUB);
		}

	}
	//========================================================================================
	//== (6-13)SB_DDEC_00 減算処理
	//========================================================================================
	static bool mSB_DDEC_00(int mode)
	{
		bool ret = true;
		if(mode==0)
		{
			if(getHLm()==0)ret = false;
			else{
				setHLm(getHLm() - 1);
				if(getHLm()==0)ret = false;
			}
		}else{
			
			if(getHLt()==0)ret = false;		
		}
		return ret;
	}
	//========================================================================================
	//== (5-05)_STSB_GPCX_00 図柄位置設定処理
	//========================================================================================
	//
	//入力パラメータ
	//            HL ﾚｼﾞｽﾀ  :  回胴 ﾃｰﾌﾞﾙ ､ 停止 ﾃｰﾌﾞﾙ
	//            IX        :  回胴制御 ﾌﾞﾛｯｸ 番地 ( 当該 ﾘｰﾙ の ﾘｰﾙｽﾃｰﾀｽ 格納先 )
	//
	//出力パラメータ
	//            A  ﾚｼﾞｽﾀ  :  図柄位置 ｶｳﾝﾀｰ
	//
	//破壊レジスタ
	//            A  ﾚｼﾞｽﾀ
	//
	static void mSTSB_GPCX_00(int mode)
	{
        ushort tmpIX = getIX();
        mLD_A_Nm((ushort)(tmpIX + Defines.DEF_CCCPIC));

		if(mode == 0)
		{
			mSTSB_GPCY_00();
		}else{
			if(getA() >= 21 )
				setA( getA() - 21 );
		
			setE(getA());
			setHL(getHL() + getA() );	
			setA( getHLm() );		
		}	
	}
	//========================================================================================
	//== (5-06)_STSB_GPCY_00 図柄位置検索処理
	//========================================================================================
	//
	//入力パラメータ
	//            A  ﾚｼﾞｽﾀ  :  図柄位置 ｶｳﾝﾀｰ
	//            HL        :  図柄配置 ﾃｰﾌﾞﾙ ､ 停止 ﾃｰﾌﾞﾙ
	//
	//出力パラメータ
	//            A  ﾚｼﾞｽﾀ  :  図柄位置の ﾃﾞｰﾀ
	//            E         :  図柄位置 ｶｳﾝﾀｰ
	//            HL        :  図柄配置 ﾃｰﾌﾞﾙ ､ 停止 ﾃｰﾌﾞﾙ
	//
	//破壊レジスタ
	//            A ､ E ､ HL ﾚｼﾞｽﾀ
	//
	//========================================================================================
	static void mSTSB_GPCY_00()
	{
		if(getA() >= 21 )
			setA( getA() - 21 );
		
//		setE(getA());
		mSB_ADRA_00(getA());
	}
	//========================================================================================
	//== (5-07)_SSEL_TPNT_00 停止テーブル検索処理
	//========================================================================================
	//
	//入力パラメータ
	//            A  ﾚｼﾞｽﾀ  :  ﾃｰﾌﾞﾙ 選択用 ｶｳﾝﾀｰ
	//            HL        :  停止 ﾃｰﾌﾞﾙ 番地
	//
	//出力パラメータ
	//            HL ﾚｼﾞｽﾀ  :  停止 ﾃｰﾌﾞﾙ 番地
	//
	//使用レジスタ
	//            A  ﾚｼﾞｽﾀ  :  ﾃｰﾌﾞﾙ 選択用 ｶｳﾝﾀｰ
	//            B         :  汎用
	//            DE        :  ﾃｰﾌﾞﾙ 長
	//            HL        :  停止 ﾃｰﾌﾞﾙ 番地
	//
	//========================================================================================
	static void mSSEL_TPNT_00()
	{
		setB(getA());
		setDE(21);
		while(true)
		{
			setHL(getHL() + getDE() );
			if( mDJNZ() == true)break;			
		}
	}
	//========================================================================================
	//== (5-08)_SSEL_SPNT_00 停止位置検索処理
	//========================================================================================
	//
	//入力パラメータ
	//            B  ﾚｼﾞｽﾀ  :  検索回数
	//            C  ﾚｼﾞｽﾀ  :  ﾏｽｸﾃﾞｰﾀ
	//            HL        :  停止 ﾃｰﾌﾞﾙ 番地
	//
	//出力パラメータ
	//            A  ﾚｼﾞｽﾀ  :  滑り駒数
	//            ZR ﾌﾗｸﾞ   :  [ ｾｯﾄ  ] 即停止要求あり ｽﾃｰﾀｽ
	//                         [ ﾘｾｯﾄ ] 即停止要求なし ｽﾃｰﾀｽ
	//
	//使用レジスタ
	//            全 ﾚｼﾞｽﾀ  :  汎用
	//
	//========================================================================================
	static bool mSSEL_SPNT_00()
	{
//_SSEL_SPNT_00:
		setD(getB());
        setE(getWork((ushort)(getIX() + Defines.DEF_CCCPIC)));
//_SSEL_SPNT_01:
		while(true)
		{
			setA(getE());
            ushort pushHL = getHL();
			mSTSB_GPCY_00();
			setHL(pushHL);
			setA(getA() & getC());
			if(getA() > 0 )break;

			setE(getE()+1);
			if( mDJNZ() == true)break;
		}
//_SSEL_SPNT_02:
		setA(getD());
		setA(getA() - getB());
		
		bool ret = true;
		if(getA() != 0 )ret = false;

		return ret;
	}
	//========================================================================================
	//== (5-09)_SCHK_SCHG_00 回胴停止後の変更処理
	//========================================================================================
	//
	//入力パラメータ
	//            C  ﾚｼﾞｽﾀ  :  当たり要求 ﾌﾗｸﾞ
	//            D         :  ｽﾄｯﾌﾟ ﾎﾞﾀﾝ 作動状態
	//            E         :  ﾒﾀﾞﾙ ｶｳﾝﾀｰ
	//
	//出力パラメータ
	//            なし
	//
	//使用レジスタ
	//            全 ﾚｼﾞｽﾀ  :  汎用
	//
	//========================================================================================
	static void mSCHK_SCHG_00()
	{	
		bool ret = false;

//_SCHK_SCHG_00:
        setHL(Defines.DEF_PCHGTB1);			
		setA(getE());
		setA(getA()-1);
		if(getA()!=0)
		{
            setA(getC() & Defines.DEF_FRUITFLG);
			if(getA()==0)
			{	
				setA(getD());
				if(getA()==0x61)
				{
                    ushort pushDE = getDE();
                      setHL(Defines.DEF_PCHGTB5 + Defines.DEF_PCTCKNUM2);
                    setB(Defines.DEF_PCTCKNUM);
					setDE(-8);
					setA(getC());
//_SCHK_SCHG_01:
                    ushort RLCA = getA();
					while(true)
					{
						//マイナスになるか少し心配だ・・・。
						setHL(getHL()+getDE());
                        RLCA = (ushort)(RLCA << 1);
						if( (RLCA & 0xFF00) > 0 ) break;
						if( mDJNZ() == true)break;
					}
//_SCHK_SCHG_02:
					setDE(pushDE);
                    mLD_A_Nm(Defines.DEF_STOPRND);

/*
					//デバッグモード時は乱数値は取らず、BET数で固定とする。
					if( ( getWork(DfOHHB_V23_DEF.DEF_GAMEST) & (0x01<<DfOHHB_V23_DEF.DEF_STOPRND_FLN) ) > 0)
					{
						setA(getE());
					}
*/
					setA(getA() & getE());
					setA(getA() & 0x03);
					setA(getA()*2);				
					mSB_ADRA_00(getA());
					mSB_DTST_00(0);
//					mSTSB_GPCX_00(0);

                    mLD_A_Nm((ushort)(getIX() + Defines.DEF_CCCPOS));
					mSTSB_GPCY_00();

					mSTSB_LPN3_00();	
					ret = true;
				}
			}
//_SCHK_SCHG_03:
			if(ret == false)
				setHL(Defines.DEF_PCHGTB2);
		}
//_SCHK_SCHG_04:
		if(ret == false)
		{
			while(true)
			{
                //Defines.TRACE("@@@getHLt():" + getHLt());
				setA(getHLt());
				if(getA()!=0xFF)
				{
					mINC_HL(1);
					if(getA() == getD())
					{
//※Z80の強引なジャンプにより位置を変更
//_SCHK_SCHG_06:
						
						setA(getHLt() & getC());
						mINC_HL(1);
						if(getA()!=0)
						{
							mLD_A_Nm(Defines.DEF_GMLVSTS);
							
							setA(getA() & getHLt());
							if(getA()!=0)
							{
								mINC_HL(1);
								mSB_DTST_00(0);
								break;
							}
						}
					}else  
						mINC_HL(1);
//_SCHK_SCHG_05:
					mINC_HL(3);
				}else {
					ret = true;			
					break;
				}
			}
//_SCHK_SCHG_07:
			if(ret == false)
			{
				while(true)
				{
					mINC_HL(1);
					
					setA(getHLt()+1);
					if(getA()!=0)
					{
						setA(getA()+1);
						if(getA()==0)
						{
							mINC_HL(1);
							
							setB(getHLt());			
							mINC_HL(1);
						}
					}else {
						ret =true;
						break;
					}
//_SCHK_SCHG_08:
					
					setA(getHLt());
					setD(21);
					mSB_DIVD_00();
					//Z80ではDfOHHB_V23_DEF.DEF_CCCPIC（ストップ位置）がそのまま最終位置になるため
					//アプリでは使用していないワークを利用する（DfOHHB_V23_DEF.DEF_CCCPOS）			
					//アプリ専用処理！！
//					if( getA() == getWork(getIX() + (DfOHHB_V23_DEF.DEF_CCCPIC)) ) break;
                    if (getA() == getWork((ushort)(getIX() + (Defines.DEF_CCCPOS)))) break;
				}
//
//=== 停止 ﾃｰﾌﾞﾙﾗｲﾝ 変更処理
//
				if(ret == false)
				{

                    setHL(Defines.DEF_CHGLINE);
					setA(getE());
					setA(getA() * 2);
					setA(getA() + getE());
					mSB_ADRA_00(getA());
					setD(getA());
					setA(128-getD());
					setE(getA());
					mINC_HL(1);

                    mLD_A_Nm(Defines.DEF_GMLVSTS);
					setA(getA() & 0x07);
					if(getA()==0)
					{
						mINC_HL(1);
						setD(getE());				
					}
//_SCHK_SCHG_09:
                    mLD_A_Nm(Defines.DEF_MEDLCTR);

					bool CY = true;
					if(getA() != 2)
					{
                        mLD_A_Nm(Defines.DEF_STOPRND);
						if( getA() >= getD() ) CY = false; 
					}
//_SCHK_SCHG_10:
					//※PUSH AFはキャリーフラグの保存なので記述しない。
					
					setA(getHLt());
					setD(16);
					mSB_DIVD_00();
					setH(getE());
					setL(getA());
					//PUSHがないので当然POPもしない。
					if(CY == false)
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
	//========================================================================================
	//== (4-06)MN_CKLN_00 入賞作動検索処理
	//========================================================================================
	//
	//入力パラメータ
	//            なし
	//
	//出力パラメータ
	//            なし
	//
	//使用レジスタ
	//            HL ﾚｼﾞｽﾀ  :  RAM ｱﾄﾞﾚｽ
	//            DE        :  組合せ ﾃｰﾌﾞﾙ
	//            B         :  ﾘｰﾙ 数
	//            C         :  ﾗｲﾝ ｽﾃｰﾀｽ
	//            A         :  汎用
	//
	//========================================================================================
    static ushort MN_CKLN;
	static void mMN_CKLN_00()
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
		while(true)
		{
			if(MN_CKLN == 0x00)
			{
				mLD_A_Nm( Defines.DEF_MLAMPST );
				setA(getA() & getC() );
				if(getA()==0)break;

				setHL( Defines.DEF_COMPTBL );
			}

			MN_CKLN = 0x00;
//
//=== 検索制御 ｺｰﾄﾞ の ﾁｪｯｸ
//
//MN_CKLN_02:
			
			setA(getHLt());			
			if(getA()!=0)
			{
				pushDE = getDE();
				pushHL = getHL();
			
				mLD_A_Nm( Defines.DEF_GMLVSTS );
				
				setA(getA() & getHLt());
				if(getA()==0) 
				{
					MN_CKLN = 0x07;
//					break;
				}else{
					mINC_HL(1);
					
					setA(getHLt() & getC());
					if(getA()==0) 
					{
						MN_CKLN = 0x07;
//						break;
					}	
				}
//
//=== 当たり判定
//
				if(MN_CKLN != 0x07)
				{
					mEX_DE_HL();
					setB(Defines.DEF_REELNUM);
//MN_CKLN_03:
					while(true)
					{
						mINC_DE(1);
						setA(getDEt());
						if(getA()!=0)
						{
							setA(getA() & getHLm())	;
							if(getA()==0)
							{
								MN_CKLN = 0x07;
								break;
							}
						}
//MN_CKLN_04:
						mINC_HL(1);
						if(mDJNZ()==true)break;
					}
//
//=== 当たり情報の ｾｯﾄ
//
					if(MN_CKLN != 0x07)
					{
						setHL(Defines.DEF_HITFLAG);
						mINC_DE(1);
						setA(getDEt());
						setA(getA() | getHLm());
						setHLm(getA());

						mINC_HL(1);
						mINC_DE(1);
						setA(getDEt());
						setA(getA()>>4);
						setA(getA() & 0x0F);

						mINC_HL(1);
						setA(getDEt());
						setA(getA() & 0x0F);
						setA(getA() + getHLm());
						if(getA() >= (Defines.DEF_PAYMAX+1) )
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
			}else MN_CKLN = 0x00;
//MN_CKLN_06:
			if(MN_CKLN != 0x07)
			{

				mINC_DE(3);
				setC(getC()<<1);
				MN_CKLN = 0x00;
			}
//MN_CKLN_07:
			if(MN_CKLN == 0x07)
			{
				setHL(pushHL);
				setDE( Defines.DEF_CMPTLNG );
				setHL(getHL()+getDE() );
				setDE(pushDE);
			}
		}
	}
	//========================================================================================
	//== (5-11)_STSB_STRE_00 使用ステータスレジスタ格納処理
	//========================================================================================
	//
	//入力パラメータ
	//            なし
	//
	//出力パラメータ
	//            B  ﾚｼﾞｽﾀ  :  遊技状態 ｽﾃｰﾀｽ
	//            C         :  当たり要求 ﾌﾗｸﾞ
	//            D         :  ｽﾄｯﾌﾟﾎﾞﾀﾝ 作動状態
	//            E         :  ﾒﾀﾞﾙ ｶｳﾝﾀｰ
	//
	//========================================================================================
	static void mSTSB_STRE_00()
	{
        setE(getWork(Defines.DEF_MEDLCTR));
        setD(getWork(Defines.DEF_STOPBIT));
        setB(getWork(Defines.DEF_GMLVSTS));
        setC(getWork(Defines.DEF_WAVEBIT));
	}
	//========================================================================================
	//== (5-02)_STOP_SSEL_00 回胴停止選択処理
	//========================================================================================
	//
	//入力パラメータ
	//            B  ﾚｼﾞｽﾀ  :  遊技状態 ｽﾃｰﾀｽ
	//            D         :  ｽﾄｯﾌﾟ ﾎﾞﾀﾝ 作動状態
	//            E         :  ﾒﾀﾞﾙ ｶｳﾝﾀｰ
	//
	//出力パラメータ
	//            なし
	//
	//使用レジスタ
	//            A  ﾚｼﾞｽﾀ  :  汎用
	//            B         :  汎用
	//            D         :  汎用
	//            HL        :  停止 ﾃﾞｰﾀ ﾃｰﾌﾞﾙ 番地
	//
	//========================================================================================
	static void mSTOP_SSEL_00()
	{
//_STOP_SSEL_00:
		int _STOP_SSEL = 0x02;
		
		// 使用 ｽﾃｰﾀｽ ﾚｼﾞｽﾀ 格納処理
		mSTSB_STRE_00();
	
		setA(getB() & 0x01 );
		if(getA() == 0)
		{
			setA(getD());
			if(getA() == 0x61)
			{
//
//=== 1st 第一停止時
//
				setA(getE());
				setA(getA()-1);
				if(getA() != 0)
				{
					// ﾗｲﾝ ﾏｽｸ ﾃﾞｰﾀ 変更処理 2
					mSTSB_LPN2_00();
					setHL(Defines.DEF_P1STTBL-21);
					mLD_A_Nm(Defines.DEF_FLGCTR);
					if( getA() >= Defines.DEF_BNSFLGC )
					{
						setA( 0 );
					}
//_STOP_SSEL_01:
					setA(getA()+1);
					_STOP_SSEL = 0x04;
				}
			}
		
		}
		if(_STOP_SSEL == 0x02)
		{
//_STOP_SSEL_02:
            mLD_A_Nm(Defines.DEF_LINENUM);
			mSTSB_LPN1_00();

            setHL(Defines.DEF_ST101TBL - 21);
            mLD_A_Nm(Defines.DEF_STOPBIT);
            ushort RRCA = (ushort)(getA() << 8);
            RRCA = (ushort)(RRCA >> 1);
			if( (RRCA & 0x00FF) == 0)
			{
                setHL(Defines.DEF_ST201TBL - 21);
                RRCA = (ushort)(RRCA >> 1);
				if( (RRCA & 0x00FF) == 0)
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
	//========================================================================================
	//== (5-03)_STOP_RECH_00 回胴停止後の設定処理
	//========================================================================================
	//
	//入力パラメータ
	//            なし
	//
	//出力パラメータ
	//            なし
	//
	//使用レジスタ
	//            全 ﾚｼﾞｽﾀ  :  汎用
	//
	//========================================================================================
	static void mSTOP_RECH_00()
	{
//_STOP_RECH_00:
		mLD_A_Nm(Defines.DEF_PUSHCTR);
		setA(getA()-1);
		if(getA() == 0)
		{
			mLD_A_Nm(Defines.DEF_GMLVSTS);
			setA(getA() & 0x03);
			if(getA() == 0)
			{	
				setD(getA());
				mLD_A_Nm(Defines.DEF_MEDLCTR);
				setA((getA()*2)-1);
				setB(getA());
				setHL(Defines.DEF_ARAY11-1);
//_STOP_RECH_01:
				while(true)
				{
					mINC_HL(1);
					setA(getHLm());
					mINC_HL(1);
					setA(getA() & getHLm() );
					mINC_HL(1);
					setA(getA() & getHLm() );
					setA(getA() & Defines.DEF_REACHDAT );
					if(getA() != 0)
					{
						setD(getD()+1);
					}
//_STOP_RECH_02:
					if(mDJNZ() == true)break;					
				}
				setA(getD());
				if(getA() != 0)
				{
/*
					setHL(DfOHHB_V23_DEF.DEF_RCH_TBL-1);
					mSB_ADRA_00(getA());
*/
					//実機ではここでBB入賞期待音とチャンスLEDを設定していますが、
					//アプリでは現状使用しませんのでここまでしておきます。
					//この場所の情報をアプリ用ワークでフラグ化すれば、アプリ側でも
					//このタイミングは拾えるようになります。（要相談）

				}
			}
		}
	}
	//========================================================================================
	//== (5-04)_STOP_SCHK_00 制御変更チェック処理
	//========================================================================================
	//
	//入力パラメータ
	//            なし
	//
	//出力パラメータ
	//            なし
	//
	//使用レジスタ
	//            A  ﾚｼﾞｽﾀ  :  汎用
	//
	//========================================================================================
	static void mSTOP_SCHK_00()
	{
		mLD_A_Nm(Defines.DEF_PUSHCTR);
		if(getA()!=0)
		{
			mLD_A_Nm(Defines.DEF_GMLVSTS);
			setA(getA()-1);
			if(getA()!=0)
			{
				// 使用 ｽﾃｰﾀｽ ﾚｼﾞｽﾀ 格納処理
				mSTSB_STRE_00();
				// 回胴停止後の変更処理
				mSCHK_SCHG_00();
			}
		}
	}
	//========================================================================================
	//== (5-12)_STSB_ARYS_00 図柄コード格納処理
	//========================================================================================
	static void mSTSB_ARYS_00()
	{
//_STSB_ARYS_00:
        setDE(Defines.DEF_STOPBIT);
		setA(getDEm() & 0x07);
		mSB_BNUM_00();
	
		//Z80では最終停止位置がDfOHHB_V23_DEF.DEF_RCB_PICに格納されているが
		//アプリではDfOHHB_V23_DEF.DEF_RCB_POSに格納しているため処理を変更。
        setHL(Defines.DEF_RCB_POS - 1);

		//何とかせねば・・・。
//		mSB_ADRA_00(getA());
		setHL(getHL() + getA());	
		setA( getHLm() );		
		
		setC(getA());
		setB(5);
		setA(getDEm());

        ushort RRCA = (ushort)(getA() << 8);

        RRCA = (ushort)(RRCA >> 1);
		setDE(Defines.DEF_ARAY51);
		setH(0);	
		if( (RRCA & 0x00FF) == 0)
		{

            RRCA = (ushort)(RRCA >> 1);
			mINC_DE(1);
			setH(getB());
			if( (RRCA & 0x00FF) == 0)
			{
				mINC_DE(1);
				setH(10);
			}
		}
//_STSB_ARYS_01:
		
		setA(getC());			
		setC(getH());
		setHL(Defines.DEF_REELTB1);
		mSTSB_GPCY_00();

//_STSB_ARYS_02:
		while(true)
		{
            ushort pushHL = getHL();
			setHL(Defines.DEF_ARSETTBL-1);
			setA(getB() + getC());
			mSB_ADRA_00(getA());
			setHL(pushHL);
			mSB_ADRA_00(getA());
			setDEm(getA());
			mDEC_DE(3);
			setHL(pushHL);
			if( mDJNZ() == true)break;
		}	
	}
	//========================================================================================
	//== (5-13)_STSB_LPN1_00 ラインマスクデータ変更処理１
	//========================================================================================
	//
	//入力パラメータ
	//            A  ﾚｼﾞｽﾀ  :  ﾗｲﾝ ﾃﾞｰﾀ [ 0 - 7 ]
	//
	//出力パラメータ
	//            C  ﾚｼﾞｽﾀ  :  ﾏｽｸ ﾃﾞｰﾀ
	//
	//使用レジスタ
	//            HL ﾚｼﾞｽﾀ  :  停止 ﾃﾞｰﾀ 有効 ﾋﾞｯﾄ ﾃｰﾌﾞﾙ 番地
	//
	//========================================================================================
	static void mSTSB_LPN1_00()
	{
		setHL(Defines.DEF_LINEDAT1);	
		mSB_ADRA_00(getA());
		setC(getA());
	}
	//========================================================================================
	//== (5-14)_STSB_LPN2_00 ラインマスクデータ変更処理２
	//========================================================================================
	//
	//入力パラメータ
	//            B  ﾚｼﾞｽﾀ  :  遊技状態 ｽﾃｰﾀｽ
	//            C         :  当たり要求 ﾌﾗｸﾞ
	//
	//出力パラメータ
	//            C  ﾚｼﾞｽﾀ  :  第一回胴第一停止用 ﾏｽｸﾃﾞｰﾀ
	//
	//使用レジスタ
	//            全 ﾚｼﾞｽﾀ  :  汎用
	//
	//========================================================================================
	static void mSTSB_LPN2_00()
	{
//_STSB_LPN2_00:
		setA(getC() & Defines.DEF_FRUITFLG2);
		if(getA()==0)
		{
			setA(getA() | 0x80);
		}
//_STSB_LPN2_01:
		setC(getA());
		setHL(Defines.DEF_FRSTTBL-8);
		setA(getB());
		mSB_BNUM_00();
		setA(getA()*4);	
		mSB_ADRA_00(getA());
		mSB_DTST_00(0);
		mSTSB_GPCX_00(0);		
		setA( getA() & getC() );
		mINC_DE(1);
		if(getA() != 0)
		{
			mINC_DE(1);
		}
//_STSB_LPN2_02:
		setA(getDEt());
		setC(getA());	
	}
	//========================================================================================
	//== (5-15)_STSB_LPN3_00 ラインマスクデータ変更処理３
	//========================================================================================
	//
	//入力パラメータ
	//            A  ﾚｼﾞｽﾀ  :  停止後変更 ｽﾄｯﾌﾟ ﾃﾞｰﾀ
	//
	//出力パラメータ
	//            なし
	//
	//使用レジスタ
	//            A  ﾚｼﾞｽﾀ  :  汎用
	//            D         :  割る数
	//            E         :  ｽﾄｯﾌﾟ 用 ﾃｰﾌﾞﾙ ﾃﾞｰﾀ
	//            HL        :  ｽﾄｯﾌﾟ 用 ﾗｲﾝ ﾃﾞｰﾀ、ｽﾄｯﾌﾟ 用 ﾃｰﾌﾞﾙ ﾃﾞｰﾀ 
	//
	//========================================================================================
	static void mSTSB_LPN3_00()
	{
//_STSB_LPN3_00:

		setD(8);
		mSB_DIVD_00();
		setH(getA());
		setL(getE());
        mLD_Nm_HL(Defines.DEF_TBLNUM);
	}
	//========================================================================================
	//== (4-11)MN_ILCK_00 イリーガルチェック処理
	//========================================================================================
	static bool mMN_ILCK_00()
	{
#if __ERR_MSG__
		String errStr = "";
#endif
		bool ret = false;
        mLD_A_Nm(Defines.DEF_WAVEBIT);
        setHL(Defines.DEF_HITFLAG);

		setA( (getA() ^ getHLm()) & getHLm() );
		if( getA() != 0  )
		{
//
//=== ｲﾘｰｶﾞﾙｴﾗｰ の表示
//
#if __ERR_MSG__
			errStr =
			(getWork(DfOHHB_V23_DEF.DEF_HITFLAG) & 0xFFFF) + ":" +
			(getWork(DfOHHB_V23_DEF.DEF_HITSND) & 0xFFFF) + ":" +
			(getWork(DfOHHB_V23_DEF.DEF_HITCTR) & 0xFFFF) + ":" +
			(getWork(DfOHHB_V23_DEF.DEF_HITLINE) & 0xFFFF);
#endif
			setA(0);
			mLD_Nm_A( Defines.DEF_HITFLAG );
			mLD_Nm_A( Defines.DEF_HITSND  );
			mLD_Nm_A( Defines.DEF_HITCTR  );
			mLD_Nm_A( Defines.DEF_HITLINE );

//			printf("MN_ILCK_00 イリーガルエラー！！\n");
#if __ERR_MSG__
			TRACE("MN_ILCK_00 イリーガルエラー！！\n");
#endif
			ret = true; //エラー
		}
#if __ERR_MSG__
		SET_ERR_OPTION3(errStr);
#endif

		return ret;
	}
	//========================================================================================
	//== (4-12)MN_FSEL_00 回転表示器回転演出抽選処理
	//========================================================================================
	//
	//入力パラメータ
	//            C  ﾚｼﾞｽﾀ  :  ｽﾄｯﾌﾟ 用 ｾﾚｸﾄ ｶｳﾝﾀｰ
	//
	//出力パラメータ
	//            なし
	//
	//使用レジスタ
	//            全 ﾚｼﾞｽﾀ  :  汎用
	//
	static void mMN_FSEL_00()
	{
        ushort pushAF = 0;
		setAF(0);
//MN_FSEL_00:
		setB(0);	
		//アプリ用に変更！（4thリールのランプ点灯中は０以上にしてください。）メソッド作るか・・・。
		mLD_A_Nm(Defines.DEF_FOUT3);
		if(getA()==0)
		{
            ushort tmp;
			Defines.RAM_TRACE("抽選開始？");
			mLD_A_Nm(Defines.DEF_GMLVSTS);
			mSB_BNUM_00();
			setHL( Defines.DEF_RLSLTBL-1 );
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
			setHL( Defines.DEF_SELECTTBL );
			
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
			
			if( getA() < getHLt() )
			{
//=== 演出当選時
				Defines.RAM_TRACE("演出当選時");
				
				mINC_HL(1);			
				mEX_DE_HL();
				mLD_A_Nm(Defines.DEF_RANDOMY);
//MN_FSEL_01:
				while(true)
				{
					pushAF = getAF();
					setHL( Defines.DEF_AVGTBL );
					setA( getDEt() );
					setA( getA() & 0x3F );
					mSB_ADRA_00(getA());
					setAF(pushAF);
					
					setAF(getAF() - getHLt());
					if( (getAF() & 0xFF00 ) > 0) break;
					mINC_DE(2);
				}
//MN_FSEL_02:
				setHL( Defines.DEF_FLASH );
				setA(getDEt());
				setA(getA() & 0xC0 );
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
	//========================================================================================
	//== (6-27)SB_RTCK_00 ＲＴ遊技数チェック処理
	//========================================================================================
	static void mSB_RTCK_00()
	{
		//この機種はＲＴはありません。
	}
	//========================================================================================
	//== (4-08)MN_GCCK_00 ＢＢ、ＲＢ遊技数チェック処理
	//========================================================================================
	//
	//入力パラメータ
	//            A  ﾚｼﾞｽﾀ  ;  遊技状態 ｽﾃｰﾀｽ
	//
	//出力パラメータ
	//            ｷｬﾘｰﾌﾗｸﾞ  :  ＢＢ終了時の ﾗﾑ ｸﾘｱ ｽﾃｰﾀｽ
	//
	//使用レジスタ
	//            全 ﾚｼﾞｽﾀ  :  汎用
	//
	//========================================================================================
	static bool mMN_GCCK_00()
	{
		bool CY = false;
//MN_GCCK_00:
		
		
		//TRACE("ＢＢ、ＲＢ遊技数チェック処理:" + (getA()&0xFFFF));
		
		if( (getA() & 0x01) == 0)
		{
//
//=== ＢＢ遊技数チェック処理
//
			setHL(Defines.DEF_BBGMCTR);
			mSB_DDEC_00(0);
		
			mLD_A_Nm(Defines.DEF_HITFLAG);
			if( (getA() & (0x01<<Defines.DEF_RJAC_BITN)) > 0)
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
				setHLm(getHLm() | (0x01<<Defines.DEF_RBGC_FLN) );

				mOmatsuri.JacIn();
				return CY;
//				mLD_A_Nm(BIGGCTR);	
			}	

//
//=== BB 作動中の遊技数 ﾁｪｯｸ
//
//MN_GCCK_01:
			//TRACE("BB中チェック？" + (getHLm() & 0xFFFF));
			setA(getHLm());
			if(getA()!=0)
				return CY;
//
//=== 役物入賞チェック処理
//
//MN_GCCK_02:
		}
		bool jac_ctr_bool = false;
		mLD_A_Nm(Defines.DEF_HITFLAG);
		if(getA()!=0)
		{
			setHL(Defines.DEF_JAC_CTR);
			setHLm(getHLm()-1);
			if(getHLm() == 0)
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
			if( (mSB_DDEC_00(0) == true)&& (jac_ctr_bool==false))
			{
//				setA(0);
//				mLD_Nm_A(DfOHHB_V23_DEF.DEF_JAC_CTR);
				return CY;
			}else {
//
//=== ＲＢ終了時の処理
				//TRACE("RB or JAC 終了時の処理");
//MN_GCCK_04:
				setA(0);
				mLD_Nm_A(Defines.DEF_HITREQ);
				mLD_Nm_A(Defines.DEF_JACGAME);
				mLD_Nm_A(Defines.DEF_JAC_CTR);

				setHL(Defines.DEF_GAMEST);
				setHLm(getHLm() & ~(0x01<<Defines.DEF_RBGC_FLN) );
//
//=== 遊技復帰条件の ﾁｪｯｸ
//		
				if( (getHLm() & (0x1<<Defines.DEF_BBGC_FLN)) == 0)
				{
					CY = true;		
					return CY;
				}

			}
//		}

//MN_GCCK_05:
		//  残りJACIN可能回数
		
		setHL(Defines.DEF_BIGBCTR);
		setHLm(getHLm()-1);
		//TRACE("残りJACIN可能回数の処理:" + (getHLm()&0xFFFF));
		if(getHLm()!=0)
		{
			mLD_A_Nm(Defines.DEF_BBGMCTR);
			if(getA() > 0)
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
	static void mMN_GMIT_00()
	{
//MN_GMIT_00:
        ushort MN_GMIT = 0x00;
		mLD_A_Nm(Defines.DEF_HITFLAG);
		if( (getA()& 0x20) > 0) MN_GMIT = 0x01;
		if( (getA()& 0x10) > 0) MN_GMIT = 0x02;

		switch(MN_GMIT)
		{
		case 0x00:
			if( (getA()& 0x08) > 0)
			{
//
//=== 再遊技初期設定処理
//
				setHL(Defines.DEF_GAMEST);
				setHLm(getHLm() | (0x1<<Defines.DEF_RPLC_FLN));
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
//
//=== ＲＢ初期設定処理
//
			setA(0);
			mLD_Nm_A(Defines.DEF_HITREQ);
			mLD_Nm_A(Defines.DEF_FOUT3);
			setA(8);
			mLD_Nm_A(Defines.DEF_JAC_CTR);
			setA(12);
			mLD_Nm_A(Defines.DEF_JACGAME);
			setHL(Defines.DEF_GAMEST);
			setHLm(getHLm() | (0x01<<Defines.DEF_RBGC_FLN) );
			break;
		}
	}
	//========================================================================================
	//== (6-17)SB_RRND_00 乱数値更新処理
	//========================================================================================
	//
	//入力パラメータ
	//            なし
	//
	//出力パラメータ
	//            なし
	//	
	//破壊レジスタ
	//            なし
	//
	//========================================================================================
	static void mSB_RRND_00()
	{
	//大祭りではこの関数は使用しない。
/*
		  int pushHL = getHL();
		  int pushDE = getDE();
		
		mLD_HL_Nm(RANDOMA);
		setHL(getHL()+RNDPLUS);
		mLD_Nm_HL(RANDOMA);

		setHL(pushHL);
		setDE(pushDE);
*/
	}

	//========================================================================================
	//== (4-02)MN_PRND_00 乱数値抽出処理
	//========================================================================================
	//入力パラメータ
	//            なし
	//
	//出力パラメータ
	//            なし
	//
	//使用レジスタ
	//            HL ﾚｼﾞｽﾀ  :  汎用
	//            A         :  汎用
	//
	//========================================================================================
	static void mMN_PRND_00()
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
	////////////////////////////////////////////////////////////////
	//↓ユーザー公開メソッド
	////////////////////////////////////////////////////////////////

	//========================================================================================
	//== (4-03)MN_GLCK_00 遊技状態監視処理
	//========================================================================================
	//
	//入力パラメータ
	//            なし
	//
	//出力パラメータ
	//            A  ﾚｼﾞｽﾀ  :  遊技状態 ｽﾃｰﾀｽ
	//
	//使用レジスタ
	//            A   ﾚｼﾞｽﾀ :  内部当たり ﾌﾗｸﾞ
	//            HL        :  内部当たり ﾌﾗｸﾞ､遊技状態 ﾌﾗｸﾞ 格納領域
	//========================================================================================
	public static void mMN_GLCK_00()
	{
		setA( 0x01 );		
		setHL( Defines.DEF_GAMEST );		
		if( (getHLm() & (0x01<<Defines.DEF_RBGC_FLN)) == 0 )
		{
			setA( getA() *2 );		
			if( (getHLm() & (0x01<<Defines.DEF_BBGC_FLN)) == 0 )
			{
				setA( getA() *4 );		
				setHL( Defines.DEF_HITREQ );		
				if( (getHLm() & (0x01<<Defines.DEF_RBGL_BITN)) == 0 )
				{
					setA( getA() *2 );		
					if( (getHLm() & (0x01<<Defines.DEF_BBGL_BITN)) == 0 )
					{
						setA(0x04);
					}
				}				
			}	
		}		
		//アプリで追加。
		mLD_Nm_A(Defines.DEF_GMLVSTS);

		//アプリで追加。
		setHL( Defines.DEF_GAMEST );		
		setHLm( (getHLm() & ~(0x01<<Defines.DEF_RPLC_FLN)) );
		//デバッグ用フラグをクリア
		setHLm( (getHLm() & ~(0x01<<Defines.DEF_STOPRND_FLN)) );
	}	
	//========================================================================================
	//== (4-04)MN_WCAL_00 確率抽選処理
	//========================================================================================
	//
	//入力パラメータ
	//            なし
	//
	//出力パラメータ
	//            C  ﾚｼﾞｽﾀ  :  当選番号
	//
	//使用レジスタ
	//            全 ﾚｼﾞｽﾀ  :  汎用
	//========================================================================================
	public static void mMN_WCAL_00()
	{
//MN_WCAL_00:
        ushort MN_WCAL = 0x02;
		
		mLD_HL_Nm(Defines.DEF_RANDOMX);			// 乱数値 X [ 0-16383 ]
		setC( 1 );
		mLD_A_Nm(Defines.DEF_GMLVSTS);
        ushort tmpAF = (ushort)(getA() << 8);
        tmpAF = (ushort)(tmpAF >> 1);

		// RB 作動中か ?
		if( (tmpAF & 0x00FF ) > 0)
		{//RB作動中！
			//強制フラグ処理。(JAC中はﾊｽﾞﾚとﾘﾌﾟﾚｲ以外は処理しない。)
            ushort flag = getWork(Defines.DEF_FORCE_FLAG);
            if ((flag > 0) && ((flag == (int)Defines.ForceYakuFlag.HAZURE) || (flag == (int)Defines.ForceYakuFlag.REPLAY)))
			{//強制フラグの指定だった！
					setA( flag -1 );
					setC( getA() );
					mLD_Nm_A(Defines.DEF_WAVEBIT);				
					MN_WCAL = 0x01;
					
			}else{
				mLD_DE_Nt(Defines.DEF_PRB_JHTBL);	// JAC の確率抽選値 ﾃﾞｰﾀ
				setA(Defines.DEF_JACHITF);
				int ADD = (int)(getHL() + getDE() );
				//当選か ?
				if(ADD > 0xFFFF)
				{//不当選（16/16383）
					setA( 0 );
					setC( getA() );
				}	
//MN_WCAL_01:
				mLD_Nm_A(Defines.DEF_WAVEBIT);				
				MN_WCAL = 0x01;
			}
		}
//
//=== 一般遊技時、BB 作動時の抽選
//
		if( MN_WCAL == 0x02)
		{
//MN_WCAL_02:
			setDE( Defines.DEF_PRB_BBTBL );		
			setB( Defines.DEF_ETC_NUM );
            tmpAF = (ushort)(tmpAF >> 1);
			if( (tmpAF & 0x00FF ) == 0)
			{
                tmpAF = (ushort)(tmpAF >> 1);
				if( (tmpAF & 0x00FF ) > 0)
				{
					setB(Defines.DEF_FLAGNUM);
				}
//MN_WCAL_03:
				setDE( Defines.DEF_PRB_FLTBL );
			}
//
//=== 確率抽選値の選択
//
//MN_WCAL_04:
			while(true)
			{
				//強制フラグ処理。(BB中のボーナス指定は無効。)
                ushort flag = getWork(Defines.DEF_FORCE_FLAG);
				
				if(flag > 0) 
				{//強制ワークに値がある！
					Defines.RAM_TRACE("強制フラグ" + (flag&0xFFFF));
					if( ((int)(flag-1)) <= getB() )
					{//状態別のフラグ数以下なら強制フラグをセットする。
                        setC((ushort)(flag - 1));
						break;
					}
				}

                ushort pushDE = getDE();		// 期待値 ﾃｰﾌﾞﾙ を退避
                ushort pushHL = getHL();		// 乱数値を退避
//				ushort pushDE1 = getDE();		// 期待値 ﾃｰﾌﾞﾙ を退避

				Defines.RAM_TRACE("子役抽選開始！！！");
				
				setHL( Defines.DEF_SATDAT - 1 );
				Defines.RAM_TRACE("抽選1:mDataTable:" + (getHL()&0xFFFF));
				setA( getC() );
				mSB_ADRA_00(getA());
				setE(getA());
				Defines.RAM_TRACE("抽選2:mDataTable:" + (getHL()&0xFFFF));
				//アプリでは使用しない。
				//********************************************************
//		          CALL    SB_MDCK_00				// ﾒﾀﾞﾙ 投入枚数 ﾁｪｯｸ 処理
//		          JR      Z,$						// 0 枚なら ｼﾞｬﾝﾌﾟ
				//********************************************************

				//アプリ用の処理とする。
				//※（危険そうなので後で枚数チェックをすること！！）
				
				mLD_A_Nm(Defines.DEF_MEDLCTR);
				Defines.RAM_TRACE("抽選3:mDataTable:" + (getHL()&0xFFFF));
				//ﾒﾀﾞﾙ ｶｳﾝﾀｰ 格納領域 ｾｯﾄ
				// BET別抽選確率のトップ位置
				setHL( pushDE );				// 期待値 ﾃｰﾌﾞﾙ を復帰
				Defines.RAM_TRACE("抽選4:mDataTable:" + (getHL()&0xFFFF));
				//ＣＹ判定用にローカルワークを準備
                ushort lCY = (ushort)(getE() << 1);
				setE( getE() << 1 );			// ﾃﾞｰﾀ 構成用に調整
				setD(0);
				Defines.RAM_TRACE("抽選5:mDataTable:" + (getHL()&0xFFFF));
				setHL(getHL() + getDE() );
				Defines.RAM_TRACE("抽選6:mDataTable:" + (getHL()&0xFFFF));
				setA(getA()-1);

				Defines.RAM_TRACE("抽選7:mDataTable:" + (getHL()&0xFFFF));
				// 設定別の判断が不要なら ｼﾞｬﾝﾌﾟ
				if( (lCY & 0xFF00) > 0 )
				{
					
					setA( getA()*12 );
					mSB_ADRA_00(getA());
					//SB_WVCK_00の代わり
					//---------------------
					mLD_A_Nm( Defines.DEF_WAVENUM );
					Defines.RAM_TRACE("設定別に内容変更！！:" + (getA()&0xFFFF));
					if(getA() > Defines.DEF_WAVEMAX )
					{
						setA(Defines.DEF_SET_INIT);
						mLD_Nm_A( Defines.DEF_WAVENUM );
					}
					//---------------------
				}
				Defines.RAM_TRACE("抽選8:mDataTable:" + (getHL()&0xFFFF));
//MN_WCAL_05:
				setA(getA()*2);
				mSB_ADRA_00(getA());
//
//=== 抽選の実行
//
				Defines.RAM_TRACE("子役確率1:mDataTable[" + (getHL()&0xFFFF) + "]=0x" + ZZ.hexShort((short)(getHLt()&0xFFFF)));
				setE(getHLt());
				mINC_HL( 1 );
				
				Defines.RAM_TRACE("子役確率2:mDataTable[" + (getHL()&0xFFFF) + "]=0x" + ZZ.hexShort((short)(getHLt()&0xFFFF)));
				setD(getHLt());
				
				// 乱数のセット
				setHL( pushHL );
				
				Defines.RAM_TRACE("確率？: 乱数:" + (getHL()&0xFFFF) + " ﾃｰﾌﾞﾙﾃﾞｰﾀ？:0x" + ZZ.hexShort((short)(getDE()&0xFFFF)));
				
				int SBC;
				if( (lCY & 0xFF00) > 0 )
				{	// 設定別＝ボーナスしかなかったのでこれで仕分けする
                    Defines.RAM_TRACE("確率変更:" + getDE() + "→" + (getDE() * mOmatsuri.GPW_chgProba()));
                    SBC = (int)((getHL() & 0xFFFF) - (getDE() * mOmatsuri.GPW_chgProba()));
				}
				else
				{
					SBC = (int)((getHL()&0xFFFF) - (getDE()) );
				}
                setHL((ushort)SBC);

				setDE( pushDE );
				
				Defines.RAM_TRACE("当選ﾁｪｯｸ？" + SBC);
				// 当選か ?
				if(SBC < 0)break;


				setC(getC()+1);
				if( mDJNZ()==true)
				{
					setC(getB());
					break;
				}
			}
//MN_WCAL_06:
			
			setHL( Defines.DEF_HFLGTBL );				// 当たり要求 ﾌﾗｸﾞ ﾃｰﾌﾞﾙ
			setA(getC());
			setA(getA()*2);
			mSB_ADRA_00(getA());
			mINC_HL(1);
			
			setD( getHLt() );
			
			setHL( Defines.DEF_HITREQ );				// 内部当たり ﾌﾗｸﾞ 格納領域
			setA(getA() | getHLm() );
			setHLm(getA());					// 内部当たり ﾌﾗｸﾞ ｾｯﾄ
			Defines.RAM_TRACE("内部当たり:" + (getHLm() & 0xFFFF));
			mINC_HL(1);						// [ DfOHHB_V23_DEF.DEF_WAVEBIT ]
			setA(getA()|getD());
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
		mLD_Nm_A( Defines.DEF_RCB_ST+0 );		// [ DfOHHB_V23_DEF.DEF_RCB_ST+0 ]   1stﾘｰﾙｽﾃｰﾀｽｸﾘｱ
		mLD_Nm_A( Defines.DEF_RCB_ST+1 );		// [ DfOHHB_V23_DEF.DEF_RCB_ST+1 ]   2ndﾘｰﾙｽﾃｰﾀｽｸﾘｱ
		mLD_Nm_A( Defines.DEF_RCB_ST+2 );		// [ DfOHHB_V23_DEF.DEF_RCB_ST+2 ]   3rdﾘｰﾙｽﾃｰﾀｽｸﾘｱ
		setA(0x07);
		mLD_Nm_A( Defines.DEF_SLAMPBIT );		// [ DfOHHB_V23_DEF.DEF_SLAMPBIT ]   ｽﾄｯﾌﾟﾎﾞﾀﾝLED状態に全ﾘｰﾙ回転ｾｯﾄ
		setA(Defines.DEF_REELNUM);
		mLD_Nm_A( Defines.DEF_PUSHCTR );		// [ DfOHHB_V23_DEF.DEF_PUSHCTR ]    ｽﾄｯﾌﾟﾎﾞﾀﾝ作動ｶｳﾝﾀｰ全ﾘｰﾙ回転ｾｯﾄ

		setHL(Defines.DEF_REELST);
		setA(getHLm() | 0x07);
		setHLm(getA());				// [ DfOHHB_V23_DEF.DEF_REELST ]     ﾘｰﾙ制御ﾌﾗｸﾞに全ﾘｰﾙ回転要求ｾｯﾄ

		//-----------------------------------
	}
	//========================================================================================
	//== (4-05)MN_SDAT_00 テーブル、ライン選択処理
	//========================================================================================
	//
	//入力パラメータ
	//            なし
	//
	//出力パラメータ
	//            なし
	//
	//使用レジスタ
	//            全 ﾚｼﾞｽﾀ  :  汎用
	//
	//========================================================================================
    static ushort mDbSdatAdr;
	public static void mMN_SDAT_00()
	{
//MN_SDAT_00:
		setHL( Defines.DEF_GMLVSTS );
		setA(getHLm());
		mSB_BNUM_00();
		mINC_HL(1);
		setA(getHLm());
		setB(getB()-1);
		setHL( Defines.DEF_STPTBL1 );
		if(getB() != 0)
		{
			setC(getA());
			setHL( Defines.DEF_STPTBL0 );
			mSB_ADRA_00(getA());
		
			setHL( Defines.DEF_STPTBL2-1 );
			mSB_ADRA_00(getA());

			mLD_A_Nm(Defines.DEF_MEDLCTR);			
			if( Defines.DEF_FRTFLGC < getC() )
			{
				setB(0);			
			}else{
//MN_SDAT_01:
				setA(getA()-1);
				setA(getA()*4);			
			}
		}
//MN_SDAT_02:
		setA(getA()+getB());
		mSB_ADRA_00(getA());
		setD(23);
		mSB_DIVD_00();
		setHL( Defines.DEF_LINEDAT2 );
		mSB_ADRA_00(getA());
		mSB_ADRA_00(getA());
		setA(getE());
		mLD_Nm_A(Defines.DEF_TBLNUM);

        //		  ushort R = 0;
        ushort R = (ushort)clRAND8.mGetRnd8();
		//デバッグ用にHLアドレスを保存
        mDbSdatAdr = (ushort)getHL();
		
		
		setA( R & 0x0F );
		setD(getHLt());
		mINC_HL(1);
		mSB_DIVD_00();
		mSB_ADRA_00(getA());
		mLD_Nm_A(Defines.DEF_LINENUM);
	}
	//========================================================================================
	//== (5-01)MN_STOP_00 回胴停止制御処理
	//========================================================================================
	public static void mMN_STOP_00()
	{
		//mDebugStopRndが呼ばれている
		if( ( getWork(Defines.DEF_GAMEST) & (0x01<<Defines.DEF_STOPRND_FLN) ) > 0)
		{//デバッグフラグが立っていたら処理する
			if((mDbSdatAdr != 0))
			{
				
				setHL((int)mDbSdatAdr);
				
				setD(getHLt());
				mINC_HL(1);
				mLD_A_Nm(Defines.DEF_STOPRND);
				mSB_ADRA_00(getA() % getD() );
				mLD_Nm_A(Defines.DEF_LINENUM);
				mDbSdatAdr = 0;
			}			
		}
	
/*
		//DfOHHB_V23_DEF.DEF_ARAY11～DfOHHB_V23_DEF.DEF_ARAY13の全てを回転中コードにする(0x7F)
		setHL( (DfOHHB_V23_DEF.DEF_ARAY * 256 ) + DfOHHB_V23_DEF.DEF_ARAY );
		mLD_Nm_HL( DfOHHB_V23_DEF.DEF_ARAY11 );
		mLD_Nm_HL( DfOHHB_V23_DEF.DEF_ARAY12 );
*/		
//MN_STOP_01～MN_STOP_02はアプリでは処理しない。

//MN_STOP_03:
//
//=== ｽﾄｯﾌﾟﾎﾞﾀﾝ の ﾁｪｯｸ
//
		setHL(Defines.DEF_SLAMPBIT);		//ｽﾄｯﾌﾟﾎﾞﾀﾝ LED 状態格納領域 ｾｯﾄ
		setA(getHLm());			//ｽﾄｯﾌﾟﾎﾞﾀﾝ LED 状態 取得 ( 点灯で回転中 )
		setD( ~getA() );		//反転 ( 消灯で回転中 )

		mLD_A_Nm( Defines.DEF_INBUFF0 );		//入力 ﾎﾟｰﾄ 0 ( ｽﾄｯﾌﾟﾎﾞﾀﾝ 入力 ) の状態 取得
		setE(getA());
		setA(getA() & getD() );	//有効な ( 回転中の ) ｽﾄｯﾌﾟﾎﾞﾀﾝ 以外の操作か ?

		//有効なストップボタン以外は処理しない
		if(getA() == 0)
		{
			//入力 ﾎﾟｰﾄ 0 ( ｽﾄｯﾌﾟﾎﾞﾀﾝ 入力 ) の状態 取得
			mLD_A_Nm( Defines.DEF_INBUFF0 );
			//有効な ( 回転中の ) ｽﾄｯﾌﾟﾎﾞﾀﾝ 入力か ?
			if( (getA() & getHLm()) > 0)
			{
//
//=== ｽﾄｯﾌﾟ 開始時の初期化
//
//MN_STOP_05:
//				  ushort pushAF = getAF();	

				setIX(Defines.DEF_RCB_ST);				//  ﾘｰﾙ ｽﾃｰﾀｽ 格納領域
				setBC( (2*256)+0x01 );		// ﾘｰﾙ 数 ､ ｽﾄｯﾌﾟﾎﾞﾀﾝ 作動 ﾋﾞｯﾄ
//				setDE(R2_CTRL-R1_CTRL);		// 制御 ﾃﾞｰﾀ 格納領域 幅 ｾｯﾄ
//MN_STOP_06:
                ushort RRCA = (ushort)((getA() & 0x00FF) << 8);
				while(true)
				{
                    RRCA = (ushort)(RRCA >> 1);
					if( (RRCA & 0x0080) > 0 ) break;
//					setIX( getIX() + getDE() );		
					mINC_IX(1);
					setC( (getC()<<1) );
					if( mDJNZ()==true) break;
				}
//MN_STOP_07:
//				setAF(pushAF);
				//ここではあまり使用したくないが・・・
	            //SET     4,(IX+DfOHHB_V23_DEF.DEF_CCCST)        ; 補正禁止 ﾌﾗｸﾞ ｵﾝ
				//↑の代わり。
//				setWork((getIX() + DfOHHB_V23_DEF.DEF_CCCST),0x10);
				
				// ｽﾄｯﾌﾟﾎﾞﾀﾝ LED 状態 ｾｯﾄ
				// *   ｽﾄｯﾌﾟﾎﾞﾀﾝ 作動 ﾋﾞｯﾄ 反転 (停止対象を'0')
				// *   現在の状態を加味して更新
				setA( (~getC() & getHLm()) );
				setHLm( getA() );

				// ｽﾄｯﾌﾟﾎﾞﾀﾝ 作動状態 ｾｯﾄ
				// ( DfOHHB_V23_DEF.DEF_STOPBIT に ｾｯﾄ するため上位 4ﾋﾞｯﾄ へ )
				setA( (getA()<<4) | getC() );
				// [ DfOHHB_V23_DEF.DEF_STOPBIT ] ｽﾄｯﾌﾟﾎﾞﾀﾝ 作動状態 格納領域
				mINC_HL(1);
				setHLm(getA());

				// [ DfOHHB_V23_DEF.DEF_PUSHCTR ] ｽﾄｯﾌﾟﾎﾞﾀﾝ 作動 ｶｳﾝﾀｰ 格納領域
				// ｽﾄｯﾌﾟﾎﾞﾀﾝ 作動 ｶｳﾝﾀｰ を更新
				mINC_HL(1);
				setHLm( getHLm()-1 );

/*				ここはアプリでは処理しないが、SB_BCLR_00は4ｔｈ制御するなら必要。
				LD      A,(HL)
				CALL    SB_BCLR_00          ; 回転表示器回転要求ﾋﾞｯﾄｸﾘｱ処理
;
				LD      HL,STOPON           ; ﾘｰﾙ ｽﾄｯﾌﾟ管理用 ﾀｲﾏｰ ｾｯﾄ
				RST     SB_SCHD_00          ; ｽﾄｯﾌﾟﾎﾞﾀﾝ 1-3 LED ｵﾌ
				CALL    SB_OTLP_00          ; ﾘｰﾙ 回転停止音 ｵﾝ
*/

//				  ushort R = 1;
                ushort R = (ushort)(clRAND8.mGetRnd8() & 0x7F);
				//デバッグフラグは立っているか？
				if( ( getWork(Defines.DEF_GAMEST) & (0x01<<Defines.DEF_STOPRND_FLN) ) == 0)
				{//立っていないの通常処理。
					setA( R );
					mLD_Nm_A(Defines.DEF_STOPRND);
				}//else フラグが立っていたので既にセットされているDfOHHB_V23_DEF.DEF_STOPRNDを使用。

//=== ｽﾄｯﾌﾟ 位置検索
//
				//この部分はミニスロ仕様に依存するため一部変更します。(リールパルスの基準値判定を行わない。)
                ushort tmpIX = (ushort)(getIX() + (Defines.DEF_CCCPIC));
//				setA( getIXm() );

				mLD_A_Nm(tmpIX);
				//大花では使用しない変数だが念のため保存しておく。
				mLD_Nm_A(Defines.DEF_STP_INPT);

				// 使用 ｽﾃｰﾀｽ ﾚｼﾞｽﾀ 格納処理
				mSTSB_STRE_00();
				// 回胴停止選択処理（Aﾚｼﾞｽﾀｰに滑り値を格納）
				
				mSTOP_SSEL_00();

                ushort tmpA = getA();
				mLD_A_Nm(tmpIX);
				setA( getA() + tmpA );

				if(getA() >= 21 )
				{
					setA( getA() - 21 );
				}
//MN_STOP_12:
				//停止予定位置を格納
				//Z80ではDfOHHB_V23_DEF.DEF_CCCPIC（ストップ位置）がそのまま最終位置になるため
				//アプリでは使用していないワークを利用する（DfOHHB_V23_DEF.DEF_CCCPOS）			
				//アプリ専用処理！！
                tmpIX = (ushort)(getIX() + Defines.DEF_CCCPOS);
				mLD_Nm_A(tmpIX);

				// 図柄 ｺｰﾄﾞ 格納処理
				mSTSB_ARYS_00();				
//
//=== 回胴停止後の処理
//
				// 回胴停止後の設定処理
				mSTOP_RECH_00();
				// 制御変更 ﾁｪｯｸ 処理
				mSTOP_SCHK_00();
			}
		}
	}
	/////////////////////////////////////////////////////////////////////////
	//初期化関数
	/////////////////////////////////////////////////////////////////////////
	// n : 乱数初期化用の数値。（現在時刻をsec単位にした数値等）
	//-----------------------------------------------------------------------
	public static void mInitializaion(int n )
	{
//System.out.println("Randseed="+(n&0xffff));
        //clRAND8.mInitializaion(n & 0xFFFF);
        clRAND8.mInitializaion(n);
        clearWork(Defines.DEF_CLR_AREA_1);
//		mRtEndBakup_f = false;			
	}
	/////////////////////////////////////////////////////////////////////////
	//乱数更新処理（必ず最も速くループする処理プロセス内で呼んで下さい！）
	/////////////////////////////////////////////////////////////////////////
	//※このメソッドは大祭りでは使用しない。（使用しても問題はない。）
	//-----------------------------------------------------------------------
	public static void mRandomRefresh()
	{
		mSB_RRND_00();
	}
	/////////////////////////////////////////////////////////////////////////
	//乱数取得（パチスロの抽選以外では使用しないで下さい！！）
	/////////////////////////////////////////////////////////////////////////
	// return : 0～16383までの乱数（パチスロ機種依存型の乱数）
	//-----------------------------------------------------------------------
	public static int mRandomX()
	{
		mMN_PRND_00();	
		return getWork16(Defines.DEF_RANDOMX);
	}
	/////////////////////////////////////////////////////////////////////////
	//リールスタート関数（レバーＯＮ）
	/////////////////////////////////////////////////////////////////////////
	// random : 抽選乱数（0～16383）
	// medal  : 投入メダル枚数（1～3）※0もしくは3以上の場合は動作保証しません。
	//-----------------------------------------------------------------------
	// 変化　 : DfOHHB_V23_DEF.DEF_WAVEBIT
	//		  : DfOHHB_V23_DEF.DEF_HITREQ 
	//		  : FLAGCTR 
	//		  : DfOHHB_V23_DEF.DEF_TBLNUM 
	//		  : DfOHHB_V23_DEF.DEF_LINENUM 
	//		  : MEDALCTR 
	//-----------------------------------------------------------------------
	public static void mReelStart(int random , int medal)
	{

		//_GMSB_MDIN_00メダル投入処理の一部をここで行う。
		//LD      A,(HL)              ; ﾒﾀﾞﾙ ｶｳﾝﾀｰ
        //LD      HL,MLAMPTBL-1       ; ﾒﾀﾞﾙ ﾗｲﾝ ﾗﾝﾌﾟ ﾃｰﾌﾞﾙ
        //RST     SB_ADRA_00
        //LD      (DfOHHB_V23_DEF.DEF_MLAMPST),A         ; 有効 ﾗｲﾝ ｾｯﾄ
		// 有効ライン数のセット。

        setWork(Defines.DEF_MEDLCTR, (ushort)medal);


		  ushort[] MLAMPTBL={           // ﾒﾀﾞﾙﾗｲﾝﾗﾝﾌﾟの点灯ﾊﾟﾀｰﾝﾃｰﾌﾞﾙ
             0x08,                    // 1ﾒﾀﾞﾙ投入時(ﾒﾀﾞﾙﾗｲﾝﾗﾝﾌﾟ1点灯)
             0x38,                    // 2ﾒﾀﾞﾙ投入時(ﾒﾀﾞﾙﾗｲﾝﾗﾝﾌﾟ1､2A､2B点灯)
             0xF8,                    // 3ﾒﾀﾞﾙ投入時(全ﾒﾀﾞﾙﾗｲﾝﾗﾝﾌﾟ点灯)
		};
		mLD_A_Nm(Defines.DEF_MEDLCTR);
		if(getA()>0)
		{
			setA(MLAMPTBL[getA()-1]);	
			mLD_Nm_A(Defines.DEF_MLAMPST);
		}
        setWork((int)Defines.DEF_RANDOMX, (ushort)random);

		// 遊技状態監視処理
		mMN_GLCK_00();
//MAIN_04:
		// 確率抽選処理
		mMN_WCAL_00();
/*
		※mMN_GLCK_00内へ移動

*/
//
//=== ﾘｰﾙ ｽﾄｯﾌﾟ 実行
//
		mMN_SDAT_00();

		//MN_STOP_00の先頭で行っている回転中コードをここで行う。
		setHL(Defines.DEF_ARAY11);
		setBC(15*256+Defines.DEF_ARAY);
		//SB_WKST_00を展開
		while(true)
		{
			setHLm(getC());
			mINC_HL(1);
			if(mDJNZ() == true)break;
		}

	}
	/////////////////////////////////////////////////////////////////////////
	//強制フラグのセット（mReelStartメソッドの前で呼んで下さい。）
	/////////////////////////////////////////////////////////////////////////
	// flagIndex : 強制フラグ(0:無効/1～DfOHHB_V23_DEF.DEF_FORCE_MAX-1）※下記のワードを参照。
	//-----------------------------------------------------------------------
	//					BB未動作時		｜	BB作動時				｜	JAC作動中
	//DfOHHB_V23_DEF.DEF_FORCE_HAZURE	：（ﾊｽﾞﾚ）			｜（ﾊｽﾞﾚ）					｜（ﾊｽﾞﾚ）		
	//DfOHHB_V23_DEF.DEF_FORCE_CHERY 	：（ﾁｪﾘｰ）			｜（15枚役）ﾄﾞﾝ・ﾍﾞﾙ・ﾍﾞﾙ	｜（無効）	
	//FORCE_DfOHHB_V23_DEF.DEF_BELL 	：（ﾍﾞﾙ）			｜（ﾍﾞﾙ）					｜（無効）		
	//DfOHHB_V23_DEF.DEF_FORCE_SUIKA 	：（ｽｲｶ）			｜（ｽｲｶ　or ﾁｪﾘｰ）			｜（無効）
	//DfOHHB_V23_DEF.DEF_FORCE_REPLAY	：（ﾘﾌﾟﾚｲ）			｜（JACIN）					｜（15枚役）ﾘﾌﾟ・ﾘﾌﾟ・ﾘﾌﾟ
	//DfOHHB_V23_DEF.DEF_FORCE_REG 	：（ﾚｷﾞｭﾗｰﾎﾞｰﾅｽ）	｜（無効）					｜（無効）
	//DfOHHB_V23_DEF.DEF_FORCE_BIG 	：（ﾋﾞｯｸﾞﾎﾞｰﾅｽ）	｜（無効）					｜（無効）
	//DfOHHB_V23_DEF.DEF_FORCE_MAX 	： フラグ数
	//-----------------------------------------------------------------------
	//※無効時は、通常通りmReelStart()がRAMDOMXを参照し処理されます。
	//※セットされている強制フラグはDfOHHB_V23_DEF.DEF_FORCE_FLAGという名の場所に保存されます。
	//※DfOHHB_V23_DEF.DEF_FORCE_FLAGワークはclaerWork(DfOHHB_V23_DEF.DEF_CLR_AREA_3)でゼロクリアされます。
	//-----------------------------------------------------------------------
	public static void mSetForceFlag(Defines.ForceYakuFlag flagIndex)
	{
		Defines.RAM_TRACE("mSetForceFlag:["+Defines.DEF_FORCE_FLAG+"]" + flagIndex);
        setWork(Defines.DEF_FORCE_FLAG, (ushort)((int)flagIndex & (Enum.GetNames(typeof(Defines.ForceYakuFlag)).Length - 1)));	
	}
	/////////////////////////////////////////////////////////////////////////
	//リールストップ関数（ストップボタン）
	/////////////////////////////////////////////////////////////////////////
	// reel : 回転中に停止されたリール（0:左/1:中/2:右）
	// pos  : 停止したリールのｾﾝﾀｰﾗｲﾝ位置番号（0～20）
	//-----------------------------------------------------------------------
	// return : 最終リール位置番号（0～20）
	//-----------------------------------------------------------------------
    public static ushort mReelStop(int reel, ushort pos)
	{
          ushort[] REEL_PIC = { (Defines.DEF_RCB_PIC + 0), (Defines.DEF_RCB_PIC + 1), (Defines.DEF_RCB_PIC + 2) };
          ushort[] REEL_STP = { (Defines.DEF_RCB_POS + 0), (Defines.DEF_RCB_POS + 1), (Defines.DEF_RCB_POS + 2) };
          ushort bit = 0x01;
		int i = reel;
        for (int j = 0; j < i; j++) bit = (ushort)(bit << 1);
		//有効なボタンが押されたよ！
		setWork(Defines.DEF_INBUFF0,bit);
		//その時のリール位置
		setWork(REEL_PIC[i],pos);

		mMN_STOP_00();

		return getWork(REEL_STP[i]);
	}
	/////////////////////////////////////////////////////////////////////////
	//乱数テーブル選択用ワークのセット（mReelStart()～mReelStop()の間で呼んで下さい。）
	/////////////////////////////////////////////////////////////////////////
	// random  : 停止テーブル選択用の乱数値（通常時：0～3 / 特殊時：0～255）
	//-----------------------------------------------------------------------
	// ※このメソッドは引数の値をDfOHHB_V23_DEF.DEF_STOPRNDという名のワークにセットします。
	// ※DfOHHB_V23_DEF.DEF_STOPRNDは通常、ストップボタンを押すたびに乱数生成されますが、このメソッドを
	//   呼び出すと、指定の乱数値になります。(0～3の値にマスクされます。)
	// ※DfOHHB_V23_DEF.DEF_STOPRNDはフラグ間や、変則押しに時に特殊な判定に使用されています。
	//  　そのため、0～255までの値をセットできるようにしてあります。
	// ※DfOHHB_V23_DEF.DEF_FORCE_FLAGワークはclaerWork(DfOHHB_V23_DEF.DEF_CLR_AREA_3)でゼロクリアされます。
	// ※このメソッドは物凄く機種依存ですのでご注意ください。
	//-----------------------------------------------------------------------
	public static void mSetDebugStopRnd(int random)
	{				
		setHL( Defines.DEF_GAMEST );		
		setHLm(getHLm() | (0x01<<Defines.DEF_STOPRND_FLN));
        setWork(Defines.DEF_STOPRND, (ushort)(random & 0xFF));
	}
	/////////////////////////////////////////////////////////////////////////
	//入賞＆メダル払出し関数
	/////////////////////////////////////////////////////////////////////////
	// return : 払い出しメダル数（0～15）0xFFFFの場合はエラー
	//-----------------------------------------------------------------------
    public static ushort mPayMedal()
	{
        ushort ret = 0xFFFF;


//      LD      HL,CKLNINI          ; 入賞検索処理用に ｲﾆｼｬﾙ
//      RST     SB_SCHD_00          ; *
//		↑の代わりにワーククリアする
		setA(0);
		mLD_Nm_A( Defines.DEF_HITFLAG );
		mLD_Nm_A( Defines.DEF_HITSND  );
		mLD_Nm_A( Defines.DEF_HITCTR  );
		mLD_Nm_A( Defines.DEF_HITLINE );

		mMN_CKLN_00();

		if(mMN_ILCK_00() == false)
		{		
#if __ERR_MSG__
			SET_ERR_OPTION(0);
#endif
//特賞時の７ＳＥＧカウンター処理。
//			mSB_BSEG_00();
			ret = getWork( Defines.DEF_HITCTR );
		}
#if __ERR_MSG__
		else
		{
			SET_ERR_OPTION(1);
		}		
		if( ret > 15)
		{	// 15以上ならば
			SET_ERR_CODE(ERR_CODE_PAY_UP_RAM);
			SET_ERR_OPTION(mOmatsuri.int_s_value[DfOHHB_V23_DEF.DEF_INT_WIN_COIN_NUM]);
			SET_ERR_OPTION2("HITCTR:" + (getWork( DfOHHB_V23_DEF.DEF_HITCTR ) & 0xFFFF));
		}
#endif

// エラー対応
		if( ret > 15 ) {
			ret = 0;
		}
		
		
		return ret;
	}
	/////////////////////////////////////////////////////////////////////////
	//ボーナス関連関連処理
	/////////////////////////////////////////////////////////////////////////
	// return : 特賞状態フラグ　（0 : 一般中 / 1: BB終了タイミング）
	//-----------------------------------------------------------------------
	// 変化　 : DfOHHB_V23_DEF.DEF_GAMEST   主にボーナス中とリプレイ中の状態変化に使用。※下記のビットでチェック可能。
	//		  : DfOHHB_V23_DEF.DEF_BBGMCTR  BB子役ゲーム中カウンター（0～30）
	//		  : DfOHHB_V23_DEF.DEF_BIGBCTR  残りJACIN可能回数（0～3）
	//		  : DfOHHB_V23_DEF.DEF_JAC_CTR  JACゲーム 入賞可能回数（0～8）
	//		  : DfOHHB_V23_DEF.DEF_JACGAME: JACゲーム 遊技可能回数（0～12）
	//-----------------------------------------------------------------------
	//DfOHHB_V23_DEF.DEF_BBGC_FLN   7                   ; BB 作動 ﾁｪｯｸ ﾋﾞｯﾄ
	//DfOHHB_V23_DEF.DEF_ERRC_FLN   3                   ; ｴﾗｰ ﾁｪｯｸ ﾋﾞｯﾄ
	//DfOHHB_V23_DEF.DEF_RPLC_FLN   2                   ; 再遊技 ﾁｪｯｸ ﾋﾞｯﾄ
	//DfOHHB_V23_DEF.DEF_RBGC_FLN   0                   ; RB 作動 ﾁｪｯｸ ﾋﾞｯﾄ
	//DfOHHB_V23_DEF.DEF_GMC_NUM    8                   ; 遊技状態数
	//;
	//;                    DfOHHB_V23_DEF.DEF_GAMEST              ; 遊技状態 ﾌﾗｸﾞ
	//;                                        ; [ D7 ] BB 作動中
	//;                                        ; [ D6 ] 正回転中
	//;                                        ; [ D5 ] 打止め中
	//;                                        ; [ D4 ] 逆回転中
	//;                                        ; [ D3 ] ｴﾗｰ 中
	//;                                        ; [ D2 ] 再遊技中
	//;                                        ; [ D1 ] 未使用
	//;                                        ; [ D0 ] RB 作動中
	//;
	//;                    DfOHHB_V23_DEF.DEF_GMLVSTS             ; 遊技状態 ｽﾃｰﾀｽ
	//;                                        ; [ D7 ] 未使用
	//;                                        ; [ D6 ] 未使用
	//;                                        ; [ D5 ] 未使用
	//;                                        ; [ D4 ] BB 内部当たり中
	//;                                        ; [ D3 ] RB 内部当たり中
	//;                                        ; [ D2 ] 一般遊技中
	//;                                        ; [ D1 ] BB 作動中
	//;                                        ; [ D0 ] RB 作動中
	//;
	//-----------------------------------------------------------------------
    public static ushort mBonusCounter()
	{
        ushort ret = 0;
		mLD_A_Nm( Defines.DEF_GMLVSTS );

		//TRACE("遊技状態 ｽﾃｰﾀｽ:" + (getA()&0xFFFF));
		if( (getA() & 0x03) != 0)
		{	// ボーナス限定処理
			if(mMN_GCCK_00() == true)
			{
				clearWork(Defines.DEF_CLR_AREA_2);
				ret = Defines.DEF_BBEND_FLX;
			}else{

//             CALL    MN_BSEG_00          ; ﾎﾞｰﾅｽ 7SEG 制御処理

				return ret;
			}
		}
//MAIN_06:
		mMN_GMIT_00();

//             CALL    MN_BSEG_00          ; ﾎﾞｰﾅｽ 7SEG 制御処理
		
		return ret;
	}

////////////////////////////////////////////////////////////////
//compact.CompactClass char[]

/*------------------ class clOHHB_V23 ------------------*/

/** AUTO GENERATED int ARRAY BY compact.CompactClass */
#if __COM_TYPE__
		////////////////////////////////////////////////////////////////
//compact.CompactClass byte[]

/*------------------ class clOHHB_V23 ------------------*/

	static clOHHB_V23() {
		//clZ80RAM.mDataTable = mDataTable;
		loadRAM();
	}
	
	public static void loadRAM()
	{
		int i;
		int j;
		sbyte[] mDataTable;
		
		//short tmp;

		try {
			// javaはビックエンディアンらしいので
            // TODO エンディアン
            mDataTable = SlotInterfaceResource.getResourceData("ClZ80RAM_big.dat");
			clZ80RAM.mDataTable = new ushort[mDataTable.Length/2];
            
			//mDataTable = GpHandler.V_getResourceData( "ClZ80RAM_short.dat" );
            //clZ80RAM.mDataTable = new ushort[mDataTable.length];
			
			for (i = 0, j = 0; i < mDataTable.Length; i+=2, j++) {
//////////////////////////////////////////////////////////////////////////
// signedとunsigendの関係で数値がおかしくなったのでテストしてみた。
//////////////////////////////////////////////////////////////////////////
//				int tmp2;
//				int tmp3;
//				int tmp4;
//				//////////////////////////////////////////////////////////////////////////
//				// OK
//				tmp2 = (char)(mDataTable[i] << 8);
//				tmp3 = (char)(mDataTable[i+1] & 0xFF);
//				System.out.println("load["+(i)+"] tmp2:"+(tmp2&0xFFFF) + " tmp3:" + (tmp3&0xFFFF));
//				
//				//////////////////////////////////////////////////////////////////////////
//				// OK
//				tmp4 = (char)(tmp2 + tmp3);
//				System.out.println("load["+(i)+"]"+(tmp4&0xFFFF));
//				
//				//////////////////////////////////////////////////////////////////////////
//				// OK
//				tmp4 = (char)(tmp2 | tmp3);
//				System.out.println("load["+(i)+"]"+(tmp4&0xFFFF));
//				
//				//////////////////////////////////////////////////////////////////////////
//				//////////////////////////////////////////////////////////////////////////
//				// OK
//				tmp2 = (char)((mDataTable[i] << 8) & 0xFF);
//				tmp3 = (char)(mDataTable[i+1] & 0xFF);
//				System.out.println("load["+(i)+"] tmp2:"+(tmp2&0xFFFF) + " tmp3:" + (tmp3&0xFFFF));
//				
//				//////////////////////////////////////////////////////////////////////////
//				// OK
//				tmp4 = (char)(((mDataTable[i] << 8) & 0xFF) | (mDataTable[i+1] & 0xFF));
//				System.out.println("load["+(i)+"]"+(tmp4&0xFFFF));
//				
//				//////////////////////////////////////////////////////////////////////////
//				// ダメ
//				tmp4 = (char)(((mDataTable[i] << 8) | mDataTable[i+1]) & 0xFFFF);
//				System.out.println("load["+(i)+"]"+(tmp4&0xFFFF));
//				
//				//////////////////////////////////////////////////////////////////////////
//				// ダメ
//				tmp4 = (char)((mDataTable[i] << 8) | (mDataTable[i+1]));
//				System.out.println("load["+(i)+"]"+(tmp4&0xFFFF));
//				
//////////////////////////////////////////////////////////////////////////
                clZ80RAM.mDataTable[j] = (ushort)((mDataTable[i] << 8) & 0xFF);
                clZ80RAM.mDataTable[j] |= (ushort)(mDataTable[i + 1] & 0xFF);
                //UnityEngine.Debug.Log(mDataTable.Length / 2 - j);
                //clZ80RAM.mDataTable[mDataTable.Length/2 - j - 1] = (ushort)((mDataTable[i] << 8) & 0xFF);
                //clZ80RAM.mDataTable[mDataTable.Length/2 - j - 1] |= (ushort)(mDataTable[i + 1] & 0xFF);
                //System.out.println("load mDataTable["+(j)+"]"+(clZ80RAM.mDataTable[j] & 0xFFFF));
			}
		}
		catch( Exception )
		{
            // TODO C#移植 なにこれ
            // セグメントフォルトをわざと起こしてアプリを落としているのだと思います。
            // でも正規の手順でアプリを落とすべきだと思います。
            // なぜかというとこれをセグメントフォルトと認識しない処理系もあるからです。
			clZ80RAM.mDataTable[-1] = 00; // ROMデータの読み込み失敗(強制落とし)
		}
	}
#else
		static char[] mDataTable = "\u0000\u0003\u0006\u0009\u008C\u009E\u0011\u00C0\u0005\u0000\u0088\u0013\u0027\u0035\u000E\u0000\u000F\u0000\u0010\u0000\u0002\u0000\u0009\u0000\n\u0000\u00BC\u0007\u0064\n\u00BE\n\u002A\u0000\u004D\u0000\u0000\u0001\u0000\u0002\u00FB\u0003\u00B9\u0005\u0010\u0000\u002A\u0000\u00C0\u0000\u00C5\u0008\u00C5\u0008\u00C5\u0008\u0005\u0000\u0005\u0000\u0005\u0000\u0005\u0000\u0005\u0000\u0005\u0000\u000F\u0000\u000F\u0000\u000F\u0000\u000F\u0000\u000F\u0000\u000F\u0000\u0019\u0000\u0019\u0000\u001B\u0000\u001B\u0000\u0020\u0000\"\u0000\r\u0000\r\u0000\r\u0000\r\u0000\r\u0000\r\u0000\u0017\u0000\u0017\u0000\u0017\u0000\u0017\u0000\u0017\u0000\u0017\u0000\u0026\u0000\u0029\u0000\u002D\u0000\u0033\u0000\u0038\u0000\u0044\u0000\u0000\u0000\u0000\u0001\u0000\u0002\u0000\u0004\u0000\u0008\u0010\u0010\u0020\u0020\u0001\u0008\u0004\u0020\u0010\u0040\u0001\u0008\u0020\u0002\u0002\u0002\u0008\u0020\u0040\u0001\u0010\u0008\u0020\u0010\u0020\u0001\u0008\u0008\u0020\u0004\u0040\u0020\u0008\u0001\u0010\u0040\u0020\u0008\u0004\u0040\u0004\u0020\u0008\u0040\u0002\u0020\u0008\u0010\u0008\u0020\u0010\u0020\u0040\u0008\u0010\u0020\u0001\u0040\u0008\u0010\u0020\u0040\u0008\u0010\u0020\u0002\u0008\u0040\u0020\u0004\u0008\u0010\u0020\u0001\u0002\u0000\u0002\u0000\u0018\u0019\u0017\u0018\u0018\u002F\u0030\u002E\u002E\u0030\u0008\u0007\u0006\u0005\u0004\u0003\u0002\u0001\u0000\u0014\u0013\u0012\u0011\u0010\u000F\u000E\r\u000C\u000B\n\u0009\u001E\u00F8\u0001\u0001\u0001\u0020\u001F\u001E\u00F8\u0002\u0002\u0002\u0020\u002F\u001E\u00F8\u0004\u0004\u0004\u0010\u000F\u001C\u00F8\u0008\u0008\u0008\u0008\u0000\u0002\u00F8\u0008\u0008\u0008\u0008\u000F\u001E\u00F8\u0010\u0010\u0010\u0004\u0006\u0002\u00F8\u0040\u0000\u0000\u0004\u0002\u001E\u00F8\u0020\u0020\u0020\u0002\n\u0002\u00F8\u0002\u0020\u0020\u0001\u000F\u001C\u00F8\u0040\u0000\u0000\u0001\u0002\u0001\u0008\u000C\u0008\u0008\u0008\u000F\u0000\u0080\u0004\u0008\u0002\u0010\u0001\u0020\u0040\n\u000B\u000C\r\u000E\u000F\u0012\u0015\u001A\u001E\u0001\u0000\u0001\u0005\u0001\u0003\u0001\u0001\u0001\u0002\u0003\u0003\u0001\u0002\u0003\u0000\u0007\u0006\u0005\u0005\u0003\u0001\u0002\u0004\u0004\u0005\u0003\u0002\u0004\u0004\u0006\u0006\u0006\u0001\u0000\u000C\u0018\u0024\u0030\u003C\u003F\u007B\u0076\u001D\u001D\u008A\u00A1\u0018\u001C\u008F\u00A9\u0018\u001E\u008F\u00A9\u002E\u002F\u0030\u0032\u002E\u002F\u0030\u0031\u0034\u002F\u0030\u0031\u0045\u0045\u0045\u0045\u0048\u004A\u004A\u004A\u0048\u004C\u004C\u004C\\\\\\\\\u0061\u0061\u0061\u0061\u0063\u0063\u0063\u0063\u0076\u0076\u0076\u0076\u0076\u0078\u0078\u0078\u007C\u007A\u007A\u007A\u008A\u008F\u008F\u00A1\u00A9\u00A9\u00E3\u0013\u0001\u0010\u00F8\u0013\u0002\u0020\r\u0014\u0004\u0040\"\u0014\u0008\u0080\u0000\u0000\u0000\u0001\u0001\u0000\u0000\u0000\u0004\u000C\r\u0080\u0000\u0002\u0002\u0002\u0002\u0000\u0000\u0000\u0000\u0082\u0000\u0008\u0009\n\u0080\u0086\u000C\u0009\u0081\u0009\u0004\u0006\u0007\n\u0089\u0004\u0082\u000F\u0089\u0000\u0002\u0001\u0009\u0082\u0088\u0004\u000F\u0080\u0009\u0088\u0001\u0005\u0084\u0088\u0086\u0008\u0086\u0004\u0008\u0000\u0008\u0001\u0002\u0008\u0089\u0004\u0083\u0007\u008C\u0008\u0008\u0001\u0005\u0008\u0006\u0008\u0086\u0086\u0004\u0080\n\u0008\u000E\u0020\u000F\u0000\u0000\u0000\u00C3\u002C\u00C2\u000C\u0068\u000F\u0000\u0000\u0000\u005B\u0024\u0002\u0020\u00C3\u0020\u0021\u0005\u0001\u000E\u00C2\u002E\u0010\u0002\u000C\u00C1\u0000\u0018\u002C\u00E0\u000E\u0001\u002D\u0005\u0009\u0007\u0009\u0000\u0068\u0087\u0000\u0000\u0000\u000E\u0069\u000E\u0080\u0000\u000F\u0000\u0000\u0000\u0000\u002D\u00D0\u000E\u0021\u0086\u0000\u0000\u000F\u0000\u0000\u0001\u0000\u000F\u00E0\u0000\u0000\u000E\u0010\u0000\u0000\u00E1\u000E\u0020\u00C3\u002E\u0000\u00A5\u000F\u0000\u0000\u0000\u0000\u00E1\u000E\u0000\u0000\u00F0\u000F\u0000\u0000\u0000\u000E\u00E1\u000E\u0000\u0000\u004A\u0061\u0020\u0014\u0033\u0015\u0061\u0010\u000C\u003F\u0015\u0052\u0020\u0014\u0037\u0015\u0052\u0010\u000C\u0045\u0015\u0034\u0020\u0014\u003B\u0015\u0034\u0010\u000C\u004A\u0015\u00FF\u0061\u0008\u001E\u0007\u0015\u0061\u0004\u001E\u0014\u0015\u0061\u0002\u001E\"\u0015\u00FF\u0050\u0015\u0065\u0015\u007A\u0015\u008F\u0015\u00A4\u0015\u00B9\u0015\u00CE\u0015\u00E3\u0015\u00F8\u0015\r\u0016\"\u0016\u0037\u0016\u0040\u0011\u0011\u0040\u0024\u0024\u0040\u0035\u0035\u0040\u0066\u0066\u0040\u0077\u0077\u0020\u0023\u0045\u0078\u0065\u0065\u0040\u0000\u0000\u0078\u0024\u0024\u0040\u0044\u0044\u0078\u0035\u0035\u00FE\u0005\u0000\u002B\u0006\u0031\u001F\u000B\u0024\u0010\u003B\u0029\u00FF\u00FE\u0004\u00AA\u0005\u0085\u0086\u00C8\u0021\u00A2\u00E2\u00B9\u0012\u0091\u00FF\u00FE\u0003\u0016\u0002\u001B\u0007\u0032\u0020\u0025\u0011\u007B\u0013\u003E\u00FE\u0009\u001E\u00FF\u00FE\u0007\u0055\u00FF\u00FE\u0007\u0055\u00FF\u00FE\u0007\u00CF\u00FF\u00FE\u0006\u0059\u004D\u005D\u00FF\u00FE\u0006\u0059\u004F\u00FF\u00FE\u0006\u0044\u0062\u00FF\u0055\u0008\u0055\u004F\u0011\u0040\u005D\u0075\u0048\u0010\u0057\u006C\u0029\u0011\u002F\u0054\u001D\u0074\u003E\u0087\u0070\u0052\u0077\u0064\u005F\u000F\u002F\u005B\u0074\u0035\u0017\u0067\n\u0033\u001C\u005F\u0056\u000B\u0031\u0034\u0080\u0018\u0051\u007F\u0066\u0040\u0013\u0047\u0059\u0071\u0028\u0016\u006F\u006F\u002C\u006C\u0063\u0057\u005B\u001F\u002E\u0055\u005F\u0053\u0060\u0067\u001C\u0074\u005F\u000C\u0073\n\u001A\u0039\u003B\u0038\n\u0020\u0050\u007F\u0011\u0009\u0086\u007D\u0048\u0013\u0063\u001A\u006F\u001B\u0048\u0077\u006D\u0059\u0059\u006A\u002A\u0040\u002B\u0048\u002D\u003E\u001E\u003E\u003B\u004F\u0030\u0065\u0015\u0043\u0054\u004F\u0074\u0049\u0025\u0037\u0069\u0031\u0065\u001C\u0049\u0043\u0031\u0047\u0024\u0059\u004E\u005A\u0061\u0011\u003E\u007C\u004C\u0032\u0065\u006A\u0033\u000E\u003E\u007A\u007C\u002F\u000C\u003C\u004F\u0078\u007C\u004C\u0030\u0062\u001C\u0029\u007C\u004D\u003E\u0015\u0037\u0070\u0025\u001C\u003E\u001B\u004B\u0024\u0078\u005F\u002D\u0047\u0040\u0015\\\u001A\u003A\u0079\u0040\u0076\u004A\u0038\u0011\u0060\u001C\u0014\u0011\u0040\u002D\u003E\u001E\u0014\u0011\u0047\u0031\u005E\u0011\u006A\u0011\u0047\u0085\u0027\u005F\u005F\u0045\u0031\u004F\u006C\u0047\u0041\u003C\u0047\u0084\u007A\u0046\u0072\u005F\u001A\u0014\u006C\u0043\u003A\u004F\u007F\u006B\u006B\u002A\u003A\u0055\u0068\u007E\u003A\u0060\u0081\u007B\u0044\u0074\u0058\r\u003C\u007A\u0045\u0078\u0083\u006B\u0045\u0012\u003E\u003D\r\u0042\u0024\u006E\r\u0082\u0079\u000B\u00F4\u000B\u0000\u0000\u00E0\u009E\u0001\u0048\u0000\u0064\u00DF\u0000\u0000\u00E0\u0012\u009F\u0040\u0034\u004B\u00B4\u0021\u0082\u0060\u0004\u000B\u0004\u00E0\u0002\r\u00E0\u00A8\u0060\u0006\u0009\u0006\u00C8\u002B\u0006\u0080\u0049\u008C\u0003\u0018\u0087\u0000\u0000\u0000\u001B\u0084\u0003\u0000\u0001\u001E\u0080\u0000\u0000\u0007\u0018\u0084\u0018\u0084\u0003\u0002\u0003\u001C\u0080\u0000\u0080\u0017\u0088\u000C\u0000\u008B\u0017\u0000\u0000\u0000\u009C\u0003\u0018\u009C\u0083\u0004\u002C\u0027\u001B\u0000\u0000\u0000\u0024\u003B\u0020\u0020\u0038\u0007\u0000\u0000\u0000\u0039\u0006\u0003\u0020\u0000\u0018\u0008\u00E0\u000E\u0000\u0000\u00E0\u0006\u0028\u0000\u00C8\u000C\u002E\u0000\u0000\u00E0\u000E\u0000\u00E8\u0006\u0000\u0060\u0011\u00C1\u001A\u0000\u0000\u00C0\u0003\u0018\u0008\u00D1\n\u0019\u0000\u0000\u00C0\u00CB\u0010\u0000\u0081\u0018\u0042\u0054\u00BE\u0003\u0094\u00CA\u0033\u0089\u0001\u0014\u00CA\u0034\u000B\u0023\u00D4\u008A\u0048\u0037\u0080\u00CA\u0021\u0000\u00C4\u003A\u0005\u00C0\u0038\u0015\u0007\r\u00C0\u0028\u0012\r\u0012\u00C0\u0020\u0011\u001E\u00C0\u0030\u000B\u0000\u0095\"\u0048\u00D5\u0002\u0020\u0040\u0008\u0095\u0062\u0000\u0060\u0008\u00D5\u0002\u0020\u0008\u00D5\"\u0000\u0048\u0000\u000C\u00F3\u0000\u0000\u0008\u00D5\"\u0000\u0000\u0048\u00D5\"\u0000\u0008\u0097\u0060\u0040\u0008\u00D5\"\u0042\u0080\u0040\u0008\u00B5\u0042\u0080\u0040\u0088\u0035\u0082\u0040\u0000\u00C8\u0035\u00C2\u0000\u0048\u00B5\n\u00B5\u0000\u00EC\u001B\u0008\u0024\u00C0\u0023\u005D\u0000\u0000\u00BD\u0002\u00FC\u0002\u0000\u005D\u00A2\u0000\u0044\u00AA\u0011\u0005\u00F0\u000E\u0000\u0008\u00FD\u0003\u0097\u0000\u0008\u0064\u009B\u0076\u0000\u0000\u0008\u00FF\u0000\u0028\u00D0\u0002\u0088\u0057\u00A8\u0000\u0004\u0059\u00E7\u0049\u0010\u0026\u0051\u008E\u0049\u0038\u0016\u0064\u0099\u006A\u0010\u0066\u0011\u00AA\u0055\u009A\u0008\u0025\u004A\u0098\u004A\u0008\u0025\u0052\u00A9\u0056\u0080\u0025\u0043\u00BA\u0058\u0004\u0019\u0004\u0014\u00CB\u003C\u00C8\u0036\u0001\u0054\u002B\u00CE\u0020\u00DA\u0004\u0011\u00EC\"\u00C5\u0010\u00C9\u0036\u0000\u0021\u0082\u001D\u0068\u0082\u0054\u0029\u0017\u00E9\u0012\u00E4\u001B\u0060\u0099\u0006\u0004\u003A\u00C9\u0096\u0065\u0098\u0040\u0090\n\u00ED\u0010\u0065\u0002\u0099\u0064\u0091\u0060\n\u0094\u000E\u0091\u006A\u0005\u007E\u0091\u0012\u0089\u0064\u002A\u00E5\u0030\u004A\u0005\u001A\u00AA\u0015\u006A\u0085\u00F8\u0003\u0095\u006A\u0000\u00E4\u0011\u006B\u0085\u002A\u0014\u0080\u003B\u00EE\u0010\u0014\u00A9\u003C\u00EA\u0001\u0010\u002E\u0059\u0089\u0014\u0012\u00E4\u0029\u0011\u0016\u0068\u0084\u00F6\u0081\u0078\u0080\u0000\u007F\u000B\u00B9\u00E4\u0000\u008A\u0035\u00E9\u0002\u0000\u006C\u00B9\u00C6\u0000\r\u0002\u0048\u0017\u00A8\u0040\u0006\u0099\u0037\u0028\u0049\u00A6\u0050\u002F\u0090\u0069\u0014\u0088\u0037\u0048\u00E0\u0017\u0000\u0096\u0063\u0010\u0088\u0004\u0043\u0020\u0010\u0088\u0086\u0061\u0014\u0088\u0000\u0043\u0034\u0088\u0004\u0043\u0030\u0088\u0098\u002F\u0000\u0040\u0098\u0027\u0000\u0010\u0040\u0098\u0027\u0000\u0040\u0098\u0027\u0010\u0040\u0088\u0027\u0010\u0040\u0084\u0012\u0040\u0029\u0084\u0012\u0020\u0040\u0029\u0084\u0012\u0040\u0029\u0084\u0012\u0040\u0029\u0084\u0012\u0040\u0029\u0072\u0000\u0049\u0084\u0072\u0000\u0000\u0009\u0084\u0072\u0040\u0009\u0084\u0032\u0000\u0049\u0084\u0032\u0000\u0049\u0084\u0000\u0009\u00A4\u0052\u0000\u0000\u0009\u00A4\u0052\u0080\u0009\u00A4\u0052\u0000\u0009\u00E4\u0012\u0040\u0009\u00A4\u0052\u0004\u00B1\u005B\u0001\u0004\u00B0\u0040\u005A\u0021\u0004\u00F1\u001A\u0001\u0004\u00F0\n\u0001\u0004\u00F1\u008A\u0001\u0029\u00D2\u0000\u0004\u0029\u00C2\u00D1\u0000\u0004\u0029\u00D6\u0000\u0004\u0029\u00C2\u0095\u0000\u0028\u0096\u0041\u0004\u00D1\u0024\u0080\u0006\u0079\u0018\u00B7\u0040\u002E\u00D3\u0024\u0040\u000C\u00F3\u0015\u00C8\"\u005D\u0021\u0080\u002E\u00D4\u002B\u0056\u0001\u00B4\u0008\u0067\u008A\u0021\u00DC\u0023\u0096\u0021\u00C6\u0009\u005E\u0001\u00A0\u0009\u00D6\u0021\u00B2\u0017\u0058\u0085\u00AA\u0055\u0000\u00B8\u0001\u00E6\u0013\u0068\u0084\n\u0017\u0020\u004D\u00A2\u001D\u0060\r\u0046\u0029\u0008\u0092\u006C\u0001\u0078\u0018\u0092\u006D\u0021\u0018\u0082\u006C\u0001\u00F0\u0082\u0064\u0029\u0090\u0092\u0066\u007C\u00C1\u0018\u0086\u0038\u0087\u00E1\u0018\u00C6\u003C\u00C1\u0008\u0006\u0030\u00C5\u0010\u00EA\u0010\u008D\u0018\u00D5\u0094\u0060\n\u0045\u0010\u00AD\u0060\n\u00CD\u0094\"\u0000\u00CD\u0014\"\u0008\u00C5\u0014\u00E0\n\u0002\u00E9\u002D\u0010\u00A2\u000F\u00E8\u006F\u0010\"\u00C9\u00CF\u0010\u0020\u0009\u00EE\u0010\u002A\u00A1\u006E\u0010\u005E\u0010\u00A1\u0001\u0040\u000E\u00B0\u00A7\u0008\u0047\u0090\u0026\u0009\u0044\u0010\u00E6\u0009\u0064\u0010\u00AE\u0009\u0092\u0031\u00CB\u0000\u0006\u0060\u001F\u00C8\u002E\u0092\u0035\u004A\u0004\u0010\u00B0\u004F\u0028\u0096\u0074\u000B\u002E\u0001\u0003\u0005\n\u000F\u0014\u001E\u0028\u0032\u00FF\u000E\u000E\u001A\u0028\u0036\u0042\\\u007A\u00A0\u00C2\u00D0\u00DC\u00E0\u00E4\u0000\u0005\u0002\u0004\u0006\u0009\u0008\u0070\u0008\u0018\u0004\u0019\u0009\u0076\u007F\u0000\u008B\u0004\u002E\u0002\u0090\u0001\u0091\u0005\u0034\u0003\u0095\u0009\u0020\u0060\u0006\u004D\u0006\u004E\u0008\u008F\u0007\u0053\u0007\u0054\u0042\u0000\u0009\u0096\u002A\u0002\u008C\u0008\u004D\u0006\u0090\u0004\u0092\u0007\u0095\u0009\u0053\u0080\u0003\u0085\u0003\u008A\u0004\u008B\u0004\u008C\u0005\u004E\u0000\u00CF\u0007\u008F\u0004\u0091\u0004\u0092\u0007\u0053\u0006\u0054\u0000\u00B5\u0009\u0096\u0046\u0083\u0002\u0083\u0003\u0084\u0007\u0083\u0008\u0085\u00AA\u0006\u008B\u0084\u00AC\u0005\u004D\u0085\u001A\u0084\u00D1\u0004\u0092\u0005\u0054\u0005\u00B5\u0004\u0096\u0009\u00CF\u0059\u00C1\u00E0\u0083\u0001\u0082\u0003\u0083\u00C5\u0083\u0006\u0083\u0008\u0083\u00AA\u0083\u00CB\u0004\u008C\u0004\u004D\u0005\u00CF\u0003\u008F\u0004\u0091\u0083\u00B2\u0003\u0054\u0005\u00B5\u0082\u0017\u0048\u0000\u0089\u001A\u003E\u0084\u0003\u0005\u0085\u0085\u0008\u0005\u008A\u0005\u008B\u0082\u008C\u0006\u004E\u0003\u00CF\u0004\u0090\u0082\u0091\u0004\u0092\u0005\u0053\u0004\u0034\u0003\u00B5\u0003\u0095\u0000\u0018\u0089\u001A\u006C\u0008\u0020\u0086\u004D\u0005\u0090\u0008\u0091\u0086\u0053\u0085\u0096\u0009\u008B\u0064\u0086\u002E\u0086\u008F\u0008\u0092\u0086\u0054\u0008\u0095\u0009\u008C\u0080\u0009\u001B\u0009\u001B\u0080\u0009\u001C\u0009\u001C\u0080\u0009\u0018\u0009\u0018\u0005\u0006\n\u0010\u000F\u0000\u0000\r\u000B\r\r\u000C\u0001\u0003\u0004\u0005\u0002\u0006\u0007\u0008\u0000\n\u0005\u0009".ToCharArray();
			
	static clOHHB_V23() {
		//clZ80RAM.mDataTable = mDataTable;
	}

#endif

};






using System;
public class SlotInterfaceResource {

/////////////////////////////////////////////////////////////////
//
// リソース操作
//
/////////////////////////////////////////////////////////////////
	/**
	 * リソースからのデータ取得
	 * リソースに置いたファイルをバイト配列に取得
	 * @param strPath リソースに置いた取得ファイル名
	 *     「resource:///」はフレームワーク側が付けるので、
	 *     呼び出し側では付けないでください。
	 * @return ファイル中身バイト配列
	 * @throws Exception
	 */
	public static sbyte[] getResourceData(string strPath) 
	{
        // TODO C#移植　リソース読む処理
        //InputStream input = null;
        sbyte[] loadBytes = null;

        loadBytes = SaveData.LoadChipData();
        //input = Connector.openInputStream("resource:///" + strPath);
        //int iSize;
        //ByteArrayOutputStream ar = new ByteArrayOutputStream();
        //byte[] arBuff = new byte[ 1024 ];
        //while( ( iSize = input.read( arBuff ) ) != -1 ) {
        //    ar.write( arBuff, 0, iSize );
        //}
        //ar.close();
        //loadBytes = ar.toByteArray();
        //if( input != null ) {
        //    input.close();
        //}         

		return loadBytes;
        
	}

    // TODO C#移植 スタブ
    [Obsolete]
    public class Image : UnityEngine.Texture {
    }
	
	
#if	_DOCOMO		//{
	 /**
	 * リソースイメージ読み込み
	 * @param strName リソースファイル名
	 * @return 読み込んだイメージ
	 * @throws Exception
	 */
    [Obsolete]
	public static Image loadImage( string strName )
	{
        return null;
        // TODO C#移植 スタブ
//#if	_DOCOMO	// {
//        MediaImage mi = MediaManager.getImage( "resource:///" + strName );
//        mi.use();
//        return mi.getImage();
//#else			// } {
//        return Image.createImage(strName);
//#endif			// }
		

	}
	
	/**
	 * スクラッチパッドイメージ読み込み（注: DoJaのみ利用可能）
	 * @param iPos スクラッチパッド オフセット
	 * @param iSize イメージサイズ
	 * @return 読み込んだイメージ
	 * @throws Exception
	 */
    [Obsolete]
	Image loadImage( int iPos, int iSize )
	{
        // TODO C#移植 スタブ
        return null;
        //MediaImage mi = MediaManager.getImage( "scratchpad:///0;pos=" + iPos + ",length=" + iSize );
        //mi.use();
        //return mi.getImage();
	}
	
	/**
	 * リソースイメージ読み込み
	 * @param strName リソースファイル名
	 * @return 読み込んだイメージ
	 * @throws Exception
	 */
    [Obsolete]
	Image webImage( String strName )
	{
        // TODO C#移植 スタブ
        return null;
        //MediaImage mi = MediaManager.getImage( strName );
        //mi.use();
        //return mi.getImage();
	}
	
    [Obsolete]
    // TODO C#移植 スタブ
    public class MediaSound {}

	/**
	* リソースイメージ読み込み
	* @param strName リソースファイル名
	* @return 読み込んだイメージ
	* @throws Exception
	*/
    
    [Obsolete]
	public MediaSound loadSound( String strName )
	{
    // TODO C#移植 スタブ
        return null;
        //MediaSound ms = MediaManager.getSound("resource:///" + strName );
        //ms.use();
        //return ms;

	}
#else				//}{
	 /**
	 * リソースイメージ読み込み
	 * @param strName リソースファイル名
	 * @return 読み込んだイメージ
	 * @throws Exception
	 */
	public Image loadImage( String strName )
	{
		return Image.createImage("/" + strName);
		//MediaImage mi = MediaManager.getImage( "resource:///" + strName );
		//mi.use();
		//return mi.getImage();
	}
	/**
	 * リソースイメージ読み込み
	 * @param strName リソースファイル名
	 * @return 読み込んだイメージ
	 * @throws Exception
	 */
	Image webImage( String strName )
	{
		byte[] img_buf = _connectSimple( strName, null );
		Image img = Image.createImage( img_buf, 0, img_buf.Length );
		return img;
	}

	public Phrase loadSound( String strName )
	{
		byte[]	buf;
		Phrase	ret;
		buf = make_ResData(strName);
		ret = new Phrase(buf);
		return ret;
	}
#endif				// }
	//-----------------------------------------------------------------------------
	// リソースからバイナリデータ取り出し
	//
	// s                ファイル名
	//
	// return            正常に展開できた時はバイト配列が返却される(失敗時はnullが返る)
	//
	// ※引数sにはファイル名のみを記述する
	//   "resource:///"などの記述は一切不要
	//-----------------------------------------------------------------------------
	[Obsolete]
    byte[] make_ResData( String s )
	{
    // TODO C#移植 スタブ
        return null;
        //InputStream inst = null;
        //byte[] b = null;
        //int len = 0;
		
        //try {
        //    // データロード
        //    inst = Connector.openInputStream("resource:///" + s);
        //    // ファイルサイズを調べる
        //    len = inst.available();
        //    if( len > 0 ){
        //        // データを読み込む
        //        b = new byte[len];
        //        inst.read(b);
        //    }
        //    // クローズ
        //    inst.close();
        //}
        //catch( Exception e ) {
        //    Defines.TRACE("resdata err "+ e );
        //    try {
        //        if( inst != null ){
        //            inst.close();
        //        }
        //    }
        //    catch( Exception e_2 ) {
        //        Defines.TRACE("resdata err2 "+e_2 );
        //    }
        //    return null;
        //}
        //// 正常終了
        //return b;
	}
	
/////////////////////////////////////////////////////////////////
//
// スクラッチパッド操作
//
/////////////////////////////////////////////////////////////////
	// セーブデータの書き込み
    [Obsolete]
	public void outSaveData()
	{
    // TODO C#移植 スタブ
        //PRINT("*--> is Save");
        //saveData[OPT_SOUND_VOL]  = opt_value[SAVE_SOUND_VOL] ;
        //saveData[OPT_AUTO_MEDAL] = opt_value[SAVE_AUTO_MEDAL];
        //saveData[OPT_AUTO_PLAY]  = opt_value[SAVE_AUTO_PLAY] ;
        //saveData[OPT_BONUS_CUT]  = opt_value[SAVE_BONUS_CUT] ;
        //saveData[OPT_MEOSHI]     = opt_value[SAVE_MEOSHI]    ;
        //saveData[OPT_OSHIJUN]    = opt_value[SAVE_OSHIJUN]   ;
        //saveData[OPT_INIT]       = opt_value[SAVE_INIT]      ;
        //saveData[OPT_SETTING_NOW]= opt_value[SAVE_SETTING_NOW];
		
        //Defines.TRACE("Save BONUS_CUT" + opt_value[SAVE_BONUS_CUT]);
		
        //if( saveRecord(saveData , 2, SAVE_MAX) )
        //{	// エラー処理
			
        //}
	}
	
	// セーブデータの読み込み
    [Obsolete]
	public void inSaveData()
	{
    // TODO C#移植 スタブ
        //PRINT("*--> is Load");
        //if( loadRecord(saveData , 2, SAVE_MAX) )
        //{	// エラー処理
			
        //}
        ////opt_value = new int[SAVE_MAX];
        //opt_value[OPT_SOUND_VOL]  = saveData[SAVE_SOUND_VOL] ;
        //opt_value[OPT_AUTO_MEDAL] = saveData[SAVE_AUTO_MEDAL];
        //opt_value[OPT_AUTO_PLAY]  = saveData[SAVE_AUTO_PLAY] ;
        //opt_value[OPT_BONUS_CUT]  = saveData[SAVE_BONUS_CUT] ;
        //opt_value[OPT_MEOSHI]     = saveData[SAVE_MEOSHI]    ;
        //opt_value[OPT_OSHIJUN]    = saveData[SAVE_OSHIJUN]   ;
        //opt_value[OPT_INIT]       = saveData[SAVE_INIT]      ;
        //opt_value[SAVE_SETTING_NOW]=saveData[OPT_SETTING_NOW];
		
        //Defines.TRACE("Load BONUS_CUT" + opt_value[SAVE_BONUS_CUT]);
	}


	
/////////////////////////////////////////////////////////////////
//
// サーバーデータ操作
//
/////////////////////////////////////////////////////////////////

	//#define GET_Z80_TO_STR(str) String.valueOf(str & 0xFFFF)
	//#define GET_STR_TO_Z80(str) (char)(Integer.valueOf(str).intValue() & 0xFFFF);
	
	//#define GET_Z80_TO_STR(str) PadLeft(Integer.toHexString(str & 0xFFFF), 4, "0")	// 4ﾊﾞｲﾄ埋めにする
	//#define GET_Z80_TO_STR(str) Integer.toHexString(str & 0xFFFF)	// 4ﾊﾞｲﾄ埋めをしない
	//#define GET_STR_TO_Z80(str) (char)Integer.parseInt(str, 16)// 符号付きで変換されるので一つ上の型からキャストする

    public static string GET_Z80_TO_STR(ushort str) { return (str & 0xFFFF).ToString(); }	// 4ﾊﾞｲﾄ埋めをしない
	//public static sbyte GET_STR_TO_Z80(string str) { return (sbyte)long.Parse(str); }// 符号付きで変換されるので一つ上の型からキャストする
    public static ushort GET_STR_TO_Z80(string str) { return ushort.Parse(str); }

	String appData_str = "";

	// 固定長の履歴データを生成する
    [Obsolete]
	private String getHistoryString()
	{
		int i;
		String str = "";
        // TODO C#移植
        //// 履歴情報を表示
        //for( i = 0; i < 10; i++ ) {
        //    if( str != "" ) {
        //        str += ",";
        //    }
        //    str += bonus_history[ i * 3 + 0 ] + ",";
        //    str += bonus_history[ i * 3 + 1 ] + ",";
        //    str += bonus_history[ i * 3 + 2 ] + "";
        //}
		return str;
	}


	// 固定長のgp汎用データを生成する
    [Obsolete]
	private String getGpMemberString(int type)
	{
		int i;
		int tmp;
		String str = "";
		
		if(type == -1)
		{	// 初期化用
			str += "-1,0,0,0,0,0";
		}
		if(type == 0)
		{	// 通常はこっち
            // TODO C#移植
            //str += setting_sub_num + ",";

            //////////////////////////////////////////////////////
            //// バージョン11.1以上から付与
            //if( gpif_naibucyu_f == true)
            //{
            //    tmp = 1;
            //} else {
            //    tmp = 0;
            //}
            //str = str + tmp + ",";
			
            //if( gpif_bonuscyu_f == true)
            //{
            //    tmp = 1;
            //} else {
            //    tmp = 0;
            //}
				
            //str = str + tmp + ",";
			
            //str = str + bonus_getcoin + ",";
            //str = str + bonus_type + ",";
            //str = str + bonus_incount;
            ////str = str + dai_bns_rot;
            //////////////////////////////////////////////////////
		}
		return str;
	}

	// 固定長の履歴データを更新する
    [Obsolete]
	private int setHistoryString( string[] split_str, int index )
	{
		int i;
//		// エラー制御
//		if( str == null || str.equals("") || str.equals("null") ) {
//			str = "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0";
////			str = "100,1,200,110,1,400,120,3,600,130,3,800,140,1,1000,150,1,1100,160,3,1200,170,3,1400,180,1,1600,190,1,1800";
//		}
//		int[] historyData = new int[ GP_APP_DATA_SIZE ];
//		String[] split_str = getSplitString( str, ',' );
        // TODO C#移植
        return 0;
		// 履歴情報を表示
        //for( i = 0; i < GP_APP_DATA_SIZE; i += 3 ) {
        //    bonus_history[ i + 0 ] = Integer.parseInt( split_str[ index + i + 0 ] );
        //    bonus_history[ i + 1 ] = Integer.parseInt( split_str[ index + i + 1 ] );
        //    bonus_history[ i + 2 ] = Integer.parseInt( split_str[ index + i + 2 ] );
        //}
        //return (index + i);
	}

	// 固定長のgp汎用データを更新する
    [Obsolete]
	private int setGpMemberString( string[] split_str, int index )
	{
        // TODO C#移植
        return 0;   
//        int i=0;
//        int tmp;
////		int[] gpMember = new int[ GP_APP_DATA_SIZE_M ];
////		for( i = 0; i < GP_APP_DATA_SIZE_M; i++ ) {
////			gpMember[ i ] = Integer.parseInt( split_str[ index + i ] );
////		}
//        setting_sub_num = Integer.parseInt( split_str[ index + i ] );
//        i++;
		
//        if( mOmatsuri.maj_ver >= 12 && mOmatsuri.sub_ver >= 0)
//        {// バージョン11.1以上から付与
//            // 追加して欲しいapp_data
//            //	bool gpif_naibucyu_f
//            //	bool gpif_bonuscyu_f
//            //	int     bonus_getcoin
//            //	int     bonus_type
//            //	int     bonus_incount
//            //	int     dai_bns_rot

//            tmp = Integer.parseInt( split_str[ index + i ] ); i++;
//            if(tmp == 1)
//            {
//                gpif_naibucyu_f = true;
//            }
//            else
//            {
//                gpif_naibucyu_f = false;
//            }

//            tmp = Integer.parseInt( split_str[ index + i ] ); i++;
//            if(tmp == 1)
//            {
//                gpif_bonuscyu_f = true;
//PRINT_PRI(54,"?");
//            }
//            else
//            {
//                gpif_bonuscyu_f = false;
//            }

//            bonus_getcoin = Integer.parseInt( split_str[ index + i ] ); i++;
//            bonus_type = Integer.parseInt( split_str[ index + i ] ); i++;
//            bonus_incount = Integer.parseInt( split_str[ index + i ] ); i++;
//            //dai_bns_rot = Integer.parseInt( split_str[ index + i ] ); i++;
//        }
//        return( index + i );
	}

	// RAMﾃﾞｰﾀやレジストリデータを文字列化する
	// ﾃﾞｰﾀ形式は16進数の4ﾊﾞｲﾄで桁数あわせ
    [Obsolete]
	private String z80ToString()
	{
		int i;
		String str;
		
		str = "";
		
        // TODO C#移植

//        //clZ80RAM.mWorkRam[0] = 0xFFFF;
//        //clZ80RAM.mWorkRam[1] = 0xFF00;
//        //clZ80RAM.mWorkRam[2] = 0x00FF;
//        int tmp;
////		System.out.println("★RAMデータの登録★:" + (DEF_WORKEND + 1));
//        for(i = 0; i < (DEF_WORKEND + 1); i++)
//        {
////System.out.println("clZ80RAM.mWorkRam["+i+"]="+GET_Z80_TO_STR(clZ80RAM.mWorkRam[i]));
////System.out.println("clZ80RAM.mWorkRam["+i+"]="+((int)clZ80RAM.mWorkRam[i]));
//            // RAMデータは16進数の4ﾊﾞｲﾄ揃いで格納する
//            str = str + GET_Z80_TO_STR(clZ80RAM.mWorkRam[i]) + ",";
//        }
//        //表レジスタ
//        str = str + GET_Z80_TO_STR( clZ80RAM.front.AF ) + ",";
//        str = str + GET_Z80_TO_STR( clZ80RAM.front.BC ) + ",";
//        str = str + GET_Z80_TO_STR( clZ80RAM.front.DE ) + ",";
//        str = str + GET_Z80_TO_STR( clZ80RAM.front.HL ) + ",";
//        str = str + GET_Z80_TO_STR( clZ80RAM.front.IX ) + ",";
//        str = str + GET_Z80_TO_STR( clZ80RAM.front.IY ) + ",";
//        //裏レジスタ
//        str = str + GET_Z80_TO_STR( clZ80RAM.back.AF ) + ",";
//        str = str + GET_Z80_TO_STR( clZ80RAM.back.BC ) + ",";
//        str = str + GET_Z80_TO_STR( clZ80RAM.back.DE ) + ",";
//        str = str + GET_Z80_TO_STR( clZ80RAM.back.HL ) + ",";
//        str = str + GET_Z80_TO_STR( clZ80RAM.back.IX ) + ",";
//        str = str + GET_Z80_TO_STR( clZ80RAM.back.IY );
		
		
//#if	_DOCOMO
//        Defines.TRACE("z80ToString:"+str);
//#endif
		
		return str;
	}
		
	// RAMﾃﾞｰﾀやレジストリデータを文字列から復元する
	// ﾃﾞｰﾀ形式は16進数の4ﾊﾞｲﾄで桁数あわせ
	private int StringToZ80(string[] appData, int top)
	{
		int i;
		//String tmp[];
		
		//tmp = Tool.getSplitString(str, ',');
		for(i = 0; i < clZ80RAM.mWorkRam.Length; i++)
		{
			// RAMデータは16進数の4ﾊﾞｲﾄ揃いで格納している
			clZ80RAM.mWorkRam[i] = GET_STR_TO_Z80(appData[top + i]);
		}
		//表レジスタ(16進数の4ﾊﾞｲﾄ揃いで格納している)
		clZ80RAM.front.AF = GET_STR_TO_Z80(appData[top + i]); i++;
		clZ80RAM.front.BC = GET_STR_TO_Z80(appData[top + i]); i++;
		clZ80RAM.front.DE = GET_STR_TO_Z80(appData[top + i]); i++;
		clZ80RAM.front.HL = GET_STR_TO_Z80(appData[top + i]); i++;
		clZ80RAM.front.IX = GET_STR_TO_Z80(appData[top + i]); i++;
		clZ80RAM.front.IY = GET_STR_TO_Z80(appData[top + i]); i++;
		//裏レジスタ(16進数の4ﾊﾞｲﾄ揃いで格納している)
		clZ80RAM.back.AF = GET_STR_TO_Z80(appData[top + i]); i++;
		clZ80RAM.back.BC = GET_STR_TO_Z80(appData[top + i]); i++;
		clZ80RAM.back.DE = GET_STR_TO_Z80(appData[top + i]); i++;
		clZ80RAM.back.HL = GET_STR_TO_Z80(appData[top + i]); i++;
		clZ80RAM.back.IX = GET_STR_TO_Z80(appData[top + i]); i++;
		clZ80RAM.back.IY = GET_STR_TO_Z80(appData[top + i]); i++;
		
		//Defines.TRACE("ramdata:" + str);
		return (top + i);
	}	
	
	
	// ゲーム情報をサーバーに送る文字列にする
	// グローバル変数にappDataを生成します
	// 引数
	// str		サーバーからの情報文字列(カンマ区切りの文字列)
	// max		必要上の数
	public bool setAppDataString()
	{
		String str;
		String strRam;
		String strOhana;
		String strHis;
		
		Defines.TRACE("★★★ゲーム情報をサーバー文字列に変換する★★");

		// サーバーデータのバージョン
		str = Defines.SVR_DATA_MAJ_ERSION + "," + Defines.SVR_DATA_SUB_VERSION;
		
		// Z80系の情報
		strRam = z80ToString();
#if	_DOCOMO
		Defines.APP_TRACE("Z80系:" + strRam);
#endif
		// ゲーム本体の情報
		strOhana = mOmatsuri.OmatsuriToString();
#if	_DOCOMO
		Defines.APP_TRACE("ゲーム本体:" + strOhana);
#endif
		// GP関係の情報
		strHis = getHistoryString() + "," + getGpMemberString(0);

#if	_DOCOMO // TODO #ifdefの不足？ とりあえず_DOCOMOを入れておく
        Defines.APP_TRACE("GP関係:" + strHis);
#endif
		// グローバルのアプリデータに書き込んでおく
		appData_str = str + "," + strRam + "," + strOhana + "," + strHis;
#if	_DOCOMO
		Defines.APP_TRACE("appData_str[0]:" + appData_str.Substring(0, 300));
		Defines.APP_TRACE("appData_str[1]:" + appData_str.Substring(300));
#endif

		{
			String[] split_str = Tool.getSplitString( appData_str, ',' );
			PublicDefine.PRINT_PRI(50," ");
			for( int i = 0; i < split_str.Length;) {
//				String astr = "0x"+Integer.toHexString(i)+"|";
				String astr = i+"|";
				for( int j = 0; j < 32 && i < split_str.Length; i++,j++ ) {
//					astr += Integer.toHexString(Integer.parseInt(split_str[i]))+",";
					astr += split_str[i]+",";
				}
				PublicDefine.PRINT_PRI(50,astr);
			}
		}

		return true;
	}
	
	
	// サーバーからのゲーム情報の復元
	// 引数
	// str		サーバーからの情報文字列(カンマ区切りの文字列)
	public bool getAppDataString(String str)
	{
		string[] split_str;
		//String ram_str;
		//String gp_str;
		int	index = 0;

        PublicDefine.PRINT_PRI(55, "★★サーバーからの情報復元★★:" + Defines.APP_SERVER_DATA_SIZE + ":" + str);
		
		// エラー制御
		if( str == null || str == "" || str == "null" ) {
			PublicDefine.PRINT_PRI(55,"★アプリデータの初期化");

            str = Defines.SVR_DATA_MAJ_ERSION + "," + Defines.SVR_DATA_SUB_VERSION + "," + 
				z80ToString() + "," + 
				mOmatsuri.OmatsuriToString(true) + "," + 
				"0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0" + "," + 
				getGpMemberString(-1);

			{
				String[] _split_str = Tool.getSplitString( str, ',' );
				PublicDefine.PRINT("appData_length="+_split_str.Length);
				for( int i = 0; i < _split_str.Length;) {
					String _str = "";
					for( int j = 0; j < 4 && i < _split_str.Length; i++,j++ ) {
						if( j == 0 ) {
							_str = "|";
						}
						else {
							_str += ",";
						}
						_str += _split_str[i];
					}
					PublicDefine.PRINT(_str);
				}
			}





#if	_DOCOMO
			Defines.APP_TRACE("appData初期化:" + str);
#endif
		}
		//TRACE("復元:" + str);
		
		split_str = Tool.getSplitString( str, ',' );















		
		if( split_str == null)
		{	// 
			Defines.TRACE("★スプリットに失敗");
			return false;
		}
//		if( split_str.Length != APP_SERVER_DATA_SIZE)
//		{	// 数がおかしい
//			TRACE("配列の数がおかしいよ！！！！:" + split_str.Length + ":" + APP_SERVER_DATA_SIZE + ":" + APP_SERVER_DATA_SIZE);
//			//split_str[-1] = "";
//			split_str[0] = "-1";
//		}
		
		mOmatsuri.maj_ver = 0;
		mOmatsuri.sub_ver = 0;
		
		// サーバーのバージョン取得
		mOmatsuri.maj_ver = int.Parse(split_str[index]);
		index++;
		if( mOmatsuri.maj_ver >= 12 && mOmatsuri.sub_ver >= 0)
		{	// バージョン11以上からサブのバージョンを付与
            mOmatsuri.sub_ver = int.Parse(split_str[index]);
			index++;
			
			if( split_str.Length != Defines.APP_SERVER_DATA_SIZE)
			{	// 数がおかしい
                Defines.TRACE("配列の数がおかしいよ！！！！:" + split_str.Length + ":" + Defines.APP_SERVER_DATA_SIZE + ":" + Defines.APP_SERVER_DATA_SIZE);
				getAppDataString("");
				//split_str[-1] = "";	// 強制落とし用
			}
		}
		
		if( (mOmatsuri.maj_ver) == 10 || ( (mOmatsuri.maj_ver >= 12) && (mOmatsuri.sub_ver >= 0)) )
		{	// バージョン10と12以上の読み込み方法
			PublicDefine.PRINT_PRI(55,"★★★★★サーバーバージョン:"+mOmatsuri.maj_ver+"." + mOmatsuri.sub_ver + ":" + split_str.Length + "★★★★★");
			
			//index++;
			// Z80系の情報
			index = StringToZ80(split_str, index);
			//index = SYSTEM_SIZE;
			
			
			// ゲーム本体の情報
			Defines.TRACE("ゲーム本体の情報:" + index);
			index = mOmatsuri.StringToOmatsuri(split_str, index);
			
			// GP関係の情報
			//setSplitAppDataToHistory(str);
			//PRINT( "*HISTORY:" + gp_str );
			Defines.TRACE("GP関係の情報1:["+index+"]=" + split_str[index]);
			index = setHistoryString( split_str, index );

			// GP関係の情報
			Defines.TRACE("GP関係の情報2:["+index+"]=" + split_str[index]);
			index = setGpMemberString( split_str, index );
		}
		else
		{	// 違う時
			// バージョン11も初期化してしまう
			Defines.TRACE("★★★★★サーバーバージョン違い★★★★★");
						
			getAppDataString("");
		}
		
		Defines.TRACE("サーバーからの情報復元 おわり");
		
		return true;
	}
	
	//==================================================================================
	//       指定の文字数になるまで先頭を文字で埋めます。
	//
	// @Param    stTarget    処理対象となる文字列。
	// @Param    iLength     文字の長さ。
	// @Param    [chOne]     埋める文字。
	// @Return               先頭を指定の文字で iLength の長さまで埋められた文字列。
	//==================================================================================
	public static String PadLeft(String stTarget, int iLength , String chOne)
	{
		while (stTarget.Length < iLength)
		{
			stTarget = chOne + stTarget;
		}
		//PadLeft = Right(stTarget, iLength)
		//Defines.TRACE("stTarget:"+stTarget);
		return stTarget;
	}

}

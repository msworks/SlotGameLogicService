using System;

public class SlotInterfaceResource
{
	public static sbyte[] getResourceData(string strPath) 
	{
        sbyte[] loadBytes = null;
        loadBytes = SaveData.LoadChipData();

		return loadBytes;
	}

    public static string GET_Z80_TO_STR(ushort str) { return (str & 0xFFFF).ToString(); }	// 4ﾊﾞｲﾄ埋めをしない
    public static ushort GET_STR_TO_Z80(string str) { return ushort.Parse(str); }

	// 固定長の履歴データを生成する
    [Obsolete]
	private String getHistoryString()
	{
		int i;
		String str = "";
		return str;
	}

	// 固定長のgp汎用データを生成する
    [Obsolete]
	private String getGpMemberString(int type)
	{
		int i;
		String str = "";
		
		if(type == -1)
		{	// 初期化用
			str += "-1,0,0,0,0,0";
		}
		if(type == 0)
		{	// 通常はこっち
		}
		return str;
	}

	// 固定長の履歴データを更新する
    [Obsolete]
	private int setHistoryString( string[] split_str, int index )
	{
        return 0;
	}

	// 固定長のgp汎用データを更新する
    [Obsolete]
	private int setGpMemberString( string[] split_str, int index )
	{
        return 0;   
	}

	// RAMﾃﾞｰﾀやレジストリデータを文字列化する
	// ﾃﾞｰﾀ形式は16進数の4ﾊﾞｲﾄで桁数あわせ
    [Obsolete]
	private String z80ToString()
	{
		return "";
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

			Defines.APP_TRACE("appData初期化:" + str);
		}
		//TRACE("復元:" + str);
		split_str = Tool.getSplitString( str, ',' );
		
		if( split_str == null)
		{	// 
			Defines.TRACE("★スプリットに失敗");
			return false;
		}
		
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

}

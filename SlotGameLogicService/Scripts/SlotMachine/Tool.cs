using System;
using System.Text;
public class Tool {

	// 内部でバッファ
	static public string[] split_buf = new string[ 600 ];

	static protected int delim_split( sbyte[] bs, char delim )
	{
		return delim_split( bs, delim, split_buf ,0 );
	}

	/**
	 * 改行コードでの分割（呼び出すと前の結果がクリアされます）
	 * @return int count 分割後の個数を返す
	 */
	static public int delim_split( sbyte[] bs, char delim, string[] split_buf )
	{
		return delim_split( bs, delim, split_buf ,0 );
	}

	/**
	 * 改行コードでの分割（呼び出すと前の結果がクリアされます）
	 * numをこえたらバッファーに文字を入れていく
	 * @return int count 分割後の個数を返す
	 */
    [Obsolete]
	static public int delim_split( sbyte[] bs, char delim, string[] split_buf , int num )
	{
        // TODO C#移植 
        return -1;
//        int cur = 0; // スタート
//        int c = 0; // バッファの個数
//        int cc;
//        int strNum = 0;
//        for( int i = 0; i < bs.Length; i++ ) {
//            cc = bs[i]; // 判定コードのキャラクターコードのセット
//            if( cc >= 0x81 && cc <= 0x9f || cc >= 0xe0 && cc <= 0xef || cc == 0xf8 || cc == 0xf9 ) {
//                // マルチバイト文字の場合
//                i++;
//            }
//            else if( bs[ i ] == delim ) { // 区切り文字指定
//                if( cur == i ) {
//                    cur = i+1;
//                    if( c >= num ) {
//                        split_buf[ strNum ] = "";
//                        strNum++;
//                    }
//                    c++;
//                    continue; // 区切りが並んだ場合は空白を入れてスキップ（nullにするかは後日談）
//                }
//                int skip = 0;
//                if( bs[ i - 1 ] == '\r'  ) {
//                    // これもSkip対象
//                    skip = 1;
//                }
//                // 区切り文字の場合
//                if( c >= num ) {
//                    split_buf[ strNum ] = new string( bs, cur, i-cur - skip );
//                    strNum++;
//                }
////				split_buf[ c ] = s.substring( cur, i );
//                cur = i + 1;
//                c++;
//                if( split_buf.Length <= c - num ) {
//                    // ここまで
//                    return strNum;
//                    //return c;
//                }
//            }
//        }
//        if( cur < bs.Length ) {
//            // 最後の文字列を追加
//            if( c >= num ) {
//                split_buf[ strNum ] = new string( bs, cur, bs.Length - cur );
//            }
//            c++;
//        }
//        // 不要部分のクリア
//        for( int i = c; i < split_buf.Length; ++ i ) {
//            split_buf[ i ] = null;
//        }
//        return	strNum;
//        //return c; // 分割後の個数を返す
	}

	/**
	 * 任意文字での分割
	 */
	public static void delimSplitStr( string target, string find, string[] outValue )
	{
		if( target == null || find == null || outValue == null ) {
			return ;
		}
		int findStrLen = find.Length;
		int fndIndex = 0;
		int curIndex = 0;
		int elemIndex = 0;
		while( true ) {
			if( outValue.Length <= elemIndex ) {
				break ;
			}
			if( ( fndIndex = target.IndexOf( find, curIndex ) ) != -1 ) {
				outValue[ elemIndex++ ] = target.Substring( curIndex, fndIndex );
				curIndex = fndIndex + findStrLen;
			}
			else {
				outValue[ elemIndex ] = target.Substring( curIndex );
				break;
			}
		}
	}

	/**
	 * グリパチ通信レスポンスのひとつのカラム名と値の組から値のみを取得する。
	 *
	 * @param  カラム名と値の組（例：column=123）
	 * @return カラム値。
	 *         但し、以下の異常時はnullを返却する。
	 *         ・カラム名と値の組がnull
	 *         ・カラム名と値の区切り文字（"="）が見つからない
	 */
	private static string getResValue(string ColumnValue)
	{
		if (ColumnValue == null) {
			return null;
		}

		int delim = ColumnValue.IndexOf("=");
		if (delim != -1) {
			if(ColumnValue.Length > delim) {
				string v = ColumnValue.Substring(delim + 1);
				return v;
			}
			else {
				return "";
			}
		}
		else {
			return null;
		}
	}

	//-------------------------------------------------------------------------------------
	// manasoft * utl
	//-------------------------------------------------------------------------------------
	const char DEFAULT_DELIMITER = '\n'; // デフォルトデリミタ

    public static string[] getSplitBytes(sbyte[] pbytes, char delim)
	{
		return _getSplitBytes( 0, 0, pbytes, delim );
	}

	public static string[] getSplitBytes( sbyte[] pbytes )
	{
		return _getSplitBytes( 0, 0, pbytes, DEFAULT_DELIMITER );
	}

	public static string[] getSplitString( string str, char delim )
	{
		if( str == null ) {
			return null;
		}
		//return getSplitBytes( str.getBytes(), delim );
        //return getSplitBytes(Encoding.Default.GetBytes(str), delim); // TODO C#移植 多分SJIS
        return getSplitBytes(Array.ConvertAll<byte, sbyte>(Encoding.Default.GetBytes(str), v => (sbyte)v), delim); 
        
	}

    public static string[] getSplitString(string str)
	{
		if( str == null ) {
			return null;
        }
        //return getSplitBytes( str.getBytes() );
        //return getSplitBytes(Encoding.Default.GetBytes(str)); // TODO C#移植 多分SJIS
        return getSplitBytes(Array.ConvertAll<byte, sbyte>(Encoding.Default.GetBytes(str), v => (sbyte)v)); 
	}

    [Obsolete]
	private static string[] _getSplitBytes( int pindex, int pLength, sbyte[] pbytes, char delimiter )
	{
        // TODO C#移植 
        return null;
        //if( pbytes == null ) {
        //    return null;
        //}
        //if( pbytes.Length == 0 ) {
        //    return null;
        //}
        //if( pLength == 0 ) {
        //    pLength = pbytes.Length - pindex;
        //}
        //sbyte[] pbufs = new sbyte[ pLength ];
        //System.arraycopy( pbytes, pindex, pbufs, 0, pbufs.Length ); // 必要な分をｺﾋﾟｰ
        //string pstr = new string( pbufs ); // 文字列に変換
        //if( pstr.charAt( pstr.Length() - 1 ) != delimiter ) {
        //    pstr += delimiter;
        //}
        //int pnum = 0;
        //for( int j = 0; j < pstr.Length(); j++ ) {
        //    if( pstr.charAt( j ) == delimiter ) {
        //        pnum++;
        //    }
        //}
        //string[] prets = new string[pnum];
        //for( int j = 0,k = 0; j < pnum; j++ ) {
        //    stringBuffer pbuf = new stringBuffer();
        //    while( pstr.charAt( k++ ) != delimiter ) {
        //        pbuf.append( pstr.charAt( k - 1 ) );
        //    }
        //    prets[ j ] = pbuf.tostring().trim();
        //}
        //return prets;
	}

    [Obsolete]
	public static string[] getSplit( string trg )
	{
        // TODO C#移植 
        return null;
        //string[] ret = new string[ 256 ]; // 最大256分割まで
        //string buf;
        //int num = 0;
        //int idx = 0;
        //try {
        //    buf = trg.tostring();
        //    while( true ) {
        //        idx = buf.indexOf("\\n");
        //        num++;
        //        if( idx == -1 ) {
        //            ret[ num-1 ] = buf;
        //            break;
        //        }
        //        else {
        //            try {
        //                ret[ num-1 ] = buf.substring( 0, idx - 0 );
        //            }
        //            catch( Exception e ) {
        //            }
        //            buf = buf.substring( idx + 2 );
        //        }
        //    }
        //}
        //catch( Exception e ) {
        //}
        //int last = 0;
        //for( int i = 0; i < num; i++ ) {
        //    if( ret[ i ] == null || ret[ i ].equals("") ) {
        //    }
        //    else {
        //        last = i;
        //    }
        //}
        //string[] ret2 = new string[ last + 1 ];
        //for( int i = 0; i < ret2.Length; i++ ) {
        //    ret2[ i ] = ret[ i ];
        //}
        //return ret2;
	}
}

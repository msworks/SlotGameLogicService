//#include "DfImport.h"

//#include "DfMain.h"


//#ifdef __CANVAS2__
//	#ifdef	_DOCOMO	// {
//		#define GP_KEY_0         ( Display.KEY_0 )        // 0
//		#define GP_KEY_1         ( Display.KEY_1 )        // 1
//		#define GP_KEY_2         ( Display.KEY_2 )        // 2
//		#define GP_KEY_3         ( Display.KEY_3 )        // 3
//		#define GP_KEY_4         ( Display.KEY_4 )        // 4
//		#define GP_KEY_5         ( Display.KEY_5 )        // 5
//		#define GP_KEY_6         ( Display.KEY_6 )        // 6
//		#define GP_KEY_7         ( Display.KEY_7 )        // 7
//		#define GP_KEY_8         ( Display.KEY_8 )        // 8
//		#define GP_KEY_9         ( Display.KEY_9 )        // 9
//		#define GP_KEY_SELECT    ( Display.KEY_SELECT )   // SELECT
//		#define GP_KEY_UP        ( Display.KEY_UP )       // UP
//		#define GP_KEY_DOWN      ( Display.KEY_DOWN )     // DOWN
//		#define GP_KEY_LEFT      ( Display.KEY_LEFT )     // LEFT
//		#define GP_KEY_RIGHT     ( Display.KEY_RIGHT )    // RIGHT
//		#define GP_KEY_SOFT1     ( Display.KEY_SOFT1 )    // SOFT1
//		#define GP_KEY_SOFT2     ( Display.KEY_SOFT2 )    // SOFT2
//		#define GP_KEY_ASTERISK  ( Display.KEY_ASTERISK ) // ASTERISK
//		#define GP_KEY_POUND     ( Display.KEY_POUND )    // POUND
//	#else			// } {
///*
//	#define GP_KEY_0        0x00000001 // 〇
//	#define GP_KEY_1        0x00000002 // ①
//	#define GP_KEY_2        0x00000004 // ②
//	#define GP_KEY_3        0x00000008 // ③
//	#define GP_KEY_4        0x00000010 // ④
//	#define GP_KEY_5        0x00000020 // ⑤
//	#define GP_KEY_6        0x00000040 // ⑥
//	#define GP_KEY_7        0x00000080 // ⑦
//	#define GP_KEY_8        0x00000100 // ⑧
//	#define GP_KEY_9        0x00000200 // ⑨
//	
//	#define GP_KEY_SELECT   0x00010000 // ◎
//	#define GP_KEY_UP       0x00001000 // ↑
//	#define GP_KEY_DOWN     0x00008000 // ↓
//	#define GP_KEY_LEFT     0x00002000 // ←
//	#define GP_KEY_RIGHT    0x00004000 // →
//	#define GP_KEY_SOFT1    0x00020000 
//	#define GP_KEY_SOFT2    0x00040000 
//	
//	#define GP_KEY_ASTERISK 0x00000400 // *
//	#define GP_KEY_POUND    0x00000800 // #
//*/
//	#define GP_KEY_0        0x00000000 // 〇
//	#define GP_KEY_1        0x00000001 // ①
//	#define GP_KEY_2        0x00000002 // ②
//	#define GP_KEY_3        0x00000003 // ③
//	#define GP_KEY_4        0x00000004 // ④
//	#define GP_KEY_5        0x00000005 // ⑤
//	#define GP_KEY_6        0x00000006 // ⑥
//	#define GP_KEY_7        0x00000007 // ⑦
//	#define GP_KEY_8        0x00000008 // ⑧
//	#define GP_KEY_9        0x00000009 // ⑨
//	
//	#define GP_KEY_SELECT   0x00000014 // ◎
//	#define GP_KEY_UP       0x00000011 // ↑
//	#define GP_KEY_DOWN     0x00000013 // ↓
//	#define GP_KEY_LEFT     0x00000010 // ←
//	#define GP_KEY_RIGHT    0x00000012 // →
//	#define GP_KEY_SOFT1    0x00000015 
//	#define GP_KEY_SOFT2    0x00000016 
//	
//	#define GP_KEY_ASTERISK 0x0000000a // *
//	#define GP_KEY_POUND    0x0000000b // #
//	#endif			// }
//#endif
#if	_DOCOMO	// {

// ZZってなに？

public partial class ZZ
{
    //#include "DoCoMo\ZZ_DOCOMO.cpp"
    //#include "DoCoMo\ZZ_MainCanvas2.cpp"
	//#include "DoCoMo\ZZ_D_MainCanvas.cpp"
	
	#if __COM_TYPE__
	#else
	//	#include "DoCoMo\ZZ_AuthorizationPanel.cpp"
	#endif
#else			// } {
public partial class ZZ // extends MIDlet implements Runnable 
{
    //public MIDlet midlet;	//MIDletの保存
    //public static String strAppParam="";
	
    //#include "SoftBank\ZZ_SOFT.cpp"
    //#include "SoftBank\ZZ_S_MainCanvas.cpp"
	
	// 共通
//	#include "ZZ_sub.cpp"
	
	//}
	
#endif			// }
	
	/**
	 * ソフトキーに任意のラベルを追加する
	 */
// {
		public static void addSoftKey(int key_pos, string label) {
            // TODO
        //    m_strSoftKeyName[key_pos] = label;
        //    int	frame_key[] = {	Frame.SOFT_KEY_1,	Frame.SOFT_KEY_2,	};
        //    canvas.setSoftLabel(frame_key[key_pos], label);
        //}

        //static String soft1_buf = "";
        //static String soft2_buf = "";
        //public static void addSoftKey( String soft1_str, String soft2_str )
        //{
        //    if( soft1_str.equals( soft1_buf ) && soft2_str.equals( soft2_buf ) ) {
        //        return;
        //    }
        //    soft2_buf = soft2_str;
        //    soft1_buf = soft1_str;
        //    canvas.setSoftLabel( Frame.SOFT_KEY_1, soft1_str );
        //    canvas.setSoftLabel( Frame.SOFT_KEY_2, soft2_str );
		}
// }
	
	public SlotInterface gp;
	
//	#include "ZZDebug.cpp"
//	#include "ZZ_MainCanvas.cpp"
		
}

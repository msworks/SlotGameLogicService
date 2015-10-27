using UnityEngine;
using System.Collections;

public partial class SlotInterface {

	// 互換性のために用意するメソッド、メンバー類
	// ファイル関係
	//#include "SlotInterface_FIle.cpp"


	int slump_count = 0;

//	public const int BB = 1;
//	public const int RB = 2;
	public int bonus_type;    // 獲得種別(BB=1,RB=2)
	public int bonus_incount; // 回転数(ボーナス当選時のhall.dai_bns_rotを入れる）
	public int bonus_getcoin; // 獲得枚数(777時の15枚含む)

	// 設定系
	public int gpif_setting; // 台設定
	public int gpif_coin;    // 台総コイン(ユーザー不可視※クレジット表示とは別です)
	public bool gpif_bonuscyu_f; // ボーナス中フラグ
	public bool gpif_naibucyu_f; // ボーナス内部中フラグ
	public bool gpif_flash_f; // 再描画
	public bool gpif_auto_f;        // オートフラグ

	// アイテム効果
	public bool gpif_bonuscut_f;    // ボーナスカットフラグ
	public bool gpif_tatsujin_f;    // 達人オートフラグ
	public bool gpif_triple_f;      // トリプルスリーカード
	
	public bool gpif_lock_f; // 内部表示ロックフラグ(エラー時など描画だけおこない、内部ステートは変更しないロック状態)

	public bool gpif_nonstop_f;	// ノンストップオート
	public short gpif_bonus_n;		//通常0～設定時BB＝1、RB＝2
	public short gpif_kakuhen_n = 1; // 確率アップ(通常1～設定時33)


	const int TATSUJIN = 3;
	const int NONSTOP  = 2;
	const int AUTO     = 1;

	public int auto_num = 0; // オートフラグの復帰に使う

	public short gpif_oshijun_n;	// メニューの押し順(0～5)
	public short[][] gpif_oshijun_list = // メニューの押し順リスト
	{ 
		new short[]{0,1,2},	//0:左、中、右（順押し）
		new short[]{0,2,1},	//1:左、右、中
		new short[]{1,0,2},	//2:中、左、右
		new short[]{1,2,0},	//3:中、右、左
		new short[]{2,0,1},	//4:右、左、中
		new short[]{2,1,0}		//5:右、中、左（逆押し）
	};
	
	// 描画
	static int lockcount = 0;
		
	// ソフトキーラベル
	string buf_l_label = null;
	string buf_r_label = null;
		
	// サウンドボリューム
	static int[] vollist = { 0,20,30,50,65,90,100,};
		
	// コイン補充タイミング用
	bool get_coin_f = false;
		
	// オートプレイの停止タイミング用
	public bool gpif_auto_stop_f = false;

	// 参照系
	public bool betFlag = false;
	public bool betNow() { 
		// ベット可能状態（リプレイ当選時のベットタイミングはtrue）
		// true = ベットできない
		// false = ベットできる
		
		//TRACE("ベット状態チェック:" + betFlag);
		return betFlag;
//		if( (mOmatsuri.int_s_value[DEF_INT_REQUEST_MODE] == DEF_RMODE_BET) 
//			&& (mOmatsuri.int_s_value[DEF_INT_CURRENT_MODE] == DEF_RMODE_WAIT)
//		)
//		{	// キーが押されてBET処理次からベット処理にいく場合用
//			TRACE("次回ベット");
//			return true;
//		}
//		else if( (mOmatsuri.int_s_value[DEF_INT_CURRENT_MODE] == DEF_RMODE_WAIT) ||
//			(mOmatsuri.int_s_value[DEF_INT_CURRENT_MODE] == DEF_RMODE_BET) )
//		{
//			
//			if( (mOmatsuri.int_s_value[DEF_INT_BETTED_COUNT] > 0) )
//			{	// すでにベットできていた場合は
//				return true;
//			}
//			if( !mOmatsuri.IS_REPLAY() )
//			{	// リプレイじゃないとき
//				return false;
//			}
//		}
//		// ベットできない
//		return true;
	}
	
	public bool bonusNow() { return false; } // ボーナス状態

	// キー

	// キー press時にtrue。gpif_lock_f時は常にfalse
	bool getKey( int keycode )
	{
		// キーロック時はfalse
		if( gpif_lock_f ) {
			return false;
		}
		if( ( Mobile.keyTrigger & ( 1 << keycode ) ) != 0 ) {
			return true;
		}
		else {
			return false;
		}
	}

	// キー press時にtrue。gpif_lock_fの状態によらない
	bool getKey2( int keycode )
	{
		if( ( Mobile.keyTrigger & ( 1 << keycode ) ) != 0 ) {
			return true;
		}
		else {
			return false;
		}
	}

	public void resetKey()
	{
		Mobile.keyTrigger = 0;
	}


	
	
	/**
	 * jar/jad内のparamを取得する
	 */
	 string[] getAppParam()
	{
#if	_DOCOMO		// {
         // TODO C#移植 実行時引数取得？
         return null;
		//return( IApplication.getCurrentApp().getArgs() );
#else			// } {
		//string strParam = ZZ.getAppParamForSoft();
		//String[] param = getSplitString( strParam, ' ' );
		
		string Param = mainapp.getAppProperty("MIDlet-AppParam"); //$NON-NLS-1$
		String[] param = getSplitString( Param, ' ' );
	
		//midlet.getAppProperty(APP_SBPARAM + $param_name[i]);
		//string strParam = getAppProperty("AppParam"); //$NON-NLS-1$
		return param;
	
#endif			// }
	}

	/**
	 * (初回)起動フラグを取得する
	 */
	 bool getInitial()
	{
		return ( opt_value[ (int)SAVE.SAVE_INIT ] == 1 );
	}

	/**
	 * (初回)起動フラグを設定する
	 */
	 void setInitial()
	{
		opt_value[ (int)SAVE.SAVE_INIT ] = 1;
	}


	void setColor( int col_r, int col_g, int col_b )
	{
        // TODO Unity側で描画色設定？
//#if	_DOCOMO		//{
//        g.setColor( Graphics.getColorOfRGB( col_r, col_g, col_b ) );
//#else				//}{
//        g.setColor( col_r, col_g, col_b );
//#endif				// }
	}

	void fillRect( int x, int y, int wid, int hei )
	{
        // TODO Unity側でライン描画する処理？
        //g.fillRect( x + mainapp.ofX, y + mainapp.ofY, wid, hei );
	}

	void drawLine( int x1, int y1, int x2, int y2 )
	{
        // TODO Unity側でライン描画する処理？
        //g.drawLine( x1 + mainapp.ofX, y1 + mainapp.ofY, x2 + mainapp.ofX, y2 + mainapp.ofY );
	}

	void drawString( string str, int x, int y )
	{
        // TODO Unity側で文字描画する処理？
//#if	_DOCOMO		//{
//        g.drawString( str, x + mainapp.ofX, y + mainapp.ofY + fontHeight );
//#else				//}{
////		g.drawString( str, x, y + fontHeight, ( Graphics.BOTTOM | Graphics.LEFT ) );
////		g.drawString( str, x, y + fontHeight, ( Graphics.BASELINE | Graphics.LEFT ) );
//        g.drawString( str, x + mainapp.ofX, y + mainapp.ofY, ( Graphics.TOP | Graphics.LEFT ) );

//#endif				// }
	}

	void drawString2( string str, int x, int y, Font f )
	{
        // TODO Unity側で文字描画する処理？
//#if	_DOCOMO		//{
//        int ac = f.getAscent();
//        g.drawString( str, x + mainapp.ofX, y + mainapp.ofY + ac );
//#else				//}{
////		g.drawString( str, x, y + fontHeight, ( Graphics.BOTTOM | Graphics.LEFT ) );
////		g.drawString( str, x, y + font.getBaselinePosition(), ( Graphics.BASELINE | Graphics.LEFT ) );
//        g.drawString( str, x + mainapp.ofX, y + mainapp.ofY, ( Graphics.TOP | Graphics.LEFT ) );
//#endif				// }
	}

    // TODO スタブ
    class Image {
        public void setAlpha(int a) {}
    }

	void drawImage( Image img, int x, int y )
	{
#if	_DOCOMO		//{

        // TODO C#移植 一旦コメントアウト
        //g.drawImage( img, x + mainapp.ofX, y + mainapp.ofY );

#else				//}{
		g.drawImage( img, x + mainapp.ofX, y + mainapp.ofY, (Graphics.TOP | Graphics.LEFT) );
#endif				// }
	}
	
	void drawImage( Image img, int dest_x, int dest_y, int src_x, int src_y, int wid, int hei )
	{
#if	_DOCOMO		//{

        // TODO C#移植 一旦コメントアウト
        //g.drawImage( img, dest_x + mainapp.ofX, dest_y + mainapp.ofY, src_x, src_y, wid, hei );
#else				//}{
		g.drawRegion( img, src_x, src_y, wid, hei, Sprite.TRANS_NONE, dest_x + mainapp.ofX, dest_y + mainapp.ofY, (Graphics.TOP | Graphics.LEFT) );
#endif				// }
	}
	
	void setImageAlpah( Image img, int alpha )
	{
#if	_DOCOMO		//{
		img.setAlpha( alpha );
#endif				// }
	}

	int stringWidth( string str )
	{
        return 8;
        // TODO C#移植 一旦コメントアウト
        //return( font.stringWidth( str ) );
	}

	
#if	_DOCOMO		//{
	void setSoftLabel( int key_pos, string label )
	{
//		ZZ.addSoftKey( key_pos, label );
	}

	void setSoftLabel( string leftLabel, string rightLabel )
	{

//		ZZ.addSoftKey( DEF_SOFT_KEY1, leftLabel  );
//		ZZ.addSoftKey( DEF_SOFT_KEY2, rightLabel );
        
        // TODO C#移植 一旦コメントアウト
		//ZZ.addSoftKey( leftLabel, rightLabel );

	}
#else				//}{
	void setSoftLabel( string leftLabel, string rightLabel )
	{
		ZZ.addSoftKey( leftLabel, rightLabel );
	}
#endif				// }
		
		
    /// <summary>
    /// ブラウザを開く
    /// </summary>
    /// <param name="url">ブラウザに表示するサイトのURL</param>
	public void goWeb( string url )
	{
        Application.OpenURL(url);
	}

    /// <summary>
    /// バージョン更新処理
    /// </summary>
	public void versionUp()
	{
        // TODO C#移植 バージョン更新処理 機種毎の実装はソース単位で分割したい
        switch (Application.platform) {
            case RuntimePlatform.Android:
                break;
            case RuntimePlatform.IPhonePlayer:
                break;
            default:
                Defines.TRACE("versionUp:想定外のプラットフォーム:"+Application.platform.ToString());
                break;
        }
	}
	
    /// <summary>
    /// アプリケーション終了
    /// </summary>
	public void terminate()
	{
        Application.Quit();
	}

    /// <summary>
    /// ボリューム設定
    /// </summary>
    /// <param name="vol"></param>
	public void setVolume( int vol )
	{
		//PRINT( "*--> vol=" + vollist[ vol ] );
		ZZ.setVolume( vollist[ vol ], Defines.DEF_SOUND_MULTI_BGM );
		ZZ.setVolume( vollist[ vol ], Defines.DEF_SOUND_MULTI_SE );
        AudioListener.volume = vol;
	}
		
	// コールバック
	// クレジットが0で投入が必要な時に呼ばれる
	public void onCreditZero()
	{
		Defines.TRACE("call onCreditZero");
		//PRINT("キタ");
		get_coin_f = true;
	}

	// クレジット加算（コイン＋1時）時に呼ばれる
	public void onCreditUp()
	{
        gpif_coin++;
        // TODO C#移植 ホール処理コメントアウト(コイン処理が癒着している？)
//        param_hall.usr_coin++;
//        gpif_coin++;
//        param_hall.dai_day_out++;
////		if( gpif_bonuscyu_f ) {
//            bonus_getcoin++;
////		}
//        call( GPH_VIEW_DATAWINDOW );
	}

	// クレジット減算（コイン－1時）時に呼ばれる
	public void onCreditUse()
	{
        gpif_coin--;
        // TODO C#移植 ホール処理コメントアウト(コイン処理が癒着している？)
//        if( bonus_seiritsu_f ) {
//            bonus_seiritsu_f = false;
//            auto_off = false;
//            if( auto_off_bonus == 1 )
//            {
//                auto_off_bonus = 2;
//            }
//            flash();
//        }
//        param_hall.usr_coin--;
//        gpif_coin--;
//        param_hall.dai_day_in++;
////		if( gpif_bonuscyu_f ) {
//            bonus_getcoin--;
////		}
//        call( GPH_VIEW_DATAWINDOW );
	}

	// レアシーンを表示した場合にその演出番号が呼ばれる
	public void onReaSceneGet()
	{
		//userDirection = "1,2,3,4,5,6"; // なんか入れる

        // TODO C#移植 GP処理コメントアウト
        //call( GPH_CON_DIRECTION ); // 演出ID通信

		userDirection = ""; // 安全のため消去
	}

	public bool getBusy()
	{
        return false;
        // TODO C#移植 GP処理コメントアウト
        //if( lock_ch == GPH_NOPROCESS && intr_ch == GPH_NOPROCESS && !isBusy && !gpif_lock_f && !seisan ) {
        //    return false;
        //}
        //else {
        //    return true;
        //}
	}

	
	// ボーナス内部当選時に呼ばれる
	public void onBonusNaibu()
	{
        Defines.TRACE("★ボーナス確定演出発生");
        // TODO C#移植 一旦コメントアウト
        //PRINT( "内部中になりました" );
        //gpif_naibucyu_f = true; // 目押しできる
        ////-------------------------------------------
		
        //////////////////////////////////////////
        ////＠＠＠
        //// autot_flagがfalseの為通らない
        //// アイテム使用ではないからtrueになる事がない？
        //if( gpif_auto_f )
        //{
        //    gpif_auto_f = false;
        //    auto_off(); // オフにする
        //    auto_off_bonus = 1;
        //    gpif_auto_stop_f = true;
        //}
        //else if( gpif_tatsujin_f ) { // 達人オート中
        //    PRINT( "達人オート中なのでスルー" );
        //}
        //else if( gpif_nonstop_f ) { // ノンストップオート中
        //    PRINT( "ノンストップオート中なのでスルー" );
        //}
        //////////////////////////////////////////
        ////if( autot_flag ) { // オート系アイテム使用中
        ////	TRACE("ここ通る？");
        ////	gpif_auto_f = false;
        ////	if( gpif_nonstop_f ) { // オート中
        ////		gpif_nonstop_f = false;
        ////		auto_off(); // オフにする
        ////		auto_off_bonus = 1;
        ////		auto_num = NONSTOP;
        ////	}
        ////	else if( gpif_tatsujin_f ) { // 達人フラグ
        ////		gpif_tatsujin_f = false;
        ////		auto_off(); // オフにする
        ////		auto_off_bonus = 1;
        ////		auto_num = TATSUJIN;
        ////	}
        ////}
	}

	// ボーナス当選BB時に呼ばれる
	public void onBonusBB()
	{
		Defines.TRACE("★ビッグボーナス入賞");
        GameManager.Instance.OnBonusBB();
        // TODO C#移植 一旦コメントアウト        
//        gpif_naibucyu_f = false; // 目押しできない
//        l_m_bEyeSupport = false; // 目押しフラグ
//        PRINT( "--> Big BONUS START" );
//        gpif_bonuscyu_f = true; // ボーナス中フラグ
//        param_hall.dai_day_bb++;
//        param_hall.usr_ply_bb++;
//        param_hall.dai_bns_rot_s = 100; // BB
//        { // ここに移動してよいもの？
//            bonus_getcoin = 0; // 獲得枚数
//            bonus_type = BB;
//            bonus_incount = param_hall.dai_bns_rot;
//        }
//        setAppDataString(); // ここでapp_dataを作らないと保存されない
//        call( GPH_CON_2003 ); // ボーナス通信(BB)
//        param_hall.dai_bns_rot_s = 0; // BB
////		bonus_getcoin = 0; // 獲得枚数
////		bonus_type = BB;
////		bonus_incount = param_hall.dai_bns_rot;
//        ////////////////////////////////////////
//        //＠＠＠
//        // offにしてる箇所がみつからなかったので
//        if( (gpif_auto_f == true ) && (gpif_auto_stop_f == false) )
//        {
//            gpif_auto_f = false;
//            auto_off(); // オフにする
//            auto_off_bonus = 1;
//        }
//        gpif_auto_stop_f = false;
//        ////////////////////////////////////////
	}

	// ボーナス当選RB時に呼ばれる
	public void onBonusRB()
	{
		Defines.TRACE("★レギュラーボーナス入賞");
        GameManager.Instance.OnBonusRB();
        // TODO C#移植 一旦コメントアウト
//        gpif_naibucyu_f = false; // 目押しできない
//        l_m_bEyeSupport = false; // 目押しフラグ
//        PRINT( "--> Reg BONUS START" );
//        gpif_bonuscyu_f = true; // ボーナス中フラグ
//        param_hall.dai_day_rb++;
//        param_hall.usr_ply_rb++;
//        param_hall.dai_bns_rot_s = 200; // RB
//        { // ここに移動してよいもの？
//            bonus_getcoin = 0; // 獲得枚数
//            bonus_type = RB;
//            bonus_incount = param_hall.dai_bns_rot;
//        }
//        setAppDataString(); // ここでapp_dataを作らないと保存されない
//        call( GPH_CON_2003 ); // ボーナス通信(RB)
//        param_hall.dai_bns_rot_s = 0; // RB
////		bonus_getcoin = 0; // 獲得枚数
////		bonus_type = RB;
////		bonus_incount = param_hall.dai_bns_rot;
//        ////////////////////////////////////////
//        //＠＠＠
//        // offにしてる箇所がみつからなかったので
//        if( (gpif_auto_f == true ) && (gpif_auto_stop_f == false) )
//        {
//            gpif_auto_f = false;
//            auto_off(); // オフにする
//            auto_off_bonus = 1;
//        }
//        gpif_auto_stop_f = false;
//        ////////////////////////////////////////
	}

	// ボーナス中JACIN時に呼ばれる
	public void onBonusJACIN()
	{
		//TRACE("JAC IN");

        // TODO C#移植 一旦コメントアウト
        //call( GPH_CON_2003 ); // ボーナス通信(JACIN)
        //param_hall.dai_bns_rot_s = 0;
	}

	// ボーナス終了時に呼ばれる
	public void onBonusEND()
	{
        // 大当たり間ゲーム数カウントクリア
        GameManager.Instance.OnBonusEnd(bonus_incount);
        bonus_incount = 0;

        // TODO C#移植 一旦コメントアウト
///*
//        PRINT_PRI(54, "--> ボーナス終了！count=" + bonus_incount + "/get=" + bonus_getcoin );
////		gpif_bonuscyu_f = false; // ボーナス中フラグ
//        param_hall.dai_bns_rot = 0;
//        param_hall.usr_ply_bns_rot = 0;
//        param_hall.dai_bns_rot_s = 0;
//        addHistory( bonus_incount, bonus_type, bonus_getcoin );
//        PRINT( "--> BONUS END" );
		
//        TRACE( "--> BONUS END:" + bonus_middle_end_f );
//        if( bonus_middle_end_f ) {
//            PRINT("ボーナス途中で精算された場合");
//            setAppDataString();
//        }
//        call( GPH_CON_2003 ); // ボーナス終了通信
		
//        bonus_middle_end_f = false;
//        gpif_bonuscyu_f = false; // ボーナス中フラグ
//*/
//        addHistory( bonus_incount, bonus_type, bonus_getcoin );
//        PRINT( "--> BONUS END" );
//        param_hall.dai_bns_rot = 0;
//        param_hall.dai_bns_rot_s = 0;
//        param_hall.usr_ply_bns_rot = 0;
//        bonus_middle_end_f = false;
//        gpif_bonuscyu_f = false; // ボーナス中フラグ
//// 終了後の状態を保存して送信する
//        setAppDataString();
//// 通信はすべてのパラメータを設定した後
//        call( GPH_CON_2003 ); // ボーナス終了通信
	}

	bool bonus_middle_end_f = false;

	public void SettingNow()
	{        
        // TODO C#移植 一旦コメントアウト
        ////設定値に関する処理
        ////SAVE_SETTING_NOW=0  :設定値なし
        ////SAVE_SETTING_NOW=1～:設定値あり(ただし、usr_ply_rot、usr_ply_bns_rot=0の時は無効)
        //if( opt_value[SAVE_SETTING_NOW] == 0 ) { // 設定値なし
        //    PRINT_PRI(53,"そのまま設定:");
        //    setSetting( param_hall.setting_n ); // そのままの設定
        //}
        //else {
        //    PRINT_PRI(53,"引き継ぎ設定:");
        //    setSetting( opt_value[SAVE_SETTING_NOW] ); // 引き継ぎ設定
        //}
	}
	
	// ボーナス中に精算された場合に呼ばれる
	// ※ボーナスを終了させないといけない
	public void onBonusMiddleEND()
	{
        // TODO C#移植 一旦コメントアウト
//        bool endFg = false;

//PRINT_PRI(55,"--> BONUS Middle ENDが呼ばれました");
//        if( bonus_type == BB )
//        {	// ビッグボーナス
//            if( bonus_getcoin < BIG_BONUS_AVENUM)
//            {
//                bonus_getcoin = BIG_BONUS_AVENUM;
//            }
//            endFg = true;
//        }
//        else if( bonus_type == RB )
//        {	// レギュラーボーナス
//            if( bonus_getcoin < REG_BONUS_AVENUM)
//            {
//                bonus_getcoin = REG_BONUS_AVENUM;
//            }
//            endFg = true;
//        }
		
//        if( endFg )
//        {
//            bonus_middle_end_f = true;
//            PRINT_PRI(55,"--> BONUS Middle ENDが呼ばれました");
//            PRINT_PRI(55,"--> BONUSを終了させます");
//            endFg = false;
//            // プレイヤーの変更時
//            mOmatsuri.chgPrayer();
//        }

////		
////		
////		BonusEnd(1);
////		gpif_bonuscyu_f = false; // ボーナス中フラグ
////		addHistory( bonus_incount, bonus_type, coin );
////		PRINT( "--> BONUS END" );
////		
//////＠＠＠通信必要？
////		call( GPH_CON_2003 ); // ボーナス終了通信
//////		dai_bns_rot = 0;
////		param_hall.dai_bns_rot = 0;
////		param_hall.usr_ply_bns_rot = 0;
////		
	}
		
	// レバーON（回転数が＋1※リプレイ含む）時に呼ばれる
	public void onCountUp()
	{
        GameManager.Instance.OnCountUp();
        if (!mOmatsuri.IS_BONUS()) {
            bonus_incount++;
        }
        // TODO C#移植 一旦コメントアウト
//        // モーニングカードの削除
//        if( itemSlot[ 5 ] != -1 ) {
//            PRINT( "モーニングカードの削除" );
//            int id = -1;
//            for( int i = 0; i < param_itemu.item_list_len; i++ ) {
//                if( param_itemu.item_list[ i ].item_type == 1300 ) { // モーニング券
//                    id = i;
//                    break;
//                }
//            }
//            if( id != -1 ) { // モーニングを消す
//                param_itemu.item_list = itemDel( param_itemu, id );
//            }
//            itemSlot[ 5 ] = -1;
//            itemType[ 5 ] = -1;
//        }
//        // ボーナス中はカウントUPしない
//        if( !gpif_bonuscyu_f ) {
//            param_hall.dai_bns_rot++; // Total Play
//            param_hall.dai_day_rot++;
////			dai_bns_rot++;
//            param_hall.usr_ply_rot++;
//            param_hall.usr_ply_bns_rot++;
//            slump_count++;
//            if( slump_count == 10 ) {
//                DEBUGOUT( "ｽﾗﾝﾌﾟ追加したどぅ/回転数=" + param_hall.dai_day_rot + "/差枚数=" + (param_hall.dai_day_out - param_hall.dai_day_in) );
//                slump_count = 0;
//                slumpData2 = addSlump( param_hall.dai_day_rot, param_hall.dai_day_out - param_hall.dai_day_in, slumpData2 );
//            }
//        }
	}



	// 台内部当選確率変更
	public void setMorningBonus( int bb, int rb, int hazure ) {
		// 100でくるのを*100
		int r;
		Defines.TRACE("★★★★モーニング");
		r = random.Next(10000) & 0xffff;
		
		
		if( (bb != 0) && (r < (bb*100)))
		{	// BB当選
			Defines.TRACE("モーニングBB");
			gpif_bonus_n = 1;
			return;
		}
		else if( rb != 0 )
		{	//RB当選
			Defines.TRACE("モーニングRB");
			gpif_bonus_n = 2;
			return;
		}
		Defines.TRACE("モーニング 想定外");
	}

#if	__DEBUG__

	string telopStr;
	bool telopFg;
	long telopTime;

	// テロップのセット
	public void setTelop(string str)
	{
		telopStr = str;
		telopFg = true;
		telopTime = Util.GetMilliSeconds() + 2000;
	}
	// テロップの描画
	public void drawTelop()
	{
		if( telopFg == true)
		{
			if( Util.GetMilliSeconds() > telopTime ) {
				telopFg = false;
			}
			else {
				setColor( 0, 0, 0 );
				drawString( telopStr, ( 240 - stringWidth( telopStr ) ) / 2 - 1, ( 240 - fontHeight ) / 2 + 0 - 20);
				drawString( telopStr, ( 240 - stringWidth( telopStr ) ) / 2 + 1, ( 240 - fontHeight ) / 2 + 0 - 20);
				drawString( telopStr, ( 240 - stringWidth( telopStr ) ) / 2 + 0, ( 240 - fontHeight ) / 2 - 1 - 20);
				drawString( telopStr, ( 240 - stringWidth( telopStr ) ) / 2 + 0, ( 240 - fontHeight ) / 2 + 1 - 20);
				setColor( 255, 255, 255 );
				drawString( telopStr, ( 240 - stringWidth( telopStr ) ) / 2, ( 240 - fontHeight ) / 2  - 20);
			}
            
            // TODO C#移植 一旦コメントアウト
            //flash();
		}
	}
#endif
	// メニュー設定の取得
	public int getOptValue(int index)
	{
//const public int OPT_SOUND_VOL	=  0;
//const public int OPT_AUTO_MEDAL	=  1;
//const public int OPT_AUTO_PLAY	=  2;
//const public int OPT_BONUS_CUT	=  3;
//const public int OPT_MEOSHI		=  4;
//const public int OPT_OSHIJUN		=  5;
//const public int OPT_OPT_NUM		=  6;
		return opt_value[ index ];
	}
	// メニュー設定の書き込み
	public void setOptValue(int index, int val)
	{
//const public int OPT_SOUND_VOL	=  0;
//const public int OPT_AUTO_MEDAL	=  1;
//const public int OPT_AUTO_PLAY	=  2;
//const public int OPT_BONUS_CUT	=  3;
//const public int OPT_MEOSHI		=  4;
//const public int OPT_OSHIJUN		=  5;
//const public int OPT_OPT_NUM		=  6;
		opt_value[ index ] = val;
	}

    // TODO C#移植 一旦コメントアウト
    //synchronized public void GP_resume()
    //{
    //    Defines.TRACE("レジュームイベント");
    //    if( !resumeflg && !seisan && startflg ) {
    //        // #27対応:satoh/レジューム通信が割り込みでerrResetしてしまうからだと思う
    //        if( !no_resume_f ) { // 各種通信中だった場合は、競合しないように配慮する
    //            resumeflg = true;
    //            call( GPH_CON_RESUME_SUB );
    //        }
    //    }
    //    PRINT_PRI(11,"レジュームイベント抜けた");
    //}


}

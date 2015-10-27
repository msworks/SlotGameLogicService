// TODO C#移植 Unityとのインタフェース作るならここ？
#define _UNITY_CONVERT_ // Unity+C#移植作業用
public partial class ZZ {

    
////////////////////////////////////////////////////////////////
//
//	MainCanvas
//
////////////////////////////////////////////////////////////////
public sealed class MainCanvas 
{
	//#include "..\GpH\PublicDefine.java"

	public Canvas cv = null;
    public class Canvas {} // TODO C#移植 スタブ
	public Mobile mb = null;
	public ZZ mainapp = null;
	// 無限ループ
	long time_rest;
	long next_time;
	//--------------------------------------------------------------------
	//				メインループ
	//--------------------------------------------------------------------
	public void	run()
	{
		Defines.TRACE("game main start");
#if _UNITY_CONVERT_
#else 
		//System.gc();
		
		//main();	// 制御メイン関数
#endif
	}
////////////////////////////////////////////////////////////////////////////////////////
	/**
	 * @param g graphics
	 * @ s e e com.nttdocomo.ui.Frame#paint(com.nttdocomo.ui.Graphics)
	 */
	public void paint(Graphics g) {
		// なにもしない
	}
#if	_DOCOMO		//{
    #if _UNITY_CONVERT_
    // TODO C#移植 キー入力処理
    #else 
	/**
	 * @param event event
	 * @param param param
	 *
	 * @ s e e com.nttdocomo.ui.Frame#processEvent(int, int)
	 */
	public void processEvent(int event, int param) {
		// if(DEF_IS_DEBUG)
		 //Defines.TRACE("processEvent event ="+event+",param="+param);
		switch (event) {
		case Display.KEY_PRESSED_EVENT: // TOBE ソフトきー
//			if(param == Display.KEY_SOFT1 && m_strSoftKeyName[0] == null){
//				break;
//			}
//			if(param == Display.KEY_SOFT2 && m_strSoftKeyName[1] == null){
//				break;
//			}
//			Defines.TRACE("キーデータ:" + (1 << param));
			int_value[DEF_Z_INT_KEYPRESS] |= (1 << param);
			int_value[DEF_Z_INT_KEYPRESSING] |= (1 << param);
			break;
		case Display.KEY_RELEASED_EVENT:
			if(param == Display.KEY_SOFT1 && m_strSoftKeyName[0] == null){
				break;
			}
			if(param == Display.KEY_SOFT2 && m_strSoftKeyName[1] == null){
				break;
			}
			int_value[DEF_Z_INT_KEYRELEASE] |= (1 << param);
			int_value[DEF_Z_INT_KEYPRESSING] ^= (1 << param);
			break;
		case Display.TIMER_EXPIRED_EVENT:
			break;
		}
	}
    #endif
#else				// } {
//------------------------------------------------------------------
//		コマンドを通知するイベントが発生した時に呼ばれる
//------------------------------------------------------------------
    // TODO C#移植 未使用につきコメントアウト
//    public void	commandAction(Command c, Displayable d)
//    {
////		if (c == $skey[0]) {					// 左ソフトキー
////			$sw[SW_TMP] |= bKEY_SOFT1;
////			$sw[SW_NEW] |= bKEY_SOFT1;
////			return;
////		}
////		if (c == $skey[1]) {				// 右ソフトキー
////			$sw[SW_TMP] |= bKEY_SOFT2;
////			$sw[SW_NEW] |= bKEY_SOFT2;
////			return;
////		}
//    }
#endif				// }

    
    #if _UNITY_CONVERT_
    // TODO C#移植 mediaAction
    #else 
	/**
	 * @see com.nttdocomo.ui.MediaListener#mediaAction(com.nttdocomo.ui.MediaPresenter,
	 *	  int, int)
	 */
	public void mediaAction(MediaPresenter src, int event, int param) {
		try {
			if (DEF_IS_DEBUG_SOUND) {
				if (src.equals(audio[DEF_SOUND_MULTI_SE])) {
					Defines.TRACE("mediaActionSE(, " + event + ", "
							+ param + ")");

				} else if (src.equals(audio[DEF_SOUND_MULTI_BGM])) {
					Defines.TRACE("mediaActionBGM(, " + event + ", "
							+ param + ")");
				}
			}

			switch (event) {
			case AudioPresenter.AUDIO_RESTARTED:
			case AudioPresenter.AUDIO_PLAYING:
				break;
			default:
				if (src.equals(audio[DEF_SOUND_MULTI_SE])) {
					is[DEF_IS_SQ_FREE_SE] = true; // セット
					
				} else if (src.equals(audio[DEF_SOUND_MULTI_BGM])) {
					is[DEF_IS_SQ_FREE_BGM] = true; // セット
				}
				break;
			}
		} catch (final Exception e) {
			if (DEF_IS_DEBUG) {
				e.printStackTrace();
			}
		}
	}
#endif
	
	//------------------------------------------------------------------
	//		ゲームメイン
	//------------------------------------------------------------------
	private void	main()
	{
// satoh
Defines.TRACE("docomo.run()");
        
    #if _UNITY_CONVERT_
    // TODO C#移植 制御メイン
    #else 
			gp = new GpHandler();
			gp.mainapp = mainapp;
			System.gc();
			cv = canvas;

			if (DEF_IS_DEBUG) {
				Defines.TRACE(" > call start()is DEBUG MODE");
			}
			
#if __TRY_FALSE__
			
#else
			try {
#endif
				canvas.setBackground(Graphics.BLACK); // 黒固定
				// JAM からの初期化
				String fname;
				{
					final String[] args = getArgs();
					try {
						threadSpeed = Integer.parseInt(args[DEF_JAM_PARAM_LOOP]);
					} catch (Exception e) {
						threadSpeed = 40;
					}
					fname = DEF_RESOURCE_FILE + ".d_";
				}
				grp = canvas.getGraphics();
				setOrigin(ofX, ofY);
				grp.setFont(font);

#if __TRY_FALSE__
				try {
					System.gc();
					Thread.sleep(100);	// １秒間停止
				} catch (InterruptedException e) {
					System.out.println(e);
				}
#else
				System.gc();
				Thread.sleep(100); // ちょっと待つ
#endif
				setColor(getColor(255, 255, 255)); // デフォルト文字色
				set3Denv();

//				gp      = new GpHandler();
				gp.g    = grp;
				gp.font = Font.getDefaultFont();
				gp.g.setFont( gp.font );

				PRINT(">>in ----------------------------");

				gp.callINTR( GPH_START );
				while( true ) {
					gp.lock();
					Mobile.keyTrigger = ZZ.getKeyPressed();
					if( gp.process() == TRUE ) {
						break;
					}
					gp.unlock( true );
				}
				long adtime = System.currentTimeMillis() + 4000; // だいたい3s
				gp.callINTR( GPH_IMAGELOAD );
				while( true ) {
					gp.lock();
					Mobile.keyTrigger = ZZ.getKeyPressed();
					if( gp.process() == TRUE ) {
						break;
					}
					gp.unlock( true );
				}
				PRINT(">>out ---------------------------");
				
				
#if __TRY_FALSE__
				try {
					Thread.sleep(100);
				} catch (InterruptedException e) {
					System.out.println(e);
				}
#else
				Thread.sleep(100);
#endif
		
#if __COM_TYPE__
#else
// satoh
Defines.TRACE(" > サーバからダウンロード");
				// サーバからダウンロード
				cache(fname);
				if (DEF_IS_DEBUG)
					Defines.TRACE(" > cache() end");
				System.gc();
				Thread.sleep(1000); // たっぷり待つ

#endif
				// リソースのロード
// satoh
				PRINT("*ZZ_MainCanvas:イメージの初期化2(内部リソース)");
				try {
					loadResources();
				} catch (InterruptedException e) {
					System.out.println(e);
				}
				catch (Exception e) {
					Defines.TRACE("ロードエラー"+e);
				}
				if (DEF_IS_DEBUG){
					Defines.TRACE("loadResource end");

					for(int i=0; i<DEF_RES_FIGURE_MAX; i++){
						if(figures[i] == null) Defines.TRACE("figures[" + i + "] is NULL!!!");
					}for(int i=0; i<DEF_RES_TEXTURE_MAX; i++){
						if(textures[i] == null) Defines.TRACE("textures[" + i + "] is NULL!!!");
					}for(int i=0; i<DEF_RES_ACTIONTABLE_MAX; i++){
						if(actions[i] == null) Defines.TRACE("actions[" + i + "] is NULL!!!");
					}

				}
//				System.gc();
				isLoaded = true;
//				Thread.sleep(100); // ちょっと待つ

				// サウンドセッティング
				audio[DEF_SOUND_MULTI_SE].setMediaListener(this);
				audio[DEF_SOUND_MULTI_BGM].setMediaListener(this);
				soundQueue[DEF_SOUND_MULTI_SE][0] = DEF_SQ_NOP;
				soundQueue[DEF_SOUND_MULTI_BGM][0] = DEF_SQ_NOP;
				is[DEF_IS_SQ_FREE_SE] = true;
				is[DEF_IS_SQ_FREE_BGM] = true;
				is[DEF_IS_SQ_ON] = true;
				// TOBE サウンド修正
				if (medias.length > 0) {
					audio[DEF_SOUND_MULTI_SE].setSound(medias[0]);
				}

				// 背面ディスプレイ
				if (false) { // 今は不必要なので殺しておく
					try {
						// SubDisplay.setImage(images[DEF_RES_SUB]);
					} catch (final Exception e) {
						// nothing to do
					}
				}

				// 念のためバックライト点灯
				setBacKLight(true);

				// 無限ループ
				long time_rest;
				long next_time = System.currentTimeMillis() + threadSpeed;

				// キーバッファの初期化
				int_value[DEF_Z_INT_KEYPRESS] = 0;
				int_value[DEF_Z_INT_KEYPRESSING] = 0;
				int_value[DEF_Z_INT_KEYRELEASE] = 0;

				isRunning = true;
				isRepaint = true;

				PRINT("*ZZ_MainCanvas:使用アイテム取得");
				gp.callINTR( GPH_CON_USEITEM );
				while( true ) {
					gp.lock();
					Mobile.keyTrigger = ZZ.getKeyPressed();
					if( gp.process() == TRUE ) {
						break;
					}
					gp.unlock( true );
				}
				while( adtime > System.currentTimeMillis() ); // だいたい3s待つ

//				PRINT("*ZZ_MainCanvas:AD画面wait");
//				while( gp.process( GP_MODE_ADWAIT ) == GP_MODE_ADWAIT );

				if(DEF_IS_DEBUG){
					Defines.TRACE(" > ゲーム起動1");
				}
				
				
#if	__RAM_TRACE__
//	  	{
//		  	int i;
//	  		clOHHB_V23.loadRAM();
//		  	for(i = 0; i < clOHHB_V23.mDataTable.length; i++)
//		  	{
//		  		Defines.TRACE( "mDataTable[" + i + "]" + (clOHHB_V23.mDataTable[i] & 0xFFFF));
//		  	}
//	  	}
#endif
//System.out.println("★★★★ 1 ★★★★");
				mb = new Mobile();
//System.out.println("★★★★ 2 ★★★★");
				mb.gp = gp;
//				gp.gpHandler( G7Network.GP_HANDLER_03_GAMEMODE );
//				gp.gpLocal2Gp();
//				gp.gpHandlerGAME( G7Network.GP_HANDLER_03_INFOWINDOW ); // お知らせウィンドウ
				gp.callINTR( GPH_CTRL_INFOWINDOW );
//System.out.println("★★★★ 3 ★★★★");
				gp.lock();
					grp.clearClip();
					mb.exec(); // ちらつくので
//System.out.println("★★★★ 4 ★★★★");
					gp.call( GPH_VIEW_DATAWINDOW );
					gp.call( GPH_CTRL_MAIN );
					gp.process();
//System.out.println("★★★★ 5 ★★★★");
					gp.call( GPH_CON_SYNC );
					gp.call( GPH_VIEW_SYNCCON_AFTER );
					grp.clearClip();
//				gp.unlock( true );
//SLEEP(1000);
//PRINT(">>8");
//System.out.println("★★★★ 6 ★★★★");
				
//				gp.flash();
				while (isRunning) {
					proc_main();
//SLEEP(2000);
//PRINT(">>9");
				}
#if __TRY_FALSE__
#else
			} catch (final Throwable t) {
// satoh
Defines.TRACE(" > 例外発生で終了 is run()");
			if (DEF_IS_DEBUG) {
				t.printStackTrace();
			}
		}
		terminate(); // 終了
#endif
#endif
	}
	
	
	
	//--------------------------------------------------------------------
	//				ゲーム
	//--------------------------------------------------------------------

	private void proc_main()
	{
		
    #if _UNITY_CONVERT_
    // TODO C#移植 ゲームメイン
    #else 
		final String[] launch = new String[1];
//----------------------------------------------------------------------------
//----------------------------------------------------------------------------
		grp.lock();
		if( isLoaded ) {
			mb.exec();
			gp.call( GPH_VIEW_DATAWINDOW );
			gp.call( GPH_CTRL_MAIN );
			gp.process();
			gp.call( GPH_CON_SYNC );
			gp.call( GPH_VIEW_SYNCCON_AFTER );
		}
		grp.unlock( true );
//----------------------------------------------------------------------------
//----------------------------------------------------------------------------

		// i では通常のペイントを repaint ですると
		// マルチスレッドになってしまうのでやめた方が良い
		// canvas.paint(grp);

		// サウンド

		try {
			// エラーが出たので D505 の為にリセットする。取り直したら再サウンドセッティング
			if (isSESetting) {
				if(audio[DEF_SOUND_MULTI_SE]!=null){
					audio[DEF_SOUND_MULTI_SE].setMediaListener(null);
				}
				audio[DEF_SOUND_MULTI_SE] = AudioPresenter
						.getAudioPresenter(DEF_SOUND_MULTI_SE);
				audio[DEF_SOUND_MULTI_SE].setMediaListener(this);
				is[DEF_IS_SQ_FREE_SE] = true;
				isSESetting = false; // おろす
				

			}
			processSound(DEF_SOUND_MULTI_SE);
		} catch (Exception e) {
			isSESetting = true;
			if (DEF_IS_DEBUG) {
				Defines.TRACE( "[ERROR]SE SET : " + e.toString() + "," + soundQueue[ DEF_SOUND_MULTI_BGM ][ 0 ] );
				e.printStackTrace();
			}
		}

		try {
			if (isBGMSetting) {
				if(audio[DEF_SOUND_MULTI_BGM]!=null){
					audio[DEF_SOUND_MULTI_BGM].setMediaListener(null);
				}
				audio[DEF_SOUND_MULTI_BGM] = AudioPresenter
						.getAudioPresenter(DEF_SOUND_MULTI_BGM);
				audio[DEF_SOUND_MULTI_BGM].setMediaListener(this);
				is[DEF_IS_SQ_FREE_BGM] = true;
				isBGMSetting = false; // おろす
			}
			processSound(DEF_SOUND_MULTI_BGM);
		} catch ( Exception e) {
			if (DEF_IS_DEBUG) {
				Defines.TRACE( "[ERROR]BGM SET : " + e.toString() + "," + soundQueue[ DEF_SOUND_MULTI_BGM ][ 0 ] );
				e.printStackTrace();
			}
			isBGMSetting = true;
		}
#if __COM_TYPE__
#else
		// WEB_TO 機能追加
		if (DEF__DF_AUTO_CAN_WEBTO_) {
			// TOBE ＷＥＢ　ＴＯ変更
			if (DEF__DF_AUTO_CAN_WEBTO_KISYU_) {
				launch[0] = "http://" + webURL + "&" + DEF_UID_NULLGWDOCOMO;
			} else {
				launch[0] = "http://" + webURL + "?" + DEF_UID_NULLGWDOCOMO;
			}
//						Defines.TRACE("@ZZ 使うところ webURL = "+webURL);
			is[DEF_USE_WEBTO] = true; // TODO これはどこで使っているのでしょうか？
			
			if(isWebToVerUp){ // TOBE 旧バージョンアプリ
				isWebToVerUp = false;
				launch(LAUNCH_VERSIONUP, null);
			}else
			if (isWebTo) {
				isWebTo = false;
				launch(LAUNCH_BROWSER, launch);
				isRepaint = true;
			}
		}
#endif
#if __TRY_FALSE__
		try {
			// 余裕があったら待つ
			time_rest = next_time - System.currentTimeMillis();
			while (time_rest > 1) {
				Thread.sleep( time_rest / 2 ); // 他のスレッドに処理を譲る
				time_rest = next_time - System.currentTimeMillis();
			}
			next_time = System.currentTimeMillis() + threadSpeed;
		} catch (InterruptedException e) {
			System.out.println(e);
		}
#else
		try {
			// 余裕があったら待つ
			time_rest = next_time - System.currentTimeMillis();
			while (time_rest > 1) {
				Thread.sleep(time_rest / 2); // 他のスレッドに処理を譲る
				time_rest = next_time - System.currentTimeMillis();
			}
			next_time = System.currentTimeMillis() + threadSpeed;
		} catch (InterruptedException e) {
			System.out.println(e);
		}
#endif
#endif
	}
	
}


}

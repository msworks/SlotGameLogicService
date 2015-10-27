#define _UNITY_CONVERT_ // Unity+C#移植作業用

using System;
using System.Threading;
public partial class ZZ {
    // TOBE ソフトきー
	/** ソフトきー */
	public static string[] m_strSoftKeyName =  {"",""};
	
	/**
     * バイブレータ状態フラグ
     */
    private static bool isVibrator;

	/**
	 * @see #processSound(int)
	 * @see #resume()
	 */
	private static bool isResumePass;

	/** @see #processSound */
	private static int[] audioCurrentTime = { -1, -1};


// /** TOBE その場で鳴らすに変更 */
//	private static readonly bool PLAY_SE_IMMEDIATELY = true;

	/** int 変数 */
	public static readonly int[] int_value = new int[Defines.DEF_Z_INT_MAX];

	/** bool 変数 */
    // TODO 予約語なのでis -> is_valueに変更。
	static readonly bool[] is_value = new bool[Defines.DEF_IS_MAX]; // フィールドで宣言してるので


	/** イメージ */
	static readonly Image[] images = new Image[Defines.DEF_RES_IMAGE_MAX];
    public class Image {}// TODO C#移植 スタブ

	/** 音 */
	static readonly MediaSound[] medias = new MediaSound[Defines.DEF_RES_SOUND_MAX];
    class MediaSound {}// TODO C#移植 スタブ

	/** TOBE サウンド処理キュー */
	static readonly int[,] soundQueue = new int[2,Defines.DEF_Z_SOUND_QUE_MAX];

	/** jar test */
	//private static JarInflater[] jif = new JarInflater[Defines.DEF_RES_JAR_MAX];
	/** reschar */
	public static readonly char[][] reschar = new char[Defines.DEF_RES_JAR_MAX][];

	/** 3D Figure_data in resource.d_* */
	static readonly Figure[] figures = new Figure[Defines.DEF_RES_FIGURE_MAX];
    class Figure {}// TODO C#移植 スタブ

	/** Figureに複数Textureを貼る場合のwork */
	static readonly Texture[][] figures_texture = new Texture[Defines.DEF_RES_FIGURE_MAX][];
    class Texture {}// TODO C#移植 スタブ

    /** 3D Action_data in resource.d_* */
	static readonly ActionTable[] actions = new ActionTable[Defines.DEF_RES_ACTIONTABLE_MAX];
    class ActionTable {}// TODO C#移植 スタブ

	/** 3D Texture_data in resource.d_* */
	static readonly Texture[] textures = new Texture[Defines.DEF_RES_TEXTURE_MAX];
	/** 3D Texture_data in resource.d_* */
	static Texture env_texture = null;

	/** ActionTableのFrame*/
	static int[] arrFrame = new int[Defines.DEF_RES_ACTIONTABLE_MAX];
	/** ActionTableNo */
	static int[] arrActNo = new int[Defines.DEF_RES_FIGURE_MAX];




	/** Graphics */
	static Graphics grp;

	/** for clippiingRegion */
	public static readonly int[] clipRegion = new int[4];

	/** メインキャンバス */
	static MainCanvas canvas;

	/** TOBE フォント QVGA 用なのでフォント大きくしていいですか */
	static readonly Font font = Font.getFont(Font.SIZE_TINY); // SIZE_MEDIUM=(24)
															// :SIZE_SMALL=機種ごとに変わってしまうので、つかってはダメ
															// :SIZE_TINY=(12)
    class Font {// TODO C#移植 スタブ
        public const int SIZE_TINY = 0;
        public static Font getFont(int size) { return new Font();}
    }


	/** TOBE オーディオ */
	static readonly AudioPresenter[] audio = new AudioPresenter[2];
    class AudioPresenter {}// TODO C#移植 スタブ

	/** TOBE オーディオ */
	static readonly int[] volume = {100,100
	};


	/** offsetX */
	static int ofX;

	/** offsetY */
	static int ofY;


	/** 再描画するかどうか */
	static bool isRepaint;

	// Mobile.class,Hanabi.classが直に参照してるのでリファクタリング禁止
	/** 実行中かどうか */
	static bool isRunning;

	/** ロード終了したかどうか? */
	static bool isLoaded;

	/** 起動時間 */
	static long startTime;

	/** 1ループ時間(ms) */
	static int threadSpeed;

	/** 接続先URL */
	static string serverURL;

	/** 通信フラグ */
	static bool isUpdate;

	/** サーバーコイン:初期値 -2 */
	static int serverCoin;

	/** 獲得コイン数 */
	static int winCoin;

	/** 送信パラメータ */
	static int[] sendParam;

	/** ブラウザ連携 */
	static bool isWebTo;

	/** ブラウザリンク先ＵＲＬ */
	private static string webURL = ""; // TOBE ＷＥＢ　ＴＯ変更

	/** バージョンアップ */
	private static bool isWebToVerUp; // TOBE 旧バージョンアプリ 

	/** サウンド */
	static bool isSESetting = false;
	/** サウンド */
	static bool isBGMSetting = false;
	
	/** 広告文 */
	public static string http_content = "";
	
	private static int authResult = Defines.DEF_AUTHMEMBER_NON;//DEF_AUTH_ERROR;

//////////////////////

	/**
	 * @see com.nttdocomo.ui.IApplication#start()
	 */
	public void start() {
//		mainapp = this;

//		if(DEF_IS_DEBUG_AUTH_THROUGH == false){
//
//			if(checkAuth_SP()){
//				Display.setCurrent(this.p); //認証 パネルを表示
//				this.p.go();
//			}else{
//				startCanvas(); // 認証が不必要なので、ゲーム画面へ
//			}
//
//		}else{
			// TODO デバッグ
			startCanvas(); // 認証スルーでゲーム画面起動
//		}
#if __COM_TYPE__
#else
		HParam = getParameter("hall");
//		switch (getLaunchType()) {
//		case LAUNCHED_AFTER_DOWNLOAD:// ダウンロード直後（通常起動1回目）
//		case LAUNCHED_AS_ILET:// ダウンロード即起動アプリ
//			cache_clear();// キャッシュをクリアする
//		}
#endif
	}
	/**
	 * @see com.nttdocomo.ui.IApplication#resume()
	 */
	public void resume() {
#if _UNITY_CONVERT_
#else
		// キーバッファの初期化
		int_value[Defines.DEF_Z_INT_KEYPRESS] = 0;
		int_value[Defines.DEF_Z_INT_KEYPRESSING] = 0;
		int_value[Defines.DEF_Z_INT_KEYRELEASE] = 0;
		// TOBE P505i, SH505iなどで、電源ボタン→終わらない で戻ってきたときにラップ音
		// is[DEF_IS_SQ_FREE] = true;
		isRepaint = true;
		// 念のためバックライト点灯
		setBacKLight(true);
		isResumePass = true; // resume()を通ってきたことを通知するフラグ
		setVibrator(isVibrator);
		
		
		gp.GP_resume();
#endif
	}
	
	/**
	 * ゲーム画面の呼び出し
	 */
	public void startCanvas(){
        // TODO C#移植　エントリーポイントっぽいところ
#if _UNITY_CONVERT_
#else
		canvas = new MainCanvas();
		canvas.mainapp = this;
		Display.setCurrent(canvas);
		new Thread(canvas).start();
#endif
#if __COM_TYPE__
#else
//		this.p = null;
#endif
	}
	
	/** アプリバージョンを取得 */
	private static int getAppVersion() {
		int ret=0;
		string str = Defines.DEF_APPLI_VER;
		string strTmp = "";
		char cTmp;
		try {
			for(int i=0; i<str.Length; i++){
				cTmp = str[i];// TODO C#移植 str.charAt(i) -> str[i]に変更
				if(cTmp == '.'){
					continue;
				}
				strTmp = strTmp + cTmp;
//				Defines.TRACE("strTmp = "+strTmp);
			}
		} catch (Exception e) { // TODO C#移植 Throwable -> Exceptionに変更
			Defines.TRACE("ダメ = "+e);
		}

		ret = int.Parse(strTmp);// TODO C#移植 Integer.parseInt -> int.Parseに変更
// satoh
		Defines.TRACE("アプリバージョン:(10進)"+ret);
		return ret;
	}
	
	/**
	 * 今のソフトキーのラベル取得
	 * @return ソフトキーが無い場合はnullを返す
	 * TOBE ソフトキー２を使うときは、．．．
	 */
	public static string getSoftKeyName()
	{
		return m_strSoftKeyName[0];
	}

	/**
	 * 初期化ブロック
	 */
	public ZZ(){
#if _UNITY_CONVERT_
#else
		if(DEF_IS_DEBUG){
			Defines.TRACE("ZZ init");
		}
		
		serverCoin = DEF_SERVER_INI;

		// サウンド
		audio[DEF_SOUND_MULTI_SE] = AudioPresenter
				.getAudioPresenter(DEF_SOUND_MULTI_SE);
		audio[DEF_SOUND_MULTI_BGM] = AudioPresenter
				.getAudioPresenter(DEF_SOUND_MULTI_BGM);
#if __COM_TYPE__
		// GPでは下詰めで描画する為
		ofX = (Display.getWidth() - DEF_POS_WIDTH);
		ofY = (Display.getHeight() - DEF_POS_HEIGHT);
#else
		ofX = (Display.getWidth() - DEF_POS_WIDTH) / 2;
		ofY = (Display.getHeight() - DEF_POS_HEIGHT) / 2;
#endif
		centerX += ofX;
		centerY += ofY;
		
		startTime = System.currentTimeMillis();
		isLoaded = false;
#endif
	}

	/**
	 * TODO 認証結果を取得（アクセッサ）
	 */
	public static int getAuthResult(){
		if( Defines.DEF_IS_DEBUG_AUTH_THROUGH ){
			return Defines.DEF_AUTHMEMBER_PAYING;
		}
		return authResult;
	}

	// //////////////////////////////////////////////////////////////
	// ユーティリティ
	// //////////////////////////////////////////////////////////////
    
#if _UNITY_CONVERT_
    [Obsolete]
    public static int read(object input, byte[] buf) {
    return 0; // TODO スタブ
}
#else
	/**
	 * 中断されてもきっちり読む為に
	 *
	 * @param in
	 * @param buf
	 * @return 読み込んだバイト数
	 * @throws IOException
	 */
	public static int read(InputStream in, byte[] buf) {
    // TODO
        //int size = 0;
        //while (size < buf.Length) {
        //    readonly int t = in.read(buf, size, buf.Length - size);
        //    if (t == -1) {
        //        break;
        //    }
        //    size += t;
        //}
        //return size;
	}
#endif
    
#if _UNITY_CONVERT_
    [Obsolete]
	public static byte[] toByteArray(object input, int size){
    return null; // TODO スタブ
    }
#else
	/**
	 * バイト列読み込み
	 *
	 * @param in
	 *			読み出し元
	 * @param size
	 *			バッファサイズ
	 * @return 読み出したバイト列
	 * @exception IOException
	 */
	public static byte[] toByteArray(InputStream in, int size){
		ByteArrayOutputStream bout;
		try {
			/* バッファ確保 */
			bout = new ByteArrayOutputStream(size * 2);
			byte[] bytes = new byte[size];
			/* size を流用 */
			while ((size = in.read(bytes)) > 0) {
				bout.write(bytes, 0, size);
			}
			// bytes = null;
			bout.close();
		} finally {
			/* 閉じる */
			in.close();
		}
		return bout.toByteArray();
	}
#endif

	// //////////////////////////////////////////////////////////////
	// ランダム
	// //////////////////////////////////////////////////////////////

	/** ランダム */
	public static Random RANDOM = new Random();

	/**
	 * ランダム取得 上位ビットほどランダム具合がいいので２の乗数の時は #getBitRandom(int) を使うこと！
	 *
	 * @see #getBitRandom(int)
	 * @param n
	 *			値の範囲
	 * @return (RANDOM.nextInt() >>> 1) % n;
	 */
	public static int getRandom(int n) {
		return getBitRandom(31) % n;
	}

	/**
	 * ランダム値の取得にはこれを使う
	 *
	 * @param n
	 *			値のビット数のランダム 1-32
	 * @return 32 を渡した場合負値も含まれるので注意
	 * @see Random#nextInt()
	 */
	public static int getBitRandom(int n) {
		return RANDOM.Next() >> (32 - n);
	}

	// //////////////////////////////////////////////////////////////
	// システム
	// //////////////////////////////////////////////////////////////

	/**
	 * メインスレッドの間隔取得
	 *
	 * @return ミリ秒
	 */
	public static int getThreadSpeed() {
		return threadSpeed;
	}

	/**
	 * メインスレッドの間隔取得
	 *
	 * @return ミリ秒
	 */
	public static void setThreadSpeed(int n) {
		threadSpeed = n;
	}

	/**
	 * 終了
	 */
	public static void exit() {
		isRunning = false;
	}

	// //////////////////////////////////////////////////////////////
	// 通信
	// //////////////////////////////////////////////////////////////

	/**
	 * ＷＥＢＴＯフラグセット＆ＵＲＬを指定
	 * TOBE ＷＥＢ　ＴＯ変更
	 * 
	 * @param b	WEB TO ﾌﾗｸﾞ
	 * @param bVer	VERSION UP ﾌﾗｸﾞ
	 * @param url	WEB TO URL / VERSION UP URL(for Only SoftBank) 	
	 * 
	 */
	public static void setWebTo(bool b, bool bVer, string url){
		isWebToVerUp = bVer; // TOBE 旧バージョンアプリ
		isWebTo = b;
		webURL = url;
	}

	/**
	 * 通信フラグを立ててサーバーコイン数を返す 初期値:-2 エラー:-3
	 * 
	 * @param coin
	 *            コイン数
	 * @return 0>=:サーバコイン数 -1:ユーザではない -2:初期値 -3:エラー
	 */
	public static int updateCoin(int coin) {
		return updateCoin(new int[]{coin});
	}
	
	/**
	 * 通信フラグを立ててサーバーコイン数を返す 初期値:-2 エラー:-3
	 * 
	 * @param coin
	 *            送信パラメータ
	 * @return 0>=:サーバコイン数 -1:ユーザではない -2:初期値 -3:エラー
	 */
	public static int updateCoin(int[] param) {
		/* 後で通信するときに使う */
		sendParam = param;
		if (serverCoin == Defines.DEF_SERVER_INI) {
			/* serviceRepaint()後通信するようにする */
			isUpdate = true;
		}
		int result = serverCoin;
		/* 通信結果を初期化 */
		serverCoin = Defines.DEF_SERVER_INI;
		return result;
	}
#if __COM_TYPE__

#else
//	/**
//	 * コインをアップし、フィールドを更新
//	 */
//	void _updateCoin() {
//		if (DEF_IS_DEBUG)
//			Defines.TRACE("called _updateCoin()");
//		System.gc(); // メモリ確保に努める
//		try {
//			//string server_url = getSourceURL()
//			//+ (DEF_Z_PHP_COINUP + DEF_UID_NULLGWDOCOMO + "&" + DEF_Z_PARAM_IMODE_COIN)
//			//+ winCoin + ("&" + DEF_Z_PARAM_TIME) + startTime
//			//+ ("&" + DEF_Z_PARAM_SLOT + DEF_APPLI_SERIAL);
//			string server_url = getSourceURL()
//			+ DEF_Z_PHP_COINUP + DEF_UID_NULLGWDOCOMO + "&" + DEF_Z_PARAM_IMODE_COIN
//			+ sendParam[0] + "&" + DEF_Z_PARAM_TIME + startTime
//			+ "&" + DEF_Z_PARAM_SLOT + DEF_KISYU_ID
//			+ "&type=" + DEF_APPLI_TYPE_ID				// TODO 2009/12/09 アプリ分類ＩＤを送信
//			+ "&" + DEF_Z_PARAM_VER + getAppVersion();	// TOBE アプリバージョンを送信
//			for(int i = 1;i<sendParam.Length ;i++){
//				if(DEF_IS_DEBUG_VOID_B){if(i==1) continue;}
//				server_url+="&"+(char)('a'+i)+"="+sendParam[i];
//			}
//			try {
//				serverCoin = coin_up(server_url);
//			} catch (ConnectionException ce) {
//				if (DEF_IS_DEBUG_COIN_UP) {
//					Defines.TRACE("_updateCoin() NO_RESOURCE?");
//					ce.printStackTrace();
//					setColor(getColor(255, 0, 0));
//					drawstringCenter(ce.getMessage(), 80);
//				}
//				if (ce.getStatus() == ConnectionException.NO_RESOURCES) {
//					try {
//						// イメージリソースを開放して
//						for (int i = 0; i < images.Length; i++) {
//							Image local = images[i];
//							images[i] = null;
//							local.dispose();
//						}
//						System.gc();
//						Thread.sleep(1000);
//						// 接続
//						serverCoin = coin_up(server_url);
//					} finally {
//						// リソース再取得
//						try {
//							loadResources();
//						} catch (InterruptedException e) {
//							Console.WriteLine(e);
//						}
//					}
//				} else {
//					throw ce;
//				}
//			}
//		if(false == DEF_IS_DEBUG_COIN_UP_TIME){ // TOBE コインＵＰＴＩＭＥデバッグ
//			/* 起動時間を更新 */
//			startTime = System.currentTimeMillis();
//		}
//			/* 獲得コインを初期化 */
//			winCoin = 0;
//		} catch (Exception e) {
//			if (DEF_IS_DEBUG_COIN_UP) {
//				Defines.TRACE("_updateCoin()");
//				e.printStackTrace();
//
//				clear();
//				setColor(getColor(255, 255, 255));
//				drawstringCenter(e.tostring(), 40);
//				drawstringCenter(e.getMessage(), 80);
//
//				try {
//					Thread.sleep(DEF_DEBUG_DISPLAY_SLEEP_MS);
//				} catch (InterruptedException e1) {
//					// nothing to do
//				}
//			}
//			serverCoin = DEF_SERVER_ERR; // エラーのとき
//		}
//	}
//
//	/**
//	 * コインアップ
//	 *
//	 * @param path
//	 * @return int
//	 * @throws Exception
//	 */
//	static int coin_up(string path) throws Exception {
//		int size;
//		byte[] buf = new byte[DEF_BUFFER_SIZE];
//		{
//			// HTTP には Connector.open(string, int, bool) を使わなければならない！
//			HttpConnection con = (HttpConnection) Connector.open(path,
//					Connector.READ, true);
//			con.setRequestMethod(HttpConnection.GET);
//			try {
//				size = read(con, buf);
//			} finally {
//				con.close();
//			}
//		}
//		System.gc();
//
//		if (DEF_IS_DEBUG_COIN_UP) {
//			string ret = new string(buf, 0, size).trim();
//			Defines.TRACE("coin_up() " + ret + " / " + ret.length());
//
//			clear();
//			setColor(getColor(255, 255, 255));
//			drawstringCenter(ret, 40);
//			drawstringCenter("" + ret.length(), 80);
//
//			try {
//				Thread.sleep(DEF_DEBUG_DISPLAY_SLEEP_MS);
//			} catch (InterruptedException e1) {
//				// nothing to do
//			}
//		}
//
//		size = Integer.parseInt(new string(buf, 0, size).trim());
//		return size;
//	}
#endif

#if _UNITY_CONVERT_
#else 
	/**
	 * 読み込み synchronized を外部で保証しなければならない
	 *
	 * @param con
	 *			connect 後のもの
	 * @param buf
	 *			バッファ
	 * @return 読み込んだサイズ
	 * @throws IOException
	 */
	public static int read(HttpConnection con, byte[] buf)
			throws IOException {
		int size;
		try {
			con.connect(); // 実際に接続
			if (DEF_IS_DEBUG)
				Defines.TRACE(con.getURL());
			InputStream in = con.openInputStream();
			size = read(in, buf);
			in.close();
		} finally {
			con.close();
		}
		return size;
	}
#endif

	// //////////////////////////////////////////////////////////////
	// ロード
	// //////////////////////////////////////////////////////////////

	/**
	 * ゲージを描く
	 */
	static void incrimentGauge() {        
#if _UNITY_CONVERT_
        // TODO C#移植 ゲージ描画
#else 
		// 不正な値をチェック
		int gauge_len = int_value[DEF_Z_GAUGE_LENGTH];
		int gauge_pos = int_value[DEF_Z_GAUGE_POSITION];
		if (gauge_len <= 0) {
			gauge_len = 1;
		}
		if (gauge_pos > gauge_len) {
			if (DEF_IS_DEBUG) {
				Defines.TRACE(gauge_pos + " / " + gauge_len);
			}
			gauge_pos = gauge_len;
		}
		int_value[DEF_Z_GAUGE_LENGTH] = gauge_len;
		int_value[DEF_Z_GAUGE_POSITION] = gauge_pos + 1;

		// 枠幅は1ポイント固定
		/** ゲージ幅 */
		int GAUGE_W = (DEF_POS_WIDTH - 4) - 1 * 2;
		/** ゲージ高 */
		int GAUGE_H = getFontHeight() / 2;
		/** ゲージX */
		int GAUGE_X = (DEF_POS_WIDTH - GAUGE_W) / 2;
		/** ゲージY */
		int GAUGE_Y = DEF_POS_HEIGHT / 2 + GAUGE_H;

		// ゲージ部分だけ描画
		// drawstringCenter(DEF_NOW_DOWNLOAD, DEF_POS_HEIGHT / 2);
		// setColor(getColor(255, 255, 255)); // 枠色
		drawRect(GAUGE_X - 1, GAUGE_Y - 1, GAUGE_W + 1, GAUGE_H + 1); // 枠
		// 現在の論理位置を座標に変換
		int bound = GAUGE_W * gauge_pos / gauge_len;
		setColor(getColor(170, 170, 170)); // 済み色
		fillRect(GAUGE_X, GAUGE_Y, bound, GAUGE_H); // 左側
		setColor(getColor(85, 85, 85)); // 未だ色
		fillRect(GAUGE_X + bound, GAUGE_Y, GAUGE_W - bound, GAUGE_H); // 右側
		setColor(getColor(255, 255, 255)); // デフォルト文字色に戻す
#endif
	}

	/**
	 * リソースのロード <code>
	 *  0 12345678 12
	 * +-+--------+--+-~+
	 * |T|DEVTYPE |S | ~|
	 * |I|(2:KDDI |I | ~|
	 * |P|   ONLY)|Z | ~|
	 * |E|		|E | ~|
	 * +-+--------+--+-~+
	 * </code>
	 *
	 * @throws InterruptedException
	 * @throws IOException
	 */
	void loadResources()
	{
	}
	
	/**
	 * バックライトのオンオフ
	 *
	 * @param isOn
	 *			つけるかどうか
	 */
	public static void setBacKLight(bool isOn) {
#if _UNITY_CONVERT_
#else
		PhoneSystem.setAttribute(PhoneSystem.DEV_BACKLIGHT,
				isOn ? PhoneSystem.ATTR_BACKLIGHT_ON
						: PhoneSystem.ATTR_BACKLIGHT_OFF);
#endif
	}

	/**
	 * バイブレータのオンオフ
	 *
	 * @param isOn
	 *			つけるかどうか
	 */
	public static void setVibrator(bool isOn) {
		// TOBE もし IllegalArgumentException が発生するようなら try - catch しましょう
		//PhoneSystem.setAttribute(PhoneSystem.DEV_VIBRATOR,
		//		isOn ? PhoneSystem.ATTR_VIBRATOR_ON
		//				: PhoneSystem.ATTR_VIBRATOR_OFF);
		//isVibrator = isOn;
	}

	// //////////////////////////////////////////////////////////////
	// キー
	// //////////////////////////////////////////////////////////////

	/**
	 * キーを取得する、渡したらクリアする
	 *
	 * @return キービット
	 */
	public static int getKeyPressed() {
		// if(Defines.DEF_IS_DEBUG)
		// Defines.TRACE("getKeyPressed called ="+int_value[Defines.DEF_Z_INT_KEYPRESS]);
		int returnKey = int_value[Defines.DEF_Z_INT_KEYPRESS];
		int_value[Defines.DEF_Z_INT_KEYPRESS] = 0;
		return returnKey;
	}

	/**
	 * キーを取得する、渡したらクリアする
	 *
	 * @return キービット
	 */
	public static int getKeyReleased() {
		// if(Defines.DEF_IS_DEBUG)
		// Defines.TRACE("getKeyPressed called
		// ="+int_value[Defines.DEF_Z_INT_KEYRELEASE]);
		int returnKey = int_value[Defines.DEF_Z_INT_KEYRELEASE];
		int_value[Defines.DEF_Z_INT_KEYRELEASE] = 0;
		return returnKey;
	}
	/**
	 * 押されているキーを取得する
	 *
	 * @return キービット
	 */
	public static int getKeyPressing() {
		return int_value[Defines.DEF_Z_INT_KEYPRESSING];
	}
	
	// //////////////////////////////////////////////////////////////
	// フォント
	// //////////////////////////////////////////////////////////////

	/**
	 * 設定フォントの長さを求める
	 *
	 * @param s
	 *			対象文字列
	 * @return int 1文字の横幅
	 * @see Font#stringWidth(java.lang.string)
	 */
	public static int stringWidth(string s) {
        return 0;
        // TODO C#移植 一旦コメントアウト
        //return font.stringWidth(s);
	}

	/**
	 * 設定フォントの高さを求める
	 *
	 * @return int 1文字の高さ
	 * @see Font#getHeight()
	 */
	public static int getFontHeight() {
        return 0;
        // TODO C#移植 一旦コメントアウト
        //return font.getHeight();
	}

	// //////////////////////////////////////////////////////////////
	// 色
	// //////////////////////////////////////////////////////////////

	/**
	 * 色は必ずこれで作成する
	 *
	 * @param r
	 *			赤成分 0-255
	 * @param g
	 *			緑成分 0-255
	 * @param b
	 *			青成分 0-255
	 * @return 色
	 * @see Graphics#getColorOfRGB(int, int, int)
	 */
	public static int getColor(int r, int g, int b) {
        return 0;
        // TODO C#移植 一旦コメントアウト
        //return Graphics.getColorOfRGB(r, g, b);
	}

	/**
	 * 色をセットする
	 *
	 * @param col
	 *			セットする色
	 * @see #getColor(int, int, int)
	 * @see Graphics#setColor(int)
	 */
	public static void setColor(int col) {
        // TODO C#移植 一旦コメントアウト
        //grp.setColor(col);
	}

	// //////////////////////////////////////////////////////////////
	// 画面
	// //////////////////////////////////////////////////////////////

	/**
	 * 画面サイズ
	 *
	 * @return 幅
	 * @see Frame#getWidth()
	 */
	public static int getWidth() {
        return 0;
        // TODO C#移植 一旦コメントアウト
        //return canvas.getWidth();
	}

	/**
	 * 画面サイズ
	 *
	 * @return 高さ
	 * @see Frame#getHeight()
	 */
	public static int getHeight() {
        return 0;
        // TODO C#移植 一旦コメントアウト
        //return canvas.getHeight();
	}

	/**
	 * オフセット位置
	 *
	 * @return X座標
	 */
	public static int getOffsetX() {
		return ofX;
	}

	/**
	 * オフセット位置
	 *
	 * @return Y座標
	 */
	public static int getOffsetY() {
		return ofY;
	}

	// //////////////////////////////////////////////////////////////
	// 再描画
	// //////////////////////////////////////////////////////////////

	/**
	 * 再描画の必要が有るかどうか このメソッドを呼び出すと必要性はクリアされる
	 *
	 * @return 再描画の必要有り
	 */
	public static bool setIsRepaint() {
        return false;
	}

	/**
	 * 再描画必要と設定
	 *
	 * @see #isRepaint()
	 * @deprecated 互換性の為においてあるだけだが容量の関係で外れるかもしれない
	 */
	public static void setRepaint() {}

	// //////////////////////////////////////////////////////////////
	// 描画
	// //////////////////////////////////////////////////////////////

	/**
	 * 原点をずらす
	 *
	 * @param x
	 * @param y
	 * @see Graphics#setOrigin(int, int)
	 */
	public static void setOrigin(int x, int y) {
		if (Defines.DEF_IS_DEBUG) {
			// Defines.TRACE("setOrigin(" + x + ", " + y + ")");
		}
		ofX = x;
		ofY = y;
		// grp.setOrigin(x, y);
	}

    // TODO C#移植 スタブ作成
    public class Graphics {
    }
    public class Graphics3D : Graphics {
    }

	/**
	 * 矩形をクリッピングする
	 *
	 * @param x
	 *			X座標
	 * @param y
	 *			Y座標
	 * @param w
	 *			幅
	 * @param h
	 *			高さ
	 * @see Graphics#setClip(int, int, int, int)
	 */
	public static void setClip(int x, int y, int w,
			int h) {
        // TODO C#移植 一旦コメントアウト
        //clipRegion[0] = x;
        //clipRegion[1] = y;
        //clipRegion[2] = w;
        //clipRegion[3] = h;

        //if (Defines.DEF_IS_DEBUG) {
        //    // Defines.TRACE("setClip(" + x + ", " + y + ")");
        //}
        //grp.setClip(x + ofX, y + ofY, w, h);
        //((Graphics3D)grp).setClipRect3D(x + ofX, y + ofY, w, h);//3D clip
	}

	/**
	 * drawImage
	 *
	 * @param id
	 *			イメージID
	 * @param x
	 *			X座標
	 * @param y
	 *			Y座標
	 * @see Graphics#drawImage(com.nttdocomo.ui.Image, int, int)
	 */
	public static void drawImage(int id, int x, int y) {
		if (Defines.DEF_IS_DEBUG) {
			if (id >= images.Length) {
				Defines.TRACE("だめお" + id);
				return;
			}
			if (images[id] == null) {
				Defines.TRACE("images[" + id + "]:" + images[id]);
			}
		}

        // TODO C#移植 一旦コメントアウト
        //// grp.drawImage(images[id], x, y);
        //grp.drawImage(images[id], x + ofX, y + ofY);
	}
	public static void drawImage(Image img, int x, int y) {
		if (img == null) {
			Defines.TRACE("画像ががない");
		}
		else
		{
            // TODO C#移植 一旦コメントアウト
            //grp.drawImage(img, x + ofX, y + ofY);
		}
	}
	/**
	 * Imageの範囲指定描画メソッド
	 *
	 * @param id
	 *			イメージＩＤ
	 * @param x
	 *			描画先のX座標
	 * @param y
	 *			描画先のY座標
	 * @param sx
	 *			元画像の書き出しX座標
	 * @param sy
	 *			元画像の書き出しX座標
	 * @param sw
	 *			元画像の描画幅
	 * @param sh
	 *			元画像の描画高さ
	 *
	 */
	public static void drawImageRegion(int id, int x, int y, int sx, int sy,
			int sw, int sh) {

		if (true) {
            // TODO C#移植 一旦コメントアウト
            //grp.drawImage(images[id], x + ofX, y + ofY, sx, sy, sw, sh);

		} else {
			// TODO SO902iで使ってはいけない for MEXA Core
			// 画面上の書き込みたい場所をクリッピングする
			setClipIntersect(x, y, sw, sh);

			drawImage(id, x - sx, y - sy);

            // TODO C#移植 一旦コメントアウト
            // 書き終わったら元のクリッピング領域を指定しなおす
            //grp.setClip(clipRegion[0] + ofX, clipRegion[1] + ofY,
            //        clipRegion[2], clipRegion[3]);

			// Defines.TRACE("描画直後のクリッピング領域＝ (" + clipRegion[0] + ", " +
			// clipRegion[1] + ", " + clipRegion[2] + ", " + clipRegion[3] +
			// ")");

		}

	}

	/**
	 *
	 * 元のクリッピング領域と新たなクリッピング領域が重なったら領域を作り直す
	 * 重なりが見られなければ、とりあえず（0,0,0,0）の領域をクリッピングしておく（表示しない）
	 *
	 * @deprecated SO902iで使ってはいけない
	 *
	 * @param x x
	 * @param y y
	 * @param w width
	 * @param h height
	 */
	private static void setClipIntersect(
		int x, int y, int w, int h
	) {

		bool intersect = true;

		int nx, ny, nw, nh;
		int nsx, nsy, nex, ney;

		nx = x;
		ny = y;
		nw = w;
		nh = h;
		nsx = nsy = nex = ney = 0;

		// Defines.TRACE("-------------------------------------------");

		int xmin, xmax, ymin, ymax = 0; // 元のクリッピング座標
		xmin = clipRegion[0];
		xmax = clipRegion[0] + clipRegion[2];
		ymin = clipRegion[1];
		ymax = clipRegion[1] + clipRegion[3];
		// Defines.TRACE("最初のクリッピング座標＝ (" + xmin + ", " + ymin + ", " +
		// xmax + ", " + ymax + ")");

		int xxmin, xxmax, yymin, yymax = 0; // 次のクリッピング座標
		xxmin = x;
		xxmax = x + w;
		yymin = y;
		yymax = y + h;
		// Defines.TRACE("次のクリッピング座標＝ (" + xxmin + ", " + yymin + ", " +
		// xxmax + ", " + yymax + ")");

		intersect = true;

		// 交差してなくねぇ？
		if (xmax <= xxmin) {
			intersect = false;
		}

		if (xxmax <= xmin) {
			intersect = false;
		}

		if (ymax <= yymin) {
			intersect = false;
		}

		if (yymax <= ymin) {
			intersect = false;
		}

		// 交差していないことが判明したら処理中断
		if (intersect == false) {
            // TODO C#移植 一旦コメントアウト
            //grp.setClip(0, 0, 0, 0);
			return;
		}

		// 交差具合
		nsx = (xmin > xxmin) ? xmin : xxmin; // ｘ始点
		nex = (xmax < xxmax) ? xmax : xxmax; // ｘ終点

		nsy = (ymin > yymin) ? ymin : yymin; // ｙ始点
		ney = (ymax < yymax) ? ymax : yymax; // ｙ終点

		// Defines.TRACE("これでクリッピング座標＝ (" + nsx + ", " + nsy + ", " + nex +
		// ", " + ney + ")");

		nx = nsx;
		nw = nex - nsx;
		ny = nsy;
		nh = ney - nsy;

		// Defines.TRACE("これでクリッピング＝ (" + nx + ", " + ny + ", " + nw + ", "
		// + nh + ")");
		// Defines.TRACE("\n");

        // TODO C#移植 一旦コメントアウト
		// 新たなクリッピング領域を指定する
        //grp.setClip(nx + ofX, ny + ofY, nw, nh);

	}

	public static void drawRotateImage(int id, int x, int y, int rot)
    {
        if (images == null)
        {
            return;
        }
        if (id < 0 || id >= images.Length)
        {
            return;
        }
	}


	public static void drawstring(string str, int x, int y) {}
	public static void drawstringRight(string str, int y) {}
	public static void drawstringCenter(string str, int y) {}
	public static void fillRect(int x, int y, int w, int h) {}
	public static void drawRect(int x, int y, int w, int h) {}
	public static void drawLine(int x1, int y1, int x2, int y2) {}
	public static void clear() {}

	/**
	 * オンオフ
	 *
	 * @return スイッチが入っているかどうか true: 鳴る
	 */
	public static bool changeSound() {
		try {
		} catch (Exception e) {
			return is_value[Defines.DEF_IS_SQ_ON];
		}

		is_value[Defines.DEF_IS_SQ_ON] = !is_value[Defines.DEF_IS_SQ_ON];

		return is_value[Defines.DEF_IS_SQ_ON];
	}

	/**
	 * サウンド設定 利便性の為に作ってあるが，直接キューに入れて構わない。
	 */
	public static void stopSound() {
		stopSound(Defines.DEF_SOUND_MULTI_SE);
		stopSound(Defines.DEF_SOUND_MULTI_BGM);
	}

	/**
	 * サウンド設定 利便性の為に作ってあるが，直接キューに入れて構わない。
	 *
	 * @param id
	 *			定義されていない負値を入れると落ちる
	 * @param isRepeat
	 *			この曲をリピートするかどうか
	 */
	public static void playSound( int id, bool isRepeat ){
		try{
			playSound( id, isRepeat, Defines.DEF_SOUND_MULTI_SE );
		}
		catch( Exception e ){
			if( Defines.DEF_IS_DEBUG ){
                Console.WriteLine(e.StackTrace);
			}
		}
	}

	/**
	 * サウンド設定 利便性の為に作ってあるが，直接キューに入れて構わない。
	 *
	 * @param mode
	 *			サウンドモード
	 */
	public static void stopSound(int mode) {
        switch (mode) { 
            case Defines.DEF_SOUND_MULTI_BGM:
                GameManager.StopBGM();
                break;
            case Defines.DEF_SOUND_MULTI_SE:
                GameManager.StopSE();
                break;

        }
	}

	/**
	 * サウンド設定 利便性の為に作ってあるが，直接キューに入れて構わない。
	 *
	 * @param id
	 *			定義されていない負値を入れると落ちる
	 * @param isRepeat
	 *			この曲をリピートするかどうか
	 * @param mode
	 *			サウンドモード
	 */
	public static void playSound(int id, bool isRepeat, int mode)
    {
		if (id < 0 || Defines.DEF_RES_SOUND_MAX <= id) {
			return;
		}

        GameManager.PlaySE(id);
	}

	public static void setVolume(int vol, int mode) {
	}

	/**
	 * キューを進める
	 *
	 * @param mode
	 */
	static void driveQueue(int mode) {
		if (soundQueue[mode,0] == Defines.DEF_SQ_REPEAT) {
			// リピートの時は何もしない
			return;
		}
		// ループでまわすなんて。。。
        for (int i = 1; i < soundQueue.GetLength(1); i++) {
			soundQueue[mode,i - 1] = soundQueue[mode,i];
		}
		// ちゃんとターミネート
        soundQueue[mode, soundQueue.GetLength(1) - 1] = Defines.DEF_SQ_NOP;
	}

	/**
	 * サウンドの実行
	 *
	 * @param mode
	 * @throws Exception
	 */
	static void processSound(int mode) {
	}

    /**
	 * 本体からデータを読み出す.
	 *
	 * @param return_buff
	 *			データを読み込む配列.
	 * @return 読み込んだサイズ.
	 */
	public static int getRecord(ref sbyte[] return_buff)
    {
		if (Defines.DEF_IS_DEBUG) {
            var cursor = 2; // TOBE 先頭は"読込済マーク"だったりするので要注意.
            Defines.TRACE("cursor: " + cursor);
		}

        var read_size = 0;

		try {
            SaveData.Load(ref return_buff);
            read_size = return_buff.Length;
        } catch (Exception e) {
            if (Defines.DEF_IS_DEBUG)
                Console.WriteLine(e.StackTrace);
            read_size = -1;
		}

        Defines.TRACE("getRecord:" + read_size);
		return read_size;
	}

	/**
	 * 本体にデータを書き込む.
	 *
	 * @param data
	 *			保存するデータ.
	 * @return 書き込んだサイズ.
	 */

	public static int setRecord(sbyte[] data)
    {
		int write_size = 0;

		if (data == null) {
			return -1;
		}

		if (Defines.DEF_IS_DEBUG)
        {
            int cursor = 2; // TOBE 先頭は"読込済マーク"だったりするので要注意.
            Defines.TRACE("cursor: " + cursor);
		}

        try {
            SaveData.Save(data);
            write_size = data.Length;
        } catch (Exception e) {
            if (Defines.DEF_IS_DEBUG) {
                Console.WriteLine(e);
                Console.WriteLine(e.StackTrace);
            }
            write_size = -1;
		}

        Defines.TRACE("setRecord:" + write_size);
		return write_size;
	}

	/**
	 * 円弧を描画.
	 *
	 * @param x 左上の座標x
	 * @param y 左上の座標y
	 * @param w width
	 * @param h height
	 * @param start start_angle
	 * @param angle "start_angle"+angle
	 */
	public static void drawArc(int x, int y, int w, int h, int start, int angle){
	}

	/**
	 * 楕円を描画.
	 *
	 * @param x 左上の座標x
	 * @param y 左上の座標y
	 * @param w width
	 * @param h height
	 */
	public static void drawCircle(int x, int y, int w, int h){
	}

	/**
	 * 扇形を塗りつぶし.
	 *
	 * @param x 左上の座標x
	 * @param y 左上の座標y
	 * @param w width
	 * @param h height
	 * @param start start_angle
	 * @param angle "start_angle"+angle
	 */
	public static void fillArc(int x, int y, int w, int h, int start, int angle){}
	public static void fillCircle(int x, int y, int w, int h){}

	/**
	 * FigureにTextureをセット.
	 *
	 * @param fig FigureNo
	 * @param tex TextureNo
	 * @deprecated
	 */
	public static void setTexture(int fig, int tex){
	}

	/**
	 * FigureにTextureをセット.
	 *
	 * @param fig FigureNo
	 * @param tex TextureNo_ARRAY
	 * */
	public static void setTextures(int fig, int[] tex){
	}

	/**
	 * FigureにActionをセット.
	 *
	 * @param fig FigureNo
	 * @param act ActionTableNo
	 * @param no ActionNo
	 * @param frm Frame
	 */
	public static void setPosture(int fig, int act, int no, int frm){
	}

	/**
	 * FigureのActionのFrameを進める.
	 *
	 * @param fig FigureNo
	 * @param act ActionTableNo
	 * @param no ActionNo
	 */
	public static void incPostureFrame(int fig, int act, int no)
    {
	}

	/**
	 * FigureのAction長を得る.
	 *
	 * @param act ActionTableNo
	 * @return action-num
	 */
	public static int getActionLength(int act)
    {
        return 0;
	}

	/**
	 * FigureのActionのFrame長を得る.
	 *
	 * @param act ActionTableNo
	 * @param no ActionNo
	 * @return frame_length
	 */
	public static int getActionFrameLength(int act, int no)
    {
        return 0;
    }


	public static void incPostureNo(int fig, int act){}

	/** 3D用中心座標 */
	public static int centerX = Defines.DEF_CONST3D_CENTER_X;
	/** 3D用中心座標 */
	public static int centerY = Defines.DEF_CONST3D_CENTER_Y;
	/** 3D用スケール */
	public static int scaleX = Defines.DEF_CONST3D_SCALE_X;
	/** 3D用スケール */
	public static int scaleY = Defines.DEF_CONST3D_SCALE_Y;

	static void set3Denv(){}
	public static void setLight(int x, int y, int z, int d, int a){}
	public static void drawFigure(int fig){}
	public static void drawFigure(int fig, int x, int y){}
	public static void flush3D(){}
	public static void activateEnvMap(){}
	public static void rotateX(int d){}
	public static void rotateY(int d){}
	public static void rotateZ(int d){}

	public static void rotateV(int d, int x, int y, int z){}

	public static void scale3D(int percent){}

	private static UnityEngine.Vector3 view_trans_position;
	private static UnityEngine.Vector3 view_trans_look;
	private static UnityEngine.Vector3 view_trans_up;

    public static void setViewTrans(
		int px, int py, int pz,
		int lx, int ly, int lz,
		int ux, int uy, int uz
	){}

	static void setViewTrans(
		UnityEngine.Vector3 position,
		UnityEngine.Vector3 look,
		UnityEngine.Vector3 up
	){}
	public static void drawPolygonRect(int[] poly, int[] col){}
	public static void drawPolygonRectAdd(int[] poly, int[] col){}
	public static void drawPolygonRectSub(int[] poly, int[] col){}
	public static void executeCommandListTextures(int[] cmd_list){}
}

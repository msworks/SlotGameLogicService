public partial class Defines {
/*
 * 作成日: 2003/08/19
 */


/**
 * Z デバッグ用 Df 定義
 * 
 * @author A03393KS
 */

	/** Mobuilder デバッグフラグ */
    // TODO ***DfDebugで同一定義あり。***
	//public static readonly bool DEF_IS_DEBUG = !false;

	/** SoundPlayer デバック */
    // TODO ***DfDebugで同一定義あり。***
    //public static readonly bool DEF_IS_DEBUG_SOUND = false;

	/** コインアップデバック */
	public static readonly bool DEF_IS_DEBUG_COIN_UP = false;

	/** コインアップＴＩＭＥデバッグ */
	public static readonly bool DEF_IS_DEBUG_COIN_UP_TIME = false; // TODO コインＵＰＴＩＭＥデバッグ

	/** b=をつけないで */
	public static readonly bool DEF_IS_DEBUG_VOID_B = false; // TODO b=のパラメータをつけないで、

	/** デバック表示時間 ms */
    public static readonly int DEF_DEBUG_DISPLAY_SLEEP_MS = 20 * 1000;

	/**
	 * 会員認証作業をしない
	 * true 	 認証しない
	 * false 	 認証する(デフォルト)
	 * 
	 */
	public static readonly bool DEF_IS_DEBUG_AUTH_THROUGH = true;

	/**
	 * 毎回認証(年月の判定をしない)
	 * 
	 * true 	 毎回認証(デフォルト)
	 * false 	 月１回の認証 
	 * 
	 */
	public static readonly bool DEF_IS_DEBUG_AUTH_EVERY = true;

	/**
	 * 体験版？
	 * true 	 体験版だから認証結果をもらっても起動しちゃう
	 * false 	 通常の認証動作をする(デフォルト)
	 */
	public static readonly bool DEF_IS_DEBUG_AUTH_THROUGH_FOR_TAIKEN = false;
}

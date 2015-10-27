using UnityEngine;
using System.Collections;

public partial class Defines {


/**
 * キー配列 ドコモにそろえる [ 不可：再設定 ]
 * 
 * 数字キー以外は KEY_BIT を使う事。 数字キーは注意して使用の事
 */


    ////////////////////////////////////////////////////////////////
    // キー（ドコモと同じです）
    // bit マクロが使えないのがちょっと難

    /** @deprecated 未定義 */
    public const int DEF_UNDEF = -1;

    /** 数字キー0 */
    public const int DEF_KEY_0 = 0;

    /** 数字キー1 */
    public const int DEF_KEY_1 = 1;

    /** 数字キー2 */
    public const int DEF_KEY_2 = 2;

    /** 数字キー3 */
    public const int DEF_KEY_3 = 3;

    /** 数字キー4 */
    public const int DEF_KEY_4 = 4;

    /** 数字キー5 */
    public const int DEF_KEY_5 = 5;

    /** 数字キー6 */
    public const int DEF_KEY_6 = 6;

    /** 数字キー7 */
    public const int DEF_KEY_7 = 7;

    /** 数字キー8 */
    public const int DEF_KEY_8 = 8;

    /** 数字キー9 */
    public const int DEF_KEY_9 = 9;

    /** @deprecated アスタリスク（スター） */
    public const int DEF_KEY_ASTERISK = 10;

    /** @deprecated パウンド（シャープ） */
    public const int DEF_KEY_POUND = 11;

    /** @deprecated 方向キー左 */
    public const int DEF_KEY_LEFT = 16;

    /** @deprecated 方向キー上 */
    public const int DEF_KEY_UP = 17;

    /** @deprecated 方向キー右 */
    public const int DEF_KEY_RIGHT = 18;

    /** @deprecated 方向キー下 */
    public const int DEF_KEY_DOWN = 19;

    /** @deprecated 選択キー */
    public const int DEF_KEY_SELECT = 20;

    /** @deprecated ソフトキー‘１’ */
    public const int DEF_KEY_SOFT1 = 21;

    /** @deprecated ソフトキー‘２’ */
    public const int DEF_KEY_SOFT2 = 22;

    /**
     * ソフトキー‘３’
     * 
     * @deprecated 使用されない
     */
    public const int DEF_KEY_SOFT3 = 23;

    /** @deprecated iアプリキー */
    public const int DEF_KEY_APPLI = 24;

    ////////////////////////////////////////////////////////////////
    // キービット
    // キーの比較にはこれを使うこと

    /** 数字キービット 0 */
    public const int DEF_KEY_BIT_0 = 1 << DEF_KEY_0;

    /** 数字キービット 1 */
    public const int DEF_KEY_BIT_1 = 1 << DEF_KEY_1;

    /** 数字キービット 2 */
    public const int DEF_KEY_BIT_2 = 1 << DEF_KEY_2;

    /** 数字キービット 3 */
    public const int DEF_KEY_BIT_3 = 1 << DEF_KEY_3;

    /** 数字キービット 4 */
    public const int DEF_KEY_BIT_4 = 1 << DEF_KEY_4;

    /** 数字キービット 5 */
    public const int DEF_KEY_BIT_5 = 1 << DEF_KEY_5;

    /** 数字キービット 6 */
    public const int DEF_KEY_BIT_6 = 1 << DEF_KEY_6;

    /** 数字キービット 7 */
    public const int DEF_KEY_BIT_7 = 1 << DEF_KEY_7;

    /** 数字キービット 8 */
    public const int DEF_KEY_BIT_8 = 1 << DEF_KEY_8;

    /** 数字キービット 9 */
    public const int DEF_KEY_BIT_9 = 1 << DEF_KEY_9;

    /** パウンド（シャープ）ビット */
    public const int DEF_KEY_BIT_ASTERISK = 1 << DEF_KEY_ASTERISK;

    /** アスタリスク（スター）ビット */
    public const int DEF_KEY_BIT_POUND = 1 << DEF_KEY_POUND;

    /** 方向キービット左 */
    public const int DEF_KEY_BIT_LEFT = 1 << DEF_KEY_LEFT;

    /** 方向キービット上 */
    public const int DEF_KEY_BIT_UP = 1 << DEF_KEY_UP;

    /** 方向キービット右 */
    public const int DEF_KEY_BIT_RIGHT = 1 << DEF_KEY_RIGHT;

    /** 方向キービット下 */
    public const int DEF_KEY_BIT_DOWN = 1 << DEF_KEY_DOWN;

    /** 選択キービット */
    public const int DEF_KEY_BIT_SELECT = 1 << DEF_KEY_SELECT;

    /** ソフトキー‘１’ビット */
    public const int DEF_KEY_BIT_SOFT1 = 1 << DEF_KEY_SOFT1;

    /** ソフトキー‘２’ビット */
    public const int DEF_KEY_BIT_SOFT2 = 1 << DEF_KEY_SOFT2;

    /**
     * ソフトキー‘３’
     * 
     * @deprecated 使用されない
     */
    public const int DEF_KEY_BIT_SOFT3 = 1 << DEF_KEY_SOFT3;

    /** iアプリキービット */
    public const int DEF_KEY_BIT_APPLI = 1 << DEF_KEY_APPLI;

	/** 回転なし */
	public const int DEF_FLIP_NONE = 0;

	/** 左右反転 */
	public const int DEF_FLIP_HORIZONTAL = 1;
	/** 左右反転 */
	public const int DEF_FLIP_LR = DEF_FLIP_HORIZONTAL;

	/** 上下反転 */
	public const int DEF_FLIP_VERTICAL = 2;
	/** 上下反転 */
	public const int DEF_FLIP_UP_DOWN = DEF_FLIP_VERTICAL;

	/** 180度回転 */
	public const int DEF_FLIP_ROTATE = 3;
	/** 180度回転 */
	public const int DEF_FLIP_180 = DEF_FLIP_ROTATE;

	/** 270度回転 */
	public const int DEF_FLIP_ROTATE_LEFT = 4;
	/** 270度回転 */
	public const int DEF_FLIP_270 = DEF_FLIP_ROTATE_LEFT;

	/** 90度回転 */
	public const int DEF_FLIP_ROTATE_RIGHT = 5;
	/** 90度回転 */
	public const int DEF_FLIP_90 = DEF_FLIP_ROTATE_RIGHT;


}

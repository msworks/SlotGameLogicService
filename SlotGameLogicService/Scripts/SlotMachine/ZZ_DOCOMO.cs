#define _UNITY_CONVERT_ // Unity+C#移植作業用

using System;
using System.Threading;
using UnityEngine;

public partial class ZZ
{
    clOHHB_V23 v23;
    GameManager GameManager;

    public ZZ(clOHHB_V23 v23)
    {
        this.v23 = v23;
    }

    public void SetV23(clOHHB_V23 v23)
    {
        this.v23 = v23;
    }

    public void SetGameManager(GameManager GameManager)
    {
        this.GameManager = GameManager;
    }

    /** int 変数 */
    public readonly int[] int_value = new int[Defines.DEF_Z_INT_MAX];

    /** イメージ */
    readonly Image[] images = new Image[Defines.DEF_RES_IMAGE_MAX];
    public class Image { }// TODO C#移植 スタブ

    class Font
    {
        static public int SIZE_TINY = 0;
        static public Font getFont(int size) { return new Font(); }
    }

    /** offsetX */
    int ofX;

    /** offsetY */
    int ofY;

    /** 1ループ時間(ms) */
    int threadSpeed;

    Random RANDOM = new Random();

    public ZZ() { }

    /**
     * ランダム値の取得にはこれを使う
     * @param n 値のビット数のランダム 1-32
     * @return 32 を渡した場合負値も含まれるので注意
     * @see Random#nextInt()
     */
    public int getBitRandom(int n)
    {
        return RANDOM.Next() >> (32 - n);
    }

    public int getThreadSpeed(){ return threadSpeed; }
    public void setThreadSpeed(int n){ threadSpeed = n; }

    /**
     * キーを取得する、渡したらクリアする
     * @return キービット
     */
    public int getKeyPressed()
    {
        int returnKey = int_value[Defines.DEF_Z_INT_KEYPRESS];
        int_value[Defines.DEF_Z_INT_KEYPRESS] = 0;
        return returnKey;
    }

    /**
     * 押されているキーを取得する
     * @return キービット
     */
    public int getKeyPressing()
    {
        return int_value[Defines.DEF_Z_INT_KEYPRESSING];
    }

    /**
     * 設定フォントの長さを求める
     * @param s 対象文字列
     * @return int 1文字の横幅
     */
    static public int stringWidth(string s)
    {
        return 0;
    }

    /**
     * 設定フォントの高さを求める
     * @return int 1文字の高さ
     */
    static public int getFontHeight()
    {
        return 0;
    }

    /**
     * 色は必ずこれで作成する
     * @param r 赤成分 0-255
     * @param g 緑成分 0-255
     * @param b 青成分 0-255
     * @return 色
     */
    static public int getColor(int r, int g, int b)
    {
        return 0;
    }

    /**
     * 色をセットする
     * @param col セットする色
     */
    public void setColor(int col)
    {
    }

    /**
     * 画面サイズ
     * @return 幅
     */
    public int getWidth()
    {
        return 0;
    }

    /**
     * 画面サイズ
     *
     * @return 高さ
     * @see Frame#getHeight()
     */
    public int getHeight()
    {
        return 0;
        // TODO C#移植 一旦コメントアウト
        //return canvas.getHeight();
    }

    /**
     * オフセット位置
     *
     * @return X座標
     */
    public int getOffsetX()
    {
        return ofX;
    }

    /**
     * オフセット位置
     *
     * @return Y座標
     */
    public int getOffsetY()
    {
        return ofY;
    }

    /**
     * 原点をずらす
     * @param x
     * @param y
     */
    public void setOrigin(int x, int y)
    {
        ofX = x;
        ofY = y;
    }

    /**
     * 矩形をクリッピングする
     * @param x X座標
     * @param y Y座標
     * @param w 幅
     * @param h 高さ
     */
    public void setClip(int x, int y, int w, int h){}

    /**
     * drawImage
     * @param id イメージID
     * @param x X座標
     * @param y Y座標
     */
    public void drawImage(int id, int x, int y)
    {
        if (Defines.DEF_IS_DEBUG)
        {
            if (id >= images.Length)
            {
                Defines.TRACE("だめお" + id);
                return;
            }
            if (images[id] == null)
            {
                Defines.TRACE("images[" + id + "]:" + images[id]);
            }
        }
    }

    public void fillRect(int x, int y, int w, int h) { }

    /// <summary>
    /// サウンド設定
    /// </summary>
    /// <param name="mode">サウンドモード</param>
    public void stopSound(int mode)
    {
        switch (mode)
        {
            case Defines.DEF_SOUND_MULTI_BGM:
                GameManager.StopBGM();
                break;
            case Defines.DEF_SOUND_MULTI_SE:
                GameManager.StopSE();
                break;

        }
    }

    /// <summary>
    /// サウンド設定
    /// </summary>
    /// <param name="id">定義されていない負値を入れると落ちる</param>
    /// <param name="isRepeat">この曲をリピートするかどうか</param>
    /// <param name="mode">サウンドモード</param>
    public void playSound(int id, bool isRepeat, int mode)
    {
        if (id < 0 || Defines.DEF_RES_SOUND_MAX <= id)
        {
            return;
        }

        GameManager.PlaySE(id);
    }

    /// <summary>
    /// 本体からデータを読み出す
    /// </summary>
    /// <param name="return_buff">データを読み込む配列</param>
    /// <returns>読み込んだサイズ</returns>
    public int getRecord(ref sbyte[] return_buff)
    {
        if (Defines.DEF_IS_DEBUG)
        {
            var cursor = 2; // TOBE 先頭は"読込済マーク"だったりするので要注意.
            Defines.TRACE("cursor: " + cursor);
        }

        var read_size = 0;

        try
        {
            SaveData.Load(ref return_buff);
            read_size = return_buff.Length;
        }
        catch (Exception e)
        {
            if (Defines.DEF_IS_DEBUG)
                Console.WriteLine(e.StackTrace);
            read_size = -1;
        }

        Defines.TRACE("getRecord:" + read_size);
        return read_size;
    }

    /// <summary>
    /// 本体にデータを書き込む
    /// </summary>
    /// <param name="data">保存するデータ</param>
    /// <returns>書き込んだサイズ</returns>
    public int setRecord(sbyte[] data)
    {
        int write_size = 0;

        if (data == null)
        {
            return -1;
        }

        if (Defines.DEF_IS_DEBUG)
        {
            int cursor = 2; // TOBE 先頭は"読込済マーク"だったりするので要注意.
            Defines.TRACE("cursor: " + cursor);
        }

        try
        {
            SaveData.Save(data);
            write_size = data.Length;
        }
        catch (Exception e)
        {
            if (Defines.DEF_IS_DEBUG)
            {
                Console.WriteLine(e);
                Console.WriteLine(e.StackTrace);
            }
            write_size = -1;
        }

        Defines.TRACE("setRecord:" + write_size);
        return write_size;
    }

    public void setLight(int x, int y, int z, int d, int a) { }
    public void flush3D() { }
    public void scale3D(int percent) { }
    public void drawPolygonRect(int[] poly, int[] col) { }
    public void drawPolygonRectAdd(int[] poly, int[] col) { }
    public void drawPolygonRectSub(int[] poly, int[] col) { }
}

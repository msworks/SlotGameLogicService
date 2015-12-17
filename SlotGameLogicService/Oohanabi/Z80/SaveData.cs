using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;

/// <summary>
/// セーブデータ管理クラス
/// </summary>
public class SaveData {

    public const string SAVE_FILE_NAME = "save.dat";
    public const string PATH_DATA_CHIP = "ClZ80RAM_big";

    /// <summary>
    /// セーブデータ書き込み
    /// </summary>
    /// <param name="saveData">書き込むデータ/param>
    public static void Save(sbyte[] saveData) {
        PlayerPrefs.SetString(SAVE_FILE_NAME, new string(Array.ConvertAll<sbyte, char>(saveData, v => (char)v)));
    }

    /// <summary>
    /// セーブデータ読み込み
    /// </summary>
    /// <param name="loadData">読み込み先バッファ</param>
    public static void Load(ref sbyte[] loadData) {
        string buffer = PlayerPrefs.GetString(SAVE_FILE_NAME);
        Buffer.BlockCopy(buffer.ToCharArray(), 0, loadData, 0, buffer.ToCharArray().Length);
    }

    /// <summary>
    /// チップデータ読み込み
    /// </summary>
    /// <returns></returns>
    public static sbyte[] LoadChipData() {
        try {
            TextAsset a = Resources.Load<TextAsset>(PATH_DATA_CHIP);
            return Array.ConvertAll<byte, sbyte>(a.bytes, v => (sbyte)v);
        } catch (Exception e) {
            Defines.TRACE("LoadBinary失敗:" + e.ToString());
            return null;
        }
    }
}

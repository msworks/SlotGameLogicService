using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager
{
    public static GameManager Instance = new GameManager();

    public static Defines.ForceYakuFlag forceYakuValue;

    public static void PlayBGM(int soundID, bool isLoop){}

    public static void PlaySE(int soundID){

    }

    public static void StopSE() { }
    public static void StopBGM() { }

    public void OnCoinInsert() {
        Console.WriteLine("[INFO]OnCoinInsert");
    }

    public void SetReelTexture(int row, int col, bool isLit) { }
    public void Set4thReelTexture(bool isLit) {}
    public void StopAutoPlay(string log){}
    public void OnStartPlay(){}

    public static int GetRandomSeed()
    {
        return (((int)(Util.GetMilliSeconds())) & 0xFFFF);
    }

    public void OnBonusRB(){}
    public void OnBonusBB(){}
    public void UpdateCommonUIAvg(){}
    public void OnBonusEnd(int bonus_incount){}
    public void OnCountUp(){}

}

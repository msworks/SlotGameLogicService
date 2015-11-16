using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager
{
    int seed;
    Mobile mobile;

    public Defines.ForceYakuFlag forceYakuValue;

    public int Seed
    {
        get
        {
            return seed;
        }
    }

    public GameManager(Mobile mobile)
    {
        var seed = (((int)(Util.GetMilliSeconds())) & 0xFFFF);
        this.seed = seed;
        this.mobile = mobile;
        Console.WriteLine("[INFO]Seed:"+ seed);
    }

    public void OnCoinInsert()
    {
        mobile.Playstate = PLAYSTATE.InsertCoin;
        Console.WriteLine("[INFO]CoinInsert");
    }

    public void OnStartPlay()
    {
        mobile.Playstate = PLAYSTATE.Lever;
        Console.WriteLine("[INFO]Lever");
    }

    public void OnAllReelStopped()
    {
        mobile.Playstate = PLAYSTATE.AllReelStopped;
        Console.WriteLine("[INFO]ALL REEL STOPPED");
    }

    public void PlayBGM(int soundID, bool isLoop){}
    public void PlaySE(int soundID){}
    public void StopSE() { }
    public void StopBGM() { }
    public void SetReelTexture(int row, int col, bool isLit) { }
    public void Set4thReelTexture(bool isLit) {}
    public void StopAutoPlay(string log){}
    public void OnBonusRB(){}
    public void OnBonusBB(){}
    public void UpdateCommonUIAvg(){}
    public void OnBonusEnd(int bonus_incount){}
    public void OnCountUp(){}
}

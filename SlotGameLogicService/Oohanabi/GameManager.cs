public class GameManager
{
    int seed;
    Mobile mobile;
    public Defines.ForceYakuFlag forceYakuValue;
    public bool SettingZeroMode = false;
    public Setting0Machine setting0Machine = new Setting0Machine();

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
    }

    public void OnCoinInsert()
    {
        mobile.Playstate = PLAYSTATE.InsertCoin;
    }

    public void OnStartPlay()
    {
        mobile.Playstate = PLAYSTATE.Lever;
    }

    public void OnAllReelStopped()
    {
        mobile.Playstate = PLAYSTATE.AllReelStopped;
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

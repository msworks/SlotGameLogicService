using System;

public class CallbackToController
{
    /// <summary>
    /// 引数はペイアウトするコイン数
    /// </summary>
    public Action<int> Payout;

    /// <summary>
    /// 引数は役
    /// </summary>
    public Action<int> ReelStart;

    /// <summary>
    /// 引数はbutton, reelIndex
    /// </summary>
    public Action<int, int> ButtonStop;

    /// <summary>
    /// 引数は押下キー
    /// </summary>
    public Action<int> KeyTrigger;

    public Action Bet;
}

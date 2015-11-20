namespace TheOcean
{
    /// <summary>
    /// 役
    /// </summary>
    public enum Yaku
    {
        /// <summary>
        /// はずれ
        /// </summary>
        Hazure = 0x0000,

        /// <summary>
        /// 当たり
        /// </summary>
        Atari = 0x0001,
    }

    /// <summary>
    /// ルート
    /// </summary>
    public enum Route
    {
        /// <summary>
        /// 回収
        /// </summary>
        Abandon = 0x0000,

        /// <summary>
        /// チャッカー -> 回収
        /// </summary>
        Chacker = 0x0100,

        /// <summary>
        /// チャッカー -> 7賞球
        /// </summary>
        Chacker7 = 0x1107,

        /// <summary>
        /// 7個賞球
        /// </summary>
        Syokyu7 = 0x1007,

        /// <summary>
        /// 15個賞球
        /// </summary>
        Syokyu15 = 0x100F,

        /// <summary>
        /// 回転体突入
        /// </summary>
        Kaitenti = 0x0200,

        /// <summary>
        /// アタッカー
        /// </summary>
        Atacker = 0x0300,

        /// <summary>
        /// 空通知
        /// </summary>
        Nothing = 0xFFFF,
    }
}

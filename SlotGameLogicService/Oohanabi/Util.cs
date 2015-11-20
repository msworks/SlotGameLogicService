public static class Util
{
    public static long GetMilliSeconds()
    {
        return System.DateTime.Now.Ticks / 10000;
    }
}

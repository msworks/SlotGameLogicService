//using UnityEngine;
using System.Collections;

public static class Util {
    public static long GetMilliSeconds() {
        return System.DateTime.Now.Ticks / 10000;
    }
}

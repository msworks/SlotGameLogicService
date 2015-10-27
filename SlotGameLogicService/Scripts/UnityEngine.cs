using System;
using System.ComponentModel;

namespace UnityEngine
{
    public enum RuntimePlatform {
        OSXEditor = 0,
        OSXPlayer = 1,
        WindowsPlayer = 2,
        OSXWebPlayer = 3,
        OSXDashboardPlayer = 4,
        WindowsWebPlayer = 5,
        WindowsEditor = 7,
        IPhonePlayer = 8,
        PS3 = 9,
        XBOX360 = 10,
        Android = 11,
        [Obsolete("NaCl export is no longer supported in Unity 5.0+.")]
        NaCl = 12,
        LinuxPlayer = 13,
        [Obsolete("FlashPlayer export is no longer supported in Unity 5.0+.")]
        FlashPlayer = 15,
        WebGLPlayer = 17,
        [Obsolete("Use WSAPlayerX86 instead")]
        MetroPlayerX86 = 18,
        WSAPlayerX86 = 18,
        WSAPlayerX64 = 19,
        [Obsolete("Use WSAPlayerX64 instead")]
        MetroPlayerX64 = 19,
        WSAPlayerARM = 20,
        [Obsolete("Use WSAPlayerARM instead")]
        MetroPlayerARM = 20,
        WP8Player = 21,
        BlackBerryPlayer = 22,
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("BB10Player has been deprecated. Use BlackBerryPlayer instead (UnityUpgradable) -> BlackBerryPlayer", true)]
        BB10Player = 22,
        TizenPlayer = 23,
        PSP2 = 24,
        PS4 = 25,
        PSM = 26,
        XboxOne = 27,
        SamsungTVPlayer = 28,
        WiiU = 30,
    }

    public sealed class Font { }

    public struct Vector3 { }

    public class Texture { }

    public sealed class Application {
        public static void OpenURL(string url);
        public static RuntimePlatform platform { get; }
        public static void Quit();
    }

    public sealed class AudioListener
    {
        public AudioListener();
        public static bool pause { get; set; }
        public static float volume { get; set; }
        public static void GetOutputData(float[] samples, int channel);
        [Obsolete("GetOutputData returning a float[] is deprecated, use GetOutputData and pass a pre allocated array instead.")]
        public static float[] GetOutputData(int numSamples, int channel);
    }
}

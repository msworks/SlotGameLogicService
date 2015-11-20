﻿using System;
using System.Text;

static class RandomExtension
{
    /// <summary>
    /// 0～255のランダム値を返す
    /// </summary>
    static public int RndFF(this Random source)
    {
        var CHUSEN_LEN = 256;       // 抽選のサイズ
        return source.Next(CHUSEN_LEN);
    }

    /// <summary>
    /// 0～65535のランダム値を返す
    /// </summary>
    static public int RndFFFF(this Random source)
    {
        var CHUSEN_LEN = 65536;       // 抽選のサイズ
        return source.Next(CHUSEN_LEN);
    }
}

static class stringExtension
{
    /// <summary>
    /// Single Quart
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    static public string SQ(this string source)
    {
        return Quart(source, "'");
    }

    /// <summary>
    /// Double Quart
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    static public string DQ(this string source)
    {
        return Quart(source, @"""");
    }

    static string Quart(string source, string quart)
    {
        var builder = new StringBuilder();
        builder.Append(quart);
        builder.Append(source);
        builder.Append(quart);
        return builder.ToString();
    }

    static public int ParseInt(this string source)
    {
        return int.Parse(source);
    }
}

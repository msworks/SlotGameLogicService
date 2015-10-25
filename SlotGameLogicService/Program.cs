using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Threading;
using System.Runtime.Serialization;

class GameLogic
{
    static void Main(string[] args)
    {
        new GameLogic().Run();
    }

    void Run()
    {
        var writer = Console.Out;
        var listener = new HttpListener();

        try
        {
            listener.Prefixes.Add("http://*:9876/");
            listener.Start();
        }
        catch (Exception e)
        {
            writer.WriteLine("[ERROR]GAMELOGIC SERVER RUN FAILED");
            writer.WriteLine(e);
            return;
        }

        writer.WriteLine("[INFO]GAMELOGIC SERVER RUN PORT:9876");

        Func<string> defaultResponse = () =>
        {
            var table = new[]
            {
                new { key = "result", value = "error".DQ() },
            };

            var res = "{" +
                      string.Join(",", table.Select(e => e.key.DQ() + ":" + e.value)) +
                      "}";

            return res;
        };

        Func<string> configResponse = () =>
        {
            var table = new []
            {
                new { key = "balance", value = "123.4" },
                new { key = "setting", value = "1" },
                new { key = "reelleft", value = "0" },
                new { key = "reelcenter", value = "0" },
                new { key = "reelright", value = "0" },
                new { key = "seed", value = "0" },
            };

            var res = "{" +
                      string.Join(",", table.Select(e => e.key.DQ() + ":" + e.value)) +
                      "}";

            return res;
        };

        Func<string> initResponse = () =>
        {
            var table = new[]
            {
                new { key = "balance", value = "123.4" },
            };

            var res = "{" +
                      string.Join(",", table.Select(e => e.key.DQ() + ":" + e.value)) +
                      "}";

            return res;
        };

        Func<string> playResponse = () =>
        {
            var table = new[]
            {
                new { key = "balance", value = "123.4" },
                new { key = "yaku", value = "1" },
                new { key = "route", value = "2" },
            };

            var res = "{" +
                      string.Join(",", table.Select(e => e.key.DQ() + ":" + e.value)) +
                      "}";

            return res;
        };

        Func<string> correctResponse = () =>
        {
            var table = new[]
            {
                new { key = "balance", value = "123.4" },
                new { key = "result", value = "WIN".DQ() },
            };

            var res = "{" +
                      string.Join(",", table.Select(e => e.key.DQ() + ":" + e.value)) +
                      "}";

            return res;
        };

        var pathTable = new []
        {
            new { path = "/config", response = configResponse },
            new { path = "/init", response = initResponse },
            new { path = "/play", response = playResponse },
            new { path = "/correct", response = correctResponse },
        };

        while (true)
        {
            var context = listener.GetContext();
            var req = context.Request;
            var res = context.Response;

            var url = req.Url;
            var localPath = url.LocalPath;
            var query = url.Query;

            var responseString = pathTable.Where(pt => pt.path == localPath)
                                          .Select(pt => pt.response())
                                          .FirstOrDefault() ?? defaultResponse();

            var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

            res.ContentLength64 = buffer.Length;
            res.OutputStream.Write(buffer, 0, buffer.Length);
            res.Close();
        }
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
}

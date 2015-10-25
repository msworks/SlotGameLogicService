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
        }

        writer.WriteLine("[INFO]GAMELOGIC SERVER RUN PORT:9876");

        while (true)
        {
            var context = listener.GetContext();
            var req = context.Request;
            var res = context.Response;

            var url = req.Url;

            var responseString = @"{""test"":123}";
            var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

            res.ContentLength64 = buffer.Length;
            res.OutputStream.Write(buffer, 0, buffer.Length);
            res.Close();
        }
    }
}



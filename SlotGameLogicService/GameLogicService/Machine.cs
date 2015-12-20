using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLogicService
{
    using Associative = Dictionary<string, string>;
    using GameId = String;
    using UserId = String;

    public interface IMachine
    {
        Associative Config(Associative param);
        Associative Init(Associative param);
        Associative Play(Associative param);
        Associative Collect(Associative param);
    }

    public class MachineFactory
    {
        static public IMachine Create(GameId gameId, UserId userId, int settingValue)
        {
            var dais = 100;
            var oceanStart = 0;
            var hanabiStart = oceanStart + dais;
            var pierotStart = hanabiStart + dais;
            var otherStart = pierotStart + dais;

            var oceans = Enumerable.Range(oceanStart, dais).Select(i=>
                new {
                    gameId = i.ToString(),
                    createFunc = (Func<GameId, UserId, int, IMachine>)CreateTheOcean
                });

            var hanabi = Enumerable.Range(hanabiStart, dais).Select(i
                => new {
                    gameId = i.ToString(),
                    createFunc = (Func<GameId, UserId, int, IMachine>)CreateOohanabi
                });

            var pierot = Enumerable.Range(pierotStart, dais).Select(i
                => new {
                    gameId = i.ToString(),
                    createFunc = (Func<GameId, UserId, int, IMachine>)CreateOohanabi
                });

            var other = Enumerable.Range(otherStart, dais).Select(i
                => new {
                    gameId = i.ToString(),
                    createFunc = (Func<GameId, UserId, int, IMachine>)CreateOohanabi
                });

            var machineFactory = oceans.Concat(hanabi).Concat(pierot).Concat(other);

            return machineFactory.Where(table => table.gameId == gameId)
                                 .Select(table => table.createFunc(gameId, userId, settingValue))
                                 .FirstOrDefault();
        }

        static IMachine CreateOohanabi(GameId gameId, UserId userId, int settingValue)
        {
            return new Oohababi(gameId, userId, settingValue);
        }

        static IMachine CreateTheOcean(GameId gameId, UserId userId, int settingValue)
        {
            return new TheOcean(gameId, userId, settingValue);
        }
    }

    public enum MACHINE_STATE
    {
        CREATED,
        CONFIG,
        INIT,
        PLAY,
        COLLECT,
    }

}

public enum PLAYSTATE
{
    InsertCoin,
    Lever,
    AllReelStopped,
};

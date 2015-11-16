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
        static public IMachine Create(GameId gameId, UserId userId)
        {
            var machineFactory = new[]
            {
                new { gameId = "1", createFunc = (Func<GameId, UserId, IMachine>)CreateTheOcean },
                new { gameId = "2", createFunc = (Func<GameId, UserId, IMachine>)CreateOohanabi },
            };

            return machineFactory.Where(table => table.gameId == gameId)
                                 .Select(table => table.createFunc(gameId, userId))
                                 .FirstOrDefault();
        }

        static IMachine CreateOohanabi(GameId gameId, UserId userId)
        {
            return new Oohababi(gameId, userId);
        }

        static IMachine CreateTheOcean(GameId gameId, UserId userId)
        {
            return new TheOcean(gameId, userId);
        }
    }

    public enum MACHINE_STATE
    {
        CREATED,
        CONFIG,
        INIT,
        PLAY,
        CORRECT,
    }

}

public enum PLAYSTATE
{
    InsertCoin,
    Lever,
    AllReelStopped,
};

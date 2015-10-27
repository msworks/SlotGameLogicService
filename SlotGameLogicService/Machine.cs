using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotGameLogicService
{
    interface IMachine
    {
    }

    class MachineFactory
    {
        static public IMachine Create(string gameId)
        {
            var machineFactory = new[]
            {
                new { gameId = "1", createFunc = (Func<IMachine>)CreateOohanabi },
                new { gameId = "2", createFunc = (Func<IMachine>)CreateTheOcean },
            };

            return machineFactory.Where(table => table.gameId == gameId)
                                 .Select(table => table.createFunc())
                                 .FirstOrDefault();
        }

        static IMachine CreateOohanabi()
        {
            return new Oohababi();
        }

        static IMachine CreateTheOcean()
        {
            return new TheOcean();
        }
    }

    class Oohababi : IMachine
    {
    }

    class TheOcean : IMachine
    {
    }
}

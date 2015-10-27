using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogicService
{
    interface IMachine
    {
        void Config();
        void Init();
        void Play();
        void Correct();
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

    enum MACHINE_STATE
    {
        CREATED,
        CONFIG,
        INIT,
        PLAY,
        CORRECT,
    }

    abstract class AbstractMachine : IMachine
    {
        MACHINE_STATE state;

        public AbstractMachine()
        {
            state = MACHINE_STATE.CREATED;
        }

        public void Config()
        {
            state = MACHINE_STATE.CONFIG;
        }

        public void Correct()
        {
            state = MACHINE_STATE.INIT;
        }

        public void Init()
        {
            state = MACHINE_STATE.PLAY;
        }

        public void Play()
        {
            state = MACHINE_STATE.CORRECT;
        }
    }

    class Oohababi : AbstractMachine, IMachine
    {
    }

    class TheOcean : AbstractMachine, IMachine
    {
    }
}

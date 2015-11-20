using System.Linq;

namespace TheOcean
{
    public enum ReelState
    {
        Stop,
        Cycle,
    }

    /// <summary>
    /// リールの状態遷移マシン
    /// </summary>
    class ReelMachine
    {
        public ReelState State
        {
            get;
            private set;
        }

        public ReelMachine()
        {
            State = ReelState.Stop;
        }

        public void Switch()
        {
            var table = new[]
            {
                new { pre = ReelState.Stop, post = ReelState.Cycle },
                new { pre = ReelState.Cycle, post = ReelState.Stop },
            };

            State = table.Where(t => t.pre == State)
                         .First()
                         .post;
        }
    }
}

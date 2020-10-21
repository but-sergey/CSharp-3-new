using System;
using System.Threading;

namespace TestConsole
{
    class MathTask
    {
        private readonly Thread _CalculationThread;
        private int _Result;
        private bool _IsCompleted;

        public int Result
        {
            get
            {
                if (!_IsCompleted)
                    _CalculationThread.Join();
                return _Result;
            }
        }

        public MathTask(Func<int> Calculation)
        {
            _CalculationThread = new Thread(
                () =>
                {
                    _Result = Calculation();
                    _IsCompleted = true;
                }) { IsBackground = true };
        }

        public void Start() => _CalculationThread.Start();
    }
}

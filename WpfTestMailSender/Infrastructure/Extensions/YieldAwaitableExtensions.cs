using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace System.Threading.Tasks
{
    static class YieldAwaitableExtensions
    {
        public static YieldAsyncAwaitable ConfigureAwait(this YieldAwaitable _, bool LockContext)
        {
            return new YieldAsyncAwaitable(LockContext);
        }
    }

    internal struct YieldAsyncAwaitable
    {
        private readonly bool _LockContext;

        public YieldAsyncAwaitable(bool LockContext) => _LockContext = LockContext;

        public YieldAsyncAwaiter GetAwaiter() => new YieldAsyncAwaiter(_LockContext);

        [StructLayout(LayoutKind.Sequential, Size = 1)]
        public struct YieldAsyncAwaiter : ICriticalNotifyCompletion, INotifyCompletion
        {
            [NotNull] private static readonly WaitCallback __WaitCallbackRunAction = RunAction;
            [NotNull] private static readonly SendOrPostCallback __SendOrPostCallbackRunAction = RunAction;

            public bool IsCompleted => false;

            private static void RunAction([NotNull] object action) => ((Action)action)();

            [SecurityCritical]
            private static void QueueContinuation([NotNull] Action continuation, bool FlowContext)
            {
                if (continuation is null) throw new ArgumentNullException(nameof(continuation));

                if (FlowContext)
                    ThreadPool.QueueUserWorkItem(__WaitCallbackRunAction, continuation);
                else
                    ThreadPool.UnsafeQueueUserWorkItem(__WaitCallbackRunAction, continuation);
            }

            [SecurityCritical]
            public void OnCompleted([NotNull] Action continuation) => QueueContinuation(continuation, true);

            [SecurityCritical]
            public void UnsafeOnCompleted([NotNull] Action continuation) => QueueContinuation(continuation, false);

            public void GetResult() { }

        }
    }
}

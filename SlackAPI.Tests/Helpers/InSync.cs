using System;
using System.Threading;
using System.Runtime.CompilerServices;
using Xunit;

namespace SlackAPI.Tests.Helpers
{
    public class InSync : IDisposable
    {
        private readonly TimeSpan WaitTimeout = TimeSpan.FromSeconds(5);

        private readonly ManualResetEventSlim waiter;
        private readonly string message;

        public InSync([CallerMemberName] string message = null)
        {
            this.message = message;
            this.waiter = new ManualResetEventSlim();
        }

        public void Proceed()
        {
            this.waiter.Set();
        }

        public void Dispose()
        {
            Assert.True(this.waiter.Wait(this.WaitTimeout), $"Took too long to do '{this.message}'");
        }
    }
}
using System;

namespace NukedBit.Core
{
    public interface ISingleTapManager
    {
        void Tap();
    }

    public interface ISingleTapManager<T> : ISingleTapManager
    {
        void Tap(T arg);
    }

    public static class SingleTapManager
    {
        private class SingleTapManagerImpl<T> : ISingleTapManager<T>
        {
            private readonly Action<T> action;
            private readonly TimeSpan window;

            public SingleTapManagerImpl(Action<T> action, TimeSpan? window)
            {
                this.action = action;
                this.window = window ?? TimeSpan.FromSeconds(1);
            }

            private DateTime? lastAccepted;

            public void Tap(T arg)
            {
                if (lastAccepted is null)
                {
                    this.action.Invoke(arg);
                }
                else if ((DateTime.Now - lastAccepted.Value) >= window)
                {
                    this.action.Invoke(arg);
                    lastAccepted = DateTime.Now;
                }
            }

            public void Tap()
            {
                this.Tap(default(T));
            }
        }


        public static ISingleTapManager<T> Create<T>(Action<T> action, TimeSpan? window = null)
        {
            return new SingleTapManagerImpl<T>(action, window);
        }

        public static ISingleTapManager Create(Action action, TimeSpan? window = null)
        {
            return new SingleTapManagerImpl<object>((_) => {
                action();
            }, window);
        }
    }
}

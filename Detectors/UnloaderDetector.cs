using System;
using BuzzWin;

namespace Detectors
{
    public enum UnloaderPresenceEnum
    {
        Unknown,
        UnloaderNotEmpty,
        UnloaderEmpty
    }

    public abstract class UnloaderDetector : IDisposable
    {
        public event EventHandler UnloadBufferNotEmpty;
        public event EventHandler UnloadBufferEmpty;

        public abstract UnloaderPresenceEnum UnloaderPresence { get; }

        protected HIDIODevice _ioCard;


        public UnloaderDetector(HIDIODevice ioCard)
        {
            _ioCard = ioCard;
        }

        /// <summary>
        /// Raise the default LoadBufferNotEmpty event (with no arguments).
        /// </summary>
        protected void OnUnloadBufferNotEmpty()
        {
            UnloadBufferNotEmpty?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raise the default LoadBufferEmpty event (with no arguments).
        /// </summary>
        protected void OnUnloadBufferEmpty()
        {
            UnloadBufferEmpty?.Invoke(this, EventArgs.Empty);
        }

        public abstract void Dispose();
    }
}

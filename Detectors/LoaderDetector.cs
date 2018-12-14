using System;
using BuzzWin;

namespace Detectors
{
    public enum LoaderPresenceEnum
    {
        Unknown,
        LoaderNotEmpty,
        LoaderEmpty
    }

    public abstract class LoaderDetector: IDisposable
    {
        public event EventHandler LoadBufferNotEmpty;
        public event EventHandler LoadBufferEmpty;

        public abstract LoaderPresenceEnum LoaderPresence { get; }

        protected HIDIODevice _ioCard;


        public LoaderDetector(HIDIODevice ioCard)
        {
            _ioCard = ioCard;
        }

        /// <summary>
        /// Raise the default LoadBufferNotEmpty event (with no arguments).
        /// </summary>
        protected void OnLoadBufferNotEmpty()
        {
            LoadBufferNotEmpty?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raise the default LoadBufferEmpty event (with no arguments).
        /// </summary>
        protected void OnLoadBufferEmpty()
        {
            LoadBufferEmpty?.Invoke(this, EventArgs.Empty);
        }

        public abstract void Dispose();
    }
}

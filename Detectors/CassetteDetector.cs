using System;

using BuzzWin;

namespace Detectors
{
    public enum CassettePresenceEnum
    {
       Unknown,
       CassettePresent,
       CassetteAbsent
    }

    public abstract class CassetteDetector: IDisposable
    {
        public event EventHandler CassettePresent;
        public event EventHandler CassetteAbsent;

        public abstract CassettePresenceEnum CassettePresence { get; }

        protected HIDIODevice _ioCard;

        public CassetteDetector(HIDIODevice ioCard)
        {
            _ioCard = ioCard;
        }

        /// <summary>
        /// Raise the defaultCassettePresent event (with no arguments).
        /// </summary>
        protected void OnCassettePresent()
        {
            CassettePresent?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raise the defaultCassetteUnloaded event (with no arguments).
        /// </summary>
        protected void OnCassetteAbsent()
        {
            CassetteAbsent?.Invoke(this, EventArgs.Empty);
        }

        public abstract void Dispose();
    }
}

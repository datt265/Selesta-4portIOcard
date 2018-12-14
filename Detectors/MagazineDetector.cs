using System;
using BuzzWin;

namespace Detectors
{
    public enum MagazinePresenceEnum
    {
        Unknown,
        MagazinePresent,
        MagazineNotPresent
    }

    public abstract class MagazineDetector : IDisposable
    {
        public event EventHandler MagazineUnLoaded;

        public abstract MagazinePresenceEnum MagazinePresence { get; }

        protected HIDIODevice _ioCard;


        public MagazineDetector(HIDIODevice ioCard)
        {
            _ioCard = ioCard;
        }

        /// <summary>
        /// Raise the default MagazineUnloaded event (with no arguments).
        /// </summary>
        protected void OnMagazineUnloaded()
        {
            MagazineUnLoaded?.Invoke(this, EventArgs.Empty);
        }

        public abstract void Dispose();
    }
}

using System;
using BuzzWin;

namespace Detectors
{
    public class CassetteSensorDetector : CassetteDetector
    {
        private CassettePresenceEnum _cassettePresent = CassettePresenceEnum.Unknown;

        InputChangedEventHandler _handler;

        public override CassettePresenceEnum CassettePresence
        {
            get { return _cassettePresent; }
        }

        public CassetteSensorDetector(HIDIODevice ioCard) : base(ioCard)
        {
            _handler = new InputChangedEventHandler(_ioCard_OnReceiveData);
            _ioCard.OnReceiveData += _handler;
        }

        void _ioCard_OnReceiveData(object sender, InputChangedEventArgs args)
        {
            if ((args.devicest.swstatus & 3) != 2) { return; }

            if (_cassettePresent == CassettePresenceEnum.Unknown)
            {
                _cassettePresent = args.devicest.input_3 ? CassettePresenceEnum.CassettePresent : CassettePresenceEnum.CassetteAbsent;
            }
            else if (_cassettePresent == CassettePresenceEnum.CassettePresent && !args.devicest.input_3)
            {
                // Cassette Empty
                _cassettePresent = CassettePresenceEnum.CassetteAbsent;
                this.OnCassetteAbsent();
            }
            else if (_cassettePresent == CassettePresenceEnum.CassetteAbsent && args.devicest.input_3)
            {
                // Cassette Not Empty
                _cassettePresent = CassettePresenceEnum.CassettePresent;
                this.OnCassettePresent();
            }
        }

        public override void Dispose()
        {
            _ioCard.OnReceiveData -= _handler;
        }
    }
}

using BuzzWin;

namespace Detectors
{
    public class InverseMagazineSensorDetector : MagazineDetector
    {
        private MagazinePresenceEnum _magazinePresent = MagazinePresenceEnum.Unknown;
        InputChangedEventHandler _handler;

        public override MagazinePresenceEnum MagazinePresence
        {
            get { return _magazinePresent; }
        }

        public InverseMagazineSensorDetector(HIDIODevice ioCard) : base (ioCard)
        {
            _handler = new InputChangedEventHandler(_ioCard_OnReceiveData);
            _ioCard.OnReceiveData += _handler;
        }

        void _ioCard_OnReceiveData(object sender, InputChangedEventArgs args)
        {
            if ((args.devicest.swstatus & 3) != 2) { return; }

            if (_magazinePresent == MagazinePresenceEnum.Unknown)
            {
                _magazinePresent = args.devicest.input_2 ? MagazinePresenceEnum.MagazineNotPresent : MagazinePresenceEnum.MagazinePresent;
            }
            else if (_magazinePresent == MagazinePresenceEnum.MagazinePresent && args.devicest.input_2)
            {
                // Magazine Ejected
                _magazinePresent = MagazinePresenceEnum.MagazineNotPresent;
                this.OnMagazineUnloaded();
            }
            else if (_magazinePresent == MagazinePresenceEnum.MagazineNotPresent && !args.devicest.input_2)
            {
                // Magazine Loaded
                _magazinePresent = MagazinePresenceEnum.MagazinePresent;
            }
        }

        public override void Dispose()
        {
            _ioCard.OnReceiveData -= _handler;
        }
    }
}

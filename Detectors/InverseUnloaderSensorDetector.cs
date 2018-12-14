using BuzzWin;

namespace Detectors
{
    public class InverseUnloaderSensorDetector : UnloaderDetector
    {
        private UnloaderPresenceEnum _unloaderPresent = UnloaderPresenceEnum.Unknown;
        InputChangedEventHandler _handler;

        public override UnloaderPresenceEnum UnloaderPresence
        {
            get { return _unloaderPresent; }
        }

        public InverseUnloaderSensorDetector(HIDIODevice ioCard) : base(ioCard)
        {
            _handler = new InputChangedEventHandler(_ioCard_OnReceiveData);
            _ioCard.OnReceiveData += _handler;
        }

        void _ioCard_OnReceiveData(object sender, InputChangedEventArgs args)
        {
            if ((args.devicest.swstatus & 3) != 2) { return; }

            if (_unloaderPresent == UnloaderPresenceEnum.Unknown)
            {
                _unloaderPresent = args.devicest.input_1 ? UnloaderPresenceEnum.UnloaderEmpty : UnloaderPresenceEnum.UnloaderNotEmpty;
            }
            else if (_unloaderPresent == UnloaderPresenceEnum.UnloaderNotEmpty && args.devicest.input_1)
            {
                // UnLoad Buffer Empty
                _unloaderPresent = UnloaderPresenceEnum.UnloaderEmpty;
                this.OnUnloadBufferEmpty();
            }
            else if (_unloaderPresent == UnloaderPresenceEnum.UnloaderEmpty && !args.devicest.input_1)
            {
                // UnLoad buffer Not Empty
                _unloaderPresent = UnloaderPresenceEnum.UnloaderNotEmpty;
                this.OnUnloadBufferNotEmpty();
            }
        }

        public override void Dispose()
        {
            _ioCard.OnReceiveData -= _handler;
        }
    }
}

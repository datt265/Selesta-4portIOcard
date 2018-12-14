using BuzzWin;

namespace Detectors
{
    public class UnloaderSensorDetector : UnloaderDetector
    {
        private UnloaderPresenceEnum _unloaderPresent = UnloaderPresenceEnum.Unknown;
        InputChangedEventHandler _handler;

        public override UnloaderPresenceEnum UnloaderPresence
        {
            get { return _unloaderPresent; }
        }

        public UnloaderSensorDetector(HIDIODevice ioCard) : base(ioCard)
        {
            _handler = new InputChangedEventHandler(_ioCard_OnReceiveData);
            _ioCard.OnReceiveData += _handler;
        }

        void _ioCard_OnReceiveData(object sender, InputChangedEventArgs args)
        {
            if ((args.devicest.swstatus & 3) != 2) { return; }

            if (_unloaderPresent == UnloaderPresenceEnum.Unknown)
            {
                _unloaderPresent = args.devicest.input_1 ? UnloaderPresenceEnum.UnloaderNotEmpty : UnloaderPresenceEnum.UnloaderEmpty;
            }
            else if (_unloaderPresent == UnloaderPresenceEnum.UnloaderNotEmpty && !args.devicest.input_1)
            {
                // Unloader Unloaded
                _unloaderPresent = UnloaderPresenceEnum.UnloaderEmpty;
                this.OnUnloadBufferEmpty();
            }
            else if (_unloaderPresent == UnloaderPresenceEnum.UnloaderEmpty && args.devicest.input_1)
            {
                // Unloader Loaded
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

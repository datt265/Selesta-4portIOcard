using BuzzWin;

namespace Detectors
{
    public class InverseLoaderSensorDetector : LoaderDetector
    {
        private LoaderPresenceEnum _loaderPresent = LoaderPresenceEnum.Unknown;
        InputChangedEventHandler _handler;

        public override LoaderPresenceEnum LoaderPresence
        {
            get { return _loaderPresent; }
        }

        public InverseLoaderSensorDetector(HIDIODevice ioCard) : base(ioCard)
        {
            _handler = new InputChangedEventHandler(_ioCard_OnReceiveData);
            _ioCard.OnReceiveData += _handler;
        }

        void _ioCard_OnReceiveData(object sender, InputChangedEventArgs args)
        {
            if ((args.devicest.swstatus & 3) != 2) { return; }

            if (_loaderPresent == LoaderPresenceEnum.Unknown)
            {
                _loaderPresent = args.devicest.input_0 ? LoaderPresenceEnum.LoaderEmpty : LoaderPresenceEnum.LoaderNotEmpty;
            }
            else if (_loaderPresent == LoaderPresenceEnum.LoaderNotEmpty && args.devicest.input_0)
            {
                // Load Buffer Empty
                _loaderPresent = LoaderPresenceEnum.LoaderEmpty;
                this.OnLoadBufferEmpty();
            }
            else if (_loaderPresent == LoaderPresenceEnum.LoaderEmpty && !args.devicest.input_0)
            {
                // Load Buffer Not Empty
                _loaderPresent = LoaderPresenceEnum.LoaderNotEmpty;
                this.OnLoadBufferNotEmpty();
            }
        }

        public override void Dispose()
        {
            _ioCard.OnReceiveData -= _handler;
        }
    }
}

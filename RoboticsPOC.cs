using System;
using System.Drawing;
using System.Windows.Forms;
using BuzzWin;
using Detectors;

namespace RoboticsPOC
{
    public enum IOCardConnectionEnum
    {
        NoIoBoardFound,
        Connected
    }

    public partial class RoboticsPOC : UsbAwareForm
    {
        private IOCardConnectionEnum _boardConnectionState;

        private MagazinePresenceEnum _magazinePresence;
        private LoaderPresenceEnum _loaderPresence;
        private UnloaderPresenceEnum _unloaderPresence;
        private CassettePresenceEnum _cassettePresence;

        private HIDIODevice _ioCard = null;

        private delegate void AddLogEntryDelegate(string entry);

        #region Public Events

        public event EventHandler IOCardDetected;

        public event EventHandler MagazineDetectionMethodChanged
        {
            add { magazineDetectionMethod.SelectedValueChanged += value; }
            remove { magazineDetectionMethod.SelectedValueChanged -= value; }
        }

        public event EventHandler LoaderDetectionMethodChanged
        {
            add { loaderDetectionMethod.CheckedChanged += value; }
            remove { loaderDetectionMethod.CheckedChanged -= value; }
        }

        public event EventHandler UnloaderDetectionMethodChanged
        {
            add { unloaderDetectionMethod.CheckedChanged += value; }
            remove { unloaderDetectionMethod.CheckedChanged -= value; }
        }

        public event EventHandler CassetteDetectionMethodChanged
        {
            add { cassetteDetectionMethod.CheckedChanged += value; }
            remove {cassetteDetectionMethod.CheckedChanged -= value; }
        }



        #endregion

        #region Public Properties

        public IOCardConnectionEnum BoardConnectionState
        {
            get { return _boardConnectionState; }
            set
            {
                // If necessary switch to UI thread before changing anything.
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<IOCardConnectionEnum>((v) => this.BoardConnectionState = v), new object[] { value });
                    return;
                }

                _boardConnectionState = value;

                switch (_boardConnectionState)
                {
                    case IOCardConnectionEnum.Connected:
                        boardConnectionState.Text = "Connected";
                        break;

                    case IOCardConnectionEnum.NoIoBoardFound:
                        boardConnectionState.Text = "No Board";
                        break;

                    default:
                        boardConnectionState.Text = "ERROR";
                        break;
                }
            }
        }

        /*
         * Sensor related properties
         */

        public LoaderPresenceEnum LoaderPresence
        {
            get { return _loaderPresence; }
            set
            {
                // If necessary switch to UI thread before changing anything.
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<LoaderPresenceEnum>((v) => this.LoaderPresence = v), new object[] { value });
                    return;
                }

                _loaderPresence = value;
                this.updateLoaderRelated();
            }
        }

        public UnloaderPresenceEnum UnloaderPresence
        {
            get { return _unloaderPresence; }
            set
            {
                // If necessary switch to UI thread before changing anything.
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<UnloaderPresenceEnum>((v) => this.UnloaderPresence = v), new object[] { value });
                    return;
                }

                _unloaderPresence = value;
                this.updateUnloaderRelated();
            }
        }

        public CassettePresenceEnum CassettePresence
        {
            get { return _cassettePresence; }
            set
            {
                // If necessary switch to UI thread before changing anything.
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<CassettePresenceEnum>((v) => this.CassettePresence = v), new object[] { value });
                    return;
                }

                _cassettePresence = value;
                this.updateCassetteRelated();
            }
        }

        public MagazinePresenceEnum MagazinePresence
        {
            get { return _magazinePresence; }
            set
            {
                // If necessary switch to UI thread before changing anything.
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<MagazinePresenceEnum>((v) => this.MagazinePresence = v), new object[] { value });
                    return;
                }

                _magazinePresence = value;
            }
        }


        /*
         * Read-only properties for GUI elements
         */
        public string MagazineDetectionMethod
        {
            get
            {
                // If necessary switch to UI thread before changing anything.
                if (this.InvokeRequired)
                {
                    return (string)this.Invoke(new Func<string>(() => this.MagazineDetectionMethod));
                }

                return (string)magazineDetectionMethod.SelectedItem;
            }
        }

        public string LoaderDetectionMethod
        {
            get
            {
                // If necessary switch to UI thread before changing anything.
                if (this.InvokeRequired)
                {
                    return (string)this.Invoke(new Func<string>(() => this.LoaderDetectionMethod));
                }

                if (loaderDetectionMethod.Checked)
                    return "Load Buffer (Inverted)";
                else
                    return "Load Buffer";
            }
        }
        public string UnloaderDetectionMethod
        {
            get
            {
                // If necessary switch to UI thread before changing anything.
                if (this.InvokeRequired)
                {
                    return (string)this.Invoke(new Func<string>(() => this.UnloaderDetectionMethod));
                }

                if (unloaderDetectionMethod.Checked)
                    return "Unload Buffer (Inverted)";
                else
                    return "Unload Buffer";
            }
        }

        public string CassetteDetectionMethod
        {
            get
            {
                // If necessary switch to UI thread before changing anything.
                if (this.InvokeRequired)
                {
                    return (string)this.Invoke(new Func<string>(() => this.CassetteDetectionMethod));
                }

                if (cassetteDetectionMethod.Checked)
                    return "Cassette (Inverted)";
                else
                    return "Cassette";
            }
        }

        /*
         * The thing that should not be
         */
        public HIDIODevice IOCard
        {
            get { return _ioCard; }
        }

        #endregion

        #region Public Methods

        public void AddLogEntry(string entry)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AddLogEntryDelegate(this.AddLogEntry), new string[1] { entry });
                return;
            }

            string ts = DateTime.Now.ToString("H:m:s: ");
            messageLog.Items.Add(ts + entry);
            messageLog.TopIndex = messageLog.Items.Count - 1;
        }

        #endregion

        public RoboticsPOC()
        {
            InitializeComponent();

            // Append full assembly version to the form title.
            this.Text += " - " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            // MagazineDetector
            magazineDetectionMethod.Items.AddRange(Detectors.Factory.GetMagazineList());

            // Select default detectors
            this.magazineDetectionMethod.SelectedIndex = 0;
            this.loaderDetectionMethod.Checked = false;
            this.unloaderDetectionMethod.Checked = false;
            this.cassetteDetectionMethod.Checked = false;

            // Initialize Property controlled UI elements to default values
            this.LoaderPresence = LoaderPresenceEnum.Unknown;
            this.UnloaderPresence = UnloaderPresenceEnum.Unknown;
            this.CassettePresence = CassettePresenceEnum.Unknown;

            // Initialize Property controlled UI elements to default values
            this.MagazinePresence = MagazinePresenceEnum.Unknown;

            this.BoardConnectionState = IOCardConnectionEnum.NoIoBoardFound;

            // Trigger timer that will handle the rest of the initialization
            this.AddLogEntry("Starting hardware search");
            hardwareInitTimer.Enabled = true;
        }

        #region Private Methods

        private void updateLoaderRelated()
        {
            switch (_loaderPresence)
            {
                case LoaderPresenceEnum.Unknown:
                    // Special case state used at initialization only
                    loaderPresense.Text = "Unknown";
                    loaderPresense.BackColor = Color.White;
                    break;

                case LoaderPresenceEnum.LoaderNotEmpty:
                    loaderPresense.Text = "Not Empty";
                    loaderPresense.BackColor = Color.LightGreen;
                    break;

                case LoaderPresenceEnum.LoaderEmpty:
                    loaderPresense.Text = "Empty";
                    loaderPresense.BackColor = Color.LightPink;
                    break;

                default:
                    loaderPresense.Text = "ERROR";
                    loaderPresense.BackColor = Color.Red;
                    break;
            }
        }

        private void updateUnloaderRelated()
        {
            switch (_unloaderPresence)
            {
                case UnloaderPresenceEnum.Unknown:
                    // Special case state used at initialization only
                    unloaderPresense.Text = "Unknown";
                    unloaderPresense.BackColor = Color.White;
                    break;

                case UnloaderPresenceEnum.UnloaderNotEmpty:
                    unloaderPresense.Text = "Not Empty";
                    unloaderPresense.BackColor = Color.LightGreen;
                    break;

                case UnloaderPresenceEnum.UnloaderEmpty:
                    unloaderPresense.Text = "Empty";
                    unloaderPresense.BackColor = Color.LightPink;
                    break;

                default:
                    unloaderPresense.Text = "ERROR";
                    unloaderPresense.BackColor = Color.Red;
                    break;
            }
        }

        private void updateCassetteRelated()
        {
            switch (_cassettePresence)
            {
                case CassettePresenceEnum.Unknown:
                    // Special case state used at initialization only
                    cassettePresense.Text = "Unknown";
                    cassettePresense.BackColor = Color.White;
                    break;

                case CassettePresenceEnum.CassettePresent:
                    cassettePresense.Text = "Present";
                    cassettePresense.BackColor = Color.LightGreen;
                    break;

                case CassettePresenceEnum.CassetteAbsent:
                    cassettePresense.Text = "Absent";
                    cassettePresense.BackColor = Color.LightPink;
                    break;

                default:
                    cassettePresense.Text = "ERROR";
                    cassettePresense.BackColor = Color.Red;
                    break;
            }
        }

        #endregion

        #region Other Event Handlers

        private void hardwareInitTimer_Tick(object sender, EventArgs args)
        {
            // Try setting up the IO Card if it's connected.
            _ioCard = HIDIODevice.FindDevice();

            if (_ioCard != null)
            {
                ((Timer)sender).Enabled = false;

                // Link up events from the card to our handlers.
                _ioCard.OnDeviceRemoved +=new EventHandler(_ioCard_OnDeviceRemoved);

                if (IOCardDetected != null)
                    IOCardDetected(this, EventArgs.Empty);

            }
        }

        private void _ioCard_OnDeviceRemoved(object sender, EventArgs args)
        {
            // Restart looking for an IO Card.  Timer needs to be enabled through an Invoke'd
            // call as it's a UI element.
            this.Invoke((MethodInvoker)delegate()
            {
                hardwareInitTimer.Enabled = true;
            }, new object[] { });
        }

        #endregion
    }
}

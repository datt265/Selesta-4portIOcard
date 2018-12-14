using System;
using System.IO;
using System.Windows.Forms;
using BuzzWin;
using Detectors;

namespace RoboticsPOC
{
    static class Program
    {
        private static RoboticsPOC _mainUI = null;
        private static HIDIODevice _ioCard = null;
        private static MagazineDetector _magazineDetector = null;
        private static LoaderDetector _loaderDetector = null;
        private static UnloaderDetector _unloaderDetector = null;
        private static CassetteDetector _cassetteDetector = null;

        private static string _appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace("file:\\", "");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _mainUI = new RoboticsPOC();

            // Link Up event handlers
            _mainUI.MagazineDetectionMethodChanged += new EventHandler(_mainUI_MagazineDetectionMethodChanged);
            _mainUI.LoaderDetectionMethodChanged += new EventHandler(_mainUI_LoaderDetectionMethodChanged);
            _mainUI.UnloaderDetectionMethodChanged += new EventHandler(_mainUI_UnloaderDetectionMethodChanged);
            _mainUI.CassetteDetectionMethodChanged += new EventHandler(_mainUI_CassetteDetectionMethodChanged);

            // The main UI also hosts IOCard detection logic as the IOCard driver
            // needs to be linked to a form.  Can probably move the logic to a dummy
            // hidden form as well.
            _mainUI.IOCardDetected += new EventHandler(_mainUI_IOCardDetected);

            // Add a 'startup banner' to log file.
            logToFile(@"======================================================================");
            logToFile(@"= RoboticsPOC Starting up");
            logToFile(@"======================================================================");

            Application.Run(_mainUI);

        }

        #region IO Card Event Handling

        static void _ioCard_OnDeviceRemoved(object sender, EventArgs args)
        {
            logMessage("IOCard: Card Disconnected");

            // Update UI fields
            _mainUI.BoardConnectionState = IOCardConnectionEnum.NoIoBoardFound;
            _mainUI.MagazinePresence = MagazinePresenceEnum.Unknown;
            _mainUI.LoaderPresence = LoaderPresenceEnum.Unknown;
            _mainUI.UnloaderPresence = UnloaderPresenceEnum.Unknown;
            _mainUI.CassettePresence = CassettePresenceEnum.Unknown;

            // Throw away existing IO Card and Detector object as we'll initialize new ones
            // when we next detect the IO Card as being connected.
            _magazineDetector = null;
            _loaderDetector = null;
            _unloaderDetector = null;
            _ioCard = null;
        }

        #endregion

        #region IOCard Detection event

        static void _mainUI_IOCardDetected(object sender, EventArgs e)
        {
            _ioCard = _mainUI.IOCard;            
            logMessage("IOCard: Found device " + _ioCard.device_enum );

            _mainUI.BoardConnectionState = IOCardConnectionEnum.Connected;

            // Link up events from the card to our handlers.
            _ioCard.OnDeviceRemoved +=new EventHandler(_ioCard_OnDeviceRemoved);

            initializeDetectors();
        }

        #endregion

        #region UI Event handling

        static void _mainUI_MagazineDetectionMethodChanged(object sender, EventArgs e)
        {
            logMessage("MagazineDetector: Changed detection logic to " + _mainUI.MagazineDetectionMethod);

            initializeDetectors();
        }

        static void _mainUI_LoaderDetectionMethodChanged(object sender, EventArgs e)
        {
            logMessage("LoaderDetector: Changed detection logic to " + _mainUI.LoaderDetectionMethod);

            initializeDetectors();
        }

        static void _mainUI_UnloaderDetectionMethodChanged(object sender, EventArgs e)
        {
            logMessage("UnloaderDetector: Changed detection logic to " + _mainUI.UnloaderDetectionMethod);

            initializeDetectors();
        }

        static void _mainUI_CassetteDetectionMethodChanged(object sender, EventArgs e)
        {
            logMessage("CassetteDetector: Changed detection logic to " + _mainUI.CassetteDetectionMethod);

            initializeDetectors();
        }

        #endregion

        #region Sensor Detector Event Handling

        static void _magazineDetector_MagazineUnLoaded(object sender, EventArgs e)
        {
            logMessage("MagazineDetector: Magazine Unloaded");
            _mainUI.MagazinePresence = MagazinePresenceEnum.MagazineNotPresent;
        }

        #endregion

        #region Detector Event Handling

        static void _loaderDetector_LoadBufferNotEmpty(object sender, EventArgs e)
        {
            logMessage("LoadBuffer: Not Empty");
            _mainUI.LoaderPresence = LoaderPresenceEnum.LoaderNotEmpty;
        }

        static void _loaderDetector_LoadBufferEmpty(object sender, EventArgs e)
        {
            logMessage("LoadBuffer: Empty");
            _mainUI.LoaderPresence = LoaderPresenceEnum.LoaderEmpty;
        }

        static void _unloaderDetector_UnloadBufferNotEmpty(object sender, EventArgs e)
        {
            logMessage("UnloadBuffer: Not Empty");
            _mainUI.UnloaderPresence = UnloaderPresenceEnum.UnloaderNotEmpty;
        }

        static void _unloaderDetector_UnloadBufferEmpty(object sender, EventArgs e)
        {
            logMessage("UnloadBuffer: Empty");
            _mainUI.UnloaderPresence = UnloaderPresenceEnum.UnloaderEmpty;
        }

        static void _cassetteDetector_CassettePresent(object sender, EventArgs e)
        {
            logMessage("Cassette: Present");
            _mainUI.CassettePresence =CassettePresenceEnum.CassettePresent;
        }

        static void _cassetteDetector_CassetteAbsent(object sender, EventArgs e)
        {
            logMessage("Cassette: Absent");
            _mainUI.CassettePresence = CassettePresenceEnum.CassetteAbsent;
        }

        #endregion

        #region Initialize object function

        static void initializeDetectors()
        {
            if (_ioCard != null)
            {
                logMessage("Initializing instance");

                _magazineDetector?.Dispose(); //dispose if null
                _magazineDetector = Detectors.Factory.GetInstanceByFriendlyName(
                    _mainUI.MagazineDetectionMethod,
                    _ioCard
                );
                _magazineDetector.MagazineUnLoaded += new EventHandler(_magazineDetector_MagazineUnLoaded);

                _loaderDetector?.Dispose(); //dispose if null
                _loaderDetector = Factory.GetLoaderInstanceByFriendlyName(
                    _mainUI.LoaderDetectionMethod,
                    _ioCard
                );
                _loaderDetector.LoadBufferNotEmpty += new EventHandler(_loaderDetector_LoadBufferNotEmpty);
                _loaderDetector.LoadBufferEmpty += new EventHandler(_loaderDetector_LoadBufferEmpty);

                _unloaderDetector?.Dispose(); //dispose if null
                _unloaderDetector = Factory.GetUnloaderInstanceByFriendlyName(
                    _mainUI.UnloaderDetectionMethod,
                    _ioCard
                );
                _unloaderDetector.UnloadBufferNotEmpty += new EventHandler(_unloaderDetector_UnloadBufferNotEmpty);
                _unloaderDetector.UnloadBufferEmpty += new EventHandler(_unloaderDetector_UnloadBufferEmpty);


                _cassetteDetector?.Dispose(); //dispose if null
                _cassetteDetector = Factory.GetCassetteInstanceByFriendlyName(
                    _mainUI.CassetteDetectionMethod,
                    _ioCard
                );
                _cassetteDetector.CassettePresent += new EventHandler(_cassetteDetector_CassettePresent);
                _cassetteDetector.CassetteAbsent += new EventHandler(_cassetteDetector_CassetteAbsent);

                // Refresh sensor information once after a 500ms delay
                // to allow the IOCard polling loop to start.
                System.Timers.Timer refresh = new System.Timers.Timer(500);
                refresh.Elapsed += delegate(object sender, System.Timers.ElapsedEventArgs e)
                {
                    _mainUI.MagazinePresence = _magazineDetector.MagazinePresence;
                    if (_magazineDetector.MagazinePresence == MagazinePresenceEnum.MagazinePresent)
                    {
                        logMessage("MagazineDetector: Found Magazine already loaded at IOCard connect");
                    }
                    _mainUI.LoaderPresence = _loaderDetector.LoaderPresence;
                    if (_loaderDetector.LoaderPresence == LoaderPresenceEnum.LoaderNotEmpty)
                    {
                        logMessage("LoadBuffer: Found Load Buffer Not EMPTY at IOCard connect");
                    }
                    _mainUI.UnloaderPresence = _unloaderDetector.UnloaderPresence;
                    if (_unloaderDetector.UnloaderPresence == UnloaderPresenceEnum.UnloaderNotEmpty)
                    {
                        logMessage("UnLoadBuffer: Found Unload Buffer Not EMPTY at IOCard connect");
                    }
                    _mainUI.CassettePresence = _cassetteDetector.CassettePresence;
                    if (_cassetteDetector.CassettePresence == CassettePresenceEnum.CassettePresent)
                    {
                        logMessage("Cassette: Found Cassette Present at IOCard connect");
                    }
                };
                refresh.AutoReset = false;
                refresh.Start();
                _ioCard.Refresh(); //refresh card status to read inputs
            }
            else
            {
                logMessage("SensorDetector: Skipping Initialization because there is no IOCard connected");
            }
        }

        #endregion

        #region Logging Support

        /// <summary>
        /// Log the supplied message to a daily log file.  Log file should be placed in
        /// the same location as executable and named RoboticsPOC-YYYYMMDD.log.
        /// </summary>
        /// <param name="message"></param>
        private static void logToFile(string message)
        {
            DateTime ts = DateTime.Now;
            string logFileName = "RoboticsPOC-" + ts.ToString("yyyyMMdd") + ".log";
            logFileName = System.IO.Path.Combine(_appPath, logFileName);

            FileStream fs = new FileStream(logFileName, FileMode.Append, FileAccess.Write, FileShare.Read);
            StreamWriter sr = new StreamWriter(fs);

            sr.WriteLine(ts.ToString("H:m:s: ") + message);
            sr.Close();
            fs.Close();
        }

        /// <summary>
        /// Log the supplied message to UI and to a daily log file.
        /// </summary>
        /// <param name="message"></param>
        private static void logMessage(string message)
        {
            logToFile(message);
            _mainUI.AddLogEntry(message);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using BuzzWin;

namespace Detectors
{
    public static class Factory
    {
        /// <summary>
        /// Static manually defined list of detector methods.  Key is
        /// a user friendly name to be displayed in UI or related use.
        /// Value is actual class name to initialize.
        /// </summary>
        private static Dictionary<string, string> _detectorNameList = new Dictionary<string, string>()
        {
            {"Load Buffer", "Detectors.LoaderSensorDetector"},
            {"Load Buffer (Inverted)", "Detectors.InverseLoaderSensorDetector"},
            {"Unload Buffer", "Detectors.UnloaderSensorDetector"},
            {"Unload Buffer (Inverted)", "Detectors.InverseUnloaderSensorDetector"},
            {"Cassette", "Detectors.CassetteSensorDetector"},
            {"Cassette (Inverted)", "Detectors.InverseCassetteSensorDetector"}
        };

        private static Dictionary<string, string> _magazineNameList = new Dictionary<string, string>()
        {
            { "Normal", "Detectors.MagazineSensorDetector"},
            { "Inverted", "Detectors.InverseMagazineSensorDetector"},
        };

        public static string [] GetDetectorList()
        {
            return _detectorNameList.Keys.ToArray();
        }

        public static string [] GetMagazineList()
        {
            return _magazineNameList.Keys.ToArray();
        }

        public static MagazineDetector GetInstanceByFriendlyName(string detectorName, HIDIODevice ioCard)
        {
            string detectorClassName = string.Empty;
            if (_magazineNameList.TryGetValue(detectorName, out detectorClassName))
            {
                return GetInstanceByClassName(detectorClassName, ioCard);
            }

            throw new ArgumentException("Invalid detector name supplied");
        }

        public static MagazineDetector GetInstanceByClassName(string detectorName, HIDIODevice ioCard )
        {
            return (MagazineDetector)Activator.CreateInstance(Type.GetType(detectorName), new object[] {ioCard});
        }


        public static LoaderDetector GetLoaderInstanceByFriendlyName(string detectorName, HIDIODevice ioCard)
        {
            string detectorClassName = string.Empty;
            if (_detectorNameList.TryGetValue(detectorName, out detectorClassName))
            {
                return GetLoaderInstanceByClassName(detectorClassName, ioCard);
            }

            throw new ArgumentException("Invalid Load Buffer name supplied");
        }

        public static LoaderDetector GetLoaderInstanceByClassName(string detectorName, HIDIODevice ioCard)
        {
            return (LoaderDetector)Activator.CreateInstance(Type.GetType(detectorName), new object[] { ioCard });
        }

        public static UnloaderDetector GetUnloaderInstanceByFriendlyName(string detectorName, HIDIODevice ioCard)
        {
            string detectorClassName = string.Empty;
            if (_detectorNameList.TryGetValue(detectorName, out detectorClassName))
            {
                return GetUnloaderInstanceByClassName(detectorClassName, ioCard);
            }

            throw new ArgumentException("Invalid Unload Buffer name supplied");
        }

        public static UnloaderDetector GetUnloaderInstanceByClassName(string detectorName, HIDIODevice ioCard)
        {
            return (UnloaderDetector)Activator.CreateInstance(Type.GetType(detectorName), new object[] { ioCard });
        }

        public static CassetteDetector GetCassetteInstanceByFriendlyName(string detectorName, HIDIODevice ioCard)
        {
            string detectorClassName = string.Empty;
            if (_detectorNameList.TryGetValue(detectorName, out detectorClassName))
            {
                return GetCassetteInstanceByClassName(detectorClassName, ioCard);
            }

            throw new ArgumentException("Invalid Cassette name supplied");
        }

        public static CassetteDetector GetCassetteInstanceByClassName(string detectorName, HIDIODevice ioCard)
        {
            return (CassetteDetector)Activator.CreateInstance(Type.GetType(detectorName), new object[] { ioCard });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace TrainsTestTask
{
    class StationParser
    {
        public Station Parse(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            var station = LoadHardcodedExample();
            return station;
        }

        private Station LoadHardcodedExample()
        {
            var p01 = new Point() { X = 400, Y = 40 };
            var p02 = new Point() { X = 320, Y = 40 };
            var p03 = new Point() { X = 280, Y = 80 };
            var p04 = new Point() { X = 240, Y = 80 };
            var p05 = new Point() { X = 440, Y = 80 };

            var p06 = new Point() { X = 40, Y = 120 };
            var p07 = new Point() { X = 80, Y = 120 };
            var p08 = new Point() { X = 200, Y = 120 };
            var p09 = new Point() { X = 240, Y = 120 };
            var p10 = new Point() { X = 480, Y = 120 };
            var p11 = new Point() { X = 520, Y = 120 };
            var p12 = new Point() { X = 640, Y = 120 };
            var p13 = new Point() { X = 680, Y = 120 };

            var p14 = new Point() { X = 40, Y = 160 };
            var p15 = new Point() { X = 120, Y = 160 };
            var p16 = new Point() { X = 160, Y = 160 };
            var p17 = new Point() { X = 240, Y = 160 };
            var p18 = new Point() { X = 480, Y = 160 };
            var p19 = new Point() { X = 560, Y = 160 };
            var p20 = new Point() { X = 600, Y = 160 };
            var p21 = new Point() { X = 680, Y = 160 };

            var p22 = new Point() { X = 160, Y = 200 };
            var p23 = new Point() { X = 280, Y = 200 };
            var p24 = new Point() { X = 360, Y = 200 };

            var p25 = new Point() { X = 320, Y = 240 };
            var p26 = new Point() { X = 400, Y = 240 };

            p01.AddTwoWays(p02);
            p02.AddTwoWays(p03);
            p03.AddTwoWays(p04);
            p03.AddTwoWays(p05);
            p03.AddTwoWays(p09);
            p05.AddTwoWays(p10);
            p06.AddDestination(p07);
            p07.AddDestination(p08);
            p07.AddDestination(p15);
            p08.AddTwoWays(p09);
            p08.AddTwoWays(p16);
            p09.AddTwoWays(p10);
            p10.AddTwoWays(p11);
            p11.AddDestination(p12);
            p19.AddDestination(p11);
            p12.AddDestination(p13);
            p20.AddDestination(p12);
            p15.AddDestination(p14);
            p15.AddTwoWays(p16);
            p16.AddTwoWays(p17);
            p17.AddTwoWays(p18);
            p17.AddTwoWays(p23);
            p18.AddTwoWays(p26);
            p18.AddTwoWays(p19);
            p19.AddTwoWays(p20);
            p21.AddDestination(p20);
            p22.AddTwoWays(p23);
            p23.AddTwoWays(p24);
            p23.AddTwoWays(p25);
            p25.AddTwoWays(p26);

            var station = new Station();
            station.Points = new() { p01, p02, p03, p04, p05, p06,
                p07, p08, p09, p10, p11, p12, p13, p14, p15, p16,
                p17, p18, p19, p20, p21, p22, p23, p24, p25, p26
            };

            var track = new Track()
            {
                Points = new() // random line to test highlighting.
                {
                    p06, p07, p15, p16, p08, p09, p03, p05, p10, p11, p12, p13
                }
            };

            station.SavedTracks.Add(track);

            var park = new Park();
            park.AddTrack(track);
            station.Parks.Add(park);

            return station;
        }
    }
}

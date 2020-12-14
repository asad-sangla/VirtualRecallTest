using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualRecall.Domain.Entities;
using VirtualRecall.Domain.Enums;

namespace VirtualRecall.Application
{
    public class LibClass
    {
        /// <summary>
        /// Gets all points x2 + y2 less than equal to 1 = number of points inside circle
        /// Gives approximation for given points as Pi/4
        /// </summary>
        /// <param name="pts"></param>
        /// <returns></returns>
        public static double Approx(Point[] pts)
        {
            var numberOfPointsInCircle = (double)pts.Select(p => Math.Pow(p.X, 2) + Math.Pow(p.Y, 2)).Count(xyScore => xyScore <= 1);
            return (numberOfPointsInCircle / pts.Count()) * 4;
        }

        /// <summary>
        /// Give average of input numerics
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public static double Average(IEnumerable<IHasNumeric> set)
        {
            return set.Average(x => x.Num);
        }


        ///Given a number of square tiles. Place the tiles together into square shapes, making each square shape as large as possible, until all the tiles have been used.
        ///Return the number of tiles in each square.

        ///For example, 15 tiles = [9, 4, 1, 1]
        /// [ ][ ][ ]
        /// [ ][ ][ ]  [ ][ ]
        /// [ ][ ][ ]  [ ][ ]  [ ]  [ ]        
        public static IEnumerable<int> GetPanelArrays(int numPanels)
        {
            var result = new List<int>();
            var numPanelsCounter = numPanels;

            do
            {
                var floor = (int)Math.Floor(Math.Sqrt(numPanelsCounter));
                var panelSquare = (int)Math.Pow(floor, 2);
                result.Add(panelSquare);

                numPanelsCounter -= panelSquare;

            } while (numPanelsCounter > 0);

            return result;
        }


        private int _counter;


        /// <summary>
        /// After executing this function, the value of _counter should be increased by n * (n + 1) / 2, however this is not always the case.
        /// Propose a change to the RunStep function to resolve the issue, whilst maintaining the asynchronous, concurrent execution behaviour.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public async Task<int> ParallelIncrement(int n)
        {
            var tasks = Enumerable.Range(1, n).Select(RunStep);
            await Task.WhenAll(tasks);

            return _counter;
        }

        private Task RunStep(int stepValue)
        {
            _counter += stepValue;

            return Task.Run(async () =>
            {
                await Task.Delay(2);
            });
        }



        /// <summary>
        /// Return false if none of the requested capabilities have been granted, otherwise true.
        /// </summary>
        /// <param name="granted">Set of flags indicated which capabilities have been granted.</param>
        /// <param name="requested">Which capabilities are being requested.</param>
        /// <returns> Whether ANY of the required capabilities have been granted. Also return true if none is requested.
        /// </returns>
        public static bool AnyGranted(Capabilities granted, Capabilities requested)
        {
            if (requested == Capabilities.None)
                return true;

            var capabilities = Enum.GetValues(typeof(Capabilities));
            return capabilities.Cast<Capabilities>().Where(capability => capability != Capabilities.None)
                .Any(capability => requested.HasFlag(capability) && granted.HasFlag(capability));
        }


    }
}
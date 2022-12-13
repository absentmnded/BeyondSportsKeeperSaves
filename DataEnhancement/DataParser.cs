using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEnhancement
{
    internal class DataParser
    {
        public static Frame ReadOneFrame(string rawData)
        {           
            var parts = rawData.Split(':', StringSplitOptions.RemoveEmptyEntries);
            var trackedObjects = parts[1].Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();
            var listTrackedObjects = trackedObjects.Select(t => (ITrackedObject)(new TrackedObject(t))).ToList();
            var ballData = new BallData(parts.Last().TrimEnd(';'));
            return new Frame(Int32.Parse(parts[0]), listTrackedObjects, ballData);
        }
    }
}

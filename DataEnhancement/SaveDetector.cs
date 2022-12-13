using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEnhancement
{
    public static class SaveDetector
    {
        public static (List<Frame>, Frame)? FilterOutNoSaves(List<Frame> frames)
        {
            var minBallKeeperDistance = frames.Min(f => f.BallData.Point.Distance(f.TrackedObjects.Where(t => t.PlayerNumber == 22).FirstOrDefault().Point));
            var minFrame = frames.Where(f => f.BallData.Point.Distance(f.TrackedObjects.Where(t => t.PlayerNumber == 22).FirstOrDefault().Point) == minBallKeeperDistance).FirstOrDefault();
            if (minBallKeeperDistance > 50)
            {
                //Console.WriteLine("no save");
                return null;
            }
            else return (frames, minFrame);

        }

        public static string SaveDirection(List<Frame> frames, Frame save)
        {
            var index = frames.IndexOf(save);
            Frame? beforeSave = null;

            for (int i = 15; i > 0; i--)
            {
                if (frames.ElementAtOrDefault(index - i) != null)
                {
                    beforeSave = frames.ElementAt(index - i);
                    break;
                }
            }
            if (beforeSave == null)
                return "Direction is impossible to determine.";

            var ballBefore = save.BallData.Point;
            var keeperBefore = beforeSave.TrackedObjects.Where((t) => t.PlayerNumber == 22).FirstOrDefault().Point;
            var keeperAtSave = save.TrackedObjects.Where(t => t.PlayerNumber == 22).FirstOrDefault().Point;
            //line connecting keeper points
            var kL = new Line(keeperAtSave, keeperBefore);
            // line connecting the ball and the keeper before save
            var kBL = new Line(keeperBefore, ballBefore);
            //Angle between the lines
            var angle = Math.Atan((kBL.A * kL.B - kL.A * kBL.B)/(kL.A*kBL.A + kL.B*kBL.B));
            string side;
            var keeperToRight = keeperBefore.X > ballBefore.X;

            var pointAbove = kBL.PlacePoint(keeperAtSave) > 0;
            if (pointAbove)
                if (keeperToRight)
                    side = "Right";
                else
                    side = "Left";
            else
                if (keeperToRight)
                side = "Left";
            else
                side = "Right";

            if (Math.Abs(angle) <= Math.PI / 12)
                side = "  center";

            return side;
        }

    }
}

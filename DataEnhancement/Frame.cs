using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEnhancement
{
    public enum Team
    {
        Away = 0,
        Home = 1,
        Ball = 2,
        Referee = 3, 
        None = 4
    }
    public enum BallClickerFlag
    {
        SetAway = 0,
        SetHome = 1,
        Whistle = 2,
        None = 3,
        B4 = -1
    }
    public enum Posession
    {
        A = 0, 
        H = 1
    }
    public enum BallState
    {
        Dead = 0,
        Alive = 1
    }
    public interface IDataBase
    {
        public Point Point { get; set; }
        public float Speed { get; set; }

    }
    public interface ITrackedObject : IDataBase
    {
        public Team Team { get; set; }
        public int Id { get; set; }
        public int PlayerNumber { get; set; }
    }
    public interface IBallData : IDataBase
    {
        public Posession Posession { get; set; }
        public BallState BallState { get; set; }
        public BallClickerFlag BallClickerFlag { get; set; }

    }

    internal class TrackedObject : ITrackedObject
    {
        public TrackedObject(string rawInput)
        {
            var components = rawInput.Split(',', StringSplitOptions.RemoveEmptyEntries);
            Team = (Team)Enum.Parse(typeof(Team),components[0]);
            Id = Int32.Parse(components[1]);
            PlayerNumber = Int32.Parse(components[2]);
            Point = new Point(float.Parse(components[3]), float.Parse(components[4]));
            Speed = float.Parse(components[5]);
        }
        public Team Team { get; set; }
        public int Id { get; set; }
        public int PlayerNumber { get; set; }
        public Point Point { get; set; }
        public float Speed { get; set; }
    }
    internal class BallData : IBallData
    {
        public BallData(string rawInput)
        {
            var components = rawInput.Split(',', StringSplitOptions.RemoveEmptyEntries);
            Point = new Point(float.Parse(components[0]), float.Parse(components[1]), float.Parse(components[2]));
            Speed = float.Parse(components[3]);
            Posession = (Posession)Enum.Parse(typeof(Posession), components[4]);
            BallState = (BallState)Enum.Parse(typeof(BallState), components[5]);
            BallClickerFlag = components.Count() == 7 ? (BallClickerFlag)Enum.Parse(typeof(BallClickerFlag), components[6]) : BallClickerFlag.None;

        }
        public Point Point { get; set; }
        public float Speed { get; set; }
        public Posession Posession { get; set; }
        public BallState BallState { get; set; }
        public BallClickerFlag BallClickerFlag { get; set; }

    }
    public class Frame
    {
        public int FrameCount { get; set; }
        public List<ITrackedObject> TrackedObjects { get; set; }
        public IBallData BallData { get; set; }
        public Frame (int frameCount, List<ITrackedObject> trackedObjects, IBallData ballData)
        {
            FrameCount = frameCount;
            this.TrackedObjects = trackedObjects;
            this.BallData = ballData;
        }

        public void PrintBallKeeper(List<Frame> list, Line goalLine)
        {
            foreach(var f in list)
            {
                Console.WriteLine(f.FrameCount);
                Console.WriteLine("ball: " + f.BallData.Point.X + ", " + f.BallData.Point.Y);
                var keeperA = f.TrackedObjects.Where(t => t.PlayerNumber == 22).FirstOrDefault().Point;
                Console.WriteLine("keeperA: " + keeperA.X + ", " + keeperA.Y);
                var d = f.BallData.Point.Distance(keeperA);
                Console.WriteLine("Distance: " + d);
                Console.WriteLine("Distance to goal line: " + f.BallData.Point.Distance(goalLine));
                Console.WriteLine();
            }
        }
    }
}

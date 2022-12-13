//using DataEnhancement;
//using System.ComponentModel.DataAnnotations;

//var dirName = Directory.GetCurrentDirectory();
//var parent1 = Directory.GetParent(dirName)!.Parent!.Parent!.Parent;

//Point awayDangerL = new Point(-5250, -3000);
//Point awayDangerR = new Point(-5250, 3000);
//Point homeDangerL = new Point(5250, 366);
//Point homeDangerR = new Point(5250, -366);

//using (StreamReader sr = File.OpenText(parent1!.FullName + "\\" + "KeeperSaveAssignment.dat"))
//{
//    string? s = string.Empty;

//    var topLine = new Line();
//    var bottomLine = new Line();

//    var goalLineH = new Line(homeDangerL, homeDangerR);
//    var goalLineA = new Line(awayDangerL, awayDangerR);

//    List<Frame> framesOfInterest = new List<Frame>();
//    //int frameN =0;
//    int countFramesApproaching = 0;
//    bool inDangerOnPrevIteration = false;
//    //var cc = 0;
//    while ((s = sr.ReadLine()) != null)
//    {

//        //var ll = new List<int>() { 1525041, 1526848, 1527574, 1536827, 1546251, 1546871, 1563645, 1569919, 1586400, 1615466, 1621550, 1630832, 1638012, 1638400, 1643516, 1651550, 1651774, 1657641, 1665443, 1667817, 1683127, 1689533 };

//        //frameN++;
//        var f = DataParser.ReadOneFrame(s);
//        //if (ll.Contains(f.FrameCount))
//        //{
//        //    var text = f.BallData.Point.X < 0 ? "A" : "H";
//        //    Console.WriteLine(f.FrameCount + "     " + text);

//        //}
//        //continue;
//        //f.PrintBallKeeper(new List<Frame> { f }, goalLineA);
//        // Picking the frames when the ball is close to either of the goal lines
//        if (f.BallData.Point.Distance(goalLineA) <= 1650)
//        {
//            if (!inDangerOnPrevIteration)
//            {
//                topLine = new Line(f.BallData.Point, awayDangerR);
//                bottomLine = new Line(f.BallData.Point, awayDangerL);
//                inDangerOnPrevIteration = true;
//                continue;
//            }

//            //Check if the ball is headed towards the danger line
//            if (AdditionalFunctions.BallApproachingDangerZone(topLine, bottomLine, f.BallData.Point))
//            {
//                countFramesApproaching++;
//                framesOfInterest.Add(f);

//            }
//            else //approaching is interrupted
//            {
//                // If the ball has been heading towards the danger zone for more than 10 frames and then changed direction or stopped we want to check those frames and a few additional ones
//                if (countFramesApproaching > 10)
//                {
//                    //f.PrintBallKeeper(framesOfInterest, goalLineA);
//                    //framesOfInterest = new List<Frame>();
//                    //Console.WriteLine("------------------");

//                    for (int i = 1; i <= 15; i++)
//                    {
//                        s = sr.ReadLine();
//                        f = DataParser.ReadOneFrame(s);
//                        //frameN++;
//                        framesOfInterest.Add(f);
//                    }
//                    var framesWithSaves = SaveDetector.FilterOutNoSaves(framesOfInterest);
//                    if (framesWithSaves != null)
//                    {
//                        var direction = SaveDetector.SaveDirection(framesWithSaves.Value.Item1, framesWithSaves.Value.Item2);
//                        Console.WriteLine(framesWithSaves.Value.Item2.FrameCount + "   " + direction);
//                        //cc++;
//                        //if(dd.Value.Item2.FrameCount == 1563644)
//                        //    f.PrintBallKeeper(dd.Value.Item1, goalLineA);

//                    }
//                    //f.PrintBallKeeper(dd, goalLineA);
//                    //f.PrintBallKeeper(framesOfInterest, goalLineA);
//                    countFramesApproaching = 0;
//                    //Console.WriteLine("------------------");
//                    //Console.WriteLine("------------------");
//                    //Console.WriteLine("------------------");

//                }

//                countFramesApproaching = 0;
//                framesOfInterest = new List<Frame>();
//            }



//            //topLine = new Line(f.BallData.Point, awayDangerR);
//            //bottomLine = new Line(f.BallData.Point, awayDangerL);

//            //Console.WriteLine(frameN);



//        }
//        else
//        {
//            inDangerOnPrevIteration = false;
//            countFramesApproaching = 0;
//            framesOfInterest = new List<Frame>();
//        }
//        topLine = new Line(f.BallData.Point, awayDangerR);
//        bottomLine = new Line(f.BallData.Point, awayDangerL);
//    }
//    //if (frameN == 14894)
//    //    Console.WriteLine(count);
//    //var keeperA = f.TrackedObjects.Where(t => t.PlayerNumber == 22).FirstOrDefault();
//    //var keeperH = f.TrackedObjects.Where(t => t.PlayerNumber == 12).FirstOrDefault();
//    //if ((keeperA == null) || (keeperH == null))
//    //    continue;

//    //var checkAwayL = awayLL.PlacePoint(f.BallData.Point);
//    //var checkAwayR = awayRL.PlacePoint(f.BallData.Point);
//    //if ((checkAwayL >= 0) && (checkAwayR <= 0))
//    //    count++;
//    //else
//    //{
//    //    //if (count > 5)
//    //    //    Console.WriteLine(count + "  frame: " + frameN + "   " + f1.BallData.BallPosition());
//    //    count = 0;
//    //}
//    //if ((frameN <= 14900) && (frameN >= 14800))
//    //{
//    //    Console.WriteLine(keeperA.Point.X + "," + keeperA.Point.Y);
//    //    Console.WriteLine(f1.BallData.Point.X + "," + f1.BallData.Point.Y);
//    //    Console.WriteLine(frameN + "      " +count);
//    //    Console.WriteLine("left: " + awayLL.A + "x + " + awayLL.B + "y + " + awayLL.C + "  check:" + checkAwayL);
//    //    Console.WriteLine("right: " + awayRL.A + "x + " + awayRL.B + "y + " + awayRL.C + "  check:" + checkAwayR);

//    //    Console.WriteLine("------------------");

//    ////}
//    //var distanceBallkeeperH = f.BallData.Point.Distance(keeperH.Point);
//    //var distanceBallkeeperA = f.BallData.Point.Distance(keeperA.Point);
//    //if (distanceBallkeeperA == 0)
//    //    Console.WriteLine(distanceBallkeeperA + "    " + frameN);


//    //Console.WriteLine(distanceBallkeeperA + "    " + frameN);
//    //Console.WriteLine(distanceBallkeeperH + "    " + frameN);


//    //awayLL = new Line(f.BallData.Point, awayDangerL);
//    //awayRL = new Line(f.BallData.Point, awayDangerR);

//    //homeLL = new Line(f.BallData.Point, homeDangerL);
//    //homeRL = new Line(f.BallData.Point, homeDangerR);

//    //var check1 = awayRL.PlacePoint(f1.BallData.Point);
//    //var check2 = awayLL.PlacePoint(f1.BallData.Point);


//    //var c1 = f1.TrackedObjects.Where(t => t.Team == Team.Referee).Count();
//    //var c2 = f1.TrackedObjects.Where(t => t.Team == Team.Home).Count();
//    //var c3 = f1.TrackedObjects.Where(t => t.Team == Team.Away).Count();
//    //var c4 = f1.TrackedObjects.Where(t => t.Team == Team.Ball).Count();
//    //var c5 = f1.TrackedObjects.Where(t => t.Team == Team.None).Count();

//    Console.WriteLine(cc);
//    Console.WriteLine("-----------------------------------------------------");

//}




//public class Triangle
//{
//    public Point P1 { get; set; }
//    public Point P2 { get; set; }
//    public Point P3 { get; set; }

//    public Triangle (Point p1, Point p2, Point p3)
//    {
//        P1 = p1;
//        P2 = p2;
//        P3 = p3;
//    }

//    private float Area 

//}

//public string BallPosition()
//{
//    var result = "";

//    if (Point.Y < -1700)
//        result += "B";
//    if (Point.Y > 1700)
//        result += "T";
//    if (Math.Abs(Point.Y) <= 1700)
//        result += "C";

//    if (Point.X < -2625)
//        result += "L";
//    if (Point.X > 2625)
//        result += "R";
//    if (Math.Abs(Point.X) <= 2650)
//        result += "C";
//    return result;
//}

using DataEnhancement;
using System.ComponentModel.DataAnnotations;

var dirName = Directory.GetCurrentDirectory();
var parent1 = Directory.GetParent(dirName)!.Parent!.Parent!.Parent;

Point awayDangerL = new Point(-5250, -3000);
Point awayDangerR = new Point(-5250, 3000);

using (StreamReader sr = File.OpenText(parent1!.FullName + "\\" + "KeeperSaveAssignment.dat"))
{
    string? s = string.Empty;

    //these are lines between the ball and the goal posts
    var topLine = new Line();
    var bottomLine = new Line();

    var goalLineA = new Line(awayDangerL, awayDangerR);

    // this variable will store the frames which might contain a save
    List<Frame> framesOfInterest = new List<Frame>();
    int countFramesApproaching = 0;

    while ((s = sr.ReadLine()) != null)
    {
        var f = DataParser.ReadOneFrame(s);

        // Picking the frames when the ball is close to the goal line
        if (f.BallData.Point.Distance(goalLineA) <= 1650)
        {
            //Check if the ball is headed towards the goal line 
            if (AdditionalFunctions.BallApproachingDangerZone(topLine, bottomLine, f.BallData.Point))
            {
                countFramesApproaching++;
                framesOfInterest.Add(f);
            }
            else //approaching is interrupted
            {
                // If the ball has been heading towards the danger zone for more than 10 frames and then changed direction or stopped we want to check those frames and a few additional ones
                if (countFramesApproaching > 10)
                {
                    for (int i = 1; i <= 15; i++)
                    {
                        s = sr.ReadLine();
                        f = DataParser.ReadOneFrame(s);
                        framesOfInterest.Add(f);
                    }
                    var framesWithSaves = SaveDetector.FilterOutNoSaves(framesOfInterest);
                    if (framesWithSaves != null)
                    {
                        var direction = SaveDetector.SaveDirection(framesWithSaves.Value.Item1, framesWithSaves.Value.Item2);
                        Console.WriteLine(framesWithSaves.Value.Item2.FrameCount + "   " + direction);
                    }
                }
                // Reset the tracked frames
                countFramesApproaching = 0;
                framesOfInterest = new List<Frame>();
            }
        }
        else // we stop tracking the ball if it is too far from the goal line
        {
            countFramesApproaching = 0;
            framesOfInterest = new List<Frame>();
        }
        // lines between the goal posts and the ball are updated in the end of an iteration so they can be used on the next one
        topLine = new Line(f.BallData.Point, awayDangerR);
        bottomLine = new Line(f.BallData.Point, awayDangerL);
    }

    Console.WriteLine("-----------------------------------------------------");

}






using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class WallHugger : Bot
{   
    
    static void Main(string[] args)
    {
        new WallHugger().Start();
    }

    

    WallHugger() : base(BotInfo.FromFile("WallHugger.json")) { }

    public override void Run()
    {
        /* Customize bot colors */
        BodyColor = Color.FromArgb(0xab, 0x5d, 0xee);
        TurretColor = Color.FromArgb(0x1d, 0xdd, 0x33);
        RadarColor = Color.FromArgb(0xc5, 0xc5, 0xc5);
        BulletColor = Color.Red;
        ScanColor = Color.FromArgb(0xbe, 0x80, 0xff);
        TracksColor = Color.FromArgb(0xbe, 0x80, 0xff);
        GunColor = Color.FromArgb(0xfc, 0xfc, 0xfc);
    
         

        bool toLeft = false, toRight = false, toTop = false, toBottom = false;

        
        var distanceToRight = ArenaWidth - this.X;
        var distanceToLeft = this.X;
        var distanceToTop = ArenaHeight - this.Y;
        var distanceToBottom = this.Y;


        if (distanceToBottom <= distanceToRight && distanceToBottom <= distanceToTop && distanceToBottom <= distanceToLeft) {
            toBottom = true;
            // Console.WriteLine("to bottom");
        }
        if (distanceToTop <= distanceToBottom && distanceToTop <= distanceToLeft && distanceToTop <= distanceToRight) {
            toTop = true; 
            // Console.WriteLine("to top");
        }
        if (distanceToRight <= distanceToLeft && distanceToRight <= distanceToTop && distanceToRight <= distanceToBottom) {
            toRight = true;
            // Console.WriteLine("to right");
        }
        if (distanceToLeft <= distanceToRight && distanceToLeft <= distanceToTop && distanceToLeft <= distanceToBottom) {
            toLeft = true;
            // Console.WriteLine("to left");
        }
        

        if (toBottom && IsRunning) {
            TurnLeft(CalcBearing(-90));
            while (this.Y > 50 && IsRunning) {
                Forward(15);
                TurnGunLeft(CalcBearing(90));
                TurnGunLeft(CalcBearing(-90));
                // Console.WriteLine("####### to bottom 1 #######");
                // PrintBotInfo();
            }

            while (IsRunning) {

                while (this.X < (ArenaWidth - 50) && IsRunning) {
                    TurnLeft(CalcBearing(0));
                    TurnGunLeft(CalcGunBearing(90));
                    TurnGunLeft(CalcGunBearing(180));
                    TurnGunLeft(CalcGunBearing(90));
                    TurnGunLeft(CalcGunBearing(0));
                    Forward(25);
                    // Console.WriteLine("####### to bottom 2 #######");
                    // PrintBotInfo();
                }

                while (this.X > 50 && IsRunning) {
                    TurnLeft(CalcBearing(180));
                    TurnGunLeft(CalcGunBearing(90));
                    TurnGunLeft(CalcGunBearing(0));
                    TurnGunLeft(CalcGunBearing(90));
                    TurnGunLeft(CalcGunBearing(180));
                    Forward(25);
                    // Console.WriteLine("####### to bottom 3 #######");
                    // PrintBotInfo();
                }

                
            }
        }

        if (toTop && IsRunning) {
            TurnLeft(CalcBearing(90));
            while (this.Y < (ArenaHeight - 50) && IsRunning) {
                Forward(15);
                TurnGunLeft(CalcBearing(-90));
                TurnGunLeft(CalcBearing(90));
                // Console.WriteLine("####### to top 1 #######");
                // PrintBotInfo();
            }

            while (IsRunning) {

                while (this.X < (ArenaWidth - 50) && IsRunning) {
                    TurnLeft(CalcBearing(0));
                    TurnGunLeft(CalcGunBearing(-90));
                    TurnGunLeft(CalcGunBearing(-180));
                    TurnGunLeft(CalcGunBearing(-90));
                    TurnGunLeft(CalcGunBearing(0));
                    Forward(25);
                    // Console.WriteLine("####### to top 2 #######");
                    // PrintBotInfo();
                }

                while (this.X > 50 && IsRunning) {
                    TurnLeft(CalcBearing(180));
                    TurnGunLeft(CalcGunBearing(-90));
                    TurnGunLeft(CalcGunBearing(0));
                    TurnGunLeft(CalcGunBearing(-90));
                    TurnGunLeft(CalcGunBearing(-180));
                    Forward(25); 
                    // Console.WriteLine("####### to top 3 #######"); 
                    // PrintBotInfo();  
                }
            }
        }

        if (toLeft && IsRunning) {
            TurnLeft(CalcBearing(180));
            while (this.X > 50 && IsRunning) {
                Forward(15);
                TurnGunLeft(CalcBearing(0));
                TurnGunLeft(CalcBearing(180));
                // Console.WriteLine("####### to left 1 #######");
                // PrintBotInfo();
            }

            while (IsRunning) {

                while (this.Y < (ArenaHeight - 50) && IsRunning) {
                    TurnLeft(CalcBearing(90));
                    TurnGunLeft(CalcGunBearing(0));
                    TurnGunLeft(CalcGunBearing(-90));
                    TurnGunLeft(CalcGunBearing(0));
                    TurnGunLeft(CalcGunBearing(90));
                    Forward(25);
                    // Console.WriteLine("####### to left 2 #######");
                    // PrintBotInfo();
                }

                while (this.Y > 50 && IsRunning) {
                    TurnLeft(CalcBearing(-90));
                    TurnGunLeft(CalcGunBearing(0));
                    TurnGunLeft(CalcGunBearing(90));
                    TurnGunLeft(CalcGunBearing(0));
                    TurnGunLeft(CalcGunBearing(-90));
                    Forward(25);
                    // Console.WriteLine("####### to left 3 #######");
                    // PrintBotInfo();
                }
            }
        }

        if (toRight && IsRunning) {
            TurnLeft(CalcBearing(0));
            while (this.X < (ArenaWidth - 50) && IsRunning) {
                Forward(15);
                TurnGunLeft(CalcBearing(180));
                TurnGunLeft(CalcBearing(0));
                // Console.WriteLine("####### to right 1 #######");
                // PrintBotInfo();
            }

            while (IsRunning) {
                // Fire(1);

                while (this.Y < (ArenaHeight - 50) && IsRunning) {
                    
                    TurnLeft(CalcBearing(90));
                    TurnGunLeft(CalcGunBearing(180));
                    TurnGunLeft(CalcGunBearing(-90));
                    TurnGunLeft(CalcGunBearing(180));
                    TurnGunLeft(CalcGunBearing(90));
                    Forward(25);
                    // Console.WriteLine("####### to right 2 #######");
                    // PrintBotInfo();
                }

                while (this.Y > 50 && IsRunning) {
                    TurnLeft(CalcBearing(-90));
                    TurnGunLeft(CalcGunBearing(180));
                    TurnGunLeft(CalcGunBearing(90));
                    TurnGunLeft(CalcGunBearing(180));
                    TurnGunLeft(CalcGunBearing(-90));
                    Forward(25);
                    // Console.WriteLine("####### to right 3 #######");
                    // PrintBotInfo();
                }
            }
        }

    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        if (EnemyCount == 1) {
            Fire(1);
        } else {
            if (DistanceTo(e.X, e.Y) <= 600 && DistanceTo(e.X, e.Y) >= 200) {
                Fire(1);
            } else if (DistanceTo(e.X, e.Y) < 200) {
                Fire(2);
            }
        }
    }

    public override void OnHitBot(HitBotEvent e)
    {
        Back(15);
    }

    public override void OnHitWall(HitWallEvent e)
    {
        Back(15);
    }

    public void PrintBotInfo() {
        Console.WriteLine($"my distance remaining: {this.DistanceRemaining}");
        Console.WriteLine($"my gun turn rate: {this.GunTurnRate}");
        Console.WriteLine($"my gun turn turn remaining: {this.GunTurnRemaining}");
        Console.WriteLine($"my is running: {this.IsRunning}"); 
        Console.WriteLine($"my radar turn rate: {this.RadarTurnRate}"); 
        Console.WriteLine($"my radar turn remaining: {this.RadarTurnRate}"); 
        Console.WriteLine($"my target speed: {this.TargetSpeed}"); 
        Console.WriteLine($"my turn rate: {this.TurnRate}"); 
        Console.WriteLine($"my turn remaining: {this.TurnRemaining}");
    }





    // public override void OnRoundStarted(RoundStartedEvent roundStatedEvent)
    // {
    //     Console.WriteLine("round start bot info: ");
    //     PrintBotInfo();
        
    // }


    /* Read the documentation for more events and methods */
}

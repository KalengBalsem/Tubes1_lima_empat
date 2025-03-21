using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;
using System;
using System.Drawing;
using System.Collections;
using SvgNet.Types;
using System.Runtime;

public class DistancePlease : Bot
{
    // State
    private Hashtable enemies = new Hashtable();
    private microEnemy target;
    private Vector2D nextDestination;
    private Vector2D lastPosition;
    private Vector2D myPos;
    private double myEnergy;
    private Random random = new Random();
    private double delta = 0;
    // Initializing variables

    
    // Constructor
    public DistancePlease() : base(BotInfo.FromFile("DistancePlease.json")) { }

    // Main loop
    public override void Run()
    {
        // Tank Color
        BodyColor = Color.FromArgb(0x80, 0x80, 0x80);    // Black
        TurretColor = Color.FromArgb(0x80, 0x80, 0x80);  // Black
        RadarColor = Color.FromArgb(0xFF, 0xFF, 0xFF);   // White
        BulletColor = Color.FromArgb(0x80, 0x80, 0x80);  // Black
        ScanColor = Color.FromArgb(0x80, 0x80, 0x80);    // Black
        TracksColor = Color.FromArgb(0x80, 0x80, 0x80);  // Black
        GunColor = Color.FromArgb(0x80, 0x80, 0x80);     // Black

        // Set radar to continuously scan the battlefield
        RadarTurnRate = MaxRadarTurnRate;
        nextDestination = lastPosition = myPos = new Vector2D(X, Y);
        target = new microEnemy();

        Console.WriteLine("Run");
        // main bot activity
        while (IsRunning)
        {
            myPos = new Vector2D(X, Y);
            myEnergy = Energy;

            if (target.live && TurnNumber > 9) {
                try {
                    DoMovement();
                    DoGun();
                } catch (Exception ex) {
                    Console.WriteLine("Exception in doMovementAndGun: " + ex.Message);
                }
            }
            Go();
        }
    }
    
    // minor events
    public override void OnBotDeath(BotDeathEvent e) {
        ((microEnemy)enemies[e.VictimId]).live = false;
    }

    // scan event
    public override void OnScannedBot(ScannedBotEvent e) {
        Console.WriteLine("Scanned bot event triggered");
        
        microEnemy en = (microEnemy)enemies[e.ScannedBotId];
        if (en == null) {
            Console.WriteLine("en == null -> new Enemy Entry");
            en = new microEnemy();
            enemies[e.ScannedBotId] = en;

            Console.WriteLine($"Enemy {e.ScannedBotId} updated. Enemies count: {enemies.Count}");
        }
        
        en.energy = e.Energy;
        en.live = true;

        double e_distance = myPos.Distance(new Vector2D(e.X, e.Y));
        en.pos = new Vector2D(e.X, e.Y);
        
        // Now target selection is handled by UpdateTarget() in your Run loop.
        // Target selection: choose the closest live enemy
        if (target == null || !target.live || e_distance < myPos.Distance(target.pos)) {
            target = en;
            // Console.WriteLine($"Setting target to {e.ScannedBotId}, distance={e_distance:F1}, live={target.live}");
        }

        Console.WriteLine("OnScannedBot end");
    }

    public void DoGun() {
        double distanceToTarget = DistanceTo(target.pos.X, target.pos.Y);
        // Gun (HeadOnTargeting)
        // fire when gun is aligned, cooled, and energy > 1 (avoid suicide)
        delta = NormalizeRelativeAngle(GunDirection - calcAngleDegrees(target.pos, myPos));

        if(Math.Abs(delta) < 1 && GunHeat==0 && myEnergy > 1) {
            SetFire( Math.Min(Math.Min(myEnergy/6d, 1300d/distanceToTarget), target.energy/3d) );
        }
        
        // Console.WriteLine($"Debug: GunAngle={delta:F2}, NormalizedGunAngle={NormalizeRelativeAngle(delta)}, BearingToTarget={GunBearingTo(target.pos.X, target.pos.Y)}, target position: ({target.pos.X}, {target.pos.Y}), GunDirection={GunDirection:F2}");

        SetTurnGunRight(delta);
    }

    public void DoMovement() {
        // Move
        double distanceToNextDestination = DistanceTo(nextDestination.X, nextDestination.Y);
        // search a new destination if I reached this one
        if(distanceToNextDestination < 15) {
            int aliveEnemies = 0;
            foreach (DictionaryEntry entry in enemies)
            {
                microEnemy enemy = (microEnemy)entry.Value;
                if (enemy.live)
                {
                    aliveEnemies++;
                }
            }

            double addLast = 1 - Math.Round(Math.Pow(random.NextDouble(), aliveEnemies));

            Rectangle2D battleField = new Rectangle2D(30, 30, ArenaWidth - 60, ArenaHeight - 60);
            Vector2D testPoint;
            for (int i = 0; i<200; i++) {
                testPoint = calcPoint(myPos, Math.Min(DistanceTo(target.pos.X, target.pos.Y)*0.8, 100 + 200*random.NextDouble()), 360*random.NextDouble());

                if(battleField.Contains(testPoint.X, testPoint.Y) && Evaluate(testPoint, addLast) < Evaluate(nextDestination, addLast)) {
                    nextDestination = testPoint;
                }
            }
            lastPosition = myPos;
            Console.WriteLine($"selected nextDestination: ({nextDestination.X}, {nextDestination.Y}), last position: ({lastPosition.X}, {lastPosition.Y}), distance: {distanceToNextDestination}");
        } else {
            double angle = calcAngleDegrees(nextDestination, myPos) - Direction;   
            double desiredAngle = calcAngleDegrees(nextDestination, myPos);    
            double moveDirection = 1;
            
            Console.WriteLine(angle);

            if (Math.Cos(angle * Math.PI / 180) < 0) {
                angle += 180;
                moveDirection = -1;
                angle = NormalizeRelativeAngle(angle);
            } else {
                angle = -1 * NormalizeRelativeAngle(angle);
            }
            
            SetForward(distanceToNextDestination * moveDirection);
            Console.WriteLine(angle);
            SetTurnRight(angle);
            
            // Set speed based on the actual turn angle
            MaxSpeed = Math.Abs(angle) > 57 ? 0 : 8;
            
            // Console.WriteLine($"Turn {TurnNumber}: New Direction={Direction}, DesiredAngle={desiredAngle}, RelativeAngle={angle}, TurnAngle={NormalizeRelativeAngle(angle)}, MoveDirection={moveDirection}, MaxSpeed={MaxSpeed}, Distance={distanceToNextDestination}");
            // Console.WriteLine($"selected nextDestination: ({nextDestination.X}, {nextDestination.Y}), last position: ({lastPosition.X}, {lastPosition.Y})");
        }

    }

    // eval position
    // Evaluate risk for candidate position "p"
    // currentPosWeight is a weight for penalizing staying too close to current position (optional)
    public double Evaluate(Vector2D p, double addLast){
        // Base risk: penalize candidate points that are too close to current position
        // This encourages movement
        double eval = addLast*0.08/p.DistanceSquared(lastPosition);
        
        // Add risk contributions from each enemy
        foreach (DictionaryEntry entry in enemies) {
            microEnemy en = (microEnemy)entry.Value;
            double angleDiff = calcAngleDegrees(myPos, p) - calcAngleDegrees(en.pos, p);
            if (en.live) {
                eval += Math.Min(en.energy/myEnergy,2) * (1 + Math.Abs(Math.Cos(angleDiff * Math.PI / 180))) / p.DistanceSquared(en.pos);
            }
        }
        return eval;
    }

    // math
    private Vector2D calcPoint(Vector2D p, double dist, double angDegrees) {
        double angRadians = angDegrees * Math.PI / 180;
        return new Vector2D(p.X + dist * Math.Sin(angRadians), p.Y + dist * Math.Cos(angRadians));
    }

    private double calcAngleDegrees(Vector2D p2, Vector2D p1) {
        return Math.Atan2(p2.Y - p1.Y, p2.X - p1.X) * 180 / Math.PI;
    }

    // microEnemy
    public class microEnemy {
        public Vector2D pos;
        public double energy;
        public bool live;
    }


    // Helper Lightweight Data Structure
    public struct Vector2D
    {
        public double X { get; }
        public double Y { get; }

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Vector2D operator +(Vector2D a, Vector2D b) => new Vector2D(a.X + b.X, a.Y + b.Y);
        public static Vector2D operator -(Vector2D a, Vector2D b) => new Vector2D(a.X - b.X, a.Y - b.Y);
        public static Vector2D operator *(double scalar, Vector2D v) => new Vector2D(scalar * v.X, scalar * v.Y);
        public double Dot(Vector2D other) => X * other.X + Y * other.Y;

        // Method to calculate the squared distance between two Vector2D points
        public double DistanceSquared(Vector2D other)
        {
            double deltaX = this.X - other.X;
            double deltaY = this.Y - other.Y;
            return deltaX * deltaX + deltaY * deltaY;
        }

        public double Distance(Vector2D other)
        {
            return Math.Sqrt(DistanceSquared(other));
        }
    }

    public struct Rectangle2D
    {
        public double X;
        public double Y;
        public double Width;
        public double Height;

        public Rectangle2D(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        // Check if a point (x, y) is inside the rectangle
        public bool Contains(double x, double y)
        {
            return x >= X && x <= X + Width && y >= Y && y <= Y + Height;
        }

        // Check if another rectangle intersects with this one
        public bool Intersects(Rectangle2D other)
        {
            return X < other.X + other.Width && X + Width > other.X &&
                Y < other.Y + other.Height && Y + Height > other.Y;
        }

        public override string ToString() => $"Rectangle2D[X={X}, Y={Y}, W={Width}, H={Height}]";
    }

    static void Main(string[] args)
    {
        new DistancePlease().Start();
    }
}
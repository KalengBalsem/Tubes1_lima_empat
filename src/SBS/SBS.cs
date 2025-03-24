using System;
using System.Drawing;
using Microsoft.VisualBasic;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class SBS : Bot
{
    bool peek;
    double moveAmount; // how much to move
    // The main method starts our bot
    static void Main(string[] args)
    {
        new SBS().Start();
    }

    // Constructor, which loads the bot config file
    SBS() : base(BotInfo.FromFile("SBS.json")) { }

    // Called when a new round is started -> initialize and do some movement
    public override void Run()
    {

        BodyColor = Color.FromArgb(0xFF, 0xD7, 0x00);   // Gold
        TurretColor = Color.FromArgb(0xFF, 0xA5, 0x00); // Orange
        RadarColor = Color.FromArgb(0xFF, 0x8C, 0x00);  // Dark Orange
        BulletColor = Color.FromArgb(0xFF, 0x45, 0x00); // Orange-Red
        ScanColor = Color.FromArgb(0xFF, 0xFF, 0x00);   // Bright Yellow 
        TracksColor = Color.FromArgb(0x99, 0x33, 0x00); // Dark   
        GunColor = Color.FromArgb(0xCC, 0x55, 0x00);    // Medium Orange
      
        // Initiliaze the Move to go to walls first. 
        moveAmount = Math.Max(ArenaHeight, ArenaWidth);
        peek = false;
        TurnRight(Direction %90);
        Forward(moveAmount);
        peek = true;
        TurnGunRight(90);
        TurnRight(90);

        // Repeat while the bot is running
        while (IsRunning)
        {
            TurnGunRight(360);
        }
    }

    // THATS A TANK?
    // We set energy level for Ranged Bots. Dont wanna put all power to a bot thats far
    public override void OnScannedBot(ScannedBotEvent e)
    {
        SetTurnRight(CalcBearing(e.Direction));
        double Range = Math.Sqrt(e.X * e.X + e.Y * e.Y);
        if (Range >= 500) 
        {
            SetFire(1);
            SetForward(100);
            Rescan();
        }
        else if (Range >= 400 && Range <500)
        {
            SetFire(2);
            SetForward(100);
            Rescan();
        }
        else if (Range <400)
        {
            SetFire(3);
            SetForward(100);
            Rescan();
        }
    }
    

    // GOT HIT BY A BULLET
    // Revenge by firing a mini bullet into its location
    public override void OnHitByBullet(HitByBulletEvent e)
    {
        // Calculate the bearing to the direction of the bullet
        var bearing = CalcBearing(e.Bullet.Direction);

        // Turn 90 degrees to the bullet direction based on the bearing
        TurnLeft(90 - bearing);
        Back(100);
        TurnGunLeft(bearing);
        SetFire(1);
        Forward(100);
    }

    // GOT HIT TANK
    // We Fire on the tank on Hit
    public override void OnHitBot(HitBotEvent e)
    {
        // If On The Front
        var bearing = BearingTo(e.X, e.Y);
        if (bearing > -90 && bearing < 90)
        {
            TurnGunRight(bearing);
            SetFire(3);
            Back(100);
        }
        else
        { // If On the Back
            TurnGunRight(bearing);
            SetFire(3);
            Forward(100);
        }
    }

    
}




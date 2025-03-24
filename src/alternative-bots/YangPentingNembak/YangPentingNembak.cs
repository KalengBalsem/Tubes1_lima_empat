using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;
using System.Drawing;
using System.Collections.Generic;

// ------------------------------------------------------------------
// YangPentingNembak
// ------------------------------------------------------------------
// Menggunakan peluru dengan damage terbesar
// ------------------------------------------------------------------

public class YangPentingNembak : Bot
{
    // The main method starts our bot
    static void Main(string[] args)
    {
        new YangPentingNembak().Start();
    }

    // Constructor, which loads the bot config file
    YangPentingNembak() : base(BotInfo.FromFile("YangPentingNembak.json")) { }

    // Called when a new round is started -> initialize and do some movement
    int gunDirection = 1;

    public override void Run() 
    {
        BodyColor = Color.FromArgb(0xFF, 0xFF, 0xFF);
        TurretColor = Color.FromArgb(0xFF, 0xFF, 0xFF);
        RadarColor = Color.FromArgb(0xFF, 0xFF, 0xFF);
        BulletColor = Color.FromArgb(0xFF, 0xFF, 0xFF);
        ScanColor = Color.FromArgb(0xFF, 0xFF, 0xFF);
        TracksColor = Color.FromArgb(0xFF, 0xFF, 0xFF);
        GunColor = Color.FromArgb(0xFF, 0xFF, 0xFF);
        // Turns the gun infinitely, looking for enemies
        while (IsRunning) 
        {
            TurnGunRight(360);
        }
    }

    public override void OnScannedBot(ScannedBotEvent e) {
        // Arahkan bot ke musuh
        SetTurnRight(CalcBearing(e.Direction));
        // HeadOnTargeting
        SetFire(3);
        // Bergerak maju
        SetForward(100);
        // Balikkan senjata di setiap turn
        GunTurnRate = -gunDirection;
        // Berbalik 360 derajat
        SetTurnGunRight(360 * gunDirection);
    }
}

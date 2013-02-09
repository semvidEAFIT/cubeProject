using UnityEngine;
using System.Collections;

public class LaserSensor : Sensor, ILaserReactive {

    private int lasersHitting = 0;

    public void HitByLaser(Vector3 laserPosition)
    {
        lasersHitting++;
    }

    public void StopHit(Vector3 laserPosition)
    {
        lasersHitting--;
    }

    public override bool CheckPressed()
    {
        return lasersHitting > 0;
    }
}

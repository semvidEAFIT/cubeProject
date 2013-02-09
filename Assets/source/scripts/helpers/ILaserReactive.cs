using UnityEngine;
using System.Collections;

public interface ILaserReactive  {

    void HitByLaser(Vector3 laserPosition);

    void StopHit(Vector3 laserPosition);
}

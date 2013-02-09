using UnityEngine;
using System.Collections;

public class NormalPushButton : Sensor{

    public override bool CheckPressed()
    {
        return Level.Singleton.ContainsElement(transform.position + transform.up.normalized);
    }
}

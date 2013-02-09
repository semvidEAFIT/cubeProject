using UnityEngine;
using System.Collections;

public class DigitalCube : WallWalkingCube {

    public float maxMovements;
    
    protected override bool IsDropable()
    {
        return true;
    }

    protected override bool IsWalkable(Entity e)
    {
        return e.tag == "Circuit";
    }

    public override bool IsSelected
    {
        get
        {
            return base.IsSelected && maxMovements > 0;
        }
    }
}

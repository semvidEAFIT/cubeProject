using UnityEngine;
using System.Collections;

public class MetalCube : WallWalkingCube {
    
    protected override bool IsWalkable(Entity e)
    {
        return e.gameObject.tag.Equals("Magnet");
    }

    protected override bool IsDropable()
    {
        return false;
    }
}

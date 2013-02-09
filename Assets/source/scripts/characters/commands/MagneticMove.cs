using UnityEngine;
using System.Collections;

public class MagneticMove : Move {

    Vector3 floorDirection;
    public MagneticMove(WallWalkingCube receiver, Vector3 endPosition, Vector3 floorDirection) : base(receiver, endPosition) {
        this.floorDirection = floorDirection;
    }

    public override void Execute()
    {
        ((WallWalkingCube)Cube).MagneticMoveTo(EndPosition, floorDirection);
        if(Cube is DigitalCube){
            ((DigitalCube)Cube).maxMovements--;
        }
    }
}

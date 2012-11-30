using UnityEngine;
using System.Collections;

public class MagneticMove : Move {
    public MagneticMove(WallWalkingCube receiver, Vector3 endPosition) : base(receiver, endPosition) { }

    public override void Execute()
    {
        base.Execute();
        if(Cube is DigitalCube){
            ((DigitalCube)Cube).maxMovements--;
        }
    }
}

using UnityEngine;
using System.Collections;

public class Move : Command {

    public Move(Cube receiver, Vector3 endPosition) : base(receiver, endPosition){
        
    }

    public override void Execute()
    {
        if (EndPosition.x >= Level.Dimension || EndPosition.x < 0 || EndPosition.z >= Level.Dimension || EndPosition.z < 0)
        {
            Cube.FallOutOfBounds();
        }
        else 
        {
            Cube.MoveTo(EndPosition);
        }
        EndExecution();
    }
}

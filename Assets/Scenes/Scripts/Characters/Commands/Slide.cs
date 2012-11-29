using UnityEngine;
using System.Collections;

public class Slide : Command {
    private Vector3 direction;

    public Vector3 Direction
    {
        get { return direction; }
    }
	public Slide(IceCube receiver, Vector3 endPosition, Vector3 direction): base(receiver, endPosition){
        this.direction = direction;
    }
	
	public override void Execute ()
	{
		((IceCube)Cube).Slide(EndPosition, direction);
		if(EndPosition.x >= Level.Dimension || EndPosition.x < 0 || EndPosition.z >= Level.Dimension || EndPosition.z < 0){
			Cube.FallOutOfBounds();
		}
        EndExecution();
	}
}

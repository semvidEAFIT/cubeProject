using UnityEngine;
using System.Collections;

public class Slide : Command {

	public Slide(IceCube receiver, Vector3 endPosition): base(receiver, endPosition){}
	
	public override void Execute ()
	{
		((IceCube)Cube).Slide(EndPosition);
		if(EndPosition.x >= Level.Dimension || EndPosition.x < 0 || EndPosition.z >= Level.Dimension || EndPosition.z < 0){
			Cube.FallOutOfBounds();
		}
        EndExecution();
	}
}

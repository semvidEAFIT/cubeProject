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
<<<<<<< HEAD:Assets/Scripts/Characters/Commands/Slide.cs
=======
		if(EndPosition.x >= Level.Dimension || EndPosition.x < 0 || EndPosition.z >= Level.Dimension || EndPosition.z < 0){
			Cube.FallOutOfBounds();
		}
>>>>>>> 9a72fa277aa9f97db008dd86cf78565f7c60917e:Assets/Scenes/Scripts/Characters/Commands/Slide.cs
        //EndExecution();
	}
}

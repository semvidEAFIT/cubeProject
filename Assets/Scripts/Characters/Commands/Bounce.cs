using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bounce : Command {
	
	private List<Vector3> bouncePositions;
		
	// Use this for initialization
<<<<<<< HEAD
	public Bounce(RubberCube receiver, Vector3 endPosition, List<Vector3> bouncePositions) : base(receiver, endPosition)
=======
	public Bounce(RubberCube receiver, Vector3 endPosition, Vector3 intermediate) : base(receiver, endPosition)
>>>>>>> b119637261c615f04775455a894d1e68ccbe6498
	{
		this.bouncePositions = bouncePositions;
	}

	public override void Execute ()
	{
		//Cube.MoveTo(intermediate);
<<<<<<< HEAD
		//Cube.MoveTo(EndPosition);
		//AnimationHelper.AnimateBounce(Cube,Vector3.down,bouncePosition,EndPosition);
		((RubberCube)Cube).Bounce(EndPosition,bouncePositions);
		//EndExecution();
	}

	public List<Vector3> BouncePositions {
		get {
			return this.bouncePositions;
		}
		set {
			bouncePositions = value;
		}
=======
		((RubberCube)Cube).Bounce(EndPosition);
		//EndExecution();
>>>>>>> b119637261c615f04775455a894d1e68ccbe6498
	}
}
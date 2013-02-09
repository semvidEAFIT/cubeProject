using UnityEngine;
using System.Collections;

public class Levitate : Command {
	
	private Vector3 direction;

	// Use this for initialization
	public Levitate(Cube receiver, Vector3 endPosition, Vector3 direction) : base(receiver, endPosition)
	{
		this.direction = direction;
	}
	
	// Update is called once per frame
	public override void Execute()
    {
		((LevitatorCube)Cube).CurrentDirection = direction;
        ((LevitatorCube)Cube).Levitate(EndPosition);
       // EndExecution();
    }
}

using UnityEngine;
using System.Collections.Generic;

public class CloudCube : Cube
{
	
	private bool fix;
		
		
	// Use this for initialization
	void Start ()
	{
		base.Start ();
		fix = false;
	}
	
	public override Command[] Options {
		get {
			List<Command> options = new List<Command> ();
			if (!fix) {
                Command[] baseOptions = base.Options;
                for (int i = 0; i < baseOptions.Length; i++ )
                {
                    Vector3 direction = new Vector3(baseOptions[i].EndPosition.x, this.transform.position.y, baseOptions[i].EndPosition.z) - this.transform.position;
                    if (this.transform.position.y - baseOptions[i].EndPosition.y > 1)
                    {
                        Vector3 newHangPosition = new Vector3(baseOptions[i].EndPosition.x, transform.position.y - 1.0f, baseOptions[i].EndPosition.z);
                        options.Add(new Hang(this, newHangPosition));
                    }
                    else 
                    {
                        options.Add(baseOptions[i]);
                    }
                }
			}
			
			return options.ToArray ();
		}
		
	}

	public void Hang (Vector3 nextPosition)
	{
		fix = true;
		Level.Singleton.RemoveEntity (transform.position);
		//transform.position = nextPosition;
		AnimationHelper.AnimateJump2 (gameObject, Vector3.down, nextPosition, 0f, "EndExecution", null);
		Level.Singleton.AddEntity (nextPosition, this);
	}
}

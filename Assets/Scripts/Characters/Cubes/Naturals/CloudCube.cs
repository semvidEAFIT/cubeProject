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
				for (int i = 0; i < 4; i ++) {
					Vector3 direction = Vector3.zero;
					switch (i) {
					case 0:
						direction = Vector3.forward;
						break;
					case 1:
						direction = Vector3.forward * -1f;
						break;
					case 2:
						direction = Vector3.right;
						break;
					case 3:
						direction = Vector3.right * -1f;
						break;
					}
					
<<<<<<< HEAD
					
					if( transform.position.y-2 > 0 
						&& !CubeHelper.IsFree(new Vector3(transform.position.x+direction.x,transform.position.y-2,transform.position.z+direction.z)) 
						&& !CubeHelper.IsFree(new Vector3(transform.position.x+direction.x,transform.position.y-1,transform.position.z+direction.z))
						&& !CubeHelper.IsFree(new Vector3(transform.position.x+direction.x,transform.position.y,transform.position.z+direction.z))
						){
	            		options.Add( new Hang(this, GetNextPositionCloude(direction)));
					}else{
=======
					if (transform.position.y - 2 >= 0) {
						Debug.Log ("abajo");
					}
					if (transform.position.y - 2 > 0 && !Level.Singleton.Entities.ContainsKey (new Vector3 (transform.position.x + direction.x, transform.position.y - 2, transform.position.z + direction.z)) && !Level.Singleton.Entities.ContainsKey (new Vector3 (transform.position.x + direction.x, transform.position.y - 1, transform.position.z + direction.z))) {
						options.Add (new Hang (this, GetNextPositionCloude (direction)));
					} else {
>>>>>>> 89c8d088db79abd721fad425e0e75efc19541c40
						
						options.Add (new Move (this, GetNextPositionCloude (direction)));
					}
				}
				//options.Add( new Move(this, GetNextPositionCloude(Vector3.forward)));
				//options.Add( new Move(this, GetNextPositionCloude(Vector3.forward * -1.0f)));
				//options.Add( new Move(this, GetNextPositionCloude(Vector3.right)));
				//options.Add( new Move(this, GetNextPositionCloude(Vector3.right * -1.0f)));
				
			
				for (int i = 0; i < options.Count; i++) {
					
<<<<<<< HEAD
					if (options [i].EndPosition.y - transform.position.y > 1) {
						options.RemoveAt (i);
					} else {
						if (options [i].EndPosition.x >= Level.Dimension || options [i].EndPosition.x < 0 || options [i].EndPosition.z >= Level.Dimension || options [i].EndPosition.z < 0) {
							options [i] = new OutOfBounds (this, options [i].EndPosition);
						}
					}
				}
=======
	                if(options[i].EndPosition.y - transform.position.y > 1)
	                {
	                    options.RemoveAt(i);
	                }
	            }
>>>>>>> 9a72fa277aa9f97db008dd86cf78565f7c60917e
				
				return options.ToArray ();
			}
			
			
			
			return options.ToArray ();
		}
		
	}
	
	public Vector3 GetNextPositionCloude (Vector3 direction)
	{
		Vector3 nextPosition = transform.position + direction.normalized;
		bool isFree = !Level.Singleton.Entities.ContainsKey (nextPosition);
		if (!isFree) {
			while (!isFree) {
				nextPosition.y += 1.0f;
				isFree = !Level.Singleton.Entities.ContainsKey (nextPosition);
			}
		
		} else {
            
			nextPosition.y -= 1.0f;
			if (!Level.Singleton.Entities.ContainsKey (nextPosition)) {
				if (nextPosition.y < 0) {
					nextPosition.y += 1;
						
				}
				return nextPosition;
					
					
			}
            
			nextPosition.y += 1.0f;
		}

		return nextPosition;
	
	}

	public void Hang (Vector3 nextPosition)
	{
		fix = true;
		Level.Singleton.Entities.Remove (transform.position);
		//transform.position = nextPosition;
		AnimationHelper.AnimateJump2 (gameObject, Vector3.down, nextPosition, 0f, "EndExecution", null);
		Level.Singleton.Entities.Add (nextPosition, this);
		
	}
}

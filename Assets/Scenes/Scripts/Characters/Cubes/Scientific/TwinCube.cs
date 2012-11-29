using UnityEngine;
using System.Collections.Generic;

public class TwinCube :Cube {
	
	public TwinCube twin;
	public string simAxis;
	public float lim;
	protected override void Start () 
	{
		base.Start();
		if(twin.transform.position.x == transform.position.x){
			simAxis = "z";
			lim = transform.position.z;
		}else{
			simAxis = "x";
			lim = transform.position.x;
		}
		
	}
	
	public override Command[] Options {
		get{
			//vector3.forward = 0,0,1
			List<Command> options = new List<Command>();
            options.Add(new Mimic(this, twin, CubeHelper.GetTopPosition(transform.position+Vector3.forward),Vector3.forward));
            options.Add(new Mimic(this, twin, CubeHelper.GetTopPosition(transform.position + Vector3.forward * -1.0f),Vector3.forward * -1f));
            options.Add(new Mimic(this, twin, CubeHelper.GetTopPosition(transform.position + Vector3.right),Vector3.right));
            options.Add(new Mimic(this, twin, CubeHelper.GetTopPosition(transform.position + Vector3.right * -1.0f),Vector3.right * - 1));

            for (int i = 0; i < options.Count; i++ )
            {
				
                if(options[i].EndPosition.y - transform.position.y > 1 )
                {
                    options.RemoveAt(i);
                }else if (options[i].EndPosition+Vector3.up == twin.transform.position || options[i].EndPosition+Vector3.down == twin.transform.position ){
					options.RemoveAt(i);
				}
                else
                {
                    if (options[i].EndPosition.x >= Level.Dimension || options[i].EndPosition.x < 0 || options[i].EndPosition.z >= Level.Dimension || options[i].EndPosition.z < 0)
                    {
                        options[i] = new OutOfBounds(this, options[i].EndPosition);
                    }
                }
            }

            return options.ToArray();
        }	
	}
	
	public Vector3 FindNextTwinPosition(Vector3 direction){
		Debug.Log(direction);
		Debug.Log(Vector3.right*-1);
		if(simAxis=="x"){
			if(direction == Vector3.forward || direction == Vector3.forward * -1){
				direction = CubeHelper.GetTopPosition(transform.position + direction);
			}else{
				direction = CubeHelper.GetTopPosition(transform.position+direction*-1);
			}
		}else if(direction == Vector3.right || direction == Vector3.right * -1){
			direction = CubeHelper.GetTopPosition(transform.position+direction	);
		}else{
			direction = CubeHelper.GetTopPosition(transform.position+direction*-1);
		}
		
		if( direction.y - transform.position.y > 1){
			direction = transform.position;
		}
		return direction;
	}
	
	public  void Mimic(Vector3 nextPosition, Vector3 direction) {
	
		
        Level.Singleton.Entities.Remove(transform.position);
        transform.position = nextPosition;
        Level.Singleton.Entities.Add(transform.position, this);
       
		Vector3 NextTwinPosition = twin.FindNextTwinPosition(direction);
		
		Level.Singleton.Entities.Remove(twin.transform.position);
		twin.transform.position = NextTwinPosition;
		Level.Singleton.Entities.Add(NextTwinPosition,twin);
    }
}
	



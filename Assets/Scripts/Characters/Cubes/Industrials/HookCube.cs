using UnityEngine;
using System.Collections.Generic;

public class HookCube : Cube {

	
	void Start () {
		base.Start();
	}
	

	void Update () {
	
	}
	
	public override Command[] Options {
		get {
			List<Command>options = new List<Command>();
				
			
			if(CubeHelper.GetLastPositionInDirection(transform.position , Vector3.forward).x - Level.Dimension < 0 && CubeHelper.GetLastPositionInDirection(transform.position , Vector3.forward).z - Level.Dimension < 0 )
            	options.Add( new Launch(this, CubeHelper.GetLastPositionInDirection(transform.position, Vector3.forward)));
            if(CubeHelper.GetLastPositionInDirection(transform.position , Vector3.forward * -1.0f).x - Level.Dimension < 0 && CubeHelper.GetLastPositionInDirection(transform.position, Vector3.forward* -1.0f).z - Level.Dimension < 0 )
				options.Add( new Launch(this, CubeHelper.GetLastPositionInDirection(transform.position, Vector3.forward * -1.0f)));
            if(CubeHelper.GetLastPositionInDirection(transform.position , Vector3.right).x - Level.Dimension < 0 && CubeHelper.GetLastPositionInDirection(transform.position, Vector3.right).z - Level.Dimension < 0 )
				options.Add( new Launch(this, CubeHelper.GetLastPositionInDirection(transform.position, Vector3.right)));
            if(CubeHelper.GetLastPositionInDirection(transform.position , Vector3.right * -1.0f).x - Level.Dimension < 0 && CubeHelper.GetLastPositionInDirection(transform.position, Vector3.right * -1.0f).z - Level.Dimension < 0 )
				options.Add( new Launch(this, CubeHelper.GetLastPositionInDirection(transform.position, Vector3.right * -1.0f)));	
			//if(CubeHelper.GetLastPositionInDirection(transform.position , Vector3.up).y - Level.Dimension < 10)
				//options.Add( new Launch(this, CubeHelper.GetLastPositionInDirection(transform.position, Vector3.up)));
			
			options.Add( new Move(this, CubeHelper.GetTopPosition(transform.position + Vector3.forward)));
            options.Add( new Move(this, CubeHelper.GetTopPosition(transform.position + Vector3.forward * -1.0f)));
            options.Add( new Move(this, CubeHelper.GetTopPosition(transform.position + Vector3.right)));
            options.Add( new Move(this, CubeHelper.GetTopPosition(transform.position + Vector3.right * -1.0f)));	
			
           for (int i = 0; i < options.Count; i++ )
            {
					
                if(options[i].EndPosition.y - transform.position.y > 1)
                {
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
	
	public void Launch(Vector3 nextPosition){
		Level.Singleton.Entities.Remove(transform.position);
        transform.position = nextPosition;
        Level.Singleton.Entities.Add(transform.position, this);
			
	}
	
}
	
	


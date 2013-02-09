using UnityEngine;
using System.Collections.Generic;

public class HookCube : Cube {

    public override bool IsSelected
    {
        get
        {
            return isSelected;
        }
        set
        {
            base.IsSelected = value;
        }
    }
	public override Command[] Options {
		get {
			List<Command>options = new List<Command>();
            options.AddRange(base.Options);

            Vector3[] positionsAround = CubeHelper.GetPositionsAround(this.transform.position);

            foreach (Vector3 v in positionsAround)
            {
                Vector3 direction = (v - transform.position).normalized;
                try
                {
                    Launch launch = new Launch(this, CubeHelper.GetLastPositionInDirection(this.transform.position, direction));
                    if (launch.EndPosition != this.transform.position)
                    {
                        options.Add(launch);
                    }
                }
                catch (UnityException e)
                {
                }
            }
        	return options.ToArray();
		}			
	}
	
	public void Launch(Vector3 nextPosition){
		Level.Singleton.RemoveEntity(transform.position);
        transform.position = nextPosition;
        Level.Singleton.AddEntity(nextPosition, this);
			
	}
	
}
	
	


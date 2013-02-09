using UnityEngine;
using System.Collections.Generic;

public class IceCube : Cube {
	public override Command[] Options {
		get {
			List<Command> options = new List<Command>();
            Command[] commands = base.Options;
            for (int i = 0; i < commands.Length; i++ )
            {
                if (commands[i].EndPosition.y == transform.position.y)
                {
                    Vector3 direction = (commands[i].EndPosition - transform.position).normalized;
                    try
                    {
                        options.Add(new Slide(this, CubeHelper.GetLastPositionInDirection(transform.position, direction), direction));
                    }
                    catch (UnityException e) {
                        float scale = Mathf.Abs(Vector3.Dot(transform.position, direction) - Level.Dimension);
                        if (scale > 10) {
                            scale -= (Level.Dimension-1);
                        }
                        options.Add(new Slide(this, this.transform.position + direction * scale, direction));
                    }
                }
                else 
                {
                    options.Add(commands[i]);
                }
            }
            return options.ToArray();
		}	
	}
	
	public void Slide(Vector3 endPosition, Vector3 direction){
        Level.Singleton.RemoveEntity(transform.position);
        CubeAnimations.AnimateSlide(gameObject, endPosition);//"SlideEndExecution", new float[] { direction.x, direction.y, direction.z }
        Level.Singleton.AddEntity(endPosition, this);
	}

    public void Break() {
        Destroy(this.gameObject);
    }
}

using UnityEngine;
using System.Collections.Generic;

public class IceCube : Cube {
	public override Command[] Options {
		get {
			List<Command> commands = new List<Command>();
            commands.AddRange(base.Options);
            for (int i = 0; i < commands.Count; i++ )
            {
                if(commands[i].EndPosition.y == transform.position.y){
                    commands.Add(new Slide(this, CubeHelper.GetLastPositionInDirection(transform.position, (commands[i].EndPosition - transform.position).normalized), (commands[i].EndPosition - transform.position).normalized));
                    commands.RemoveAt(i);
                }
            }
            return commands.ToArray();
		}	
	}
	
	public void Slide(Vector3 endPosition, Vector3 direction){
        Level.Singleton.Entities.Remove(transform.position);
        AnimationHelper.AnimateSlide(gameObject, endPosition, 0f);//"SlideEndExecution", new float[] { direction.x, direction.y, direction.z }
       
        Level.Singleton.Entities.Add(transform.position, this);
        Entity hit = CubeHelper.GetEntityInPosition(transform.position + direction.normalized);
        if(hit is RockCube){ //Agregar cubo metal
            Break();       
        }
	}

    private void Break() {
        Destroy(this.gameObject);
    }
}

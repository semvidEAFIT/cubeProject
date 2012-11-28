using UnityEngine;
using System.Collections.Generic;

public class IceCube : Cube {
	public override Command[] Options {
		get {
			List<Command> commands = new List<Command>();
			commands.Add(new Slide(this, GetLastPositionInDirection(transform.position,Vector3.forward)));
            commands.Add(new Slide(this, GetLastPositionInDirection(transform.position, Vector3.forward * -1.0f)));
            commands.Add(new Slide(this, GetLastPositionInDirection(transform.position, Vector3.right)));
            commands.Add(new Slide(this, GetLastPositionInDirection(transform.position, Vector3.right * -1.0f)));

            return commands.ToArray();
		}	
	}
	public Vector3 GetLastPositionInDirection(Vector3 position, Vector3 direction){
			position += direction; 
			bool isFree = !Level.Singleton.Entities.ContainsKey(position) && !(position.x >= Level.Dimension || position.x < 0 || position.z >= Level.Dimension || position.z < 0 );
			while(isFree){
				position += direction; 
				isFree = !Level.Singleton.Entities.ContainsKey(position) && !(position.x >= Level.Dimension || position.x < 0 || position.z >= Level.Dimension || position.z < 0 );
			}
            if(Level.Singleton.Entities.ContainsKey(position)){
			    position -= direction;
            }
		return position;
	}
	
	public void Slide(Vector3 endPosition){
        Level.Singleton.Entities.Remove(transform.position);
		transform.position = endPosition;
        Level.Singleton.Entities.Add(transform.position, this);
	}
}

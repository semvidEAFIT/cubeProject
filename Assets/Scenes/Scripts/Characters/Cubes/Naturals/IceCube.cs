using UnityEngine;
using System.Collections.Generic;

public class IceCube : Cube {
	public override Command[] Options {
		get {
			List<Command> commands = new List<Command>();
			commands.Add(new Slide(this, CubeHelper.GetLastPositionInDirection(transform.position,Vector3.forward), Vector3.forward));
            commands.Add(new Slide(this, CubeHelper.GetLastPositionInDirection(transform.position, Vector3.forward * -1.0f), Vector3.forward * -1.0f));
            commands.Add(new Slide(this, CubeHelper.GetLastPositionInDirection(transform.position, Vector3.right), Vector3.right));
            commands.Add(new Slide(this, CubeHelper.GetLastPositionInDirection(transform.position, Vector3.right * -1.0f), Vector3.right * -1.0f));

            for (int i = 0; i < commands.Count; i++ )
            {
                Command c = commands[i];
                if (c.EndPosition == transform.position && c is Slide)
                {
                    Move move = new Move(this, CubeHelper.GetTopPosition(transform.position + ((Slide)c).Direction));
                    if (move.EndPosition.y - transform.position.y <= 1)
                    {
                        commands.Add(move);
                    }
                    commands.RemoveAt(i);
                }
            }

            return commands.ToArray();
		}	
	}
	
	public void Slide(Vector3 endPosition, Vector3 direction){
        Level.Singleton.Entities.Remove(transform.position);
		transform.position = endPosition;
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

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RubberCube : Cube {
	
	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
	}
	
	public override Command[] Options {
		get {
			
			List<Command> commands = new List<Command>();
			
			SideOptions(commands,Vector3.forward);
			SideOptions(commands,Vector3.back);
			SideOptions(commands,Vector3.right);
			SideOptions(commands,Vector3.left);
			
			Command[] options = new Command[commands.Count];
			for (int i = 0 ; i < commands.Count ; i++){
				options[i] = commands[i];
			}
            return options;
		}
	}
	
	private void SideOptions(List<Command> commands, Vector3 direction){
		Vector3 currentPosition = transform.position + direction;
		if (Level.Singleton.ContainsElement(currentPosition)){
			Entity entity = Level.Singleton.getEntity(currentPosition);
			// Check if cubes exists on top of me
			if (!Level.Singleton.ContainsElement(currentPosition + Vector3.up)){
				commands.Add(new Move(this,currentPosition + Vector3.up));
			}
		}else{
			if (Level.Singleton.ContainsElement(currentPosition + Vector3.down)){
				commands.Add(new Move(this,currentPosition));
			}else{
				int jumpSize = CubeHelper.GetDifferenceInDirection(currentPosition,Vector3.down) - 1;
				Vector3 jumpPosition = currentPosition + (jumpSize * Vector3.down);
				List<Vector3> jumpPositions = new List<Vector3>();
				jumpPositions.Add(jumpPosition);
				
				if (Level.Singleton.ContainsElement(currentPosition + direction)){
					//Cae en el mismo lugar donde salto por su primera vez
					commands.Add(new Bounce(this,jumpPosition,jumpPositions));
				}else{
					int jumpSizeNext = CubeHelper.GetDifferenceInDirection(currentPosition + direction,Vector3.down) - 1;

					//print ("jp" + jumpSize + "," + jumpSizeNext);

					if (jumpSizeNext < jumpSize){
						commands.Add(new Bounce(this,currentPosition + direction + (Vector3.down * jumpSizeNext),jumpPositions));
					}else{
						//Puede seguir reborando
						JumpRecursive(jumpPosition, commands,jumpPositions,direction);
					}
				}
			}
		}
	}
	
	private void JumpRecursive(Vector3 currentPosition, List<Command> commands, List<Vector3> jumpPositions, Vector3 direction)
	{
		Vector3 nextPosition = currentPosition + direction;
		if (Level.Singleton.ContainsElement(nextPosition + Vector3.down)){
			// exite piso osea que salte por ultima vez
			if (!Level.Singleton.ContainsElement(nextPosition)){
				commands.Add(new Bounce(this,nextPosition,jumpPositions));
			}else if (!Level.Singleton.ContainsElement(nextPosition + Vector3.up)){
				commands.Add(new Bounce(this,nextPosition + Vector3.up,jumpPositions));
			}
		}else{
			// Sigue la recursividad
			int jumpSize = CubeHelper.GetDifferenceInDirection(nextPosition,Vector3.down) - 1;
			if (jumpSize >= 0){
				Vector3 jumpPosition = nextPosition + Vector3.down *jumpSize ; // changed multiply

			    jumpPositions.Add(jumpPosition);
				JumpRecursive(jumpPosition,commands,jumpPositions,direction);
			}
		}
	}
	
	public void Bounce(Vector3 endPosition, List<Vector3> bouncePositions){
		Level.Singleton.RemoveEntity(transform.position);
		bouncePositions.Add(endPosition);
		Vector3[] positions = new Vector3[bouncePositions.Count];
		for (int i = 0 ; i < bouncePositions.Count ; i++){
			print (bouncePositions[i]);
			positions[i] = bouncePositions[i];
		}
		CubeAnimations.AnimateBounce(gameObject,Vector3.down,positions);
        Level.Singleton.AddEntity(endPosition, this);
	}
	
	public  void Bounce(Vector3 nextPosition) {
        Level.Singleton.RemoveEntity(transform.position);
        transform.position = nextPosition;
        Level.Singleton.AddEntity(transform.position, this);
    }
	
}

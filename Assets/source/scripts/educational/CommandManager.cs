using UnityEngine;
using System.Collections;

public class CommandManager:Cube {
	
	
	
	private Vector3 nextPosition = Vector3.forward; 
		
	protected void foward(){
		Level.Singleton.RemoveEntity(transform.position);
        CubeAnimations.AnimateMove(gameObject, Vector3.down, nextPosition);
        Level.Singleton.AddEntity(FindNextPosition(), this);
	}
	
	private Vector3 FindNextPosition(){
		return CubeHelper.GetTopPosition(transform.position + nextPosition);
	}
	
	protected void backward(){
		nextPosition = nextPosition*-1;
	}
	
	protected void turnLeft(){
		nextPosition = Vector3.Cross( Vector3.down,nextPosition );
	}
	
	protected void turnRight(){
		nextPosition = Vector3.Cross( Vector3.up,nextPosition );
	}
}

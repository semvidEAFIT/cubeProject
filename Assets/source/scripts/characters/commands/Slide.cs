using UnityEngine;
using System.Collections;

public class Slide : Command {
    private Vector3 direction;

    public Vector3 Direction
    {
        get { return direction; }
    }
	public Slide(IceCube receiver, Vector3 endPosition, Vector3 direction): base(receiver, endPosition){
        this.direction = direction;
    }

    public override void Execute()
    {
        ((IceCube)Cube).Slide(EndPosition, direction);
        //EndExecution();
    }

    public override void EndExecution()
    {
         Entity hit = CubeHelper.GetEntityInPosition(EndPosition + direction.normalized);
        if(hit is RockCube || hit is MetalCube){ 
            ((IceCube)Cube).Break();       
        }
        base.EndExecution();
    }
}

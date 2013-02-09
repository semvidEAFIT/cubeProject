using UnityEngine;
using System.Collections;

public class Launch : Command {

     public Launch(HookCube receiver, Vector3 endPosition) : base(receiver, endPosition){
        
    }

    public override void Execute()
    {
        ((HookCube)Cube).Launch(EndPosition);
        EndExecution();
    }
}

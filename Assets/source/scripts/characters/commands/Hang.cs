using UnityEngine;
using System.Collections;

public class Hang : Command {

    public Hang(CloudCube receiver, Vector3 endPosition) : base(receiver, endPosition){
        
    }

    public override void Execute()
    {
        ((CloudCube)Cube).Hang(EndPosition);
    }
}

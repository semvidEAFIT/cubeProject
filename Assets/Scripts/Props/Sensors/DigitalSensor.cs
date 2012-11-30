using UnityEngine;
using System.Collections;

public class DigitalChip : Sensor {

    /**
     * Puede hacerse un metodo recursivo que verifique si todas las piezas estan conectadas
     * */
    public override bool CheckPressed()
    {
        Entity[] around = CubeHelper.GetEntitiesAround(this.transform.position);
        foreach(Entity e in around){
            if(e is DigitalCube){
                return true;
            }
        }
        return false;
    }
}

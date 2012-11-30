using UnityEngine;
using System.Collections.Generic;

public abstract class WallWalkingCube :  Cube{

    //Usado para saber si se puede soltar
    private bool canDrop;

    protected abstract bool IsDropable();

    void Awake(){
        canDrop = IsDropable();
    }

    public override Command[] Options
    {
        get
        {
            List<Command> options = new List<Command>();
            Vector3[] posiblePositions = CubeHelper.GetPositionsAround(this.transform.position);
            foreach(Vector3 v in posiblePositions){
                if (CubeHelper.IsFree(v))
                {
                    if(HasFloorAround(v)){
                        options.Add(new Move(this, v));
                    }
                    else
                    {
                        if (v.x != this.transform.position.x || v.z != this.transform.position.z)
                        {
                            options.Add(new Move(this, CubeHelper.GetTopPosition(v)));
                        }
                    }
                }
                else 
                {
                    CubeHelper.GetEntityInPosition(v);
                    if(IsWalkable(CubeHelper.GetEntityInPosition(v))){
                        foreach(Vector3 availablePosition in GetAvaiblePositionsRelatedTo(v)){
                            options.Add(new Move(this, availablePosition));
                        }           
                    }
                }
            }
            Vector3 below = CubeHelper.GetTopPosition(this.transform.position);
            if(canDrop && this.transform.position.y-below.y > 0){
                options.Add(new Move(this, below));
            }
            return options.ToArray();
        }
    }

    protected abstract bool IsWalkable(Entity e);

    private Vector3[] GetAvaiblePositionsRelatedTo(Vector3 direction) {
        List<Vector3> availablePositions = new List<Vector3>();
        if (IsWalkable(CubeHelper.GetEntityInPosition(transform.position + direction)))
        {
            Vector3 restComponents = Vector3.one - new Vector3(direction.normalized.x, direction.normalized.y, direction.normalized.z);
            if (restComponents.x == 1)
            {
                if (CubeHelper.IsFree(transform.position + direction + Vector3.right))
                {
                    availablePositions.Add(transform.position + direction + Vector3.right);
                }
                if (CubeHelper.IsFree(transform.position + direction + Vector3.left))
                {
                    availablePositions.Add(transform.position + direction + Vector3.left);
                }
            }
            if (restComponents.y == 1)
            {
                if (CubeHelper.IsFree(transform.position + direction + Vector3.up))
                {
                    availablePositions.Add(transform.position + direction + Vector3.up);
                }
                if (CubeHelper.IsFree(transform.position + direction + Vector3.down))
                {
                    availablePositions.Add(transform.position + direction + Vector3.down);
                }
            }
            if (restComponents.z == 1)
            {
                if (CubeHelper.IsFree(transform.position + direction + Vector3.forward))
                {
                    availablePositions.Add(transform.position + direction + Vector3.forward);
                }
                if (CubeHelper.IsFree(transform.position + direction + Vector3.back))
                {
                    availablePositions.Add(transform.position + direction + Vector3.back);
                }
            }
        }
        return availablePositions.ToArray();
    }

    private bool HasFloorAround(Vector3 v) {
        Entity[] around = CubeHelper.GetEntitiesAround(v);
        foreach(Entity e in around){
            if(IsWalkable(e)){
                return true;
            }
        }
        return false;
    }
}

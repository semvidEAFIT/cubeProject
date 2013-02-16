using UnityEngine;
using System.Collections.Generic;

public abstract class WallWalkingCube :  Cube{

    //Usado para saber si se puede soltar
    private bool canDrop;

    protected abstract bool IsDropable();

    public override bool IsSelected
    {
        get
        {
            return selected;
        }
        set
        {
            base.IsSelected = value;
        }
    }
    void Awake(){
        canDrop = IsDropable();
    }

    public override Command[] Options
    {
        get
        {
            Debug.Log(CubeHelper.IsFree(transform.position + Vector3.up));
            List<Command> options = new List<Command>();
            Vector3[] posiblePositions = CubeHelper.GetPositionsAround(this.transform.position);
            foreach(Vector3 v in posiblePositions){
                if (CubeHelper.IsFree(v))
                {
                    Vector3 floorDirection = HasFloorAround(v);
                    if(floorDirection != Vector3.zero){
                        options.Add(new MagneticMove(this, v, floorDirection));
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
                    foreach(Vector3 availablePosition in GetAvaiblePositionsRelatedTo((v - transform.position).normalized)){
                        options.Add(new MagneticMove(this, availablePosition, (availablePosition - v).normalized));
                    }           
                }
            }

            //Remuevo los arrojes que pasan por un magneto inmediatamente
            foreach(Command c in base.Options){
                bool alreadyIn = false;
                foreach(Command option in options){
                    if (c.EndPosition.x == option.EndPosition.x && c.EndPosition.z == option.EndPosition.z)
                    {
                        alreadyIn = true;
                        break;
                    }
                }
                if(!alreadyIn){
                    options.Add(c);
                }
            }

            Vector3 below = CubeHelper.GetLastPositionInDirection(this.transform.position, Vector3.down);
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

    private Vector3 HasFloorAround(Vector3 v) {
        Entity[] around = CubeHelper.GetEntitiesAround(v);
        foreach(Entity e in around){
            if(e.transform.position!= this.transform.position && IsWalkable(e)){
                return (e.transform.position - this.transform.position).normalized;
            }
        }
        return Vector3.zero;
    }

    public void MagneticMoveTo(Vector3 nextPosition, Vector3 floorDirection)
    {
        Level.Singleton.RemoveEntity(transform.position);
        CubeAnimations.AnimateMove(gameObject, floorDirection, nextPosition);
        Level.Singleton.AddEntity(nextPosition,this);
    }
}

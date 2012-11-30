using UnityEngine;
using System.Collections.Generic;

public class Cube : Entity, IClickable{
	
    private bool isSelected = false;
    private static Cube selectedCube;

    public bool IsSelected
    {
        get { return isSelected && CubeHelper.IsFree(transform.position + Vector3.up); }
        set { isSelected = value; }
    }

    public virtual void MoveTo(Vector3 nextPosition) {
        Level.Singleton.Entities.Remove(transform.position);
        transform.position = nextPosition;
        Level.Singleton.Entities.Add(transform.position, this);
    }

    public virtual Command[] Options{ 
        get {
            List<Command> options = new List<Command>();
            options.Add(new Move(this, CubeHelper.GetTopPosition(transform.position+Vector3.forward)));
            options.Add(new Move(this, CubeHelper.GetTopPosition(transform.position + Vector3.forward * -1.0f)));
            options.Add(new Move(this, CubeHelper.GetTopPosition(transform.position + Vector3.right)));
            options.Add(new Move(this, CubeHelper.GetTopPosition(transform.position + Vector3.right * -1.0f)));

            for (int i = 0; i < options.Count; i++ )
            {
				
                if(options[i].EndPosition.y - transform.position.y > 1)
                {
                    options.RemoveAt(i);
                }
            }

            return options.ToArray();
        }
    }

    public void FallOutOfBounds()
    {
        Destroy(gameObject);
    }

    public void NotifyClick()
    {
        if (selectedCube != this)
        {
            if(selectedCube != null){
                selectedCube.IsSelected = false;
            }
            isSelected = true;
            selectedCube = this;
        }
    }
}

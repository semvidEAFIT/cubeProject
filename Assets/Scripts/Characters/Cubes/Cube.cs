using UnityEngine;
using System.Collections.Generic;

public class Cube : Entity, IClickable{
	
    private bool isSelected = false;
    private static Cube selectedCube;
	public Command command;

	public Command Command {
		get {
			return this.command;
		}
		set {
			command = value;
		}
	}
    public bool IsSelected
    {
        get { return isSelected; }
        set { isSelected = value; }
    }

    protected override void Start() {
        base.Start();
    }

    public virtual void MoveTo(Vector3 nextPosition) {
        Level.Singleton.Entities.Remove(transform.position);
		AnimationHelper.AnimateJump2(gameObject,Vector3.down,nextPosition,0f,"EndExecution",null);
        Level.Singleton.Entities.Add(nextPosition, this);
    }
	
	public void EndExecution(){
		OrganizeTransform();
		command.EndExecution();
	}
	
	private Vector3 Vector3Round(Vector3 v){
		return new Vector3(Mathf.Round(v.x),Mathf.Round(v.y),Mathf.Round(v.z));
	}
	
	public void OrganizeTransform(){
		Transform obj = gameObject.transform.parent;
		transform.parent = null;
		if (obj != null){
			MonoBehaviour.Destroy(obj.gameObject);
		}
		transform.position = Vector3Round(transform.position);
		transform.rotation = Quaternion.Euler(Vector3Round(transform.rotation.eulerAngles));
	}
	
    public virtual Command[] Options{ 
        get {
            List<Command> options = new List<Command>();
            options.Add(new Move(this, GetTopPosition(Vector3.forward)));
            options.Add(new Move(this, GetTopPosition(Vector3.forward * -1.0f)));
            options.Add(new Move(this, GetTopPosition(Vector3.right)));
            options.Add(new Move(this, GetTopPosition(Vector3.right * -1.0f)));

            for (int i = 0; i < options.Count; i++ )
            {
				
                if(options[i].EndPosition.y - transform.position.y > 1)
                {
                    options.RemoveAt(i);
                }
                else
                {
                    if (options[i].EndPosition.x >= Level.Dimension || options[i].EndPosition.x < 0 || options[i].EndPosition.z >= Level.Dimension || options[i].EndPosition.z < 0)
                    {
                        options[i] = new OutOfBounds(this, options[i].EndPosition);
                    }
                }
            }

            return options.ToArray();
        }
    }

    public virtual Vector3 GetTopPosition(Vector3 direction)
    {
        Vector3 nextPosition = transform.position + direction.normalized;
        bool isFree = !Level.Singleton.Entities.ContainsKey(nextPosition);
        if (!isFree)
        {
            while (!isFree) {
                nextPosition.y += 1.0f;
                isFree = !Level.Singleton.Entities.ContainsKey(nextPosition);
            }
        }
        else
        {
            while (isFree) {
                nextPosition.y -= 1.0f;
                isFree = !Level.Singleton.Entities.ContainsKey(nextPosition);
                if(nextPosition.y < 0){
                    break;
                }
            }
            nextPosition.y += 1.0f;
        }

        return nextPosition;
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

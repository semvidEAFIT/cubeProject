using UnityEngine;
using System.Collections.Generic;

public class Cube : Entity, IClickable{
	
	/// <summary>
	/// The cube that is selected in the whole world.
	/// </summary>
	private static Cube selectedCube;
	
	/// <summary>
	/// The cube is selected.
	/// </summary>
    protected bool selected = false;
	
    /// <summary>
    /// The current command that is executing, if its null then there.
    /// </summary>
	public Command command;
	
	/// <summary>
	/// Moves Cube to the direction .
	/// </summary>
	/// <param name='nextPosition'>
	/// Next position.
	/// </param>
    public virtual void MoveTo(Vector3 nextPosition) {
        Level.Singleton.Entities.Remove(Vector3Int.getVector(transform.position));
		CubeAnimations.AnimateMove(gameObject, Vector3.down, nextPosition);
        Level.Singleton.Entities.Add(Vector3Int.getVector(nextPosition), this);
    }
	
    public virtual Command[] Options{ 
        get {
            List<Command> options = new List<Command>();
            options.Add(new Move(this, CubeHelper.GetTopPosition(transform.position + Vector3.forward)));
            options.Add(new Move(this, CubeHelper.GetTopPosition(transform.position + Vector3.back)));
            options.Add(new Move(this, CubeHelper.GetTopPosition(transform.position + Vector3.right)));
            options.Add(new Move(this, CubeHelper.GetTopPosition(transform.position + Vector3.left)));

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
	
	public void EndExecution(){
		OrganizeTransform();
		if(command != null){
			command.EndExecution();
		}
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
	
    public void FallOutOfBounds(Vector3 outOfBouncePosition)
    {
		CubeAnimations.AnimateSlide(gameObject,outOfBouncePosition + new Vector3(0,-10,0), "KillCube", null);
    }
	
	public void KillCube(){
		Destroy(gameObject);
	}
	
    public void NotifyClick()
    {
        if (selectedCube != this)
        {
            if(selectedCube != null){
                selectedCube.IsSelected = false;
            }
            selected = true;
            selectedCube = this;
        }
    }
	
	public virtual void Update(){
	}
	
	#region Get and Sets
	public Command Command {
		get {
			return this.command;
		}
		set {
			command = value;
		}
	}
	
	public virtual bool IsSelected
    {
        get { return selected && CubeHelper.IsFree(transform.position + Vector3.up); }
        set { selected = value; }
    }
	#endregion
}

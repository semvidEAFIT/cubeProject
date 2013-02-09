using UnityEngine;
using System.Collections.Generic;

public class Cube : Entity, IClickable{
	
	private string[] sounds = new string[]{
		"Sounds/Effects/Characters/Bloque 1",
		"Sounds/Effects/Characters/Bloque 2",
		"Sounds/Effects/Characters/Bloque 3",
		"Sounds/Effects/Characters/Bloque 4",
		"Sounds/Effects/Characters/Bloque 5"
	};
	
    protected bool isSelected = false;
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
	
    public virtual bool IsSelected
    {
        get { return isSelected && CubeHelper.IsFree(transform.position + Vector3.up); }
        set { isSelected = value; }
    }

    public virtual void MoveTo(Vector3 nextPosition) {
        Level.Singleton.Entities.Remove(transform.position);
		CubeAnimations.AnimateMove(gameObject, Vector3.down, nextPosition);
        Level.Singleton.Entities.Add(nextPosition, this);
    }
	
	public void EndExecution(){
		OrganizeTransform();
		if(command != null){
			command.EndExecution();
		}
		AudioSource ac = GetComponent<AudioSource>();
		ac.clip = Resources.Load(sounds[Random.Range(0,sounds.Length -1)]) as AudioClip;
		ac.Play();
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
            isSelected = true;
            selectedCube = this;
        }
    }
	
}

using UnityEngine;
using System.Collections.Generic;

public class LevitatorCube : Cube {

	private bool isLevitating = false;
	public Vector3 CurrentDirection
	{
		get;
		set;
	}
	
	protected override void Start () {
		base.Start();
		CurrentDirection = Vector3.zero;
	}
	
	public override Command[] Options {
		get {
			if(!Level.Singleton.ContainsElement(transform.position + (Vector3.down)))
			{
				isLevitating = true;
			}
			else
			{
				isLevitating = false;
			}
			
            List<Command> options = new List<Command>();
			
			if(!isLevitating){
				options.Add(new Move(this, GetTopPosition(Vector3.forward)));
				options.Add(new Move(this, GetTopPosition(Vector3.back)));
				options.Add(new Move(this, GetTopPosition(Vector3.right)));
				options.Add(new Move(this, GetTopPosition(Vector3.left)));
				CurrentDirection = Vector3.zero;
			}
			else
			{
				options.Add(new Move(this, GetTopPosition(Vector3.down)));
			}
			
			if(canLevitate(transform.position, Vector3.forward) 
				&& (CurrentDirection.Equals(Vector3.zero) || CurrentDirection.Equals(Vector3.forward)))
			{
				options.Add(new Levitate(this, transform.localPosition + Vector3.forward, Vector3.forward));
			}
			
			if(canLevitate(transform.position, Vector3.back) 
				&& (CurrentDirection.Equals(Vector3.zero) || CurrentDirection.Equals(Vector3.back)))
			{
				options.Add(new Levitate(this, transform.localPosition + Vector3.back, Vector3.back));
			}
			
			if(canLevitate(transform.position, Vector3.right) 
				&& (CurrentDirection.Equals(Vector3.zero) || CurrentDirection.Equals(Vector3.right)))
			{
				options.Add(new Levitate(this, transform.localPosition + Vector3.right, Vector3.right));
			}
			
			if(canLevitate(transform.position, Vector3.left) 
				&& (CurrentDirection.Equals(Vector3.zero) || CurrentDirection.Equals(Vector3.left)))
			{
				options.Add(new Levitate(this, transform.localPosition + Vector3.left, Vector3.left));
			}

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
	
	public Vector3 GetTopPosition (Vector3 direction)
	{
		Vector3 nextPosition = transform.position + direction.normalized;
        bool isFree = !Level.Singleton.ContainsElement(nextPosition);
        if (!isFree)
        {
            while (!isFree && !isLevitating) {
                nextPosition.y += 1.0f;
                isFree = !Level.Singleton.ContainsElement(nextPosition);
            }
        }
        else
        {
            while (isFree) {
                nextPosition.y -= 1.0f;
                isFree = !Level.Singleton.ContainsElement(nextPosition);
                if(nextPosition.y < 0){
                    break;
                }
            }
            nextPosition.y += 1.0f;
        }
        return nextPosition;
	}
	
	public bool canLevitate(Vector3 position, Vector3 direction)
	{
		Vector3 nextPosition = position + direction;
		
		return !Level.Singleton.ContainsElement(nextPosition) 
			&& !Level.Singleton.ContainsElement(nextPosition + (Vector3.up * -1f));
	}
	
	public void Levitate(Vector3 nextPosition)
	{
		Level.Singleton.RemoveEntity(transform.position);
		AnimationHelper.AnimateJump2(gameObject,Vector3.down,nextPosition,0f,"EndExecution",null);
        Level.Singleton.AddEntity(nextPosition, this);
	}
}

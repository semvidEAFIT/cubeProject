using UnityEngine;
using System.Collections;

public class RubberCube : Cube {
	
	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	public override Command[] Options {
		get {
			Command[] options = new Command[4];
			if((transform.position.y > GetTopPosition(Vector3.forward).y) 
				&& (transform.position.y >= GetNextBouncePosition(Vector3.forward).y))
			{
            	options[0] = new Bounce(this, GetNextBouncePosition(Vector3.forward), GetTopPosition(Vector3.forward));
			}
			else
			{
				options[0] = new Move(this, GetTopPosition(Vector3.forward));
			}
			
			if((transform.position.y > GetTopPosition(Vector3.forward * -1.0f).y) 
				&& (transform.position.y >= GetNextBouncePosition(Vector3.forward * -1.0f).y))
			{
				options[1] = new Bounce(this, GetNextBouncePosition(Vector3.forward * -1.0f), GetTopPosition(Vector3.forward * -1.0f));
			}
			else
			{
				options[1] = new Move(this, GetTopPosition(Vector3.forward * -1.0f));
			}
            
			if((transform.position.y > GetTopPosition(Vector3.right).y) 
				&& (transform.position.y >= GetNextBouncePosition(Vector3.right).y))
			{
				options[2] = new Bounce(this, GetNextBouncePosition(Vector3.right), GetTopPosition(Vector3.right));
			}
			else
			{
				options[2] = new Move(this, GetTopPosition(Vector3.right));
			}
			
			if((transform.position.y > GetTopPosition(Vector3.right * -1.0f).y) 
				&& (transform.position.y >= GetNextBouncePosition(Vector3.right * -1.0f).y))
			{
				options[3] = new Bounce(this, GetNextBouncePosition(Vector3.right * -1.0f), GetTopPosition(Vector3.right * -1.0f));
			}
            else
			{
				options[3] = new Move(this, GetTopPosition(Vector3.right * -1.0f));
			}

            for (int i = 0; i < 4; i++ )
            {
                if (options[i].EndPosition.x >= Level.Dimension || options[i].EndPosition.x < 0 || options[i].EndPosition.z >= Level.Dimension || options[i].EndPosition.z < 0)
                {
                    options[i] = new OutOfBounds(this, options[i].EndPosition);
                }
            }
            Debug.Log(options[0] + "" +options[0].EndPosition);

            return options;
		}
	}
	
	public override Vector3 GetTopPosition (Vector3 direction)
	{
        Vector3 nextPosition = transform.position + direction.normalized;
		
        bool isFree = !Level.Singleton.Entities.ContainsKey(nextPosition);
        if (!isFree)
        {
            while (!isFree) 
			{
                nextPosition.y += 1.0f;
                isFree = !Level.Singleton.Entities.ContainsKey(nextPosition);
            }
        }
        else
        {
            while (isFree) 
			{
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
	
	public Vector3 GetNextBouncePosition(Vector3 direction)
	{
		Vector3 nextBouncePosition = transform.position + 2 * direction.normalized;
		
		bool isFreeToBounce = !Level.Singleton.Entities.ContainsKey(nextBouncePosition);
		
		if (isFreeToBounce)
        {
            while (isFreeToBounce) 
			{
                nextBouncePosition.y -= 1.0f;
                isFreeToBounce = !Level.Singleton.Entities.ContainsKey(nextBouncePosition);
                if(nextBouncePosition.y < 0)
				{
                    break;
                }
            }
            nextBouncePosition.y += 1.0f;
        }
		else return transform.position;
		
		return nextBouncePosition;
	}
}

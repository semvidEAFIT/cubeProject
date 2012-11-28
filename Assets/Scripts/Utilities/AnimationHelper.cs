using UnityEngine;
using System.Collections;

public class AnimationHelper : MonoBehaviour{ //TODO quitar Monobehavior
	
	public static float offset = 0.4f;
	public static GameObject originTemp; // Its used to change the origin of the gameObject
	public static GameObject rotationGameObject;
	
	void Start(){
		// Animation test code
		AnimateJump(gameObject,new Vector3(0,-1,0),transform.position + new Vector3(1,-1,0),0f,"Seguir","hola");

		//AnimateJump2(gameObject,new Vector3(0,-1,0),transform.position + new Vector3(1,0,0),0f,"Test");
	}
	
	void Seguir(object vals){
		print (vals);
		AnimateJump(gameObject,new Vector3(0,-1,0),transform.position + new Vector3(1,-1,0),0f);
	}
	
	
	private static Hashtable getBasicHs(Vector3 amount,float time, float delay, iTween.EaseType easeType){
       Hashtable hs = new Hashtable();
       hs.Add("amount",amount);
       hs.Add("time",time);
       hs.Add("delay",delay);
       hs.Add("space",Space.World);
       hs.Add("easetype",easeType.ToString());
       return hs;
   }
	
	#region Animate Jumps
	public static void AnimateJump(GameObject objective, Vector3 down, Vector3 finalPosition, float delay){
		AnimateJump(objective,down,finalPosition,delay,null,null);
	}
	
	/// <summary>
    /// Animate
    /// </summary>
    /// <returns>
    /// Time spend on animation plus delay
    /// </returns>
    /// <param>
    /// <param name='objective'>
    /// GameObject to
    /// </param>
    /// <param name='down'>
    /// Down unitary movement
    /// </param>
    /// <param name='finalPosition'>
    /// Final position.
    /// </param>
    /// </param>
    /// <param name='delay'>
    /// Delay of the animation
    /// </param>
    ///
    public static void AnimateJump(GameObject objective, Vector3 down, Vector3 finalPosition, float delay, string onCompleteMethod, object parameters){
        
		Vector3 vertical = Vector3.zero;
		Vector3 finalVertical = Vector3.zero;
		Vector3 finalMovement = finalPosition - objective.transform.position;
		Vector3 directionAxis = -down;// movement is countrary to down
		Vector3 upMovement = Vector3.Dot(directionAxis, finalMovement) * directionAxis;
        Vector3 vectorOffset = (offset * directionAxis);
		Vector3 sideMovement =(finalMovement - upMovement);
		Vector3 rotate = Vector3.Cross(sideMovement.normalized,down) * 90;
		
		if (Vector3.Dot(down,upMovement) < 0){
			// Jumping
			vertical = vectorOffset + upMovement;
			finalVertical = -vectorOffset;
		} else if (Vector3.Dot(down,upMovement) > 0){
			// Going Down
			vertical = vectorOffset;
			finalVertical = upMovement - vertical;
		} else {
			// Moving to the side
			vertical = vectorOffset;
			finalVertical = -vertical;
		}
		
        Hashtable hs = getBasicHs(vertical, 0.5f, delay, iTween.EaseType.spring);
        iTween.MoveAdd(objective, hs);
        delay += 0.5f;
		
        hs = getBasicHs(sideMovement,0.5f,delay, iTween.EaseType.spring);
        iTween.MoveAdd(objective,hs);
        delay += 0.5f;
		
        hs = getBasicHs(rotate, 0.3f, delay, iTween.EaseType.spring);
        iTween.RotateAdd(objective, hs);
        delay += 0.3f;
		
        hs = getBasicHs(finalVertical, 0.5f, delay, iTween.EaseType.easeInBack);
		if (onCompleteMethod != null && onCompleteMethod.Length > 0){
			hs.Add("onComplete",onCompleteMethod);
			hs.Add("onCompleteTarget",objective);
			if (parameters != null){
				hs.Add("oncompleteparams",parameters);
			}
		}
        iTween.MoveAdd(objective,hs);
        delay += 0.5f;
    }
	
	public static void AnimateJump2(GameObject objective, Vector3 down, Vector3 finalPosition, float delay){
		AnimateJump2(objective,down,finalPosition,delay,null,null);
	}
	
	/// <summary>
	/// Animates the jump2.
	/// </summary>
	/// <param name='objective'>
	/// Objective. GameObject that will be animated
	/// </param>
	/// <param name='down'>
	/// Down. The direction where the cube is stepping on
	/// </param>
	/// <param name='finalPosition'>
	/// Final position. Where the cube wants to end up
	/// </param>
	/// <param name='delay'>
	/// Delay. The time delay if you are connecting more than one animation
	/// </param>
	/// <param name='onCompleteMethod'>
	/// On complete method. If you want to continue or add logic after an animation
	/// place the name of the method on this parameter, if you dont just leave it null or blank.
	/// </param>
	public static void AnimateJump2(GameObject objective, Vector3 down, Vector3 finalPosition, float delay, string onCompleteMethod, object parameters){
		Vector3 finalMovement = finalPosition - objective.transform.position;
        Vector3 directionAxis = -down;// movement is countrary to down
        Vector3 upMovement = Vector3.Dot(directionAxis, finalMovement) * directionAxis;// MoveDirection*Quantity + offset
        Vector3 sideDirection = (finalMovement - upMovement).normalized;
        Vector3 rotationAxis = Vector3.Cross(sideDirection,down) * 90;
        Vector3 sideMovement = finalMovement - upMovement;
		
		// Sets the new Origin to rotate around an specific Axis - originTemp
		originTemp = new GameObject("temp");
		originTemp.transform.position = objective.transform.position + (down + sideDirection)/2f;
		objective.transform.parent = originTemp.transform;
		rotationGameObject = objective;
		
		Hashtable hs;
		
		if (Vector3.Dot(down,upMovement) < 0){
			// Jumps
			hs = getBasicHs(upMovement,0.5f,delay,iTween.EaseType.spring);
			iTween.MoveAdd(originTemp,hs);
			delay = delay + 0.5f;
		}
		// Rotates
		hs = getBasicHs(rotationAxis,0.5f,delay,iTween.EaseType.spring);
		hs.Add("onComplete","RotationFinished");
		hs.Add("onCompleteTarget",originTemp);
		iTween.RotateAdd(originTemp,hs);
		delay = delay + 0.5f;
		
		if (Vector3.Dot(down,upMovement) > 0){
			// Goes Down
			hs = getBasicHs(upMovement,0.5f,delay,iTween.EaseType.spring);
			iTween.MoveAdd(objective,hs);
			delay = delay + 0.5f;
		}
		if (onCompleteMethod != null && onCompleteMethod.Length > 0){
			hs = getBasicHs(Vector3.zero,0.0f,delay,iTween.EaseType.spring);
			hs.Add("onComplete",onCompleteMethod);
			hs.Add("onCompleteTarget",objective);
			if (parameters != null){
				hs.Add("oncompleteparams",parameters);
			}
			iTween.MoveAdd(objective,hs);
		}
	}
	
	#endregion
	
	/// <summary>
	/// Method that is called once the originTemp is finished rotating
	/// The idea is to remove rotationGameObject from its parent originTemp
	/// to bring it back to its normal state
	/// </summary>
	public static void RotationFinished(){
		if (originTemp != null){
			rotationGameObject.transform.parent = null;
			rotationGameObject = null;
			MonoBehaviour.Destroy(originTemp);
		}
	}

  /// <summary>
   /// Animates the slide movement of a cube.
   /// </summary>
   /// <returns>
   /// Time spent on animation plus delay
   /// </returns>
   /// <param name='gameObject'>
   /// Game object of the cube.
   /// </param>
   /// <param name='finalPosition'>
   /// Final position of the slide.
   /// </param>
   /// <param name='delay'>
   /// Total Delay. It takes into acount the delay parameter
   /// </param>
   public static float AnimateSlide(GameObject gameObject, Vector3 finalPosition, float delay){
       Vector3 direction = finalPosition - gameObject.transform.position;
       float time = direction.magnitude * 0.2f;
       Hashtable hs = getBasicHs(direction, time, delay, iTween.EaseType.spring);
       iTween.MoveAdd(gameObject,hs);
       return delay + time;
   }
	
}
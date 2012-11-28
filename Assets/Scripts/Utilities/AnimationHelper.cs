using UnityEngine;
using System.Collections;

public class AnimationHelper{
	
	float offset = 0.4f;
	GameObject originTemp; // Its used to change the origin of the gameObject

	private Hashtable getBasicHs(Vector3 amount,float time, float delay, iTween.EaseType easeType){
       Hashtable hs = new Hashtable();
       hs.Add("amount",amount);
       hs.Add("time",time);
       hs.Add("delay",delay);
       hs.Add("space",Space.World);
       hs.Add("easetype",easeType.ToString());
       return hs;
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
    float AnimateJump(GameObject objective, Vector3 down, Vector3 finalPosition, float delay){
       
        Vector3 finalMovement = finalPosition - objective.transform.position;
        Vector3 directionAxis = -down;// movement is countrary to down
        Vector3 vectorOffset = (offset * directionAxis);
        Vector3 upMovement = Vector3.Dot(directionAxis, finalMovement) * directionAxis + vectorOffset;// MoveDirection*Quantity + offset
        Vector3 sideDirection = (finalMovement - upMovement + vectorOffset).normalized;
        Vector3 rotationAxis = Vector3.Cross(sideDirection,down) * 90;
        Vector3 sideMovement = finalMovement - upMovement + vectorOffset;
       
        print (sideDirection);
        Hashtable hs = getBasicHs(upMovement, 0.5f, delay, iTween.EaseType.spring);
        iTween.MoveAdd(objective, hs);
           
        delay += 0.5f;
        hs = getBasicHs(sideMovement,0.5f,delay, iTween.EaseType.spring);
        iTween.MoveAdd(objective,hs);
       
        delay += 0.5f;
        hs = getBasicHs(rotationAxis, 0.3f, delay, iTween.EaseType.spring);
        iTween.RotateAdd(objective, hs);
       
        delay += 0.3f;
        hs = getBasicHs(down*offset, 0.5f, delay, iTween.EaseType.easeInBack);
        iTween.MoveAdd(objective,hs);
       
        delay += 0.5f;
       
        return delay;
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
   float AnimateSlide(GameObject gameObject, Vector3 finalPosition, float delay){
       Vector3 direction = finalPosition - gameObject.transform.position;
       float time = direction.magnitude * 0.2f;
       Hashtable hs = getBasicHs(direction, time, delay, iTween.EaseType.spring);
       iTween.MoveAdd(gameObject,hs);
       return delay + time;
   }
	
}

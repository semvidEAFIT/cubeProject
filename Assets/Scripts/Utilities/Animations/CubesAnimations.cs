using UnityEngine;
using System.Collections;

public class CubesAnimations : MonoBehaviour
{
	#region Diplicator Cube Animations
	
	public static void AnimateDuplication (GameObject myGameObject)
	{
		//AnimationHelper.AnimateJump2(gameObject,Vector3.down,nextPosition,0f,"EndExecution",null);
		myGameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		AnimationHelper.AnimateGrow(myGameObject, 0f, "EndExecution", myGameObject);
		//AnimationHelper.AnimateGrow(myGameObject, 0f, "EndExecution", null);

	}
	
	#endregion
}

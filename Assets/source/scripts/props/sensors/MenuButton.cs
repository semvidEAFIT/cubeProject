using UnityEngine;
using System.Collections;

public class MenuButton : NormalPushButton {
    public string sceneName;
    
    protected override void Update()
    {
        if(CheckPressed()){
            if(sceneName == "Exit"){
				Application.Quit();
			}else{
				if(sceneName == "Random"){
					Application.LoadLevel(UnityEngine.Random.Range(1,Application.levelCount-2));
				}else{
					Application.LoadLevel(sceneName);
				}
			}
        }
		//Debug.DrawRay(this.transform.position, transform.up);
    }
	
	public override bool CheckPressed ()
	{
		return !CubeHelper.IsFree(transform.position + Vector3.up);
	}
}

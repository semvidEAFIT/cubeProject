using UnityEngine;
using System.Collections;

public class MenuButton : NormalPushButton {
    public string sceneName;
    
    protected override void Update()
    {
        if(CheckPressed()){
            GameController.Singleton.LoadScene(sceneName);
        }
    }
}

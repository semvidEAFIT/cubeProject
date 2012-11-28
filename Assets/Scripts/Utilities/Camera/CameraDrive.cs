using UnityEngine;
using System.Collections;

public class CameraDrive : MonoBehaviour {
	
	public GameObject cubo;
	public Vector3 cubop;
	public float speedRot;
	public float speedMov;
	
	protected virtual void Start () {
		cubop=cubo.transform.position;
		cubop.Set(cubop.x,cubop.y+5,cubop.z);
	}
	
	
	protected virtual void Update () {
		camera.transform.LookAt(cubop);
		if(Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow)){
			camera.transform.RotateAround(cubo.transform.position,new Vector3(0,-1,0),speedRot*Time.deltaTime);
		} else if(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow)){
			camera.transform.RotateAround(cubo.transform.position,new Vector3(0,1,0),speedRot*Time.deltaTime);
		}
		
		
		if(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow)){
			cubop.Set(cubop.x,cubop.y+(speedMov)*Time.deltaTime,cubop.z);
			camera.transform.Translate(0,speedMov*Time.deltaTime,0,cubo.transform);
		} else if(camera.transform.position.y-10>=cubo.transform.position.y&&(Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow))){
			if(cubop.y>cubo.transform.position.y){
				cubop.Set(cubop.x,cubop.y+(-speedMov)*Time.deltaTime,cubop.z);
			}
			camera.transform.Translate(0,-speedMov*Time.deltaTime,0,cubo.transform);
		}
		
		
		if(camera.transform.position.y-2>=cubop.y&&Input.GetAxis("Mouse ScrollWheel") > 0){
			Vector3 mov = (cubop-camera.transform.position)*Time.deltaTime;
			mov=mov.normalized;
			camera.transform.position+=mov;
	    }
	    if(Input.GetAxis("Mouse ScrollWheel") < 0){
			Vector3 mov = (cubo.transform.position-camera.transform.position)*-Time.deltaTime;
			mov=mov.normalized;
			camera.transform.position+=mov;
	    } 
			
	}
}

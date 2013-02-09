using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {
	
	public int delay = 6;
	public int rotateTime = 4;
	
	// Use this for initialization
	void Start () {
		Rotate ();
	}
	
	void Rotate(){
			iTween.RotateAdd(gameObject,iTween.Hash(
			"amount",Vector3.up*90f,
			"time",rotateTime,
			"onComplete", "Rotate",
			"onCompleteTarget", gameObject,
			"space", Space.World,
			"delay", delay));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	// Use this for initialization
	protected virtual void Start () {
        Level.Singleton.AddEntity(this, this.transform.position);
		gameObject.AddComponent<AudioSource>();
	}
	
	public virtual void Update(){
		
	}
}

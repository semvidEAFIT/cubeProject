using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class Level : MonoBehaviour
{
    private Dictionary<Vector3, Entity> entities;
    private static int dimension = 10;

    public Rect restartButton = new Rect(0,0, Screen.width*0.1f, Screen.height *0.1f);
	private string[] music = new string[] {
		"Sounds/Music/Cientifico",
		"Sounds/Music/Clan Magico",
		"Sounds/Music/Clan Natural"
	};
	

    public static int Dimension
    {
        get { return Level.dimension; }
    }

    public Dictionary<Vector3, Entity> Entities
    {
        get {return entities; }
    }

    private ArrayList sensors;
    private static Level singleton;

    public static Level Singleton
    {
        get
        {
            if (singleton == null)
            {
                singleton = new GameObject("Level").AddComponent<Level>();
            }
            return singleton;
        }
    }

    void Awake()
    {
        entities = new Dictionary<Vector3, Entity>(new Vector3EqualityComparer());
        sensors = new ArrayList();
    }
	
	void Start(){
		GameObject obj = new GameObject("Sound Manager");
		obj.AddComponent<AudioSource>();
		AudioSource ac = obj.GetComponent<AudioSource>();
		AudioClip sound1 =  Resources.Load(music[UnityEngine.Random.Range(0,music.Length-1)]) as AudioClip;
		ac.clip = sound1;
	 	ac.Play();
	}

    void OnDestroy()
    {
        singleton = null;
    }

    public void AddEntity(Entity e, Vector3 position)
    {
        try
        {
            entities.Add(position, e);
            if (e is Sensor)
            {
                sensors.Add(e);
            }
        }
        catch (Exception ex) { 
            //Machetazo!
        }
		
    }

    public void NotifyChangePressed(Sensor s)
    {
        Debug.Log(s);
        if (s.IsPressed)
        {
            sensors.Remove(s);
            if(sensors.Count == 0){
                //GameController.Singleton.NotifyEndLevel(Application.loadedLevelName);
				
				Application.LoadLevel(UnityEngine.Random.Range(1,Application.levelCount-1));
            }
        }
        else
        { 
            if(!sensors.Contains(s)){
                sensors.Add(s);
            }
        }
    }

    void OnGUI() { 
        if(GUI.Button(restartButton, "Restart")){
            Application.LoadLevel(Application.loadedLevelName);    
        }
    }
	
	/*void Update(){
		foreach(Vector3 v in entities.Keys){
			if(entities[v] is Cube)
			{
				Debug.Log(v);
			}
		}
		
	}*/
}

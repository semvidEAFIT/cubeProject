using UnityEngine;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour {
    
    private static GameController singleton;
    private static List<String> done, toDo;

    public List<String> Done
    {
        get {
            Debug.Log("Done");
            return done; 
        }
    }
    // // 
    private static string defaultlevels = "C01, C02, C03, I01, I02, I03, M01, M02, M03, N01, N02, N03, CI01, CI02, CI03, CI04, CI05, CI06, CI07, CN01, CN02, CN03, CN04, CN05, CN06, CN07, MN01, MN02, MN03, MN04, MN05, MN06, MN07, MI01, MI02, MI03, MI04, MI05, MI06, MI07, CIM01, CIM02, CIN01, CIN02, CMN01, CMN02, IMN01, IMN02 : ";
    
    public static GameController Singleton
    {
        get
        {
            if (singleton == null)
            {
                singleton = new GameObject("GameController").AddComponent<GameController>();
                DontDestroyOnLoad(singleton.gameObject);
                Debug.Log("Singleton");
            }
            return singleton;
        }
    }

    void Awake() {
        string load = defaultlevels;
        /*
        if (PlayerPrefs.HasKey("saveData"))
        {
            Debug.Log("HasData");
            load = PlayerPrefs.GetString("saveData");
        }
        else 
        {
            load = defaultlevels;
        }*/
        string[] levels = load.Split(':');

        string[] remainingLevels = levels[0].Split(',');
        string[] passedLevels = levels[1].Split(',');
        toDo = new List<string>(48);
        done = new List<string>(48);
        for (int i = 0; i < toDo.Count; i++)
        {
            toDo[i] = remainingLevels[i];
            done[i] = passedLevels[i];
        }
        Debug.Log(toDo.Count);
    }

    public void NotifyEndLevel(String s) {
        done.Add(s);
        toDo.Remove(s);
    }

    void OnApplicationQuit() {
        string levels = "";
        foreach(string td in toDo){
            levels += td + " , ";
        }
        levels += " : ";
        foreach (string dn in done)
        {
            levels += dn + " , ";
        }
        PlayerPrefs.SetString("saveData", levels.Substring(0, levels.Length - 1));
        PlayerPrefs.Save();
    }

    public void LoadScene(string sceneName)
    {
        try
        {
            Application.LoadLevel(sceneName);
        }
        catch (UnityException e) 
        {
            Debug.Log(e);
        }
    }
}

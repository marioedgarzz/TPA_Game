using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour {

    public string toScene;
    public int toMap;
    static int toMapGlobal;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void goToScene()
    {
        SceneManager.LoadScene(toScene);
        toMapGlobal = toMap;
    }

    public static int getMapIndex()
    {
        return toMapGlobal;
    }
}

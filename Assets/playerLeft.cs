using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerLeft : MonoBehaviour {

    int enemyLeft;
    public Text text;
    public GameObject enemyLeftt;
	// Use this for initialization
	void Start () {
        BSPMazeScript b = enemyLeftt.GetComponent<BSPMazeScript>();
        text.text = b.getEnemyCount() + "";
    }
	
	// Update is called once per frame
	void Update () {
        

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToMini : MonoBehaviour {

    public GameObject sphere;

    Vector3 posPlayer;

	// Use this for initialization
	void Start () {
        Vector3 posPlayer = transform.position;
        sphere.transform.position = new Vector3(posPlayer.x, posPlayer.y + 60, posPlayer.z);
	}
	
	// Update is called once per frame
	void Update () {
        posPlayer = transform.position;
        sphere.transform.position = new Vector3(posPlayer.x, posPlayer.y + 60, posPlayer.z);
    }
}

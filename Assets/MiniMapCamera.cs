using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour {

    public GameObject player;
	// Use this for initialization
	void Start () {
        float x = transform.rotation.x;
        float y = transform.rotation.y;
        float z = transform.rotation.z;

        //transform.position = new Vector3(player.transform.position.x,
        //    player.transform.position.y + 100, player.transform.position.z);
        transform.rotation = Quaternion.Euler(x + 90,y,z);
        
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.localPosition = new Vector3(player.transform.localPosition.x,
            player.transform.localPosition.y + 100, player.transform.localPosition.z);

        //Vector3 newPos = player.transform.position;
        //newPos.y = transform.position.y;
        //transform.position = newPos;
        //transform.rotation = Quaternion.Euler(, player.transform.eulerAngles.y, 0f);

    }
}

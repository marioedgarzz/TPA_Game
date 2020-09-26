using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRightMousePressedScript : MonoBehaviour {

    //CharacterController character;
    // Use this for initialization

    float rotate;

	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButton(1))
        {
            rotate = Input.GetAxis("Mouse X");
            Debug.Log(rotate);
            transform.Rotate(Vector3.down, rotate * 10);
        }
        
    }
}

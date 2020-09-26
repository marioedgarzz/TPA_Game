using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseScript : MonoBehaviour {

    float posY, posX;

	// Use this for initialization
	void Start () {
        posY = transform.rotation.y;
        posX = transform.rotation.x;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            if(posY - 10 < transform.rotation.y && posY + 10 > transform.rotation.y)
            {
                posY -= y;
            }
            else if(!(posY + 10 > transform.rotation.y))
            {
                posY++;
            }
            else
            {
                posY--;
            }
            posX += x;
            transform.rotation = Quaternion.Euler(posY, posX, 0);

        }
    }
}

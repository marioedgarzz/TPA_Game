using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    private Vector3 initPos;
    private DesignMapInitialState mouse;
    private int tileY;
    private float posX, posY;
	// Use this for initialization
	void Start () {
        initPos = new Vector3(transform.localPosition.x, 
            transform.localPosition.y, transform.localPosition.z);
        mouse = GameObject.FindGameObjectWithTag("InitialState").GetComponent<DesignMapInitialState>();
        tileY = (int)mouse.getMazePos().y;
        posX = transform.localRotation.eulerAngles.y;
        posY = transform.localRotation.eulerAngles.x;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKey(KeyCode.W))
        {
            if (transform.localPosition.y + 65 + 50 > tileY)
            {
                if (initPos.z - 150 < transform.localPosition.z)
                    transform.Translate(new Vector3(0, 0, 1));
            }
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x, -114,
                    transform.localPosition.z);
            }
        }
        else if(Input.GetKey(KeyCode.A))
        {
            if (initPos.x + 100 > transform.localPosition.x)
                transform.Translate(new Vector3(-1, 0, 0));
        }
        else if(Input.GetKey(KeyCode.S))
        {
            if (transform.localPosition.y + 65 + 50 > tileY)
            {
                if (initPos.z + 50 > transform.localPosition.z)
                {
                    transform.Translate(new Vector3(0, 0, -1));
                }
            }
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x, -114,
                    transform.localPosition.z);
            }
        }
        else if(Input.GetKey(KeyCode.D))
        {
            if (initPos.x - 100 < transform.localPosition.x)
                transform.Translate(new Vector3(1, 0, 0));
        }

        if (Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");


            posY -= y;
            posX += x;
            transform.rotation = Quaternion.Euler(posY, posX, 0);

        }


    }
}

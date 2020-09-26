using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMaze : MonoBehaviour {

    // Use this for initialization

    public GameObject target;
    public bool isGoing;
    Vector3 targetPosition;
	void Start () {
        targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        isGoing = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (isGoing == true)
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 15f * Time.deltaTime);
    }

    public void move()
    {
        isGoing = true;
    }
}

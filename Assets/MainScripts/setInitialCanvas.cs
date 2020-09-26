using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setInitialCanvas : MonoBehaviour {

    public Canvas canvasToShow;
    public Canvas canvasToHide1;
    public Canvas canvasToHide2;

    // Use this for initialization
    void Start () {
        Cursor.visible = true;
        canvasToShow.gameObject.SetActive(true);
        canvasToHide1.gameObject.SetActive(false);
        canvasToHide2.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

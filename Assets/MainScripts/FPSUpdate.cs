using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FPSUpdate : MonoBehaviour {

    // Use this for initialization

    public Toggle myToggle;
    public Text fpsText;
    float temp;
	void Start () {
        myToggle.isOn = true;
        temp = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(myToggle.isOn == false)
        {
            QualitySettings.vSyncCount = 0;
        }
        else
        {
            QualitySettings.vSyncCount = 1;
        }

        //float microsecond = Time.deltaTime * 1000.0f;
        temp += (Time.deltaTime - temp) * 0.2f;
        float getFps = 1.0f / temp;
        int fps = (int)getFps;



        fpsText.text = fps + "";
    }
}

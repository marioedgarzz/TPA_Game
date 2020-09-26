using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class DropDownResolutionScript : MonoBehaviour {

    public Dropdown dropDown;

	// Use this for initialization
	void Start () {
        Resolution[] resolutions = Screen.resolutions;

        List<String> res = new List<String>();

        Resolution initRes = Screen.currentResolution;
        String initialVal = initRes.width + " x " + initRes.height;
        int setValue = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
            Resolution resol = resolutions[i];
            string resName = resol.width + " x " + resol.height;
            res.Add(resName);
            if (resName.Equals(initialVal))
            {
                setValue = i;
            }
        }
        
        dropDown.AddOptions(res);
        dropDown.value = setValue;

        dropDown.onValueChanged.AddListener(delegate
        {
            onDropDownChanged(dropDown);
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void onDropDownChanged(Dropdown dropDown)
    {
        string dropDownText = dropDown.captionText.text;
        string[] splitText = dropDownText.Split('x');

        int height = Convert.ToInt32(splitText[0].Trim());
        int width = Convert.ToInt32(splitText[1].Trim());

        Screen.SetResolution(height, width, true);
    }
}

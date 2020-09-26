using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QualityImageScript : MonoBehaviour {

	// Use this for initialization
    string[] qNames;
    public Dropdown dropDownQuality;
	void Start () {

        List<Dropdown.OptionData> listData = dropDownQuality.options;

        
        qNames = QualitySettings.names;
        List<string> lists = new List<string>();
        for(int i = 0; i < qNames.Length; i++)
        {
            lists.Add(qNames[i]);
        }

        dropDownQuality.AddOptions(lists);

        dropDownQuality.value = QualitySettings.GetQualityLevel();

        dropDownQuality.onValueChanged.AddListener(delegate {
            onQualityValChanged(dropDownQuality);
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void onQualityValChanged(Dropdown dropdown)
    {
        for(int i = 0; i < qNames.Length; i++)
        {
            if(dropdown.captionText.text.Equals(qNames[i]))
            {
                QualitySettings.SetQualityLevel(i+1,true);
                break;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeCanvasScript : MonoBehaviour {

    public Button myButton;
    public Canvas canvasToShow;
    public Canvas canvasToHide1;
    public Canvas canvasToHide2;
    // Use this for initialization
    void Start()
    {
        myButton.onClick.AddListener(() => toDoMethod());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void toDoMethod()
    {
        canvasToShow.gameObject.SetActive(true);
        canvasToHide1.gameObject.SetActive(false);
        canvasToHide2.gameObject.SetActive(false);
    }
    
}

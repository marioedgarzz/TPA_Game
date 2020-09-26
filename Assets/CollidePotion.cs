using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidePotion : MonoBehaviour {

    public GameObject potion;
    public GameObject Shield;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Name : " + collision.gameObject.name);
        if(collision.gameObject.tag.Equals("Potion"))
        {
            Destroy(collision.gameObject);
           
            Shield.SetActive(true);
        }
        
        
    }
}

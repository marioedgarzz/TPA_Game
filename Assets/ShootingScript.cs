using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour {

    public GameObject explosion;
    public GameObject splash;
    public GameObject splashPlace;
    private bool isShooting;
	// Use this for initialization
	void Start () {

    }

    RaycastHit objectHit;

    private IEnumerator delay()
    {
        Instantiate(splash,splashPlace.transform.localPosition + new Vector3(5,5,5), Quaternion.identity);
        Instantiate(explosion, objectHit.transform.localPosition - new Vector3(5, 5, 5), Quaternion.identity);
        yield return new WaitForSeconds(1);
        isShooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        
        if (Physics.Raycast(ray, out objectHit, Mathf.Infinity))
        {
            if (Input.GetMouseButtonUp(0) && !isShooting)
            {
                isShooting = true;
                StartCoroutine(delay());
            }
        }
    }
}

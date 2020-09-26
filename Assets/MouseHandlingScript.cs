using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHandlingScript : MonoBehaviour {
    
    private char[,] map;
    public GameObject wall;
    public GameObject potion;
    private DesignMapInitialState init;
    private insertObjectScript script;
    private static Vector3 posMaze;
    public GameObject tile;
    private static int tileLength;
    float minFov = 15f;
    float maxFov = 90f;
    float sensitivity = 10f;
    // Use this for initialization
    void Start () {
        init = transform.GetComponent<DesignMapInitialState>();
        script = transform.GetComponent<insertObjectScript>();
        tileLength = (int)wall.transform.localScale.x;
        map = init.getMap();
        posMaze = init.getMazePos();
        script.setMap(map);
    }

	// Update is called once per frame
	void Update ()
    {
        if(Input.GetMouseButtonUp(0))
        {
            createGameObject(script.getInsertObject(), script.getPosObject());   
        }
        float fov = Camera.main.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        fov = Mathf.Clamp(fov, minFov, maxFov);

        Camera.main.fieldOfView = fov;
	}

    //private IEnumerator test()
    //{
    //    yield return new WaitForSecondsRealtime(2);
    //}

    public static Vector2 getPosInMap(Vector3 pos)
    {
        int posX = (int)((pos.x - posMaze.x) / tileLength);
        int posY = (int)((pos.z - posMaze.z) / tileLength);

        Vector2 a = new Vector2(posX, posY);
        return a;
    }

    bool validateCanPutObject(Vector2 pos)
    {
        int posY = (int)pos.y;
        int posX = (int)pos.x;
        if (map[posY, posX] != ' ')
        {
            return false;
        }
        return true;
    }

    public void createGameObject(GameObject objectInsert, Vector3 pos)
    {
        Debug.Log(objectInsert);
        Vector2 getPos = getPosInMap(pos);
        bool canPut = validateCanPutObject(getPos);
        //if (objectInsert == null) return;
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
            int posY = (int)getPos.y;
        int posX = (int)getPos.x;

        if (objectInsert == wall)
        {
            if (canPut == false)
            {
                return;
            }
            Debug.Log("Wall made");
            Instantiate(objectInsert, pos, Quaternion.identity);
            
            map[posY, posX] = '#';
            script.setMap(map);
        }
        else if (objectInsert == potion)
        {
            if (canPut == false)
            {
                return;
            }
            map[posY, posX] = 'O';
            Instantiate(objectInsert, pos, Quaternion.identity);
            script.setMap(map);
            Debug.Log("afasd");
        }
        else if (objectInsert == null)
        {
            GameObject g = script.getSelectedObject();
            Debug.Log("aaaa" + g);
            if (map[posY, posX] == ' ') return;
            else if (posY == 22 || posY == 0 || posX == 0 || posX == 22)
            {
                return;
            }
            Destroy(g);
            map[posY, posX] = ' ';
            script.setMap(map);
            Debug.Log("Test");
        }
        else
        {
            Debug.Log("Failed");
        }
    }
    

}

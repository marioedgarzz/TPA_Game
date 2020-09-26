using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class insertObjectScript : MonoBehaviour {

    RaycastHit objectHit;
    public Material highLight;
    private GameObject objectInsert;
    public GameObject wall;
    public GameObject potion;
    private Vector3 posGlobal;
    private char[,] map;
    private GameObject selectedObject;
    public GameObject greenTile;
    public GameObject redTile;
    public Texture2D cursor;
    // Use this for initialization
    void Start () {
        objectInsert = wall;
    }
	
    // Update is called once per frame
    
    void Update ()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out objectHit, Mathf.Infinity))
        {
            //tidak boleh nunjuk ke panel/ semua UI yang ada
            Debug.Log(objectHit.collider.gameObject);
            selectedObject = objectHit.transform.gameObject;
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                if (selectedObject == wall || selectedObject == potion) return;
                var selection = objectHit.transform;
                var selectionRenderer = selection.GetComponent<Renderer>();
                
                if (selection != null)
                {
                    Vector3 pos;
                    if(objectInsert == null)
                    {
                        pos = selectionRenderer.transform.localPosition;
                        posGlobal = new Vector3(pos.x, pos.y, pos.z);
                        return;
                    }
                    pos = selectionRenderer.transform.localPosition;
                    float y = pos.y + objectInsert.transform.localScale.y / 2 + 1;
                    posGlobal = new Vector3(pos.x, y, pos.z);
                    Vector2 posInMap = MouseHandlingScript.getPosInMap(posGlobal);

                    int mapY = (int)posInMap.y;
                    int mapX = (int)posInMap.x;
                    
                    if(map[mapY,mapX] == ' ')
                    {
                        greenTile.transform.position = new Vector3(pos.x, pos.y, pos.z);
                        redTile.transform.position = new Vector3(100, -100, 100);
                        objectInsert.transform.localPosition = posGlobal;
                    }
                    else
                    {
                        redTile.transform.position = new Vector3(pos.x, pos.y, pos.z);
                        greenTile.transform.position = new Vector3(100, -100, 100);
                        objectInsert.transform.localPosition = new Vector3(100, -100, 100);
                    }
                    
                }
            }
        }
        else
        {
            if (objectInsert == null) return;
            objectInsert.transform.localPosition = new Vector3(100, -100, 100);
        }
        
    }
    

    public Vector3 getPosObject()
    {
        return posGlobal;
    }
    public void changeObjectToWall()
    {
        if (objectInsert != null) 
            objectInsert.transform.localPosition = new Vector3(100, -100, 100);
        objectInsert = wall;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void changeObjectToPotion()
    {
        if (objectInsert != null)
            objectInsert.transform.localPosition = new Vector3(100, -100, 100);
        objectInsert = potion;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void changeObjectToRemove()
    {
        if(objectInsert != null) {
            objectInsert.transform.localPosition = new Vector3(100, -100, 100);
        }
        objectInsert = null;
        Cursor.SetCursor(cursor,Vector2.zero, CursorMode.Auto);
    }

    public GameObject getInsertObject()
    {
        return objectInsert;
    }

    public GameObject getSelectedObject()
    {
        return selectedObject;
    }

    public void setMap(char [,] map)
    {
        this.map = map;
    }

    public char [,] getMap()
    {
        return map;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignMapInitialState : MonoBehaviour {

    private char[,] map;
    public GameObject wall;
    public GameObject tile;
    private Vector3 posMaze;
    private int row, column;
    private int widthWall, lengthWall;

    public void generateMap()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                if(i == 0 || j == 0 || i == (row-1) || j == (column-1))
                {
                    map[i, j] = '#';
                }
                else map[i, j] = ' ';
            }
            
        }
    }

    public void printMap()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Instantiate(tile, new Vector3(posMaze.x + i * widthWall, posMaze.y, posMaze.z + j * lengthWall), Quaternion.identity);
                if (map[i, j] == '#')
                {
                    Instantiate(wall, new Vector3(posMaze.x + i * widthWall, posMaze.y + wall.transform.localScale.y/2, posMaze.z + j * lengthWall), Quaternion.identity);
                }
            }
        }
    }

    // Use this for initialization
    void Start () {

        lengthWall = (int)wall.transform.localScale.x;
        widthWall = (int)wall.transform.localScale.z;

        posMaze = new Vector3(0,0,0);

        this.row = 23;
        this.column = 23;
        map = new char[row + 1, column + 1];
        generateMap();
        printMap();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public char [,] getMap()
    {
        return map;
    }

    public Vector3 getMazePos()
    {
        return posMaze;
    }
}

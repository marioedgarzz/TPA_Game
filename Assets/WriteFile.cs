using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class WriteFile : MonoBehaviour {

    insertObjectScript mouse;
    public Text text;
    char[,] map;
    char[,] mapTemp;
    void floodFill(int x, int y)
    {
        if(mapTemp[y,x] == '#' || mapTemp[y,x] == 'O')
        {
            return;
        }
        mapTemp[y, x] = '#';
        floodFill(x + 1, y);
        floodFill(x - 1, y);
        floodFill(x, y + 1);
        floodFill(x, y - 1);
    }



    bool checkAll()
    {
        for(int i = 0; i < 23; i++)
        {
            for(int j = 0; j < 23; j++)
            {
                if(mapTemp[i,j] == ' ')
                {
                    return false;
                }
            }
        }
        return true;
    }

    bool floodFill()
    {
        mapTemp = new char[23, 23];
        for (int i = 0; i < 23; i++)
        {
            for(int j = 0; j < 23; j++)
            {
                mapTemp[i, j] = map[i, j];
            }
        }

        floodFill(1, 1);
        return checkAll();
    }

    public void writeFile()
    {
        map = mouse.getMap();
        if (floodFill() == false)
        {
            text.enabled = true;
            StartCoroutine(wait());
            return;
        }
        string path = "Assets/Maps/";

        string filename = "";

        for(int i = 1; i <=4; i++)
        {
            filename = path + i + ".txt";
            if (!File.Exists(filename))
            {
                break;
            }
        }

        StreamWriter writer = new StreamWriter(filename,false);
        for(int i = 0; i < 23; i++)
        {
            for(int j = 0; j < 23; j++)
            {
                writer.Write(map[i, j]);
            }
            writer.WriteLine();
        }
        writer.Close();
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        SceneManager.LoadScene("MainScene");
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(3);
        text.enabled = false;
    }

	// Use this for initialization
	void Start () {
        mouse = GetComponent<insertObjectScript>();
        text.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

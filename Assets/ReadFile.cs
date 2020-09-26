using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class ReadFile : MonoBehaviour {

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    // Use this for initialization
    void Start ()
    {
        string path = "Assets/Maps/";
        string filename = "1.txt";
        
        if (File.Exists(path + filename))
        {
            button1.SetActive(true);
        }
        else
        {
            button1.SetActive(false);
        }

        filename = "2.txt";
        if (File.Exists(path + filename))
        {
            button2.SetActive(true);
        }
        else
        {
            button2.SetActive(false);
        }
        filename = "3.txt";
        if (File.Exists(path + filename))
        {
            button3.SetActive(true);
        }
        else
        {
            button3.SetActive(false);
        }
        filename = "4.txt";
        if (File.Exists(path + filename))
        {
            button4.SetActive(true);
        }
        else
        {
            button4.SetActive(false);
        }

        string lastFile = "0.txt";

        for(int i = 1; i <= 4; i++)
        {
            if(File.Exists(path + i + ".txt"))
            {
                lastFile = "1.txt";
            }
        }
        if (lastFile.Equals("0.txt")) return;
        read(path + lastFile);
    }

    static char[,] map;

    void read(string filePath)
    {
        map = new char[24, 24];
        StreamReader reader = new StreamReader(filePath);

        int curr = 0;
        while(!reader.EndOfStream)
        {
            string row = reader.ReadLine();
            char[] rowChar = row.ToCharArray();
            for(int i = 0; i < 23; i++)
            {
                map[curr, i] = rowChar[i];
            }
            curr++;
        }
        reader.Close();
    }

	// Update is called once per frame
	void Update () {
		
	}

    public static char[,] getByIndex(int a)
    {
        char[,] map2 = new char[24, 24];
        StreamReader reader = new StreamReader("Assets/Maps/" + a + ".txt");

        int curr = 0;
        while (!reader.EndOfStream)
        {
            string row = reader.ReadLine();
            char[] rowChar = row.ToCharArray();
            for (int i = 0; i < 23; i++)
            {
                map2[curr, i] = rowChar[i];
            }
            curr++;
        }
        reader.Close();
        return map2;
    }

    public static char[,] getMap()
    {
        return map;
    }

}

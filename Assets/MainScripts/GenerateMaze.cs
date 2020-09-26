using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMaze : MonoBehaviour {

    public GameObject wall;
    public GameObject maze;
    private int lengthWall;
    private int widthWall;
    private int row;
    private int column;
    private List<Point> list = new List<Point>();
    private char[,] map;
    private Vector3 posMaze;
    public GameObject readerFile;
    public class Point
    {
        public int fromX;
        public int fromY;
        public int toX, toY;

        public Point(int fromX, int fromY, int toX, int toY)
        {
            this.fromX = fromX;
            this.fromY = fromY;
            this.toX = toX;
            this.toY = toY;
        }
    }
    
    public void generateMap()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                map[i,j] = '#';
            }
        }
    }


    public void makeMap()
    {
        int[] posChangeY = { 0, 0, 2, -2 };
        int[] posChangeX = { 2, -2, 0, 0 };

        // y, x -> right, left, down, up
        Point startPoint = new Point(1, 1, 3, 1);
        list.Add(startPoint);
        Point startPoint2 = new Point(1, 1, 1, 3);
        list.Add(startPoint2);
        while (list.Count != 0)
        {
            int get = (int)Random.Range(0,list.Count);
            Point currPoint = list[get];

            int currX = currPoint.fromX;
            int currY = currPoint.fromY;
            int nextX = currPoint.toX;
            int nextY = currPoint.toY;

            Debug.Log(nextY + " " + nextX);

            if (map[nextY,nextX] == ' ')
            {
                list.Remove(list[get]);
                continue;
            }

            for (int j = currX; j <= nextX; j++)
            {
                map[currY,j] = ' ';
            }

            for (int j = currX; j >= nextX; j--)
            {
                map[currY,j] = ' ';
            }

            for (int j = currY; j <= nextY; j++)
            {
                map[j,currX] = ' ';
            }

            for (int j = currY; j >= nextY; j--)
            {
                map[j,currX] = ' ';
            }

            for (int i = 0; i < 4; i++)
            {
                int nextXx = nextX + posChangeX[i];
                int nextYy = nextY + posChangeY[i];

                if (nextXx < 0 || nextXx >= (column - 1) || nextYy < 0 || nextYy >= (row - 1))
                {
                    continue;
                }

                list.Add(new Point(nextX, nextY, nextXx, nextYy));
            }
            //printMap();
            list.Remove(list[get]);
        }
    }

    public void printMap()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                if (map[i, j] == '#')
                {
                    Instantiate(wall, new Vector3(posMaze.x + i * widthWall,posMaze.y ,posMaze.z + j * lengthWall),Quaternion.identity);
                }
            }
        }
    }

    // Use this for initialization
    
    void Start () {

        posMaze = maze.transform.localPosition;

        
        int lengthMaze = (int)maze.transform.localScale.x; //panjang
        int widthMaze = (int)maze.transform.localScale.z; //lebar

        lengthWall = (int)wall.transform.localScale.x;
        widthWall = (int)wall.transform.localScale.z;
        
        int heightWall = (int)wall.transform.localScale.y; 
        posMaze = new Vector3(posMaze.x - lengthMaze / 2, posMaze.y + heightWall/2, posMaze.z - widthMaze/2);

        this.row = lengthMaze/lengthWall;
        this.column = widthMaze/widthWall;

        map = ReadFile.getMap();

        if(map != null)
        {
            printMap();
            return;
        }

        map = new char[row + 1,column + 1];
        generateMap();
        makeMap();
        printMap();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

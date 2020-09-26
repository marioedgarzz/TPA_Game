using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSPMazeScript : MonoBehaviour {

    public GameObject wall;
    public GameObject tile;
    public GameObject player;
    public GameObject enemy;
    public GameObject potion;
    private List<Room> roomList;

    private Vector3 initPos;

    private int row, column;
    const int MINIMUM_WIDTH = 5;
    const int MINIMUM_HEIGHT = 5;


    private int room;

    private List<Partition> partList;
    private char[,] map;
    public GameObject safeZone;
    private class Room
    {
        public int fromX, fromY, toX, toY;
        public Room(int fromX, int fromY, int toX, int toY)
        {
            this.fromX = fromX;
            this.fromY = fromY;
            this.toX = toX;
            this.toY = toY;
        }
    }

    public char[,] getMap()
    {
        return map;
    }



    public class Partition
    {

        public int fromX, fromY;
        public int toX, toY;
        public string type;

        public Partition(int fromX, int fromY, int toX, int toY, string type)
        {
            this.fromX = fromX;
            this.fromY = fromY;
            this.toX = toX;
            this.toY = toY;
            this.type = type;
        }
    }

    public void generateMap()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                if (i == 0 || i == (row - 1) || j == 0 || j == (column - 1))
                {
                    map[i,j] = '#';
                }
                else map[i,j] = ' ';
            }
        }
        partList.Add(new Partition(0, 0, row - 1, column - 1, "First"));
        makeMap();
    }

    public void makeMap()
    {
        while (partList.Count != 0)
        {
            Partition toPartition = partList[0];
            
            if (toPartition.type.Equals("First"))
            {
                int random = (int)Random.Range(0,2);
                if (random == 1)
                {
                    buildHorizontalWall(toPartition);
                }
                else
                {
                    buildVerticalWall(toPartition);
                }
            }
            else if (toPartition.type.Equals("Horizontal"))
            {
                buildHorizontalWall(toPartition);
            }
            else if (toPartition.type.Equals("Vertical"))
            {
                buildVerticalWall(toPartition);
            }
            partList.Remove(partList[0]);
        }
    }

    public void buildHorizontalWall(Partition p)
    {
        //		int width = p.getToX() - p.getFromX();
        int height = p.toY - p.fromY;
        int splitAtY;
        int loop = 0;
        do
        {
            int randomPercentage = Random.Range(30, 71);
            splitAtY = p.fromY + (height * randomPercentage / 100);
            loop++;
            if(loop == 10)
            {
                //cannot split
                return;
            }
        } while (map[splitAtY,p.toX] == ' ' || map[splitAtY,p.fromX] == ' ');
        int area1FromX = p.fromX;
        int area1FromY = p.fromY;
        int area1ToX = p.toX;
        int area1ToY = splitAtY;
        int area2FromX = p.fromX;
        int area2FromY = splitAtY;
        int area2ToX = p.toX;
        int area2ToY = p.toY;

        int area1Height = area1ToY - area1FromY;
        int area2Height = area2ToY - area2FromY;

        if (area1Height < MINIMUM_HEIGHT || area2Height < MINIMUM_HEIGHT)
        {
            //simpan ruangan
            roomList.Add(new Room(p.fromX, p.fromY, p.toX, p.toY));
            return;
        }

        int random = Random.Range(p.fromX, p.toX);

        if (random == p.fromX) random += 1;

        for (int i = p.fromX; i < p.toX; i++)
        {
            if (i == random) {
                map[splitAtY,i] = ' ';
                if ((i - 1) == p.fromX)
                {
                    i++;
                    map[splitAtY, i] = ' ';
                }
                else
                {
                    map[splitAtY, i - 1] = ' ';
                }
            }
            else map[splitAtY,i] = '#';
        }

        room++;

        partList.Add(new Partition(area1FromX, area1FromY, area1ToX, area1ToY, "Vertical"));
        partList.Add(new Partition(area2FromX, area2FromY, area2ToX, area2ToY, "Vertical"));
        printMap();
    }

    public void buildVerticalWall(Partition p)
    {
        int width = p.toX - p.fromX - 2;
        int splitAtX;
        int loop = 0;
        do
        {
            int randomPercentage = (int)Random.Range(30, 71);
            splitAtX = p.fromX + (width * randomPercentage / 100);
            loop++;
            if(loop == 10)
            {
                //cannot split
                return;
            }
        } while (map[p.fromY,splitAtX] == ' ' || map[p.toY,splitAtX] == ' ');
        int area1FromX = p.fromX;
        int area1FromY = p.fromY;
        int area1ToX = splitAtX;
        int area1ToY = p.toY;
        int area2FromX = splitAtX;
        int area2FromY = p.fromY;
        int area2ToX = p.toX;
        int area2ToY = p.toY;

        int area1Width = area1ToX - area1FromX; 
        int area2Width = area2ToX - area2FromX;
        
        if (area1Width < MINIMUM_WIDTH || area2Width < MINIMUM_WIDTH)
        {
            roomList.Add(new Room(p.fromX, p.fromY, p.toX, p.toY));
            return;
        }

        int random = Random.Range(p.fromY, p.toY);
        if (random == p.fromY) random += 1;

        for (int i = p.fromY; i < p.toY; i++)
        {
            if (i == random) {
                map[i,splitAtX] = ' ';
                if ((i - 1) == p.fromY)
                {
                    i++;
                    map[i,splitAtX] = ' ';
                }
                else
                {
                    map[i - 1,splitAtX] = ' ';
                }
            }
            else map[i,splitAtX] = '#';
        }

        room++;
        partList.Add(new Partition(area1FromX, area1FromY, area1ToX, area1ToY, "Horizontal"));
        partList.Add(new Partition(area2FromX, area2FromY, area2ToX, area2ToY, "Horizontal"));
        printMap();
    }

    public void printMap()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                if (map[i, j] == '#')
                    Instantiate(wall, new Vector3(initPos.x + i * wall.transform.localScale.x, initPos.y + wall.transform.localScale.y / 2, initPos.z + j * wall.transform.localScale.z), Quaternion.identity);
                else if (map[i, j] == 'P')
                    player.transform.position = new Vector3(initPos.x + i * wall.transform.localScale.x, initPos.y + wall.transform.localScale.y / 2, initPos.z + j * wall.transform.localScale.z);
                else if (map[i, j] == 'E')
                    Instantiate(enemy, new Vector3(initPos.x + i * wall.transform.localScale.x, initPos.y + wall.transform.localScale.y / 2, initPos.z + j * wall.transform.localScale.z), Quaternion.identity);
                else if (map[i, j] == 'O')
                    Instantiate(potion, new Vector3(initPos.x + i * potion.transform.localScale.x, initPos.y + potion.transform.localScale.y / 2, initPos.z + j * potion.transform.localScale.z), Quaternion.identity);
                Instantiate(tile, new Vector3(initPos.x + i * tile.transform.localScale.x, initPos.y, initPos.z + j * tile.transform.localScale.z), Quaternion.identity);

                if(i == 11 && j == 11)
                {
                    safeZone.transform.position = new Vector3(initPos.x + i * wall.transform.localScale.x, initPos.y + wall.transform.localScale.y / 2, initPos.z + j * potion.transform.localScale.z);
                }

            }
        }

    }
    

    // Use this for initialization
    void Start ()
    {
        Cursor.visible = false;
        roomList = new List<Room>();
        //mendapatkan posisi ujung kiri atas
        
        initPos = new Vector3(0, 0, 0);

        row = 23;
        column = 23;

        partList = new List<Partition>();
        if(changeScene.getMapIndex() != 0)
        {
            map = ReadFile.getByIndex(changeScene.getMapIndex());

            if (map != null)
            {
                customMapGenerate();
                printMap();
                return;
            }
        }
        

        map = new char[row + 1, column + 1];
        generateMap();
        generatePlayerAndEnemy();
        printMap();
    }
	
    void customMapGenerate()
    {
        int randY, randX;
        do
        {
            randY = Random.Range(0,23);
            randX = Random.Range(0, 23);
        } while (map[randY, randX] != ' ');
        map[randY,randX] = 'P';
        do
        {
            randY = Random.Range(0, 23);
            randX = Random.Range(0, 23);
        } while (map[randY, randX] != ' ');
        map[randY, randX] = 'E';
        enemyCount = 1;
    }

    int enemyCount;

    public int getEnemyCount()
    {
        return enemyCount;
    }

    void generatePlayerAndEnemy()
    {
        int count = 0;
        enemyCount = 0;
        while (roomList.Count != 0)
        {
            
            Room currRoom = roomList[0];

            int fromX = currRoom.fromX;
            int fromY = currRoom.fromY;
            int toX = currRoom.toX;
            int toY = currRoom.toY;

            int randX = Random.Range(fromX,toX);
            int randY = Random.Range(fromY, toY);

            if (randX == fromX) randX++;
            else if (randX == toX) randX--;
            if (randY == fromY) randY++;
            else if (randY == toY) randY--;

            if (count == 0) map[randY, randX] = 'P';
            else
            {
                int random = (int) Random.Range(0, 2);
                if (random == 1)
                {
                    map[randY, randX] = 'E';
                    enemyCount++;
                }
                    
                else map[randY, randX] = 'O';
            }
            count++;

            roomList.RemoveAt(0);
        }
    }

	// Update is called once per frame
	void Update () {
		
	}


}

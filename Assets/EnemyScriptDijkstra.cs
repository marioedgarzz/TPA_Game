using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScriptDijkstra : MonoBehaviour {

    private Animator animator;

    private class Tile
    {
        const int INF = 1000000;

        public int x, y;
        public int startPosX, startPosY;
        public float distance;
        public Tile parent;
        public bool visited;
        public int cost;
        public int size;

        public Tile(int x, int y, int size, int startPosX, int startPosY)
        {
            this.y = y;
            this.x = x;
            this.size = size;
            this.parent = null;
            distance = INF;
            visited = false;
            cost = 1;
            this.startPosX = startPosX;
            this.startPosY = startPosY;
        }

    }

    class PriorityQueue
    {
        public List<Tile> tiles;

        public PriorityQueue()
        {
            tiles = new List<Tile>();

        }

        public void add(Tile t)
        {
            for(int i = 0; i < tiles.Count; i++)
            {
                if(tiles[i].distance < t.distance)
                {
                    tiles.Insert(i, t);
                    return;
                }
            }
            tiles.Add(t);
        }

        public Tile remove()
        {
            Tile t = tiles[0];
            tiles.RemoveAt(0);
            return t;
        }

        public int count()
        {
            return tiles.Count;
        }

    }

    private List<GameObject> enemies;
    public GameObject maze;
    private char [,] map;
	private int row, column;
    private Tile [,]tiles;
    private int sourceX, sourceY;
    private int destX, destY;
    private int tileSize;
    private GameObject target;
    private GameObject[] walls;
    public void init()
    {
        int mazeLength = (int)maze.transform.localScale.x;
        int mazeWidth = (int)maze.transform.localScale.z;
       
        int mazePosCenterX = (int)maze.transform.localPosition.x;
        int mazePosCenterZ = (int)maze.transform.localPosition.z;

        int mazeStartPosX = mazePosCenterX - mazeLength/2;
        int mazeStartPosZ = mazePosCenterZ - mazeWidth / 2;

        enemies = new List<GameObject>();

        walls = GameObject.FindGameObjectsWithTag("Wall");
       
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < temp.Length; i++)
        {
            Debug.Log("aaaa");
            if (temp[i] == gameObject)
            {
                Debug.Log("Same");
                continue;
            }
            enemies.Add(temp[i]);
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        enemies.Add(player);

        target = enemies[(int)Random.Range(0, enemies.Count)];

        tileSize = (int)target.transform.localScale.z; // or x
        
        this.row = mazeLength/tileSize;
        this.column = mazeWidth / tileSize ;
        tiles = new Tile[row + 1, column + 1];
        
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                int startPosX = mazeStartPosX + j * tileSize;
                int startPosY = mazeStartPosZ + i * tileSize;
                tiles[i,j] = new Tile(j, i, tileSize, startPosX,startPosY);
            }
        }

    }

    private bool posIsWall(Tile t)
    {
        BSPMazeScript script = GameObject.FindGameObjectWithTag("Maze").GetComponent<BSPMazeScript>();
        
        map = script.getMap();
        int x = t.x;
        int y = t.y;
        if(map[y,x] == '#')
        {
            return true;
        }
        return false;
    }

    private Tile getEnemyPos(GameObject enemy)
    {
        int enemyPosX = (int)enemy.transform.localPosition.x;
        int enemyPosZ = (int)enemy.transform.localPosition.z;

        int startPosTileX = tiles[0, 0].startPosX;
        int startPosTileZ = tiles[0, 0].startPosY;

        int differentX = enemyPosX - startPosTileX;
        int differentY = enemyPosZ - startPosTileZ;

        int getIndexX = differentX / tileSize;
        int getIndexY = differentY / tileSize;
        Debug.Log("Index X : " + getIndexX + "Index Y : " + getIndexY);

        destY = getIndexY;
        destX = getIndexX;

        return tiles[getIndexY,getIndexX];
    }

    void getPlayerPos()
    {
        int playerPosX = (int)transform.localPosition.x;
        int playerPosZ = (int)transform.localPosition.z;

        int startPosTileX = tiles[0, 0].startPosX;
        int startPosTileZ = tiles[0, 0].startPosY;

        int differentX = playerPosX - startPosTileX;
        int differentY = playerPosZ - startPosTileZ;

        int getIndexX = differentX / tileSize;
        int getIndexY = differentY / tileSize;
        Debug.Log("Index X : " + getIndexX + "Index Y : " + getIndexY);

        sourceY = getIndexY;
        sourceX = getIndexX;
    }

    public void pathFinding()
    {
        PriorityQueue queueTile = new PriorityQueue(); 
		
		Tile curr = getEnemyPos(target);
        curr.distance = 0;
		queueTile.add(curr);
        getPlayerPos();
		int []moveX = { 0, 0, -1, 1 };
        int []moveY = { -1, 1, 0, 0 };
		
		while(queueTile.count() != 0) {
            curr = queueTile.remove();
			
			curr.visited = true;
			
			//found
			if(curr == tiles[sourceY,sourceX]) {
                Debug.Log("Break!");
				break;
			}
			
			//check sekitar
			for(int i = 0; i< 4 ; i++) {
				int nextX = curr.x + moveX[i];
                int nextY = curr.y + moveY[i];

                if(nextX < 0 || nextX >= column || nextY < 0 || nextY >= row)
                {
                    continue;
                }

                Tile temp = tiles[nextY,nextX];
				
				if(posIsWall(temp) == true) {
                    Debug.Log("Is a wall!");
					continue;
				}
				else if(temp.visited == true) {
                    Debug.Log("Visited");
					continue;
				}
				
				float newDistance = curr.distance + temp.cost;
                Debug.Log(newDistance);
				if(newDistance<temp.distance) {
					temp.distance = newDistance;
					temp.parent = curr;
                    Debug.Log(temp.parent.distance);
					queueTile.add(temp);
				}
				
			}
		}
		
	}
	
	public bool backtrack()
    {
        Tile curr = tiles[sourceY,sourceX];
        Debug.Log("Test");
        while (curr.parent != null)
        {
            Debug.Log("Hey");
            Vector3 move = movePos(curr, curr.parent);
            curr = curr.parent;
            gameObject.transform.Translate(move * Time.deltaTime);
        }
        return true;
    }

    private Vector3 movePos(Tile src, Tile dest)
    {
        int srcX = src.startPosX;
        int srcZ = src.startPosY;
        int destX = dest.startPosX;
        int destZ = dest.startPosY;

        Vector3 temp = new Vector3(destX - srcX, 0, destZ - srcZ);
        return temp;
    }
    
    bool searching = false;
    //bool backtracking = false;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        init();
        animator.SetBool("isMoving", true);
    }
	
	// Update is called once per frame
	void Update () {
        
        if (!searching)
        {
            searching = true;
            pathFinding();
            
        }
        //backtrack();
        StartCoroutine(test());
        

    }
    private IEnumerator test()
    {
        //backtrack();
        yield return new WaitUntil(() => backtrack());
    }


    void LateUpdate()
    {
        //backtrack();
    }
}

using UnityEngine;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
    static int width = 10;
    static int height = 10;
    List<List<GameObject>> grid = new List<List<GameObject>>();

    List<GameObject> guns = new List<GameObject>();
    List<GameObject> walls = new List<GameObject>();

    [SerializeField]
    GameObject tilePrefab = null;
    [SerializeField]
    GameObject wallPrefab = null;
    [SerializeField]
    GameObject gunPrefab = null;

    static int gridLength = 1;

    // predefined map
    // top left corner is 0,0
    // 0: floor
    // 1: wall
    // 2: pit
    // 7: gun
    static int[] boardArray = new int[]
    {
        0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,
        0,0,1,1,1,1,1,1,0,0,
        0,0,1,7,0,0,7,1,0,0,
        0,0,0,0,1,1,0,0,0,0,
        0,0,0,0,1,1,0,0,0,0,
        0,0,1,7,0,0,7,1,0,0,
        0,0,1,1,1,1,1,1,0,0,
        0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0
    };

    public static Vector3 hostSpawn = new Vector3(0, 0, 0);
    public static Vector3 clientSpawn = new Vector3(9, 0, -9);


    public void InitializeGrid()
    {
        foreach (GameObject go in guns)
        {
            Destroy(go);
        }
        foreach (GameObject go in walls)
        {
            Destroy(go);
        }
        foreach (var lst in grid)
        {
            foreach (GameObject go in lst)
            {
                Destroy(go);
            }
        }

        for (int h = 0; h < Board.height; h++)
        {
            List<GameObject> rowList = new List<GameObject>();
            for(int w = 0; w < Board.width; w++)
			{
                GameObject newTile = Instantiate(tilePrefab);
                newTile.transform.SetParent(transform);
                newTile.transform.position = Vector3.right * Board.gridLength * w + Vector3.back * Board.gridLength * h;
                rowList.Add(newTile);
                // set color
                if ((h + w) % 2 == 1)
                {
                    newTile.GetComponentInChildren<MeshRenderer>().material.color = Color.black;
                }
                else
                {
                    newTile.GetComponentInChildren<MeshRenderer>().material.color = Color.white;
                }

                // wall
                if (Board.boardArray[w + h * Board.height] == 1)
                {
                    GameObject newWall = Instantiate(wallPrefab);
                    newWall.transform.SetParent(transform);
                    newWall.transform.position = Vector3.right * Board.gridLength * w + Vector3.back * Board.gridLength * h;
                    walls.Add(newWall);
                }
                // gun
                if (Board.boardArray[w + h * Board.height] == 7)
                {
                    GameObject newGun = Instantiate(gunPrefab);
                    newGun.transform.SetParent(transform);
                    newGun.transform.position = Vector3.right * Board.gridLength * w + Vector3.back * Board.gridLength * h;
                    guns.Add(newGun);
                }

            }

            grid.Add(rowList);
        }
    }

    public GameObject GetTileAt(int w, int h)
    {
        return grid[h][w];
    }
}

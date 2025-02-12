using UnityEngine;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
    static int width = 10;
    static int height = 10;
    List<List<Tile>> grid = new List<List<Tile>>();

    [SerializeField]
    GameObject tilePrefab = null;
    [SerializeField]
    GameObject wallPrefab = null;
    static int gridLength = 1;

    public void InitializeGrid()
    {
        for(int h = 0; h < Board.height; h++)
        {
            List<Tile> rowList = new List<Tile>();
            for(int w = 0; w < Board.width; w++)
			{
                GameObject newTile = Instantiate(tilePrefab);
                newTile.transform.SetParent(transform);
                newTile.transform.position = Vector3.right * Board.gridLength * w + Vector3.back * Board.gridLength * h;
                // set color
                if((h + w) % 2 == 1)
                {
                    newTile.GetComponentInChildren<MeshRenderer>().material.color = Color.black;
                }
                else
                {
                    newTile.GetComponentInChildren<MeshRenderer>().material.color = Color.white;
                }

                // temp wall generation
                if(Random.Range(0,10) > 8)
                {
                    GameObject newWall = Instantiate(wallPrefab);
                    newWall.transform.SetParent(transform);
                    newWall.transform.position = Vector3.right * Board.gridLength * w + Vector3.back * Board.gridLength * h;
                }
            }

            grid.Add(rowList);
        }
    }

    public Tile GetTileAt(int w, int h)
    {
        return grid[h][w];
    }
}

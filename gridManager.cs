using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;   
public class gridManager : MonoBehaviour
{
    public Vector2 gridWorldSize;
    public Node[,] grid; 
    GameObject hitObject;
    public Tilemap tilemap;
    public int gridSizeX, gridSizeY;   
    GameObject[] players; 
    Vector3 LastCoorEnemy;
        
    void Start() {
        
        gridSizeX = (Mathf.RoundToInt(gridWorldSize.x)+1);
        gridSizeY = (Mathf.RoundToInt(gridWorldSize.y)+1);
        grid = new Node[gridSizeX, gridSizeY];
        CreateGrid();
        players = GameObject.FindGameObjectsWithTag("Player");
    if (players.Length > 0)
    {
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        if (playerManager != null)
        {
            playerManager.passagriglia(grid);
        }
        else
        {
            Debug.LogError("PlayerManager non trovato!");
        }
    }
}
public void stampa(Node[,] gri)
{ for (int x = 0; x < gridSizeX; x++)
     {
            for (int y = 0; y < gridSizeY; y++)
            {
                Debug.Log(gri[x,y].isWalkable + " " + gri[x,y].cellPosition);
            }
    }
}
void CreateGrid() {
        bool isWalkable;
        for (int y = 0; y < gridSizeY; y++) {
            for (int x = 0; x < gridSizeX; x++) {
                
                
                Vector3Int cellPosition = new Vector3Int(x, y,0); 
                TileBase tile = tilemap.GetTile(cellPosition);
                if(tile!=null)
                    {
                        isWalkable = true;
                    }
                else
                    {
                        isWalkable = false;                        
                    }
                grid[x, y] = new Node(isWalkable, cellPosition, x, y);               
        }
    }    
}
public List<Vector3Int> FindPath(Vector3 PosIniziale, Vector3 PosTarget)
{
    Node startNode = GetNodeFromWorldPoint(PosIniziale);
    Node targetNode = GetNodeFromWorldPoint(PosTarget);
    List<Node> nodtar= caselleVicinePlayer(targetNode.cellPosition);
    List<Node> listaAperta = new List<Node>();
    HashSet<Node> listaChiusa = new HashSet<Node>();
    listaAperta.Add(startNode);

    while (listaAperta.Count > 0)
    {
        Node currentNode = listaAperta[0];
        for (int i = 1; i < listaAperta.Count; i++)
        {
            if (listaAperta[i].fCost < currentNode.fCost || listaAperta[i].fCost == currentNode.fCost && listaAperta[i].hCost < currentNode.hCost)
            {
                currentNode = listaAperta[i];
            }
        }
        listaAperta.Remove(currentNode);
        listaChiusa.Add(currentNode);
         foreach (Node nod in nodtar )
        {
           if (currentNode == nod)
            {
                return RetracePath(startNode, nod);
            }
        }

        foreach (Node csv in caselleVicine(currentNode))
        {
            if (!csv.isWalkable || listaChiusa.Contains(csv))
            {
                continue;
            }

            int nuovoCosto = currentNode.gCost + GetDistance(currentNode, csv);
            if (nuovoCosto < csv.gCost || !listaAperta.Contains(csv))
            {
                csv.gCost = nuovoCosto;
                csv.hCost = GetDistance(csv, targetNode);
                csv.parent = currentNode;

                if (!listaAperta.Contains(csv))
                {
                    listaAperta.Add(csv);
                }
            }
        }
    }
    return null;
}
    List<Node> caselleVicine(Node node)
    {
        List<Node> caselleAd = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {if(grid[checkX, checkY].utilizzabile==true)
                    {
                        caselleAd.Add(grid[checkX, checkY]);
                    }
                }    
            }
        }
        return caselleAd;
    }   
int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        return dstX + dstY; 
    }
Node GetNodeFromWorldPoint(Vector3 worldPosition)
    {
        int x = Mathf.RoundToInt(worldPosition.x);
        int y = Mathf.RoundToInt(worldPosition.y);
        return grid[x, y];
    }         
  List<Vector3Int> RetracePath(Node startNode, Node endNode)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode.cellPosition);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }      
   public List<Node> caselleVicinePlayer(Vector3 playerPosition)
    {
        List<Node> caselleAd = new List<Node>();
        int gridX = Mathf.RoundToInt(playerPosition.x);
        int gridY = Mathf.RoundToInt(playerPosition.y);        
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }
                int checkX = gridX + x;
                int checkY = gridY + y;
                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    if(grid[checkX, checkY].isWalkable==true)
                      {
                        if(grid[checkX, checkY].utilizzabile==true)
                            {
                                caselleAd.Add(grid[checkX, checkY]);
                            }
                     }
                }
            }
        }
        return caselleAd;
    }
    public void Clean_ColorTile(List<Node> vicine)
        {if(vicine!=null)
            {
                foreach (Node nodo in vicine)
                {
                    Vector3Int tilePosition = tilemap.WorldToCell(tilemap.CellToWorld(nodo.cellPosition));
                    Color currentColor = tilemap.GetColor(tilePosition);
                    currentColor.a = currentColor.a == 1f ? 0.5f : 1f;
                    tilemap.SetTileFlags(tilePosition,TileFlags.None);
                    tilemap.SetColor(tilePosition, currentColor);
                    currentColor = tilemap.GetColor(tilePosition);
                } 
            }else Debug.Log("azzz");
        }
 public void enableDisableCell(Vector3 newPos,Vector3 LastPos)
   {
        int x = Mathf.RoundToInt(LastPos.x);
        int y = Mathf.RoundToInt(LastPos.y);
        if(LastPos!=new Vector3(-1,-1,0))
            {      
                grid[x,y].utilizzabile= true;
            }
        x = Mathf.RoundToInt(newPos.x);
        y = Mathf.RoundToInt(newPos.y);
        Debug.Log(x + " " + y); 
        grid[x,y].utilizzabile= false;
        Debug.Log("non utiliz");    
   }
}

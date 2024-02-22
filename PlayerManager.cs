using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
public class PlayerManager : MonoBehaviour
{
     public static PlayerManager instance;

    GameManager2 gameManager;
    gridManager gridManager;
    Node[,] grid2;
    public Tilemap tilemap;
    int gridSizeX, gridSizeY;
    Vector2 gridWorldSize;
    Vector3 PlayerPosition;
    List<Node> vicine;
    bool walk = false; 
    bool limitChange = false;

    Vector3 LastPlayerPosition;
        void Awake()
    {
        if (instance == null)
        {
            instance = this;
            GameObject gridObject = GameObject.Find("Grid");
            if (gridObject != null)
            {
                tilemap = gridObject.GetComponentInChildren<Tilemap>();
                if (tilemap == null)
                {
                    Debug.LogError("Tilemap non trovato nell'oggetto grid!");
                }
            }
            else
            {
                Debug.LogError("Oggetto grid non trovato nella scena!");
            }
        }
        else
        {
            Destroy(gameObject);
        }
            gameManager = FindObjectOfType<GameManager2>();
            gridManager = FindObjectOfType<gridManager>();
            
            if (gridManager == null)
            {
                Debug.LogError("GridManager non trovato!");
            }
            gridSizeX = gridManager.gridSizeX;
            gridSizeY = gridManager.gridSizeY;
            gridWorldSize = gridManager.gridWorldSize;
    }
    void Start()
    {
        LastPlayerPosition = new Vector3(-1,-1,0);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {   
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int Possx = Mathf.FloorToInt(mousePosition.x);
            int Possy = Mathf.FloorToInt(mousePosition.y);         
            Vector3 Poss = new Vector3(Possx,Possy,0f);
            Vector3 SuppPos = new Vector3(Mathf.FloorToInt(transform.position.x-0.5f),Mathf.FloorToInt(transform.position.y-1f),0f);
            if(walk==true&&SuppPos != Poss)
                { 
                 foreach (Node nodo in vicine)
                    {                       
                        if(Poss==nodo.cellPosition)
                            {
                                transform.position =new Vector3((Poss.x+0.5f),(Poss.y+1f),Poss.z);
                                gridManager.Clean_ColorTile(vicine);
                                walk = false;
                                limitChange=false;
                            }
                    }
                    
                }
        }
        Vector3 PlayerPosition= new Vector3(Mathf.FloorToInt(transform.position.x-0.5f),Mathf.FloorToInt(transform.position.y-1f),0f);
        if(LastPlayerPosition!=PlayerPosition)
        {
           gridManager.enableDisableCell(PlayerPosition,LastPlayerPosition);
           LastPlayerPosition=PlayerPosition;
        }
    }
private Vector3Int UpdatePos()
{
    Vector3 newPosition = new Vector3((transform.position.x-0.5f),(transform.position.y-1),0);      
    //Debug.Log(newPosition);
    int Posx = Mathf.FloorToInt((newPosition.x));
    int Posy = Mathf.FloorToInt((newPosition.y));
    Vector3Int PosAtt = new Vector3Int(Posx, Posy, 0);
    return PosAtt;
}
   public void passagriglia(Node[,] grid)
    {
         grid2 = grid;
             //Debug.Log("Grid2: " + grid2);
         for (int x = 0; x < gridSizeX; x++)
          {
            for (int y = 0; y < gridSizeY; y++)
             {
                //Debug.Log(grid2[x,y].isWalkable + " " + grid2[x,y].cellPosition);
             }
          }
    }
    public void ChangeWalk()
    {
        if(!limitChange)
        {
             limitChange=true;
             Vector3Int PosAtt=UpdatePos();
             vicine = gridManager.caselleVicinePlayer(PosAtt);
             gridManager.Clean_ColorTile(vicine); 
             
                walk = true;
           
        } 
    }
}
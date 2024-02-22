using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class enemyManager : MonoBehaviour
{
private gridManager gridManager;
public GameObject target;
public GameObject target;
Vector3 newCoor;
Vector3 targetPos = new Vector3(-1,-1,0);
Vector3 LastCoor = new Vector3(-1,-1,0);
Vector3 offsetposition = new Vector3(0.5f,1f,0f);
bool enemyturn = false;

public static enemyManager instance;
void Awake()
    {   
        
        if (instance == null)
        {
            instance = this;
        }           
        else
        {

            Destroy(gameObject);
        }
    }

void Start()
    {
        gridManager = GameObject.FindObjectOfType<gridManager>();

            if (gridManager == null)
                {
                    Debug.LogError("GridManager non trovato!");
                    
                }
        target = GameObject.FindGameObjectWithTag("Player");

            if(target!=null) 
                {
                    Debug.Log("ci sta");
                }
    }
void Update()
 //VEDI QUAAAAAAAAAAAAAAAAAAAAAAAA AGGIORNA IL TURNO PER FAR GIOCARE IL NEMICO, NON C'E BISOGNO CHE IL NEMICO CALCOLI SEMPRE IL PERCORSO PERCHE LO FARA
 //QUANDO INIZIA IL TURNO
 {
    if(enemyturn)
    { 
        enemyturn = false;
        Vector3 targetPosition =  new Vector3((target.transform.position.x-0.5f),(target.transform.position.y-1),0);
         ///Vector3Int[] Percorso = new Vector3Int(0f,0f,0f);
        if (!(Mathf.Abs(targetPosition.x - LastCoor.x) <= 1 && Mathf.Abs(targetPosition.y - LastCoor.y) <= 1))
        { 
          Vector3Int[] Percorso=CalcolaPercorso();
          transform.position = Percorso[0]+offsetposition;
         }
          else
        {
           Debug.Log("attacca");
        }
          Vector3 newCoor= new Vector3(Mathf.FloorToInt(transform.position.x-0.5f),Mathf.FloorToInt(transform.position.y-1f),0f);
            if(LastCoor!=newCoor)
                {          
                    gridManager.enableDisableCell(newCoor,LastCoor);
                    //CalcolaPercorso();
                    LastCoor = newCoor;
                }
        /*
         Vector3 targetPosition =  new Vector3((target.transform.position.x-0.5f),(target.transform.position.y-1),0);
        if (!(Mathf.Abs(targetPosition.x - LastCoor.x) <= 1 && Mathf.Abs(targetPosition.y - LastCoor.y) <= 1))
        { 
        
            if(targetPosition !=targetPos)
                {
                    CalcolaPercorso();
                    targetPos = targetPosition;
                }
            Vector3 newCoor= new Vector3(Mathf.FloorToInt(transform.position.x-0.5f),Mathf.FloorToInt(transform.position.y-1f),0f);
            if(LastCoor!=newCoor)
                {          
                    gridManager.enableDisableCell(newCoor,LastCoor);
                    CalcolaPercorso();
                    LastCoor = newCoor;
                }
        }
       else
        {
           Debug.Log("attacca");
        } */
   
    }
 }

    public Vector3Int[] CalcolaPercorso()
    {
            Vector3 enemyPosition = new Vector3((transform.position.x-0.5f),(transform.position.y-1),0);
            Vector3 targetPosition =  new Vector3((target.transform.position.x-0.5f),(target.transform.position.y-1),0);
            List<Vector3Int> pathCoordinates = gridManager.FindPath(enemyPosition, targetPosition);
            
            if (pathCoordinates != null)
            {Vector3Int[] CoordPercorso = new Vector3Int[pathCoordinates.Count]; 
             int i = 0;
                foreach (Vector3Int coordinate in pathCoordinates)
                {
                 CoordPercorso[i] = coordinate;
                Debug.Log(coordinate);
                i++;
                }return CoordPercorso;
            }
            else
            {
                Debug.Log("Nessun percorso trovato!");
            }return null;
}
public void Changeturn()
{
    enemyturn= true;
}
}

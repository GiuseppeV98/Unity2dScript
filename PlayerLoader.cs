using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    public GameObject player;
      public GameObject nemico;
       public GameObject nemico2;
    public Vector3 spawnPosition;
    public Vector3 spawnEnemy;
     public Vector3 spawnEnemy2;
    // Start is called before the first frame update
    void Start()
    {
       if(PlayerManager.instance == null)
            {
               
                  Instantiate(player,spawnPosition, transform.rotation);
                
               
            }
        if(enemyManager.instance == null)
            {
               
                  Instantiate(nemico,spawnEnemy, transform.rotation);
                
               
            }
          if(Enemyprov.instance == null)
            {
               
                  Instantiate(nemico2,spawnEnemy2, transform.rotation);
                
               
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

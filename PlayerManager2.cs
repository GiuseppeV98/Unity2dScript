using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager2 : MonoBehaviour
{
    GameManager2 gameManager;
    
    // Start is called before the first frame update
void Start()
{
     gameManager = GameObject.FindObjectOfType<GameManager2>();
     //gameManager.SendMessage("dammipos", SendMessageOptions.DontRequireReceiver);
    // Debug.Log("target.position");
}

// Update is called once per frame
void Update()
{
     
}
public void OnMouseDown()
     {
          
          //PlayerData data = new PlayerData(this.name,this.Position);
          //gameManager.SendMessage("OnPlayerClick", SendMessageOptions.DontRequireReceiver);
     }    
}

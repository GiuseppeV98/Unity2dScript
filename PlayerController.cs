using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
     public Animator animator;
    Vector3 LastCoor;
    Vector3 newCoor;
    //aggiungi offset
   
     void Awake()
    {
        LastCoor= new Vector3(Mathf.FloorToInt(transform.position.x-0.5f),Mathf.FloorToInt(transform.position.y-1f),0f);
    }
  private void Start()
    {   

    }
    public void FixedUpdate()
    {        
      newCoor= new Vector3(Mathf.FloorToInt(transform.position.x-0.5f),Mathf.FloorToInt(transform.position.y-1f),0f);
        if(LastCoor!=newCoor)
        {
            Vector3 differenza = newCoor-LastCoor; 
            CambiaDir(differenza);
        }
    }     
    public void CambiaDir(Vector3 diff)     
      {
         LastCoor=newCoor;  
        if(diff.x ==1 && diff.y==0)
            {
                animator.SetFloat("X",1f);
                animator.SetFloat("Y",0f);
            }
        else if (diff.x==0 &&diff.y ==1)
            {
                animator.SetFloat("X",0f);
                animator.SetFloat("Y",1f);
                
            }
        else if(diff.x ==-1 && diff.y==0)
            {
                animator.SetFloat("X",-1f);
                animator.SetFloat("Y",0f);
            }
        else if (diff.x ==0 && diff.y ==-1)
            {
                animator.SetFloat("X",0f);
                animator.SetFloat("Y",-1f);
            }
         
    }

}

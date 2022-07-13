using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostInstantiate : MonoBehaviour
{
    public GameObject prefab;
    public Transform point;
    public GameObject Parent;
    public float livingTime;
   
   public void GhostInstatiate(){
        GameObject instantiateObject = Instantiate(prefab, point.position, Parent.transform.rotation) as GameObject;
        
        
        if(livingTime>0f){
            Destroy(instantiateObject, livingTime);
        }

   }
}


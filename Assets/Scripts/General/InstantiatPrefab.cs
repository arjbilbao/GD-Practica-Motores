using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatPrefab : MonoBehaviour
{
    public GameObject prefab, Parent;
    public Transform point;
    public float livingTime;
   
   public void Instatiate(){
        GameObject instantiateObject = Instantiate(prefab, point.position, Parent.transform.rotation) as GameObject;
        
        
        if(livingTime>0f){
            Destroy(instantiateObject, livingTime);
        }

   }
}

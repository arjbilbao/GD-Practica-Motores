using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{           Vector3 LocalScale;
            public GameObject character;
            public int Health;
            public int index;
    // Start is called before the first frame update
    void Start()
    {
        LocalScale = transform.localScale;
        
    }

    // Update is called once per frame
    void Update()
    {   
        HealthBar();
       
    }

    public void HealthBar(){

       

        if(index==1)
    {

        Health=character.GetComponent<BossController>()._health1;
    }
         
    if(index==2)
    {
         Health=character.GetComponent<BossController>()._health2;
    }
     if(index==3)
    {
             Health=character.GetComponent<BossController>()._health3;
    }

            if(Health<0){
                Health=0;
            }
        LocalScale.x=(Health*0.3266634f)/100f;
        transform.localScale = LocalScale;
    }
}

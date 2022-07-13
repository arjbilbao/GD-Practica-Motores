using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{           Vector3 LocalScale;
            public GameObject character;
            public float Health;
    // Start is called before the first frame update
    void Start()
    {
        LocalScale = transform.localScale;
        
    }

    // Update is called once per frame
    void Update()
    {   
        Damage();
       
    }

    public void Damage(){

        Health=character.GetComponent<SamuraiEnemy>()._health;

            if(Health<0){
                Health=0;
            }
        LocalScale.x=(Health*0.3266634f)/100f;
        transform.localScale = LocalScale;
    }
}


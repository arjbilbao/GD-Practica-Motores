using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallex : MonoBehaviour
{           private float length, startpos;
            public GameObject cam;
            public float parallexEffect;
    // Start is called before the first frame update
    void Start()
    {
        startpos=transform.position.x;
        length=GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
            //This section controls the velocity of the background layer respect to the camera.

        float dist = cam.transform.position.x*parallexEffect;
        transform.position = new Vector3 (startpos+dist,transform.position.y,transform.position.z);


            //This sections renews the sequence of the backgrounf once the camera reaches the end of it

        float temp = cam.transform.position.x*(1-parallexEffect);
        if(temp>(startpos+length)){startpos+=length;}
        else if(temp<(startpos-length)){startpos-=length;}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forward : MonoBehaviour
{
    public int speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vector3 = (Vector3.forward + new Vector3(0.0f, 0.0f, speed));
        GetComponent<Rigidbody>().velocity = vector3 * speed;
    }
}

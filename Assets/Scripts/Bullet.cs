using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

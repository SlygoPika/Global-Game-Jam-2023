using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swing : MonoBehaviour
{
    public Vector2 vineVelocityWhenGrabbed;
    Transform currentSwingable;
    ConstantForce myConstantForce;
    Rigidbody2D rb;
    bool swinging = false;

    // Start is called before the first frame update
    void Start()
    {
        myConstantForce = GetComponent<ConstantForce>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (swinging == true)
        {
            myConstantForce.enabled = false;
            transform.position = currentSwingable.position;
            if(Input.GetKeyDown(KeyCode.G)){
                swinging = false;
                rb.velocity = currentSwingable.GetComponent<Rigidbody2D>().velocity;
                // rb.useGravity = true;
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Vine")
        {
            other.GetComponent<Rigidbody2D>().velocity = vineVelocityWhenGrabbed;
            swinging = true;
            currentSwingable = other.transform;
        }
    }
}

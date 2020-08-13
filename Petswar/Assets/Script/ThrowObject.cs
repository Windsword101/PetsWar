using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public bool inWindZone = false;
    public GameObject windZone;
    Rigidbody rb;

    public float turn = 5;
   
    void Start()
    {
       
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (inWindZone)
        {
            rb.AddForce(windZone.GetComponent<WindArea>().direction * windZone.GetComponent<WindArea>().strength);
        }
    }
    void Update()
    {
        transform.Rotate(new Vector3(turn, 0, 0));
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "WindArea")
        {
            windZone = coll.gameObject;
            inWindZone = true;
        }
        if (coll.gameObject.tag == "Protection")
        {
            Destroy(gameObject);
            
        }
        if (coll.gameObject.tag == "Prop")
        {
            gameObject.GetComponent<Collider>().isTrigger = false;

        }
    }
    private void OnTriggerExit(Collider coll)
    {
        if(coll.gameObject.tag == "WindArea")
        {
            inWindZone = false;
        }
        if(coll.gameObject.tag == "Prop")
        {
            gameObject.GetComponent<Collider>().isTrigger = true;
        }
    }
   
}

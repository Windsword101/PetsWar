using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game05_Ball : MonoBehaviour
{
    private bool gamestart;
    private void Awake()
    {
        gamestart = true;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && gamestart)
        {
            transform.SetParent(null);
            gameObject.transform.SetParent(collision.gameObject.transform);
            gameObject.transform.localPosition = new Vector3(0, 2, 2);
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>(), true);
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            gameObject.transform.localPosition = new Vector3(0, 2, 2);
            gamestart = false;
        }
        if (collision.gameObject.tag == "Player" && gamestart == false)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 15, 0));
        }
    }

}

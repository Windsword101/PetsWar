using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerPropoty : MonoBehaviour
{
    PhotonView pv;
    GameObject dog;
    string[] pets = { "Dog", "Turtle", "Rib" };

    private void Start()
    {
        //int i = Random.Range(0, 3);
        pv = GetComponent<PhotonView>();
        if (pv.IsMine)
        {
            dog = GameObject.Find("Dog");
            transform.parent = dog.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }


    }
}

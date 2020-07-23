using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerPropoty : MonoBehaviour
{
    PhotonView pv;
    Player myController;
    

    private void Start()
    {
        //int i = Random.Range(0, 3);
        pv = GetComponent<PhotonView>();
        if (pv.IsMine)
        {
            myController = FindObjectOfType<Player>();
            /*transform.parent = dog.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;*/
        }


    }
}

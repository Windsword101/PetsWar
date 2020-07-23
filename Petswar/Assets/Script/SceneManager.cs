using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SceneManager : MonoBehaviourPunCallbacks
{
    public GameObject[] spawnPoint;

    private void Awake()
    {
        spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }
    void Start()
    {
        PhotonNetwork.Instantiate("dog", Vector3.up * 3f, Quaternion.identity);
        /*if (PhotonNetwork.PlayerList.Length == 1)
            PhotonNetwork.Instantiate("dog", spawnPoint[0].transform.position, Quaternion.identity);
        if (PhotonNetwork.PlayerList.Length == 2)
            PhotonNetwork.Instantiate("rib", spawnPoint[1].transform.position, Quaternion.identity);
        if (PhotonNetwork.PlayerList.Length == 3)
            PhotonNetwork.Instantiate("turtle", spawnPoint[2].transform.position, Quaternion.identity);
        /*if (PhotonNetwork.PlayerList.Length == 4)
            PhotonNetwork.Instantiate("dog", spawnPoint[3].transform.position, Quaternion.identity);*/
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();

    }
}

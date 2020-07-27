using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SceneManager : MonoBehaviourPunCallbacks
{
    //public GameObject[] spawnPoint;
    public GameObject[] pets;

    private void Awake()
    {
        //spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }
    void Start()
    {
        SpawnPlayer();
       
    }
    void SpawnPlayer()
    {
        int i;
        i = Random.Range(0, 4);
        if (PhotonNetwork.PlayerList.Length == 1)
        {
            PhotonNetwork.Instantiate(pets[i].name, new Vector3(-7.25f, 1f, -7.8f), Quaternion.Euler(0,180,0));
        }
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            PhotonNetwork.Instantiate(pets[i].name, new Vector3(6.6f, 1f, 5f), Quaternion.Euler(0, 180, 0));
        }
        if (PhotonNetwork.PlayerList.Length == 3)
        {
            PhotonNetwork.Instantiate(pets[i].name, new Vector3(5.9f, 1f, -9f), Quaternion.Euler(0, 180, 0));
        }
        if (PhotonNetwork.PlayerList.Length == 4)
        {
            PhotonNetwork.Instantiate(pets[i].name, new Vector3(-5, 1f, 4.8f), Quaternion.Euler(0, 180, 0));
        }

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

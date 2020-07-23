using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SceneManager : MonoBehaviourPunCallbacks
{
   public GameObject[] spawnPoint;
   string[] pets = { "Dog", "Turtle", "Rib" };

    private void Awake()
    {
        spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }
    void Start()
    {
        int i = Random.Range(0, 3);
        GameObject tmpPlayer = new GameObject("PlayerController");
        tmpPlayer.AddComponent<Player>();

        //PhotonNetwork.Instantiate("dog", Vector3.up * 3f, Quaternion.identity);
        if (PhotonNetwork.PlayerList.Length == 1)
            tmpPlayer.transform.position = spawnPoint[0].transform.position;
        if (PhotonNetwork.PlayerList.Length == 2)
            tmpPlayer.transform.position = spawnPoint[1].transform.position;
        if (PhotonNetwork.PlayerList.Length == 3)
            tmpPlayer.transform.position = spawnPoint[2].transform.position;
        //if (PhotonNetwork.PlayerList.Length == 4)
        // PhotonNetwork.Instantiate("dog", spawnPoint[3].transform.position, Quaternion.identity);

        GameObject myPlayer = PhotonNetwork.Instantiate(pets[i], Vector3.up * 3, Quaternion.identity);
        myPlayer.transform.parent = tmpPlayer.transform;
        myPlayer.transform.localPosition = Vector3.zero;
        myPlayer.transform.localRotation = Quaternion.identity;
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

using UnityEngine;
using System.Collections.Generic;
using KID;

public class DancePlayerManager : MonoBehaviour
{
    /// <summary>
    /// 玩家資料
    /// </summary>
    public PlayerData player;
    /// <summary>
    /// 打擊音效
    /// </summary>
    public AudioClip sounds;

    private AudioSource aud;
    private DanceManager dm;

    private KeyCode[] playerInput = new KeyCode[7];
    private DancdNodeType[] nodeType = new DancdNodeType[7];

    private void Awake()
    {
        playerInput[0] = player.forward;
        playerInput[1] = player.backward;
        playerInput[2] = player.left;
        playerInput[3] = player.right;
        playerInput[4] = player.a;
        playerInput[5] = player.b;
        playerInput[6] = player.c;

        nodeType[0] = DancdNodeType.上;
        nodeType[1] = DancdNodeType.下;
        nodeType[2] = DancdNodeType.左;
        nodeType[3] = DancdNodeType.右;
        nodeType[4] = DancdNodeType.A;
        nodeType[5] = DancdNodeType.B;
        nodeType[6] = DancdNodeType.C;

        aud = GetComponent<AudioSource>();

        dm = FindObjectOfType<DanceManager>();
    }

    private void Update()
    {
        CheckNode();
    }

    private void CheckNode()
    {
        if (!Input.anyKeyDown) return;

        if (Input.anyKeyDown)
        {
            for (int i = 0; i < playerInput.Length; i++)
            {
                if (Input.GetKeyDown(playerInput[i]))
                {
                    List<DancdNodeType> nodes = dm.nodesPlays[0];
                    List<Transform> nodeObjects = dm.nodesPlayObjects[0];

                    if (nodes[dm.nodePlaysIndex[i]] == nodeType[i])
                    {
                        aud.Stop();
                        print("正確");
                        nodeObjects[dm.nodePlaysIndex[0]].GetComponent<DanceNode>().speed = 0;
                    }
                }
            }
        }
    }
}

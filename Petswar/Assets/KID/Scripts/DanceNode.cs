using System.Collections.Generic;
using UnityEngine;

public class DanceNode : MonoBehaviour
{
    /// <summary>
    /// 節點類型
    /// </summary>
    public DancdNodeType node;
    /// <summary>
    /// 速度
    /// </summary>
    public float speed;
    /// <summary>
    /// 屬於哪位玩家的節點
    /// </summary>
    public int indexPlayer;

    private bool outOfLine;

    private DanceManager dm;

    public List<DancdNodeType> nodes = new List<DancdNodeType>();

    private void Awake()
    {
        dm = FindObjectOfType<DanceManager>();
    }

    private void Update()
    {
        //transform.Translate(-speed * Time.deltaTime, 0, 0);
        transform.Translate(0, -speed * Time.deltaTime, 0);

        if (GetComponent<RectTransform>().anchoredPosition.x <= -350 && !outOfLine)
        {
            outOfLine = true;
            dm.nodePlaysIndex[indexPlayer]++;
        }
        if (outOfLine && GetComponent<RectTransform>().anchoredPosition.x <= -360)
        {
            nodes = dm.nodesPlays[indexPlayer];
            if (dm.nodePlaysIndex[indexPlayer] - 1 >= 0) nodes[dm.nodePlaysIndex[indexPlayer] - 1] = DancdNodeType.無;
        }
    }
}

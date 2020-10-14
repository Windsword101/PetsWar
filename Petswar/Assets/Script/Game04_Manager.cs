using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Game04_Manager : MonoBehaviour
{
    public class TimeCompare : IComparer<GameObject>
    {
        public int Compare(GameObject x, GameObject y)
        {
            if (x.GetComponent<PlayerControl>().game04_caketimer < y.GetComponent<PlayerControl>().game04_caketimer) return 1;
            else if (x.GetComponent<PlayerControl>().game04_caketimer > y.GetComponent<PlayerControl>().game04_caketimer) return -1;
            else return 0;
        }
    }
    [Header("場景內四個玩家")]
    public List<GameObject> player = new List<GameObject>();
    public GameObject Scoreboard;
    public Text[] game04_result;
    public Text text;
    public int timer = 180;
    private float _timer = 1;
    //用於排列名次
    private List<GameObject> players = new List<GameObject>();
    private bool isEnd;

    void Start()
    {
        for (int i = 0; i < player.Count; i++)
        {
            players.Add(player[i]);
        }
    }

    void Update()
    {
        CountDown();
        if (timer <= 0)
        {
            Scoreboard.SetActive(true);
            int index = -1;
            players.Sort(new TimeCompare());
            foreach (GameObject p in players)
            {
                if (p.transform.Find("Cake"))
                {
                    index = players.IndexOf(p);
                }
            }
            if (index != -1)
            {
                GameObject p = players[index];
                players.RemoveAt(index);
                players.Insert(0, p);
            }
            for (int i = 0; i < players.Count; i++)
            {
                players[i].GetComponent<PlayerControl>().PlayerScore = KID.ScoreSystem.scores[i];
                game04_result[i].text = player[i].name + ":" + KID.ScoreSystem.PlayerScore[i];
            }
            isEnd = true;
        }
        if (isEnd)
        {
            for (int i = 0; i < player.Count; i++)
            {
                KID.ScoreSystem.PlayerScore[i] += player[i].GetComponent<PlayerControl>().PlayerScore;
            }
            isEnd = false;
        }
    }

    /// <summary>
    /// 時間倒數
    /// </summary>
    private void CountDown()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            if (timer > 0)
            {
                timer -= 1;
                float min = timer / 60;
                float sec = timer % 60;
                text.text = min + ":" + (sec < 10 ? "0" + sec : sec + "");
                _timer = 1;
            }
        }

    }
}


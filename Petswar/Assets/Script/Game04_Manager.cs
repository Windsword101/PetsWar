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
    public GameObject Scoreboard;
    public Text[] game04_result;
    public int[] scores;
    public GameObject player1, player2, player3, player4;
    public Text text;
    public int timer = 180;
    private float _timer = 1;
    private List<GameObject> players = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        players.Add(player1);
        players.Add(player2);
        players.Add(player3);
        players.Add(player4);
    }

    // Update is called once per frame
    void Update()
    {
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
                players[i].GetComponent<PlayerControl>().game04_Score = scores[i];
                game04_result[i].text = players[i].name + ":" + players[i].GetComponent<PlayerControl>().game04_Score;
                foreach (GameObject p in players)
                {
                    print(p);
                }
                /*foreach (GameObject p in players)
                {

                    caketimer = new List<float> { p.GetComponent<PlayerControl>().game04_caketimer };
                    foreach (float t in caketimer)
                    {
                        int max = caketimer.IndexOf(caketimer.Max());
                        int min = caketimer.IndexOf(caketimer.Min());
                    }
                }*/
            }
        }
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


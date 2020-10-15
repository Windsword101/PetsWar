using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game02_Manager : MonoBehaviour
{
    [Header("場景內四個玩家")]
    public List<GameObject> player = new List<GameObject>();
    [Header("時間倒數")]
    public float timer;
    public Text timer_text;
    //用於排列名次
    public List<GameObject> _player = new List<GameObject>();
    public List<GameObject> players = new List<GameObject>();

    private void Awake()
    {
        ScoreBoard.gameIsPlaying = true;
    }
    void Start()
    {
        for (int i = 0; i < player.Count; i++)
        {
            _player.Add(player[i]);
        }
    }

    void Update()
    {
        timer_text.text = timer.ToString();
        timer -= Time.deltaTime;
        if (ScoreBoard.gameIsPlaying)
        {
            for (int i = 0; i < _player.Count; i++)
            {
                if (_player[i].GetComponent<PlayerControl>().enabled == false)
                {
                    GameObject p = _player[i];
                    int index = _player.IndexOf(p);
                    players.Add(p);
                    _player.RemoveAt(index);
                }
            }
            if (_player.Count == 0)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    players[i].GetComponent<PlayerControl>().PlayerScore = KID.ScoreSystem.scores[i];
                    print(players[i].name + players[i].GetComponent<PlayerControl>().PlayerScore);
                }
                ScoreBoard.isEnd = true;
                ScoreBoard.gameIsPlaying = false;
            }
        }
        if (ScoreBoard.isEnd)
        {
            for (int i = 0; i < player.Count; i++)
            {
                KID.ScoreSystem.PlayerScore[i] += player[i].GetComponent<PlayerControl>().PlayerScore;
            }
            ScoreBoard.ShowResult = true;
            ScoreBoard.isEnd = false;
        }
    }
}

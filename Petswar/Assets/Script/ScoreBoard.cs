using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ScoreBoard : MonoBehaviour
{
    public List<PlayerScore> playerscore;
    public Text[] gameResult;
    public List<Image> avatarResult;
    public Sprite[] Avatar;
    public List<string> playerName;
    public static bool gameIsPlaying, isEnd, ShowResult;
    public GameObject scoreboard;
    private float timer;
    private bool isScoreSort;

    private void Awake()
    {
        ShowResult = false;
        gameIsPlaying = true;
        isEnd = false;
        playerscore = new List<PlayerScore>() { new PlayerScore(Avatar[0], 0),
                                                new PlayerScore(Avatar[1], 0),
                                                new PlayerScore(Avatar[2], 0),
                                                new PlayerScore(Avatar[3], 0)};
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

    }

    void Update()
    {
        for (int i = 0; i < gameResult.Length; i++)
        {
            playerscore[i].Score = KID.ScoreSystem.PlayerScore[i];
            /*avatarResult[i].sprite = Avatar[i];
            gameResult[i].text = KID.ScoreSystem.PlayerScore[i].ToString();*/
        }
        var queryOrder = playerscore.OrderByDescending(e => e.Score);
        var sortPlayerScore = new List<PlayerScore>();
        foreach (var item in queryOrder)
        {
            sortPlayerScore.Add(item);
        }
        print(sortPlayerScore[0].Score);
        for (int i = 0; i < sortPlayerScore.Count; i++)
        {
            avatarResult[i].sprite = sortPlayerScore[i].Avatar;
            gameResult[i].text = sortPlayerScore[i].Score.ToString();
        }
        if (ShowResult)
        {
            scoreboard.SetActive(true);
        }
        if (scoreboard.activeSelf == true)
        {
            ScoreboardActive();
        }
    }

    private void ScoreboardActive()
    {
        timer += Time.deltaTime;
        if (timer >= 2f && Input.anyKeyDown)
        {
            scoreboard.SetActive(false);
            ShowResult = false;
            string randomscene = KID.RandomScene.GetRandomScene();
            if (randomscene == "")
            {
                Application.LoadLevel("WinnerScene");
            }
            else
            {
                Application.LoadLevel(randomscene);
            }
            timer = 0;
        }
    }
}

public class PlayerScore
{
    public Sprite Avatar;
    public int Score;

    public PlayerScore(Sprite avatar, int score)
    {
        this.Avatar = avatar;
        this.Score = score;
    }
}

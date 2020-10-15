using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public Text[] gameResult;
    public List<string> playerName;
    public static bool gameIsPlaying, isEnd, ShowResult;
    public GameObject scoreboard;

    private void Awake()
    {
        ShowResult = false;
        gameIsPlaying = true;
        isEnd = false;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < gameResult.Length; i++)
        {
            gameResult[i].text = playerName[i] + ":" + KID.ScoreSystem.PlayerScore[i];
        }
        if (ShowResult)
        {
            scoreboard.SetActive(true);
        }
        if (scoreboard.activeSelf == true)
        {
            StartCoroutine("ScoreboardActive");
        }

    }

    IEnumerator ScoreboardActive()
    {
        yield return new WaitForSeconds(2f);
        if (Input.anyKeyDown)
        {
            scoreboard.SetActive(false);
            ShowResult = false;
            string randomscene = KID.RandomScene.GetRandomScene();
            if (randomscene != null)
            {
                Application.LoadLevel(randomscene);
            }
        }
    }
}

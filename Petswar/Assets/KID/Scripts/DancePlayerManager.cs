using UnityEngine;
using KID;

public class DancePlayerManager : MonoBehaviour
{
    public PlayerData[] players;

    public AudioSource aud;

    public AudioClip[] sounds;

    private void Update()
    {
        CheckNode();
    }

    private void CheckNode()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetKeyDown(players[i].forward) || Input.GetKeyDown(players[i].backward) || Input.GetKeyDown(players[i].left) || Input.GetKeyDown(players[i].right) || Input.GetKeyDown(players[i].a) || Input.GetKeyDown(players[i].b) || Input.GetKeyDown(players[i].c))
            {
                float volume = Random.Range(0.8f, 1.2f);
                float pitch = Random.Range(0.8f, 1.2f);
                aud.Stop();
                aud.pitch = pitch;
                aud.PlayOneShot(sounds[0], volume);
            }
        }
    }
}

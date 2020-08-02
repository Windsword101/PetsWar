using UnityEngine;
using UnityEngine.UI;

public class WindArea : MonoBehaviour
{
    public int strength;
    public Vector3 direction;
    public Image Windstr;
    public Sprite[] WindUI;
    private float timer;

    private void Awake()
    {
        WindUI = Resources.LoadAll<Sprite>("WindSprite");
    }

    private void Update()
    {
        Windstr.sprite = WindUI[strength];
        timer += Time.deltaTime;
        if (timer >= 5f)
        {
            strength = Random.Range(0, 6);
            timer = 0;
        }
    }
}

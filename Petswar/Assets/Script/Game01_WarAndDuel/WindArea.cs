using UnityEngine;
using UnityEngine.UI;

public class WindArea : MonoBehaviour
{
    public int strength;
    public Vector3 direction;
    public Image Windstr;
    public Sprite[] WindUI;
    private float timer;
    private int _strength;

    private void Awake()
    {
        WindUI = Resources.LoadAll<Sprite>("WindSprite");
    }

    private void Update()
    {
        _strength = Mathf.Abs(strength);
        Windstr.sprite = WindUI[_strength];
        timer += Time.deltaTime;
        if (timer >= 5f)
        {
            strength = Random.Range(-5, 6);
            timer = 0;
        }
        if (strength <= 0) Windstr.transform.eulerAngles = new Vector3(0, 180, 0);
        else Windstr.transform.eulerAngles = new Vector3(0, 0, 0);
    }
}

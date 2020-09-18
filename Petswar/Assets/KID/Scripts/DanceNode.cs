using UnityEngine;

public class DanceNode : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
    }
}

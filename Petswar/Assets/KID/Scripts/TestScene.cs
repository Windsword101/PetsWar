using UnityEngine;
using KID;

public class TestScene : MonoBehaviour
{
    private void Start()
    {
        // 初始化場景名稱
        RandomScene.SetAllScene("第一關", "第二關", "第三關");
    }

    private void Update()
    {
        // 在要取得隨機場景的地方
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // 呼叫 隨機場景.取得隨機場景
            print(RandomScene.GetRandomScene());
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

namespace KID
{
    [CreateAssetMenu(fileName = "玩家資料", menuName = "KID/玩家資料")]
    public class PlayerData : ScriptableObject
    {
        [Header("前")]
        public KeyCode forwad;
        [Header("後")]
        public KeyCode backward;
        [Header("左")]
        public KeyCode left;
        [Header("右")]
        public KeyCode right;
        [Header("按鍵 A")]
        public KeyCode a;
        [Header("按鍵 B")]
        public KeyCode b;
        [Header("按鍵 C")]
        public KeyCode c;
        [Header("選取的角色")]
        public Character character;
        [Header("對象A")]
        public GameObject targetA;
        [Header("對象B")]
        public GameObject targetB;
        [Header("對象C")]
        public GameObject targetC;
    }
}
using UnityEngine;

namespace GameJam
{
    [CreateAssetMenu(menuName = "FaceData", fileName = "FaceData", order = 0)]
    public class FaceData : ScriptableObject
    {
        public Sprite faceSprite;
        public bool needToSetFace;
    }
}
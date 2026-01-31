using UnityEngine;

namespace GameJam
{
    [CreateAssetMenu(menuName = "MaskData", fileName = "MaskData", order = 0)]
    public class MaskData : ScriptableObject
    {
        public Sprite _spriteMask;
        public MaskLayer MaskLayer = MaskLayer.Pelle;
    }
}
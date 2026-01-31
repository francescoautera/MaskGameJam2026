using UnityEngine;

[CreateAssetMenu(fileName = "New SweetSpot Data", menuName = "Data/SweetSpot Data")]
public class SweetSpotData : ScriptableObject
{
    [Range(-1, 1)] public Vector2 SweetSpotX;
    [Range(-1, 1)] public Vector2 SweetSpotY;
}
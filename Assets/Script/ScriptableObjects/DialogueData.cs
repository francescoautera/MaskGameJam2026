using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Data", menuName = "Scriptable Objects/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [SerializeField] private Sprite _portrait;
    [SerializeField] private string _name;
    [SerializeField,TextArea(3,3)] private string _line;

    public Sprite Portrait => _portrait;
    public string Name => _name;
    public string Line => _line;
}
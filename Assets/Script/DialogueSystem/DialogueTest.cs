using System.Collections;
using EasyButtons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTest : MonoBehaviour
{
    [SerializeField] private float _waitTime;

    [SerializeField] private Image _renderer;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _line;


    public DialogueData DialogueData;

    [Button]
    public void CallDisplayDialogue()
    {
        StartCoroutine(DisplayDialogue());
    }

    private IEnumerator DisplayDialogue()
    {
        if (DialogueData != null)
        {
            _renderer.sprite = DialogueData.Portrait;
            _name.text = DialogueData.Name;

            var lineSize = DialogueData.Line.Length;

            _line.maxVisibleCharacters = 0;
            
            _line.text = DialogueData.Line;

            while (_line.maxVisibleCharacters < lineSize)
            {
                _line.maxVisibleCharacters++;

                yield return new WaitForSeconds(_waitTime);
            }
        }

        yield return null;
    }
}
using System.Collections;
using EasyButtons;
using GameJam;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueTest : MonoBehaviour
{
    public UnityEvent OnStartDialogue;
    public UnityEvent OnEndDialogue;
    
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

    public void StartDialogue(DialogueData dialogueData)
    {
        DialogueData = dialogueData;
        _line.text = "";
        _name.text = dialogueData.Name;
        if (GetComponent<CanvasGroupController>())
        {
            GetComponent<CanvasGroupController>().Show(() =>
            {
                OnStartDialogue?.Invoke();
                StartCoroutine(DisplayDialogue());
            });
            return;
        }
        
        StartCoroutine(DisplayDialogue());

    }

    private IEnumerator DisplayDialogue()
    {
        if (DialogueData != null)
        {
            if (_renderer)
            {
                _renderer.sprite = DialogueData.Portrait;
            }
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
        OnEndDialogue?.Invoke();
        if (GetComponent<CanvasGroupController>())
        {
            GetComponent<CanvasGroupController>().Close(null);
        }
    }
}
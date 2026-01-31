using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHighlight : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public GameObject highlight;

    public void OnSelect(BaseEventData eventData)
    {
        highlight.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        highlight.SetActive(false);
    }
}
using UnityEngine;
using UnityEngine.Serialization;

public class HeadShake : MonoBehaviour
{
    [SerializeField] private TheWholeToy _toy;

    public float normalAmplitude;
    public float normalSpeed;
    public float closeAmplitude;
    public float closeSpeed;

    private Vector3 startPos;
    private bool isActive = false;

    public void SetActive(bool state)
    {
        isActive = state;

        startPos = transform.localPosition;
    }

    void Update()
    {
        if (!isActive)
        {
            Debug.Log("sono false");

            return;
        }

        if (_toy.isInSweetSpot)
        {
            transform.localPosition = startPos;
            return;
        }

        if (_toy.isInCloseRange)
        {
            var x = Mathf.Sin(Time.time * closeSpeed) * closeAmplitude;
            transform.localPosition = startPos + new Vector3(x, 0f, 0f);
            return;
        }

        var y = Mathf.Sin(Time.time * normalSpeed) * normalAmplitude;
        transform.localPosition = startPos + new Vector3(y, 0f, 0f);
    }
}
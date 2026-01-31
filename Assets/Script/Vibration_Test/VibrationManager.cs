using GameJam;
using UnityEngine;
using UnityEngine.InputSystem;

public class VibrationManager : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;

    [SerializeField] private Vector2 _sweetspotRange;

    private float lowFreq;
    private float highFreq;

    private Gamepad pad;

    private void Start()
    {
        pad = Gamepad.current;

        _inputManager.LeftStickPositionChanged += SetAbsoluteValues;
    }

    private void Update()
    {
        SetVibration();
    }

    public void SetSweetspotRange(float min, float max)
    {
        _sweetspotRange = new Vector2(min, max);
    }

    private void SetAbsoluteValues(Vector2 obj)
    {
        lowFreq = Mathf.Abs(obj.x);
        highFreq = Mathf.Abs(obj.y);
    }

    private void SetVibration()
    {
        if (lowFreq > _sweetspotRange.x && lowFreq < _sweetspotRange.y ||
            highFreq > _sweetspotRange.x && highFreq < _sweetspotRange.y)
        {
            pad.SetMotorSpeeds(lowFreq, highFreq);
        }
        else
        {
            pad.SetMotorSpeeds(0, 0);
        }
    }
}
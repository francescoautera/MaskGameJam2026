using UnityEngine;
using UnityEngine.InputSystem;

public class Vibration_Test : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float _lowFrequency;
    [SerializeField, Range(0f, 1f)] private float _highFrequency;

    private void Update()
    {
        Gamepad gamepad = Gamepad.current;

        gamepad.SetMotorSpeeds(_lowFrequency, _highFrequency);
    }
}
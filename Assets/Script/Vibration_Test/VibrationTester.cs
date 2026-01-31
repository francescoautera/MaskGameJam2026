using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class VibrationTester : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _low;
    [SerializeField, Range(0, 1)] private float _high;

    private Gamepad pad;

    private void Start()
    {
        pad = Gamepad.current;
    }

    private void Update()
    {
        pad.SetMotorSpeeds(_low, _high);
    }
}
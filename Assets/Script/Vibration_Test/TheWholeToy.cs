using System;
using GameJam;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TheWholeToy : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;

    [Header("Timers")] [SerializeField] private float _timeToSolve;
    [SerializeField] private float _TimeToDealDmg;

    [Header("SweetSpots Ranges")] [SerializeField]
    private Vector2 _sweetSpotX;

    [SerializeField] private Vector2 _sweetSpotY;
    [SerializeField] private Vector2 _closeToSweetSpotX;
    [SerializeField] private Vector2 _closeToSweetSpotY;

    [Header("Events")] public UnityEvent onSweetSpotEnter;
    public UnityEvent onSweetSpotExit;
    public UnityEvent onTimerReached;
    public UnityEvent onDamageTaken;

    public bool isActive = false;

    private float valueX;
    private float valueY;
    private float sweetSpotTimer;
    private float dmgTimer;

    private bool isInSweetSpot;

    private Gamepad pad;

    private void Start()
    {
        pad = Gamepad.current;

        _inputManager.LeftStickPositionChanged += ReadValues;
    }


    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        CheckSweetSpot();
    }

    private void ReadValues(Vector2 obj)
    {
        valueX = obj.x;
        valueY = obj.y;
    }

    private void CheckSweetSpot()
    {
        if (!isInSweetSpot)
        {
            dmgTimer += Time.deltaTime;
        }

        int targetX = 0;
        int targetY = 0;

        var isXNegative = _sweetSpotX.x < 0;
        var isYNegative = _sweetSpotY.x < 0;

        if (isXNegative && isYNegative) //If both are negative
        {
            targetX = valueX < _sweetSpotX.x && valueX > _sweetSpotX.y && valueY < 0 ? 1 : 0;
            targetY = valueY < _sweetSpotY.x && valueY > _sweetSpotY.y && valueX < 0 ? 1 : 0;
        }

        if (isXNegative && !isYNegative) //If only X values are negative
        {
            targetX = valueX < _sweetSpotX.x && valueX > _sweetSpotX.y ? 1 : 0;
            targetY = valueY > _sweetSpotY.x && valueY < _sweetSpotY.y ? 1 : 0;
        }

        if (!isXNegative && isYNegative) //If only Y values are negative
        {
            targetX = valueX > _sweetSpotX.x && valueX < _sweetSpotX.y ? 1 : 0;
            targetY = valueY < _sweetSpotY.x && valueY > _sweetSpotY.y ? 1 : 0;
        }

        if (!isXNegative && !isYNegative) //If both are positive
        {
            targetX = valueX > _sweetSpotX.x && valueX < _sweetSpotX.y ? 1 : 0;
            targetY = valueY > _sweetSpotY.x && valueY < _sweetSpotY.y ? 1 : 0;
        }

        if (dmgTimer >= _TimeToDealDmg)
        {
            Debug.Log("Damage Taken");

            onDamageTaken?.Invoke();

            dmgTimer = 0;
        }

        if (targetX == 1 && targetY == 1 && !isInSweetSpot)
        {
            isInSweetSpot = true;

            Debug.Log("Entrato in sweet spot");

            onSweetSpotEnter?.Invoke();
        }

        if ((targetX != 1 || targetY != 1) && isInSweetSpot)
        {
            isInSweetSpot = false;

            sweetSpotTimer = 0;

            Debug.Log("Uscito da sweet spot");

            onSweetSpotExit?.Invoke();
        }

        if (isInSweetSpot)
        {
            sweetSpotTimer += Time.deltaTime;

            if (sweetSpotTimer >= _timeToSolve)
            {
                Debug.Log("Raggiunto timer");

                sweetSpotTimer = 0;

                onTimerReached?.Invoke();
            }
        }

        pad.SetMotorSpeeds(targetX, targetY);
    }

    public void Reset()
    {
        pad.SetMotorSpeeds(0, 0);

        dmgTimer = 0;

        sweetSpotTimer = 0;
    }


    public void SetIsActive(bool active) => isActive = active;
}
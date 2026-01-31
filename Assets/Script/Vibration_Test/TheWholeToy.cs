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

    public void SetValues(SweetsPotContainer sweetsPotContainer)
    {
        _sweetSpotX = sweetsPotContainer.sweetSpotX;
        _sweetSpotY = sweetsPotContainer.sweetSpotY;
        _closeToSweetSpotX = sweetsPotContainer.closeToSweetSpotX;
        _closeToSweetSpotY = sweetsPotContainer.closeToSweetSpotY;
    }

    private void CheckSweetSpot()
    {
        if (!isInSweetSpot)
        {
            dmgTimer += Time.deltaTime;
        }

        float targetX = 0;
        float targetY = 0;

        var isXNegative = _sweetSpotX.x < 0;
        var isYNegative = _sweetSpotY.x < 0;

        #region Calculations for Sweet Spot

        if (isXNegative && isYNegative) //If both are negative
        {
            targetX = valueX < _sweetSpotX.x && valueX > _sweetSpotX.y && valueY < 0 ? 1 : 0;
            targetY = valueY < _sweetSpotY.x && valueY > _sweetSpotY.y && valueX < 0 ? 1 : 0;
        }

        if (isXNegative && !isYNegative) //If only X values are negative
        {
            targetX = valueX < _sweetSpotX.x && valueX > _sweetSpotX.y && valueY > 0 ? 1 : 0;
            targetY = valueY > _sweetSpotY.x && valueY < _sweetSpotY.y && valueX < 0 ? 1 : 0;
        }

        if (!isXNegative && isYNegative) //If only Y values are negative
        {
            targetX = valueX > _sweetSpotX.x && valueX < _sweetSpotX.y && valueY < 0 ? 1 : 0;
            targetY = valueY < _sweetSpotY.x && valueY > _sweetSpotY.y && valueX > 0 ? 1 : 0;
        }

        if (!isXNegative && !isYNegative) //If both are positive
        {
            targetX = valueX > _sweetSpotX.x && valueX < _sweetSpotX.y && valueY > 0 ? 1 : 0;
            targetY = valueY > _sweetSpotY.x && valueY < _sweetSpotY.y && valueX > 0 ? 1 : 0;
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

        if (isInSweetSpot)
        {
            return;
        }

        #endregion

        #region Calculations for close to sweet spot

        if (isXNegative && isYNegative) //If both are negative
        {
            targetX = valueX < _closeToSweetSpotX.x && valueX > _closeToSweetSpotX.y && valueY < 0 ? .1f : 0;
            targetY = valueY < _closeToSweetSpotY.x && valueY > _closeToSweetSpotY.y && valueX < 0 ? .1f : 0;
        }

        if (isXNegative && !isYNegative) //If only X values are negative
        {
            targetX = valueX < _closeToSweetSpotX.x && valueX > _closeToSweetSpotX.y && valueY > 0 ? .1f : 0;
            targetY = valueY > _closeToSweetSpotY.x && valueY < _closeToSweetSpotY.y && valueX < 0 ? .1f : 0;
        }

        if (!isXNegative && isYNegative) //If only Y values are negative
        {
            targetX = valueX > _closeToSweetSpotX.x && valueX < _closeToSweetSpotX.y && valueY < 0 ? .1f : 0;
            targetY = valueY < _closeToSweetSpotY.x && valueY > _closeToSweetSpotY.y && valueX > 0 ? .1f : 0;
        }

        if (!isXNegative && !isYNegative) //If both are positive
        {
            targetX = valueX > _closeToSweetSpotX.x && valueX < _closeToSweetSpotX.y && valueY > 0 ? .1f : 0;
            targetY = valueY > _closeToSweetSpotY.x && valueY < _closeToSweetSpotY.y && valueX > 0 ? .1f : 0;
        }

        pad.SetMotorSpeeds(targetX, targetY);

        #endregion
    }

    public void Reset()
    {
        isInSweetSpot = false;
        
        pad.SetMotorSpeeds(0, 0);

        dmgTimer = 0;

        sweetSpotTimer = 0;
    }


    public void SetIsActive(bool active) => isActive = active;
}

[Serializable]
public class SweetsPotContainer
{
    public Vector2 sweetSpotX;
    public Vector2 sweetSpotY;
    public Vector2 closeToSweetSpotX;
    public Vector2 closeToSweetSpotY;
}
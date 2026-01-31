using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameJam
{
    public class InputManager : MonoBehaviour
    {
        private InputActionMap _inputActionMap;

        private InputAction _frontSelectionAction;
        private InputAction _leftEyeSelectionAction;
        private InputAction _rightEyeSelectionAction;
        private InputAction _noseSelectionAction;
        private InputAction _mouthSelectionAction;
        private InputAction _stickMovementAction;

        [SerializeField] private InputActionAsset _inputActionAsset;

        private const string PLAYER_MAP = "Player";

        private const string LEFT_STICK_MOVEMENT_KEY = "Move";
        private const string FRONT_SELECTION_KEY = "FrontSelection";
        private const string LEFT_EYE_SELECTION_KEY = "LeftEyeSelection";
        private const string RIGHT_EYE_SELECTION_KEY = "RightEyeSelection";
        private const string NOSE_SELECTION_KEY = "NoseSelection";
        private const string MOUTH_SELECTION_KEY = "MouthSelection";

        private void Start()
        {
            _inputActionMap = _inputActionAsset.FindActionMap(PLAYER_MAP);

            _stickMovementAction = _inputActionMap.FindAction(LEFT_STICK_MOVEMENT_KEY);
            _frontSelectionAction = _inputActionMap.FindAction(FRONT_SELECTION_KEY);
            _leftEyeSelectionAction = _inputActionMap.FindAction(LEFT_EYE_SELECTION_KEY);
            _rightEyeSelectionAction = _inputActionMap.FindAction(RIGHT_EYE_SELECTION_KEY);
            _noseSelectionAction = _inputActionMap.FindAction(NOSE_SELECTION_KEY);
            _mouthSelectionAction = _inputActionMap.FindAction(MOUTH_SELECTION_KEY);

            _stickMovementAction.performed += OnLeftStickPositionChanged;
            _stickMovementAction.canceled += OnLeftStickPositionChanged;
            _frontSelectionAction.started += OnFrontSelectionStarted;
            _frontSelectionAction.canceled += OnFrontSelectionCanceled;
            _leftEyeSelectionAction.started += OnLeftEyeSelectionStarted;
            _leftEyeSelectionAction.canceled += OnLeftEyeSelectionCanceled;
            _rightEyeSelectionAction.started += OnRightEyeSelectionStarted;
            _rightEyeSelectionAction.canceled += OnRightEyeSelectionCanceled;
            _noseSelectionAction.started += OnNoseSelectionStarted;
            _noseSelectionAction.canceled += OnNoseSelectionCanceled;
            _mouthSelectionAction.started += OnMouthSelectionStarted;
            _mouthSelectionAction.canceled += OnMouthSelectionCanceled;
        }


        public event Action<Vector2> LeftStickPositionChanged;
        public event Action FrontSelectionStarted;
        public event Action FrontSelectionCanceled;
        public event Action LeftEyeSelectionStarted;
        public event Action LeftEyeSelectionCanceled;
        public event Action RightEyeSelectionStarted;
        public event Action RightEyeSelectionCanceled;
        public event Action NoseSelectionStarted;
        public event Action NoseSelectionCanceled;
        public event Action MouthSelectionStarted;
        public event Action MouthSelectionCanceled;

        private void OnLeftStickPositionChanged(InputAction.CallbackContext obj)
        {
            //Debug.Log("OnLeftStickMovementPerformed" + obj.ReadValue<Vector2>());
            LeftStickPositionChanged?.Invoke(obj.ReadValue<Vector2>());
        }

        private void OnFrontSelectionStarted(InputAction.CallbackContext obj)
        {
            FrontSelectionStarted?.Invoke();
        }

        private void OnFrontSelectionCanceled(InputAction.CallbackContext obj)
        {
            FrontSelectionCanceled?.Invoke();
        }
        
        private void OnLeftEyeSelectionStarted(InputAction.CallbackContext obj)
        {
            LeftEyeSelectionStarted?.Invoke();
        }

        private void OnLeftEyeSelectionCanceled(InputAction.CallbackContext obj)
        {
            LeftEyeSelectionCanceled?.Invoke();
        }

        private void OnRightEyeSelectionStarted(InputAction.CallbackContext obj)
        {
            RightEyeSelectionStarted?.Invoke();
        }

        private void OnRightEyeSelectionCanceled(InputAction.CallbackContext obj)
        {
            RightEyeSelectionCanceled?.Invoke();
        }

        private void OnNoseSelectionStarted(InputAction.CallbackContext obj)
        {
            NoseSelectionStarted?.Invoke();
        }

        private void OnNoseSelectionCanceled(InputAction.CallbackContext obj)
        {
            NoseSelectionCanceled?.Invoke();
        }

        private void OnMouthSelectionStarted(InputAction.CallbackContext obj)
        {
            MouthSelectionStarted?.Invoke();
        }

        private void OnMouthSelectionCanceled(InputAction.CallbackContext obj)
        {
            MouthSelectionCanceled?.Invoke();
        }
    }
}
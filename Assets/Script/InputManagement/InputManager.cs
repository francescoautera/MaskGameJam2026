using System;
using EasyButtons;
using UnityEngine;
using UnityEngine.Events;
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

        private InputAction _tutorialAction;

        private int _buttonsPressed = 0;

        [SerializeField] private InputActionAsset _inputActionAsset;

        private const string PLAYER_MAP = "Player";

        private const string LEFT_STICK_MOVEMENT_KEY = "Move";
        private const string FRONT_SELECTION_KEY = "FrontSelection";
        private const string LEFT_EYE_SELECTION_KEY = "LeftEyeSelection";
        private const string RIGHT_EYE_SELECTION_KEY = "RightEyeSelection";
        private const string NOSE_SELECTION_KEY = "NoseSelection";
        private const string MOUTH_SELECTION_KEY = "MouthSelection";
        private const string TUTORIAL_KEY = "ShowTutorial";

        public UnityEvent InputPressed;
        public UnityEvent InputReleased;
        public UnityEvent OnAllInputReleased;
        public UnityEvent OntriggerPressed;
        public UnityEvent OntriggerReleased;

        private void Start()
        {
            _inputActionMap = _inputActionAsset.FindActionMap(PLAYER_MAP);

            _stickMovementAction = _inputActionMap.FindAction(LEFT_STICK_MOVEMENT_KEY);
            _frontSelectionAction = _inputActionMap.FindAction(FRONT_SELECTION_KEY);
            _leftEyeSelectionAction = _inputActionMap.FindAction(LEFT_EYE_SELECTION_KEY);
            _rightEyeSelectionAction = _inputActionMap.FindAction(RIGHT_EYE_SELECTION_KEY);
            _noseSelectionAction = _inputActionMap.FindAction(NOSE_SELECTION_KEY);
            _mouthSelectionAction = _inputActionMap.FindAction(MOUTH_SELECTION_KEY);
            _tutorialAction = _inputActionMap.FindAction(TUTORIAL_KEY);

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

            _tutorialAction.started += OnTutorialStarted;
            _tutorialAction.canceled += OnTutorialCanceled;

            _inputActionMap.Disable();
            //DisableInput();
        }

        private void OnTutorialCanceled(InputAction.CallbackContext obj)
        {
            OntriggerReleased?.Invoke();
        }

        private void OnTutorialStarted(InputAction.CallbackContext obj)
        {
            OntriggerPressed?.Invoke();
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


        public void DisableInput()
        {
            _inputActionMap.Disable();
            _buttonsPressed = 0;
            CheckInputs();
        }

        [Button]
        public void EnableInput()
        {
            _inputActionMap.Enable();
        }

        private void OnLeftStickPositionChanged(InputAction.CallbackContext obj)
        {
            //Debug.Log("OnLeftStickMovementPerformed" + obj.ReadValue<Vector2>());
            LeftStickPositionChanged?.Invoke(obj.ReadValue<Vector2>());
        }

        private void OnFrontSelectionStarted(InputAction.CallbackContext obj)
        {
            InputPressed?.Invoke();
            FrontSelectionStarted?.Invoke();
            _buttonsPressed++;
        }

        private void OnFrontSelectionCanceled(InputAction.CallbackContext obj)
        {
            InputReleased?.Invoke();
            FrontSelectionCanceled?.Invoke();
            _buttonsPressed--;
            CheckInputs();
        }

        private void OnLeftEyeSelectionStarted(InputAction.CallbackContext obj)
        {
            InputPressed?.Invoke();
            _buttonsPressed++;
            LeftEyeSelectionStarted?.Invoke();
        }

        private void OnLeftEyeSelectionCanceled(InputAction.CallbackContext obj)
        {
            InputReleased?.Invoke();
            _buttonsPressed--;
            LeftEyeSelectionCanceled?.Invoke();
            CheckInputs();
        }

        private void OnRightEyeSelectionStarted(InputAction.CallbackContext obj)
        {
            InputPressed?.Invoke();
            _buttonsPressed++;
            RightEyeSelectionStarted?.Invoke();
        }

        private void OnRightEyeSelectionCanceled(InputAction.CallbackContext obj)
        {
            InputReleased?.Invoke();
            _buttonsPressed--;
            RightEyeSelectionCanceled?.Invoke();
            CheckInputs();
        }

        private void OnNoseSelectionStarted(InputAction.CallbackContext obj)
        {
            InputPressed?.Invoke();
            _buttonsPressed++;
            NoseSelectionStarted?.Invoke();
        }

        private void OnNoseSelectionCanceled(InputAction.CallbackContext obj)
        {
            InputReleased?.Invoke();
            _buttonsPressed--;
            NoseSelectionCanceled?.Invoke();
            CheckInputs();
        }

        private void OnMouthSelectionStarted(InputAction.CallbackContext obj)
        {
            InputPressed?.Invoke();
            _buttonsPressed++;
            MouthSelectionStarted?.Invoke();
        }

        private void OnMouthSelectionCanceled(InputAction.CallbackContext obj)
        {
            InputReleased?.Invoke();
            _buttonsPressed--;
            MouthSelectionCanceled?.Invoke();
            CheckInputs();
        }

        private void CheckInputs()
        {
            if (_buttonsPressed > 0)
            {
                return;
            }

            _buttonsPressed = 0;
            OnAllInputReleased?.Invoke();
        }
    }
}
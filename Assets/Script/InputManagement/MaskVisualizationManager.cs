using UnityEngine;


namespace GameJam
{
    [RequireComponent(typeof(InputManager))]
    public class MaskVisualizationManager : MonoBehaviour
    {
        private InputManager _inputManager;
        private Vector3 _movementVector;

        [SerializeField] private GameObject _mask;
        [SerializeField] private GameObject _frontMask;
        [SerializeField] private GameObject _leftEyeMask;
        [SerializeField] private GameObject _rightEyeMask;
        [SerializeField] private GameObject _noseMask;
        [SerializeField] private GameObject _mouthMask;

        [SerializeField] private float _minInputMagnitude = 0.1f;
        [SerializeField] private float _maskMovementSpeed = 0.1f;
 
        private void Awake()
        {
            _inputManager = GetComponent<InputManager>();
        }

        private void Start()
        {
            _inputManager.LeftStickPositionChanged += HandleLeftStickMovement;
            _inputManager.FrontSelectionStarted += HandleFrontSelectionStarted;
            _inputManager.FrontSelectionCanceled += HandleFrontSelectionCanceled;
            _inputManager.LeftEyeSelectionStarted += HandleLeftEyeSelectionStarted;
            _inputManager.LeftEyeSelectionCanceled += HandleLeftEyeSelectionCanceled;
            _inputManager.RightEyeSelectionStarted += HandleRightEyeSelectionStarted;
            _inputManager.RightEyeSelectionCanceled += HandleRightEyeSelectionCanceled;
            _inputManager.NoseSelectionStarted += HandleNoseSelectionStarted;
            _inputManager.NoseSelectionCanceled += HandleNoseSelectionCanceled;
            _inputManager.MouthSelectionStarted += HandleMouthSelectionStarted;
            _inputManager.MouthSelectionCanceled += HandleMouthSelectionCanceled;
        }

        private void Update()
        {
            if (_movementVector.sqrMagnitude <= _minInputMagnitude * _minInputMagnitude)
            {
                return;
            }

            _mask.transform.position += _movementVector * (_maskMovementSpeed * Time.deltaTime);
        }

        private void HandleLeftStickMovement(Vector2 obj)
        {
            _movementVector = new Vector3(obj.x, obj.y, 0f);
        }


        private void HandleNoseSelectionStarted()
        {
            _noseMask.SetActive(true);
        }

        private void HandleNoseSelectionCanceled()
        {
            _noseMask.SetActive(false);
        }

        private void HandleRightEyeSelectionStarted()
        {
            _rightEyeMask.SetActive(true);
        }

        private void HandleRightEyeSelectionCanceled()
        {
            _rightEyeMask.SetActive(false);
        }

        private void HandleLeftEyeSelectionStarted()
        {
            _leftEyeMask.SetActive(true);
        }

        private void HandleLeftEyeSelectionCanceled()
        {
            _leftEyeMask.SetActive(false);
        }

        private void HandleMouthSelectionStarted()
        {
            _mouthMask.SetActive(true);
        }

        private void HandleMouthSelectionCanceled()
        {
            _mouthMask.SetActive(false);
        }

        private void HandleFrontSelectionStarted()
        {
            _frontMask.SetActive(true);
        }

        private void HandleFrontSelectionCanceled()
        {
            _frontMask.SetActive(false);
        }
    }
}
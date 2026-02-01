using OmbreDiAretua;
using UnityEngine;
using UnityEngine.Serialization;


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
        [SerializeField] private Transform _maskPivotTransform;

        public bool _rightMaskBoolSet;
        public bool _frontMaskSet;
        public bool _leftEyeMaskBool;
        public bool _noseMaskBool;
        public bool _mouthMaskBool;
        

        [SerializeField] private float _minInputMagnitude = 0.1f;
        [SerializeField] private float _maskMovementIntensity = 0.1f;

        [SerializeField] private SfxPlayer _application; 
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

        private void HandleLeftStickMovement(Vector2 obj)
        {
           // _movementVector = new Vector3(obj.x, obj.y, 0f);
        }


        public void Reset()
        {
            _rightMaskBoolSet = false;
            _frontMaskSet = false;
            _leftEyeMaskBool = false;
            _noseMaskBool = false;
            _mouthMaskBool = false;
        }

        private void HandleNoseSelectionStarted()
        {
            _noseMask.SetActive(true);
            _noseMaskBool = true;
            _application.PlayFx();
            
        }

        private void HandleNoseSelectionCanceled()
        {
            _noseMask.SetActive(false);
            _noseMaskBool = false;
        }

        private void HandleRightEyeSelectionStarted()
        {
            _rightEyeMask.SetActive(true);
            _rightMaskBoolSet = true;
            _application.PlayFx();

        }

        private void HandleRightEyeSelectionCanceled()
        {
            _rightEyeMask.SetActive(false);
            _rightMaskBoolSet = false;
        }

        private void HandleLeftEyeSelectionStarted()
        {
            _leftEyeMask.SetActive(true);
            _leftEyeMaskBool = true;
            _application.PlayFx();

        }

        private void HandleLeftEyeSelectionCanceled()
        {
            _leftEyeMask.SetActive(false);
            _leftEyeMaskBool = false;
        }

        private void HandleMouthSelectionStarted()
        {
            _mouthMask.SetActive(true);
            _mouthMaskBool = true;
            _application.PlayFx();

        }

        private void HandleMouthSelectionCanceled()
        {
            _mouthMask.SetActive(false);
            _mouthMaskBool = false;
        }

        private void HandleFrontSelectionStarted()
        {
            _frontMask.SetActive(true);
            _frontMaskSet = true;
            _application.PlayFx();

        }

        private void HandleFrontSelectionCanceled()
        {
            _frontMask.SetActive(false);
            _frontMaskSet = false;
        }
    }
}
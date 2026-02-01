using System;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;
using UnityEngine.Events;

namespace GameJam
{
    public class Character : MonoBehaviour
    {
        public UnityEvent OnEndDialogue;
        public UnityEvent onEndCalculatePoints;
        public UnityEvent OnStartCalculatePoints;
        public UnityEvent OnDeath;
        
        public DialogueData currentDialogueData;
        public List<PointsDialogue> pointsDialogues = new List<PointsDialogue>();

        [Header("CurrentData")]
        public FaceData _front;
        public FaceData _rightEye;
        public FaceData _leftEye;
        public FaceData _nose;
        public FaceData mouth;

        [Header("CurrentData")] public MaskData _frontMask;
        public MaskData _rightEyeMask;
        public MaskData _leftEyeMask;
        public MaskData _noseMask;
        public MaskData mouthMask;

        [Header("LoadSprites")] public bool loadSprites;

        [Header("Sprite Faces")] [SerializeField]
        SpriteRenderer frontSprite;

        [SerializeField] SpriteRenderer rightEyeSprite;
        [SerializeField] SpriteRenderer leftEyeSprite;
        [SerializeField] SpriteRenderer noseSprite;
        [SerializeField] SpriteRenderer mouthSprite;

        [Header("Sprite Mask")] [SerializeField]
        SpriteRenderer frontMaskSprite;

        [SerializeField] SpriteRenderer rightEyeMaskSprite;
        [SerializeField] SpriteRenderer leftEyeMaskSprite;
        [SerializeField] SpriteRenderer noseMaskSprite;
        [SerializeField] SpriteRenderer mouthMaskSprite;

        [Header("Life")]
        public int life;
        public GameObject mask;
        private GameObject copyMask;
        private int currentLife;
        private string currentNameCharacter;


        public void Setup(LoadData loadData)
        {
            currentLife = life;
            _front = loadData._front;
            _rightEye = loadData._rightEye;
            _leftEye = loadData._leftEye;
            _nose = loadData._nose;
            mouth = loadData.mouth;
            _frontMask = loadData._frontMask;
            _noseMask = loadData._noseMask;
            _leftEyeMask = loadData._leftEyeMask;
            _rightEyeMask = loadData._rightEyeMask;
            mouthMask = loadData.mouthMask;

            currentDialogueData = loadData.dialogueDataLoaded;
            currentNameCharacter = currentDialogueData.Name;

            if (!loadSprites)
            {
                return;
            }

            mouthSprite.sprite = mouth.faceSprite;
            frontSprite.sprite = _front.faceSprite;
            noseSprite.sprite = _nose.faceSprite;
            rightEyeSprite.sprite = _rightEye.faceSprite;
            leftEyeSprite.sprite = _leftEye.faceSprite;
            mouthMaskSprite.sprite = mouthMask._spriteMask;
            mouthMaskSprite.rendererPriority = (int)mouthMask.MaskLayer;
            frontMaskSprite.sprite = _frontMask._spriteMask;
            frontMaskSprite.rendererPriority = (int)_frontMask.MaskLayer;
            rightEyeMaskSprite.sprite = _rightEyeMask._spriteMask;
            rightEyeMaskSprite.rendererPriority = (int)_rightEyeMask.MaskLayer;
            leftEyeMaskSprite.sprite = _leftEyeMask._spriteMask;
            leftEyeMaskSprite.rendererPriority = (int)_leftEyeMask.MaskLayer;
            noseMaskSprite.sprite = _noseMask._spriteMask;
            noseMaskSprite.rendererPriority = (int)_noseMask.MaskLayer;
        }

        public void StartDialogue()
        {
            FindFirstObjectByType<DialogueTest>().StartDialogue(currentDialogueData, OnEndDialogue);
        }


        [Button]
        public void CalculatePoint(int point)
        {
            foreach (var points in pointsDialogues)
            {
                if (points.points.x <= point && points.points.y >= point)
                {
                    OnStartCalculatePoints?.Invoke();
                    FindFirstObjectByType<DialogueTest>().StartDialogue(points.pointDialogue, onEndCalculatePoints);
                    return;
                }
            }
        }

        public void CalculateFinalPoints()
        {
            var markVisualization = FindFirstObjectByType<MaskVisualizationManager>();
            int pointFront = _front.needToSetFace == markVisualization._frontMaskSet ? 1 : 0;
            int pointMouth = mouth.needToSetFace == markVisualization._mouthMaskBool ? 1 : 0;
            int pointRightEye = _rightEye.needToSetFace == markVisualization._rightMaskBoolSet ? 1 : 0;
            int pointLeftEye = _leftEye.needToSetFace == markVisualization._leftEyeMaskBool ? 1 : 0;
            int pointNose = _nose.needToSetFace == markVisualization._noseMaskBool ? 1 : 0;
            var sum = pointFront + pointMouth + pointRightEye + pointLeftEye + pointNose;
            sum -= (life - currentLife);
            sum = Math.Clamp(sum, 0, 5);
            CalculatePoint(sum);
        }

        public void ReduceLife()
        {
            currentLife--;
            if (currentLife <=0)
            {
                OnDeath?.Invoke();
            }
        }

        public void SpawnMask()
        {
            copyMask = Instantiate(mask, mask.transform);
        }

        public void DestroyMask()
        {
            if (copyMask)
            {
                Destroy(copyMask);
            }
        }


    }

    [Serializable]
    public class PointsDialogue
    {
        public Vector2 points;
        public DialogueData pointDialogue;
    }
}
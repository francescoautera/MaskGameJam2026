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
  
  public DialogueData currentDialogueData;
  public List<PointsDialogue> pointsDialogues = new List<PointsDialogue>();

  [Header("CurrentData")] 
  public FaceData _front;
  public FaceData _rightEye;
  public FaceData _leftEye;
  public FaceData _nose;
  public FaceData mouth;
  
  [Header("CurrentData")]
  public MaskData _frontMask;
  public MaskData _rightEyeMask;
  public MaskData _leftEyeMask;
  public MaskData _noseMask;
  public MaskData mouthMask;

  public bool loadSprites;
  [Header("Sprite Faces")] 
  [SerializeField]  SpriteRenderer frontSprite;
  [SerializeField]  SpriteRenderer rightEyeSprite;
  [SerializeField]  SpriteRenderer leftEyeSprite;
  [SerializeField]  SpriteRenderer noseSprite;
  [SerializeField]  SpriteRenderer mouthSprite;
  
  [Header("Sprite Mask")]
  [SerializeField]  SpriteRenderer frontMaskSprite;
  [SerializeField]  SpriteRenderer rightEyeMaskSprite;
  [SerializeField]  SpriteRenderer leftEyeMaskSprite;
  [SerializeField]  SpriteRenderer noseMaskSprite;
  [SerializeField]  SpriteRenderer mouthMaskSprite;

  private string currentNameCharacter;
  
  
  public void Setup(LoadData loadData)
  {
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
    FindFirstObjectByType<DialogueTest>().StartDialogue(currentDialogueData,OnEndDialogue);
  }


  [Button]
  public void CalculatePoint(int point)
  {
    foreach (var points in pointsDialogues)
    {
      if (points.points.x <= point && points.points.y >= point)
      {
        points.pointDialogue.name = currentNameCharacter;
        FindFirstObjectByType<DialogueTest>().StartDialogue(points.pointDialogue,onEndCalculatePoints);
        return;
      }
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

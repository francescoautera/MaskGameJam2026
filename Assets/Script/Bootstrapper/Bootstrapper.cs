using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameJam
{
    
public class Bootstrapper : MonoBehaviour
{
    [Header("FaceData Setup")]
    public List<FaceData> front = new List<FaceData>();
    public List<FaceData> rightEye = new List<FaceData>();
    public List<FaceData> leftEye = new List<FaceData>();
    public List<FaceData> nose = new List<FaceData>();
    public List<FaceData> mouth = new List<FaceData>();
    
    [Header("MaskData Setup")]
    public List<MaskData> frontMask = new List<MaskData>();
    public List<MaskData> rightEyeMask = new List<MaskData>();
    public List<MaskData> leftEyeMask = new List<MaskData>();
    public List<MaskData> noseMask = new List<MaskData>();
    public List<MaskData> mouthMask = new List<MaskData>();

    [Header("Reference")]
    [SerializeField] Character _character;


    [Header("DialogueRef")] 
     public List<DialogueData> _dialogueDatas = new List<DialogueData>();

    private int index;

    public void SetupCharacter()
    {
        var loadData = new LoadData()
        {
            _front = front[ Random.Range(0,front.Count)],
            _rightEye = rightEye[ Random.Range(0,rightEye.Count)],
            _leftEye = leftEye[ Random.Range(0,leftEye.Count)],
            _nose = nose[ Random.Range(0,nose.Count)],
            mouth = mouth[ Random.Range(0,mouth.Count)],
            _frontMask = frontMask[ Random.Range(0,frontMask.Count)],
            _rightEyeMask = rightEyeMask[ Random.Range(0,rightEyeMask.Count)],
            _noseMask =  noseMask[ Random.Range(0,noseMask.Count)],
            _leftEyeMask = leftEyeMask[ Random.Range(0,leftEyeMask.Count)],
            mouthMask = mouthMask[Random.Range(0,mouthMask.Count)],
            dialogueDataLoaded = _dialogueDatas[index]
        };
        index++;
        _character.Setup(loadData);
    }

}

[Serializable]
public class LoadData
{
    public FaceData _front;
    public FaceData _rightEye;
    public FaceData _leftEye;
    public FaceData _nose;
    public FaceData mouth;
    
    public MaskData _frontMask;
    public MaskData _rightEyeMask;
    public MaskData _leftEyeMask;
    public MaskData _noseMask;
    public MaskData mouthMask;

    public DialogueData dialogueDataLoaded;

}

public enum MaskLayer
{
    Pelle=2,
    Metallo=3
}
}


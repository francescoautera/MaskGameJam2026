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
        FindFirstObjectByType<DialogueTest>().StartDialogue(points.pointDialogue,onEndCalculatePoints);

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

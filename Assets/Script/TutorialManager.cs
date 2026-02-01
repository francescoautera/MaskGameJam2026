using GameJam;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorial;
    public CanvasGroupController tutorialVibration;
    public CanvasGroupController _activeTutorial;
    private bool isFirstTime;
    private bool isEnable;

    public void TryActiveTutorial()
    {
        isEnable = true;
        var isActive = FindFirstObjectByType<Bootstrapper>().IsFirstEnemy();
        tutorial.SetActive(isActive);
        isFirstTime = isActive;
        if (!isActive)
        {
            _activeTutorial.Show(null);
        }
    }

    public void ActiveTutorial()
    {
        if (isFirstTime && !isEnable)
        {
            return;
        }
        tutorial.SetActive(true);
    }

    public void DeactiveTutorial()
    {
        if (isFirstTime && !isEnable)
        {
            return;
        }
        tutorial.SetActive(false);
    }

    public void ForceClose()
    {
        isEnable = false;
        tutorial.SetActive(false);
        CloseActiveVibration();
    }


    public void TryActiveVibration()
    {
        if (!isFirstTime)
        {
            return;
        }
        tutorialVibration.Show(null);
    }


    public void CloseActiveVibration()
    {
        tutorialVibration.Close(null);
    }
    

}

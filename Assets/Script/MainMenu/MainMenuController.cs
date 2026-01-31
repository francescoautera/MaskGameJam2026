using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameJam
{
    
public class MainMenuController : MonoBehaviour
{
    [SerializeField]  CanvasGroupController _canvasGroupController;
    [SerializeField]  string sceneToLoad;
    [SerializeField]  string sceneToUnload;

    private void Start()
    {
        _canvasGroupController.Close(null);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    public void ChangeToLevel()
    {
        _canvasGroupController.Show(()=>
            StartCoroutine(UnloadCor())
           );
    }

    public void QUit() => Application.Quit();

    IEnumerator UnloadCor()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
        yield return null;
        SceneManager.UnloadSceneAsync(sceneToUnload);
    }
}
}

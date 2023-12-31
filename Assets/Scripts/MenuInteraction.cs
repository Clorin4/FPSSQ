using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuInteraction : MonoBehaviour
{
    public UnityEvent onSceneChangeRequested;
    public string sceneName;

    public GameObject canvasPrincipal;
    public GameObject canvasLore;


    public void RequestSceneChange()
    {
        if(onSceneChangeRequested != null)
        {
            onSceneChangeRequested.Invoke();
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangePanel()
    {
        canvasLore.SetActive(true);
        canvasPrincipal.SetActive(false);
    }

   

}

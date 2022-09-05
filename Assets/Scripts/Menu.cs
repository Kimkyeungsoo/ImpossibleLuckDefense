using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public SceneControler sceneControler;
    private float time = 0.0f;
    
    public void OnMenu()
    {
        time = Time.timeScale;
        Time.timeScale = 0f;
        menu.SetActive(true);
    }

    public void Continue()
    {
        Time.timeScale = time;
        menu.SetActive(false);
    }

    public void Exit()
    {
        Time.timeScale = 1f;
        menu.SetActive(false);

        sceneControler.MenuScene();
    }
}

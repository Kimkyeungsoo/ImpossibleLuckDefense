using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffUI : MonoBehaviour
{
    private bool isActive = false;
    public GameSpeed speed;
    public void OnOff()
    {
        isActive = !isActive;
        gameObject.SetActive(isActive);
    }

    public void QuestButton()
    {
        Stop();
        if(!gameObject.activeSelf)
        {
            return;
        }
        GameObject.FindWithTag("GameMgr").GetComponentInChildren<QuestManager>().CheckQuestButton();
    }
    public void Stop()
    {
        isActive = !isActive;
        gameObject.SetActive(isActive);
        if(isActive)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = speed.currentSpeed;
        }
    }
}

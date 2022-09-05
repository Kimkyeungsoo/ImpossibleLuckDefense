using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSpeed : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float currentSpeed;
    private void Start()
    {
        currentSpeed = 1f;
    }
    public void InGameSpeed()
    {
        if (currentSpeed == 1f)
        {
            Time.timeScale = ++currentSpeed;
        }
        else
        {
            Time.timeScale = --currentSpeed;
        }
        text.text = currentSpeed.ToString("F1") + "X";
    }
}

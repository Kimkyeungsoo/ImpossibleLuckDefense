using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellctSuper : MonoBehaviour
{
    public string SuperUnit = string.Empty;
    public OnOffUI onOffUI;

    public void Fire()
    {
        Sellect("Fire");
    }
    public void Ice()
    {
        Sellect("Ice");
    }
    public void Ellect()
    {
        Sellect("Ellect");
    }
    public void Gun()
    {
        Sellect("Gun");
    }
    public void Lay()
    {
        Sellect("Lay");
    }
    public void Sword()
    {
        Sellect("Sword");
    }

    private void Sellect(string type)
    {
        gameObject.SetActive(false);
        SuperUnit = type;
        Time.timeScale = onOffUI.speed.currentSpeed;
    }
}

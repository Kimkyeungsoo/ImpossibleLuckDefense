using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TextUI : MonoBehaviour
{
    public TextMeshProUGUI diaText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI lifeText;

    private int dia = 3;
    private int gold = 200;
    private int wave = 1;
    private int life = 10;
    private float timer;
    private void Start()
    {
        diaText.text = $"{dia}";
        goldText.text = $"{gold}G";
        waveText.text = $"{wave}";
        lifeText.text = $"{life}";
        timer = 15f;
    }
    public void TextUIUpdate()
    {
        if (timer > 0f)
        {
            timerText.text = $"{timer:00.0}";
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timerText.text = string.Empty;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetDia(3);
        }
    }

    public void SetDia(int d)
    {
        dia += d;
        diaText.text = $"{dia}";
    }
    public void SetGold(int g)
    {
        gold += g;
        goldText.text = $"{gold}G";
    }
    public void SetWave()
    {
        waveText.text = $"{++wave}";
    }
    public void SetLife(int damage)
    {
        life -= damage;
        lifeText.text = $"{life}";
        if(life <= 0)
        {
            //Time.timeScale = 0f;
            lifeText.text = "0";
            var gameState = GameObject.FindWithTag("GameMgr").GetComponent<GameManager>().gameState;
            gameState.SetActive(true);
            gameState.GetComponentInChildren<TextMeshProUGUI>().text = "게임 오버";
        }
    }
    public void SetTimer(float t)
    {
        timer = t;
    }

    public int GetDia()
    {
        return dia;
    }
    public int GetGold()
    {
        return gold;
    }
    public int GetWave()
    {
        return wave;
    }
    public int GetLife()
    {
        return life;
    }
    public float GetTimer()
    {
        return timer;
    }
}

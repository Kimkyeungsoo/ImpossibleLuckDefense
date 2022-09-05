using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Image hpGage;
    private void Update()
    {
        gameObject.transform.rotation = Camera.main.transform.rotation;
    }
    public void Hp(int maxHp, int hp)
    {
        hpGage.fillAmount = (float)hp / (float)maxHp;
    }
}

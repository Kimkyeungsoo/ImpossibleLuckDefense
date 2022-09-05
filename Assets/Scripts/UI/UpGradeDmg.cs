using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpGradeDmg : MonoBehaviour
{
    private TextUI text;

    public TextMeshProUGUI ellectLevel;
    public TextMeshProUGUI ellectUp;
    public TextMeshProUGUI fireLevel;
    public TextMeshProUGUI fireUp;
    public TextMeshProUGUI iceLevel;
    public TextMeshProUGUI iceUp;
    public TextMeshProUGUI layLevel;
    public TextMeshProUGUI layUp;
    public TextMeshProUGUI gunLevel;
    public TextMeshProUGUI gunUp;
    public TextMeshProUGUI swordLevel;
    public TextMeshProUGUI swordUp;

    private int ellect = 0;
    private int fire = 0;
    private int ice = 0;
    private int lay = 0;
    private int gun = 0;
    private int sword = 0;

    public GameObject pool;
    public void Start()
    {
        text = GameObject.FindWithTag("GameMgr").GetComponentInChildren<TextUI>();
    }

    // ��� �̱� ~�� ����
    public void GoldBuy()
    {
        if(text.GetDia() > 0)
        {
            text.SetDia(-1);
            int random = Random.Range(150, 250);
            text.SetGold(random);
        }
    }
    public void UpgradeEllect()
    {
        if (IsGold(ellect))
        {
            // ��� ����
            text.SetGold(-(ellect + 1));
            // ���׷��̵� text ����
            ellect++;
            ellectLevel.text = $"���� ( Lv.{ellect} )";
            ellectUp.text = $"{ellect + 1}";
            // ���׷��̵� ����
            var units = pool.GetComponentsInChildren<EllectAttack>(true);
            foreach(var unit in units)
            {
                unit.upgradeLevel = ellect;
            }
        }
    }
    public void UpgradeFire()
    {
        if (IsGold(fire))
        {
            // ��� ����
            text.SetGold(-(fire + 1));
            // ���׷��̵� text ����
            fire++;
            fireLevel.text = $"�� ( Lv.{fire} )";
            fireUp.text = $"{fire + 1}";
            // ���׷��̵� ����
            var units = pool.GetComponentsInChildren<FireAttack>(true);
            foreach (var unit in units)
            {
                unit.upgradeLevel = fire;
            }
        }
    }
    public void UpgradeIce()
    {
        if (IsGold(ice))
        {
            // ��� ����
            text.SetGold(-(ice + 1));
            // ���׷��̵� text ����
            ice++;
            iceLevel.text = $"���� ( Lv.{ice} )";
            iceUp.text = $"{ice + 1}";
            // ���׷��̵� ����
            var units = pool.GetComponentsInChildren<IceAttack>(true);
            foreach (var unit in units)
            {
                unit.upgradeLevel = ice;
            }
        }
    }
    public void UpgradeLay()
    {
        if (IsGold(lay))
        {
            // ��� ����
            text.SetGold(-(lay + 1));
            // ���׷��̵� text ����
            lay++;
            layLevel.text = $"������ ( Lv.{lay} )";
            layUp.text = $"{lay + 1}";
            // ���׷��̵� ����
            var units = pool.GetComponentsInChildren<LayAttack>(true);
            foreach (var unit in units)
            {
                unit.upgradeLevel = lay;
            }
        }
    }
    public void UpgradeGun()
    {
        if (IsGold(gun))
        {
            // ��� ����
            text.SetGold(-(gun + 1));
            // ���׷��̵� text ����
            gun++;
            gunLevel.text = $"ȭ�� ( Lv.{gun} )";
            gunUp.text = $"{gun + 1}";
            // ���׷��̵� ����
            var units = pool.GetComponentsInChildren<GunAttack>(true);
            foreach (var unit in units)
            {
                unit.upgradeLevel = gun;
            }
        }
    }
    public void UpgradeSword()
    {
        if (IsGold(sword))
        {
            // ��� ����
            text.SetGold(-(sword + 1));
            // ���׷��̵� text ����
            sword++;
            swordLevel.text = $"�� ( Lv.{sword} )";
            swordUp.text = $"{sword + 1}";
            // ���׷��̵� ����
            var units = pool.GetComponentsInChildren<SwordAttack>(true);
            foreach (var unit in units)
            {
                unit.upgradeLevel = sword;
            }
        }
    }

    private bool IsGold(int buy)
    {
        if (text.GetGold() < buy)
        {
            return false;
        }
        return true;
    }
}

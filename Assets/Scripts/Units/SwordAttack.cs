using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : UnitStatus
{
    public Transform attackStart;

    public GameObject attackPrefab;

    private void Update()
    {
        LookTarget();
        if (target != null)
        {
            timer += Time.deltaTime;
            if (timer > attackTime)
            {
                timer = 0f;
                if (target.CompareTag("Monster"))
                {
                    target.GetComponent<EnemyMove>().Hit(damage + (upgradeDmg * upgradeLevel));
                }
                else
                {
                    target.GetComponent<QuestMonsterMove>().Hit(damage + (upgradeDmg * upgradeLevel));
                }
            }
        }
    }
}

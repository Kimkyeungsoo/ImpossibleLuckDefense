using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAttack : UnitStatus
{
    public Transform attackStart;

    public GameObject attackPrefab;


    private GameObject temp;

    private bool isAttack;

    private Vector3 boxSize = new Vector3(6, 5, 6);
    private void Update()
    {
        LookTarget();
        if (target != null)
        {
            timer += Time.deltaTime;
            if (timer > attackTime)
            {
                if (temp != null)
                {
                    Destroy(temp);
                }
                timer = 0f;

                //attackPrefab.transform.position = target.transform.position;
                //temp = Instantiate(attackPrefab);
                isAttack = true;
            }
        }

        if (isAttack)
        {
            isAttack = false;
            Bomb(target.transform.position);
        }
    }

    private void Bomb(Vector3 position)
    {
        var cols = Physics.OverlapBox(position, boxSize * 0.5f);
        foreach (var col in cols)
        {
            if (col.CompareTag("Monster"))
            {
                col.GetComponent<EnemyMove>().Hit(damage + (upgradeDmg * upgradeLevel));
            }
            if(col.CompareTag("QuestMonster"))
            {
                col.GetComponent<QuestMonsterMove>().Hit(damage + (upgradeDmg * upgradeLevel));
            }
        }
    }
}

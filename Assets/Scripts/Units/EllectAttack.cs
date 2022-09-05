using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllectAttack : UnitStatus
{
    public Transform attackStart;

    public GameObject attackPrefab;

    private float sturnTime = .5f;
    private float sturnTimer;
    private bool isSturn;

    private float oriSpeed;
    private EnemyMove targetMob;
    private QuestMonsterMove questMob;
    private void Update()
    {
        LookTarget();


        if (target != null)
        {
            timer += Time.deltaTime;
            if (timer > attackTime)
            {
                // Test Attack
                // var temp = Instantiate(attackPrefab);
                // temp.GetComponent<Basic>().Setting(attackStart.position, target);

                timer = 0f;
                if(target.CompareTag("Monster"))
                {
                    var mob = target.GetComponent<EnemyMove>();
                    mob.Hit(damage + (upgradeDmg * upgradeLevel));

                    var random = Random.Range(0, 10);
                    //if (random < 2)
                    {
                        targetMob = mob;
                        isSturn = true;
                        oriSpeed = mob.oriSpeed;
                        mob.oriSpeed = 0f;
                    }
                }
                else
                {
                    var mob = target.GetComponent<QuestMonsterMove>();
                    mob.Hit(damage + (upgradeDmg * upgradeLevel));

                    var random = Random.Range(0, 10);
                    //if (random < 2)
                    {
                        questMob = mob;
                        isSturn = true;
                        oriSpeed = mob.oriSpeed;
                        mob.oriSpeed = 0f;
                    }
                }
            }
        }

        if (isSturn)
        {
            sturnTimer += Time.deltaTime;
            if (sturnTimer > sturnTime)
            {
                sturnTimer = 0;
                isSturn = false;
                if(targetMob != null)
                {
                    targetMob.oriSpeed = oriSpeed;
                }
                if (questMob != null)
                {
                    questMob.oriSpeed = oriSpeed;
                }                
            }
        }
    }
}

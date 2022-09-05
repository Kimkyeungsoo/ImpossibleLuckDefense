using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAttack : UnitStatus
{
    public Transform attackStart;

    public GameObject attackPrefab;

    private float slowTime = .9f;
    private float slowTimer;
    private bool isSlow;

    private float oriSpeed;
    private EnemyMove targetMob;
    private QuestMonsterMove questMob;
    private void Update()
    {
        LookTarget();

        if (isSlow)
        {
            slowTimer += Time.deltaTime;
            if (slowTimer > slowTime)
            {
                isSlow = false;
                if (targetMob != null)
                {
                    targetMob.oriSpeed = Mathf.Lerp(targetMob.oriSpeed, oriSpeed, 2f);

                }
                if (questMob != null)
                {
                    questMob.oriSpeed = Mathf.Lerp(questMob.oriSpeed, oriSpeed, 2f);
                }
            }
        }

        if (target != null)
        {
            timer += Time.deltaTime;
            if (timer > attackTime)
            {
                timer = 0f;
                if (target.CompareTag("Monster"))
                {
                    var mob = target.GetComponent<EnemyMove>();
                    targetMob = mob;
                    targetMob.Hit(damage + (upgradeDmg * upgradeLevel));

                    isSlow = true;
                    oriSpeed = targetMob.speed;
                    targetMob.oriSpeed = 1f;
                    slowTimer = 0;
                }
                else
                {
                    var mob = target.GetComponent<QuestMonsterMove>();
                    questMob = mob;
                    questMob.Hit(damage + (upgradeDmg * upgradeLevel));

                    isSlow = true;
                    oriSpeed = questMob.speed;
                    questMob.oriSpeed = 1f;
                    slowTimer = 0;
                }
            }
        }

    }
}

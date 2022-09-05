using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMonsterMove : EnemyStatus
{
    private List<Transform> wayPoint = new List<Transform>();
    private Transform target;
    private TextUI textUI;
    private GameManager gameManager;

    private Transform startPos;
    private Vector3 dir;
    private int wayCount = 0;
    private bool isAlive = true;
    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameMgr").GetComponent<GameManager>();
        textUI = GameObject.FindWithTag("GameMgr").GetComponentInChildren<TextUI>();

        // WayPoint 가져오기
        var way = GameObject.FindWithTag("WayPoint").GetComponent<WayPoint>();
        var wayPoint = way.GetWayPoints();
        startPos = wayPoint[0];
        this.wayPoint = wayPoint;
        // 초기 셋팅
        target = this.wayPoint[wayCount];
        gameObject.transform.position = startPos.position;
        dir = gameObject.transform.position - target.position;
        dir.Normalize();
    }
    private void Update()
    {
        // 이동
        gameObject.transform.position -= dir * oriSpeed * Time.deltaTime;
        // 회전
        var rotDir = target.transform.position - gameObject.transform.position;
        rotDir.Normalize();
        if (rotDir != Vector3.zero)
        {
            var rotTarget = Quaternion.LookRotation(rotDir);
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotTarget, rotSpeed * Time.deltaTime);
        }
        // 도착 시 다음 포인트로 셋팅
        if (Vector3.Distance(gameObject.transform.position, target.position) < 0.1f)
        {
            wayCount++;
            if (wayCount < wayPoint.Count)
            {
                target = wayPoint[wayCount];
                dir = gameObject.transform.position - target.position;
                dir.Normalize();
            }
        }
        // 몬스터 소멸 조건
        if (wayCount > wayPoint.Count)
        {
            gameManager.MobCount -= 1;
            Destroy(gameObject);
            // 플레이어 라이프 감소
            var life = GameObject.FindWithTag("GameMgr").GetComponentInChildren<TextUI>();
            life.SetLife(5);
        }
    }
    public void Hit(int damage)
    {
        hp -= damage;
        GetComponentInChildren<HpBar>().Hp(maxHp, hp);
        if (hp <= 0 && isAlive)
        {
            isAlive = false;
            //var gameManager = GameObject.FindWithTag("GameMgr").GetComponent<GameManager>();
            // 보상
            if (gameObject.name == "QuestMonster1(Clone)")
            {
                textUI.SetGold(200);
            }
            if (gameObject.name == "QuestMonster2(Clone)")
            {
                textUI.SetGold(500);
            }
            if (gameObject.name == "QuestMonster3(Clone)")
            {
                gameManager.isBuffe = true;
                gameManager.buffeTimer = 0f;
                var units = gameManager.upGradeDmg.pool.GetComponentsInChildren<UnitStatus>(true);
                foreach (var unit in units)
                {
                    unit.upgradeLevel += 20;
                }
                textUI.SetGold(800);
            }
            gameManager.MobCount -= 1;
            Destroy(gameObject);
        }
    }
}

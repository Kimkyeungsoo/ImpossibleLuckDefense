using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : EnemyStatus
{
    private List<Transform> wayPoint = new List<Transform>();
    private GameManager gameManager;
    private Transform target;

    private Transform startPos;

    private Vector3 dir;
    private bool isAlive;

    private int wayCount = 0;

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameMgr").GetComponent<GameManager>();
    }
    private void Update()
    {
        if (isAlive)
        {
            // 이동
            gameObject.transform.position -= dir * oriSpeed * Time.deltaTime;
            // 회전
            var rotDir = target.transform.position - gameObject.transform.position;
            rotDir.Normalize();
            if(rotDir != Vector3.zero)
            {
                var rotTarget = Quaternion.LookRotation(rotDir);
                gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotTarget, rotSpeed * Time.deltaTime);
            }
            // 도착 시 다음 포인트로 셋팅
            if (Vector3.Distance(gameObject.transform.position, target.position) < 0.2f)
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
                Delete();
                // 플레이어 라이프 감소
                var life = GameObject.FindWithTag("GameMgr").GetComponentInChildren<TextUI>();
                if((gameManager.monsterWave.waveId + 1) % 5 == 0)
                {
                    life.SetLife(5);
                }
                else
                {
                    life.SetLife(1);
                }
            }
        }
    }

    public void MobSetting()
    {
        // WayPoint 가져오기
        var way = GameObject.FindWithTag("WayPoint").GetComponent<WayPoint>();
        var wayPoint = way.GetWayPoints();
        startPos = wayPoint[0];
        this.wayPoint = wayPoint;
        // 초기 셋팅
        speed = oriSpeed;
        hp = maxHp;
        isAlive = true;
        target = this.wayPoint[wayCount];
        gameObject.transform.position = startPos.position;
        dir = gameObject.transform.position - target.position;
        dir.Normalize();
    }

    public void SetReset()
    {
        // 셋팅 리셋
        isAlive = false;
        hp = maxHp;
        gameObject.transform.rotation = new Quaternion(0f, 180f, 0f, 1f);
        wayCount = 0;
        speed = oriSpeed;
        //
        gameObject.GetComponentInChildren<HpBar>().hpGage.fillAmount = 1f;

        gameObject.SetActive(false);
    }

    public void Hit(int damage)
    {
        hp -= damage;
        GetComponentInChildren<HpBar>().Hp(maxHp, hp);
        if (hp <= 0)
        {
            Delete();
        }
    }

    public void Delete()
    {
        SetReset();
        // 다음 스테이지 판단 위해서 필요
        GameObject.FindWithTag("GameMgr").GetComponent<GameManager>().MobCount--;
    }
}

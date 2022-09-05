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
            // �̵�
            gameObject.transform.position -= dir * oriSpeed * Time.deltaTime;
            // ȸ��
            var rotDir = target.transform.position - gameObject.transform.position;
            rotDir.Normalize();
            if(rotDir != Vector3.zero)
            {
                var rotTarget = Quaternion.LookRotation(rotDir);
                gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotTarget, rotSpeed * Time.deltaTime);
            }
            // ���� �� ���� ����Ʈ�� ����
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
            // ���� �Ҹ� ����
            if (wayCount > wayPoint.Count)
            {
                Delete();
                // �÷��̾� ������ ����
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
        // WayPoint ��������
        var way = GameObject.FindWithTag("WayPoint").GetComponent<WayPoint>();
        var wayPoint = way.GetWayPoints();
        startPos = wayPoint[0];
        this.wayPoint = wayPoint;
        // �ʱ� ����
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
        // ���� ����
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
        // ���� �������� �Ǵ� ���ؼ� �ʿ�
        GameObject.FindWithTag("GameMgr").GetComponent<GameManager>().MobCount--;
    }
}

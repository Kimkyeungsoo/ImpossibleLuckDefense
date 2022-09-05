using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatus : MonoBehaviour
{
    // 공격력
    public int damage = 10;
    // 업그레이드 관련
    public int upgradeDmg = 1;
    public int upgradeLevel = 0;

    // 공격 속도
    public float attackTime = 1f;
    protected float timer;
    // 타겟
    protected GameObject target;
    // 회전
    private float rotSpeed = 5f;
    private Quaternion ori;
    // 애니메이션
    private Animator animator;
    private void Start()
    {
        ori = gameObject.transform.rotation;
        animator = GetComponent<Animator>();
    }

    protected void LookTarget()
    {
        if (target != null)
        {
            var rotDir = target.transform.position - gameObject.transform.position;
            rotDir.Normalize();
            var rotTarget = Quaternion.LookRotation(rotDir);
            rotTarget.x = gameObject.transform.rotation.x;
            rotTarget.z = gameObject.transform.rotation.z;
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotTarget, rotSpeed * Time.deltaTime);
            if (!target.gameObject.activeSelf)
            {
                target = null;
                animator.SetBool("IsAttack", false);
            }
        }
        else
        {
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, ori, rotSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (target == null && other.gameObject.activeSelf)
        {
            if (other.CompareTag("Monster") || other.CompareTag("QuestMonster"))
            {
                target = other.gameObject;
                animator.SetBool("IsAttack", true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target)
        {
            target = null;
            animator.SetBool("IsAttack", false);
        }
    }
}

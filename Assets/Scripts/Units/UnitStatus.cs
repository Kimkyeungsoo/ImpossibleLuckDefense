using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatus : MonoBehaviour
{
    // ���ݷ�
    public int damage = 10;
    // ���׷��̵� ����
    public int upgradeDmg = 1;
    public int upgradeLevel = 0;

    // ���� �ӵ�
    public float attackTime = 1f;
    protected float timer;
    // Ÿ��
    protected GameObject target;
    // ȸ��
    private float rotSpeed = 5f;
    private Quaternion ori;
    // �ִϸ��̼�
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

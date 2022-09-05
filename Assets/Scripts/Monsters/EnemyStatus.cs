using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    // 이동 속도
    public float oriSpeed = 3f;
    public float speed = 3f;
    // 체력
    protected float rotSpeed = 10f;
    // 공격력
    public int maxHp = 100;
    public int hp = 100;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWave : WaveInfo
{
    private float responDelayTime = 1.5f;

    private const int conMaxCount = 15;
    private int responCount = 0;

    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameMgr").GetComponent<GameManager>();
    }
    private IEnumerator CreateEnemy(WaveType nowType)
    {
        if(++responCount > conMaxCount)
        {
            responCount = 0;
            yield break;
        }
        var unit = MultipleObjectPooling.instance.GetPooledObject($"{nowType}");
        UnitCreate(unit);
        yield return new WaitForSeconds(responDelayTime);
        StartCoroutine(CreateEnemy(nowType));
    }
    public void StartWave()
    {
        if(type >= 3)
        {
            type = 0;
        }
        var nowType = (WaveType)type;
        gameManager.MobCount = conMaxCount;
        StartCoroutine(CreateEnemy(nowType));
        type++;
    }
    public void CreateBoss()
    {
        var unit = MultipleObjectPooling.instance.GetPooledObject("Boss");
        UnitCreate(unit);
        gameManager.MobCount = 1;
    }

    private void UnitCreate(GameObject gameObject)
    {
        var unitInfo = gameObject.GetComponent<EnemyMove>();
        unitInfo.maxHp = waveHp[waveId];
        unitInfo.hp = waveHp[waveId];
        unitInfo.oriSpeed = waveSpeed[waveId];
        unitInfo.MobSetting();
        gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public List<TileInfo> unitInfos = new List<TileInfo>();
    private void Start()
    {
        var tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach(var tile in tiles)
        {
            unitInfos.Add(tile.GetComponent<TileInfo>());
        }
    }

    public void ClassUp()
    {
        var sellect = Camera.main.GetComponent<TileClick>().sellectTile.GetComponent<TileInfo>();
        if(sellect.Level == 0)
        {
            Debug.Log("영웅이 없습니다.");
            return;
        }
        foreach (var findUnit in unitInfos)
        {
            if(sellect.unitName == findUnit.unitName && sellect.name != findUnit.name)
            {
                var unitName = NextLevel(sellect.unitName);
                if(unitName == null)
                {
                    return;
                }
                // 유닛 소환
                var nextUnit = MultipleObjectPooling.instance.GetPooledObject(unitName);
                nextUnit.SetActive(true);
                nextUnit.transform.position = sellect.Object.transform.position;
                
                InfoResetting(findUnit);
                NewSetting(sellect, nextUnit);
                return;
            }
        }
        Debug.Log("2마리 이상의 영웅이 있어야 합니다.");
    }

    public void InfoResetting(TileInfo findUnit)
    {
        findUnit.Level = 0;
        findUnit.unitName = string.Empty;
        findUnit.Object.SetActive(false);
        findUnit.Object = null;
    }

    private void NewSetting(TileInfo sellect, GameObject gameObject)
    {
        sellect.Object.SetActive(false);
        sellect.Object = gameObject;
        sellect.unitName = Regex.Replace(gameObject.name, @"\(Clone\)", "");
        sellect.Level = int.Parse(Regex.Replace(gameObject.name, @"\D", ""));
    }

    public string NextLevel(string str)
    {
        var temp = GameObject.FindWithTag("GameMgr").GetComponentInChildren<UnitMaker>();
        temp.Type();
        // 다음 레벨
        var num = int.Parse(Regex.Replace(str, @"\D", ""));
        if(num >= 4)
        {
            return null;
        }
        num++;
        // 타입
        var st = temp.Type();
        return st + num;
    }
}

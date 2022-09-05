using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitMaker : MonoBehaviour
{
    public TileManager tileManager;
    public TextMeshProUGUI heroSellBuyText;
    public SellctSuper sellectSuper;

    private string[] types = new string[6];
    private GameObject tile;
    private TileInfo tileInfo;
    private void Awake()
    {
        types[0] = "Ellect";
        types[1] = "Fire";
        types[2] = "Gun";
        types[3] = "Ice";
        types[4] = "Lay";
        types[5] = "Sword";
    }
    public void CreateUnit(string unitName, int level)
    {
        // 유닛 소환
        var unit = MultipleObjectPooling.instance.GetPooledObject(unitName);
        unit.SetActive(true);
        unit.transform.position = tile.transform.position;
        // Tile Info 셋팅
        tileInfo.unitName = unitName;
        tileInfo.Object = unit;
        tileInfo.Level = level;
    }

    public void RandomUnit()
    {
        var uiText = GameObject.FindWithTag("GameMgr").GetComponentInChildren<TextUI>();
        FindTileInfo();
        if (tileInfo.unitName != string.Empty)
        {
            heroSellBuyText.text = "영웅 소환";
            tileManager.InfoResetting(tileInfo);
            uiText.SetGold(100);
            return;
        }
        // 현재 다이아 체크
        if (uiText.GetDia() <= 0 || !CheckTileInfo())
        {
            return;
        }
        // 다이아 1씩 감소 ...
        uiText.SetDia(-1);

        int random = Random.Range(1, 1001);
        float probability = random / 10f; // 95%  4%  0.7%  0.3%
        int rating;
        int typeIdx = Random.Range(0, 6);

        if (probability <= 95f)
        {
            rating = 1;
        }
        else if (probability <= 99f)
        {
            rating = 2;
        }
        else if (probability <= 99.7f)
        {
            rating = 3;
        }
        else
        {
            rating = 4;
        }

        var unit = types[typeIdx] + $"{rating}";

        if(sellectSuper.SuperUnit != string.Empty)
        {
            rating = 3;
            unit = sellectSuper.SuperUnit + rating;
            sellectSuper.SuperUnit = string.Empty;
        }
        CreateUnit(unit, rating);
        heroSellBuyText.text = "영웅 판매";
    }
    private void FindTileInfo()
    {
        tile = Camera.main.GetComponent<TileClick>().sellectTile;
        tileInfo = tile.GetComponent<TileInfo>();
    }
    private bool CheckTileInfo()
    {
        if(tile == null || tileInfo.unitName != string.Empty)
        {
            return false;
        }
        return true;
    }

    public string Type()
    {
        return types[Random.Range(0, 6)];
    }
}

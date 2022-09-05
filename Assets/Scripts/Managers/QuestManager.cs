using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    private GameObject gameManagerObj;
    private GameManager gameManager;
    private TextUI textUI;
    public TileManager tileManager;
    public OnOffUI onOffUI;
    // 보스 프리펩
    public GameObject easyBossPrefab;
    public GameObject normalBossPrefab;
    public GameObject hardBossPrefab;

    private const float easyCooltime = 120f;
    private const float normalCooltime = 120f;
    private const float hardCooltime = 180f;
    // 버튼
    public GameObject rich;
    public GameObject highNoon;
    public GameObject lucky;
    public GameObject collector;
    public GameObject triple;
    public GameObject easyBoss;
    public GameObject normalBoss;
    public GameObject hardBoss;
    // 클리어 여부
    private bool richClear = false;
    private bool highNoonClear = false;
    private bool luckyClear = false;
    private bool collectorClear = false;
    private bool tripleClear = false;

    // 보상
    public GameObject SellectSuper;

    // text Color
    private Color halfColor;
    private Color fullColor;
    private void Start()
    {
        halfColor = new Color(1f, 1f, 1f, 0.5f);
        fullColor = new Color(1f, 1f, 1f, 1f);

        gameManagerObj = GameObject.FindWithTag("GameMgr");
        gameManager = gameManagerObj.GetComponent<GameManager>();
        textUI = gameManager.textUI;
        tileManager = gameManager.tileManager;
    }
    public void CheckQuestButton()
    {
        // 일반 퀘스트
        Rich();
        HighNoon();
        Lucky();
        Collector();
        Triple();
        // 보스 퀘스트
        BossCoolCheck(easyBoss, gameManager.easyTimer);
        BossCoolCheck(normalBoss, gameManager.normalTimer);
        BossCoolCheck(hardBoss, gameManager.hardTimer);
    }
    /**************************************************************************************************************
     * 일반 퀘스트
     * ************************************************************************************************************/
    private void Rich()
    {
        if (richClear)
        {
            return;
        }
        var dia = textUI.GetDia();
        NumberCheck(dia, 6, rich);
    }
    private void HighNoon()
    {
        if (highNoonClear)
        {
            return;
        }
        int countGun = 0;
        var list = tileManager.unitInfos;
        foreach (var go in list)
        {
            if (go.unitName == string.Empty)
            {
                continue;
            }
            if (go.unitName == "Gun1")
            {
                countGun++;
            }
        }
        NumberCheck(countGun, 4, highNoon);
    }
    private void Lucky()
    {
        if (luckyClear)
        {
            return;
        }
        int countLegend = 0;
        var list = tileManager.unitInfos;
        foreach (var go in list)
        {
            if (go.unitName == string.Empty)
            {
                continue;
            }
            int level = int.Parse(Regex.Replace(go.unitName, @"\D", ""));
            if (level == 4)
            {
                countLegend++;
            }
        }
        NumberCheck(countLegend, 4, lucky);
    }
    private void NumberCheck(int num, int condition, GameObject obj)
    {
        bool active;
        Color color;
        var texts = obj.GetComponentsInChildren<TextMeshProUGUI>();

        if (num >= condition)
        {
            active = true;
            color = fullColor;
            texts[texts.Length - 1].text = $"( 완료 ) {condition} / {condition}";
        }
        else
        {
            active = false;
            color = halfColor;
            texts[texts.Length - 1].text = $"( 미완료 ) {num} / {condition}";
        }
        QuestButtonState(obj.GetComponent<Button>(), texts, active, color);
    }
    private void Collector()
    {
        if (collectorClear)
        {
            return;
        }
        List<string> activeUnit = new List<string>();
        bool active = false;
        //int unitLevel = 4;
        var list = tileManager.unitInfos;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].unitName == string.Empty)
            {
                continue;
            }
            int level = int.Parse(Regex.Replace(list[i].unitName, @"\D", ""));
            activeUnit.Add(list[i].unitName);
        }
        if (activeUnit.Count >= 4)
        {
            activeUnit.Sort();
            for (int i = 0; i < activeUnit.Count - 1; i++)
            {
                if (activeUnit[i] == activeUnit[i + 1])
                {
                    activeUnit.RemoveAt(i + 1);
                    i--;
                    continue;
                }
                activeUnit[i] = Regex.Replace(activeUnit[i], @"\d", "");
                if (i + 1 == activeUnit.Count - 1)
                {
                    activeUnit[i + 1] = Regex.Replace(activeUnit[i + 1], @"\d", "");
                }
            }

            for (var i = 0; i < activeUnit.Count - 3; i++)
            {
                if (activeUnit[i] == activeUnit[i + 1])
                {
                    if (activeUnit[i + 1] == activeUnit[i + 2])
                    {
                        if (activeUnit[i + 2] == activeUnit[i + +3])
                        {
                            active = true;
                        }
                    }
                }
            }
        }

        TrueOrFalseCheck(collector, active);
    }
    private void Triple()
    {
        if (tripleClear)
        {
            return;
        }
        List<string> maxLevels = new List<string>();
        bool active = false;
        //int count = 0;
        var list = tileManager.unitInfos;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].unitName == string.Empty)
            {
                continue;
            }
            int level = int.Parse(Regex.Replace(list[i].unitName, @"\D", ""));
            if (level == 4)
            {
                maxLevels.Add(list[i].unitName);
            }
        }
        // 정렬
        if (maxLevels.Count >= 3)
        {
            maxLevels.Sort();
            for (int i = 0; i < maxLevels.Count - 1; i++)
            {
                if (maxLevels[i] == maxLevels[i + 1])
                {
                    i++;
                    if (maxLevels[i] == maxLevels[i + 1])
                    {
                        active = true;
                        break;
                    }
                }
            }
        }

        TrueOrFalseCheck(triple, active);
    }
    private void TrueOrFalseCheck(GameObject obj, bool active)
    {
        Color color;
        var texts = obj.GetComponentsInChildren<TextMeshProUGUI>();

        if (active)
        {
            color = fullColor;
            texts[texts.Length - 1].text = "( 완료 )";
        }
        else
        {
            color = halfColor;
            texts[texts.Length - 1].text = "( 미완료 )";
        }
        QuestButtonState(obj.GetComponent<Button>(), texts, active, color);
    }
    /**************************************************************************************************************
     * 보스 퀘스트
     * ************************************************************************************************************/
    public void EasyBoss()
    {
        Instantiate(easyBossPrefab);

        gameManager.easyTimer = easyCooltime;
        var texts = easyBoss.GetComponentsInChildren<TextMeshProUGUI>();
        texts[texts.Length - 1].text = $"{easyCooltime}초 후에 시작가능";

        texts[texts.Length - 1].color = Color.white;
        QuestButtonState(easyBoss.GetComponent<Button>(), texts, false, halfColor);
        gameManager.MobCount += 1;
    }
    public void NormalBoss()
    {
        Instantiate(normalBossPrefab);

        gameManager.normalTimer = normalCooltime;
        var texts = normalBoss.GetComponentsInChildren<TextMeshProUGUI>();
        texts[texts.Length - 1].text = $"{normalCooltime}초 후에 시작가능";

        texts[texts.Length - 1].color = Color.white;
        QuestButtonState(normalBoss.GetComponent<Button>(), texts, false, halfColor);
        gameManager.MobCount += 1;
    }
    public void HardBoss()
    {
        Instantiate(hardBossPrefab);

        gameManager.hardTimer = hardCooltime;
        var texts = hardBoss.GetComponentsInChildren<TextMeshProUGUI>();
        texts[texts.Length - 1].text = $"{hardCooltime}초 후에 시작가능";

        texts[texts.Length - 1].color = Color.white;
        QuestButtonState(hardBoss.GetComponent<Button>(), texts, false, halfColor);
        gameManager.MobCount += 1;
    }
    private void BossCoolCheck(GameObject obj, float timer)
    {
        var texts = obj.GetComponentsInChildren<TextMeshProUGUI>();

        if(timer <= 0f)
        {
            QuestButtonState(obj.GetComponent<Button>(), texts, true, fullColor);
            texts[texts.Length - 1].color = Color.yellow;
            texts[texts.Length - 1].text = $"( 퀘스트 준비완료! )";
            return;
        }
        texts[texts.Length - 1].text = $"{(int)timer}초 후에 시작가능";
    }
    /**************************************************************************************************************/
    public void QuestButtonState(Button button, TextMeshProUGUI[] text, bool setActive, Color color)
    {
        button.interactable = setActive;
        foreach (var go in text)
        {
            go.color = color;
        }
    }
    /**************************************************************************************************************
     * 일반 퀘스트 클리어
     * ************************************************************************************************************/
    public void RichClick()
    {
        QuestButtonState(rich.GetComponent<Button>(), rich.GetComponentsInChildren<TextMeshProUGUI>(), false, halfColor);
        richClear = true;
        // 보상 = 300 골드
        textUI.SetGold(300);
    }
    public void HighNoonClick()
    {
        QuestButtonState(highNoon.GetComponent<Button>(), highNoon.GetComponentsInChildren<TextMeshProUGUI>(), false, halfColor);
        highNoonClear = true;
        // 보상 = 다음 소환시 원하는 속성 level3
        onOffUI.QuestButton();
        SellectSuper.SetActive(true);
        Time.timeScale = 0f;
    }
    public void LuckyClick()
    {
        QuestButtonState(lucky.GetComponent<Button>(), lucky.GetComponentsInChildren<TextMeshProUGUI>(), false, halfColor);
        luckyClear = true;
        // 보상 = 보석 2개
        textUI.SetDia(2);
    }
    public void CollectorClick()
    {
        QuestButtonState(collector.GetComponent<Button>(), collector.GetComponentsInChildren<TextMeshProUGUI>(), false, halfColor);
        collectorClear = true;
        // 보상 = 1000 골드
        textUI.SetGold(1000);
    }
    public void TripleClick()
    {
        QuestButtonState(triple.GetComponent<Button>(), triple.GetComponentsInChildren<TextMeshProUGUI>(), false, halfColor);
        tripleClear = true;
        // 보상 = 1000 골드
        textUI.SetGold(1000);
    }

    /**************************************************************************************************************
     * 보스 퀘스트 시작
     * ************************************************************************************************************/
    public void StartEasyBoss()
    {
        Instantiate(easyBossPrefab);
    }
}

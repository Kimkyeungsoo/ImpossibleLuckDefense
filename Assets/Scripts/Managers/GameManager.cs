using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject gameState;                // 게임 상태 관리( 클리어 or 게임 오버 )
    public MonsterWave monsterWave;             // 웨이브 관리
    public TextUI textUI;                       // 상부 TextUI 관리
    public MultipleObjectPooling pooling;       // ObjectPooling 관리
    public TileManager tileManager;             // 타일 정보
    public TileClick tileClick;                 // 체크 된 타일 관리
    public UpGradeDmg upGradeDmg;               // 유닛들 공격력 관리
    public SceneControler sceneControler;       // 씬 관리

    public GameObject createButton;             // 영웅생성판매 버튼 관리
    public GameObject classUpButton;            // 클래스업글 버튼 관리
    public GameObject questButton;              // 퀘스트 버튼 관리
    private Color halfColor;                    // 반투명
    private Color fullColor;                    // 원색

    public float easyTimer = 0f;                // 퀘스트 쿨타임
    public float normalTimer = 0f;              //
    public float hardTimer = 0f;                //

    public bool isBuffe = false;                // 버프 지속 시간 관리
    private float buffeTime = 90f;              //
    public float buffeTimer = 0f;               //
    public int MobCount                         // 웨이브 상태
    {
        get;
        set;
    } = -1;

    private void Start()
    {
        halfColor = new Color(1f, 1f, 1f, 0.5f);
        fullColor = new Color(1f, 1f, 1f, 1f);

        Time.timeScale = 1;
        ButtonState(questButton.GetComponent<Button>(), questButton.GetComponentInChildren<TextMeshProUGUI>(), false, halfColor);
    }
    private void Update()
    {
        // 클리어 조건
        if (textUI.GetWave() > 18)
        {
            gameState.SetActive(true);
            gameState.GetComponentInChildren<TextMeshProUGUI>().text = "게임 클리어";
        }
        if(gameState.activeSelf)
        {
            if(Input.GetMouseButtonDown(0))
            {
                sceneControler.MenuScene();
                Debug.Log("메뉴 씬 이동");
            }
            return;
        }

        // 버프 지속 시간
        if (isBuffe)
        {
            buffeTimer += Time.deltaTime;
            if(buffeTimer > buffeTime)
            {
                var units = upGradeDmg.pool.GetComponentsInChildren<UnitStatus>(true);
                foreach(var unit in units)
                {
                    unit.upgradeLevel -= 20;
                }
                isBuffe = false;
            }
        }
        // 보스 퀘스트 쿨타임
        if (easyTimer > 0f)
        {
            easyTimer -= Time.deltaTime;
        }
        if (normalTimer > 0f)
        {
            normalTimer -= Time.deltaTime;
        }
        if (hardTimer > 0f)
        {
            hardTimer -= Time.deltaTime;
        }
        //
        textUI.TextUIUpdate();

        bool isBool;
        Color color;
        // 선택 타일 체크
        if (tileClick.sellectTile == null)
        {
            isBool = false;
            color = halfColor;
        }
        else
        {
            isBool = true;
            color = fullColor;
        }
        ButtonState(createButton.GetComponent<Button>(), createButton.GetComponentInChildren<TextMeshProUGUI>(), isBool, color);
        ButtonState(classUpButton.GetComponent<Button>(), classUpButton.GetComponentInChildren<TextMeshProUGUI>(), isBool, color);
        //웨이브 클리어 시 타이머 셋팅
        if (MobCount == 0)
        {
            ButtonState(questButton.GetComponent<Button>(), questButton.GetComponentInChildren<TextMeshProUGUI>(), false, halfColor);
            NextWave();
            monsterWave.waveId++;
        }
        // 웨이브 소환
        if (textUI.GetTimer() <= 0f && MobCount < 0)
        {
            if(textUI.GetWave() % 5 == 0 || textUI.GetWave() == monsterWave.waveHp.Count)
            {
                monsterWave.CreateBoss();
                return;
            }
            ButtonState(questButton.GetComponent<Button>(), questButton.GetComponentInChildren<TextMeshProUGUI>(), true, fullColor);
            monsterWave.StartWave();
        }
    }
    /***********************************************************
     * 버튼 UI 활성화 비활성화 관리
     * *********************************************************/
    public void ButtonState(Button button, TextMeshProUGUI text, bool setActive, Color color)
    {
        button.interactable = setActive;
        text.color = color;
    }

    /***********************************************************
     * 웨이브
     * *********************************************************/
    private void NextWave()
    {
        MobCount = -1;
        textUI.SetTimer(15f);
        textUI.SetDia(3);
        textUI.SetWave();
    }
}

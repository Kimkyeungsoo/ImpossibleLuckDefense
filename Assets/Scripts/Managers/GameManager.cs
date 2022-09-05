using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject gameState;                // ���� ���� ����( Ŭ���� or ���� ���� )
    public MonsterWave monsterWave;             // ���̺� ����
    public TextUI textUI;                       // ��� TextUI ����
    public MultipleObjectPooling pooling;       // ObjectPooling ����
    public TileManager tileManager;             // Ÿ�� ����
    public TileClick tileClick;                 // üũ �� Ÿ�� ����
    public UpGradeDmg upGradeDmg;               // ���ֵ� ���ݷ� ����
    public SceneControler sceneControler;       // �� ����

    public GameObject createButton;             // ���������Ǹ� ��ư ����
    public GameObject classUpButton;            // Ŭ�������� ��ư ����
    public GameObject questButton;              // ����Ʈ ��ư ����
    private Color halfColor;                    // ������
    private Color fullColor;                    // ����

    public float easyTimer = 0f;                // ����Ʈ ��Ÿ��
    public float normalTimer = 0f;              //
    public float hardTimer = 0f;                //

    public bool isBuffe = false;                // ���� ���� �ð� ����
    private float buffeTime = 90f;              //
    public float buffeTimer = 0f;               //
    public int MobCount                         // ���̺� ����
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
        // Ŭ���� ����
        if (textUI.GetWave() > 18)
        {
            gameState.SetActive(true);
            gameState.GetComponentInChildren<TextMeshProUGUI>().text = "���� Ŭ����";
        }
        if(gameState.activeSelf)
        {
            if(Input.GetMouseButtonDown(0))
            {
                sceneControler.MenuScene();
                Debug.Log("�޴� �� �̵�");
            }
            return;
        }

        // ���� ���� �ð�
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
        // ���� ����Ʈ ��Ÿ��
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
        // ���� Ÿ�� üũ
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
        //���̺� Ŭ���� �� Ÿ�̸� ����
        if (MobCount == 0)
        {
            ButtonState(questButton.GetComponent<Button>(), questButton.GetComponentInChildren<TextMeshProUGUI>(), false, halfColor);
            NextWave();
            monsterWave.waveId++;
        }
        // ���̺� ��ȯ
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
     * ��ư UI Ȱ��ȭ ��Ȱ��ȭ ����
     * *********************************************************/
    public void ButtonState(Button button, TextMeshProUGUI text, bool setActive, Color color)
    {
        button.interactable = setActive;
        text.color = color;
    }

    /***********************************************************
     * ���̺�
     * *********************************************************/
    private void NextWave()
    {
        MobCount = -1;
        textUI.SetTimer(15f);
        textUI.SetDia(3);
        textUI.SetWave();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TileClick : MonoBehaviour
{
    public LayerMask clickableLayer;

    public Material tile;

    public Color CheckColor;

    private Renderer sellectRender;
    public GameObject sellectTile;

    // sellectTransform�� null�� �ƴ϶�� ���� ��ȯ�� Ŭ���� ���� ��ư Ȱ��ȭ
    public TextMeshProUGUI heroSellBuyText;

    private void Awake()
    {
        CheckColor = Color.black;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = Physics.RaycastAll(ray).OrderBy(h => h.distance).Where(h => h.transform.tag == "Tile").FirstOrDefault();

            if (sellectRender != null)// && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                sellectRender.material.color = sellectTile.GetComponent<TileInfo>().oriColor;
                sellectRender = null;
                sellectTile = null;
                heroSellBuyText.text = "����";
            }

            if (hit.transform != null)
            {
                sellectRender = hit.transform.gameObject.transform.GetChild(0).GetComponent<Renderer>();
                sellectTile = hit.transform.gameObject;
                sellectRender.material.color = CheckColor;

                heroSellBuyText.text = "���� ��ȯ";
                if (sellectTile.GetComponent<TileInfo>().unitName != string.Empty)
                {
                    heroSellBuyText.text = "���� �Ǹ�";
                }
            }
        }
    }

    public GameObject SellectedTile()
    {
        return sellectTile;
    }
}

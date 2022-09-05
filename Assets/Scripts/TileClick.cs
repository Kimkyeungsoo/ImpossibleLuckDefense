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

    // sellectTransform이 null이 아니라면 영웅 소환과 클래스 업글 버튼 활성화
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
                heroSellBuyText.text = "영웅";
            }

            if (hit.transform != null)
            {
                sellectRender = hit.transform.gameObject.transform.GetChild(0).GetComponent<Renderer>();
                sellectTile = hit.transform.gameObject;
                sellectRender.material.color = CheckColor;

                heroSellBuyText.text = "영웅 소환";
                if (sellectTile.GetComponent<TileInfo>().unitName != string.Empty)
                {
                    heroSellBuyText.text = "영웅 판매";
                }
            }
        }
    }

    public GameObject SellectedTile()
    {
        return sellectTile;
    }
}

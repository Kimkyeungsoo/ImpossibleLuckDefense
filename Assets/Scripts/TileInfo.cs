using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public Color oriColor;
    public int Level
    {
        get;
        set;
    } = 0;
    public string unitName
    {
        get;
        set;
    } = string.Empty;
    public GameObject Object
    {
        get;
        set;
    } = null;

    private void Start()
    {
        oriColor = GetComponentInChildren<MeshRenderer>().material.color;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveInfo : MonoBehaviour
{
    public int waveId = -1;
    protected int type = 0;
    public List<int> waveHp = new List<int>();
    public List<float> waveSpeed = new List<float>();
    protected enum WaveType
    {
        GreenCube,
        BlueCube,
        BlackCube,
    }
}

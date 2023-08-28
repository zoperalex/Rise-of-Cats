using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExpDrop
{
    [Range(0,1)] public float dropChance;
    public int amountMin;
    public int amountMax;
}

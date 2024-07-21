using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingBase : MonoBehaviour
{
    [HideInInspector] public int currentLevel = 1;
    [HideInInspector] public int currentHP;

    [HideInInspector] public int[] hpPerLevel;
    [HideInInspector] public int[] upgradePrices;

    [HideInInspector] public int[] profitPerLevel;
    [HideInInspector] public int[] damagePerLevel;

    [HideInInspector] public float attackSpeed = 1f;
    [HideInInspector] public int[] attackSpeedPerLevel;


}


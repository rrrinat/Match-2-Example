using System.Collections;
using System.Collections.Generic;
using Match2.Scripts.Core.Enums;
using Match2.Scripts.Static;
using UnityEngine;

public class LevelInfo
{
    public int LevelIndex;
    public List<GoalInfo> Goals;
    public CellType[,] CellsData;
}

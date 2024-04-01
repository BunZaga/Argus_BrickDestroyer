using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public List<BrickGridData> BrickList => brickList;
    [SerializeField] private List<BrickGridData> brickList = new List<BrickGridData>();
}

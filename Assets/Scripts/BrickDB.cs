using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BrickDB", order = 1)]
public class BrickDB : ScriptableObject
{
    public List<GameObject> brickPrefab;
}

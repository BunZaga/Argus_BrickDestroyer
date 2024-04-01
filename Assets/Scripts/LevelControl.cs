using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    [SerializeField] private List<TextAsset> levels;
    [SerializeField] private BrickDB brickDB;
    
    public LevelData CurrentLevel => currentLevel;
    [SerializeField] private LevelData currentLevel;

    private int currentLevelIndex = 0;
    private int brickLayer;

    private void Awake()
    {
        brickLayer = LayerMask.NameToLayer("Brick");
    }

    public void SetAndLoadLevel(int level)
    {
        if (level > -1)
        {
            currentLevelIndex = Mathf.Max(0, Mathf.Min(levels.Count-1, level));
        }
        LoadLevel();
    }

    public void LoadNextLevel()
    {
        currentLevelIndex = Mathf.Max(0, Mathf.Min(levels.Count-1, currentLevelIndex + 1));
        LoadLevel();
    }
    
    private void LoadLevel()
    {
        var levelString = levels[currentLevelIndex].ToString();
        var levelLines = levelString.Split("`");
        currentLevel = new LevelData();
        for (int i = 0, ilen = levelLines.Length; i < ilen; ++i)
        {
            var lineData = levelLines[i].Split("|");
            for (int j = 0, jlen = lineData.Length; j < jlen; ++j)
            {
                var brickData = lineData[j].Split(",");
                currentLevel.BrickList.Add(new BrickGridData(Int32.Parse(brickData[0]), Int32.Parse(brickData[1]), Int32.Parse(brickData[2])));
            }
        }
        
        Vector3 gridPosition = Vector3.zero;
        Vector3 localPosition = Vector3.zero;
        
        for (int i = 0, ilen = currentLevel.BrickList.Count; i < ilen; i++)
        {
            int index = i;
            GameObject newBrickGO = Instantiate(brickDB.brickPrefab[currentLevel.BrickList[index].BrickIndex].gameObject);
            BrickComponent newBrickComponent = newBrickGO.GetComponent<BrickComponent>();
            newBrickGO.layer = brickLayer;
            newBrickGO.tag = "Brick";
            gridPosition.x = currentLevel.BrickList[index].X;
            gridPosition.y = currentLevel.BrickList[index].Y;
            localPosition = GameGrid.Instance.ConvertToWorldSpace(gridPosition);
            newBrickComponent.MovePosition(localPosition);
            newBrickComponent.transform.SetParent(GameGrid.Instance.BrickRoot);
            newBrickComponent.transform.localPosition = localPosition;
            newBrickComponent.SetKinematic(true);
        }
        
        GameGrid.EvtGameLoaded.Invoke();
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    public static GameEvent EvtProjectileShot = new GameEvent();
    public static GameEvent EvtBlockHit = new GameEvent();
    public static GameEvent EvtBlockDestroyed = new GameEvent();
    public static GameEvent EvtGameOver = new GameEvent();
    public static GameEvent EvtGameStart = new GameEvent();
    public static GameEvent EvtLoadNewLevel = new GameEvent();
    public static GameEvent EvtGameLoaded = new GameEvent();
    public static GameEvent EvtOpenSwapAmmo = new GameEvent();
    public static GameEvent EvtCloseSwapAmmo = new GameEvent();
    
    public static GameGrid Instance => instance;
    private static GameGrid instance = null;

    public static bool GameInPlay => gameInPlay;
    private static bool gameInPlay = false;
    
    [SerializeField] private float height = 0.0f;
    [SerializeField] private float hOffset = 0.0f;
    [SerializeField] private float width = 0.0f;
    [SerializeField] private float vSpace = 0.0f;
    [SerializeField] private List<GameObject> gamePieces;
    
    public Transform BrickRoot => brickRoot;
    [SerializeField] private Transform brickRoot;

    [SerializeField] private LevelControl levelControl;

    [SerializeField] private SOGameEvent newAmmoEvent;
    public static bool FoundNewAmmo = false;
    
    private int bricksToRemoveCount = 0;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        
        EvtGameStart.AddListener(OnGameStart);
        EvtGameOver.AddListener(OnGameOver);
        EvtGameLoaded.AddListener(OnLevelLoaded);
        newAmmoEvent.AddListener(OnNewAmmo);
        EvtCloseSwapAmmo.AddListener(OnSwapAmmoClose);
    }

    private void Start()
    {
        for (int i = 0, ilen = gamePieces.Count; i < ilen; ++i)
        {
            gamePieces[i].SetActive(false);
        }
    }
    
    private void OnGameStart()
    {
        for (int i = 0, ilen = gamePieces.Count; i < ilen; ++i)
        {
            gamePieces[i].SetActive(true);
        }
        LoadLevel(0);
    }
    
    private void OnGameOver()
    {
        gameInPlay = false;
    }
    
    private void OnLevelLoaded()
    {
        bricksToRemoveCount = levelControl.CurrentLevel.BrickList.Count;
        gameInPlay = true;
    }
    
    public void LoadLevel(int level)
    {
        EvtLoadNewLevel.Invoke();
        levelControl.SetAndLoadLevel(level);
    }
    
    public void LoadNextLevel()
    {
        EvtLoadNewLevel.Invoke();
        levelControl.LoadNextLevel();
    }
    
    public void IncrementBricksRemoved()
    {
        instance.bricksToRemoveCount--;
        if (instance.bricksToRemoveCount <= 0)
        {
            // game over!
            Debug.Log("Game Over!");
            EvtGameOver.Invoke();
        }
    }

    private void OnNewAmmo()
    {
        if (FoundNewAmmo)
            return;

        gameInPlay = false;
        FoundNewAmmo = true;
        newAmmoEvent.RemoveListener(OnNewAmmo);
        EvtOpenSwapAmmo.Invoke();
    }

    private void OnSwapAmmoClose()
    {
        gameInPlay = true;
    }
    
    public Vector3 ConvertToWorldSpace(Vector3 gridPosition)
    {
        Vector3 localPosition = new Vector3();
        localPosition.y = -((gridPosition.y * height) + transform.localPosition.y + (vSpace * gridPosition.y));
        localPosition.x = (0.05f + (gridPosition.x * width)) + (hOffset * gridPosition.x);
        
        return localPosition;
    }
    
    private void OnDestroy()
    {
        newAmmoEvent.RemoveListener(OnNewAmmo);
    }
}

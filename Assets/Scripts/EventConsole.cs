using System.Collections.Generic;
using UnityEngine;

public class EventConsole : MonoBehaviour
{
    [SerializeField] private GameObject consoleTextPrefab;
    [SerializeField] private Transform consoleRootTransform;
    private Queue<GameObject> consoleOutput = new Queue<GameObject>();

    private const string ProjectileShot = "A projectile was shot";
    private const string BrickHit = "A projectile has hit a block";
    private const string BrickDestroyed = "A block was destroyed";
    
    
    private int maxSize = 11;

    private void Awake()
    {
        GameGrid.EvtProjectileShot.AddListener(OnProjectileShot);
        GameGrid.EvtBlockHit.AddListener(OnBlockHit);
        GameGrid.EvtBlockDestroyed.AddListener(OnBlockDestroyed);
        GameGrid.EvtGameLoaded.AddListener(OnGameLoaded);
    }

    private void Start()
    {
        // this is to avoid a studder the first time one of these is created...
        Destroy(consoleRootTransform.GetChild(1).gameObject);
    }

    private void OnProjectileShot()
    {
        AddNewConsoleText(ProjectileShot);
    }

    private void OnBlockHit()
    {
        AddNewConsoleText(BrickHit);
    }

    private void OnBlockDestroyed()
    {
        AddNewConsoleText(BrickDestroyed);
    }

    private void OnGameLoaded()
    {
        while (consoleOutput.Count > 0)
        {
            var child = consoleOutput.Dequeue();
            // TODO make this an object pool
            Destroy(child);
        }
    }
    
    private void AddNewConsoleText(string text)
    {
        GameObject newText = Instantiate(consoleTextPrefab, consoleRootTransform, true);
        newText.transform.localScale = Vector3.one;
        ConsoleText cText = newText.GetComponent<ConsoleText>();
        cText.SetText(text);
        
        consoleOutput.Enqueue(newText);
        
        while (consoleOutput.Count > maxSize)
        {
            var child = consoleOutput.Dequeue();
            // TODO make this an object pool
            Destroy(child);
        }
    }
}

using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class AmmoIcon : MonoBehaviour
{
    [SerializeField] private TextMeshPro ammoCount;
    [SerializeField] private SOGameIntEvent soGameEvent;
    public Material Background => background;
    [SerializeField] private Material background;
    
    private void Awake()
    {
        if (soGameEvent == null)
            return;
        
        soGameEvent.SetEventHandler(UpdateAmmoCount);
    }

    public void UpdateAmmoCount(int amount)
    {
        ammoCount.text = amount.ToString();
    }

    private void OnDestroy()
    {
        if (soGameEvent == null)
            return;
        
        soGameEvent.RemoveListener(UpdateAmmoCount);
    }
}
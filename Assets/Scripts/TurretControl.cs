using System;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour
{
    [SerializeField] private List<GunControl> gunControls = new List<GunControl>();
    [SerializeField] private List<AmmoIcon> ammoIcons = new List<AmmoIcon>();
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color notSelectedColor;
    
    private int currentGunIndex = 0;
    private GunControl currentGunControl;
    [SerializeField] private LaserComponent laserComponent;
    [SerializeField] private LaserRenderer laserRenderer;
    
    private float nextShootTime;
    
    private void Awake()
    {
        AimInputControl.evtOnMouseUp.AddListener(OnMouseUp);
        AimInputControl.evtOnMouseCancel.AddListener(OnMouseCancel);
        GameGrid.EvtGameStart.AddListener(OnGameStart);
    }

    private void OnGameStart()
    {
        currentGunControl = gunControls[currentGunIndex];
        currentGunControl.ShowAnimation();
        EnableCorrectAmmoIcon();
    }
    
    private void EnableCorrectAmmoIcon()
    {
        for (int i = 0, ilen = ammoIcons.Count; i < ilen; ++i)
        {
            ammoIcons[i].Background.color = i == currentGunIndex ? selectedColor: notSelectedColor;
        }
    }
    
    private void OnMouseCancel()
    {
        if (!GameGrid.GameInPlay)
            return;
        
        if (laserComponent != null)
            laserComponent.ClearPath();
        
        if(laserRenderer != null)
            laserRenderer.HideLaser();
        
        if (currentGunControl == null)
            return;
    }
    
    private void OnMouseUp()
    {
        if (!GameGrid.GameInPlay)
            return;
        
        if (laserComponent != null)
            laserComponent.ClearPath();
        
        if(laserRenderer != null)
            laserRenderer.HideLaser();
        
        if (currentGunControl != null)
        {
            if (currentGunControl.AmmoAmount > currentGunControl.MinAmmoAmount)
            {
                if (AimInputControl.CanFire && currentGunControl.CanShoot())
                {
                    currentGunControl.ShootGun();
                    if (currentGunControl.AmmoAmount <= currentGunControl.MinAmmoAmount)
                        SwitchToDefaultGun();
                }
            }
            else
            {
                SwitchToDefaultGun();
            }
        }
    }

    private void SwitchToDefaultGun()
    {
        currentGunControl.HideAnimation();
        currentGunIndex = 0;
        currentGunControl = gunControls[currentGunIndex];
        currentGunControl.ShowAnimation();
        EnableCorrectAmmoIcon();
    }
    
    private void Update()
    {
        if (!GameGrid.GameInPlay)
            return;
        
        if (AimInputControl.InputDirection.magnitude == 0)
            return;

        if (GameGrid.FoundNewAmmo)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                int wantedIndex = (currentGunIndex + 1);
                if (wantedIndex > gunControls.Count - 1)
                {
                    wantedIndex = 0;
                }

                currentGunControl.HideAnimation();
                currentGunIndex = wantedIndex;
                currentGunControl = gunControls[currentGunIndex];
                currentGunControl.ShowAnimation();
                EnableCorrectAmmoIcon();
            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                int wantedIndex = (currentGunIndex - 1);
                if (wantedIndex < 0)
                {
                    wantedIndex = gunControls.Count - 1;
                }

                currentGunControl.HideAnimation();
                currentGunIndex = wantedIndex;
                currentGunControl = gunControls[currentGunIndex];
                currentGunControl.ShowAnimation();
                EnableCorrectAmmoIcon();
            }
        }

        transform.rotation = Quaternion.LookRotation(AimInputControl.InputDirection, Vector3.back);
        if (currentGunControl != null && AimInputControl.CanFire)
        {
            HandleMouseMove();
        }
    }
    
    private void HandleMouseMove()
    {
        if (currentGunControl == null)
            return;
        
        if (laserComponent != null)
        {
            laserComponent.ClearPath();
            laserComponent.GeneratePath(transform.position, AimInputControl.InputDirection);
        }

        if (laserRenderer != null && Input.GetMouseButton(0) && AimInputControl.CanFire)
        {
            if (currentGunControl.AmmoAmount <= currentGunControl.MinAmmoAmount)
            {
                SwitchToDefaultGun();
                return;
            }
            laserRenderer.RenderLaser(Color.white, laserComponent.LinePaths);
        }
    }
}

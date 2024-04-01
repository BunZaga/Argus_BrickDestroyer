using UnityEngine;

public class GunControlLaserShooter : GunControl
{
    [SerializeField] private Transform launchTransform;
    [SerializeField] private float shotDelay;
    [SerializeField] private GameObject innerChild;
    
    private float nextShootTime;
    
    public static GunControlLaserShooter Instance => instance;
    private static GunControlLaserShooter instance = null;

    [SerializeField] private LaserComponent laserComponent;
    
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        
        ammoChangedEvent.AddListener(AmmoChanged);
        innerChild.SetActive(false);
    }
    
    public override void ShootGun()
    {
        if (laserComponent != null)
        {
            laserComponent.ClearPath();
            laserComponent.GeneratePath(launchTransform.position, AimInputControl.InputDirection);
        }
        
        GameGrid.EvtProjectileShot.Invoke();
        ammoChangedEvent.Invoke(-1);
        nextShootTime = Time.time + shotDelay;
        GameObject nextShot = Instantiate(Ammo.gameObject, null, true);
        nextShot.transform.position = launchTransform.position;
        nextShot.transform.rotation = launchTransform.rotation;
        
        var ammo = nextShot.GetComponent<AmmoLazer>();
        
        ammo.FindBricksHit(laserComponent.LinePaths);
        ammo.DamageBricks();
    }
    

    public override bool CanShoot()
    {
        if (Time.time >= nextShootTime && ammoAmount > minAmmoAmount)
        {
            return true;
        }
        return false;
    }

    public override void HideAnimation()
    {
        innerChild.SetActive(false);
    }

    public override void ShowAnimation()
    {
        innerChild.SetActive(true);
    }
}

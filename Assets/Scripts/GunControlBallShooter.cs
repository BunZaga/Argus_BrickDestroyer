using UnityEngine;

public class GunControlBallShooter : GunControl
{
    [SerializeField] private Transform launchTransform;
    [SerializeField] private float shotDelay;
    [SerializeField] private GameObject innerChild;
    
    private float nextShootTime;
    
    public static GunControlBallShooter Instance => instance;
    private static GunControlBallShooter instance = null;
    
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
    
    public override bool CanShoot()
    {
        if (Time.time >= nextShootTime)
        {
            return true;
        }
        return false;
    }
    
    public override void ShootGun()
    {
        GameGrid.EvtProjectileShot.Invoke();
        ammoChangedEvent.Invoke(-1);
        nextShootTime = Time.time + shotDelay;
        GameObject nextShot = Instantiate(Ammo.gameObject, null, true);
        nextShot.transform.position = launchTransform.position;
        nextShot.transform.rotation = launchTransform.rotation;
        
        var ammo = nextShot.GetComponent<AmmoBall>();
        ammo.Rigidbody.AddForce(AimInputControl.InputDirection * ammo.Speed);
    }
    
    // TODO: object pool enqueu here?
    public void DestroyAmmo(AmmoBall ammo)
    {
        Destroy(ammo.gameObject);
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

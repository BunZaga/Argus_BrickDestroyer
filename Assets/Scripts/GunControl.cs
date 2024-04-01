using UnityEngine;

public abstract class GunControl : MonoBehaviour
{
    public GameObject Ammo => ammo;
    [SerializeField] private GameObject ammo;
    public int AmmoAmount => ammoAmount;
    protected int ammoAmount = 0;
    public int MinAmmoAmount => minAmmoAmount;
    [Tooltip("-1 == infinite")]
    [SerializeField] protected int minAmmoAmount = 0;
    [SerializeField] protected int maxAmmoAmount = 999;
    [SerializeField] protected SOGameIntEvent ammoChangedEvent;
    [SerializeField] protected SOGameIntEvent ammoUpdateEvent;
    
    public abstract void ShootGun();
    
    public abstract bool CanShoot();

    public abstract void HideAnimation();
    public abstract void ShowAnimation();
    
    protected void AmmoChanged(int amount)
    {
        if (minAmmoAmount > -1)
        {
            ammoAmount = Mathf.Max(minAmmoAmount, Mathf.Min(maxAmmoAmount, ammoAmount + amount));
            ammoUpdateEvent.Invoke(ammoAmount);
        }
    }
}

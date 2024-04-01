using UnityEngine;

public class BrickComponentBasic : BrickComponent
{
    [SerializeField] private int ammoGrant;
    [SerializeField] private SOGameIntEvent ammoChangedEvent;
    [SerializeField] private SOGameEvent foundNewAmmo;
    
    public override void DestroyBrick()
    {
        if (ammoChangedEvent == null || ammoGrant == 0)
            return;
     
        if(foundNewAmmo != null)
            foundNewAmmo.Invoke();
        
        ammoChangedEvent.Invoke(ammoGrant);
    }
}

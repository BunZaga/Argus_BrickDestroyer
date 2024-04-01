using UnityEngine;

public class AmmoBall : Ammo
{
    public Rigidbody Rigidbody => mRigidbody;
    [SerializeField] private Rigidbody mRigidbody;

    public float Radius => radius;
    [SerializeField] private float radius;

    private float timeToLive = 30f;
    private float timeToDie;

    private void Start()
    {
        timeToDie = Time.time + timeToLive;
    }

    public override void DamageBricks()
    {
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (Time.time > timeToDie)
        {
            GunControlBallShooter.Instance.DestroyAmmo(this);
            return;
        }
        
        switch (other.gameObject.tag)
        {
            case "Brick":
                var brickData = other.gameObject.GetComponent<BrickComponent>();
                if (brickData == null)
                {
                    Debug.LogWarning("Could not locate BrickData on gameobject:"+other.gameObject.name);
                    return;
                }
                GameGrid.EvtBlockHit.Invoke();
                brickData.TakeDamage(Damage);
                break;
            default:
                // todo: sound effect?
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "DeadZone":
                GunControlBallShooter.Instance.DestroyAmmo(this);
                return; 
        }
    }
}

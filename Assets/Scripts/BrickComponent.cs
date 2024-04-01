using UnityEngine;

public abstract class BrickComponent : MonoBehaviour
{
    public int Health => health;
    [SerializeField] private int health;

    [SerializeField] private Rigidbody mRigidbody;

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            GameGrid.EvtBlockDestroyed.Invoke();
            GameGrid.Instance.IncrementBricksRemoved();
            DestroyBrick();
            Destroy(gameObject);
        }
    }

    public abstract void DestroyBrick();
    
    public void MovePosition(Vector3 position)
    {
        mRigidbody.MovePosition(position);
    }
    
    public void SetKinematic(bool value)
    {
        mRigidbody.isKinematic = value;
    }
}

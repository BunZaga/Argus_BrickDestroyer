using System.Collections.Generic;
using UnityEngine;

public abstract class Ammo: MonoBehaviour
{
    public int Damage => damage;
    [SerializeField] private int damage = 1;
    
    public float Spread => spread;
    [SerializeField] private float spread = 0;

    public int Speed => speed;
    [SerializeField] private int speed = 20;
    
    public abstract void DamageBricks();
}
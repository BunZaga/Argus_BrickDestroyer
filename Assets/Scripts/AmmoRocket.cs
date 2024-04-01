using System.Collections.Generic;

public class AmmoRocket : Ammo
{
    public List<BrickComponent> BricksHit => bricksHit;
    private List<BrickComponent> bricksHit = new List<BrickComponent>();
    
    public override void DamageBricks()
    {
        for (int i = 0, ilen = BricksHit.Count; i < ilen; ++i)
        {
            Destroy(BricksHit[i].gameObject);
        }
        Destroy(gameObject);
    }
}

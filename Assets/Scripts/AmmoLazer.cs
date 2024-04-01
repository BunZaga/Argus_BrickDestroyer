using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoLazer : Ammo
{
    public List<GameObject> BricksHit => bricksHit;
    private List<GameObject> bricksHit = new List<GameObject>();

    [SerializeField] private LaserRenderer laserRenderer;

    private List<RaycastHit> linePoints = new List<RaycastHit>();
    
    public void FindBricksHit(List<RaycastHit> linePath)
    {
        bricksHit.Clear();
        linePoints = new List<RaycastHit>(linePath);
        RaycastHit firstBrickHit = new RaycastHit();
        bool brickHit = false;
        int previousIndex = linePoints.Count - 1;
        
        for (int i = linePoints.Count - 1; i > 0; --i)
        {
            if (!linePoints[i].collider.gameObject.CompareTag("Brick"))
                continue;

            previousIndex = i - 1;
            firstBrickHit = linePoints[i];
            brickHit = true;
            break;
        }

        if (brickHit)
        {
            var position = linePoints[previousIndex].point;
            var direction = (firstBrickHit.point - position).normalized;
            RaycastHit[] hits = Physics.RaycastAll(position, direction, 100.0F);

            RaycastHit furthestHit = firstBrickHit;
            float furthestDistance = float.NegativeInfinity;
            
            for (int i = 0, ilen = hits.Length; i < ilen; ++i)
            {
                if (hits[i].collider.CompareTag("Brick"))
                {
                    bricksHit.Add(hits[i].collider.gameObject);
                }

                var distance = Vector3.Distance(position, hits[i].point);
                
                if (distance > furthestDistance)
                {
                    furthestHit = hits[i];
                    furthestDistance = distance;
                }
            }

            linePoints[linePoints.Count - 1] = furthestHit;
        }
    }
    
    private IEnumerator DestroyBricks()
    {
        for (int i = 0, ilen = bricksHit.Count; i < ilen; ++i)
        {
            var brickComponent = bricksHit[i].GetComponent<BrickComponent>();
            if (brickComponent == null)
                continue;
            
            brickComponent.TakeDamage(Damage);
        }
        yield return new WaitForSeconds(0.2f);
        laserRenderer.HideLaser();
        laserRenderer.DestroyLaser();
        Destroy(gameObject);
    }
    
    public override void DamageBricks()
    {
        if(laserRenderer != null)
            laserRenderer.RenderLaser(Color.green, linePoints);

        StartCoroutine(DestroyBricks());
    }
}

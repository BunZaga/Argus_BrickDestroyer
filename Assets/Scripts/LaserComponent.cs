using System.Collections.Generic;
using UnityEngine;

public class LaserComponent : MonoBehaviour
{
    public List<RaycastHit> LinePaths => linePoints;
    private List<RaycastHit> linePoints = new List<RaycastHit>();
    
    [SerializeField] private LayerMask pathRaycastLayer;
    
    public void GeneratePath(Vector3 startPosition, Vector3 direction)
    {
        if (linePoints.Count > 9)
            return;
        
        RaycastHit hit;
        
        if (Physics.Raycast(startPosition, direction, out hit, 100f, pathRaycastLayer.value))
        {
            if (linePoints.Count == 0)
            {
                linePoints.Add(new RaycastHit
                {
                    distance = 0.0f,
                    normal = direction,
                    point = startPosition,
                    barycentricCoordinate = Vector3.zero
                });
            }
            
            Vector3 reflecton;
            switch (hit.collider.gameObject.tag)
            {
                case "Brick":
                    linePoints.Add(hit);
                    break;
                case "Wall":
                    linePoints.Add(hit);
                    reflecton = Vector3.Reflect(direction, hit.normal);
                    GeneratePath(hit.point, reflecton.normalized);
                    break;
                case "TopWall":
                    linePoints.Add(hit);
                    reflecton = Vector3.Reflect(direction, hit.normal);
                    GeneratePath(hit.point, reflecton.normalized);
                    break;

                default:
                    Debug.LogWarning("HIT:"+hit.collider.gameObject.tag);
                    break;

            }
        }
    }

    /*public void RenderPath()
    {
        if (linePoints.Count < 2)
        {
            ClearPath();
            return;
        }
        
        if (linePoints[linePoints.Count - 1].collider.tag == "Neighbor")
        {
            RaycastHit hit = linePoints[linePoints.Count - 1];
            Vector3 hitPos = hit.collider.transform.position;
            
            //debugHitPosHighlight.transform.position = hitPos;
            //debugHitPosHighlight.SetActive(true);
            
            hit.point = hitPos + (hit.normal * GameGrid.Instance.BallRadius);

            linePoints[linePoints.Count - 1] = hit;
        }

        else
        {
            //debugHitPosHighlight.SetActive(false);
        }
    }*/

    public void ClearPath()
    {
        linePoints.Clear();
    }
}

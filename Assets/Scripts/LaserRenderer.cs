using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRenderer : MonoBehaviour
{
    [SerializeField] Material laserMaterial;
    [SerializeField] private float laserStartWidth = 1.0f;
    [SerializeField] private float laserEndWidth = 0.1f;

    private GameObject laserMesh = null;
    private MeshRenderer meshRenderer;
    private Mesh mesh;
    private MeshFilter meshFilter;

    private void Awake()
    {
        laserMesh = new GameObject();

        meshRenderer = laserMesh.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = laserMaterial;
        meshFilter = laserMesh.AddComponent<MeshFilter>();
        mesh = new Mesh();
        mesh.MarkDynamic();
    }

    public void RenderLaser(Color color, List<RaycastHit> linePoints)
    {
        if (linePoints.Count < 2)
        {
            HideLaser();
            return;
        }

        if (laserMesh == null)
            return;
        
        laserMesh.SetActive(false);
        mesh.Clear();

        Vector3[] vertices = new Vector3[(linePoints.Count - 1) * 4];
        float totalDistance = 0.0f;
        for (int i = 0, ilen = linePoints.Count - 1; i < ilen; i++)
        {
            totalDistance += (linePoints[i].point - linePoints[i + 1].point).magnitude;
        }

        laserMaterial.mainTextureScale = new Vector2(1.0f, totalDistance / 3);

        laserMaterial.color = color;

        Vector3 distance;
        Vector3 direction;
        Vector3 laserDirectionBase;
        Vector3 rightSide;
        Vector3 leftSide;
        Vector3 laserDirectionTip;

        float distanceToGo = totalDistance;
        for (int i = 0, ilen = linePoints.Count - 1; i < ilen; i++)
        {
            float baseWidth = ((laserStartWidth - laserEndWidth) * (distanceToGo / totalDistance)) + laserEndWidth;
            distance = (linePoints[i + 1].point - linePoints[i].point);
            direction = distance.normalized;

            laserDirectionBase = Vector3.Cross(direction, Vector3.back);

            rightSide = laserDirectionBase * baseWidth * 0.5f;
            leftSide = -rightSide;
            if (i > 0)
            {
                vertices[(i * 4)] = linePoints[i].point + rightSide;
                vertices[(i * 4) + 1] = linePoints[i].point + leftSide;
            }
            else
            {
                vertices[(i * 4)] = linePoints[i].point + rightSide;
                vertices[(i * 4) + 1] = linePoints[i].point + leftSide;
            }

            distanceToGo -= distance.magnitude;
            float tipWidth = ((laserStartWidth - laserEndWidth) * (distanceToGo / totalDistance)) + laserEndWidth;

            laserDirectionTip = Vector3.Cross(-direction, Vector3.back);

            leftSide = laserDirectionTip * tipWidth * 0.5f;
            rightSide = -leftSide;

            vertices[(i * 4) + 2] = linePoints[i + 1].point + leftSide;
            vertices[(i * 4) + 3] = linePoints[i + 1].point + rightSide;
        }

        mesh.vertices = vertices;

        int[] triangles = new int[(linePoints.Count - 1) * 6];


        for (int i = 0, ilen = linePoints.Count - 1; i < ilen; i++)
        {
            triangles[(i * 6)] = (i * 4);
            triangles[(i * 6) + 1] = (i * 4) + 1;
            triangles[(i * 6) + 2] = (i * 4) + 2;
            triangles[(i * 6) + 3] = (i * 4) + 2;
            triangles[(i * 6) + 4] = (i * 4) + 3;
            triangles[(i * 6) + 5] = (i * 4);
        }

        mesh.triangles = triangles;
        
        Vector3[] normals = new Vector3[vertices.Length];

        for (int i = 0, ilen = normals.Length; i < ilen; i++)
        {
            normals[i] = Vector3.back;
        }

        mesh.normals = normals;


        List<Vector3> uv = new List<Vector3>();
        float distanceToGoRight = 0.0f;
        float distanceToGoLeft = 0.0f;
        for (int i = 0, ilen = linePoints.Count - 1; i < ilen; i++)
        {
            float uvWidthBase = ((vertices[(i * 4)] - vertices[(i * 4) + 1]).magnitude / (laserStartWidth));
            // 1, 0
            uv.Add(new Vector3(uvWidthBase / 3, uvWidthBase / 3 * distanceToGoRight, uvWidthBase / 3));
            // 0, 1
            uv.Add(new Vector3(0.0f, uvWidthBase / 3 * distanceToGoRight, uvWidthBase / 3));

            distanceToGoRight += ((vertices[(i * 4)] - vertices[(i * 4) + 3]).magnitude) / totalDistance;
            distanceToGoLeft += ((vertices[(i * 4) + 1] - vertices[(i * 4) + 2]).magnitude) / totalDistance;

            float uvWidthTip = ((vertices[(i * 4) + 2] - vertices[(i * 4) + 3]).magnitude / (laserStartWidth));
            // 0, 1
            uv.Add(new Vector3(0, uvWidthTip / 3 * distanceToGoRight, uvWidthTip / 3));
            // 1, 0
            uv.Add(new Vector3(uvWidthTip / 3, uvWidthTip / 3 * distanceToGoRight, uvWidthTip / 3));
        }

        mesh.SetUVs(0, uv);

        meshFilter.mesh = mesh;

        laserMesh.SetActive(true);
    }


    public void HideLaser()
    {
        if (laserMesh == null)
            return;
        laserMesh.SetActive(false);
    }

    public void DestroyLaser()
    {
        if (laserMesh == null)
            return;
        
        Destroy(laserMesh);
        Destroy(this);
    }
}

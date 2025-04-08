using System.Collections.Generic;
using UnityEngine;

public class CurvedRoadRight : BaseMakeMesh
{
    [SerializeField] private float radius = 20f;
    [SerializeField] private float roadWidth = 20f;
    [SerializeField] private float curveAngle = 90f;

    protected override void SetVertices()
    {
        float angleStep = curveAngle / dense;
        Vector3 pivot = new Vector3(-radius, 0, 0); // 오른쪽 회전 중심점

        for (int i = 0; i <= dense; i++)
        {
            float angle = angleStep * i;
            Quaternion rotation = Quaternion.Euler(0, -angle, 0);
            Vector3 center = rotation * (Vector3.right * radius) + pivot;

            Vector3 forward;
            if (i < dense)
            {
                float nextAngle = angleStep * (i + 1);
                Vector3 nextCenter = Quaternion.Euler(0, -nextAngle, 0) * (Vector3.right * radius) + pivot;
                forward = (nextCenter - center).normalized;
            }
            else
            {
                float prevAngle = angleStep * (i - 1);
                Vector3 prevCenter = Quaternion.Euler(0, -prevAngle, 0) * (Vector3.right * radius) + pivot;
                forward = (center - prevCenter).normalized;
            }

            Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;

            Vector3 leftPoint = center - right * (roadWidth * 0.5f);
            Vector3 rightPoint = center + right * (roadWidth * 0.5f);

            vertices.Add(leftPoint);
            vertices.Add(rightPoint);
        }
    }

    protected override void SetTriangles()
    {
        for (int i = 0; i < vertices.Count - 2; i += 2)
        {
            triangles.Add(i);
            triangles.Add(i + 2);
            triangles.Add(i + 1);

            triangles.Add(i + 1);
            triangles.Add(i + 2);
            triangles.Add(i + 3);
        }
    }
    protected override void SetUV()
    {
        uv.Clear();

        float angleRad = Mathf.Deg2Rad * curveAngle;
        float arcLength = radius * angleRad;

        for (int i = 0; i <= dense; i++)
        {
            float u = (arcLength / dense) * i / arcLength; // u 좌표는 호의 비율
            uv.Add(new Vector2(u, 0)); // 안쪽
            uv.Add(new Vector2(u, 1)); // 바깥쪽
        }
    }


    protected override void SetNormals()
    {
        for (int i = 0; i <= dense; i++)
        {
            normals.Add(Vector3.up);
            normals.Add(Vector3.up);
        }
    }
}

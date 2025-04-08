using System.Collections.Generic;
using UnityEngine;

public class WaypointGenerator : MonoBehaviour
{
    public enum RoadType { Straight, LeftCurve, RightCurve }

    [Header("General Settings")]
    [SerializeField] private RoadType roadType;
    [SerializeField] private Transform waypointPrefab;
    [SerializeField] private Transform leftLaneParent;
    [SerializeField] private Transform rightLaneParent;

    [Header("Common Values")]
    [SerializeField] private float laneOffset = 1.5f;
    [SerializeField] private int dense = 20;

    [Header("Curve Only")]
    [SerializeField] private float radius = 20f;
    [SerializeField] private float curveAngle = 90f;

    [Header("Straight Only")]
    [SerializeField] private float straightLength = 20f;

    private void Start()
    {
        switch (roadType)
        {
            case RoadType.Straight:
                GenerateStraightWaypoints();
                break;
            case RoadType.LeftCurve:
                GenerateCurveWaypoints(true);
                break;
            case RoadType.RightCurve:
                GenerateCurveWaypoints(false);
                break;
        }
    }

    private void GenerateStraightWaypoints()
    {
        Vector3 origin = transform.position;
        Vector3 forward = transform.forward;

        for (int i = 0; i < dense; i++)
        {
            float t = (float)i / (dense - 1);
            Vector3 basePos = origin + forward * straightLength * t;

            CreateWaypoint(basePos - transform.right * laneOffset, leftLaneParent);
            CreateWaypoint(basePos + transform.right * laneOffset, rightLaneParent);
        }
    }

    private void GenerateCurveWaypoints(bool isLeft)
    {
        float angleRad = Mathf.Deg2Rad * curveAngle;
        float stepAngle = angleRad / (dense - 1);
        float sign = isLeft ? 1f : -1f;

        Vector3 center = transform.position + sign * transform.right * radius;

        for (int i = 0; i < dense; i++)
        {
            float theta = stepAngle * i;
            float x = Mathf.Sin(theta) * radius;
            float z = Mathf.Cos(theta) * radius;

            Vector3 dir = new Vector3(x, 0f, radius - z);
            Vector3 basePos = center + Quaternion.Euler(0f, -transform.eulerAngles.y, 0f) * dir;

            Vector3 right = Vector3.Cross(Vector3.up, (basePos - center).normalized);
            CreateWaypoint(basePos - right * laneOffset, leftLaneParent);
            CreateWaypoint(basePos + right * laneOffset, rightLaneParent);
        }
    }

    private void CreateWaypoint(Vector3 position, Transform parent)
    {
        Transform wp = Instantiate(waypointPrefab, position, Quaternion.identity, parent);
        wp.name = "Waypoint";
    }
}

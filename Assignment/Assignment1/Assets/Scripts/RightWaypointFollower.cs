using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightWaypointFollower : MonoBehaviour
{
    public string laneName = "RightLane";
    public float speed = 5f;
    public float reachThreshold = 1.0f;

    private List<Transform> waypoints = new List<Transform>();
    private int currentIndex = 0;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        LoadClosestLaneAndWaypoints();
    }

    void Update()
    {
        if (waypoints.Count == 0 || currentIndex >= waypoints.Count) return;

        Transform target = waypoints[currentIndex];
        Vector3 direction = (target.position - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;
        transform.forward = Vector3.Lerp(transform.forward, direction, Time.deltaTime * 5f);

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < reachThreshold)
        {
            currentIndex++;

            if (currentIndex >= waypoints.Count)
            {
                // 현재 세트 끝, 다음 세트 탐색
                if (!LoadClosestLaneAndWaypoints())
                {
                    Debug.Log("더 이상 갈 LeftLane이 없습니다");
                    enabled = false;
                }
            }
        }
    }

    bool LoadClosestLaneAndWaypoints()
    {
        GameObject nextLane = FindNearestLane();
        if (nextLane == null)
        {
            Debug.LogWarning("새로운 LeftLane을 찾지 못함");
            return false;
        }

        waypoints.Clear();
        foreach (Transform child in nextLane.transform)
        {
            waypoints.Add(child);
        }

        if (waypoints.Count == 0)
        {
            Debug.LogWarning("새로운 LeftLane에 웨이포인트 없음");
            return false;
        }

        currentIndex = FindClosestWaypointIndex();
        return true;
    }

    GameObject FindNearestLane()
    {
        GameObject[] lanes = GameObject.FindGameObjectsWithTag(laneName);
        GameObject closest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject lane in lanes)
        {
            float dist = Vector3.Distance(transform.position, lane.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = lane;
            }
        }

        return closest;
    }

    int FindClosestWaypointIndex()
    {
        float minDist = Mathf.Infinity;
        int closest = 0;

        for (int i = 0; i < waypoints.Count; i++)
        {
            float dist = Vector3.Distance(transform.position, waypoints[i].position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = i;
            }
        }

        return closest;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [Header("Objects")]
    public GameObject ObstaclePrefab;

    [Header("Information")]
    public int ObstacleCount;
    public float MinRadius;
    public float MaxRadius;

    public int MinRotationSpeed;
    public int MaxRotationSpeed;

    private void Start()
    {
        CreateBaseObstacles(ObstacleCount);
    }

    private void CreateBaseObstacles(int _value)
    {
        Circle c = new Circle(0, 0, (MaxRadius + MinRadius) / 2);

        for (int i = 0; i < _value; i++)
        {
            c.ChangeRadius(Random.Range(MinRadius, MaxRadius));
            Vector2 pos = c.GetRandomPoint();
            GameObject o = Instantiate(ObstaclePrefab, pos, Quaternion.identity);
            o.GetComponent<Orbitable>().Initialize(Vector3.zero, Random.Range(10, 100));
        }
    }
}

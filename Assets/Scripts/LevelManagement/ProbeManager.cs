using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbeManager : MonoBehaviour
{
    private Circle mainCircle;
    private Circle endCircle;

    public GameObject ProbePrefab;
    public GameObject MainObject;

    public int maxProbeNumber;

    private void Start()
    {
        mainCircle = new Circle(0, 0, 2);
        endCircle = new Circle(0, 0, 7);
    }

    public GameObject CreateRocket()
    {
        Vector2 pos = mainCircle.GetRandomPoint();
        GameObject probe = Instantiate(ProbePrefab, pos, Quaternion.identity);
        probe.transform.rotation = QuaternionUtil.GetOppositeDirection(probe, MainObject);
        probe.transform.SetParent(MainObject.transform);
        probe.GetComponent<RocketBehaviour>().Init(endCircle);
        return probe;

    }
}

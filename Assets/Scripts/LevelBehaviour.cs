using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
    public GameObject mainObject;
    public GameObject Prefab;
    public GameObject ObstaclePrefab;
    public GameObject LinePrefab;

    private GameObject probe;
    private Circle mainCircle;
    private Circle endCircle;

    private bool probeInitialized = false;
    private List<float> endCircleAngles = new List<float>();
    private CircleDrawer drawer;
    
    void Start()
    {
        
        mainCircle = new Circle(mainObject.transform.position.x, mainObject.transform.position.y, 2);
        endCircle = new Circle(mainObject.transform.position.x, mainObject.transform.position.y, 7);
        drawer = new CircleDrawer(LinePrefab, endCircle, 90);
        CreateBaseObstacles(40);
        CreateRocket();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !probeInitialized)
        {
            probe.GetComponent<RocketBehaviour>().ChangeMovement(true);
            probeInitialized = true;
        }

        if (probeInitialized)
        {
            if(probe == null)
            {
                probeInitialized = false;
                CreateRocket();
                return;
            }
            bool rocketCheck = probe.GetComponent<RocketBehaviour>().IsMoving();
            if (!rocketCheck)
            {
                Vector2 cirPos = probe.GetComponent<RocketBehaviour>().circlePosition;
                endCircleAngles.Add(endCircle.Angle(cirPos.x, cirPos.y));
                drawer.Draw(endCircleAngles);
                probeInitialized = false;
                CreateRocket();
                return;
            }
        }
    }

    private void CreateRocket()
    {
        Vector2 pos = mainCircle.GetRandomPoint();
        probe = Instantiate(Prefab, pos, Quaternion.identity);
        probe.transform.rotation = QuaternionUtil.GetOppositeDirection(probe, mainObject);
        probe.transform.SetParent(mainObject.transform);
        probe.GetComponent<RocketBehaviour>().Init(endCircle);
    }

    private void CreateBaseObstacles(int _value)
    {
        Circle c = new Circle(mainObject.transform.position.x, mainObject.transform.position.y, 6);

        for(int i=0; i<_value; i++)
        {
            c.ChangeRadius(Random.Range(4f, 6.5f));
            Vector2 pos = c.GetRandomPoint();
            GameObject o = Instantiate(ObstaclePrefab, pos, Quaternion.identity);
            o.GetComponent<Orbitable>().OrbitObject = mainObject.transform;
            o.GetComponent<Orbitable>().rotationSpeed = Random.Range(10, 100);
        }
    }
}

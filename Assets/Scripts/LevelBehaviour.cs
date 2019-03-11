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
    private AngleCircleArray angleArray;
    public float angleDifference = 120;
    private float distanceDifference;

    private LevelDispay ld;
    private bool controlBlock = false;
    public int maxProbeNumber;

    private LevelDegrees levelScore;
    
    void Start()
    {
        ld = GetComponent<LevelDispay>();
        ld.MaxProbeTextChanged(maxProbeNumber.ToString());
        mainCircle = new Circle(mainObject.transform.position.x, mainObject.transform.position.y, 2);
        endCircle = new Circle(mainObject.transform.position.x, mainObject.transform.position.y, 7);
        distanceDifference = Vector2.Distance(endCircle.Vector2FromAngle(0), endCircle.Vector2FromAngle(angleDifference));
        angleArray = new AngleCircleArray(new CircleDrawer(LinePrefab, endCircle, angleDifference, 1f));

        levelScore = new LevelDegrees(4, 6, maxProbeNumber);

        //CreateBaseObstacles(40);
        CreateRocket();
    }

    private void Update()
    {
        if (controlBlock)
        {
            if(probe != null)
            {
                Destroy(probe);
                probe = null;
            }
            return;
        }
            

        if (Input.GetKeyDown(KeyCode.Space) && !probeInitialized && maxProbeNumber > 0)
        {
            levelScore.IncreaseCurrentScore();
            probe.GetComponent<RocketBehaviour>().ChangeMovement(true);
            probeInitialized = true;
            maxProbeNumber--;
            ld.MaxProbeTextChanged(maxProbeNumber.ToString());
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
                Probe p = probe.GetComponent<Probe>();
                p.Angle = endCircle.Angle(cirPos.x, cirPos.y);
                angleArray.Add(p);
                float x = angleArray.Calculate();
                if (x >= 360)
                {
                    GameOver();
                }
                probeInitialized = false;
                CreateRocket();
                return;
            }
        }
    }

    private void CreateRocket()
    {

        if (maxProbeNumber > 0)
        {
            Vector2 pos = mainCircle.GetRandomPoint();
            probe = Instantiate(Prefab, pos, Quaternion.identity);
            probe.transform.rotation = QuaternionUtil.GetOppositeDirection(probe, mainObject);
            probe.transform.SetParent(mainObject.transform);
            probe.GetComponent<RocketBehaviour>().Init(endCircle);
            probe.GetComponent<Probe>().Init(Time.time, LinePrefab, distanceDifference);
        }
        else
        {
            GameOver();
            probe = null;
        }
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

    private void GameOver()
    {
        controlBlock = true;
        ld.DisplayResult(levelScore);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
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
    private int levelNumber;

    private ProbeManager probeManagement;

    [Header("Materials")]
    public Material NormalMaterial;
    public Material LightGreenMaterial;


    void Start()
    {
        probeManagement = GetComponent<ProbeManager>();
        Load();
        HandleDisplay();

        mainCircle = new Circle(0, 0, 2);
        endCircle = new Circle(0, 0, 7);
        distanceDifference = Vector2.Distance(endCircle.Vector2FromAngle(0), endCircle.Vector2FromAngle(angleDifference));
        angleArray = new AngleCircleArray(new CircleDrawer(LinePrefab, endCircle, angleDifference, 1f, NormalMaterial));

        levelScore = new LevelDegrees(4, 6, maxProbeNumber);

        
        CreateRocket();
    }

    private void Load()
    {
        SaveData save = SaveUtil.LoadSave();
        if (save != null)
        {
            if (save.maxAngleDifference != 0)
            {
                angleDifference = save.maxAngleDifference;
            }
            levelNumber = save.levelNumber;
        }
    }

    private void HandleDisplay()
    {
        ld = GetComponent<LevelDispay>();
        ld.MaxProbeTextChanged(maxProbeNumber.ToString());
        ld.LevelNumberChanged(levelNumber);
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
            probe = probeManagement.CreateRocket();
            probe.GetComponent<RocketBehaviour>().Init(endCircle);
            probe.GetComponent<Probe>().Init(Time.time, LinePrefab, distanceDifference, LightGreenMaterial);
        }
        else
        {
            GameOver();
            probe = null;
        }
    }

    

    

    private void GameOver()
    {
        if (levelScore.GetLevelDegree() > 0)
        {
            BaseCircleDrawer d = new BaseCircleDrawer(LinePrefab, endCircle, 0.5f, LightGreenMaterial);
            StartCoroutine(d.Collapse());
        }
        controlBlock = true;
        ld.DisplayResult(levelScore);
    }
}

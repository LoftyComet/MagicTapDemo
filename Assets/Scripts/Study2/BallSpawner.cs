using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;  //需要添加这个名词空间，调用DataReceivedEventArg
using System;
using Random = UnityEngine.Random;
using UnityEngine.UIElements;
using UnityEngine.Events;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
using static UnityEngine.GraphicsBuffer;

public class BallSpawner : MonoBehaviour
{
    readonly string sArguments = @"SpawnBall.py";//这里是python的文件名
    public List<GameObject> TargetObjects = new List<GameObject> { };
    public List<GameObject> obstacleObjects = new List<GameObject> { };
    public List<GameObject> allBalls = new List<GameObject> { };
    public int TargetStart = 0, TargetEnd;
    public List<int> TargetTriggered, TargetList = new List<int>();
    public Material ordinaryBall;
    public Material redBall;
    public Material pinkBall;
    public Material greenBall;
    public UnityEvent RoundOverEvent, ExpStartEvent, ExpEndEvent, HandInBallEvent;
    public List<Vector3> tatgetPos = new List<Vector3>();
    public bool triggered = false;
    public enum Mode
    {
        Tap = 0,
        longPress = 1,
        magicTap = 2,
        pinch = 3
    }
    public enum ID_kind
    {
        High = 0,
        Mid = 1,
        Low = 2,
        Change = 3
    }
    public Mode modeSelected;
    [Header("start ball index")]
    public int start;
    [Header("ID 0 for high       1 for mid       2 for low")]
    public int expRound = 0;
    [Header("Direction")]
    public int rotationDirection;
    [Header("Prefabs")]
    public GameObject ObstaclePrefab;
    public GameObject TargetPrefab;

    [Header("Circle Attributes")]
    public float CircleRadius;
    public int NumberOfBalls;

    [Header("Ball Attributes")]
    public float BallSize = 0.06f;
    public int ObNum = 11;

    [Header("Parent Object")]
    public GameObject ParentObject;

    [Header("Exp Selected")]
    public int ExpSelected = 0;
    public int initialBall;
    
    bool expStarted = false;
    public int expTime = 0;
    //方向
    
    static List<float> coordinates = new List<float>();
    float startTime, oldTime;
    public float usedTime;
    public int errorTimes, errorInTimes;
    public List<float> errorX;
    public List<float> errorY;
    public List<float> errorZ;
    public Vector3 indexLoc, thumbLoc;
    public char triggerCondition;
    
    public GameObject Got, GoNext;
    public Bounds bounds;
    public int inBound = 0;
    public List<float> oriBallX;
    public List<float> oriBallY;
    public int seed = 0;
    public List<int> seeds;
    public AudioSource source;
    public AudioClip clip;
    public string minD;
    public Vector3 largeCirclePoint = Vector3.zero;
    public int area_type;
    public int index;
    public string triggeredBy = "index";
    public int caseTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("temp").GetComponent<Renderer>().enabled = false;
        GoNext = GameObject.Find("GoNext");
        Got = GameObject.Find("Got");
        Vector3 temp = GameObject.Find("ObstacletDesignPrototypeB4CM").transform.position;
        Vector3 inLeftDown = new Vector3(temp.x, temp.y, temp.z);
        inLeftDown.x -= 1 / 2;
        inLeftDown.y -= 1 / 2;
        inLeftDown.z -= 1 / 2 * 0.3F;
        print(inLeftDown);
        //clip = Resources.Load<AudioClip>("Audio/Assets_MRTK_StandardAssets_Audio_MRTK_Manipulation_Start");
        source = GetComponent<AudioSource>();
        source.clip = clip;
        for (int i = 0; i < 16; i++)
        {
            //print("pos");
            float angle1 = i * (360f / 16);
            oriBallX.Add(CircleRadius * Mathf.Cos(angle1 * Mathf.Deg2Rad) + CircleRadius);
            oriBallY.Add(CircleRadius * Mathf.Sin(angle1 * Mathf.Deg2Rad) + CircleRadius);
            //分别计算每个球的位置
        }
        //ResetExp();
        //for (int i = 0; i < 15; i++)
        //{
        //    getNewTarget();
        //}
        HandInBallEvent.AddListener(ChangeBallIndex);
    }
    public void ResetExp()
    {
        switch (modeSelected)
        {
            case Mode.Tap:
                if (expRound == 3)
                {
                    modeSelected = Mode.longPress;
                    expRound = 0;
                    break;
                }

                break;
            case Mode.longPress:
                if (expRound == 3)
                {
                    modeSelected = Mode.magicTap;
                    expRound = 0;
                    break;
                }
                break;
            case Mode.magicTap:
                if (expRound == 3)
                {
                    modeSelected = Mode.pinch;
                    expRound = 0;
                    break;
                }
                break;
            case Mode.pinch:
                if (expRound == 3)
                {
                    //modeSelected = Mode.pinch;
                    expRound = 0;
                    break;
                }
                break;

        }
        if (expRound == 2)
        {
            switch (modeSelected)
            {
                case Mode.Tap:

                    Got.GetComponentInChildren<TMPro.TMP_Text>().text = "In this task, selection technique is \"Long-Press\".\r\nPlease complete the task quickly and accurately.\r\n";
                    break;
                case Mode.longPress:
                    Got.GetComponentInChildren<TMPro.TMP_Text>().text = "In this task, selection technique is \"Magic-Tap\".\r\nPlease complete the task quickly and accurately.\r\n";
                    break;
                case Mode.magicTap:
                    Got.GetComponentInChildren<TMPro.TMP_Text>().text = "In this task, selection technique is \"Pinch-Tap\".\r\nPlease complete the task quickly and accurately.\r\n";
                    break;
                case Mode.pinch:
                    Got.GetComponentInChildren<TMPro.TMP_Text>().text = "In this task, selection technique is \"Pinch-Tap\".\r\nPlease complete the task quickly and accurately.\r\n";
                    break;
            }
        }
        switch (expRound)
        {
            case 0:
                
                seeds = new List<int> { 0, 4, 6, 8, 9, 20, 30, 32, 34, 4, 0, 42, 43, 45, 46, 50 };
               

                minD = "7";
                BallSize = 0.03F;
                CircleRadius = 0.35F;
                TargetPrefab.GetComponent<AcceStudy3>().triggerDis = 0.015f;
                ObstaclePrefab.GetComponent<AcceStudy3>().triggerDis = 0.015f;
                ObNum = 11;
                break;
            case 1:
                seeds = new List<int> { 1, 3, 5, 6, 7, 9, 11, 12, 15, 18, 24, 25, 28, 29, 33 };
                minD = "9";
                BallSize = 0.06F;
                CircleRadius = 0.3F;
                ObNum = 5;
                TargetPrefab.GetComponent<AcceStudy3>().triggerDis = 0.03f;
                ObstaclePrefab.GetComponent<AcceStudy3>().triggerDis = 0.03f;
                break;
            case 2:
                seeds = new List<int> { 1, 5, 11, 13, 14, 18, 21, 26, 28, 31, 32, 33, 36, 38, 40 };
                minD = "11";
                BallSize = 0.09F;
                CircleRadius = 0.25F;
                TargetPrefab.GetComponent<AcceStudy3>().triggerDis = 0.045f;
                ObstaclePrefab.GetComponent<AcceStudy3>().triggerDis = 0.045f;
                ObNum = 3;
                break;
        }

        //简单粗暴，干掉所有球，重新生成
        foreach (GameObject gameObject in TargetObjects)
        {
            gameObject.SetActive(false);
        }
        foreach (GameObject gameObject in obstacleObjects)
        {
            gameObject.SetActive(false);
        }
        foreach (GameObject gameObject in allBalls)
        {
            gameObject.SetActive(false);
        }
        TargetObjects.Clear();
        obstacleObjects.Clear();
        allBalls.Clear();
        coordinates.Clear();
        expStarted = false;
        startTime = Time.time;
        //RunPythonScript(sArguments, seeds[expTime].ToString());
        RunPythonScript(sArguments, "0" + " " + minD);
        CheckBallFor16();
        GetStart();
        //CheckBall();
        //while (!CheckBall())
        //{
        //    coordinates.Clear();
        //    seed++;
        //    RunPythonScript(sArguments, seed.ToString());
        //}
        tatgetPos.Clear();
        largeCirclePoint = transform.position;
        print(transform.position.z);
        SpawnOriBalls(CircleRadius, NumberOfBalls, transform.position.z);
        switch (ExpSelected)
        {
            case 0:
                SpawnRandomBallsInLine();
                break;
            case 1:
                SpawnRandomBallsInSector();

                break;
            case 2:
                SpawnRandomBallsInArea();
                break;
            case 3:
                //SpawnRandomBallsInPyramid();
                SpawnRandomBallsWithPy();
                break;
        }
        expStarted = true;
        expRound++;
    }
    void FixedUpdate()
    {

        Handedness handedness = Handedness.Any;
        HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, handedness, out MixedRealityPose indexPose);
        indexLoc = indexPose.Position;
        print("记录");
        print(indexPose.Position.x);


        Handedness handedness2 = Handedness.Any;
        HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, handedness2, out MixedRealityPose thumbPose);
        thumbLoc = thumbPose.Position;
        print("记录");
        print(thumbPose.Position.x);
        
        
        
    }
    void GetStart()
    {
        //随机生成起点
        int temp = Random.Range(0, 16);
        //start = temp;
        int temp2 = Random.Range(0, 2);
        //rotationDirection = temp2;
        TargetStart = start;

        if (rotationDirection == 0)
        {
            //+7相当于下一个球的位置
            start = (start + 7) % 16;
        }
        else
        {
            start = (start - 7 + 16) % 16;
        }
        TargetEnd = start;
        initialBall = TargetEnd;
        print(initialBall);
        print(rotationDirection);
    }
    void SpawnOriBalls(float Radius, int BallCount, float CenterDepthCoordinate)
    {
        ParentObject = gameObject;
        //球的大小
        Vector3 BallScale = new Vector3(BallSize, BallSize, BallSize);
        if (expStarted)
        {
            for (int i = 0; i < BallCount; i++)
            {
                //分别计算每个球的位置
                float angle = i * (360f / BallCount);
                float x = transform.position.x + Radius * Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = transform.position.y + Radius * Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector3 position = new Vector3(x, y, CenterDepthCoordinate);
                print("位置");
                print(position.x);
                print(position.y);
                print(position.z);
                tatgetPos.Add(position);
                if (TargetEnd == i)
                {
                    print(i);
                    print(TargetStart);
                    //目标球
                    GameObject Target = Instantiate(TargetPrefab, position, Quaternion.identity);
                    Target.transform.localScale = BallScale;
                    Target.transform.parent = ParentObject.transform;
                    Target.GetComponent<MeshRenderer>().material = redBall;
                    Target.GetComponent<S2AccMode>().ballIndex = i;
                    TargetObjects.Add(Target);
                    allBalls.Add(Target);
                }
                else if (TargetTriggered.Contains(i) && i != TargetStart)
                {
                    //障碍球
                    GameObject Obstacles = Instantiate(ObstaclePrefab, position, Quaternion.identity);
                    Obstacles.transform.localScale = BallScale;
                    Obstacles.transform.parent = ParentObject.transform;
                    Obstacles.GetComponent<S2Obstacles>().ballIndex = i;
                    allBalls.Add(Obstacles);
                    TargetObjects.Add(Obstacles);
                    
                }
                else
                {

                    //障碍球
                    GameObject Obstacles = Instantiate(ObstaclePrefab, position, Quaternion.identity);
                    Obstacles.transform.localScale = BallScale;
                    Obstacles.transform.parent = ParentObject.transform;
                    Obstacles.GetComponent<S2Obstacles>().ballIndex = i;
                    allBalls.Add(Obstacles);
                    TargetObjects.Add(Obstacles);
                    //由于上次点击手还留在原地，故先暂时禁用该球脚本
                    if (i == TargetStart)
                    {
                        //Obstacles.GetComponent<AcceStudy3>().enabled = false;
                        //Obstacles.GetComponent<S2Obstacles>().enabled = false;
                        //Obstacles.GetComponent<S2Player>().enabled = false;
                        Obstacles.GetComponent<S2Obstacles>().triggered = true;
                        Obstacles.GetComponent<AcceStudy3>().AfterOutEvent.RemoveAllListeners();
                        triggered = true;
                        //更改外形为绿球
                        MeshRenderer meshRenderer = Obstacles.GetComponent<MeshRenderer>();
                        meshRenderer.material = greenBall;
                        print("变为绿球");
                    }
                }


            }
        }
        else
        {
            for (int i = 0; i < BallCount; i++)
            {

                //分别计算每个球的位置
                float angle = i * (360f / BallCount);
                float x = transform.position.x + Radius * Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = transform.position.y + Radius * Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector3 position = new Vector3(x, y, CenterDepthCoordinate);
                tatgetPos.Add(position);
                if (i == initialBall)
                {
                    //目标球
                    GameObject Target = Instantiate(TargetPrefab, position, Quaternion.identity);
                    Target.transform.localScale = BallScale;
                    Target.transform.parent = ParentObject.transform;
                    Target.GetComponent<S2AccMode>().ballIndex = i;
                    TargetObjects.Add(Target);
                    allBalls.Add(Target);
                    MeshRenderer meshRenderer = Target.GetComponent<MeshRenderer>();
                    meshRenderer.material = redBall;
                    //print(i);
                }
                //else if (i == TargetEnd)
                //{
                //    //目标球
                //    GameObject Target = Instantiate(TargetPrefab, position, Quaternion.identity);
                //    Target.transform.localScale = BallScale;
                //    Target.transform.parent = ParentObject.transform;

                //    Target.GetComponentInChildren<AcceStudy3>().enabled = false;
                //    //更改外形为普通球
                //    MeshRenderer meshRenderer = Target.GetComponent<MeshRenderer>();
                //    meshRenderer.material = ordinaryBall;
                //    TargetObjects.Add(Target);
                //    allBalls.Add(Target);

                //}
                else
                {
                    //障碍球
                    GameObject Obstacles = Instantiate(ObstaclePrefab, position, Quaternion.identity);
                    Obstacles.transform.localScale = BallScale;
                    Obstacles.transform.parent = ParentObject.transform;
                    //实验未开始，先禁用障碍球
                    Obstacles.GetComponent<AcceStudy3>().enabled = false;
                    Obstacles.GetComponent<S2Obstacles>().enabled = false;
                    Obstacles.GetComponent<S2Player>().enabled = false;

                    Obstacles.GetComponent<S2Obstacles>().ballIndex = i;
                    allBalls.Add(Obstacles);

                    TargetObjects.Add(Obstacles);
                    //更改外形为绿球
                    //MeshRenderer meshRenderer = Obstacles.GetComponent<MeshRenderer>();
                    //meshRenderer.material = greenBall;
                }


            }

        }
    }
    void SpawnRandomBallsInLine()
    {
        ParentObject = gameObject;
        //球的大小
        Vector3 BallScale = new Vector3(BallSize, BallSize, BallSize);
        //暂时取前两个
        GameObject StartBall = TargetObjects[0];
        GameObject EndBall = TargetObjects[1];
        //计算障碍小球个数
        float distance = Vector3.Distance(StartBall.transform.position, EndBall.transform.position);
        int ObstacleNum = (int)(distance / BallSize) / 2 + 1;

        for (int i = 0; i < ObstacleNum; i++)
        {
            //分别计算每个球的位置(线性插值)
            float gap = (float)(i + 1) / (ObstacleNum + 1);
            //添加微小扰动
            float disturbance = UnityEngine.Random.Range(-0.02F, 0.02F);
            gap += disturbance;
            Vector3 position = Vector3.Lerp(StartBall.transform.position, EndBall.transform.position, gap);


            //生成障碍球
            GameObject Obstacles = Instantiate(ObstaclePrefab, position, Quaternion.identity);
            Obstacles.transform.localScale = BallScale;
            Obstacles.transform.parent = ParentObject.transform;
            Renderer BallRenderer = ObstaclePrefab.GetComponent<Renderer>();
        }
    }
    void SpawnRandomBallsWithPy()
    {



        //球的大小
        Vector3 BallScale = new Vector3(BallSize, BallSize, BallSize);
        //立方体大小
        float lenth = 1F;

        //向外左下角的点
        //Vector3 inLeftDown =  transform.localPosition;
        //Vector3 temp = GameObject.Find("Spawner").transform.position;
        Vector3 temp = transform.position;
        Vector3 inLeftDown = new Vector3(temp.x, temp.y, temp.z);
        inLeftDown.x -= lenth / 2;
        inLeftDown.y -= lenth / 2;
        inLeftDown.z -= lenth / 2 * 0.3F;
        print(inLeftDown);

        for (int i = 0; i < coordinates.Count; i += 2)
        {
            float randomZ = 0;
            int[] nums = { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1 };
            int randomIndex = Random.Range(0, nums.Length);
            //正面
            if (nums[randomIndex] == 0)
            {
                randomZ = Random.Range(0, 0.15F);
            }
            else
            {
                randomZ = Random.Range(0, 0.15F) + 0.15F;
            }
            //randomZ = Random.Range(-0.15F, 0.15F);
            //print(randomZ);
            //randomZ = 0;
            Vector3 position = new Vector3(inLeftDown.x + coordinates[i], inLeftDown.y + coordinates[i + 1], inLeftDown.z + randomZ);
            //print(position);
            if (collisionWithTarget(TargetObjects, position, BallSize))
            {
                continue;
            }

            //生成障碍球
            GameObject Obstacles = Instantiate(ObstaclePrefab, position, Quaternion.identity);
            Obstacles.transform.localScale = BallScale;
            Obstacles.transform.parent = ParentObject.transform;
            allBalls.Add(Obstacles);
            obstacleObjects.Add(Obstacles);
            if (!expStarted)
            {
                //实验未开始，先禁用障碍球
                Obstacles.GetComponent<AcceStudy3>().enabled = false;
                Obstacles.GetComponent<S2Obstacles>().enabled = false;
                Obstacles.GetComponent<S2Player>().enabled = false;

            }
        }
    }
    void SpawnRandomBallsInSector()
    {

        //换个思路，先提前生成1m^2面积内的球，若在扇形范围内就保留
        //球的大小
        Vector3 BallScale = new Vector3(BallSize, BallSize, BallSize);
        //暂时取前两个
        GameObject startBall = TargetObjects[0];
        GameObject endBall = TargetObjects[1];
        //确定生成球的基础范围
        //左下角的点
        Vector3 leftDown = transform.position;
        UnityEngine.Debug.Log(transform.position);
        UnityEngine.Debug.Log(transform.localPosition);
        leftDown.x -= 0.5F;
        leftDown.y -= 0.5F;
        //计算每列障碍小球个数
        int ObstacleNum = (int)(1F / BallSize) / 3 + 1;
        //记录目标球的个数
        int ballNum = 0;
        //范围内的障碍球总数
        int totalObstacle = 9;
        //划分为一块块小区域
        for (int i = 0; i < ObstacleNum; i++)
        {
            for (int j = 0; j < ObstacleNum; j++)
            {
                //分别计算每个球的位置
                float gapX = (float)(i + 1) / (ObstacleNum + 1);
                float gapY = (float)(j + 1) / (ObstacleNum + 1);
                //添加微小扰动
                float disturbance1 = Random.Range(-0.025F, 0.025F);
                float disturbance2 = Random.Range(-0.025F, 0.025F);
                gapX += disturbance1;
                gapY += disturbance2;
                Vector3 position = new Vector3(leftDown.x + gapX, leftDown.y + gapY, transform.position.z);
                if (!inSector(startBall.transform.position, endBall.transform.position, position) || !inLargeCircle(position))
                {
                    if (collisionWithTarget(allBalls, position, BallSize))
                    {
                        continue;
                    }
                    //生成障碍球
                    GameObject Obstacles = Instantiate(ObstaclePrefab, position, Quaternion.identity);
                    Obstacles.transform.localScale = BallScale;
                    Obstacles.transform.parent = ParentObject.transform;
                    if (!expStarted)
                    {
                        //实验未开始，先禁用障碍球
                        Obstacles.GetComponent<AcceStudy3>().enabled = false;
                        Obstacles.GetComponent<S2Obstacles>().enabled = false;
                        Obstacles.GetComponent<S2Player>().enabled = false;

                    }
                    allBalls.Add(Obstacles);
                    continue;
                }
                else
                {
                    if (collisionWithTarget(allBalls, position, BallSize))
                    {
                        continue;
                    }
                    //生成障碍球
                    GameObject Obstacles = Instantiate(ObstaclePrefab, position, Quaternion.identity);
                    Obstacles.transform.localScale = BallScale;
                    Obstacles.transform.parent = ParentObject.transform;
                    if (!expStarted)
                    {
                        //实验未开始，先禁用障碍球
                        Obstacles.GetComponent<AcceStudy3>().enabled = false;
                        Obstacles.GetComponent<S2Obstacles>().enabled = false;
                        Obstacles.GetComponent<S2Player>().enabled = false;
                    }
                    obstacleObjects.Add(Obstacles);
                    allBalls.Add(Obstacles);
                    ballNum++;
                    //球够了就随机剔除一个
                    if (ballNum > totalObstacle)
                    {
                        int removeIndex = Random.Range(0, obstacleObjects.Count - 1);
                        allBalls.Remove(obstacleObjects[removeIndex]);
                        obstacleObjects.RemoveAt(removeIndex);
                        ballNum--;
                    }
                }

            }

        }
        //球不够就补够
        if (ballNum < totalObstacle)
        {
            int toAddNum = totalObstacle - ballNum;
            //起点到终点连线选定没有碰撞的地方添加
            for (int i = 0; i < toAddNum; i++)
            {
                float gap = (float)(i + 1) / (totalObstacle);
                //添加微小扰动
                float disturbance1 = Random.Range(-0.025F, 0.025F);
                float disturbance2 = Random.Range(-0.025F, 0.025F);
                Vector3 position = Vector3.Lerp(startBall.transform.position, endBall.transform.position, gap);
                position.x += disturbance1;
                position.y += disturbance2;
                if (!collisionWithTarget(allBalls, position, BallSize))
                {

                    //生成障碍球
                    GameObject Obstacles = Instantiate(ObstaclePrefab, position, Quaternion.identity);
                    Obstacles.transform.localScale = BallScale;
                    Obstacles.transform.parent = ParentObject.transform;
                    if (!expStarted)
                    {
                        //实验未开始，先禁用障碍球
                        Obstacles.GetComponent<AcceStudy3>().enabled = false;
                        Obstacles.GetComponent<S2Obstacles>().enabled = false;
                        Obstacles.GetComponent<S2Player>().enabled = false;
                    }
                    obstacleObjects.Add(Obstacles);
                    allBalls.Add(Obstacles);
                }
                else
                {
                    toAddNum++;
                }
            }

        }

    }
    bool inSector(Vector3 startBall, Vector3 endBall, Vector3 toTest)
    {
        float distance = Vector3.Distance(startBall, endBall);
        // 距离超出
        if (distance < Vector3.Distance(startBall, toTest))
        {
            return false;
        }
        //角度超出
        if (Vector3.Angle(endBall - startBall, toTest - startBall) > 10)
        {
            return false;
        }
        return true;
    }
    void SpawnRandomBallsInArea()
    {
        //球的大小
        Vector3 BallScale = new Vector3(BallSize, BallSize, BallSize);
        //确定生成球的基础范围
        //左下角的点
        Vector3 leftDown = transform.position;
        leftDown.x -= 0.5F;
        leftDown.y -= 0.5F;
        //左上角的点
        Vector3 leftUp = transform.position;
        leftUp.x -= 0.5F;
        leftUp.y += 0.5F;
        //右下角的点
        Vector3 rightDown = transform.position;
        leftUp.x += 0.5F;
        leftUp.y -= 0.5F;
        //计算每列障碍小球个数
        int ObstacleNum = (int)(1F / BallSize) / 2 + 3;
        //划分为一块块小区域
        for (int i = 0; i < ObstacleNum; i++)
        {
            for (int j = 0; j < ObstacleNum; j++)
            {
                //分别计算每个球的位置
                float gapX = (float)(i + 1) / (ObstacleNum + 1);
                float gapY = (float)(j + 1) / (ObstacleNum + 1);
                //添加微小扰动
                float disturbance1 = Random.Range(-0.015F, 0.015F);
                float disturbance2 = Random.Range(-0.015F, 0.015F);
                gapX += disturbance1;
                gapY += disturbance2;
                Vector3 position = new Vector3(leftDown.x + gapX, leftDown.y + gapY, transform.position.z);
                if (collisionWithTarget(allBalls, position, BallSize))
                {
                    continue;
                }
                //生成障碍球
                GameObject Obstacles = Instantiate(ObstaclePrefab, position, Quaternion.identity);
                Obstacles.transform.localScale = BallScale;
                Obstacles.transform.parent = ParentObject.transform;
                Renderer BallRenderer = ObstaclePrefab.GetComponent<Renderer>();
            }

        }
    }
    void SpawnRandomBallsInPyramid()
    {
        //实现思路同面内，先生成立方体的球 2 * distance * (2 * distance * sin10°) * distance 体积内的球，再保留四棱锥范围内的球
        //球的大小
        Vector3 BallScale = new Vector3(BallSize, BallSize, BallSize);
        //立方体大小
        float lenth = 1.1F;
        //暂时取前两个
        GameObject startBall, endBall;
        //TargetList[0]的数字为起点球编号
        if (TargetList[1] > TargetList[0])
        {
            startBall = TargetObjects[0];
            endBall = TargetObjects[1];
        }
        else
        {
            startBall = TargetObjects[1];
            endBall = TargetObjects[0];
        }
        if (!expStarted)
        {
            startBall = TargetObjects[0];
            endBall = TargetObjects[1];
        }
        //startBall = TargetObjects[0];
        //endBall = TargetObjects[1];
        //记录目标球的个数
        int ballNum = 0;
        //范围内的障碍球总数
        int totalObstacle = 9;
        //tan10
        float tan10 = Mathf.Tan(10 * Mathf.Deg2Rad);
        //向外左下角的点
        //Vector3 inLeftDown =  transform.localPosition;
        Vector3 temp = GameObject.Find("ObstacletDesignPrototypeB4CM").transform.position;
        Vector3 inLeftDown = new Vector3(temp.x, temp.y, temp.z);
        inLeftDown.x -= lenth / 2;
        inLeftDown.y -= lenth / 2;
        inLeftDown.z -= lenth / 2 * tan10;
        //计算每列障碍小球个数
        int ObstacleNum = (int)(lenth / BallSize) / 4 - 1;
        bool toBreak = false;
        for (int i = 0; i < ObstacleNum; i++)
        {
            for (int j = 0; j < ObstacleNum; j++)
            {
                for (int k = 0; k < ObstacleNum; k++)
                {
                    //分别计算每个球的位置
                    float gapX = (float)(i + 1) / (ObstacleNum + 1);
                    float gapY = (float)(j + 1) / (ObstacleNum + 1);
                    float gapZ = (float)(k + 1) / (ObstacleNum + 1) * tan10;
                    //添加微小扰动
                    float disturbance1 = Random.Range(-0.05F, 0.05F);
                    float disturbance2 = Random.Range(-0.05F, 0.05F);
                    float disturbance3 = Random.Range(-0.05F, 0.05F);
                    gapX += disturbance1;
                    gapY += disturbance2;
                    gapZ += disturbance3;
                    Vector3 position = new Vector3(inLeftDown.x + gapX, inLeftDown.y + gapY, inLeftDown.z + gapZ);
                    if (collisionWithTarget(allBalls, position, BallSize))
                    {
                        continue;
                    }
                    if (!inPyramid(startBall.transform.position, endBall.transform.position, position))
                    {
                        //生成障碍球
                        GameObject Obstacles = Instantiate(ObstaclePrefab, position, Quaternion.identity);
                        Obstacles.transform.localScale = BallScale;
                        Obstacles.transform.parent = ParentObject.transform;
                        allBalls.Add(Obstacles);
                        obstacleObjects.Add(Obstacles);
                        if (!expStarted)
                        {
                            //实验未开始，先禁用障碍球
                            Obstacles.GetComponent<AcceStudy3>().enabled = false;
                            Obstacles.GetComponent<S2Obstacles>().enabled = false;
                            Obstacles.GetComponent<S2Player>().enabled = false;

                        }
                        //continue;
                    }
                    else
                    {
                        //生成障碍球
                        GameObject Obstacles = Instantiate(ObstaclePrefab, position, Quaternion.identity);
                        Obstacles.transform.localScale = BallScale;
                        Obstacles.transform.parent = ParentObject.transform;
                        allBalls.Add(Obstacles);
                        obstacleObjects.Add(Obstacles);
                        if (!expStarted)
                        {
                            //实验未开始，先禁用障碍球
                            Obstacles.GetComponent<AcceStudy3>().enabled = false;
                            Obstacles.GetComponent<S2Obstacles>().enabled = false;
                            Obstacles.GetComponent<S2Player>().enabled = false;

                        }
                        ballNum++;
                        //球够了就随机剔除
                        //if (ballNum > totalObstacle)
                        //{
                        //    //for (int q = 0; q < 5; q++)
                        //    //{
                        //    //    int removeIndex = Random.Range(0, obstacleObjects.Count - 1);
                        //    //    obstacleObjects[removeIndex].SetActive(false);
                        //    //    allBalls.Remove(obstacleObjects[removeIndex]);
                        //    //    obstacleObjects.RemoveAt(removeIndex);
                        //    //    ballNum--;
                        //    //}
                        //    int removeIndex = Random.Range(0, obstacleObjects.Count - 1);
                        //    obstacleObjects[removeIndex].SetActive(false);
                        //    allBalls.Remove(obstacleObjects[removeIndex]);
                        //    obstacleObjects.RemoveAt(removeIndex);
                        //    ballNum--;
                        //    toBreak = true;
                        //    break;
                        //}
                    }



                }
                if (toBreak)
                {
                    break;
                }
            }
            if (toBreak)
            {
                break;
            }
        }
        //球不够就补够
        if (ballNum < totalObstacle)
        {
            int toAddNum = totalObstacle - ballNum;
            //起点到终点连线选定没有碰撞的地方添加
            for (int i = 0; i < toAddNum; i++)
            {
                float gap = (float)(i + 1) / (totalObstacle);
                //添加微小扰动
                float disturbance1 = Random.Range(-0.025F, 0.025F);
                float disturbance2 = Random.Range(-0.025F, 0.025F);
                float disturbance3 = Random.Range(-0.025F, 0.025F);
                Vector3 position = Vector3.Lerp(startBall.transform.position, endBall.transform.position, gap);
                position.x += disturbance1;
                position.y += disturbance2;
                position.z += disturbance3;
                if (!collisionWithTarget(allBalls, position, BallSize))
                {

                    //生成障碍球
                    GameObject Obstacles = Instantiate(ObstaclePrefab, position, Quaternion.identity);
                    Obstacles.transform.localScale = BallScale;
                    Obstacles.transform.parent = ParentObject.transform;
                    if (!expStarted)
                    {
                        //实验未开始，先禁用障碍球
                        Obstacles.GetComponent<AcceStudy3>().enabled = false;
                        Obstacles.GetComponent<S2Obstacles>().enabled = false;
                        Obstacles.GetComponent<S2Player>().enabled = false;
                    }
                    obstacleObjects.Add(Obstacles);
                    allBalls.Add(Obstacles);
                }
                else
                {
                    toAddNum++;
                }
            }

        }
    }
    bool inPyramid(Vector3 startBall, Vector3 endBall, Vector3 toTest)
    {
        float distance = Vector3.Distance(startBall, endBall);
        // 距离超出
        if (distance < Vector3.Distance(startBall, toTest))
        {
            return false;
        }
        //角度超出
        //因为是类似四棱锥的玩意，投影到xoy面与endBall - startBall轴角度小于10，投影到zox面与endBall - startBall轴角度小于10
        //Vector3 mainAxleTemp1 = endBall - startBall;
        //mainAxleTemp1.z = 0;
        //Vector3 xoy = toTest - startBall;
        //xoy.z = 0;
        //Vector3 mainAxleTemp2 = endBall - startBall;
        //mainAxleTemp2.y = 0;
        //Vector3 zox = toTest - startBall;
        //zox.y = 0;
        //if (Vector3.Angle(mainAxleTemp1, xoy) > 10 || Vector3.Angle(mainAxleTemp2, zox) > 10)
        //{
        //    return false;
        //}
        if (Vector3.Angle(endBall - startBall, toTest - startBall) > 10)
        {
            return false;
        }
        return true;
    }
    bool collisionWithTarget(List<GameObject> TargetList, Vector3 toTest, float radius)
    {
        for (int i = 0; i < TargetList.Count; i++)
        {
            Vector3 temp1 = TargetList[i].transform.position;
            temp1.z = 0;
            Vector3 temp2 = toTest;
            temp2.z = 0;
            if (Vector3.Distance(temp1, temp2) < radius)
            {
                return true;
            }
        }
        return false;
    }
    bool inLargeCircle(Vector3 toTest)
    {
        float distance = Vector3.Distance(transform.position, toTest);
        // 距离超出
        if (distance > CircleRadius)
        {
            return false;
        }
        return true;
    }
    //每次点击完后调用，生成新的随机球
    public void getNewTarget()
    {
        //简单粗暴，干掉所有球，重新生成
        foreach (GameObject gameObject in TargetObjects)
        {
            gameObject.SetActive(false);
        }
        foreach (GameObject gameObject in obstacleObjects)
        {
            gameObject.SetActive(false);
        }
        foreach (GameObject gameObject in allBalls)
        {
            gameObject.SetActive(false);
        }
        

        usedTime = Time.time - startTime;
        RoundOverEvent.Invoke();
        TargetObjects.Clear();
        obstacleObjects.Clear();
        allBalls.Clear();
        //while (!CheckBall())
        //{
        //    coordinates.Clear();
        //    seed++;
        //    RunPythonScript(sArguments, seed.ToString());
        //}

        if (expTime < 15)
        {
            if (rotationDirection == 0)
            {
                //+7相当于下一个球的位置
                start = (start + 7) % 16;
            }
            else
            {
                start = (start - 7 + 16) % 16;
            }
            print(TargetStart);
            TargetTriggered.Add(TargetEnd);
            TargetStart = TargetEnd;
            TargetEnd = start;
            print(TargetStart);
            print(TargetEnd);
            tatgetPos.Clear();
            largeCirclePoint = transform.position;
            SpawnOriBalls(CircleRadius, NumberOfBalls, transform.position.z);
            coordinates.Clear();
            RunPythonScript(sArguments, seeds[expTime].ToString() + " " + minD);
            //CheckBallFor16();
            //CheckBall();
            switch (ExpSelected)
            {
                case 0:
                    //SpawnRandomBallsInLine();
                    break;
                case 1:
                    SpawnRandomBallsInSector();
                    break;
                case 2:
                    //SpawnRandomBallsInArea();
                    break;
                case 3:
                    //SpawnRandomBallsInPyramid();
                    SpawnRandomBallsWithPy();
                    break;
            }
            
            expTime++;
        }
        else if (expTime == 15)
        {
            
            ExpEndEvent.Invoke();
            expTime = 0;
            Got.SetActive(true);
            GoNext.SetActive(true);
        }



        startTime = Time.time;
        errorTimes = 0;
        errorInTimes = 0;
        errorX.Clear();
        errorY.Clear();
        errorZ.Clear();
        triggerCondition = ' ';
        if (expTime == 1)
        {
            ExpStartEvent.Invoke();
        }

    }
    public static void RunPythonScript(string sArgName, string args = "")
    {
        Process p = new Process();
        //python脚本的路径
        string path = @"C:\Users\Liu_Yixuan\Downloads\MagicTapandKeyboard\Assets\Py\" + sArgName;
        path = path + " " + args;
        string sArguments = path;


        //(注意：用的话需要换成自己的)没有配环境变量的话，可以像我这样写python.exe的绝对路径
        //(用的话需要换成自己的)。如果配了，直接写"python.exe"即可
        p.StartInfo.FileName = @"D:\Anaconda\python.exe";
        //p.StartInfo.FileName = @"C:\Program Files\Python35\python.exe";

        // sArguments为python脚本的路径   python值的传递路线strArr[]->teps->sigstr->sArguments 
        //在python中用sys.argv[ ]使用该参数
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.Arguments = sArguments;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardInput = true;
        p.StartInfo.RedirectStandardError = true;
        p.StartInfo.CreateNoWindow = true;
        p.Start();
        p.BeginOutputReadLine();
        p.OutputDataReceived += new DataReceivedEventHandler(Out_RecvData);
        Console.ReadLine();
        p.WaitForExit();
    }

    static void Out_RecvData(object sender, DataReceivedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Data))
        {
            //UnityEngine.Debug.Log(e.Data);
            coordinates.Add(float.Parse(e.Data));
        }
    }

    public void CheckBallFor16()
    {
        int tempStart = initialBall;
        int tempEnd;
        for (int j = 0; j < 15; j++)
        {
            inBound = 0;
            if (rotationDirection == 0)
            {
                //+7相当于下一个球的位置
                tempEnd = (tempStart + 7) % 16;
            }
            else
            {
                tempEnd = (tempStart - 7 + 16) % 16;
            }
            //此处检验

            //分别计算开始球和结束的位置
            float angle = tempStart * (360f / 16);
            float x = transform.position.x + CircleRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = transform.position.y + CircleRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector3 startPosition = new Vector3(x, y, transform.position.z);
            angle = tempEnd * (360f / 16);
            x = transform.position.x + CircleRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
            y = transform.position.y + CircleRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector3 endPosition = new Vector3(x, y, transform.position.z);
            angle = (tempEnd + 1) % 16 * (360f / 16);
            x = transform.position.x + CircleRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
            y = transform.position.y + CircleRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
            float lenth = 1F;

            //向外左下角的点
            //Vector3 inLeftDown =  transform.localPosition;
            Vector3 temp = GameObject.Find("ObstacletDesignPrototypeB4CM").transform.position;
            Vector3 inLeftDown = new Vector3(temp.x, temp.y, temp.z);
            inLeftDown.x -= lenth / 2;
            inLeftDown.y -= lenth / 2;
            //inLeftDown.z -= lenth / 2 * 0.3F;


            for (int i = 0; i < coordinates.Count; i += 2)
            {
                Vector3 position = new Vector3(inLeftDown.x + coordinates[i], inLeftDown.y + coordinates[i + 1], inLeftDown.z);
                //print(position);
                if (inSector(startPosition, endPosition, position))
                {
                    //print(position);
                    //print(endPosition);
                    inBound++;
                    //print(333);
                }

            }


            tempStart = tempEnd;
        }
    }
    public bool CheckBall()
    {

        inBound = 0;
        int tempStart = TargetStart;
        int tempEnd = TargetEnd;
        //if (rotationDirection == 0)
        //{
        //    //+7相当于下一个球的位置
        //    tempEnd = (tempStart + 7) % 16;
        //}
        //else
        //{
        //    tempEnd = (tempStart - 7 + 16) % 16;
        //}
        //此处检验

        //分别计算开始球和结束的位置
        float angle = tempStart * (360f / 16);
        float x = transform.position.x + CircleRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = transform.position.y + CircleRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
        Vector3 startPosition = new Vector3(x, y, transform.position.z);
        angle = tempEnd * (360f / 16);
        x = transform.position.x + CircleRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
        y = transform.position.y + CircleRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
        Vector3 endPosition = new Vector3(x, y, transform.position.z);

        float lenth = 1F;

        //向外左下角的点
        //Vector3 inLeftDown =  transform.localPosition;
        Vector3 temp = GameObject.Find("ObstacletDesignPrototypeB4CM").transform.position;
        Vector3 inLeftDown = new Vector3(temp.x, temp.y, temp.z);
        inLeftDown.x -= lenth / 2;
        inLeftDown.y -= lenth / 2;
        //inLeftDown.z -= lenth / 2 * 0.3F;
        print(inLeftDown);


        for (int i = 0; i < coordinates.Count; i += 2)
        {
            Vector3 position = new Vector3(inLeftDown.x + coordinates[i], inLeftDown.y + coordinates[i + 1], inLeftDown.z);
            //print(position);
            if (inSector(startPosition, endPosition, position))
            {
                inBound++;
                //print(333);
            }

        }
        if (inBound != ObNum)
        {


            //print(seeds[expTime]);
            print("error");
            //RunPythonScript(sArguments, seed.ToString());
            //CheckBall();
            return false;
        }
        else
        {
            print(seed);
            seeds.Add(seed);
            print(inBound);
            print(222);
            return true;
        }


    }
    public void PlaySound2()
    {
        source.Play();
    }
    public void ChangeBallIndex()
    {

    }
}

// 7.参数值1 GenericPropertyJSON:{"name":"seeds","type":-1,"arraySize":15,"arrayType":"int","children":[{"name":"Array","type":-1,"arraySize":15,"arrayType":"int","children":[{"name":"size","type":12,"val":15},{"name":"data","type":0,"val":0},{"name":"data","type":0,"val":4},{"name":"data","type":0,"val":6},{"name":"data","type":0,"val":8},{"name":"data","type":0,"val":9},{"name":"data","type":0,"val":20},{"name":"data","type":0,"val":30},{"name":"data","type":0,"val":32},{"name":"data","type":0,"val":34},{"name":"data","type":0,"val":40},{"name":"data","type":0,"val":42},{"name":"data","type":0,"val":43},{"name":"data","type":0,"val":45},{"name":"data","type":0,"val":46},{"name":"data","type":0,"val":50}]}]}
// 9.参数组2 GenericPropertyJSON:{"name":"seeds","type":-1,"arraySize":15,"arrayType":"int","children":[{"name":"Array","type":-1,"arraySize":15,"arrayType":"int","children":[{"name":"size","type":12,"val":15},{"name":"data","type":0,"val":1},{"name":"data","type":0,"val":3},{"name":"data","type":0,"val":5},{"name":"data","type":0,"val":6},{"name":"data","type":0,"val":7},{"name":"data","type":0,"val":9},{"name":"data","type":0,"val":11},{"name":"data","type":0,"val":12},{"name":"data","type":0,"val":15},{"name":"data","type":0,"val":18},{"name":"data","type":0,"val":24},{"name":"data","type":0,"val":25},{"name":"data","type":0,"val":28},{"name":"data","type":0,"val":29},{"name":"data","type":0,"val":33}]}]}
// 11.参数组3 GenericPropertyJSON: { "name":"seeds","type":-1,"arraySize":15,"arrayType":"int","children":[{ "name":"Array","type":-1,"arraySize":15,"arrayType":"int","children":[{ "name":"size","type":12,"val":15},{ "name":"data","type":0,"val":1},{ "name":"data","type":0,"val":5},{ "name":"data","type":0,"val":11},{ "name":"data","type":0,"val":13},{ "name":"data","type":0,"val":14},{ "name":"data","type":0,"val":18},{ "name":"data","type":0,"val":21},{ "name":"data","type":0,"val":26},{ "name":"data","type":0,"val":28},{ "name":"data","type":0,"val":31},{ "name":"data","type":0,"val":32},{ "name":"data","type":0,"val":33},{ "name":"data","type":0,"val":36},{ "name":"data","type":0,"val":38},{ "name":"data","type":0,"val":40}]}]}
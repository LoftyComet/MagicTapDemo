using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class S1ButtonBehavior2 : MonoBehaviour
{
    public AcceStimulate acce;
    public S1Clock s1clock;
    public float TestTime = 5f;
    public float coolDown = 5f;
    LogWriter logWriter;
    StartBallBehavior startBallBehavior;
    public bool canTrigger = true;
    public bool EnteredTheArea = false;
    public bool Completed = false;
    public Material shallowBall;
    public Material oriBall;
    int expTimes = 0;
    // Start is called before the first frame update
    void Start()
    {
        s1clock = GetComponentInChildren<S1Clock>();
        acce = GetComponent<AcceStimulate>();
        logWriter = GetComponent<LogWriter>();
        startBallBehavior = GetComponentInChildren<StartBallBehavior>();
        // Use Lambda fucntion
        acce.InEvent.AddListener(() =>
        {
            if (startBallBehavior.GetState() && Completed == false)
            {
                s1clock.TapBlueBall(expTimes);
                s1clock.StartCoroutine("CountdownCoroutine");
            }
            else if (s1clock.CountdownTime < 0.005)
            {
                //Completed = true;
                s1clock.StopCoroutine("CountdownCoroutine");
                //s1clock.OnSucceed();
            }
        });
        acce.InEvent.AddListener(TurnShallow);
        s1clock.CountCompleteEvent.AddListener(() =>
        {
            Completed = true;
            startBallBehavior.TureUnstarted();
            //StartCoroutine("StartCoolDown");
        });

        acce.OutEvent.AddListener(() =>
        {
            //Completed = false;
            if (s1clock.CountdownTime > 0.01 && Completed == false)
            {
                s1clock.StopCountDown();
                startBallBehavior.TureUnstarted();
                //StartCoroutine("StartCoolDown");
                s1clock.Warning();
            }
            else
            {
                print("complete");
                startBallBehavior.TureUnstarted();
                expTimes++;

                s1clock.CountUpdate();
                s1clock.ResetStates();
                Completed = false;
                if (expTimes == 10)
                {
                    acce.enabled = false;
                    //完成五次实验，开始数据分析
                    DataAnalyze();
                }
                // s1clock.OnSucceed();
            }
        });
        acce.OutEvent.AddListener(TurnShallowBack);
    }

    // Update is called once per frame
    void Update()
    {
        //print(s1clock.CountdownTime);
        //print(s1clock.PrematureExit);
    }

    void TurnShallow()
    {
        gameObject.GetComponent<MeshRenderer>().material = shallowBall;
    }
    void TurnShallowBack()
    {
        gameObject.GetComponent<MeshRenderer>().material = oriBall;
    }
    void DataAnalyze()
    {

    }
}

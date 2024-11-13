using Oculus.Platform.Samples.VrHoops;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2Obstacles : MonoBehaviour
{
    public AcceStudy3 Acce;
    public AcceStudy3_2 Acce2;
    public S2Player Player;
    public BallSpawner ballSpawner;
    int modeSelected;
    float oldTime;
    public AudioSource source;
    public AudioClip clip;
    public bool inBall = false, inBall2 = false;
    public int ballIndex = 0;
    public int ballKind = 2;
    public bool triggered = false;
    public Pinch pinchob;
    public bool useIndex = false;
    public void Start()
    {
        Acce = GetComponent<AcceStudy3>();
        Acce2 = GetComponent<AcceStudy3_2>();
        Player = GetComponentInChildren<S2Player>();
        ballSpawner = GameObject.Find("Spawner").GetComponent<BallSpawner>();
        modeSelected = (int)ballSpawner.modeSelected;
        pinchob = GetComponent<Pinch>();
        switch (modeSelected)
        {
            case 0:
                SetFastMode();
                break;
            case 1:
                Acce.Delay = true;
                Acce.WaitTime = 0.5F;
                SetHesMode();
                break;
            case 2:
                Acce.Acce = true;
                SetHesMode();
                //print("Hes");
                break;
            case 3:
                SetPinchMode();
                break;


        }
        source = GetComponent<AudioSource>();
        source.clip = clip;
        ballIndex = ballSpawner.obstacleObjects.IndexOf(gameObject);
        //SetFastMode();
        //SetHesMode();
    }
    public void SetPinchMode()
    {
        if (ballSpawner != null)
        {
            

            //Acce.InEvent.AddListener();
            Acce.InEvent.AddListener(() => {

                if (triggered)
                {
                    print("触发目标球后还没离开");
                    ballSpawner.area_type = 4;
                    ballSpawner.index = -1;
                }
                if (inBall == false && !triggered && !pinchob.isPinch)
                {
                    Player.TurnShallow();
                    inBall = true;
                    print("在障碍球中");
                    ballSpawner.area_type = 2;
                    ballSpawner.index = ballIndex;
                }

                else if (inBall && !triggered && !pinchob.isPinch)
                {
                    ballSpawner.area_type = 2;
                    ballSpawner.index = ballIndex;
                }


            });
            //Acce.HesEvent.AddListener(Player.);
            //Acce.AfterOutEvent.AddListener(Player.TurnShallowBack);
            //Acce.InEvent.AddListener(ballSpawner.changeOpacity);
            //Acce.OutEvent.AddListener(ballSpawner.getNewTarget);
            Acce.AfterOutEvent.AddListener(() =>
            {
                print("离开障碍球");
                ballSpawner.area_type = 3;
                ballSpawner.index = -1;

                //triggered = false;
                oldTime = Time.time;
                inBall = false;
                if (!triggered)
                {
                    Player.TurnYellowBack();
                }
                useIndex = false;
                ballSpawner.triggered = false;
            });

            pinchob.PinchStart.AddListener(() =>
            {
                if (inBall) //手在球里
                {
                    if (!triggered)
                    {
                        useIndex = true;
                        Player.TurnYellow();
                        source.Play();
                        print("在障碍球触发");
                        ballSpawner.area_type = 5;
                        ballSpawner.index = ballIndex;
                        ballSpawner.errorTimes++;
                        ballSpawner.errorInTimes++;
                        ballSpawner.errorX.Add(gameObject.transform.position.x);
                        ballSpawner.errorY.Add(gameObject.transform.position.y);
                        ballSpawner.errorZ.Add(gameObject.transform.position.z);
                    }
                }

            });
        }

        if (ballSpawner != null)
        {


            //Acce.InEvent.AddListener();
            Acce2.InEvent.AddListener(() => {

                if (triggered)
                {
                    print("触发目标球后还没离开");
                    ballSpawner.area_type = 4;
                    ballSpawner.index = -1;
                }
                if (inBall2 == false && !triggered && !pinchob.isPinch)
                {
                    Player.TurnShallow();
                    inBall2 = true;
                    print("在障碍球中");
                    ballSpawner.area_type = 2;
                    ballSpawner.index = ballIndex;
                }

                else if (inBall2 && !triggered && !pinchob.isPinch)
                {
                    ballSpawner.area_type = 2;
                    ballSpawner.index = ballIndex;
                }


            });
            //Acce.HesEvent.AddListener(Player.);
            //Acce.AfterOutEvent.AddListener(Player.TurnShallowBack);
            //Acce.InEvent.AddListener(ballSpawner.changeOpacity);
            //Acce.OutEvent.AddListener(ballSpawner.getNewTarget);
            Acce2.AfterOutEvent.AddListener(() =>
            {
                print("离开障碍球");
                ballSpawner.area_type = 3;
                ballSpawner.index = -1;

                //triggered = false;
                oldTime = Time.time;
                inBall2 = false;
                if (!triggered)
                {
                    Player.TurnYellowBack();
                }
                ballSpawner.triggeredBy = "index";
                ballSpawner.triggered = false;
            });

            pinchob.PinchStart.AddListener(() =>
            {
                if (inBall2) //手在球里
                {
                    if (!triggered && !useIndex)
                    {
                        ballSpawner.triggeredBy = "thumb";
                        Player.TurnYellow();
                        source.Play();
                        print("在障碍球触发");
                        ballSpawner.area_type = 5;
                        ballSpawner.index = ballIndex;
                        ballSpawner.errorTimes++;
                        ballSpawner.errorInTimes++;
                        ballSpawner.errorX.Add(gameObject.transform.position.x);
                        ballSpawner.errorY.Add(gameObject.transform.position.y);
                        ballSpawner.errorZ.Add(gameObject.transform.position.z);
                    }
                }

            });
        }
    }
    public void SetHesMode()
    {
        Acce.BeforeHesEvent.AddListener(Acce.SetInvoked);
        //Acce.HesEvent.AddListener(Acce.OutMaterial);
        Acce.HesEvent.AddListener(Acce.OpenCloseSti); // CloseSti set to be false after out
        Acce.HesEvent.AddListener(Acce.showDebugger);
        Acce.AfterOutEvent.AddListener(Acce.UnInvoked);

        if (ballSpawner != null)
        {
            //Acce.InEvent.AddListener(Player.TurnShallow);
            Acce.InEvent.AddListener(() => {
                if (!triggered)
                {
                    Player.TurnShallow();
                    ballSpawner.area_type = 2;
                    ballSpawner.index = ballIndex;
                    print("在障碍球中");
                }
                else if (triggered)
                {
                    print("触发目标球后还没离开");
                    ballSpawner.area_type = 4;
                    ballSpawner.index = ballIndex;
                }
                if (inBall == false && !triggered)
                {
                    inBall = true;

                }
                else if (inBall && !triggered)
                {

                }


            });
            //Acce.HesEvent.AddListener();
            //Acce.AfterOutEvent.AddListener(Player.TurnShallowBack);
            //Acce.InEvent.AddListener(ballSpawner.changeOpacity);
            //Acce.OutEvent.AddListener(ballSpawner.getNewTarget);
            Acce.HesEvent.AddListener(() =>
            {
                if (!triggered)
                {
                    Player.TurnYellow();
                    ballSpawner.errorTimes++;
                    ballSpawner.errorX.Add(gameObject.transform.position.x);
                    ballSpawner.errorY.Add(gameObject.transform.position.y);
                    ballSpawner.errorZ.Add(gameObject.transform.position.z);
                    source.Play();
                    //！！！是否应该放在hesevent，障碍球触发后应该变
                    print("触发障碍球后还没离开");
                    ballSpawner.area_type = 5;
                    ballSpawner.index = ballIndex;
                }

            });
            Acce.AfterOutEvent.AddListener(() => {
                if (!triggered)
                {
                    ballSpawner.errorInTimes++;
                    oldTime = Time.time;
                    Player.TurnShallowBack();

                }
                //if (!ballSpawner.triggered)
                //{
                print("离开障碍球");
                ballSpawner.area_type = 3;
                ballSpawner.index = -1;
                //triggered = false;
                //}


                ballSpawner.triggered = false;
            });
        }
    }
    public void SetFastMode()
    {
        Acce.HesEvent.AddListener(Acce.OutMaterial);
        Acce.HesEvent.AddListener(Acce.OpenCloseSti); // CloseSti set to be false after out
        if (Player != null)
        {
            //Acce.HesEvent.AddListener(ballSpawner.turnYellow);
            //Acce.InEvent.AddListener(ballSpawner.turnYellow);
            //Acce.InEvent.AddListener(Player.TurnYellow);
            //Acce.OutEvent.AddListener(Player.turnYellowBack);
        }
        if (ballSpawner != null)
        {
            //Acce.InEvent.AddListener();
            Acce.InEvent.AddListener(() => {
                if (!triggered)
                {

                    Player.TurnYellow();

                }
                else if (triggered)
                {
                    print("触发目标球后还没离开");
                    ballSpawner.area_type = 4;
                    ballSpawner.index = -1;
                }

                if (inBall == false && !triggered)
                {
                    source.Play();
                    inBall = true;
                    print("在障碍球中");
                    ballSpawner.area_type = 2;
                    ballSpawner.index = ballIndex;
                }
                else if (inBall && !triggered)
                {
                    ballSpawner.area_type = 5;
                    ballSpawner.index = ballIndex;
                }


            });
            //Acce.HesEvent.AddListener(Player.);
            //Acce.AfterOutEvent.AddListener(Player.TurnShallowBack);
            //Acce.InEvent.AddListener(ballSpawner.changeOpacity);
            //Acce.OutEvent.AddListener(ballSpawner.getNewTarget);
            Acce.AfterOutEvent.AddListener(() =>
            {
                print("离开障碍球");
                ballSpawner.area_type = 3;
                ballSpawner.index = -1;
                ballSpawner.errorTimes++;
                ballSpawner.errorInTimes++;
                ballSpawner.errorX.Add(gameObject.transform.position.x);
                ballSpawner.errorY.Add(gameObject.transform.position.y);
                ballSpawner.errorZ.Add(gameObject.transform.position.z);
                //triggered = false;
                oldTime = Time.time;
                inBall = false;

                ballSpawner.triggered = false;
            });
        }
    }
    private void Update()
    {
        if (Time.time - oldTime > 1 && !triggered && !inBall && !inBall2)
        {
            oldTime = Time.time;
            Player.TurnYellowBack();
        }
    }
    public void Set01Mode()
    {
        Acce.InEvent.AddListener(Acce.SetInvoked);
        Acce.InEvent.AddListener(Acce.OutMaterial);
        Acce.AfterOutEvent.AddListener(Acce.UnInvoked);
        print(666);
        if (Player != null)
        {
            //Acce.InEvent.AddListener(Player.ChangeColor);
        }
    }
    public void Set010Mode()
    {
        print(777);
        if (Player != null)
        {
            //Acce.OutEvent.AddListener(Player.ChangeColor);
        }
    }
}

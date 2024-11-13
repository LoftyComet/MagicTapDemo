using Oculus.Platform.Samples.VrHoops;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
// For all, Out-Enter-Out will have material change be like Out-in-out coded in AcceStudy3.cs
public class S2AccMode : MonoBehaviour
{
    public AcceStudy3 Acce;
    public AcceStudy3_2 Acce2;
    public S2Player Player;
    public BallSpawner ballSpawner;
    public GameObject TargetPrefab;
    int modeSelected;
    bool inBall = false, inBall2 = false;
    public int ballIndex = 0;
    public int ballKind = 1;
    public Pinch pinchob;
    public bool useIndex = false; 
    //public AudioSource source;
    //public AudioClip clip;
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
                break;
            case 3:
                SetPinchMode();
                break;
        }
        //clip = Resources.Load<AudioClip>("Audio/Assets_MRTK_StandardAssets_Audio_MRTK_Manipulation_Start");
        //source = GetComponent<AudioSource>();
        //source.clip = clip;
    }
    public void SetPinchMode()
    {
        if (Player != null)
        {
            Acce.InEvent.AddListener(() =>
            {
                ballSpawner.area_type = 1;
                ballSpawner.index = ballIndex;
                print("在目标球中");
                if (inBall == false)
                {
                    inBall = true;
                    Player.ChangeOpacity();
                    //print(111);
                }

            });
            //Acce.OutEvent.AddListener(ballSpawner.getNewTarget);
        }
        pinchob.PinchStart.AddListener(() =>
        {
            if (inBall)
            {
                useIndex = true;
                ballSpawner.caseTime++;
                print("触发111");
                print(Acce.IndexTipPose2.Position.x);
                ballSpawner.PlaySound2();
                ballSpawner.getNewTarget();
                //ballSpawner.triggeredBy = "index";
                
            }
        });
        Acce.AfterOutEvent.AddListener(() =>
        {
            if (!ballSpawner.triggered)
            {
                Player.ChangeOpacityBack();
                print("触发球失败");
                ballSpawner.area_type = 3;
                ballSpawner.index = -1;
                print(555);
                inBall = false;
            }
        });

        if (Player != null)
        {
            Acce2.InEvent.AddListener(() =>
            {
                ballSpawner.area_type = 1;
                ballSpawner.index = ballIndex;
                print("thumb在目标球中");
                ballSpawner.triggeredBy = "thumb";
                if (inBall2 == false)
                {
                    inBall2 = true;
                    Player.ChangeOpacity();
                    //print(111);
                }

            });
            //Acce.OutEvent.AddListener(ballSpawner.getNewTarget);
        }
        pinchob.PinchStart.AddListener(() =>
        {
            if (inBall2 && !useIndex)
            {
                print("触发111by thumb");
                print(Acce2.ThumbTipPose2.Position.x);
                ballSpawner.caseTime++;
                ballSpawner.PlaySound2();
                ballSpawner.getNewTarget();

            }
        });
        Acce2.AfterOutEvent.AddListener(() =>
        {
            if (!ballSpawner.triggered)
            {
                Player.ChangeOpacityBack();
                print("触发球失败");
                ballSpawner.area_type = 3;
                ballSpawner.index = -1;
                print(555);
                inBall2 = false;
                ballSpawner.triggeredBy = "index";
            }

        });
        
        
    }
    public void SetHesMode()
    {
        Acce.BeforeHesEvent.AddListener(Acce.SetInvoked);
        Acce.HesEvent.AddListener(Acce.OutMaterial);
        Acce.HesEvent.AddListener(Acce.OpenCloseSti); // CloseSti set to be false after out

        Acce.HesEvent.AddListener(Acce.showDebugger);

        Acce.AfterOutEvent.AddListener(Acce.UnInvoked);

        if (ballSpawner != null)
        {
            Acce.InEvent.AddListener(Player.ChangeOpacity);
            Acce.InEvent.AddListener(() =>
            {
                ballSpawner.area_type = 1;
                ballSpawner.index = ballIndex;
                print("在目标球中");
                if (inBall == false)
                {

                    inBall = true;
                }

            });
            Acce.HesEvent.AddListener(() =>
            {
                ballSpawner.caseTime++;
                ballSpawner.PlaySound2();
                ballSpawner.getNewTarget();
                print("触发111");
                print(Acce.IndexTipPose2.Position.x);
            });

            Acce.HesEvent.AddListener(() => {
                print(Acce.condition);
                ballSpawner.triggerCondition = Acce.condition;
            });
            //Acce.AfterOutEvent.AddListener();
        }
        Acce.AfterOutEvent.AddListener(() =>
        {
            if (!ballSpawner.triggered)
            {
                Player.ChangeOpacityBack();
                print("触发球失败");
                ballSpawner.area_type = 3;
                ballSpawner.index = -1;
                print(555);
            }

        });
    }
    public void SetFastMode()
    {
        Acce.HesEvent.AddListener(Acce.OutMaterial);
        Acce.HesEvent.AddListener(Acce.OpenCloseSti); // CloseSti set to be false after out
        if (Player != null)
        {
            //Acce.InEvent.AddListener(Player.ChangeOpacity);
            //Acce.HesEvent.AddListener(ballSpawner.changeOpacity);
            Acce.InEvent.AddListener(() =>
            {
                ballSpawner.area_type = ballKind;
                ballSpawner.index = ballIndex;
                print("在目标球中");
                if (inBall == false)
                {
                    ballSpawner.caseTime++;
                    print("触发111");
                    print(Acce.IndexTipPose2.Position.x);
                    ballSpawner.PlaySound2();
                    ballSpawner.getNewTarget();

                    inBall = true;
                    //print(111);
                }

            });
            //Acce.OutEvent.AddListener(ballSpawner.getNewTarget);
        }
        Acce.AfterOutEvent.AddListener(() =>
        {
            if (!ballSpawner.triggered)
            {
                print("触发球失败");
                ballSpawner.area_type = 3;
                ballSpawner.index = -1;
                print(555);
            }
        });
    }
    public void Set01Mode()
    {
        Acce.InEvent.AddListener(Acce.SetInvoked);
        Acce.InEvent.AddListener(Acce.OutMaterial);
        Acce.AfterOutEvent.AddListener(Acce.UnInvoked);
        if (Player != null)
        {
            //Acce.InEvent.AddListener(Player.ChangeColor);
            Acce.HesEvent.AddListener(Player.ChangeOpacity);
            Acce.InEvent.AddListener(Player.ChangeOpacity);
            Acce.OutEvent.AddListener(() =>
            {
                //source.Play();
                ballSpawner.caseTime++;
                ballSpawner.getNewTarget();
            });

        }
    }
    public void Set010Mode()
    {
        if (Player != null)
        {
            //Acce.OutEvent.AddListener(Player.ChangeColor);
        }
    }

}

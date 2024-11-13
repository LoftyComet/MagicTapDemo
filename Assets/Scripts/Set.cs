using Oculus.Platform.Samples.VrHoops;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set : MonoBehaviour
{
    public AcceStimulate Acce;
    public GameObject ball,ball2;
    public S2Player Player, Player2;
    public BallSpawner ballSpawner;
    public GameObject cancel;
    public int modeSelected;
    public AudioSource source;
    public AudioClip clip;
    public List<GameObject> balls;
    public bool inBall = false;
    public bool canceled = false;
    private bool handInBall = false;
    private bool inCancel = false;
    public void Start()
    {
        Acce = GetComponent<AcceStimulate>();
        Player = ball.GetComponent<S2Player>();
        Player2 = ball2.GetComponent<S2Player>();
        cancel = GameObject.Find("cancel");
        source = gameObject.GetComponent<AudioSource>();
        switch (modeSelected)
        {
            case 0:
                SetFastMode();
                break;
            case 1:
                Acce.Delay = true;
                Acce.WaitTime = 0.25F;
                SetHesMode();
                break;
            case 2:
                Acce.Acce = true;
                SetHesMode();
                //print("Hes");
                break;
            case 3:
                //¿ì»÷
                //SetFastMode();
                Acce.Fast = true;
                Set010();
                break;
            case 5:
                //¿ì»÷plus
                Acce.Fast = true;
                Set010Plus();
                if (cancel != null)
                {
                    cancel.SetActive(false);
                }
                
                break;
        }
        Acce.InEvent.AddListener(() =>
        {
            if (!handInBall)
            {
                Player2.TurnShallow();
                handInBall = true;
            }
            
        });
        Acce.AfterOutEvent.AddListener(() =>
        {
            Player2.TurnShallowBack();
            handInBall = false;
        });
    }
    public void SetHesMode()
    {
        Acce.BeforeHesEvent.AddListener(Acce.SetInvoked);
        //Acce.HesEvent.AddListener(Acce.OutMaterial);
        Acce.AfterOutEvent.AddListener(Acce.UnInvoked);


        //Acce.InEvent.AddListener(Player.TurnShallow);
        Acce.HesEvent.AddListener(Player.TurnYellow);
        Acce.HesEvent.AddListener(source.Play);

        //Acce.AfterOutEvent.AddListener(() => {
        //    Player.TurnShallowBack();
        //});

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
        
        Acce.InEvent.AddListener(() =>
        {
            
            if (!inBall)
            {
                Player.TurnYellow();
            }
            inBall = true;
        });
        
        Acce.AfterOutEvent.AddListener(() =>
        {
            inBall = false;
        });
    }
    public void Set010()
    {
        Acce.HesEvent.AddListener(() =>
        {
            canceled = true;
            Player2.TurnShallowBack();
        });
        Acce.AfterOutEvent.AddListener(() =>
        {
            if (!canceled)
            {
                Player.TurnYellow();
            }
            canceled = false;
        });
    }
    public void Set010Plus()
    {
        if (cancel != null)
        {
            cancel.GetComponent<AcceStimulate>().InEvent.AddListener(() =>
            {
                inCancel = true;

            });
            cancel.GetComponent<AcceStimulate>().AfterOutEvent.AddListener(() =>
            {
                canceled = true;
                inCancel = false;
                cancel.SetActive(false);
                canceled = false;
            });
        }
        Acce.HesEvent.AddListener(() =>
        {
            cancel.SetActive(true);                                                   
        });
        Acce.AfterOutEvent.AddListener(() =>
        {
            if (!canceled && !inCancel)
            {
                Player.TurnYellow();
                cancel.SetActive(false);
            }
            canceled = false;
        });
    }
    private void Update()
    {
        //if (Time.time - oldTime > 1)
        //{
        //    oldTime = Time.time;
        //    Player.TurnYellowBack();
        //}
    }
    public void Set01Mode()
    {        
        Acce.AfterOutEvent.AddListener(Reset);
        
    }
    public void Reset()
    {
        //foreach (var ball in balls) {
        //    ball.GetComponent<MeshRenderer>().material = 
        //}
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

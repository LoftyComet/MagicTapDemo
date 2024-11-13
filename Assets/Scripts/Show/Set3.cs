using Oculus.Platform.Samples.VrHoops;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set3 : MonoBehaviour
{
    public NewAcce2 Acce;
    public GameObject ball, ball2;
    public S2Player Player, Player2;
    public BallSpawner ballSpawner;
    public int modeSelected;
    public AudioSource source;
    public AudioClip clip;
    public List<GameObject> balls;
    public bool inBall = false;
    public bool canceled = false;
    private bool handInBall = false;
    public void Start()
    {
        Acce = GetComponent<NewAcce2>();
        Player = ball.GetComponent<S2Player>();
        Player2 = ball2.GetComponent<S2Player>();
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
                Acce.AccePlus = true;
                SetHesMode();
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
        Acce.HesEvent.AddListener(Player.TurnYellow);
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

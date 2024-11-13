using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
// For all, Out-Enter-Out will have material change be like Out-in-out coded in AcceStimulate.cs
public class S1AccMode : MonoBehaviour
{
    public AcceStimulate Acce;
    public S1Player Player;
    public BallSpawner BallSpawner;
    public bool isHinted = false;
    public float oldTime;
    public GameObject Got;
    //public Material HesMaterial;
    //public CancelRainbow CancelR;
    public void Start()
    {
        Acce = GetComponent<AcceStimulate>();
        Player = GetComponentInChildren<S1Player>();
        Acce.AfterOutEvent.AddListener(SetMode);
        Got = GameObject.Find("Got");
        GameObject Spawner = GameObject.Find("Spawner");
        if (Spawner != null)
        {
            BallSpawner = Spawner.GetComponent<BallSpawner>();
        }
        
    }
    void SetMode()
    {
        //if (BallSpawner != null)
        //{
        //    if (isHinted)
        //    {
        //        BallSpawner.ResetExp();
        //        gameObject.GetComponentInChildren<TMPro.TMP_Text>().text = "";
        //        gameObject.GetComponent<Renderer>().enabled = false;
        //        Got.SetActive(false);
        //        oldTime = Time.time;
        //        isHinted = false;
        //    }
        //    else
        //    {
        //        gameObject.GetComponentInChildren<TMPro.TMP_Text>().text = "Yes, I understand it.";
        //        isHinted = true;
        //    }
        //}
        if (BallSpawner != null)
        {
            BallSpawner.ResetExp();
            Got.SetActive(false);
            gameObject.SetActive(false);
        }
        
    }
    private void Update()
    {
        //if (Time.time - oldTime > 10)
        //{
        //    oldTime = Time.time;
        //    gameObject.GetComponent<Renderer>().enabled = true;
        //    if (isHinted)
        //    {
        //        gameObject.GetComponentInChildren<TMPro.TMP_Text>().text = "Yes, I understand it.";
        //    }
        //    else
        //    {
        //        gameObject.GetComponentInChildren<TMPro.TMP_Text>().text = "Next";
        //    }
        //    Got.SetActive(true);
        //}
    }
    public void SetHesMode()
    {
        Acce.BeforeHesEvent.AddListener(Acce.SetInvoked);

        Acce.HesEvent.AddListener(Acce.OutMaterial);
        Acce.HesEvent.AddListener(Acce.OpenCloseSti); // CloseSti set to be false after out

        Acce.HesEvent.AddListener(Acce.showDebugger);

        Acce.AfterOutEvent.AddListener(Acce.UnInvoked);

        if (Player != null)
        {
            print(1);   
            Acce.HesEvent.AddListener(Player.ChangeColor);
            Acce.InEvent.AddListener(Player.ChangeOpacity);
            Acce.OutEvent.AddListener(Player.SetOpacityBack);
        }
    }
    public void SetFastMode()
    {
        Acce.HesEvent.AddListener(Acce.OutMaterial);
        Acce.HesEvent.AddListener(Acce.OpenCloseSti); // CloseSti set to be false after out

        if (Player != null)
        {
            Acce.InEvent.AddListener(Player.ChangeOpacity);
        }
    }
    public void Set01Mode()
    {
        Acce.InEvent.AddListener(Acce.SetInvoked);
        Acce.InEvent.AddListener(Acce.OutMaterial);
        Acce.AfterOutEvent.AddListener(Acce.UnInvoked);

        if (Player != null)
        {
            Acce.InEvent.AddListener(Player.ChangeColor);
        }
    }
    public void Set010Mode()
    {
        if (Player != null)
        {
            Acce.OutEvent.AddListener(Player.ChangeColor);
        }
    }
    //public void set010cancelmode()
    //{
    //    acce.inevent.addlistener(cancelr.cancel);
    //    acce.outevent.addlistener(player.changecolor);
    //}

}

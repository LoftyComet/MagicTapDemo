using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
// For all, Out-Enter-Out will have material change be like Out-in-out coded in AcceStimulate.cs
public class S1AccModePrototypeFB : MonoBehaviour
{
    public AcceStimulate Acce;
    public S1Player Player;
    //public Material HesMaterial;
    //public CancelRainbow CancelR;
    public void Start()
    {
        Acce = GetComponent<AcceStimulate>();
        Player = GetComponentInChildren<S1Player>();
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
            Acce.HesEvent.AddListener(Player.ChangeColor);
        }
    }
    public void SetFastMode()
    {
        Acce.HesEvent.AddListener(Acce.OutMaterial);
        Acce.HesEvent.AddListener(Acce.OpenCloseSti); // CloseSti set to be false after out

        if (Player != null)
        {
            Acce.OutEvent.AddListener(Player.ChangeColor);
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

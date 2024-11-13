using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRKeys;

public class VRKeySetAcceMode : MonoBehaviour
{
    public bool Mode01, Mode010, ModeFastTap, ModeLongTap, ModeVelo, ModeAcce;
    private bool Delay, Fast, Acce, Velo;
    private float DelayTime;
    private Keyboard keyboard;
    void Start()
    {
        keyboard = GetComponent<Keyboard>();
    }

    public void SetMode()
    {
        if (Mode01)
        {
            SetAcceInHandler(gameObject);
        }
        else if (Mode010)
        {
            SetAcceOutHandler(gameObject);
        }
        else if (ModeFastTap)
        {
            Fast = true;
            DelayTime = 0.1f;
            SetAcceHesHandler(gameObject);
        }
        else if (ModeLongTap)
        {
            Delay = true;
            DelayTime = 0.1f;
            SetAcceHesHandler(gameObject);
        }
        else if (ModeVelo)
        {
            Velo = true;
            SetAcceHesHandler(gameObject);
        }
        else if (ModeAcce)
        {
            Acce = true;
            SetAcceHesHandler(gameObject);
        }
        else
        {
            Debug.LogError("Please select a button mode!");
        }
    }
    public void SetAcceInHandler(GameObject Root)
    {
        if (Root.transform.childCount == 0) { return; }
        // Access all the instances under the Root object
        foreach (Transform t in Root.transform)
        {
            SetAcceInHandler(t.gameObject);
            AcceStimulate tas = t.gameObject.GetComponent<AcceStimulate>();
            Key key = t.gameObject.GetComponent<Key>();
            if (tas && key)
            {
                Debug.LogFormat("Add handler {0}", t.gameObject.name);
                SetAcce(tas);
                tas.InEvent.AddListener(tas.SetInvoked);
                tas.InEvent.AddListener(key.HandleTouchEnter);
                tas.AfterOutEvent.AddListener(tas.UnInvoked);
                print(key);
            }
        }
    }

    public void SetAcceOutHandler(GameObject Root)
    {
        if (Root.transform.childCount == 0) { return; }
        foreach (Transform t in Root.transform)
        {
            SetAcceOutHandler(t.gameObject);
            AcceStimulate tas = t.gameObject.GetComponent<AcceStimulate>();
            Key key = t.gameObject.GetComponent<Key>();
            if (tas && key)
            {
                SetAcce(tas);
                tas.OutEvent.AddListener(tas.SetInvoked);
                tas.OutEvent.AddListener(key.HandleTouchEnter);

                tas.AfterOutEvent.AddListener(tas.UnInvoked);
            }
        }
    }
    public void SetAcceHesHandler(GameObject Root)
    {
        if (Root.transform.childCount == 0) { return; }
        foreach (Transform t in Root.transform)
        {
            SetAcceHesHandler(t.gameObject);
            AcceStimulate tas = t.gameObject.GetComponent<AcceStimulate>();
            Key key = t.gameObject.GetComponent<Key>();
            if (tas && key)
            {
                SetAcce(tas);

                tas.BeforeHesEvent.AddListener(tas.SetInvoked);
                tas.HesEvent.AddListener(key.HandleTouchEnter);

                tas.AfterOutEvent.AddListener(tas.UnInvoked);
            }
        }
    }

    public void SetAcce(AcceStimulate tas)
    {
        tas.Delay = Delay;
        tas.Fast = Fast;
        tas.Acce = Acce;
        tas.Velo = Velo;
        tas.WaitTime = DelayTime;
    }
}

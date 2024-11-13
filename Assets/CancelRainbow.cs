using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelRainbow : MonoBehaviour
{
    public AcceStimulate Acce;
    public AcceStimulate RainbowCancel;
    public S1Player Player;
    public GameObject Cancelrainbow;
    public void Cancel()
    {
        Cancelrainbow = transform.Find(name + "-Ca").gameObject;
        RainbowCancel = Cancelrainbow.GetComponent<AcceStimulate>();
        Cancelrainbow.SetActive(false);
        Acce.HesEvent.AddListener(OpenCancel);

        RainbowCancel.InEvent.AddListener(Acce.OpenCloseSti);
        RainbowCancel.InEvent.AddListener(CloseRainbow);

    }
    public void OpenCancel()
    {
        Cancelrainbow.SetActive(true);
    }
    public void CloseRainbow()
    {
        Cancelrainbow.SetActive(false);
        RainbowCancel.UnInvoked();
    }
}

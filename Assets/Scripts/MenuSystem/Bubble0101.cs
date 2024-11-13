using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble0101 : MonoBehaviour
{
    public AcceStimulate Acce;
    public BubbleOption BubOpt;
    void Start()
    {
        Acce = gameObject.GetComponent<AcceStimulate>();
        BubOpt = gameObject.GetComponent<BubbleOption>();

        Acce.OutEvent.AddListener(BubOpt.MoveSelfBack);
        Acce.OutEvent.AddListener(BubOpt.OpenSubs);
        Acce.OutEvent.AddListener(Acce.SetInvoked);
    }
    public void UnInvokedTB(HandTrackingInputEventData eventData)
    {
        Acce.UnInvoked();
    }
}

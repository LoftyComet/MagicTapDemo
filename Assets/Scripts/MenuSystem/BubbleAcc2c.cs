using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAcc2c : MonoBehaviour
{
    public BubbleOption ParentBubOpt;
    public AcceStimulate Acce;
    void Start()
    {
        ParentBubOpt = transform.parent.GetComponent<BubbleOption>();
        Acce = gameObject.GetComponent<AcceStimulate>();

        Acce.BeforeHesEvent.AddListener(Acce.SetInvoked);

        Acce.HesEvent.AddListener(ParentBubOpt.MoveSelfFront);
        Acce.HesEvent.AddListener(ParentBubOpt.MoveSubsBack);
        Acce.HesEvent.AddListener(ParentBubOpt.Close010Subs);
    }
}

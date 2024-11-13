using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleFast2c : MonoBehaviour
{
    public BubbleOption ParentBubOpt;
    public AcceStimulate Acce;
    void Start()
    {
        ParentBubOpt = transform.parent.GetComponent<BubbleOption>();
        Acce = gameObject.GetComponent<AcceStimulate>();

        Acce.HesEvent.AddListener(Acce.OpenCloseSti);
        Acce.HesEvent.AddListener(Acce.OutMaterial);

        Acce.OutEvent.AddListener(Acce.SetInvoked);

        Acce.OutEvent.AddListener(ParentBubOpt.MoveSelfFront);
        Acce.OutEvent.AddListener(ParentBubOpt.MoveSubsBack);
        Acce.OutEvent.AddListener(ParentBubOpt.Close010Subs);
    }
}

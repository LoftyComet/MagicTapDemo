using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAcc3c : MonoBehaviour
{
    public BubbleOption GrandParentBubOpt;
    public BubbleOption ParentBubOpt;
    public BubbleOption BubOpt;
    public AcceStimulate Acce;
    void Start()
    {
        GrandParentBubOpt = transform.parent.transform.parent.GetComponent<BubbleOption>();
        ParentBubOpt = transform.parent.GetComponent<BubbleOption>();
        BubOpt = gameObject.GetComponent<BubbleOption>();
        Acce = gameObject.GetComponent<AcceStimulate>();

        Acce.BeforeHesEvent.AddListener(Acce.SetInvoked);

        Acce.HesEvent.AddListener(ParentBubOpt.Close010Subs);
        Acce.HesEvent.AddListener(GrandParentBubOpt.MoveSubsFront);
        //Acce.HesEvent.AddListener(GrandParentBubOpt.MoveSelfFront);
    }
}

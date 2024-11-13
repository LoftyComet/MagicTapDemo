using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble1013c : MonoBehaviour
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

        Acce.OutEvent.AddListener(Acce.SetInvoked);

        Acce.OutEvent.AddListener(ParentBubOpt.Close010Subs);
        Acce.OutEvent.AddListener(GrandParentBubOpt.MoveSubsFront);
        //Acce.OutEvent.AddListener(GrandParentBubOpt.MoveSelfFront);
    }
}

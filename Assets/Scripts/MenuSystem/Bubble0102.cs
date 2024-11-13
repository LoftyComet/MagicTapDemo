using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble0102 : MonoBehaviour
{
    public BubbleOption ParentBubOpt;
    public BubbleOption BubOpt;
    // Set Manually
    public BubbleOption[] SibBubOpt;
    public AcceStimulate Acce;
    void Start()
    {
        ParentBubOpt = transform.parent.GetComponent<BubbleOption>();
        BubOpt = gameObject.GetComponent<BubbleOption>();
        Acce = gameObject.GetComponent<AcceStimulate>();

        foreach (var b in SibBubOpt)
        {
            Acce.OutEvent.AddListener(b.Close010Subs);
        }
        Acce.OutEvent.AddListener(Acce.SetInvoked);
        Acce.OutEvent.AddListener(ParentBubOpt.MoveSubsBack);
        Acce.OutEvent.AddListener(BubOpt.OpenSubs);
    }
}

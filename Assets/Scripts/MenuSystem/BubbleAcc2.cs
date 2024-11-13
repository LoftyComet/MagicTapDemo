using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAcc2 : MonoBehaviour
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

        foreach (var item in SibBubOpt)
        {
            Acce.BeforeHesEvent.AddListener(item.Close010Subs);
        }
        Acce.BeforeHesEvent.AddListener(Acce.SetInvoked);

        Acce.HesEvent.AddListener(ParentBubOpt.MoveSubsBack);
        Acce.HesEvent.AddListener(BubOpt.OpenSubs);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Microsoft.MixedReality.Toolkit.Experimental.UI.KeyboardKeyFunc;

public class BubbleAcc3 : MonoBehaviour
{
    public BubbleOption GrandParentBubOpt;
    public BubbleOption BubOpt;
    public AcceStimulate Acce;
    public int Function;
    void Start()
    {
        GrandParentBubOpt = transform.parent.transform.parent.GetComponent<BubbleOption>();
        BubOpt = gameObject.GetComponent<BubbleOption>();
        Acce = gameObject.GetComponent<AcceStimulate>();

        if (Function == 0)
            Acce.BeforeHesEvent.AddListener(BubOpt.ChangeColor);
        else if (Function == 1)
            Acce.BeforeHesEvent.AddListener(BubOpt.ChangeShape);
        else if (Function == 2)
        {
            Acce.BeforeHesEvent.AddListener(BubOpt.ChangeColor);
            Acce.BeforeHesEvent.AddListener(BubOpt.ChangeShape);
        }
        else
            Acce.BeforeHesEvent.AddListener(BubOpt.SaveEffect);
        Acce.BeforeHesEvent.AddListener(GrandParentBubOpt.MoveSelfFront);
        Acce.BeforeHesEvent.AddListener(Acce.SetInvoked);

        Acce.HesEvent.AddListener(GrandParentBubOpt.Close010Subs);
    }
}

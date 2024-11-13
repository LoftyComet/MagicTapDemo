using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleFast3 : MonoBehaviour
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

        Acce.HesEvent.AddListener(Acce.OpenCloseSti);
        Acce.HesEvent.AddListener(Acce.OutMaterial);

        if (Function == 0)
            Acce.OutEvent.AddListener(BubOpt.ChangeColor);
        else if (Function == 1)
            Acce.OutEvent.AddListener(BubOpt.ChangeShape);
        else if (Function == 2)
        {
            Acce.OutEvent.AddListener(BubOpt.ChangeColor);
            Acce.OutEvent.AddListener(BubOpt.ChangeShape);
        }
        else
            Acce.OutEvent.AddListener(BubOpt.SaveEffect);
        Acce.OutEvent.AddListener(GrandParentBubOpt.MoveSelfFront);
        Acce.OutEvent.AddListener(Acce.SetInvoked);

        Acce.OutEvent.AddListener(GrandParentBubOpt.Close010Subs);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAcc1 : MonoBehaviour
{
    public AcceStimulate Acce;
    public BubbleOption BubOpt;
    // Start is called before the first frame update
    void Start()
    {
        Acce = gameObject.GetComponent<AcceStimulate>();
        BubOpt = gameObject.GetComponent<BubbleOption>();

        Acce.BeforeHesEvent.AddListener(Acce.SetInvoked);
        Acce.HesEvent.AddListener(BubOpt.MoveSelfBack);
        Acce.HesEvent.AddListener(BubOpt.OpenSubs);
    }

}

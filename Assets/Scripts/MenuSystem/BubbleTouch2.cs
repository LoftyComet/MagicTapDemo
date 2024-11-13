using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleTouch2 : MonoBehaviour
{
    public BubbleOption ParentBubOpt;
    public BubbleOption BubOpt;
    // Manully Assign
    public BubbleOption[] SibBubOpt;
    public TouchBlast TB;
    void Start()
    {
        ParentBubOpt = transform.parent.GetComponent<BubbleOption>();
        BubOpt = gameObject.GetComponent<BubbleOption>();
        TB = gameObject.GetComponent<TouchBlast>();

        TB.OnTouchStarted.AddListener(MoveSubsBack);
        TB.OnTouchStarted.AddListener(OpenSubs);

        foreach (var s in SibBubOpt)
        {
            TB.BeforeBlastEvent.AddListener(s.Close01Subs);
        }
    }

    public void MoveSubsBack(HandTrackingInputEventData eventData)
    {
        ParentBubOpt.MoveSubsBack();
    }

    public void OpenSubs(HandTrackingInputEventData eventData)
    {
        BubOpt.OpenSubs();
    }
}

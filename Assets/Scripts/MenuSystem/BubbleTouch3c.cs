using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleTouch3c : MonoBehaviour
{
    BubbleOption GrandParentBubOpt;
    BubbleOption ParentBubOpt;
    public TouchBlast TB;
    void Start()
    {
        GrandParentBubOpt = transform.parent.transform.parent.GetComponent<BubbleOption>();
        ParentBubOpt = transform.parent.GetComponent<BubbleOption>();
        TB = gameObject.GetComponent<TouchBlast>();

        TB.OnTouchStarted.AddListener(Close01Subs);
        TB.OnTouchStarted.AddListener(MoveSubsFront);
        //TB.OnTouchStarted.AddListener(MoveSelfFront);
    }

    public void Close01Subs(HandTrackingInputEventData eventData)
    {
        ParentBubOpt.Close01Subs();
    }
    public void MoveSubsFront(HandTrackingInputEventData eventData)
    {
        GrandParentBubOpt.MoveSubsFront();
    }
    public void MoveSelfFront(HandTrackingInputEventData eventData)
    {
        GrandParentBubOpt.MoveSelfFront();
    }
}

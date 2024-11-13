using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleTouch2c : MonoBehaviour
{
    BubbleOption ParentBubOpt;
    public TouchBlast TB;
    void Start()
    {
        ParentBubOpt = transform.parent.GetComponent<BubbleOption>();
        TB = gameObject.GetComponent<TouchBlast>();

        TB.OnTouchStarted.AddListener(MoveSelfFront);
        TB.OnTouchStarted.AddListener(MoveSubsBack);
        TB.OnTouchStarted.AddListener(Close01Subs);
    }

    public void MoveSelfFront(HandTrackingInputEventData eventData)
    {
        ParentBubOpt.MoveSelfFront();
    }

    public void MoveSubsBack(HandTrackingInputEventData eventData)
    {
        ParentBubOpt.MoveSubsBack();
    }

    public void Close01Subs(HandTrackingInputEventData eventData)
    {
        ParentBubOpt.Close01Subs();
    }
}

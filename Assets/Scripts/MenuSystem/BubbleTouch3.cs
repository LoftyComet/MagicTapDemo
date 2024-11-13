using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleTouch3 : MonoBehaviour
{
    BubbleOption GrandParentBubOpt;
    BubbleOption BubOpt;
    public int Function;
    public TouchBlast TB;
    void Start()
    {
        GrandParentBubOpt = transform.parent.transform.parent.GetComponent<BubbleOption>();
        BubOpt = gameObject.GetComponent<BubbleOption>();
        TB = gameObject.GetComponent<TouchBlast>();

        TB.OnTouchStarted.AddListener(Close01Subs);

        if (Function == 0)
            TB.BeforeBlastEvent.AddListener(BubOpt.ChangeColor);
        else if (Function == 1)
            TB.BeforeBlastEvent.AddListener(BubOpt.ChangeShape);
        else if (Function == 2)
        {
            TB.BeforeBlastEvent.AddListener(BubOpt.ChangeColor);
            TB.BeforeBlastEvent.AddListener(BubOpt.ChangeShape);
        }
        else
            TB.BeforeBlastEvent.AddListener(BubOpt.SaveEffect);
        TB.BeforeBlastEvent.AddListener(GrandParentBubOpt.MoveSelfFront);
    }

    public void Close01Subs(HandTrackingInputEventData eventData)
    {
        GrandParentBubOpt.Close01Subs();
    }
}

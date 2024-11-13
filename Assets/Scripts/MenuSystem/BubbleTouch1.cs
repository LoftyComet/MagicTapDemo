using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleTouch1 : MonoBehaviour
{
    public BubbleOption BubOpt;
    public TouchBlast TB;
    void Start()
    {
        BubOpt = gameObject.GetComponent<BubbleOption>();
        TB = gameObject.GetComponent<TouchBlast>();

        TB.OnTouchStarted.AddListener(MoveSelfBack);
        TB.OnTouchStarted.AddListener(OpenSubs);
    }

    public void MoveSelfBack(HandTrackingInputEventData eventData)
    {
        BubOpt.MoveSelfBack();
    }

    public void OpenSubs(HandTrackingInputEventData eventData)
    {
        BubOpt.OpenSubs();
    }

}

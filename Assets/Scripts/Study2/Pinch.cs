using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Pinch : MonoBehaviour
{
    public float ActivateDistance = .02f; //meters
    public float DeactivateDistance = .05f; //meters
    public float distance = 1.0f;
    public bool isPinch = false;
    public UnityEvent PinchStart, PinchEnd;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        Handedness handedness = Handedness.Any;
        HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, handedness, out MixedRealityPose indexPose);
        HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, handedness, out MixedRealityPose thumbPose);
        distance = Vector3.Distance(indexPose.Position, thumbPose.Position);
        if (distance > DeactivateDistance)
        {
            if (isPinch)
            {
                isPinch = false;
                PinchEnd.Invoke();
            }
            
            
            //return;
        }
        else if (distance < ActivateDistance)
        {
            if (!isPinch)
            {
                isPinch = true;
                PinchStart.Invoke();
            }
            
        }
    }
}

using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;

/// <summary>
/// Makes the object follows the position of the index finger tip
/// </summary>
public class HandMarker : MonoBehaviour
{
    public GameObject cylinderMarker;
    public Vector3 PoseRotation;
    MixedRealityPose IndexTipPose;
    Vector3 InitAngles;
    Quaternion NextRotation;
    Vector3 NextAngles;

    void Start()
    {
        cylinderMarker = GameObject.FindGameObjectWithTag("HandMarker");
        InitAngles = new Vector3(68, 180, 0);
        NextRotation.eulerAngles = InitAngles;
    }

    // Update is called once per frame
    void Update()
    {
        cylinderMarker.GetComponent<MeshRenderer>().enabled = false;

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Any, out IndexTipPose))
        {
            PoseRotation = IndexTipPose.Rotation.eulerAngles;
            cylinderMarker.GetComponent<MeshRenderer>().enabled = true;
            cylinderMarker.transform.position = IndexTipPose.Position;

            cylinderMarker.transform.localRotation = NextRotation;
        }
    }
}

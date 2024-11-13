using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleC1 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] C2;
    public GameObject Player;
    public bool Active;
    public float RecoverTarget, up, right, down, left, front, back;
    MixedRealityPose IndexTipPose;
    public Vector3 p, NormalLeft, NormalRight;
    public Plane LeftSide, RightSide;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("PlayerC");
        gameObject.GetComponent<AcceStimulate>().Acce = true; // Use Acce
        Active = false;
        RecoverTarget = 0.0f;
        up = -0.03f;
        right = -0.493f;
        down = -0.143f;
        left = -0.504f;
        front = -0.04f;
        back = 0.122f;
        C2 = new GameObject[transform.childCount - 1];
        C2[0] = GameObject.FindGameObjectWithTag("C2L");
        C2[1] = GameObject.FindGameObjectWithTag("C2C");
        C2[2] = GameObject.FindGameObjectWithTag("C2R");
        foreach (GameObject t in C2)
        {
            t.SetActive(false);
            Bounds temp = t.GetComponent<Collider>().bounds;
            Debug.LogFormat("Tag: {0}, center: {1}, min: {2}, max: {3}, position: {4}", t.tag, temp.center, temp.min, temp.max, t.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        NormalLeft = Vector3.Normalize(C2[1].transform.position - C2[0].transform.position);
        NormalRight = Vector3.Normalize(C2[1].transform.position - C2[2].transform.position);
        LeftSide = new Plane(NormalLeft, C2[0].transform.position - NormalLeft * 0.02f);
        RightSide = new Plane(NormalRight, C2[2].transform.position - NormalRight * 0.02f);
        if (Active)
        {
            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Any, out IndexTipPose))
            {
                p = IndexTipPose.Position;
                //More precisely, it would be two planar equation(0 = ax + bz + c) and y boundaries, now it is rotated 90 so ignored x
                if ( p.y > C2[1].transform.position.y + 0.02f || p.y < transform.position.y - 0.03f || !LeftSide.GetSide(p) || !RightSide.GetSide(p))
                {
                    Activate(false);
                }
            }
        }
    }

    public void HesBehavior()
    {
        Activate(true);
    }

    public void Activate(bool setting)
    {
        foreach (GameObject c in C2)
        {
            c.SetActive(setting);
        }
        Active = setting;
    }


    public void ExeBehavior()
    {
    }

    public void ChangePlayer(Color color)
    {
        Player.GetComponent<MeshRenderer>().material.SetColor("Color_", color);
    }
    public void Inactivate()
    {
        Activate(false);
        foreach(GameObject c in C2)
        {
            c.GetComponent<TouchBlast>().UpdateDissolve(RecoverTarget);
        }
    }

}

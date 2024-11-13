using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using static UnityEngine.UI.CanvasScaler;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Security.AccessControl;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using System.Diagnostics;
using UnityEditor;

/// <summary>
/// Interaction control component for the six interaction methods.
/// Parameters for MenuSystem Demo:
///     WaitTime = 0.5f;
///     HesTime = 0.1f;
///     Vb = 0.2f
///     Ab = -2;
///     Vupper = 1f;
/// </summary>
/// <param name="Delay">Bool for Time Delay mode, set manually</param>
/// <param name="Fast">Bool for Fast tapping mode, set manually</param>
/// <param name="Acce">Bool for Acceleration mode using ABC, set manually</param>
/// <param name="AcceOnly">Bool for Acceleration mode using only B, set manually</param>
/// <param name="AcceOld">Bool for old-version Acceleration mode, set manually</param>
/// <param name="Velo">Bool for Velocity mode using AC, set manually</param>
/// <param name="InEvent"> Event triggered when the finger enters</param>
/// <param name="BeforeHesEvent">Event before the emission of HesEvent</param>
/// <param name="HesEvent">Event emitted when judged as hesitated</param>
/// <param name="OutEvent">Event emitted for normal execution</param>
/// <param name="AfterOutEvent">Event emitted after the finger is out</param>
/// <param name="UpdateT">Time after entering the option bubble</param>
/// <param name="WaitTime">Time waited for time delay mode (Long Tap and Short Tap)</param>
/// <param name="PrepTime">Time after entering 'Prepared' status</param>
/// <param name="HesTime">Time waited for Acce and Velo mode's hesitation confirmation</param>
/// <param name="vpre">Velocity for previous frame</param>
/// <param name="v">Velocity for current frame</param>
/// <param name="vc">Velocity recorded when recording the condition</param>
/// <param name="apre">Acceleration for previous frame</param>
/// <param name="a">Acceleration for current frame</param>
/// <param name="ac">Acceleration recorded when recording the condition</param>
/// <param name="Vb">Velocity bound for Acce and Velo mode's A condition</param>
/// <param name="Vbc">Velocity bound for Acce and Velo mode's C condition</param>
/// <param name="Ab">Acceleration bound for Acce mode's B condition</param>
/// <param name="Vupper">Velocity bound for Acce mode's B condition</param>
/// <param name="Hesitated">Bool set true if judged as hesitated</param>
/// <param name="Init">Bool set true if component start its first calculation</param>
/// <param name="Prepared">Bool set true if judged as prepared</param>
/// <param name="Entered">Bool set true if finger enters the bubble (first 0 of 010)</param>
/// <param name="CloseSti">Bool set true if decides to close the Bubble's OutEvent function</param>
/// <param name="Invoked">Bool set true if the Bubble's OutEvent is functioned</param>
/// <param name="bigger">Bool set true if previous velocity is bigger than Vb, used in line-crossing version</param>
/// <param name="smaller">Bool set true if previous velocity is smaller than Vb, used in line-crossing version</param>
/// <param name="curBigger">Bool set true if current velocity is bigger than Vb, used in line-crossing version</param>
/// <param name="IndexTipPose1">Pose for previous frame's IndexTip pose</param>
/// <param name="IndexTipPose2">Pose for current frame's IndexTip pose</param>
/// <param name="IndexDistalPose">Pose for current Distal joint index finger pose</param>
/// <param name="IndexMiddlePose">Pose for current Middle joint index finger pose</param>
/// <param name="Bounds">Object's Bounds, to manipulate the collider</param>
/// <param name="boundCenter">Object bounds' center point</param>
/// <param name="Debugger">Dynamic linking text debugger shower: Row 1 = velocity, Row 2 = Acceleration, ABC = trigger condition</param>
/// <param name="ShowDebug">Bool set true if want to show debugger</param>
/// <param name="condition">Trigger condition in the IxDL: A B or C</param>
/// <param name="OutMate">Material when the finger is out of the button</param>
/// <param name="InMate">Material when the finger is in the button</param>

[SerializeField]
public class FingerInstance
{
    public Handedness handedness;
    public TrackedHandJoint TrackedJoint;
    public MixedRealityPose PosePrevious;
    public MixedRealityPose PoseCurrent;
    public MixedRealityPose DistalPose;

    public FingerInstance(Handedness Hand, TrackedHandJoint Joint)
    {
        handedness = Hand;
        TrackedJoint = Joint;
        GetPose(Hand, Joint);
    }

    public void GetPose(Handedness Hand, TrackedHandJoint Joint)
    {
    }
}
public class AcceStimulateForKeyboardRestructured : MonoBehaviour
{
    public List<FingerInstance> Fingers;
    [TextArea(3, 10)]
    public string ScriptDescription = "This Script is designed ONLY for this keyboard, I have commented out all lines that drives the material functionality. " +
        "If you want to use anything with changing material, make sure you write them in a different script or uncomment all the lines related to " +
        "OutMate, InMate and HighlightMate";

    public UnityEvent InEvent, BeforeHesEvent, HesEvent, OutEvent, AfterOutEvent;


    [TextArea(3, 10)]
    public string AttributeDescription = "Make sure changing attributes in here to make HesEvent work";
    public float UpdateT, WaitTime, PrepTime, HesTime, vpre, v, vc, apre, a, ac, Vb, Vbc, Ab, Vupper;
    public bool HighLighted, Hesitated, Init, Delay, Fast, FastForKeyboard, Acce, AcceOnly, AcceOld, Velo, Prepared, Entered, CloseSti, Invoked, bigger, smaller, curBigger;
    public MixedRealityPose IndexTipPose1;
    public MixedRealityPose IndexTipPose2;
    public MixedRealityPose IndexDistalPose;
    Bounds Bounds;
    public Vector3 boundCenter;
    public TMP_Text[] Debugger;
    public bool showDebug;
    public char condition;
    public Material OutMate, InMate, HLMate;
    public MeshRenderer meshRenderer;
    public S2SetVariable ParentSSV;
    public float Distance; 


    void Start()
    {
        condition = 'N';
        HighLighted = false;
        Hesitated = false;
        Init = true;
        Prepared = false;
        Entered = false;
        CloseSti = false;
        Invoked = false;
        bigger = false;
        smaller = false;
        curBigger = false;
        UpdateT = 0.0f;
        vpre = float.MaxValue;
        v = float.MaxValue;
        apre = float.MaxValue;
        a = float.MaxValue;

        Bounds = gameObject.GetComponent<Collider>().bounds;
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        ParentSSV = transform.parent.GetComponent<S2SetVariable>();

        InEvent ??= new UnityEvent();
        BeforeHesEvent ??= new UnityEvent();
        HesEvent ??= new UnityEvent();
        OutEvent ??= new UnityEvent();
        AfterOutEvent ??= new UnityEvent();

    }

    void Update()
    {
        // Make sure the collider moves with the body locked transform
        Bounds.center = transform.position;
    }

    void FixedUpdate()
    {
        Bounds = gameObject.GetComponent<Collider>().bounds;
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Any, out IndexTipPose2))
        {
            #region V and A Calculation: V is without +-, A has +- for V's tendency to change bigger or smaller
            if (Init)
            {
                Init = false;
                IndexTipPose1 = IndexTipPose2;
                return;

            }

            Distance = (IndexTipPose2.Position - IndexTipPose1.Position).magnitude;
            v = Distance / Time.fixedDeltaTime;
            //print("P1  " + IndexTipPose1.Position.ToString("F5"));
            //print("P2  " + IndexTipPose2.Position.ToString("F5"));
            //if (v > Vb)
            //    curBigger = true;
            //else
            //    curBigger = false;
            IndexTipPose1 = IndexTipPose2;
            if (vpre != float.MaxValue)
            {
                a = (v - vpre) / Time.fixedDeltaTime;
            }
            //UnityEngine.Debug.LogFormat("{0}, {1}, {2}",vpre, v, a);
            vpre = v;
            apre = a;

            #endregion

            #region 010 Behavior
            // Try get the position of Distal and Middle Joint
            HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexDistalJoint, Handedness.Any, out IndexDistalPose);
            // Judge if the fingertip is inside
            if (Bounds.Contains(IndexTipPose2.Position) || Bounds.Contains(IndexDistalPose.Position))
            {
                //UnityEngine.Debug.LogFormat("Inside {0}", name);
                Entered = true;
                if (!Invoked)
                {
                    InEvent.Invoke();
                }
                // Change Outlo
                if (!CloseSti && !HighLighted)
                    //meshRenderer.material = InMate;
                UpdateT += Time.deltaTime;

                // If already enter hesitate state, do not act more
                if (Hesitated)
                    return;
                // else, act as the set mode (Delay-Long Tap, Fast-Short Tap, Acce, or Velo)
                if (Delay || Fast || FastForKeyboard)
                {
                    if (UpdateT > WaitTime)
                    {
                        // Invoke the HesEvent if time exceeds the WaitTime
                        if (!Invoked)
                        {
                            BeforeHesEvent.Invoke();
                            HesEvent.Invoke();
                        }
                        Hesitated = true;
                    }
                }
                else if (Acce)
                {
                    if (Prepared)
                    {
                        PrepTime += Time.deltaTime;
                        if (PrepTime >= HesTime)
                        {
                            //Invoke the HesEvent if prepared status exceeds the HesTime
                            if (!Invoked)
                            {
                                BeforeHesEvent.Invoke();
                                HesEvent.Invoke();
                            }
                            Hesitated = true;
                        }
                        // Condition C
                        else if (JudgeC())
                        {
                            Prepared = false;
                        }
                    }
                    // Condition A and B
                    else if (JudgeAcce())
                    {
                        PrepTime = 0;
                        Prepared = true;
                    }
                }
                else if (Velo)
                {
                    if (Prepared)
                    {
                        PrepTime += Time.deltaTime;
                        if (PrepTime > HesTime)
                        {
                            //Invoke the HesEvent if prepared status exceeds the HesTime
                            if (!Invoked)
                            {
                                BeforeHesEvent.Invoke();
                                HesEvent.Invoke();
                            }
                            Hesitated = true;
                        }
                        // Condition C
                        else if (JudgeC())
                        {
                            Prepared = false;
                        }
                    }
                    // Condition A
                    else if (JudgeV())
                    {
                        PrepTime = 0;
                        Prepared = true;
                    }
                }
                else if (AcceOnly)
                {
                    if (Prepared)
                    {
                        PrepTime += Time.deltaTime;
                        if (PrepTime > HesTime)
                        {
                            //Invoke the HesEvent if prepared status exceeds the HesTime
                            if (!Invoked)
                            {
                                BeforeHesEvent.Invoke();
                                HesEvent.Invoke();
                            }
                            Hesitated = true;
                        }
                    }
                    // Condition B
                    else if (JudgeAcceOnly())
                    {
                        PrepTime = 0;
                        Prepared = true;
                    }
                }
                else if (AcceOld)
                {
                    // Condition A and B
                    if (JudgeAcce())
                    {
                        //Invoke the HesEvent if prepared status exceeds the HesTime
                        if (!Invoked)
                        {
                            BeforeHesEvent.Invoke();
                            HesEvent.Invoke();
                        }
                        Hesitated = true;
                    }
                }
                #region Old Line-crossing version
                // Used when implementing lineCrossing AC
                if (curBigger)
                {
                    bigger = true;
                    smaller = false;
                }
                else
                {
                    smaller = true;
                    bigger = false;
                }
                #endregion
            }
            else
            {
                // After the 0 of 010, performing the 10 of 010
                if (Entered)
                {
                    if (!CloseSti && !Invoked)
                    {
                        // Invoke the OutEvent (the 10 of 010)
                        OutEvent.Invoke();
                    }
                    AfterOutEvent.Invoke();
                    // Out of the inner area
                    if (!HighLighted)
                    {
                        //OutMaterial();
                    }
                }
                UpdateT = 0;
                Prepared = false;
                Entered = false;
                Hesitated = false;
                CloseSti = false;
                condition = ' ';
            }
            #endregion
        }
    }

    public void showDebugger()
    {
        if (showDebug)
        {
            Debugger[0].text = "A=" + ac.ToString();
            Debugger[1].text = "V=" + vc.ToString();
            Debugger[2].text = "T=" + UpdateT.ToString();
            Debugger[3].text = "C=" + condition.ToString();
        }
    }
    /// <summary>
    /// Set ClostSti as true
    /// </summary>
    public void OpenCloseSti()
    {
        CloseSti = true;
    }

    /// <summary>
    /// Condition A and B for Acce mode, line-crossing version
    /// </summary>
    /// <returns></returns>
    public bool JudgeAcceLineCrossing()
    {
        if (vpre != float.MaxValue && apre != float.MaxValue)
        {
            if ((v < Vupper && a < Ab))
            {
                condition = 'B';
                return true;
            }

            if (bigger && !curBigger)
            {
                condition = 'A';
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Condition A and B for Acce mode
    /// </summary>
    /// <returns></returns>
    public bool JudgeAcce()
    {
        if (vpre != float.MaxValue && apre != float.MaxValue)
        {
            if (v < Vupper && a < Ab)
            {
                ac = a;
                vc = v;
                condition = 'B';
                return true;
            }

            if (v < Vb)
            {
                ac = a;
                vc = v;
                condition = 'A';
                return true;
            }
        }
        return false;
    }

    public bool JudgeCLineCrossing()
    {
        return smaller && curBigger;
    }

    public bool JudgeC()
    {
        if (v > Vbc)
        {
            condition = 'C';
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Condition B for Acce mode
    /// </summary>
    /// <returns></returns>
    public bool JudgeAcceOnly()
    {
        if (vpre != float.MaxValue && apre != float.MaxValue)
        {
            if ((v < Vupper && a < Ab))
            {
                condition = 'B';
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Condition A for Velo mode, line-crossing version
    /// </summary>
    /// <returns></returns>
    public bool JudgeVLineCrossing()
    {
        if (vpre != float.MaxValue && apre != float.MaxValue)
        {
            if (bigger && !curBigger)
            {
                condition = 'A';
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Condition A for Velo mode, line-crossing version
    /// </summary>
    /// <returns></returns>
    public bool JudgeV()
    {
        if (vpre != float.MaxValue && apre != float.MaxValue)
        {
            if (v < Vb)
            {
                condition = 'A';
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Set Invoked as true
    /// </summary>
    public void SetInvoked()
    {
        Invoked = true;
    }

    /// <summary>
    /// Set Invoked as false
    /// </summary>
    public void UnInvoked()
    {
        Invoked = false;
    }

    public void OutMaterial()
    {
        //meshRenderer.material = OutMate;
    }

    public void InMaterial()
    {
        //meshRenderer.material = InMate;
    }
    public void ToggleHLMaterial()
    {
        if (HighLighted)
            HighLighted = false;
        else
        {
            HighLighted = true;
            meshRenderer.material = HLMate;
        }

    }

}



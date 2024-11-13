using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleWithExecute : MonoBehaviour
{

    public AcceStimulate Acce;
    public BubbleOption BubOpt;
    public TouchBlast CancelTB;
    public GameObject Cancel;
    public Color[] ColorToChange;
    private int ColorIndex;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Cancel = transform.Find(name + "-Ca").gameObject;
        CancelTB = Cancel.GetComponent<TouchBlast>();
        Cancel.SetActive(false);

        Acce = gameObject.GetComponent<AcceStimulate>();
        BubOpt = gameObject.GetComponent<BubbleOption>();

        ColorIndex = 0;
        ColorToChange = new Color[6] { Color.red, Color.green, Color.blue, Color.yellow, Color.cyan, new Color(0.6666667f, 0.6666667f, 0.6666667f, 1) };

        Acce.HesEvent.AddListener(OpenCancel);
        Acce.HesEvent.AddListener(BubOpt.EnlargeSphereColliderRadius);

        Acce.OutEvent.AddListener(CloseCancel);

        Acce.AfterOutEvent.AddListener(BubOpt.RestoreSphereColliderRadius);

        CancelTB.OnTouchStarted.AddListener(OpenCloseSti);
        CancelTB.OnTouchStarted.AddListener(CloseCancelTB);
        CancelTB.OnTouchStarted.AddListener(ChangeColor);
        //CancelTB.OnTouchStarted.AddListener(RestoreSphereColliderRadius);
        CancelTB.OnTouchStarted.AddListener(UnInvokedTB);
    }

    public void OpenCloseSti(HandTrackingInputEventData eventData)
    {
        Acce.OpenCloseSti();
    }
    public void OpenCancel()
    {
        Cancel.SetActive(true);
    }
    public void CloseCancel()
    {
        CancelTB.SetInvoked(false);
        Cancel.SetActive(false);
    }
    public void CloseCancelTB(HandTrackingInputEventData eventData)
    {
        CloseCancel();
    }
    public void UnInvokedTB(HandTrackingInputEventData eventData)
    {
        Acce.UnInvoked();
    }
    public void RestoreSphereColliderRadius(HandTrackingInputEventData eventData)
    {
        BubOpt.RestoreSphereColliderRadius();
    }
    public void ChangeColor(HandTrackingInputEventData eventData)
    {
        player.GetComponent<MeshRenderer>().material.SetColor("Color_", ColorToChange[ColorIndex]);
        if (ColorIndex == 5)
            ColorIndex = 0;
        else
            ColorIndex++;
        gameObject.GetComponent<TouchBlast>().Invoked = false;
    }
}


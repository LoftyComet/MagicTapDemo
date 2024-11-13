using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleWithExecute1 : MonoBehaviour
{

    public AcceStimulate Acce, ExecuteAcce;
    public BubbleOption BubOpt;
    public TouchBlast CancelTB;
    public GameObject Cancel;
    public Color[] ColorToChange;
    private int ColorIndex;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        // Find green game execution bar or cancel bar
        Cancel = transform.Find(name + "-Ca").gameObject;
        // Old Mode
        CancelTB = Cancel.GetComponent<TouchBlast>();
        // New Mode
        ExecuteAcce = Cancel.GetComponent<AcceStimulate>();

        // Deactivate green execution bar
        Cancel.SetActive(false);

        // Get acceleration Simulation
        Acce = gameObject.GetComponent<AcceStimulate>();

        // Get Bubble Option
        BubOpt = gameObject.GetComponent<BubbleOption>();

        ColorIndex = 0;
        ColorToChange = new Color[6] { Color.red, Color.green, Color.blue, Color.yellow, Color.cyan, new Color(0.6666667f, 0.6666667f, 0.6666667f, 1) };

        Acce.HesEvent.AddListener(OpenCancel);
        Acce.HesEvent.AddListener(BubOpt.EnlargeSphereColliderRadius);

        Acce.OutEvent.AddListener(CloseCancel);

        Acce.AfterOutEvent.AddListener(BubOpt.RestoreSphereColliderRadius);

        ExecuteAcce.InEvent.AddListener(OpenCloseSti);
        ExecuteAcce.InEvent.AddListener(CloseExecuteAcce);
        ExecuteAcce.InEvent.AddListener(ChangeColor);
        ExecuteAcce.InEvent.AddListener(UnInvokedAcce);
        //CancelTB.OnTouchStarted.AddListener(OpenCloseSti);
        //CancelTB.OnTouchStarted.AddListener(CloseCancelTB);
        //CancelTB.OnTouchStarted.AddListener(ChangeColor);
        //CancelTB.OnTouchStarted.AddListener(RestoreSphereColliderRadius);
        //CancelTB.OnTouchStarted.AddListener(UnInvokedTB);
    }

    public void OpenCloseSti()
    {
        Acce.OpenCloseSti();
    }

    // Set cancel button active.
    public void OpenCancel()
    {
        Cancel.SetActive(true);
    }
    public void CloseCancel()
    {
        CancelTB.SetInvoked(false);
        Cancel.SetActive(false);
        ExecuteAcce.UnInvoked();
    }
    public void CloseExecuteAcce()
    {
        CloseCancel();
    }
    public void UnInvokedAcce()
    {
        Acce.UnInvoked();
    }
    public void RestoreSphereColliderRadius(HandTrackingInputEventData eventData)
    {
        BubOpt.RestoreSphereColliderRadius();
    }
    public void ChangeColor()
    {
        player.GetComponent<MeshRenderer>().material.SetColor("Color_", ColorToChange[ColorIndex]);
        if (ColorIndex == 5)
            ColorIndex = 0;
        else
            ColorIndex++;
        gameObject.GetComponent<TouchBlast>().Invoked = false;
    }
}


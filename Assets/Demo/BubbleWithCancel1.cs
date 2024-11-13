using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleWithCancel1 : MonoBehaviour
{
    public AcceStimulate Acce, CancelAcce;
    public BubbleOption BubOpt;
    public TouchBlast CancelTB;
    public GameObject Cancel;
    // Start is called before the first frame update
    void Start()
    {
        Cancel = transform.Find(name + "-Ca").gameObject;
        CancelTB = Cancel.GetComponent<TouchBlast>();
        CancelAcce = Cancel.GetComponent<AcceStimulate>();
        Cancel.SetActive(false);

        Acce = gameObject.GetComponent<AcceStimulate>();
        BubOpt = gameObject.GetComponent<BubbleOption>();

        Acce.HesEvent.AddListener(OpenCancel);
        Acce.HesEvent.AddListener(BubOpt.EnlargeSphereColliderRadius);

        Acce.OutEvent.AddListener(CloseCancel);

        Acce.AfterOutEvent.AddListener(BubOpt.RestoreSphereColliderRadius);

        CancelAcce.InEvent.AddListener(OpenCloseSti);
        CancelAcce.InEvent.AddListener(CloseCancelAcce);
        CancelAcce.InEvent.AddListener(UnInvokedAcce);
        //CancelTB.OnTouchStarted.AddListener(OpenCloseSti);
        //CancelTB.OnTouchStarted.AddListener(CloseCancelTB);
        ////CancelTB.OnTouchStarted.AddListener(RestoreSphereColliderRadius);
        //CancelTB.OnTouchStarted.AddListener(UnInvokedTB);
    }

    public void OpenCloseSti()
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
        CancelAcce.UnInvoked();
    }
    public void CloseCancelAcce()
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
}

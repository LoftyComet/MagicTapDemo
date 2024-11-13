using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Bubble Blast after touch behavior for 01 options
/// </summary>
/// <param name="OnTouchStarted">Touch start event</param>
/// <param name="OnTouchUpdated">Touching event</param>
/// <param name="OnTouchUpCompleted">Touch Complete event</param>
/// <param name="BeforeBlastEvent">Before 01 blast event</param>
/// <param name="TargetRenderer">Object's MeshRenderer component</param>
/// <param name="Material">Object's Material variable</param>
/// <param name="DissolveTarget">Float for dissolving effect</param>
/// <param name="_DissolveSpeed">Float for dissolving effect speed</param>
/// <param name="Invoke">Bool for whether it has blasted</param>
public class TouchBlast : MonoBehaviour, IMixedRealityTouchHandler
{
    #region Event handlers
    public TouchEvent OnTouchCompleted;
    public TouchEvent OnTouchStarted;
    public TouchEvent OnTouchUpdated;
    public UnityEvent BeforeBlastEvent;
    #endregion

    private MeshRenderer TargetRenderer;
    private Material material;
    private float _DissolveSpeed;
    public float DissolveTarget;
    public bool Invoked;

    private void Start()
    {
        _DissolveSpeed = 10;
        DissolveTarget = 1.0f; // LRD
        TargetRenderer = GetComponent<MeshRenderer>();
        Invoked = false;
        if ((TargetRenderer != null) && (TargetRenderer.material != null))
        {
            material = TargetRenderer.sharedMaterial;
        }
        if (BeforeBlastEvent == null)
        {
            BeforeBlastEvent = new UnityEvent();
        }
    }

    void IMixedRealityTouchHandler.OnTouchUpdated(HandTrackingInputEventData eventData)
    {
        OnTouchUpdated.Invoke(eventData);
    }

    void IMixedRealityTouchHandler.OnTouchCompleted(HandTrackingInputEventData eventData)
    {
        Debug.LogFormat("Touch complete: name {0}", gameObject.name);
        OnTouchCompleted.Invoke(eventData);
    }

    void IMixedRealityTouchHandler.OnTouchStarted(HandTrackingInputEventData eventData)
    {
        Debug.LogFormat("Touch start: name {0}", gameObject.name);
        if (!Invoked)
        {
            StartCoroutine(Burst_Iterator(DissolveTarget));
            //Before invoke the option, close the sibling
            BeforeBlastEvent.Invoke();
            //Then invoke
            Invoked = true;
            OnTouchStarted.Invoke(eventData);
        }
    }

    public void UpdateDissolve(float target)
    {
        material.SetFloat("_Dissolve", target);
    }

    IEnumerator Burst_Iterator(float target)
    {
        float start = material.GetFloat("_Dissolve");
        float lerp = 0;
        while (lerp < 1)
        {
            material.SetFloat("_Dissolve", Mathf.Lerp(start, target, lerp));
            lerp += Time.deltaTime * _DissolveSpeed;
            yield return null;
        }
    }

    public void SetInvoked(bool setting)
    {
        Invoked = setting;
    }

}

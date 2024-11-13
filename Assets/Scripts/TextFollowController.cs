using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the camera follow effect for the text
/// </summary>
/// <param name="Enable">
/// Enable == true, then all texts set the camera following effect on;
/// Enable == false, then all texts set the camera following effect off
/// </param>
public class TextFollowController : MonoBehaviour
{
    public bool Enable;
    void Start()
    {
        SetAllOpts(gameObject);
        //Debug.LogFormat("Name: {0}, pos: {1}",gameObject.name, transform.position);
    }

    public void SetAllOpts(GameObject Root)
    {
        if (Root.transform.childCount == 0) { return; }
        foreach (Transform t in Root.transform)
        {
            SetAllOpts(t.gameObject);
            if (t.gameObject.name.StartsWith("Opt"))
            {
                t.gameObject.GetComponent<FollowCamera>().enabled = Enable;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
/*
 * Author: Zhou Xinyi
 * Date: 2022/11/04
 * Desc:
 * BubbleOption is a utility class for all option bubbles. Functions within this class can
 * be organized in various ways to create different behavioral types of bubbles.
 * For example, BubbleTouch1 utilizes MoveSelfBack and OpenSubs for behavioral implementation.
 * Version: 1.0
 */
/// <param name="Player">The gameobject that can change color and shape</param>
/// <param name="SaveText">The gameobject of "saved" text</param>
/// <param name="SubBubbles">Object's children option bubbles gameobjects</param>
/// <param name="SubBubCnt">Object's number of children option bubbles</param>
/// <param name="TargetRenderer">Object's MeshRenderer component</param>
/// <param name="Material">Object's Material variable</param>
/// <param name="source">Object's AudioSource component</param>
/// <param name="clip">Object's AucioClip variable for sound effect</param>
/// <param name="DissolveTarget">Float for TouchBlast component's dissolving effect</param>
/// <param name="RecoverTarget">Float for TouchBlast component's dissolve recovering effect</param>
/// <param name="MoveSpeed">Float for movement speed</param>
/// <param name="isLeaf">Bool for whether it is the leaf option bubble</param>
/// <param name="ColorToChange">Set this color to the Player</param>
/// <param name="MeshToChange">Set this mesh to the Player</param>
/// <param name="MoveDistVec">Vector3 for level 1 option's moving distance</param>
/// <param name="SubDistVec">Vector3 for other options' moving distance</param>
/// 
public class BubbleOption : MonoBehaviour
{
    public GameObject Player, SaveText;
    public GameObject[] SubBubbles;
    int SubBubCnt;
    private MeshRenderer TargetRenderer;
    private Material material;
    public AudioSource source;
    public AudioClip clip;
    public float DissolveTarget, RecoverTarget, MoveSpeed;
    public bool isLeaf;
    
    public Color ColorToChange;
    public Mesh MeshToChange;
    public Vector3 MoveDistVec, SubDistVec;

    void Start()
    {
        TargetRenderer = GetComponent<MeshRenderer>();
        if ((TargetRenderer != null) && (TargetRenderer.material != null))
        {
            material = TargetRenderer.sharedMaterial;
        }

        #region Initialize SubBubbles
        if (isLeaf || gameObject.name.EndsWith("-C"))
            SubBubCnt = 0;
        else
            SubBubCnt = transform.childCount - 1 > 4 ? 4 : transform.childCount - 1; // -1 is for the text object
        int i = 0;
        SubBubbles = new GameObject[SubBubCnt];
        foreach (Transform t in transform)
        {
            if (t.gameObject.tag == "Option")
            {
                SubBubbles[i++] = t.gameObject;
                t.gameObject.SetActive(false);
            }
        }
        #endregion

        #region Initialize Player and Save Text
        //Player = GameObject.FindGameObjectWithTag("Player" + name[0]);
        //SaveText = Player.transform.GetChild(0).gameObject;
        //SaveText.SetActive(false);
        #endregion

        DissolveTarget = 1.0f;
        RecoverTarget = 0.0f;
        MoveSpeed = 5;
        clip = Resources.Load<AudioClip>("Audio/Assets_MRTK_StandardAssets_Audio_MRTK_Manipulation_Start");
        source = GetComponent<AudioSource>();
        source.clip = clip;
        MoveDistVec = new Vector3(0, 0, 0.02f); // subLocalPosition 0.5* local scale 0.5
        SubDistVec = new Vector3(0, 0, -0.5f);
    }

    /// <summary>
    /// For bubbles using TouchBlast, close the children bubbles
    /// </summary>
    public void Close01Subs()
    {
        source.Play();
        UpdateDissolve(RecoverTarget);
        gameObject.GetComponent<TouchBlast>().SetInvoked(false);
        foreach (GameObject b in SubBubbles)
        {
            if (b == null)
                continue;
            // If invoked, enter another close01, update by above code; else, no need to update
            if (b.GetComponent<TouchBlast>().Invoked)
            {
                b.GetComponent<BubbleOption>().Close01Subs();
            }
            b.GetComponent<TouchBlast>().SetInvoked(false);
            b.SetActive(false);
        }
    }

    /// <summary>
    /// For bubbles using AcceStimulate, close the children bubbles
    /// </summary>
    public void Close010Subs()
    {
        source.Play();
        UpdateDissolve(RecoverTarget);
        gameObject.GetComponent<AcceStimulate>().UnInvoked();
        foreach (GameObject b in SubBubbles)
        {
            if (b == null)
                continue;
            if (b.GetComponent<AcceStimulate>().Invoked)
            {
                b.GetComponent<BubbleOption>().Close010Subs();
            }
            b.GetComponent<AcceStimulate>().UnInvoked();
            b.SetActive(false);
        }
    }

    /// <summary>
    /// Open the children bubbles
    /// </summary>
    public void OpenSubs()
    {
        source.Play();
        UpdateDissolve(DissolveTarget);
        foreach (GameObject s in SubBubbles)
        {
            if (s == null)
                continue;
            if (!s.GetComponent<BubbleOption>().isLeaf)
            {
                s.transform.localPosition += SubDistVec;
            }
            s.SetActive(true);
        }
    }

    /// <summary>
    /// Change the Player's color
    /// </summary>
    public void ChangeColor()
    {
        Player.GetComponent<MeshRenderer>().material.SetColor("Color_", ColorToChange);
    }

    /// <summary>
    /// Change the Player's Mesh shape
    /// </summary>
    public void ChangeShape()
    {
        Player.GetComponent<MeshFilter>().sharedMesh = MeshToChange;
    }
    
    /// <summary>
    /// Show the save text for a while then hide the save text
    /// </summary>
    public void SaveEffect()
    {
        ShowSave();
        Invoke("HideSave", 3);
    }
    public void ShowSave()
    {
        SaveText.SetActive(true);
    }
    public void HideSave()
    {
        SaveText.SetActive(false);
    }

    /// <summary>
    /// Move the children bubble farer away
    /// </summary>
    public void MoveSubsBack()
    {
        StartCoroutine(MoveSubsBack_Iterator(0));
    }
    
    /// <summary>
    /// Move the children bubble closer
    /// </summary>
    public void MoveSubsFront()
    {
        StartCoroutine(MoveSubsBack_Iterator(SubDistVec.z));
    }
    IEnumerator MoveSubsBack_Iterator(float z)
    {
        float lerp = 0;
        Vector3[] targets = new Vector3[SubBubbles.Length];
        int i = 0;
        foreach (GameObject b in SubBubbles)
        {
            targets[i] = b.GetComponent<RectTransform>().localPosition;
            // Move to be parallel with the backed first-level bubble
            targets[i++].z = z;
        }
        while (lerp < 1)
        {
            i = 0;
            foreach (GameObject b in SubBubbles)
            {
                RectTransform rt = b.GetComponent<RectTransform>();
                rt.localPosition = Vector3.Lerp(rt.localPosition, targets[i++], lerp);
            }
            lerp += Time.deltaTime * MoveSpeed;
            yield return null;
        }
    }

    /// <summary>
    /// Move the option bubble itself farer away
    /// </summary>
    public void MoveSelfBack()
    {
        StartCoroutine(MoveSelf_Iterator(MoveDistVec));
    }

    /// <summary>
    /// Move the option bubble itself closer
    /// </summary>
    public void MoveSelfFront()
    {
        StartCoroutine(MoveSelf_Iterator(-MoveDistVec));
    }


    IEnumerator MoveSelf_Iterator(Vector3 dist)
    {
        float lerp = 0;
        Vector3 target = gameObject.GetComponent<RectTransform>().localPosition + dist;
        while (lerp < 1)
        {
            RectTransform rt = gameObject.GetComponent<RectTransform>();
            rt.localPosition = Vector3.Lerp(rt.localPosition, target, lerp);
            lerp += Time.deltaTime * MoveSpeed;
            yield return null;
        }
    }
    
    /// <summary>
    /// Enlarge the bubble sphere's collider radius (only for sphere collider)
    /// </summary>
    public void EnlargeSphereColliderRadius()
    {
        gameObject.GetComponent<SphereCollider>().radius = 0.7f;
    }

    /// <summary>
    /// Restore the bubble sphere's collider radius to normal (only for sphere collider)
    /// </summary>
    public void RestoreSphereColliderRadius()
    {
        gameObject.GetComponent<SphereCollider>().radius = 0.5f;
    }
    
    /// <summary>
    /// Update the dissolve effect for TouchBlast component
    /// </summary>
    /// <param name="target"></param>
    public void UpdateDissolve(float target)
    {
        material.SetFloat("_Dissolve", target);
    }
}

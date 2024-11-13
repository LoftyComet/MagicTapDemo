using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleA1 : MonoBehaviour
{
    // Status: 0 is initial status, 1 is blasted/chosen status, 2 is beside blasted/chosen status
    public int Status;
    public object MoveDistVec;
    public float RecoverTarget;
    public float MoveDist;
    public bool HasShield, isSub;
    public GameObject[] Bubbles2;
    // Start is called before the first frame update
    void Start()
    {
        Status = 0;
        RecoverTarget = 0.1f;
        HasShield = true;
        MoveDistVec = new Vector3(0, -0.05f, 0.1f);
        Bubbles2 = new GameObject[transform.childCount - 1];
        MoveDist = 0.1f;
        if (isSub)
        {
            RectTransform rt = gameObject.GetComponent<RectTransform>();
            rt.position -= new Vector3(0, 0, MoveDist);
        }
        int i = 0;
        foreach (Transform t in transform)
        {
            if (t.gameObject.tag == "Option")
            {
                Bubbles2[i++] = t.gameObject;
                t.gameObject.SetActive(false);
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void BlastBehavior()
    {
        // Choose A1 for the first time
        Debug.LogFormat("BlastBehavior: Status {0}, hasShield {1}, name {2}", Status, HasShield, gameObject.name);
        if (Status == 0)
        {
            Status = 1;
            HasShield = false;
            gameObject.SendMessageUpwards("Move", MoveDistVec);
            gameObject.SendMessageUpwards("ChangeBlastB", gameObject);
            ActivateChild();
        }
        // Choose A1 after one choosing event
        else if (Status == 2)
        {
            HasShield = false;
            gameObject.SendMessageUpwards("RecoverBlast");
            gameObject.SendMessageUpwards("ChangeBlastB", gameObject);
            Status = 1;
            ActivateChild();
        }
        // If there will be a second blast event after the normal one, delete the status=0 in GroupA.RecoverBlast
        //else if (Status == 1 && HasShield)
        //{
        //    Status = 0;
        //}
        // If be in the bubble for too long, will have two blast event
        //else if (Status == 1 && !HasShield)
        //{
        //    gameObject.SendMessageUpwards("CloseA2");
        //}
    }

    public void ActivateChild()
    {
        foreach (GameObject b in Bubbles2)
        {
            b.SetActive(true);
        }
    }

    public void InactivateChild(int level)
    {
        foreach (GameObject b in Bubbles2)
        {
            b.SetActive(false);
            b.GetComponent<TouchBlast>().UpdateDissolve(RecoverTarget);
        }
    }

}

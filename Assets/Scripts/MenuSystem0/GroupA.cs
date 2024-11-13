using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GroupA : MonoBehaviour
{
    public GameObject[] Bubbles1;
    public GameObject BlastB;
    public GameObject Player;
    public float RecoverTarget;
    public float MoveSpeed;
    public Vector3 RecDist;
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        RecoverTarget = 0.0f;
        MoveSpeed = 1;
        RecDist = new Vector3(0, 0.05f, -0.1f);
        //Bubbles1 = GameObject.FindGameObjectsWithTag("Option");
        Player = GameObject.FindGameObjectWithTag("PlayerA");
        BlastB = null;
        Bubbles1 = new GameObject[transform.childCount - 1];
        foreach (Transform t in transform)
        {
            if (t.gameObject.tag == "Option")
            {
                Bubbles1[i++] = t.gameObject;
                //t.gameObject.SetActive(false);
            }

        }
    }


    public void Move(Vector3 dist)
    {
        Debug.LogFormat("Move {0}", gameObject.name);
        // The next status is the initial status by default
        int nextStatus = 0;
        // If move far away, mark bubble other than the blast 2, move all the bubbles far
         if (dist[2] > 0)
        {
            Debug.Log(dist[2]);
            nextStatus = 2;
        }
        //Debug.LogFormat("Move Instantiated with next status{0}, dist {1}", nextStatus, dist);
        StartCoroutine(Move_Iterator(dist));
        foreach (GameObject b in Bubbles1)
        {
            BubbleA1 ba = b.GetComponent<BubbleA1>();
            //RectTransform rt = b.GetComponent<RectTransform>();
            //rt.position += dist;
            if (ba.HasShield && ba.Status != 1)
            {
                ba.Status = nextStatus;
            }
        }
    }

    IEnumerator Move_Iterator(Vector3 dist)
    {
        float lerp = 0;
        Vector3[] targets = new Vector3[Bubbles1.Length];
        int i = 0;
        foreach (GameObject b in Bubbles1)
        {
            targets[i++] = b.GetComponent<RectTransform>().position + dist;
        }
        while (lerp < 1)
        {
            i = 0;
            foreach (GameObject b in Bubbles1)
            {
                RectTransform rt = b.GetComponent<RectTransform>();
                rt.position = Vector3.Lerp(rt.position, targets[i++], lerp);
            }
            lerp += Time.deltaTime * MoveSpeed;
            yield return null;
        }
    }

    public void CloseSubs()
    {
        Debug.LogFormat("Close A2: name {0}",gameObject.name);
        RecoverBlast();
        Move(RecDist);
    }

    public void RecoverBlast()
    {
        BubbleA1 bubbleA1 = BlastB.GetComponent<BubbleA1>();
        BlastB.GetComponent<TouchBlast>().UpdateDissolve(RecoverTarget);
        bubbleA1.HasShield = true;
        bubbleA1.Status = 0;
        bubbleA1.InactivateChild(2);
    }

    public void ChangeBlastB(GameObject nextBlastB)
    {
        if (BlastB != null && !BlastB.name.Equals(nextBlastB.name))
        {
            BubbleA1 b = BlastB.GetComponent<BubbleA1>();
            b.Status = 2;
        }
        BlastB = nextBlastB;
    }

    public void ChangePlayer(Color color)
    {
        Player.GetComponent<MeshRenderer>().material.SetColor("Color_", color);
    }

    public void InactivateChild(int level)
    {
        if (level == 1)
        {
            // Execute Behavior
            foreach (GameObject b in Bubbles1)
            {
                //b.SetActive(false); // Temparary, no execute
                b.GetComponent<TouchBlast>().UpdateDissolve(RecoverTarget);
                b.GetComponent<BubbleA1>().Status = 0;
                b.GetComponent<BubbleA1>().HasShield = true;
            }
        }else if (level == 2)
        {
            Move(RecDist);
        }
    }

    public void Log(string str)
    {
        Debug.Log(str);
    }
}

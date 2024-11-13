using System.Collections;
using TMPro;
using UnityEngine;

public class ExperimentCounter : MonoBehaviour
{
    public int count = 0;
    public int MaxCount = 10;
    public int TaskNum = 1;

    public float coolDown = 1f;

    public AcceStimulate acce;
    public Collider SphereCollider;

    public TextMeshProUGUI DisplayNum;
    public TextMeshProUGUI TaskPrompt;
    public TextMeshProUGUI GesturePrompt;
    StartBallBehavior2 startBallBehavior;
    private string CurrentTask_trigger = "Do not trigger";
    private string CurrentTask_DoNotTrigger = "Do not trigger";

    private string NormalGesturePrompt = "  ";
    private string BackGesturePrompt = "\"Withdraw\"";
    private string CrossingGesturePrompt = "Move Away your hand";
    public Material shallowBall;
    public Material oriBall;
    private bool isCoolingDown = false; // Add a flag to check if the coroutine is running

    // Start is called before the first frame update
    void Start()
    {
        DisplayNum.text = 10.ToString();
        SphereCollider = gameObject.GetComponent<Collider>();
        acce = GetComponent<AcceStimulate>();
        acce.InEvent.AddListener(TurnShallow);
        acce.OutEvent.AddListener(UpdateCount);
        acce.OutEvent.AddListener(TurnShallowBack);
        startBallBehavior = GetComponentInChildren<StartBallBehavior2>();
        TaskNum = 0;
        SwitchTask();
    }

    // Update is called once per frame
    void Update()
    {
        if (count == MaxCount && !isCoolingDown) // Check if the coroutine is not running
        {
            count = 0;
            StartCoroutine(CoolDown());
            SphereCollider.enabled = true;
        }
    }

    public void UpdateCount()
    {
        if (startBallBehavior.GetState())
        {
            count += 1;
            DisplayNum.text = (10 - count).ToString();
        }
            
    }

    public void LockTriggerOnCrossing()
    {
        if(TaskNum == 3)
        {
            acce.OutEvent.AddListener(()=>
            {
            });
        }
    }

    public void SwitchTask()
    {
        TaskNum += 1;

        switch (TaskNum)
        {
            case 1:
                TaskPrompt.color = Color.red;
                TaskPrompt.text = CurrentTask_trigger;
                GesturePrompt.color = Color.red;
                GesturePrompt.text = "\"Sweep\"";
                break;
            case 2:
                TaskPrompt.color = Color.red;
                TaskPrompt.text = CurrentTask_DoNotTrigger;
                GesturePrompt.color = Color.red;
                GesturePrompt.text = BackGesturePrompt;
                DisplayNum.text = 10.ToString();
                break;
            case 3:
                acce.enabled = false;
                GameObject.Find("Task").SetActive(false);
                TaskPrompt.color = Color.green;
                TaskPrompt.text = "Thanks";
                GesturePrompt.text = "";
                break;
        }
    }

    private IEnumerator CoolDown()
    {
        isCoolingDown = true; // Set the flag to true when the coroutine starts

        if (TaskNum <= 3)
        {
            SwitchTask();
        }
        else
        {
            TaskNum = 1;
        }
        SphereCollider.enabled = false;

        yield return new WaitForSeconds(coolDown);
        DisplayNum.text = (10-count).ToString();

        isCoolingDown = false; // Set the flag to false when the coroutine finishes
    }
    void TurnShallow()
    {
        gameObject.GetComponent<MeshRenderer>().material = shallowBall;
    }
    void TurnShallowBack()
    {
        gameObject.GetComponent<MeshRenderer>().material = oriBall;
        startBallBehavior.TureUnstarted();
    }
}

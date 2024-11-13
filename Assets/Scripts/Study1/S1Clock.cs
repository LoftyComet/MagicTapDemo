
using System.Collections;

using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class S1Clock: MonoBehaviour
{
    public AcceStimulate acce;
    public Bounds BallBound;

    [Header("This script is to control the behaviour of the counter and the clock")]
    public Transform clockTransform;
    public TextMeshProUGUI ClockText, PerformedActionText, PerformedActionNum, HintText;
    public Material CapsuleMaterial;
    //public Vector4 DisplayBubbleActiveColor = new Vector4(0.8f, 0.8f, 0.8f, 1f);
    public Vector4 DisplayBubbleActiveColor = new Color(1f, 1f, 0f);
    public Vector4 DisplayBubbleInactiveColor = new Vector4(0.3f, 0.3f, 0.3f, 1f);

    [Space]
    public string KeepStill = "Please Keep Still";
    public string MoveAway = "Please Move Away";
    public string insertion = "Put finger in green ball";
    public string warning = "Please Move back";

    [Space]
    public bool PrematureExit = false;

    public UnityEvent CountCompleteEvent;

    [Space]
    public int Count = 0;
    public float CountdownTime = 3.0f;
    public float CT = 3.0f;
    public int expTime = 10; 
    // Start is called before the first frame update
    void Start()
    {
        acce = GetComponentInParent<AcceStimulate>();
        BallBound = GetComponentInParent<Collider>().bounds;
        CapsuleMaterial = clockTransform.Find("Capsule").GetComponent<Renderer>().material;
        //StartCoroutine(CountdownCoroutine());
        CountCompleteEvent ??= new UnityEvent();
        //CountCompleteEvent.AddListener(CountUpdate);
        setBubbleColor(DisplayBubbleInactiveColor);
        HintText.text = "Put finger in green ball";
        PerformedActionText.text = "Remaining times:";
        PerformedActionNum.text = expTime.ToString();
        ClockText.text = CT.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CountUpdate()
    {
        if(Count < expTime)
        {
            Count += 1;
            PerformedActionNum.text = (expTime-Count).ToString();
        }
        else
        {
            Count = 0;
            //PerformedActionNum.text = (5 - Count).ToString();
            acce.enabled = false;
        }
    }

    public void setTextColor(Vector4 ColorVector)
    {
        ClockText.color = ColorVector;
    }

    public void setBubbleColor(Vector4 ColorVector)
    {
        CapsuleMaterial.SetVector("_EmissionColor", ColorVector);
    }

    public void StartCountdown()
    {
        StartCoroutine("CountdownCoroutine");
    }

    public void StopCountDown()
    {
        StopCoroutine("CountdownCoroutine");
    }

    public IEnumerator CountdownCoroutine()
    {
        ResetStates();

        float timer = CT;
        setBubbleColor(DisplayBubbleActiveColor);
        ClockText.enabled = true;
        while (timer > 0 && PrematureExit == false)
        {
            CountdownTime = timer;
            HintText.text = KeepStill;
            // update the text with the current timer value
            ClockText.text = timer.ToString("F2") + "s";
            timer -= Time.deltaTime;

            // wait for one frame
            yield return null;

            // decrement the timer by delta time
        }
        if (timer < 0.1f)
        {
            StopCoroutine("CountdownCoroutine");

            OnSucceed();
        }

        CountCompleteEvent.Invoke();
    }

    public void OnSucceed()
    {
        if (PrematureExit == false)
        {
            setBubbleColor(Color.green);
            setTextColor(Color.green);
            ClockText.text = "0.00";
            HintText.text = MoveAway;
        }
    }

    public void Warning()
    {
        HintText.text = warning;
        setBubbleColor(Color.red);
        ClockText.color = Color.red;
        setTextColor(Color.red);
    }
    public void TapBlueBall(int expTimes = 0)
    {
        HintText.text = "Tap the blue ball";
        if (expTimes == 10) {
            HintText.text = "Thanks";
        }
        print(expTimes);
        //setBubbleColor(Color.red);
        //ClockText.color = Color.red;
        //setTextColor(Color.red);
    }
    public void Over()
    {
        HintText.text = "Thanks";
        ClockText.enabled = false;
        print("qqq");
    }

    public void ResetStates()
    {
        setBubbleColor(DisplayBubbleInactiveColor);
        float timer = CT;
        ClockText.text = timer.ToString("F2");
        setBubbleColor(Color.grey);
        setTextColor(Color.white);
        HintText.text = insertion;
        ClockText.enabled = false;
    }
}

using UnityEngine;

public class test : MonoBehaviour
{
    public int temp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Test()
    {
        
    }


private void Awake()
    {
        Debug.Log("Awake函数调用");
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable函数调用");
    }


    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate函数调用");
    }

    private void LateUpdate()
    {
        Debug.Log("LateUpdate函数调用");
    }

    private void OnGUI()
    {
        Debug.Log("OnGUI函数调用");
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable函数调用");
    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy函数调用");
    }
}

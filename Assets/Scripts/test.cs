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
        Debug.Log("Awake��������");
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable��������");
    }


    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate��������");
    }

    private void LateUpdate()
    {
        Debug.Log("LateUpdate��������");
    }

    private void OnGUI()
    {
        Debug.Log("OnGUI��������");
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable��������");
    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy��������");
    }
}

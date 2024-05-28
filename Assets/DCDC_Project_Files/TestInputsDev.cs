using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestInputsDev : MonoBehaviour
{

    public UnityEvent startRoom;
    public UnityEvent instantiateNetworkedObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0)) 
        {
            startRoom?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            instantiateNetworkedObject?.Invoke();
        }
    }
}

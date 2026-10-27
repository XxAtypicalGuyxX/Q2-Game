using UnityEngine;
using UnityEngine.InputSystem;

public class Reset : MonoBehaviour
{

    Vector3 startPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            //do the thing
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            
            transform.position = startPosition;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}

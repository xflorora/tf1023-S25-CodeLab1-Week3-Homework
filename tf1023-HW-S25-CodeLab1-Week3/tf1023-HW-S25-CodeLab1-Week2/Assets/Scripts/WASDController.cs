using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class WASDController : MonoBehaviour
{
    //defining variables so that they can be re-mapped to arrow keys, etc.
    public KeyCode keyUp = KeyCode.W;
    public KeyCode keyDown = KeyCode.S;
    public KeyCode keyLeft = KeyCode.A;
    public KeyCode keyRight = KeyCode.D;

    //defining rigidbody for physics and moveForce for speed
    public Rigidbody rb;
    public float moveForce = 8f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        //if there is input for keyUp (key is pressed), add force in the direction of "up"
        if (Input.GetKey(keyUp))
        {
            rb.AddForce(Vector3.forward * moveForce);
            
        }

        if (Input.GetKey(keyDown))
        {
            rb.AddForce(Vector3.back * moveForce);
        }

        if (Input.GetKey(keyLeft))
        {
            rb.AddForce(Vector3.left * moveForce);
        }

        if (Input.GetKey(keyRight))
        {
            rb.AddForce(Vector3.right * moveForce);
        }
    }
}

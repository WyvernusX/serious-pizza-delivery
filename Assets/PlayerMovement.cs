using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;  
    void Start()
    {
      rb = GetComponent<Rigidbody>();
      rb.linearDamping = 0.2f;
    }

    void Update() { 
      //nothing 
    }

    void FixedUpdate() {
      if (Input.GetKey(KeyCode.W)) {
        moveForward();  
      }
    }

    void moveForward() {
      rb.AddForce(gameObject.transform.forward * 1000f * Time.deltaTime, ForceMode.Force);
    } 
}

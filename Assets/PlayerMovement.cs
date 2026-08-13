using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 
    [Header("Object References")]
    public Camera camera;

    [Header("Drag Rate")]
    public float dragAmount;
   
    [Header("Key Binds")]
    public KeyCode sprintBind;

    Rigidbody rb;
    bool sprintBonusActive = false;

    void Start()
    {
      rb = GetComponent<Rigidbody>(); 
    }

    void Update() { 
      //nothing 
    }

    void FixedUpdate() {
      
      Damping(dragAmount);
      manageBonuses(); 
    }

    void manageBonuses() {
      if (!sprintBonusActive && Input.GetKey(sprintBind)) {
        runSpeedBonus();
      }
    }

    void manageMovement() {
      if (Input.GetKey(KeyCode.W)) {
        moveForward();  
      }
    }

    void runSpeedBonus() {
      rb.linearVelocity = new Vector3(rb.linearVelocity.x * 1.3,
          rb.linearVelocity.y,
          rb.linearVelocity.z * 1.3);
    }

    void moveForward() {
      rb.AddForce(gameObject.transform.forward * 10f, ForceMode.Force);
    }

    void moveLeft() {

    }

    void moveRight() {

    }

    void moveBack() {

    }

    void doJump() {

    }

    void doSlide() {

    }

    void doCrouch() {

    }

    void doSlide() {

    }

    void doDash() {

    }

    void startWallRun() {

    }

    void endWallRun() {

    }

    void Damping(float dampRate) { 
      rb.linearVelocity = new Vector3(rb.linearVelocity.x - dampRate * Time.deltaTime, 
          rb.linearVelocity.y - dampRate * Time.deltaTime, 
          rb.linearVelocity.z - dampRate * Time.deltaTime);
    }
}

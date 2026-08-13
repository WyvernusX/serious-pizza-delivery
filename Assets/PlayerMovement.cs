using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 
    /*[Header("Object References")]
    public Camera camera;*/

    [Header("Rates")]
    public float sprintBonusAmount;
    public float walkPower;
    public float jumpPower; 
    public float crouchPower;
    public float slidePower; 
    public float dashPower; 
    public float dragAmount;
   
    [Header("Key Binds")]
    public KeyCode sprintBind;
    public KeyCode dashBind;
    public KeyCode crouchBind;


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
      manageMovement(); 
      Damping(dragAmount); 
      Debug.Log(rb.linearVelocity.magnitude); 
    } 

    void manageMovement() { //TODO: add keybind customisability in headers 
      if (Input.GetKey(KeyCode.W)) {
        moveForward();  
      } else if (Input.GetKey(KeyCode.A)) {
        moveLeft();
      } else if (Input.GetKey(KeyCode.S)) {
        moveBack();
      } else if (Input.GetKey(KeyCode.D)) {
        moveRight(); 
      } else if (Input.GetKeyDown(KeyCode.Space)) {
        doJump();
      } else if (Input.GetKeyDown(dashBind)) {
        doDash();
      } else if (Input.GetKey(crouchBind)) {
        checkCrouchOrSlide();
      } else if (Input.GetKey(sprintBind)) {
        manageSprintMovement();
      }
    }

    void manageSprintMovement() {
      if (!sprintBonusActive && Input.GetKey(sprintBind)) { 
        runSpeedBonus(); 
      } else if (sprintBonusActive && !Input.GetKey(sprintBind) ) {
        sprintBonusActive = false;
      } 
    }

    void checkCrouchOrSlide() {
      //if (rb.linearVelocity.magnitude >)
    }

    void runSpeedBonus() {
      sprintBonusActive = true; 
      rb.linearVelocity = new Vector3(rb.linearVelocity.x * sprintBonusAmount,
          rb.linearVelocity.y,
          rb.linearVelocity.z * sprintBonusAmount);
    }

    void moveForward() {
      rb.AddForce(gameObject.transform.forward * walkPower, ForceMode.Force);
    }

    void moveLeft() {
      rb.AddForce(-gameObject.transform.right * walkPower, ForceMode.Force);
    }

    void moveRight() {
      rb.AddForce(gameObject.transform.right * walkPower, ForceMode.Force);
    }

    void moveBack() {
      rb.AddForce(-gameObject.transform.forward * walkPower, ForceMode.Force);
    }

    void doJump() {
      rb.AddForce(gameObject.transform.up * jumpPower, ForceMode.Impulse); 
    }

    void doSlide() {
      rb.AddForce(gameObject.transform.forward * slidePower, ForceMode.Force); 
    }

    void doCrouch() {
      rb.AddForce(gameObject.transform.forward * crouchPower, ForceMode.Force);
    } 

    void doDash() {
      rb.AddForce(gameObject.transform.forward * dashPower, ForceMode.Impulse);
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

using UnityEngine;

public class PlayerMovement : MonoBehaviour { 
    /*[Header("Object References")]
    public Camera camera;*/

    [Header("Cooldowns")]
    public float dashCooldown = .5f;
    public float slideCooldown = 1f;
    public float jumpCooldown = .25f;

    [Header("Rates / Bonuses")]
    public float sprintBonusAmount;
    public float walkPower;
    public float jumpPower; 
    public float crouchPower;
    public float slidePower; 
    public float dashPower; 
    public float dragAmount;
    public float wallRunBonusAmount; 

    [Header("Key Binds")]
    public KeyCode sprintBind;
    public KeyCode dashBind;
    public KeyCode crouchBind;
    public KeyCode jumpBind;

    Vector3 moveDir; 

    Rigidbody rb;
    
    bool sprintBonusActive = false; 
    public  bool isWallRunning = false;
    bool isJumping = false;
    public bool onGround = false;
    bool isDashing = false; 
    bool canJump = true;

    void Start()
    {
      rb = GetComponent<Rigidbody>();   
    }

    void Update() {  
      manageSpecialMovement();  
      Damping(dragAmount); 
    }

    void FixedUpdate() {
      manageMovement();  
      Debug.Log(rb.linearVelocity.magnitude); 
    } 

    void manageMovement() { 
      if (Input.GetKey(KeyCode.W)) { 
        moveForward();  
      } 
      if (Input.GetKey(KeyCode.A)) {
        moveLeft();
      } 
      if (Input.GetKey(KeyCode.S)) {
        moveBack();
      } 
      if (Input.GetKey(KeyCode.D)) {
        moveRight(); 
      } 
    }

    void manageSpecialMovement() {
      if (Input.GetKeyDown(jumpBind) && !isJumping && canJump) { 
        doJump();
        Invoke("resetJump", jumpCooldown); 
      } else if (Input.GetKeyDown(dashBind) && !isDashing) {
        doDash();
        Invoke("resetDash", dashCooldown); 
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
      if (rb.linearVelocity.magnitude >= 6.5f) { 
        Invoke("doSlide", slideCooldown); 
      } else { 
        doCrouch(); 
      }
    }

    void runSpeedBonus() {
      sprintBonusActive = true; 
      rb.linearVelocity = new Vector3(rb.linearVelocity.x * sprintBonusAmount,
          rb.linearVelocity.y,
          rb.linearVelocity.z * sprintBonusAmount);
    }

    void moveForward() {
      rb.AddForce(transform.forward * walkPower, ForceMode.Force);
    }

    void moveLeft() {
      rb.AddForce(-transform.right * walkPower, ForceMode.Force);
    }

    void moveRight() {
      rb.AddForce(transform.right * walkPower, ForceMode.Force);
    }

    void moveBack() {
      rb.AddForce(-transform.forward * walkPower, ForceMode.Force);
    }

    void doJump() { 
      isJumping = true; 
      canJump = false; 
      rb.AddForce(transform.up * jumpPower, ForceMode.Impulse); 
    }

    void doSlide() {  
      rb.AddForce(transform.forward * slidePower, ForceMode.Force);  
    }

    void doCrouch() {
      rb.AddForce(transform.forward * crouchPower, ForceMode.Force);
    } 

    void doDash() {
      isDashing = true; 
      rb.AddForce(gameObject.transform.forward * dashPower, ForceMode.Impulse);
    }

    void resetDash() {
      isDashing = false;
    }

    void resetJump() {
      isJumping = false;
      canJump = true; 
    }

    public void startWallRun() {
      rb.useGravity = false; 
      isWallRunning = true; 
      rb.AddForce(gameObject.transform.forward * wallRunBonusAmount, ForceMode.Force);
    }

    public void endWallRun() {
      rb.useGravity = true;
      isWallRunning = false; 
      rb.AddForce(gameObject.transform.forward * (dashPower / 2), ForceMode.Impulse);
      rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y + 2f, rb.linearVelocity.z); 
    } 

    void Damping(float dampRate) { 
      rb.linearVelocity = new Vector3(rb.linearVelocity.x - dampRate * Time.deltaTime, 
          rb.linearVelocity.y - dampRate * Time.deltaTime, 
          rb.linearVelocity.z - dampRate * Time.deltaTime);
    }
}

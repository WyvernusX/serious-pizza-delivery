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
    public bool onGround = true;
    bool isDashing = false;  
    public bool canJump = true;

    void Start()
    {
      rb = GetComponent<Rigidbody>();   
    }

    void Update() {  
      manageSpecialMovement();  
      Damping(dragAmount); 
      applySpeedLimit(); 
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
      if (Input.GetKey(jumpBind) && (!isJumping || canJump)  && isWallRunning) {
        doWallJump(); 
        Invoke("resetWallJump", jumpCooldown); 
      } else if (Input.GetKey(jumpBind) && !isJumping && canJump && onGround) { 
        doJump();
        Invoke("resetJump", jumpCooldown); 
      }
    }

    void manageSpecialMovement() {  
      if (Input.GetKeyDown(dashBind) && !isDashing) {
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

    void doWallJump() {
      isJumping = true; 
      rb.AddForce(gameObject.transform.forward * (dashPower / 10), ForceMode.Impulse); 
      rb.AddForce(gameObject.transform.up * (jumpPower / 10), ForceMode.Impulse); 
      //rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y + 2f, rb.linearVelocity.z);  
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

    void resetWallJump() {
      isJumping = false;
    }

    public void startWallRun() {
      rb.useGravity = false; 
      isWallRunning = true;
      resetJump(); 
      rb.AddForce(gameObject.transform.forward * wallRunBonusAmount, ForceMode.Force);
    }

    public void endWallRun() {
      rb.useGravity = true;
      isWallRunning = false; 
      if (isJumping) {
        doWallJump();
      }
      /*if (!isJumping) {
        rb.AddForce(gameObject.transform.forward * (dashPower / 2), ForceMode.Impulse); 
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y + 5f, rb.linearVelocity.z);
      }*/ 
    }

    void idleWallRun() {
      rb.useGravity = true; 
      isWallRunning = false;
    }

     bool exceedingLimit() {
      if (sprintBonusActive && rb.linearVelocity.magnitude > 20f) {
        return true;
      } else if (!sprintBonusActive && rb.linearVelocity.magnitude > 10f) {
        return true;
      }
      return false;
    }

    void applySpeedLimit() {
      if (exceedingLimit() && sprintBonusActive) { 
        float diagonalMoveSpeed = Mathf.Sqrt(20*20);
        Vector3 forward = transform.forward * diagonalMoveSpeed;
        Vector3 right = transform.right * diagonalMoveSpeed; 
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, 20); 
      } else if (exceedingLimit() && sprintBonusActive) {
        float diagonalMoveSpeed = Mathf.Sqrt(10*10);
        Vector3 forward = transform.forward * diagonalMoveSpeed;
        Vector3 right = transform.right * diagonalMoveSpeed; 
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, 10);
      } 
    }

    void Damping(float dampRate) { 
      if (!isJumping) {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x - (rb.linearVelocity.magnitude / 2) * Time.deltaTime, 
          rb.linearVelocity.y - (rb.linearVelocity.magnitude / 2) * Time.deltaTime, 
          rb.linearVelocity.z - (rb.linearVelocity.magnitude / 2) * Time.deltaTime);
      } 
    }
}

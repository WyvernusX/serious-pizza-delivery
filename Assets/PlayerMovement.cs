using UnityEngine;

public class PlayerMovement : MonoBehaviour { 
    
    [Header("Object References")]
    public GameObject collision; 

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

    public Rigidbody rb;
    
    public Vector3 wallRunJumpDirection; 
    public Vector3 wallRunDashDirection; 
    bool sprintBonusActive = false; 
    public  bool isWallRunning = false;
    bool isJumping = false;
    public bool onGround = true;
    bool isDashing = false;  
    public bool canJump = true;
    bool isSliding = false;
    public Vector3 startPos = new Vector3(0, 0, 0); 
    
    void Start()
    {
      rb = GetComponent<Rigidbody>();   
      startPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z); 
    }

    void Update() {  
      if (!isWallRunning) {
        manageSpecialMovement();
      }   
    }

    void FixedUpdate() {  
      manageMovement();   
      //Debug.Log(rb.linearVelocity.magnitude); 
      /*if (isWallRunning && rb.linearVelocity.magnitude < 1.5f) {
        antiWallRunIdle();
      }*/ 
      Damping(dragAmount); 
      applySpeedLimit();  
    } 

    void manageMovement() { 
      if (Input.GetKey(KeyCode.W) && !isWallRunning) { 
        moveForward();  
      } 
      if (Input.GetKey(KeyCode.A) && !isWallRunning) {
        moveLeft();
      } 
      if (Input.GetKey(KeyCode.S) && !isWallRunning) {
        moveBack();
      } 
      if (Input.GetKey(KeyCode.D) && !isWallRunning) {
        moveRight(); 
      }
      if (Input.GetKey(jumpBind) && (!isJumping || canJump) && isWallRunning) { 
        doWallJump(wallRunJumpDirection); 
        //Invoke("resetWallJump", jumpCooldown); 
      } else if (Input.GetKey(jumpBind) && !isJumping && canJump && onGround && !isWallRunning) {  
        doJump();
        Invoke("resetJump", jumpCooldown); 
      }
    }

    void manageSpecialMovement() {  
      if (Input.GetKeyDown(dashBind) && !isDashing) {
        doDash();
        Invoke("resetDash", dashCooldown); 
      } else if (Input.GetKey(crouchBind)) {
        doCrouch(); 
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
      if (rb.linearVelocity.magnitude >= 6.5f && !isSliding) { 
        doSlide(); 
        Invoke("resetSlide", slideCooldown); 
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

    void doWallJump(Vector3 jumpDirection) {
      isJumping = true; 
      rb.AddForce(jumpDirection * (dashPower * 1.3f), ForceMode.Impulse);   
    }

    void doSlide() { //TODO: FIX SLIDEE 
      isSliding = true; 
      rb.AddForce(gameObject.transform.forward * slidePower, ForceMode.Impulse);  
    }

    void doCrouch() {
      Damping(crouchPower); 
    } 

    void doDash() {
      isDashing = true; 
      rb.AddForce(gameObject.transform.forward * dashPower, ForceMode.Impulse);
    }

    void doWallDash() {
      rb.AddForce(gameObject.transform.forward * dashPower, ForceMode.Impulse);  
    }

    void resetDash() {
      isDashing = false;
    }

    void resetJump() {
      isJumping = false; 
      canJump = true; 
    }

    void resetSlide() {
      isSliding = false;
    }

    void resetWallJump() {
      isJumping = false;
    }

    void killInitialWallRunVelocity() {
      rb.linearVelocity = new Vector3(0f, 0f, 0f);
    } 

    void addWallRunInitialVelocity() {
      if (rb.linearVelocity.magnitude <= 2f) {
        doDash();
        resetDash();
      }
    }

    public void startWallRun() {  
      killInitialWallRunVelocity(); 
      addWallRunInitialVelocity(); 
      resetJump();   
      Debug.Log("wallrun started");  
      rb.useGravity = false; 
      isWallRunning = true;   
      doWallDash(); 
      rb.AddForce(gameObject.transform.forward * wallRunBonusAmount, ForceMode.Force); 
    }

    public void endWallRun() { 
      resetJump(); 
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

    void antiWallRunIdle() {
      isWallRunning = false;
      resetJump();
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
      rb.AddForce(-rb.linearVelocity.normalized * (rb.linearVelocity.magnitude * 0.9f), ForceMode.Force); 
    }
}

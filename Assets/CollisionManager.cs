using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public PlayerMovement player; 

    void OnCollisionEnter(Collision other) {
      
      Vector3 jumpDir = transform.position - other.contacts[0].point;
      exportTo(jumpDir); 

      player.canJump = true; 
      if (other.gameObject.CompareTag("FinishObj")) {
        Debug.Log("you won!"); 
      } else if (other.gameObject.CompareTag("CanWallRun")) {
        player.startWallRun();
      } else if (other.gameObject.CompareTag("Ground")) {
        player.onGround = true;
      } 
    }

    void OnCollisionExit(Collision other) {
      if (player.isWallRunning) {
        player.endWallRun();
        player.rb.useGravity = true; 
      } 
      if (other.gameObject.CompareTag("Ground")) {
        player.onGround = false;
      }
    }

    public void exportTo(Vector3 val) {
      player.wallRunJumpDirection = val; 
    }

    void Start() {
        
    }
 
    void Update() {
        
    }
}

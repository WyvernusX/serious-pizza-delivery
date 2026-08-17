using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public PlayerMovement player; 

    void OnCollisionEnter(Collision other) {
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
      } 
      if (other.gameObject.CompareTag("Ground")) {
        player.onGround = false;
      }
    }

    void Start() {
        
    }
 
    void Update() {
        
    }
}

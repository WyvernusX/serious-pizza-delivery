using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public PlayerMovement player; 

    void OnCollisionEnter(Collision other) {
      if (other.gameObject.CompareTag("CanWallRun")) {
        player.startWallRun();
      } else if (other.gameObject.CompareTag("Ground")) {
        player.onGround = true;
      } else {
        player.onGround = false;
      }
    }

    void OnCollisionExit(Collision other) {
      if (player.isWallRunning) {
        player.endWallRun();
      }
    }

    void Start() {
        
    }
 
    void Update() {
        
    }
}

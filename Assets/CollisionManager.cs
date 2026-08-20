using UnityEngine;
using UnityEngine.SceneManagement; 
using TMPro;

public class CollisionManager : MonoBehaviour
{
    public PlayerMovement player; 
    public GameObject panel; 
    public int deathAmount;  
    //public ScoreManager score;

    void OnCollisionEnter(Collision other) {
      
      Vector3 jumpDir = transform.position - other.contacts[0].point;
      exportToWallRunJump(jumpDir); 

      Vector3 jumpDir2 = other.contacts[0].point;
      exportToWallRunDash(jumpDir2);

      player.canJump = true;

      Scene currentScene = SceneManager.GetActiveScene(); 
      
      if (other.gameObject.CompareTag("Death") && !panel.activeInHierarchy) {
        player.gameObject.transform.position = player.startPos;  
        deathAmount++; 
        Debug.Log("DEATH");  
      } else if (other.gameObject.CompareTag("Ground") && currentScene.name == "LevelTwo") {
        Debug.Log("refreshing"); 
        player.resetDash();
        player.resetJump(); 
        player.onGround = true; 
      } else if (other.gameObject.CompareTag("Ground")) {
        player.onGround = true; 
      } else if (other.gameObject.CompareTag("FinishObj")) {
        panel.SetActive(true); 
        Cursor.lockState = CursorLockMode.None;  
      } else if (other.gameObject.CompareTag("CanWallRun")) {  
        player.startWallRun(); 
      }  
    }

    void OnCollisionExit(Collision other) {
      if (player.isWallRunning && other.gameObject.CompareTag("CanWallRun")) { 
        player.endWallRun();
        player.rb.useGravity = true;  
      } 
      if (other.gameObject.CompareTag("Ground")) {
        player.onGround = false;
      }
    }

    public void exportToWallRunJump(Vector3 val) {
      player.wallRunJumpDirection = val; 
    }

    public void exportToWallRunDash(Vector3 val) {
      player.wallRunDashDirection = val;
    }

    void Start() {
        
    }
 
    void Update() {
        
    }
}

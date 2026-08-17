using UnityEngine;

public class Raycast : MonoBehaviour
{ 
    void Start()
    {
        
    }
 
    void Update()
    {
        
    } 

  public bool checkWallRun() {
    RaycastHit hitObj;
    bool lookingAtWall = Physics.Raycast(gameObject.transform.position, 
        gameObject.transform.forward, 
        out hitObj, 
        5f); 
    if (lookingAtWall && hitObj.collider.gameObject.CompareTag("CanWallRun")) {
      return true;
    } else {
      return false;
    }
  }
}

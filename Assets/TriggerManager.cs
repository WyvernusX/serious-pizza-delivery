using UnityEngine;

public class TriggerManager : MonoBehaviour { 
    
    void Start() {

    }
 
    void Update() {
        
    }

    void OnTriggerEnter(Collider other) {
      if (other.gameObject.CompareTag("Pizza")) {
        Debug.Log("touched the pizza"); 
      }
    }
}

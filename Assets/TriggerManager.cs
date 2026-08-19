using UnityEngine;

public class TriggerManager : MonoBehaviour { 
   
    public int pizza = 0; 

    void Start() {

    }
 
    void Update() {
        
    }

    void OnTriggerEnter(Collider other) {
      if (other.gameObject.CompareTag("Pizza")) {
        Debug.Log("touched the pizza"); 
        pizza++;
        other.gameObject.SetActive(false); 
      }
    }
}

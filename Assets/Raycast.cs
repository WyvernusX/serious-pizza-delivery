using UnityEngine;

public class Raycast : MonoBehaviour
{ 
    void Start()
    {
        
    }
 
    void Update()
    {
        
    }

    void FixedUpdate() {
      RaycastHit hitObject;
      bool lookingAtCylinder = Physics.Raycast(gameObject.transform.position, 
          gameObject.transform.forward,
          out hitObject,
          150f);
      if (lookingAtCylinder) {
        Debug.Log(hitObject.collider.gameObject.name);
      } 
    }
}

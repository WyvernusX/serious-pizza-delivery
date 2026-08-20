using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    
    [Header("Object References")]
    public GameObject rotatedObj;
    public GameObject moveObj;

    void Start() {
        
    }
 
    void Update() {
      rotatedObj.transform.Rotate(Vector3.up * 65f * Time.deltaTime);
    }
}

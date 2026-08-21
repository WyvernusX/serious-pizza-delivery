using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    
    [Header("Object References")]
    public GameObject rotatedObj;
    public GameObject moveObj;
    float max = -4.98f;
    float min = 5.69f;
    float time = 0.0f;
    /*Vector3 objectStartPosition = new Vector3();

    void Start() {
      objectStartPosition = moveObj.gameObject.transform.position;  
    }

    void moveToPos(GameObject movingObject, Vector3 pos1, Vector3 pos2) {
      if (movingObject.transform.position == pos1) {
        Vector3.MoveTowards(objectStartPosition, new Vector3(4.9f, 1.44f, -47.66f), 5f);
      } else if (movingObject.transform.position == pos2) {
        Vector3.MoveTowards(new Vector3(4.9f, 1.44f, -47.66f), objectStartPosition, 5f);
      } 
    }*/

    void Update() {
      rotatedObj.transform.Rotate(Vector3.up * 65f * Time.deltaTime);  
      moveObj.transform.position = new Vector3(Mathf.Lerp(min, max, time), 1.44f, -47.66f); 
      time += 0.5f * Time.deltaTime; 
      
      if (time > 1.0f) {
        float temp = max;
        max = min;
        min = temp;
        time = 0.0f;
      }

      //moveToPos(moveObj, objectStartPosition, new Vector3(4.9f, 1.44f, -47.66f)); 
    
      /*float xPos = Mathf.PingPong(Time.deltaTime * 2f, 5f * 2) - 5f;
      transform.position = new Vector3(xPos, transform.position.y, transform.position.z);*/ 
    }
}

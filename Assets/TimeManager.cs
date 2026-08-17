using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static float time = 0;
    void Start()
    {
      
    }
 
    void Update()
    {
      time += Time.deltaTime; 
      //Debug.Log($"{time}"); 
    }

    void FixedUpdate() {

    }
}

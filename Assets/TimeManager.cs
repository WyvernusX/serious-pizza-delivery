using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static float time = 0;
    void Start()
    {
      
    }
 
    void Update() { 
 
    }

    void FixedUpdate() {

    }

    void startTimer() {
      time += Time.deltaTime;
    }

    void endTimer() {

    }

    void displayTime() {
      Debug.Log($"{time}");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour
{
    public float time = 0;
    public bool timeEnded = false; 

    void Start()
    {
      
    }
 
    void Update() { 
      if (!timeEnded) {
        startTimer(); 
        checkTimeMax();
      }  
    }

    void FixedUpdate() {

    }

    void startTimer() {
      time += Time.deltaTime;
    }

    public void endTimer() {
      timeEnded = true;  
    }

    void displayTime() {
      Debug.Log($"{time}");
    }

    void checkTimeMax() {
      if (time >= 300) {
        endTimer(); 
        Transfer(); 
      }
    }

    void Transfer() {
      SceneManager.LoadScene("LevelFinish");      
    }
}

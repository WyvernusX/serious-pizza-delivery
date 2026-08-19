using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour
{
    public float time = 0;
    void Start()
    {
      
    }
 
    void Update() { 
      startTimer(); 
      checkTimeMax(); 
    }

    void FixedUpdate() {

    }

    void startTimer() {
      time += Time.deltaTime;
    }

    void endTimer() {
      time = 0;
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

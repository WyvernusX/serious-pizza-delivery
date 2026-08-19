using UnityEngine;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{     
    public TriggerManager pizza; 
    public TimeManager timeScript;

    public int score = 0;
    public string rating = "Z"; 

    void Start() {
        
    }
 
    void Update() {
        
    }

    int calculateScore() {
      return (int)(300 - timeScript.time) * pizza.pizza + 100;  
    }

    string calculateRating(int score) {
      if (score >= 700) {
        return "S"; 
      } else if (score >= 600) {
        return "A";
      } else if (score >= 480) {
        return "B";
      } else if (score >= 300) {
        return "C";
      } else if (score >= 200) {
        return "D";
      } else {
        return "F";
      }
    }
}

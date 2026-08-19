using UnityEngine;
using System.Collections.Generic;
using TMPro; 

public class ScoreManager : MonoBehaviour
{     
    public TriggerManager pizza; 
    public TimeManager timeScript;
    public TextManager textScript;

    public static int score = 0;
    public static string rating = "Z"; 

    void Start() {
        
    }
 
    void Update() {
        
    }

    public static int calculateScore() { 
      return (int)(300 - timeScript.time) * pizza.pizza + 100;  
    }

    public static string calculateRating(int scoreArg) {
      Debug.Log("bye"); 
      if (scoreArg >= 700) {
        return "S"; 
      } else if (scoreArg >= 600) {
        return "A";
      } else if (scoreArg >= 480) {
        return "B";
      } else if (scoreArg >= 300) {
        return "C";
      } else if (scoreArg >= 200) {
        return "D";
      } else {
        return "F";
      }
    }
}

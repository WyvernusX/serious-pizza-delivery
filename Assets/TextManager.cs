using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour {
   
    [Header("Object References")] 
    public TMP_Text timeText;
    public TMP_Text movementText;
    public TMP_Text pizzaText;
    public PlayerMovement movement;
    public TimeManager timeObj;
    public TriggerManager pizza;  
    public TMP_Text ratingOutput;
    public TMP_Text scoreOutput;
    public GameObject panel; 

    void Start() {
        
    }
 
    void Update() {
      timeText.SetText($"{timeObj.time:F2}s"); 
      movementText.SetText($"{movement.rb.linearVelocity.magnitude:F2} u/s"); 
      pizzaText.SetText($"{pizza.pizza} / 3 pizza"); 
      if (panel.activeInHierarchy) {
        displayScore(scoreOutput);     
        displayRating(ratingOutput);
      }   
    }

    public int calculateScore() { 
      return (int)(300 - timeObj.time) * pizza.pizza + 100;  
    }

    public string calculateRating(int scoreArg) {
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

    void displayScore(TMP_Text scoreText) {   
      scoreText.SetText($"rating: {calculateScore()}");
    }

    void displayRating(TMP_Text rateText) {
      int scoreNum = calculateScore();
      Debug.Log(scoreNum);
      rateText.SetText($"{calculateRating(scoreNum)}");
    }
}

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

    void Start() {
        
    }
 
    void Update() {
      timeText.SetText($"{timeObj.time:F3}s / 300s"); 
      movementText.SetText($"{movement.rb.linearVelocity.magnitude:F2} u/s"); 
      pizzaText.SetText($"{pizza.pizza} / 3 pizza"); 
    }

    void displayScore(TMP_Text scoreText, int score) {
      scoreText.SetText($"ratig: {score}");
    }
}

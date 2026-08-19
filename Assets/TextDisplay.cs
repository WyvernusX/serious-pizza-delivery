using UnityEngine;
using TMPro;

public class TextDisplay : MonoBehaviour
{
    [Header("Object References")]
    public 
    public 
    public 
    public TMP_Text ratingOutput;
    public TMP_Text scoreOutput;  

    void Start()
    {
      displayScore(ratingOutput);     
      displayRating(scoreOutput);  
    }

    
    void Update()
    {
        
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

    void displayScore(TMP_Text scoreText) {   
      scoreText.SetText($"rating: {ScoreManager.calculateScore()}");
    }

    void displayRating(TMP_Text rateText) {
      int scoreNum = ScoreManager.calculateScore();
      Debug.Log(scoreNum);
      rateText.SetText($"{ScoreManager.calculateRating(5)}");
    }
}

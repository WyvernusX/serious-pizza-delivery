using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MenuButtons : MonoBehaviour
{ 
    
    Dictionary<int, string> dict = new Dictionary<int, string>() {
      [1] = "LevelOne",
      [2] = "LevelTwo",
      [3] = "LevelThree",
      [4] = "LevelFour",
      [5] = "LevelFive",
      [6] = "LevelSix",
      [7] = "LevelSeven",
      [8] = "LevelEight",
      [9] = "LevelNine",
      [10] = "LevelTen"
    };
     
    void Start() {
      //Debug.Log($"{thing:F3}");        
    }
 
    void Update() {
        
    }

    public static void QuitGame() {
      Application.Quit();
    }

    public static void switchToMain() {
      SceneManager.LoadScene("UI");
    }

    public void switchToLevelSelect() {
      SceneManager.LoadScene("LevelSelection");
    }
  
    public void switchToLevel(string level) {
      SceneManager.LoadScene(level);
    }
}

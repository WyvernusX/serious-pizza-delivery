using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuButtons : MonoBehaviour
{ 
    void Start() {
        
    }
 
    void Update() {
        
    }

    public void QuitGame() {
      Application.Quit();
    }

    public void switchToMain() {
      SceneManager.LoadScene("SampleScene");
    }
}


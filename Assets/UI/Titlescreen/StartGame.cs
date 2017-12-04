/*
 * StartGame.cs
 * Author: Samuel Vargas
 */

using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Titlescreen {

  public class StartGame : MonoBehaviour {
    private void LoadLevelOne() {
      Debug.Log("Starting The Game");
      SceneManager.LoadScene("001");
    }
  }

}
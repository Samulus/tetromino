/*
 * LevelResetter.cs
 * Author: Samuel Vargas
 */

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Util {

  public class LevelResetter : MonoBehaviour {
    private const float SecondsUntilReset = 1.5f;
    private float _timeButtonHeld;
    private const KeyCode ResetKey = KeyCode.R;

    private void Update() {
      if (_timeButtonHeld >= SecondsUntilReset) {
        int currentLevel;
        int.TryParse(SceneManager.GetActiveScene().name, out currentLevel);
        SceneManager.LoadScene(currentLevel.ToString("000"));
      }

      if (Input.GetKey(ResetKey)) {
        _timeButtonHeld += Time.deltaTime;
      }
      else {
        _timeButtonHeld = 0;
      }
    }
  }

}
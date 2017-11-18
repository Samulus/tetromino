/*
 * LevelExit.cs
 * Author: Samuel Vargas
 */

using Tags;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Devices.Exit {

  public class LevelExit : MonoBehaviour {
    private int _currentlevel;

    private void OnTriggerEnter(Collider other) {
      var maybeTag = other.gameObject.GetComponent<Tag>();
      if (maybeTag == null || maybeTag.Type != TagType.Agent || maybeTag.AgentId != AgentId.Player) return;
      _currentlevel += 1;
      SceneManager.LoadScene(_currentlevel.ToString("000"));
    }

    private void Start() {
      int.TryParse(SceneManager.GetActiveScene().name, out _currentlevel);
    }
  }

}
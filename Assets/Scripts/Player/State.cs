/*
	Player.State.cs
	Author: Samuel Vargas
*/

using UnityEngine;

namespace Player {
  public class State : MonoBehaviour {
    private static State _playerState;

    private void Start() {
      _playerState = null;
    }

    private void OnMouseDown() {
      _playerState = this;
    }

    public bool IsActive() {
      return _playerState == this;
    }
  }
}
/*
	Player.State.cs
	Author: Samuel Vargas
*/

using System.Collections.Generic;
using UnityEngine;

namespace Player {
  public class State : MonoBehaviour {
    private static State _playerState;
    private static List<State> _states;

    private void Start() {
      _playerState = null;
      if (_states == null) {
        _states = new List<State> {this};
      }
      else {
        _states.Add(this);
      }
    }

    private void OnMouseDown() {
      _playerState = this;
      foreach (var s in _states) {
      }
    }

    public bool IsActive() {
      return _playerState == this;
    }
  }
}
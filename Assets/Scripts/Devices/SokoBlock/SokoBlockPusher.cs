/*
 * SokoBlockPusher.cs
 * Author: Samuel Vargas
 *
 * Provides an interface for the Player to push a block SokoBlock.
 * Blocks can only be pushed, not pulled. They were inspired from
 * Sokoban.
 */

using UnityEngine;

namespace Devices.SokoBlock {

  public class SokoBlockPusher : MonoBehaviour {
    private bool _isBeingPushed;

    private Transform _parent;

    private void Start() {
      _parent = transform.parent;
    }
    
    private void Update() {
      if (_isBeingPushed) {
        _parent.position += transform.forward * Time.deltaTime * 0.45f;
      }
    }
    
    public void StartPushing() {
      _isBeingPushed = true;
      _parent.GetComponent<Rigidbody>().isKinematic = false;
    }

    public void StopPushing() {
      _isBeingPushed = false;
      _parent.GetComponent<Rigidbody>().isKinematic = true;
    }
  }

}
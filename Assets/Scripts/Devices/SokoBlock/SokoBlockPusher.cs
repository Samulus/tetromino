/*
 * SokoBlockPusher.cs
 * Author: Samuel Vargas
 *
 * Provides an interface for the Player to push a block SokoBlock.
 * Blocks can only be pushed. Hence their name.
 */

using MultiPurpose;
using UnityEngine;
using Util;

namespace Devices.SokoBlock {

  public class SokoBlockPusher : MonoBehaviour {
    public bool SpecificColorRequired = true;
    public GameObjectColor.Colors RequiredColor;
    private RequiredColorZone _requiredColorZone;

    private void Start() {
      if (SpecificColorRequired) {
        _requiredColorZone = GetComponent<RequiredColorZone>();
        _requiredColorZone.RequiredColor = RequiredColor;
      }
    }

    public bool Push() {
      if (SpecificColorRequired && _requiredColorZone.GameObjectPresentWithExpectedColor()) {
        transform.position += Vector3.forward * Time.deltaTime;
        return true;
      }

      if (!SpecificColorRequired) {
        transform.position += Vector3.forward * Time.deltaTime;
        return true;
      }

      return false;
    }
  }

}
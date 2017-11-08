/*
 * SokoBlockPusher.cs
 * Author: Samuel Vargas
 *
 * Provides an interface for the Player to push a block SokoBlock.
 * Blocks can only be pushed, not pulled. They were inspired from
 * Sokoban.
 */

using Tags;
using UnityEngine;
using Util;

namespace Devices.SokoBlock {

  public class SokoBlockPusher : MonoBehaviour {
    public bool SpecificColorRequired;
    public GameObjectColor.Colors RequiredColor;

    private TagPrescenceZone _positiveZ, _negativeZ, _positiveX, _negativeX;

    private void Start() {
      _positiveZ = transform.Find("PositiveZColorZone").GetComponent<TagPrescenceZone>();
      _negativeZ = transform.Find("NegativeZColorZone").GetComponent<TagPrescenceZone>();
      _positiveX = transform.Find("PositiveXColorZone").GetComponent<TagPrescenceZone>();
      _negativeX = transform.Find("NegativeXColorZone").GetComponent<TagPrescenceZone>();
      Debug.Assert(_positiveZ != null);
      Debug.Assert(_negativeZ != null);
      Debug.Assert(_positiveX != null);
      Debug.Assert(_negativeX != null);
    }

    private TagPrescenceZone DetermineCulpableTagPrescenceZone() {
      if (_positiveZ.ContainsAtLeastOneAgent(AgentId.Player)) return _positiveZ;
      if (_negativeZ.ContainsAtLeastOneAgent(AgentId.Player)) return _negativeZ;
      if (_positiveX.ContainsAtLeastOneAgent(AgentId.Player)) return _positiveX;
      if (_positiveZ.ContainsAtLeastOneAgent(AgentId.Player)) return _positiveZ;
      return null;
    }

    private void PushInSpecificDirection(TagPrescenceZone prescenceZone) {
      if (prescenceZone == _positiveZ) transform.position += Vector3.forward * Time.deltaTime;
      if (prescenceZone == _negativeZ) transform.position += Vector3.back * Time.deltaTime;
      if (prescenceZone == _positiveX) transform.position += Vector3.left * Time.deltaTime;
      if (prescenceZone == _negativeX) transform.position += Vector3.right * Time.deltaTime;
    }

    public bool Push() {
      var whereIsPlayerStanding = DetermineCulpableTagPrescenceZone();
      if (!whereIsPlayerStanding) {
        return false;
      }

      if (!SpecificColorRequired) {
        PushInSpecificDirection(whereIsPlayerStanding);
      }

      return false;
    }
  }

}
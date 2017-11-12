/*
 * TagPrescenceZone.cs
 * Author: Samuel Vargas
 *
 * Provides an interface to check to see if there
 * is an object with the expected Tag(s) in the specificed
 * location using a BoxCollider
 *
 * The first item that collides is returned.
 * A second item cannot replace the first item until OnTriggerExit
 * is invoked for the first item.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tags {

  public class TagPrescenceZone : MonoBehaviour {
    public Vector3 Center;
    public Vector3 Size;

    private const bool IsTrigger = true;
    private BoxCollider _boxCollider;
    private bool _isItemPresent;
    private List<GameObject> _itemsInPresceneZone;

    private Action<GameObject> _onEntry;
    private Action<GameObject> _onExit;

    public void SetOnEntry(Action<GameObject> onEntry) {
      Debug.AssertFormat(_onEntry == null, "Caught attempt to redefine OnEntry method");
      _onEntry = onEntry;
    }

    public void SetOnExit(Action<GameObject> onExit) {
      Debug.AssertFormat(_onExit == null, "Caught attempt to redefine OnExit method");
      _onExit = onExit;
    }

    private void Start() {
      _itemsInPresceneZone = new List<GameObject>();
      _boxCollider = gameObject.AddComponent<BoxCollider>();
      _boxCollider.center = Center;
      _boxCollider.size = Size;
      _boxCollider.isTrigger = IsTrigger;
    }

    public bool IsEmpty() {
      return _itemsInPresceneZone.Count == 0;
    }

    public bool ContainsAtLeastOneAgent(AgentId agentId) {
      return _itemsInPresceneZone.Select(obj => obj.GetComponent<Tag>())
                                 .Any(ttag => ttag.Type == TagType.Agent && ttag.AgentId == agentId);
    }

    public bool ContainsAtLeastOnceDevice(DeviceId device, out GameObject found) {
      found = null;
      foreach (var gameObj in _itemsInPresceneZone) {
        var ttag = gameObj.GetComponent<Tag>();
        if (ttag.Type == TagType.Device && ttag.DeviceId == device) {
          found = gameObj;
          return true;
        }
      }

      return false;
    }
    
    public GameObject GetAgent(AgentId agentId) {
      foreach (var gameObj in _itemsInPresceneZone) {
        var ttag = gameObj.GetComponent<Tag>();
        if (ttag.Type == TagType.Device && ttag.AgentId == agentId) {
          return gameObj;
        }
      }

      return null;
    }

    public List<GameObject> GetDevices(DeviceId deviceId) {
      List<GameObject> output = new List<GameObject>();
      foreach (var gameObj in _itemsInPresceneZone) {
        var ttag = gameObj.GetComponent<Tag>();
        if (ttag.Type == TagType.Device && ttag.DeviceId == deviceId) {
          output.Add(gameObj);
        }
      }

      return output;
    }

    public bool GetFirstDevice(DeviceId deviceId, out GameObject device) {
      device = null;
      foreach (var gameObj in _itemsInPresceneZone) {
        var ttag = gameObj.GetComponent<Tag>();
        if (ttag.Type == TagType.Device && ttag.DeviceId == deviceId) {
          device = gameObj;
          return true;
        }
      }
      
      return false;
    }

    private void OnTriggerEnter(Collider other) {
      var objTag = other.GetComponent<Tag>();
      //Debug.AssertFormat(objTag != null, "OnTriggerEnter: GameObject '{0}', is missing a Tag component.", other.name);
      if (!objTag)
        return;
      
      if (_onEntry != null) {
        _onEntry(other.gameObject);
      }
      _itemsInPresceneZone.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other) {
      var objTag = other.GetComponent<Tag>();
      if (!objTag)
        return;
      
      //Debug.AssertFormat(objTag != null, "OnTriggerEnter: GameObject '{0}', is missing a Tag component.", other.name);
      if (_itemsInPresceneZone.Contains(other.gameObject)) {
        if (_onExit != null) {
          _onExit(other.gameObject);
        }
        _itemsInPresceneZone.Remove(other.gameObject);
      }
    }
  }

}
/*
 * Tag.cs
 * Author: Samuel Vargas
 */

using System;
using UnityEngine;

namespace Tags {

  [Serializable]
  public class Tag : MonoBehaviour {
    [SerializeField] private TagType _type;
    [SerializeField] private SensorId _sensorId;
    [SerializeField] private AgentId _agentId;
    [SerializeField] private GeometryId _geometryId;
    [SerializeField] private DeviceId _deviceId;
    [SerializeField] private PickUpId _pickUpId;

    public TagType Type {
      get { return _type; }
      set { _type = value; }
    }

    public SensorId SensorId {
      get {
        Debug.Assert(Type == TagType.Sensor,
          "Get invoked on SensorId but TagType was: " + Enum.GetName(typeof(TagType), Type));
        return _sensorId;
      }

      set {
        Debug.Assert(Type == TagType.Sensor,
          "Set invoked on SensorId but TagType was: " + Enum.GetName(typeof(TagType), Type));
        _sensorId = value;
      }
    }

    public AgentId AgentId {
      get {
        Debug.Assert(Type == TagType.Agent,
          "Get invoked on AgentId but TagType was: " + Enum.GetName(typeof(TagType), Type));
        return _agentId;
      }
      set {
        Debug.Assert(Type == TagType.Agent,
          "Set invoked on AgentId but TagType was: " + Enum.GetName(typeof(TagType), Type));
        _agentId = value;
      }
    }

    public GeometryId GeometryId {
      get {
        Debug.Assert(Type == TagType.Geometry,
          "Get invoked on GeometryId but TagType was: " + Enum.GetName(typeof(TagType), Type));
        return _geometryId;
      }
      set {
        Debug.Assert(Type == TagType.Geometry,
          "Set invoked on Geometryid but TagType was: " + Enum.GetName(typeof(TagType), Type));
        _geometryId = value;
      }
    }

    public DeviceId DeviceId {
      get {
        Debug.Assert(Type == TagType.Device,
          "Get invoked on DeviceId but TagType was: " + Enum.GetName(typeof(TagType), Type));
        return _deviceId;
      }
      set {
        Debug.Assert(Type == TagType.Device,
          "Set invoked on DeviceId but TagType was: " + Enum.GetName(typeof(TagType), Type));
        _deviceId = value;
      }
    }

    public PickUpId PickUpId {
      get {
        Debug.Assert(Type == TagType.PickUp,
          "Get invoked on PickUpId but TagType was: " + Enum.GetName(typeof(TagType), Type));
        return _pickUpId;
      }
      set {
        Debug.Assert(Type == TagType.PickUp,
          "Set invoked on PickUpId but TagType was: " + Enum.GetName(typeof(TagType), Type));
        _pickUpId = value;
      }
    }
  }

}
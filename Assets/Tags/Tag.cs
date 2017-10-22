/*
 * Tag.cs
 * Author: Samuel Vargas
 */

using UnityEngine;

namespace Tags {

  [System.Serializable]
  public class Tag : MonoBehaviour {
    [SerializeField] public TagType Type;
    [SerializeField] public SensorId SensorId;
    [SerializeField] public AgentId AgentId;
    [SerializeField] public GeometryId GeometryId;
    [SerializeField] public DeviceId DeviceId;
  }

}
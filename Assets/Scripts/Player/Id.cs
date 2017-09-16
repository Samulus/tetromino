using System;
using UnityEngine;

namespace Player {
  public class Id {
    private string _uuid;
    private GameObject _textObject;
    private TextMesh _textMesh;

    private void Awake() {
      _uuid = Guid.NewGuid().ToString();
      _textObject = new GameObject();
      _textObject.AddComponent<TextMesh>();
    }
  }
}
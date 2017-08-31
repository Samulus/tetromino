/*
	TetrominoSpawner.cs
	Author: Samuel Vargas
*/

using UnityEngine;

namespace Tetris {
  public partial class TetrominoSpawner : MonoBehaviour {
    
    void Start() {
      GameObject instance = Instantiate(Resources.Load("L", typeof(GameObject))) as GameObject;
      instance.transform.position = new Vector3(0, 0, 1);
      instance.transform.localScale = new Vector3(0.20f, 0.20f, 0.20f);
      instance.transform.rotation = Quaternion.identity;
    }


    void Update() {
    }
  }
}
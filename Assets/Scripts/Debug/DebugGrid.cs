/*
	DebugGrid.cs
	Author: Samuel Vargas
	
	Draws a 2D grid
*/

using UnityEditor;
using UnityEngine;

public class DebugGrid : MonoBehaviour {
  public Color color = Color.yellow;
  public int lengthOfLineRenderer = 20;

  private Rect _pixelRect;

  void Start() {
    _pixelRect = Camera.current.pixelRect;
    
    LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
    lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
    lineRenderer.widthMultiplier = 0.2f;
    lineRenderer.positionCount = (int) lengthOfLineRenderer;

    lineRenderer.numCornerVertices = 100;

    for (int i = 0; i < )

        // A simple 2 color gradient with a fixed alpha of 1.0f.
          float alpha = 1.0f;
      Gradient gradient = new Gradient();
      gradient.SetKeys(
        new GradientColorKey[] {new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f)},
        new GradientAlphaKey[] {new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f)}
      );
      lineRenderer.colorGradient = gradient;
      }

      void Update() {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        var t = Time.time;
        for (int i = 0; i < lengthOfLineRenderer; i++) {
          lineRenderer.SetPosition(i, new Vector3(i * 0.5f, Mathf.Sin(i + t) * 2, 1.0f));
        }
      }
      }
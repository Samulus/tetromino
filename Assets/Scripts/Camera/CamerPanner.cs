/*
 * CameraPanner.cs
 * Author: Samuel Vargas
 *
 * This module allows the player to pan the active camera
 * by pressing Middle Mouse Button in different regions
 * of the screen.
 */

using UnityEngine;

namespace Camera {

  public class CamerPanner : MonoBehaviour {
    private float maxPercent = 0.7f;

    private Vector2 NormalizedMousePosition() {
      var mousePos = UnityEngine.Camera.main.ScreenToViewportPoint(Input.mousePosition);
      return new Vector2(mousePos.x * 2 - 1, mousePos.y * 2 - 1);
    }

    private bool PanCamera(Vector2 mouse) {
      bool panned = false;

      if (Input.GetMouseButton(2)) {
        
        // Up Left
        if (mouse.x <= -maxPercent && mouse.y >= maxPercent) {
          transform.Translate(Vector3.left * Time.deltaTime * 2);
          transform.Translate(Vector3.up * Time.deltaTime * 2);
          panned = true;
        }
        
        // Up Right
        else if (mouse.x >= maxPercent && mouse.y >= maxPercent) {
          transform.Translate(Vector3.right * Time.deltaTime * 2);
          transform.Translate(Vector3.up * Time.deltaTime * 2);
        }
        
        // Bottom Left
        else if (mouse.x <= -maxPercent && mouse.y <= -maxPercent) {
          transform.Translate(Vector3.left * Time.deltaTime * 2);
          transform.Translate(Vector3.down * Time.deltaTime * 2);
          panned = true;
        }
        
        // Bottom Right
        else if (mouse.x >= maxPercent && mouse.y <= -maxPercent) {
          transform.Translate(Vector3.right * Time.deltaTime * 2);
          transform.Translate(Vector3.down * Time.deltaTime * 2);
        }
        
        // Left
        else if (mouse.x <= -maxPercent) {
          transform.Translate(Vector3.left * Time.deltaTime * 2);
          panned = true;
        }
        
        else if (mouse.x >= maxPercent) {
          transform.Translate(Vector3.right * Time.deltaTime * 2);
          panned = true;
        }

        else if (mouse.y <= -maxPercent) {
          transform.Translate(Vector3.down * Time.deltaTime * 2);
          panned = true;
        }

        else if (mouse.y >= maxPercent) {
          transform.Translate(Vector3.up * Time.deltaTime * 2);
          panned = true;
        }
        
        /*
         * Up:
         */
      }

      return panned;
    }

    private void Update() {
      PanCamera(NormalizedMousePosition());
    }
  }

}
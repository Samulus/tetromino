using UnityEngine;

namespace _Debug {

	public class EchoTransform : MonoBehaviour {

		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		private void Update () {
			UnityEngine.Debug.Log(transform.forward);	
		}
	}

}

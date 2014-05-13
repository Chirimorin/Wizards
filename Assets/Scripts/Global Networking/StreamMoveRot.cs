using UnityEngine;
using System.Collections;

public class StreamMoveRot : MonoBehaviour {

	// Use this for initialization
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		if (stream.isWriting) {
			Vector3 posin = new Vector3();
			Quaternion rot = new Quaternion();
			posin = transform.position;
			rot = transform.rotation;
			stream.Serialize (ref posin);
		}
		if (stream.isReading) {
			Vector3 posout = new Vector3();
			Quaternion rot = new Quaternion();
			stream.Serialize (ref posout);
			transform.position = posout;
		}
	}

}

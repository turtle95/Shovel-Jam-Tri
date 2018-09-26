using UnityEngine;
using UnityEngine.Assertions;

public abstract class GetMyTransform : MonoBehaviour {

	protected Transform myTransform;

	protected virtual void Awake() {
		myTransform = GetComponent<Transform>();
		Assert.IsNotNull(myTransform);
	}

}

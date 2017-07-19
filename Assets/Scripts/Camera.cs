using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {
    public Transform target;
    private Transform self;

    private void Start()
    {
        self = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update () {
        Vector3 newPosition = target.position;
        newPosition.z = self.position.z;

        self.position = newPosition;
	}
}

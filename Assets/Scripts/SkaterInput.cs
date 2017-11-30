using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkaterInput : MonoBehaviour {

    private SkaterController skaterController;

	// Use this for initialization
	void Start () {
        skaterController = GetComponent<SkaterController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {

        if (Input.GetButtonDown("Push") && skaterController.grounded && !skaterController.grinding && Time.time > skaterController.nextPush)
        {
            
            skaterController.Push();
        }

        if (Input.GetButton("Stop"))
        {
            skaterController.Stop();
        }

        if (Input.GetButtonDown("Ollie") && (skaterController.grounded || skaterController.grinding) && Time.time > skaterController.nextOllie)
        {
            skaterController.Ollie();
        }
    }
}

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
        // PUSH
        if (Input.GetButtonDown("Push"))
        {
            skaterController.Push();
        }

        // GRIND
        if (Input.GetButton("Grind"))
        {
            skaterController.Grind();
        }
        
        if (Input.GetButtonUp("Grind"))
        {
            skaterController.ReleaseGrind();
        }

        // STOP
        if (Input.GetButton("Stop"))
        {
            skaterController.Stop();
        }


        if (Input.GetButton("Ollie"))
        {
            skaterController.SetChargingOllie(true);
        }
        else
        {
            skaterController.SetChargingOllie(false);
        }
        
        if (Input.GetButtonUp("Ollie"))
        {
            skaterController.Ollie();
        }

        if (Input.GetButton("Grab"))
        {
            skaterController.Grab();
        }
        else {
            skaterController.GrabRelease();
        }
    }
}

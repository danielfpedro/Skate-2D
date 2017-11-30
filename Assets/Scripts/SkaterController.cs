using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkaterController : MonoBehaviour {
    
    public bool askingForGrind = false;

    [Header("Push")]
    public float pushForce = 10f;
    // Depois substituir o rate pela duração da animação de push
    public float pushRate = 1f;
    public float nextPush;

    [Header("Ollie")]
    public float olliePower = 3f;
    public float ollieRate = 0.1f;
    public float nextOllie;

    // Desacceleration
    public float desaccelaration = 1f;

    public bool grounded = false;
    public bool grinding = false;
    public bool grabing = false;

    public float maxSpeed = 3f;
    public float accelaration = 1f;

    private float rayDistance = 3f;

    public Transform groundCheck;
    public LayerMask groundMask;

    public LayerMask raycastAllowed;
    public LayerMask raycastAllowedRail;

    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        askingForGrind = false;
	}

    private void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask);

        Vector2 rayInitial = (Vector2)transform.position;
        Vector2 direction = transform.TransformDirection(Vector2.down).normalized;
        float distance = 1.5f;

        RaycastHit2D[] hits = Physics2D.RaycastAll(rayInitial, direction, distance, raycastAllowed);
        Debug.DrawRay(transform.position, direction * distance);

        /**foreach (RaycastHit2D hit in hits)
        {**/
        if (hits.Length > 0)
        {
            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, hits[0].normal);
            // transform.rotation = Quaternion.FromToRotation(Vector3.up, hits[0].normal);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1f);
        }
            
            // Debug.Log("Colidou com ramp" + hit.collider.tag);
        // }

        float horizontal = Input.GetAxis("Horizontal");

        /**if (!grounded && Input.GetButton("Jump"))
        {
            askingForGrind = true;
        }
        else
        {
            askingForGrind = false;
        }**/

        /**if (!grounded && Input.GetButton("Fire2"))
        {
            grabing = true;
        } else
        {
            grabing = false;
        }**/

        if (grabing)
        {
            transform.Rotate(0.0f, 0.0f, -Input.GetAxis("Horizontal") * 5f);
        }


    }

    public void Push()
    {
        nextPush = Time.time + pushRate;
        rb.AddRelativeForce(Vector2.right * pushForce, ForceMode2D.Impulse);
    }

    public void Stop() {
        float desiredAccelaration = Mathf.Lerp(rb.velocity.x, 0.0f, desaccelaration);
        rb.velocity = new Vector2(desiredAccelaration, rb.velocity.y);
    }

    public void Ollie() {
        nextOllie = Time.time + ollieRate;
        // rb.velocity = new Vector2(rb.velocity.x, olliePower);
        rb.AddRelativeForce(Vector2.up * olliePower, ForceMode2D.Impulse);
    }
}
 
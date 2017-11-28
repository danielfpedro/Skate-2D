using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkaterController : MonoBehaviour {

    public bool grounded = false;

    public float maxSpeed = 3f;
    public float accelaration = 1f;
    public float jumpPower = 3f;
    public float jumpRate = 0.1f;

    private float nextJump;

    private float rayDistance = 3f;

    public Transform groundCheck;
    public LayerMask groundMask;

    public LayerMask raycastAllowed;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    private void Update()
    {
    }

    // Update is called once per frame
    void FixedUpdate () {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask);

        Vector2 rayInitial = (Vector2)transform.position;
        Vector2 direction = transform.TransformDirection(Vector2.down).normalized;
        float distance = 4f;

        RaycastHit2D[] hits = Physics2D.RaycastAll(rayInitial, direction, distance, raycastAllowed);
        Debug.DrawRay(transform.position, direction * distance);

        foreach (RaycastHit2D hit in hits) {
            transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            Debug.Log("Colidou com ramp" + hit.collider.tag);
        }
        

        float horizontal = Input.GetAxis("Horizontal");

        // Debug.Log(Vector2.right * accelaration * horizontal);

        if ((rb.velocity.x > -maxSpeed && rb.velocity.x < maxSpeed) && grounded)
        {
            // rb.AddRelativeForce(Vector2.right * accelaration * horizontal);
        }

        float desiredSpeed = accelaration * horizontal;
        float finalSpeed = (rb.velocity.x > -maxSpeed && rb.velocity.x < maxSpeed) ? desiredSpeed : 0;
        rb.velocity = new Vector2(rb.velocity.x + finalSpeed, rb.velocity.y);

        if (Input.GetButton("Jump") && grounded && Time.time > nextJump)
        {
            nextJump = Time.time + jumpRate;
            Debug.Log("Pular");
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }
}
 
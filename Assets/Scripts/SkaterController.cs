using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkaterController : MonoBehaviour {

    [Header("Grind")]
    public bool grinding = false;
    public bool tryinToGrind = false;
    public LayerMask raycastAllowedRail;

    [Tooltip("Colomos um Delay para ele sair do grind após o jogador tirar o dedo do botão para que ele consigo emendar um ollie de um grind.")]
    public float delayToReleaseGrind = 2f;
    private float exitGrindTimeControl;
    [HideInInspector]
    public BoxCollider2D currentRailGrinding;

    [Header("Push")]
    public float pushForce = 20f;
    // Depois substituir o rate pela duração da animação de push
    public float pushRate = 0.3f;
    public float minimunSpeedToPushForward = -1f;
    private float nextPush;

    [Header("Ollie")]
    public float olliePower = 30f;
    public float ollieRate = 0.2f;
    public float nextOllie;

    [Header("Ground Controll")]
    public Transform groundCheck;
    public LayerMask checkGroundMask;
    public float groundCheckRadius = 0.2f;
    public bool grounded = false;

    [Header("Horizontal Movement")]
    public float desaccelaration = 0.15f;
    public float maxSpeed = 5f;

    [Header("Z Rotation")]
    public float zRotationSpeed = 5f;

    [Header("Raycast")]
    public float raycastDistance = 5f;
    public Transform raycastOrigin;
    public LayerMask raycastAllowed;

    [Header("Outros")]
    public bool grabing = false;

    [HideInInspector]
    public float horizontal;

    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Animator animator;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}

    private void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        SetGrounded();

        Vector2 rayInitial = (Vector2)raycastOrigin.position;
        Vector2 direction = transform.TransformDirection(Vector2.down).normalized;

        RaycastHit2D[] hits = Physics2D.RaycastAll(rayInitial, direction, raycastDistance, raycastAllowed);
        Debug.DrawRay(rayInitial, direction * raycastDistance);

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

        horizontal = Input.GetAxis("Horizontal");

        if (grabing)
        {
            Debug.Log("Rodando Rodando");
            transform.Rotate(0.0f, 0.0f, -Input.GetAxis("Horizontal") * zRotationSpeed);
        }

        // Avoid skater reach more than max speed that he supose to reach
        if (reachedFullSpeed())
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
    }

    // First try to grind and another script on wheels says if is grinding or not
    // on this script on the variable grinding
    public void Grind() {
        if (!grounded)
        {
            // Manda mensage para o wheel que está tentando grind
            tryinToGrind = true;
            CancelInvoke("ReleaseGrindAfterDelay");
        }
    }
    public void KeepGrind() {
        grinding = true;
        if (currentRailGrinding != null)
        {
            currentRailGrinding.isTrigger = false;
        }
    }
    public void ReleaseGrindAfterDelay()
    {
        Debug.Log("Saiu do grind");
        grinding = false;
        if (currentRailGrinding != null)
        {
            currentRailGrinding.isTrigger = true;
            currentRailGrinding = null;
        }

        CancelInvoke("ReleaseGrindAfterDelay");
    }
    public void ReleaseGrind() {
        tryinToGrind = false;
        Invoke("ReleaseGrindAfterDelay", delayToReleaseGrind);
    }

    public void GrabRelease()
    {
        grabing = false;
    }
    public void Grab() {
        if (!grounded)
        {
            grabing = true;
        }
        else {
            grabing = false;
        }
    }

    private bool reachedFullSpeed() {
        return (rb.velocity.x >= maxSpeed);
    }

    public void Push()
    {
        // since velocity.x is under 0 it means that the skater is goind backwards
        // Social we stop the skate instead of push cause is not natural push forward goind backward
        if (rb.velocity.x < minimunSpeedToPushForward)
        {
            Stop();
            return;
        }

        if (grounded && !grinding && Time.time > nextPush && !reachedFullSpeed())
        {
            nextPush = Time.time + pushRate;
            rb.AddRelativeForce(Vector2.right * pushForce, ForceMode2D.Impulse);
        }
    }

    public void Stop() {
        if (grounded)
        {
            float desiredAccelaration = Mathf.Lerp(rb.velocity.x, 0.0f, desaccelaration);
            rb.velocity = new Vector2(desiredAccelaration, rb.velocity.y);
        }
    }

    public void SetChargingOllie(bool flag) {
        // Se está fora o rate não charge o Ollie
        flag = (Time.time <= nextOllie) ? false : flag;
        animator.SetBool("ChargingOllie", flag);
    }

    public void Ollie() {
        animator.SetBool("ChargingOllie", false);

        if ((grounded || grinding) && Time.time > nextOllie)
        {
            nextOllie = Time.time + ollieRate;

            rb.AddRelativeForce(Vector2.up * (olliePower + rb.velocity.x), ForceMode2D.Impulse);
        }
    }

    private void SetGrounded() {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, checkGroundMask);
        animator.SetBool("Grounded", grounded);
    }
}
 
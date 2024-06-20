using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Collider2D basicAtackCollider;
    public float speed = 3;
    public float jumpForce = 6000f;
    public float bigJumpForce = 60f;
    private Rigidbody2D rb;
    private float moveDirection;
    public bool shockWave;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("GroundImpact");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            if(IsGrounded()) {
                Jump();
            }
        }
        if (Input.GetKeyDown(KeyCode.K)){
            if(IsGrounded()) {
                BigJump();
            }
        }
        moveDirection = Input.GetKey(KeyCode.LeftArrow) ? -1 : Input.GetKey(KeyCode.RightArrow) ? 1 : 0 ;
        moveDirection = Input.GetKey(KeyCode.LeftShift) ? moveDirection * 2 : moveDirection;
        // Debug.Log(moveDirection);
        transform.Translate(Vector3.right * moveDirection * speed*Time.deltaTime);
        // Debug.Log(transform.position);
    }

    private void Jump() {
        rb.AddForce(Vector3.up * jumpForce,ForceMode2D.Impulse);
    }
    private void BigJump() {
        rb.AddForce(Vector3.up * bigJumpForce,ForceMode2D.Impulse);
    }

    private bool IsGrounded(){
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y -= 0.65f;

        RaycastHit2D hit = Physics2D.Raycast(rayCastOrigin, Vector2.down, 0.05f);
        if (hit.collider != null){
            return true;
        }
        //hit.collider.name.Equals("");
        //animator.SetBool("jumping", true);
        return false;
    }
    public IEnumerator GroundImpact(){
        float timer = 0;
        while(true){
            if(IsGrounded()){
                timer = 0;
                yield return new WaitUntil(() => !IsGrounded());
            } else {
                yield return new WaitForSeconds(0.5f);
                timer ++;
                if (timer >= 4){
                    Debug.Log("Shockwave ready");
                    shockWave= true;
                }
            }

        }
    }
}

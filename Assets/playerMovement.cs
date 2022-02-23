using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private float moveSpeed = 25;
    [Range(0.01f, 0.5f)] [SerializeField]private float movementSmoothing;
    float h;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move() {
        Vector3 refer = Vector3.zero;
        Vector3 targetVelocity = new Vector2(h * moveSpeed * 10 * Time.deltaTime, GetComponent<Rigidbody2D>().velocity.y);

        GetComponent<Rigidbody2D>().velocity = Vector3.SmoothDamp(GetComponent<Rigidbody2D>().velocity, targetVelocity, ref refer, movementSmoothing);
    }
}

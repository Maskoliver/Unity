using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float attackRange = 0.5f;
    public Transform attackPoint;
    public Animator animator;
    public Rigidbody2D rb;
    Vector2 movement;

    void Start()
    {
        Transform characterPosition = this.transform;
        characterPosition.position = new Vector3(characterPosition.position.x ,characterPosition.position.y-1f , characterPosition.position.z);
        attackPoint = characterPosition;
    }
    private void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (movement.x > 0f)
            animator.SetFloat("Facing", 4);
        if (movement.x < 0f)
            animator.SetFloat("Facing", 3);
        if (movement.y > 0f)
            animator.SetFloat("Facing", 2);
        if (movement.y < 0f)
            animator.SetFloat("Facing", 1);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
           
        }

    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementSpeed * movement * Time.fixedDeltaTime);
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange);
        
        animator.SetTrigger("Attack");
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
        }
        
    }
}

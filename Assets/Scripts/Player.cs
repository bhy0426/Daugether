using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Transform hand_right;
    public Transform hand_left;
    [SerializeField]
    float speed;
    Vector2 moveDir;
    Vector2 mouse;
    float angle_left;
    float angle_right;


    // Start is called before the first frame update
    void Awake()
    {
        rigid.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed * Time.fixedDeltaTime;

        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle_right = Mathf.Atan2(mouse.y - hand_right.position.y, mouse.x - hand_right.position.x) * Mathf.Rad2Deg;
        angle_left = Mathf.Atan2(mouse.y - hand_left.position.y, mouse.x - hand_left.position.x) * Mathf.Rad2Deg;

        if (hand_right.position.x < mouse.x)
        {
            hand_right.localRotation = Quaternion.Euler(0, 0, angle_right);
            hand_left.localRotation = Quaternion.Euler(0, 0, angle_left);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (hand_right.position.x > mouse.x)
        {
            hand_right.localRotation = Quaternion.Euler(0, 0, -(angle_right + 180f));
            hand_left.localRotation = Quaternion.Euler(0, 0, -(angle_left + 180f));
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        } 
    }

    void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + moveDir);
    }
}

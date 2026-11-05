using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rigid;
    public SpriteRenderer spriteRenderer;
    public Transform target;

    [SerializeField] bool isHit;
    [SerializeField] int hp = 10;
    float hitTime = 0.5f;

    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        rigid.GetComponent<Rigidbody2D>();
        spriteRenderer.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isHit)
        {
            Vector2 dirVec = target.position - transform.position;
            Vector2 nextVec = dirVec.normalized * speed * Time.deltaTime;
            rigid.MovePosition(rigid.position + nextVec);
        }
    }

    public void Hit()
    {
        isHit = true;
        rigid.velocity = Vector2.zero;
        Invoke("disableHit", hitTime);
        hp -= 1;
        spriteRenderer.color = Color.red;
        Debug.Log($"{transform.name}({hp}) : 맞음");
    }

    void disableHit()
    {
        isHit = false;
        spriteRenderer.color = Color.white;
    }
}

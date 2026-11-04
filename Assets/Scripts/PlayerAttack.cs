using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Collider2D[] enemies;
    [SerializeField]
    Collider2D nearestEnemy = null;
    [SerializeField]
    float nearestDis = float.PositiveInfinity;
    bool isAttack = false;
    float attackDur = 1f;
    float attackRange = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        // circleCollider2D.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.DrawRay(transform.position, transform.forward * 2f, Color.red);

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void Attack()
    {
        if (!isAttack)
        {
            isAttack = true;

            // 가까운 적을 찾아서 nearestEnemy에 저장
            nearestEnemy = null;
            nearestDis = float.PositiveInfinity;

            enemies = Physics2D.OverlapCircleAll(transform.position, attackRange);

            foreach (Collider2D enemy in enemies)
            {
                float dis = Vector2.Distance(transform.position, enemy.transform.position);

                if (nearestDis > dis)
                {
                    nearestDis = dis;
                    nearestEnemy = enemy;
                }
            }

            // 공격
            if (enemies.Length > 0)
            {
                Debug.Log("공격");
                nearestEnemy.GetComponent<Enemy>().Hit();
            }
            else
            {
                Debug.Log("근처에 적이 없음");
            }

            Invoke("AttackCoolDown", attackDur);
        }
        else
        {
            Debug.Log("공격 쿨타임");
        }
    }
    
    void AttackCoolDown()
    {
        isAttack = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

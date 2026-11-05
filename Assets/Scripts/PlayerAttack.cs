using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Player player;
    public Collider2D[] enemies;
    [SerializeField]
    Collider2D nearestEnemy = null;
    [SerializeField]
    float nearestDis = float.PositiveInfinity;


    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public void Attack()
    {
        if (player.canAttack)
        {
            player.isAttack = true;
            player.canAttack = false;

            // 가까운 적을 찾아서 nearestEnemy에 저장
            nearestEnemy = null;
            nearestDis = float.PositiveInfinity;

            enemies = Physics2D.OverlapCircleAll(transform.position, player.attackRange);

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

            Invoke("DisableIsAttack", player.attackDur);
            Invoke("EnableCanAttack", player.attackCoolDown);
        }
        else
        {
            Debug.Log("공격 쿨타임");
        }
    }
    
    void DisableIsAttack()
    {
        player.isAttack = false;
    }

    void EnableCanAttack()
    {
        player.canAttack = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, player.attackRange);
    }
}

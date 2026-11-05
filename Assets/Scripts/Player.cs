using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 컴포넌트
    private Rigidbody2D rigid;
    private PlayerAttack playerAttack;
    private PlayerMove playerMove;
    private PlayerLook playerLook;
    private Animator anim;

    // 팔 오브젝트
    public Transform handsRight;
    public Transform handsLeft;
    public Animator handsRightAnim;
    public Vector2[] handsRightPos;

    // 상태
    public float moveSpeed;
    public float rollSpeed;
    public float roolDuration; // 구르기 지속 시간
    public float rollCoolDown; // 구르기 쿨타임
    public bool isRoll = false;
    public bool canRoll = true;
    public bool isAttack = false;
    public bool canAttack = true;
    public float attackDur = 0.125f;
    public float attackCoolDown = 1f;
    public float attackRange = 0.6f;
    public float attackMultiplier = 2f;
    // public float Speed => speed; 와 동일, 읽기 전용 프로퍼티라는 의미

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerAttack = GetComponent<PlayerAttack>();
        playerMove = GetComponent<PlayerMove>();
        playerLook = GetComponent<PlayerLook>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 플레이어 시선 조작
        playerLook.Look(handsRight, handsLeft);

        // 오른손 위치 이동
        handsRight.localPosition = isAttack ? handsRightPos[1] : handsRightPos[0];

        // 오른손 공격 애니메이션
        handsRightAnim.SetBool("isAttack", isAttack);
        handsRightAnim.SetFloat("AttackMultiplier", attackMultiplier);

        // 구르기 애니메이션
        anim.SetBool("isRoll", isRoll);

        // 공격
        if (Input.GetButtonDown("Fire1"))
        {
            playerAttack.Attack();
        }

        // 공격
        if (Input.GetButtonDown("Jump"))
        {
            playerMove.Roll(roolDuration, rollCoolDown);
        }
    }

    void FixedUpdate()
    {
        playerMove.Move(rigid, moveSpeed, rollSpeed);
    }
}

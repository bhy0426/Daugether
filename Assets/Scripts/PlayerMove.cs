using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Vector2 moveDir;
    private Player player;

    void Awake()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // WASD 이동 벡터
        moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * player.moveSpeed * Time.fixedDeltaTime;
    }

    public void Move(Rigidbody2D rigid, float speed, float rollSpeed)
    {
        // 구르기 시 이동속도 증가
        float val = player.isRoll ? rollSpeed : speed;

        // 플레이어 이동
        rigid.MovePosition(rigid.position + moveDir * val);
    }

    // 구르기
    public void Roll(float rollDuration, float rollCoolDown)
    {
        if (!player.canRoll) return;

        player.isRoll = true;
        player.canRoll = false;
        Invoke("DisableIsRoll", rollDuration);
        Invoke("EnableCanRoll", rollCoolDown);

        Debug.Log("구른다");
    }

    void DisableIsRoll()
    {
        player.isRoll = false;
    }

    void EnableCanRoll()
    {
        player.canRoll = true;
    }
}

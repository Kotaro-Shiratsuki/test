using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeKunMove : MonoBehaviour
{
    Rigidbody playerRg;
    private float hor;
    private float ver;

    [Header("Movement")]
    [SerializeField] float moveForce;
    [SerializeField] float moveForceMultiplier;
    [SerializeField] bool isMove;

    //移動方向の基準にするカメラ
    [Header("Camera")]
    [SerializeField] Camera freeLoockCam;


    // Start is called before the first frame update
    void Start()
    {
        playerRg = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        var direction = Vector3.zero;

        //カメラの方向からx-z平面の単位ベクトルを取得
        Vector3 camForward = Vector3.Scale(freeLoockCam.transform.forward, new Vector3(1, 0, 1)).normalized;

        //入力とカメラの方向から移動方向を決定
        direction = camForward * ver + freeLoockCam.transform.right * hor;

        //キャラの向きを進行方向に
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
            isMove = true;
        }
        else
        {
            isMove = false;
        }

        //移動方向に移動
        playerRg.AddForce(moveForceMultiplier * ((direction * moveForce) - playerRg.velocity));
    }
}

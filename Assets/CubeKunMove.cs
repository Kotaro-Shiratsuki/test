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

    //�ړ������̊�ɂ���J����
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

        //�J�����̕�������x-z���ʂ̒P�ʃx�N�g�����擾
        Vector3 camForward = Vector3.Scale(freeLoockCam.transform.forward, new Vector3(1, 0, 1)).normalized;

        //���͂ƃJ�����̕�������ړ�����������
        direction = camForward * ver + freeLoockCam.transform.right * hor;

        //�L�����̌�����i�s������
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
            isMove = true;
        }
        else
        {
            isMove = false;
        }

        //�ړ������Ɉړ�
        playerRg.AddForce(moveForceMultiplier * ((direction * moveForce) - playerRg.velocity));
    }
}

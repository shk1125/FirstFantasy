using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public float rotateSpeed;

    float xRotation;
    bool isDownMax;
    bool isUpMax;

    Transform playerTransform;
    PlayerCtrl playerCtrl;
    void Start()
    {
        playerTransform = transform.parent;
        playerCtrl = playerTransform.GetComponent<PlayerCtrl>();
        xRotation = 0;
    }


    void Update()
    {
        if(playerCtrl.isWithNPC)
        {
            return;
        }
        if (Input.GetButton("Fire2"))
        {
            Cursor.visible = false;
            Rotate();
        }
        else
        {
            Cursor.visible = true;
        }
    }

    void Rotate()
    {
        xRotation = Input.GetAxis("Mouse Y");
        if (transform.eulerAngles.x >= 30.0f && transform.eulerAngles.x < 180)
        {
            isDownMax = true; //각도 뿐만이 아니라 position도 바뀌기 때문에 그냥 Clamp를 쓸 수는 없다.
        }
        else if (transform.eulerAngles.x <= 340 && transform.eulerAngles.x >= 180)
        {
            isUpMax = true;
        }

        if (xRotation > 0)
        {
            isDownMax = false;
        }
        else if (xRotation < 0)
        {
            isUpMax = false;
        }

        if (!isDownMax && !isUpMax)
        {
            transform.RotateAround(playerTransform.position, transform.right, xRotation * rotateSpeed * -1 * Time.deltaTime);
        }
    }
}

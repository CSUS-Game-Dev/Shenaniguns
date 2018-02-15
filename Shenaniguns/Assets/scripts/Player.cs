using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    int playerNum;
    public void Init(int index) {
        playerNum = index;
    }


	void Update () {
        var x = Input.GetAxis("j"+playerNum+"x1") * Time.deltaTime * 10.0f;
        var z = Input.GetAxis("j"+playerNum+"y1") * Time.deltaTime * 10.0f;

        transform.Translate(x, 0, -z);
        GetComponent<Animator>().Play("run");

        var x2 = Input.GetAxis("j" + playerNum + "x2") * Time.deltaTime * 150.0f;

        transform.Rotate(0, x2, 0);

        var y2 = Input.GetAxis("j" + playerNum + "y2") * Time.deltaTime * 3.0f;

        Game_Manager.instance.cameras[playerNum-1].transform.Translate(0, y2, 0);
        Game_Manager.instance.cameras[playerNum - 1].transform.LookAt(transform);

        if (Input.GetButtonDown("j"+playerNum+"a")) {
            if (IsGrounded())
            {
                GetComponent<Animator>().Play("Jump");
                GetComponent<Rigidbody>().velocity = new Vector3(0, 18, 0);
            }
        }
    }

    bool IsGrounded(){
        if (GetComponent<Rigidbody>().velocity.y == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

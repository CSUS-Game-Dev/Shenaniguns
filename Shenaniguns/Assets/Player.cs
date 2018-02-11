using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    int playerNum;
    public void Init(int index) {
        playerNum = index;
    }


	void Update () {
        var x = Input.GetAxis("j"+playerNum+"x1") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("j"+playerNum+"y1") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }
}

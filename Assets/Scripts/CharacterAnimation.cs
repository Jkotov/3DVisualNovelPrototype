using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)){
            anim.SetBool("IsRunning", true);
        }
        else {anim.SetBool("IsRunning", false); }

        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("IsTurnRight", true);
        }
        else { anim.SetBool("IsTurnRight", false); }

        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("IsTurnLeft", true);
        }
        else { anim.SetBool("IsTurnLeft", false); }

    }
}

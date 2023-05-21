using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private Animator anim;
    int noOfClicks;
    bool canClick;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        noOfClicks = 0;
        canClick = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ComboStarter();
        }

    }

    void ComboStarter()
    {
        if (canClick)
        {
            noOfClicks++;
        }

        if (noOfClicks == 1)
        {
            anim.SetInteger("animation", 31);
        }
    }

    public void ComboCheck()
    {
        canClick = false;

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && noOfClicks == 1)
        {
            anim.SetInteger("animation", 4);
            canClick = true;
            noOfClicks = 0;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && noOfClicks >= 1)
        {
            anim.SetInteger("animation", 33);
            canClick = true;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2") && noOfClicks == 2)
        {
            anim.SetInteger("animation", 4);
            canClick = true;
            noOfClicks = 0;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2") && noOfClicks >= 3)
        {
            anim.SetInteger("animation", 6);
            canClick = true;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
        {
            anim.SetInteger("animation", 4);
            canClick = true;
            noOfClicks = 0;
        }
    }

    
}

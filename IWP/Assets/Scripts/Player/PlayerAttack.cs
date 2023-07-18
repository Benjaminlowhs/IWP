using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private Animator anim;
    int noOfClicks;
    bool canClick;
    public bool isAttacking;

    public bool isInCombo = false;
    public bool continueComboCheck = false;
    public float comboTimer = 5f;
    public int attackType = 0;
    PlayerStats playerstats;
    public GameObject playerWeapon;
    PlayerMovement playerMovement;

    CharacterController characterController;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        noOfClicks = 0;
        canClick = true;
        isAttacking = false;
        playerstats = GetComponent<PlayerStats>();
        playerMovement = GetComponent<PlayerMovement>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    ComboStarter();
        //}

        if (Input.GetMouseButtonDown(0))
        {
            if (characterController.isGrounded)
                ComboClickCheck();
        }

        if (continueComboCheck)
        {
            comboTimer -= Time.deltaTime;

            //Debug.Log(comboTimer);
            if (comboTimer < 0f)
            {
                continueComboCheck = false;
                isInCombo = false;
                comboTimer = 5f;
                anim.SetInteger("animation", 4);
                attackType = 0;
            }
        }
    }

    public void ComboClickCheck()
    {
        // Check if player is in combo
        if (!isInCombo)
        {
            // Start the first attack
            anim.SetInteger("animation", 31);
            playerstats.speed = 1f;
            isInCombo = true;
            attackType = 1;
            playerMovement.rotationSpeed = 50;

        }

        if (isInCombo && continueComboCheck)
        {
            continueComboCheck = false;
            comboTimer = 5f;
            if (attackType == 1)
            {
                anim.SetInteger("animation", 33);
                playerstats.speed = 1f;
                playerMovement.rotationSpeed = 50;
                attackType = 2;
            }
            else if (attackType == 2)
            {
                anim.SetInteger("animation", 6);
                playerstats.speed = 1f;
                playerMovement.rotationSpeed = 50;
                attackType = 3;
            }
        }
    }

    public void ComboSystem()
    {
        anim.SetInteger("animation", 4);
        playerMovement.rotationSpeed = 720f;
        playerstats.speed = 5f;
        continueComboCheck = true;
    }



    public void ComboEnd()
    {
        attackType = 0;
        isInCombo = false;
        playerstats.speed = 5f;
        playerMovement.rotationSpeed = 720f;
        anim.SetInteger("animation", 4);
    }

    void ComboStarter()
    {
        if (canClick)
        {
            noOfClicks++;
            isAttacking = true;
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
            isAttacking = false;
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
            isAttacking = false;
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
            isAttacking = false;
        }
    }

    public void WeaponColliderOn()
    {
        playerWeapon.GetComponent<Collider>().enabled = true;
    }

    public void WeaponColliderOff()
    {
        playerWeapon.GetComponent<Collider>().enabled = false;
    }

    
}

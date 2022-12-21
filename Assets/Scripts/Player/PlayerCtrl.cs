using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerCtrl : MonoBehaviour
{
    public float moveSpeed;
    public float jumpPower;
    public float rotateSpeed;
    public float talkableRadius;
    public float attackableDistance;
    public Image healthBar;
    public bool isWithNPC;
    public Transform weaponTransform;
    public Vector3 weaponRotation;
    public bool isGameOver;
    public AudioClip walkSound;
    public LayerMask layerMask;

    float xInput;
    float zInput;
    float yRotation;
    float gravity = -9.8f;
    bool doEquipWeapon;
    bool isAttacking;
    
    

    Vector3 dir;
    Ray ray;
    RaycastHit hit;
    Vector3 velocity;



    CharacterController cc;
    Animator anim;
    PlayerStat _stat;
    Text healthbarText;
    PlayerQuests playerQuests;
    PlayerItems playerItems;
    Transform currentWeaponTransform;
    WeaponFormat weaponFormat;
    EnemyCtrl enemyCtrl;
    AudioSource audioSource;
    
    

    void Start()
    {
        
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        playerQuests = GetComponent<PlayerQuests>();
        playerItems = GetComponent<PlayerItems>();
        _stat = GetComponent<PlayerStat>();
        audioSource = GetComponent<AudioSource>();

        healthbarText = healthBar.transform.GetComponentInChildren<Text>();
        healthbarText.text = ((int)_stat.HP).ToString() + "/" + ((int)_stat.MaxHP).ToString();

        doEquipWeapon = false;
        isGameOver = false;
        xInput = 0;
        zInput = 0;
        yRotation = 0;
    }

    
    void Update()
    {
        if(isWithNPC || isGameOver)
        {
            return;
        }
        Rotate();
        if (Input.GetButtonDown("Fire1") && cc.isGrounded)
        {
            ClickLeftMouse();
        }
        if(Input.GetKeyDown(KeyCode.J) && !GameManager.instance.playerQuestPanel.activeSelf)
        {
            GameManager.instance.OnPlayerQuestListPanel(playerQuests);
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            GameManager.instance.OnPlayerInventoryPanel(playerItems);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.GameOver("메뉴", false);
        }
    }

    private void FixedUpdate()
    {
        if (isWithNPC || isGameOver || isAttacking)
        {
            return;
        }

        Move();

        if (Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            velocity.y = jumpPower;
            anim.SetTrigger("jump");
        }
        else
        {
            velocity.y += gravity * Time.fixedDeltaTime;
        }

        cc.Move(velocity * Time.fixedDeltaTime);
    }

    void Move()
    {
        zInput = Input.GetAxisRaw("Vertical");
        xInput = Input.GetAxisRaw("Horizontal");
        dir = new Vector3(xInput, 0, zInput);
        if (isAttacking)
        {
            dir = Vector3.zero;
        }
        dir.Normalize();
        dir = transform.TransformDirection(dir);
        cc.SimpleMove(dir * moveSpeed);

        if (Mathf.Approximately(zInput, 0) && Mathf.Approximately(xInput, 0))
        {
            anim.SetBool("isWalking", false);
            audioSource.Stop();
            audioSource.loop = false;
        }
        else
        {
            anim.SetBool("isWalking", true);
            if (!audioSource.isPlaying)
            {
                audioSource.clip = walkSound;
                audioSource.loop = true;
                audioSource.Play();
            }
            anim.SetFloat("zDir", zInput);
            anim.SetFloat("xDir", xInput);
        }
        

    }



    void Rotate()
    {
        yRotation = Input.GetAxis("Mouse X");
        if(Input.GetButton("Fire2"))
        {
            transform.eulerAngles += new Vector3(0, yRotation * rotateSpeed * Time.deltaTime, 0);
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }

    void ClickLeftMouse()
    {
        
        
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            
            switch(hit.transform.tag)
            {
                case "NPC":
                    if (Vector3.Distance(transform.position, hit.transform.position) <= talkableRadius)
                    {
                        GameManager.instance.NPCStartTalk(hit.transform);
                        anim.SetBool("isWalking", false);
                    }
                    else
                    {
                        Attack();
                    }
                    break;
                default:
                    Attack();
                    break;
            }
        }
    }

    public void UseItem(int itemNumber)
    {

        switch(playerItems.itemList[itemNumber].tag)
        {
            case "Weapon":
                EquipWeapon(itemNumber);
                break;
        }

    }

    void EquipWeapon(int ItemNumber)
    {

        if(doEquipWeapon)
        {
            if(currentWeaponTransform == playerItems.itemList[ItemNumber].transform)
            {
                currentWeaponTransform.gameObject.SetActive(false);
                doEquipWeapon = false;
                return;
            }
            else
            {
                currentWeaponTransform.gameObject.SetActive(false);
            }
        }
        currentWeaponTransform = playerItems.itemList[ItemNumber].transform;
        currentWeaponTransform.position = weaponTransform.position;
        currentWeaponTransform.SetParent(weaponTransform);
        currentWeaponTransform.localRotation = Quaternion.Euler(weaponRotation);
        currentWeaponTransform.gameObject.SetActive(true);
        weaponFormat = currentWeaponTransform.GetComponent<WeaponFormat>();
        doEquipWeapon = true;

    }


    void Attack()
    {
        if(!EventSystem.current.IsPointerOverGameObject() && doEquipWeapon)
        {
            isAttacking = true;
            anim.SetTrigger("attack");
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
    }



    public void GiveDamage()
    {
        
        if (Physics.Raycast(transform.position + (Vector3.up * 1.5f), transform.forward, out hit, 1.5f, layerMask))
        {
            enemyCtrl = hit.transform.GetComponent<EnemyCtrl>();
        }

        if (enemyCtrl != null && enemyCtrl.state != EnemyCtrl.State.Death)
        {
            enemyCtrl.TakeDamage(weaponFormat.AttackPower + _stat.AttackPower);
            weaponFormat.playWeaponSound();
        }
        
    }

    public void TakeDamage(float damage)
    {
        if (damage > _stat.DefencePower)
        {
            _stat.HP -= damage - _stat.DefencePower;
            healthBar.fillAmount = _stat.HP / _stat.MaxHP;
            healthbarText.text = ((int)_stat.HP).ToString() + "/" + ((int)_stat.MaxHP).ToString();
            if (_stat.HP <= 0)
            {
                isGameOver = true;
                anim.SetTrigger("death");
                Invoke("GameOver", 5.0f);
            }
        }
    }

    void GameOver()
    {
        GameManager.instance.GameOver("플레이어가 패배했다...", true);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, talkableRadius);
        Gizmos.color = Color.black;
    }
}

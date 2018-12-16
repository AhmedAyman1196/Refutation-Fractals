using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class KhattabController : MonoBehaviour
{

    public static Animator anim;
    public GameObject camera_, axe, sword , pauseMenu , upgradeMenu ,GameOverMenu;
    bool grounded, canDoubleJump, canRage;
    public static int health = 100, rage = 0, level = 0, XP = 0, skillPoints = 0 , maxHealth = 100 , attack = 1;
    public static float time = 0, timeStartRaging, lastCollisionTime = -100.0f, walkSpeed = 0.05f, runSpeed = 0.1f, jumpForce = 0.5f, rageTime = 10 , timeScale ;
    int[] XPTarget = {500, 1000, 2000};
    Image khattabHealthImage, khattabRageImage, khattabXPImage;
    Text khattabLevelText, khattabSkillPointsText;
    public static bool isRaging , paused;


    // Use this for initialization
    void Start()
    {
        Set();
        Get();
        if(SceneManager.GetActiveScene().name == "Ayman"){
          Debug.Log("Entered Scene 2");
          // load khattab stats
        level = PlayerPrefs.GetInt("level");
        XP  =  PlayerPrefs.GetInt("XP") ;
        skillPoints =  PlayerPrefs.GetInt("skillPoints") ;
        walkSpeed = PlayerPrefs.GetFloat("walkSpeed") ;
        runSpeed  =  PlayerPrefs.GetFloat("runSpeed") ;
        maxHealth =  PlayerPrefs.GetInt("maxHealth") ;
        health = maxHealth;
        paused = false ;
        }
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown("k") && SceneManager.GetActiveScene().name == "Level1"){
        transform.position = new Vector3(12,18,85);
        Level1Controller.TARGET_KILLS = 0 ;
      }


       if(Input.GetKeyDown(KeyCode.P))
         paused = !paused;

        if(paused){
            pauseMenu.active = true;
            Cursor.visible = true ;
            Cursor.lockState = CursorLockMode.None;
         return;
        }else{
          Cursor.visible = false ;
          Cursor.lockState = CursorLockMode.Confined;
          pauseMenu.active = false;
        }

        time += Time.deltaTime;
        PlayerCanvas();
        Animations();

    }

    void swapWeapon()
    {
        axe.SetActive(axe.activeSelf ^ true);
        sword.SetActive(sword.activeSelf ^ true);
    }
    public static void bossHit(){
      bool blocking = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
      if(!blocking){
          Debug.Log("You got hit by the boss");
        //  GetComponent<PlayerAudio>().PlayHit();
          health -= 10;
         anim.SetBool("Hit", true);
      }
    }

    public static void bossExit(){
      anim.SetBool("Hit", false);
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "MinionsArm")
        {
            bool blocking = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
            bool minionIsAttacking = GameController.findParentWithTag(collision.gameObject, "Minion").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack");
            if (time - lastCollisionTime <= 1 || blocking || !minionIsAttacking) return;
            lastCollisionTime = time;
            health -= 10;
            anim.SetBool("Hit", true);
        }

        else if (collision.gameObject.tag == "FirstFloorEntrance")
        {
            Debug.Log("Entered First Floor");
            Level1Controller.maxFloorReached = Mathf.Max(Level1Controller.maxFloorReached, 1);
        }
        else if (collision.gameObject.tag == "SecondFloorEntrance")
        {
            Debug.Log("Entered Second Floor");
            Level1Controller.maxFloorReached = Mathf.Max(Level1Controller.maxFloorReached, 2);
        }
        else if (collision.gameObject.tag == "Finish")
        {
            Debug.Log("Entered Second Floor");
            SceneManager.LoadScene("Ayman");
            // Saving khattab stats
            PlayerPrefs.SetInt("level" , level) ;
            PlayerPrefs.SetInt("XP" , XP) ;
            PlayerPrefs.SetInt("skillPoints" , skillPoints) ;
            PlayerPrefs.SetFloat("walkSpeed" , walkSpeed) ;
            PlayerPrefs.SetFloat("runSpeed" , runSpeed) ;
            PlayerPrefs.SetInt("maxHealth",maxHealth) ;

        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "MinionsArm")
        {
            GetComponent<PlayerAudio>().PlayHit();
            anim.SetBool("Hit", false);
        }
    }

    void Set()
    {
        grounded = true;
        canRage = true;
        canDoubleJump = false;
        sword.SetActive(true);
        axe.SetActive(false);
    }

    void Get()
    {
        anim = GetComponent<Animator>();
        khattabHealthImage = GameController.findChildWithTag(this.gameObject, "KhattabHealth").GetComponent<Image>();
        khattabRageImage = GameController.findChildWithTag(this.gameObject, "KhattabRage").GetComponent<Image>();
        khattabXPImage = GameController.findChildWithTag(this.gameObject, "KhattabXP").GetComponent<Image>();
        khattabLevelText = GameController.findChildWithTag(this.gameObject, "KhattabLevel").GetComponent<Text>();
        khattabSkillPointsText = GameController.findChildWithTag(this.gameObject, "KhattabSkillPoints").GetComponent<Text>();
        sword = GameController.findChildWithTag(this.gameObject, "Sword");
        axe = GameController.findChildWithTag(this.gameObject, "Axe");
    }

    void Animations()
    {
        if (!paused) { 
        sword.GetComponent<MeleeWeaponTrail>().Emit = isRaging;
        axe.GetComponent<MeleeWeaponTrail>().Emit = isRaging;

        // DIE
        if (health <= 0)
        {
            GetComponent<PlayerAudio>().PlayDying();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                GameOverMenu.active = true;
            anim.SetBool("Die", true);
            return;
        }
        transform.eulerAngles = new Vector3(camera_.transform.eulerAngles.x, camera_.transform.eulerAngles.y, camera_.transform.eulerAngles.z);
        bool shift_down = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        anim.SetBool("Rage", isRaging && (time - timeStartRaging <= 1));


        // ATTACK
        anim.SetBool("LightAttack", Input.GetMouseButtonDown(0));
        anim.SetBool("HeavyAttack", Input.GetMouseButtonDown(1));



        bool forward = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool backward = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        // FORWARD

        anim.SetBool("Running", forward && shift_down);
        anim.SetBool("Walking", forward);


        if (forward &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("LightAttack") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Rage") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Hit") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Die") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Block"))
        {
            transform.Translate(Vector3.forward * (shift_down && !left && !right ? runSpeed : walkSpeed));
            GetComponent<PlayerAudio>().PlayWalking();

        }


        // BACKWARD
        anim.SetBool("WalkingBackward", backward);
        if (backward &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("LightAttack") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Rage") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Hit") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Die") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Block"))
        {
            GetComponent<PlayerAudio>().PlayWalking();

            transform.Translate(Vector3.back * (shift_down ? runSpeed : walkSpeed));
        }



        // JUMP
        anim.SetBool("Jumping", Input.GetKey(KeyCode.Space) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Jumping"));
        grounded = !anim.GetCurrentAnimatorStateInfo(0).IsName("Jumping");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                transform.Translate(Vector3.up * jumpForce);
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump)
                {
                    canDoubleJump = false;
                    //Debug.Log("Double Jumped");
                    transform.Translate(Vector3.up * jumpForce);
                }
            }
        }


        // Right
        anim.SetBool("WalkingRight", Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow));
        if (right &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("LightAttack") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Rage") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Hit") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Die") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Block"))

        {
            GetComponent<PlayerAudio>().PlayWalking();

            transform.Translate(Vector3.right * walkSpeed);
        }

        // LEFT
        anim.SetBool("WalkingLeft", Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow));
        if (left &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("LightAttack") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Rage") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Hit") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Die") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Block"))
        {
            GetComponent<PlayerAudio>().PlayWalking();

            transform.Translate(Vector3.left * walkSpeed);
        }

        // Block
        anim.SetBool("Block", Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl));


        // SWAP WEAPON
        if (Input.GetKeyDown(KeyCode.Z)) swapWeapon();
    }
    }

    void PlayerCanvas()
    {
        khattabHealthImage.fillAmount = Mathf.Max(0.0f, (1.0f * health) / 100);
        khattabRageImage.fillAmount = Mathf.Min(1, Mathf.Max(0.0f, (1.0f * rage) / 100));
        khattabXPImage.fillAmount = Mathf.Min(1, Mathf.Max(0.0f, (1.0f * XP) / XPTarget[Mathf.Min(2, level)]));
        khattabLevelText.text = (level + 1) + "";
        khattabSkillPointsText.text = skillPoints + "";

        if(level < 3 && XP >= XPTarget[level])
        {
            level = Mathf.Min(level + 1, 3);
            skillPoints++;
        }

        if (rage >= 100 && !isRaging && Input.GetKey(KeyCode.R))
        {
            isRaging = true;
            GetComponent<PlayerAudio>().PlayRage();
            rage = 0;
            timeStartRaging = time;
        }

        if (isRaging && time - timeStartRaging >= rageTime)
        {
            isRaging = false;
        }

    }

    public static void MinionDied()
    {
        XP += 50;
    }
}

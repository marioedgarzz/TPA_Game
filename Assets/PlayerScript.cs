using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerScript : MonoBehaviour {
    
    public float walkSpeed;
    public float runSpeed;
    public float speed;

    private int health;
    private int shield;

    public Text text;
    public Slider healthSlider;
    public GameObject image;

    Rigidbody rb;
    Animator animator;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        health = 100;
        shield = 0;
        healthSlider.value = health;
	}
	
    void Update()
    {
        if(animator.GetBool("isDie") == true)
        {
            return;
        }
        if(Input.GetButton("Sprint") && speed != runSpeed)
        {
            speed = runSpeed;
        }
        else if(speed != walkSpeed)
        {
            speed = walkSpeed;
        }
        if(Input.GetMouseButton(0))
        {
            animator.SetBool("isFiring", true);
        }
        else
        {
            animator.SetBool("isFiring", false);
        }
        if(Input.GetKey(KeyCode.P) || health <= 0)
        {
            animator.SetBool("isDie", true);
            text.enabled = true;
            StartCoroutine(delay());
        }
        else if(Input.GetKey(KeyCode.O) && !isDamaging)
        {
            if (shield > 0) shield -= 10;
            else {
                StartCoroutine(minusHealth(10));
            }
            Debug.Log("Shield : " + shield + "Health : " + health);
        }
        else if(Input.GetKey(KeyCode.I))
        {
            image.SetActive(true);
            StartCoroutine(delay2());
        }
        
    }

    bool isDamaging;

    private IEnumerator delay2()
    {
        yield return new WaitForSeconds(5);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    private IEnumerator minusHealth(int minus)
    {
        isDamaging = true;
        yield return new WaitForSeconds(0.5f);
        health -= minus;
        healthSlider.value = health;
        isDamaging = false;
    }

    private IEnumerator delay()
    {
        yield return new WaitForSeconds(3);
        text.enabled = false;
        animator.SetBool("isDie", false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

	// Update is called once per frame
	void FixedUpdate()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        Debug.Log(x + " " + z);
        if (x == 0 && z == 0)
        {
            Debug.Log("False");
            animator.SetBool("isMoving", false);
        }
        else
        {
            Debug.Log("aaa");
            rb.MovePosition(transform.position + transform.TransformDirection(x, 0, z) * Time.deltaTime * speed);
            animator.SetBool("isMoving", true);
        }
    }

    public void gotShield()
    {
        shield = 100;
    }
}

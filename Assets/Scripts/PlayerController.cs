using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    private float speed = 4f;
    private float jumpPower = 6f;
    private bool isAlive = true;
    
    public bool isGameStart = false;
    public int highScore;
        
    [SerializeField] int crystal;
    [SerializeField] Transform groundChecker;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    [SerializeField] LayerMask groundLayer;
    
    private SoundManager _soundManager;
    private UIManager _uiManager;
    
    private void Awake()
    {
        _soundManager = FindObjectOfType<SoundManager>();
        _uiManager = FindObjectOfType<UIManager>();
    }
    private void Start()
    {
        _soundManager.PlayBackground();
        highScore = PlayerPrefs.GetInt("High Score", 0);
        if (_uiManager.startPanel)
        {
           _uiManager.StartPanel();
        }
        crystal = PlayerPrefs.GetInt("Crystal", 0);
        _uiManager.crystalTxt.text = crystal.ToString();
    }
    private void Update()
    {
        if(isAlive && isGameStart)
        {
            MoveInput();
            Jump();
            Flip();
        }
        
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void MoveInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal != 0)
        {
            anim.SetBool("isWalk",true);
        }
        else
        {
            anim.SetBool("isWalk",false);
        }
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && CheckGrounding())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            anim.SetBool("isJump", true);
        }
        else if (!CheckGrounding())
        {
            anim.SetBool("isJump", false);
        }
    }
    private void Move()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    private void Flip()
    {
        Vector3 playerScale = transform.localScale;
        if (horizontal > 0)
        {
            playerScale.x = 1;
        }
        else if (horizontal < 0)
        {
            playerScale.x = -1;
        }
        transform.localScale = playerScale;
    }
    private bool CheckGrounding()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(groundChecker.position, Vector2.down, 0.1f, groundLayer);
        return hit;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Crystal"))
        {
            _soundManager.PlayCrystalCollect();
            crystal++;
            other.gameObject.SetActive(false);
            _uiManager.crystalTxt.text = crystal.ToString();
        }

        if (other.CompareTag("Door"))
        {
            PlayerPrefs.SetInt("Crystal",crystal);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Fall"))
        {
            _uiManager.restartPanel.SetActive(true);
            _uiManager.crystalEndTxt.text = crystal.ToString();
            if (crystal > highScore)
            {
                PlayerPrefs.SetInt("High Score", crystal);
            }
            isAlive = false;
            Time.timeScale = 0;
        }
    }
}
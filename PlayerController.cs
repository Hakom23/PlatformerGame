using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer;
using System;
using TMPro;

namespace Platformer
{
    public class PlayerController : CustomBehavior
    {
        
        private PlayerData _data;
        public PlayerData Data { get { return _data; } }



        private PlayerAgent _agent;
        [SerializeField]
        private Rigidbody2D _rigidBody;

        [SerializeField]
        private Transform _groundCheck;

        [SerializeField]
        private bool _isGrounded;
        private bool _isPaused;
        [SerializeField]
        private AudioSource _audioSource;
        [SerializeField]
        private AudioClip _pickUpClip;
        private void Start()
        {
            _data = GetComponent<PlayerData>();
            _agent = GetComponent<PlayerAgent>();
            if (_rigidBody == null)
            {
                _rigidBody = GetComponent<Rigidbody2D>();
            }

            if (_audioSource == null)
            {

                _audioSource = GetComponent<AudioSource>();
            }

            if (_groundCheck == null)
            {
                _groundCheck = transform.Find("TouchGround");
            }
            _isPaused = false;
        }

        public override void Init(GameManager gameManager)
        {
           base.Init(gameManager);
            _gameManager.OnLevelStarted += StartNewLevel;
            _gameManager.OnLevelCompleted += LevelFinished;
        }

      
        private void OnDestroy()
        {
            _gameManager.OnLevelStarted += StartNewLevel;
            _gameManager.OnLevelCompleted += LevelFinished;
        }
  private void LevelFinished()
        {
         _isPaused = true;
      }
        private void StartNewLevel()
        {
            _data.CoinsCollected = 0;
            transform.position = Vector3.zero;
            _agent.StopAnimations();
        _isPaused = false;
        }

        private void FixedUpdate()
        {
            if (_isPaused) return;
            
            int layerMask = LayerMask.GetMask("Floor");
            _isGrounded = Physics2D.OverlapPoint(_groundCheck.position, layerMask);

            float moveX = Input.GetAxis("Horizontal");


            if (_isGrounded && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _data.JumpSpeed);
                _isGrounded = false;
                _agent.Jump();

            }
            Vector2 newVelocity = new Vector2(moveX * _data.MoveSpeed, _rigidBody.velocity.y);
            _rigidBody.velocity = newVelocity;
            _agent.Move(_rigidBody.velocity);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Coin")
            {
                _data.CoinsCollected += collision.GetComponent<PickUp>().GetPickUp();
                _gameManager.Audio.PlaySound(_pickUpClip);
            }
            else if (collision.tag == "Finish")
            {
                _gameManager.CheckIfLevelEnded();
            }
        }
        public void StopAnimations()
        { 
        }

    }
}
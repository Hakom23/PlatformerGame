using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer
{
    public class PlayerAgent : MonoBehaviour
    {
        [SerializeField]
        public Animator _animator;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        private void Start()
        {
            if (_spriteRenderer == null)
            { _spriteRenderer = GetComponentInChildren<SpriteRenderer>(); }


            if (_animator == null)
            { _animator = GetComponent<Animator>(); }
        }
      public void Jump()
        {
            _animator.SetTrigger("Jump");
        }
        public void Move(Vector2 velocity)
        {

            if (velocity.x < 0f)
                _spriteRenderer.flipX = true;
            else if (velocity.x > 0f)
                _spriteRenderer.flipX = false;
            if (velocity.y < 0f)
            {
                _animator.SetBool("isFalling", true);
            }
            else
                _animator.SetBool("isFalling", false);
            if (velocity.x != 0f)
                _animator.SetBool("isWalking", true);
            else
                _animator.SetBool("isWalking", false);
        }

        public void StopAnimations()
        {
             _animator.SetBool("isWalking", false);
            _animator.SetBool("isFalling", false);
        }
        
            
       
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MoveController : MonoBehaviour
{
    [SerializeField] 
    private float runSpeed = 10;
    
    [SerializeField] 
    private float rotationalSpeed;
    
    [SerializeField] 
    private new CapsuleCollider collider;
    
    [SerializeField] 
    private Rigidbody rb;
    
    [SerializeField]
    private FloatingJoystick floatingJoystick;

    public Animator animator;
    private PlayerStates _currentState;

    public bool _moveManually=true;

    private bool _onRotate = false;

    // Start is called before the first frame update
    void Start()
    {
        _currentState = PlayerStates.Playing;
        
    }
    
    void FixedUpdate()
    {
        if (_currentState==PlayerStates.Playing&&_moveManually)
        {
            var lookDirection = new Vector3(floatingJoystick.Horizontal, 0, floatingJoystick.Vertical);
            var lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            transform.rotation = lookRotation;
            transform.DORotateQuaternion(lookRotation, 0.1f);
            Move();
        }
        
    }

    private void StartHitAnimation(Vector3 contactPoint)
    {
        
        if(contactPoint.x < -0.5f) //right
        {
            animator.SetTrigger(PlayerAnims.HitRight.ToString());
        }
        else if(contactPoint.x > 0.5f) //left
        {
            animator.SetTrigger(PlayerAnims.HitLeft.ToString());
        }
        else//front
        {
            animator.SetTrigger(PlayerAnims.HitHead.ToString());
            collider.direction = 2;
            collider.center = new Vector3(0, 0.80f, 0);
            
        }
        StartCoroutine(Respawner(2.0f));
    
        
    }
    
    
    private void Move()
    {
        if (floatingJoystick.Direction != Vector2.zero)
        {
            var movementVector = transform.forward * runSpeed;
            var velocity = rb.velocity;
            var requiredVelocity = movementVector - velocity;
            requiredVelocity.y = 0;
            rb.AddForce(requiredVelocity, ForceMode.VelocityChange);

            animator.SetBool(PlayerAnims.RunForward.ToString(), true);
        }
        else
        {
            if (!_onRotate)
            {
                var velocity = rb.velocity;
                velocity.x = 0;
                velocity.z = 0;
                rb.velocity = velocity;
            }
            

            animator.SetBool(PlayerAnims.RunForward.ToString(), false);
        }

        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            _currentState = PlayerStates.HitChain;
            StartHitAnimation(collision.GetContact(0).normal);
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            _onRotate = true;
            transform.parent = collision.gameObject.transform;
        }
        if (collision.gameObject.CompareTag("Finish") && _moveManually)
        {
            GameManager.instance.gameState = GameStates.Painting;
            _currentState = PlayerStates.LevelEnd;
            floatingJoystick.enabled = false;
            var velocity = rb.velocity;
            velocity.x = 0;
            velocity.z = 0;
            rb.velocity = velocity;
            transform.DOMove(GameManager.instance.finishLine.position,4);
            animator.SetBool(PlayerAnims.RunForward.ToString(),false);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            _onRotate = false;
            transform.parent = null;
        }
    }

    private IEnumerator  Respawner(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Respawn();

    }
    private void Respawn(){
        if (_moveManually)
        {
            animator.SetBool(PlayerAnims.RunForward.ToString(),false);
        }
        animator.ResetTrigger(PlayerAnims.HitHead.ToString());
        animator.ResetTrigger(PlayerAnims.HitLeft.ToString());
        animator.ResetTrigger(PlayerAnims.HitRight.ToString());
        transform.position = new Vector3(0, 3, 1);
        collider.direction = 1;
        collider.center = new Vector3(0, 1.2f, 0);
        _currentState = PlayerStates.Playing;
        transform.rotation=Quaternion.identity;
        
    }


}

public enum PlayerStates
{
    Playing,
    HitChain,
    LevelEnd
}

public enum PlayerAnims
{
    RunForward,
    RunVertical,
    HitHead,
    HitRight,
    HitLeft,
    Falling
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
    //This Script is designed to control all players actions.
{   //This section contains all the global variable used within this script    
    private Rigidbody2D rb;
    private Animator _animator;
    private CircleCollider2D  _hitbox;
    public float speed;
    public bool _isGrounded;
    private Vector2 jump;
    public float _jumpForce;
    public Transform AttackPoint; //Point of attack set according the size of the sprite;
    public float AttackRange=0.4f; //Size of the attack set according the size of the sprite;
    public LayerMask EnemyLayers;

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        _animator=GetComponent<Animator>();
        _hitbox=GetComponent<CircleCollider2D>();
         jump = new Vector2(0f,1f);
            
        
    }

    // Update is called once per frame
    void Update()
    {
       
          Attacking();
    }

    void FixedUpdate(){
             PlayerMovement();
                Jumping();
        

    }

    void LateUpdate()
    {
      AnimationStateMachine();
      

    }

    void PlayerMovement()
    //This Method controls the horizontal movemento of the player
    {
            rb.velocity= new Vector2(Input.GetAxis("Horizontal")*speed*Time.deltaTime, rb.velocity.y);

    }

    void Jumping()
    
    {
            //This section controls the force add to the Vertical component so the player can jump
            if(Input.GetKeyDown(KeyCode.Space) && _isGrounded){
                
                   _animator.SetBool("Grounded",false);
                   _animator.SetBool("Idle",false);
                   _animator.SetBool("Run",false);
                 
            rb.AddForce(jump * _jumpForce, ForceMode2D.Impulse);
            _isGrounded = false;
            
                    }      

                                                                                

    }

    void Attacking()
    {

        if(Input.GetKeyDown(KeyCode.Z)&&rb.velocity.y==0)
        {
                _animator.SetTrigger("Attack");
             
                    //this section is intended to apply damage on the enemies.


              Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);
                
               foreach(Collider2D enemy in HitEnemies){

                Debug.Log("I hit " + enemy.name);
                enemy.GetComponent<SamuraiEnemy>()._animator.SetTrigger("HitTaken");
                enemy.GetComponent<SamuraiEnemy>()._bloodStream.Play();
               }

        }
    }

    void OnDrawGizmosSelected(){

            Gizmos.DrawWireSphere(AttackPoint.position,AttackRange);
    }

    void AnimationStateMachine()
        //This Method controls the Boolean States of the Animator based on the player's actions.
    {           _animator.ResetTrigger("Attack");
            if(Input.GetAxis("Horizontal")>0&&_animator.GetBool("Attack1")==false&&_isGrounded){
                 
                _animator.SetBool("Run",true);
                _animator.SetBool("Idle",false);
              transform.rotation = Quaternion.Euler(0,0,0);  
            }
             if(Input.GetAxis("Horizontal")>0&&_animator.GetBool("Attack1")==true&&_isGrounded){

                _animator.SetBool("Run",false);
                _animator.SetBool("Idle",false);
              transform.rotation = Quaternion.Euler(0,0,0);  
            }
             if(Input.GetAxis("Horizontal")<0&&_animator.GetBool("Attack1")==false&&_isGrounded){
                  
                _animator.SetBool("Run",true);
                _animator.SetBool("Idle",false);
             transform.rotation = Quaternion.Euler(0,180,0);  
            }
             if(Input.GetAxis("Horizontal")<0&&_animator.GetBool("Attack1")==true&&_isGrounded){

                _animator.SetBool("Run",false);
                _animator.SetBool("Idle",false);
             transform.rotation = Quaternion.Euler(0,180,0);  
            }

            else if (Input.GetAxis("Horizontal")==0&&_animator.GetBool("Attack1")==false&&_isGrounded)
            {
                
                 _animator.SetBool("Run",false);
                _animator.SetBool("Idle",true);
            }
         

            if(rb.velocity.y<0){

                  _animator.SetBool("Fall",true);
            }
            else if(rb.velocity.y==0) {
                  _animator.SetBool("Fall",false);
                  _animator.SetBool("Grounded",true);
            }

    }

    void OnCollisionStay2D(Collision2D collision){

            if(collision.gameObject.tag=="Ground"){

                _isGrounded=true;
            }
    }
    void OnCollisionExit2D(Collision2D collision){

             if(collision.gameObject.tag=="Ground"){

                _isGrounded=false;
            }
        
    }

    void OnCollisionEnter2D(Collision2D collision){

              if(collision.gameObject.tag=="Ground"){

                _isGrounded=true;
            }

           

    }

    void OnTriggerEnter2D(Collider2D collider){

         
    }

    
}

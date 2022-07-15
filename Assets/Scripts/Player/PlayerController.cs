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
    public LayerMask GhostLayer;
    public LayerMask PlayerLayer;
    public int combo;
    public float _startTime, _DashTime;
    public bool _isDashing;
    public int _health;
    public HealthBarUI healthBar;

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        _animator=GetComponent<Animator>();
        _hitbox=GetComponent<CircleCollider2D>();
         jump = new Vector2(0f,1f);
         combo = 0;
         _health=100;
         healthBar.SetMaxhealth(_health);
            
        
    }

    // Update is called once per frame
    void Update()
    {
       
          Attacking();
          DashAttack();
          healthBar.SetHealth(_health);
          Jumping();
    }

    void FixedUpdate(){
             PlayerMovement();
                
        

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

    void DashAttack()
    { //This method enables a Dash and also, by changing the layer of the player, creates a ghost effect for 2seconds during which the player is invulnerable
            if(Input.GetKeyDown(KeyCode.E) && _isGrounded&&_isDashing==false){

                _animator.SetTrigger("Dash");
                this.gameObject.layer=8;
                
                _isDashing=true;
             

                rb.MovePosition(new Vector2 (transform.position.x+(Input.GetAxis("Horizontal")*1.8f), transform.position.y));
                
            }
            if(_isDashing==true){

                _DashTime+=Time.deltaTime;

                    if(_DashTime>=2f){
                          this.gameObject.layer=6;
                        _isDashing=false;
                        _DashTime=0;
                    }
            }




    }

    void Jumping()
    
    {
            //This section controls the force add to the Vertical component so the player can jump
            if(Input.GetKeyDown(KeyCode.Space) && _isGrounded==true){
                
                   _animator.SetBool("Grounded",false);
                   _animator.SetBool("Idle",false);
                   _animator.SetBool("Run",false);
                 
            rb.AddForce(jump * _jumpForce, ForceMode2D.Impulse);
            _isGrounded = false;
            
                    }      

                                                                                

    }

    void Attacking()
    {

        if(Input.GetKeyDown(KeyCode.R)&&rb.velocity.y==0)
        {
               
             _animator.SetInteger("Combo",combo);
                if(combo<3){

                    combo=combo+1;
                }
                if(combo==3){

                    combo=0;
                }
                   _animator.SetTrigger("Attack");
             
                    //this section is intended to apply damage on the enemies.

                //Through this function the GameObject is able to detect all the colliders within the Layer "Enemies".
              Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);
                
               foreach(Collider2D enemy in HitEnemies){
                

                if(enemy.tag=="Enemy"){

                         Debug.Log("I hit " + enemy.name);
                enemy.GetComponent<SamuraiEnemy>()._hitTaken=true;
                enemy.GetComponent<SamuraiEnemy>()._animator.SetTrigger("HitTaken");
               
                
                if(combo==2){
                enemy.GetComponent<SamuraiEnemy>()._health-=20;

                }
                else enemy.GetComponent<SamuraiEnemy>()._health-=10;
                }
                 if(enemy.tag=="Boss"){

                         Debug.Log("I hit " + enemy.name);
                enemy.GetComponent<BossController>()._hitTaken=true;
                enemy.GetComponent<BossController>()._animator.SetTrigger("HitTaken");
               
                
                if(combo==2){
                enemy.GetComponent<BossController>()._health-=20;

                }
                else enemy.GetComponent<BossController>()._health-=10;
                }


               
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
                  _animator.SetBool("Run",false);
                  _animator.SetBool("Grounded",false);
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
                _animator.SetBool("Grounded",true);
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

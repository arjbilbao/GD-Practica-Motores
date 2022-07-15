using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{       
    public GameObject Bar;
    public Animator _animator;
    public GameObject Aura;
    public CircleCollider2D Halo;
    public bool _blood;
    private Rigidbody2D rb;
    public GameObject Player, Set;
    public bool _playerSeen;
    public float speed;
    public Transform AttackPoint;
    public float AttackRange;
    public LayerMask PlayerLayers;
    public float startTime, _attackStartTime,_hitTimer, _attackTimer;
    public bool _hitTaken, _hitPlayer;
     public bool starting, _attackStarting;
     public int _health,_health1, _health2, _health3;
     public bool Death=false;
     public int _state;
    public bool state1, state2, state3;
     public float center;
     public bool _isGrounded, jumping;
      private Vector2 jump;
    public float _jumpForce;
    public int rand;
    private Vector2 move;
    private Vector2 move2;
    public float battleTimer;

    // Start is called before the first frame update
    void Start()
    {
        _animator=GetComponent<Animator>();
        rb=GetComponent<Rigidbody2D>();
       Halo.enabled=true;
        _blood=true;
        _hitTimer=0;
        _attackTimer=0;
         starting=true;
         _attackStarting=true;
         _health=300;
         _health1=100;
         _health2=100;
         _health3=100;
         _hitPlayer=false;
         _state=0;
         state1=true;
         state2=true;
         state3=true;
         Aura.SetActive(true);
           jump = new Vector2(0f,1f);
           _isGrounded=true;
           jumping=false;
           rand=0;

           _animator.SetBool("Grounded", true);
           
      
   
    }

    // Update is called once per frame
    void Update()
    {            center=transform.rotation.y;
                  
                       
                        Bar.transform.rotation = Quaternion.Euler (0.0f, transform.rotation.y*(-1.0f), 0.0f); 
                        
                    
       if(rb.velocity.y<0){

        _animator.SetBool("Fall",true);

       }
       
     
                
    // Boss Health State Machine
    // Each time a Health Bar is taken out from the Boss, it enters into a Meditation State for 5 Seconds.       
         
    if(_health>=200)
    {

        _health3=_health-200;
    }
         
    if(_health>=100&&_health<200)
    {               
                if(state2==true){

                    _animator.SetTrigger("Meditation");
                    state2=false;
                    state3=true;
                    state1=true;
                      
                }
           if(state1==true)  {
                            //Meditation Timer
             battleTimer+=Time.deltaTime;
                    if(battleTimer>5f){

                        state1=false;
                        _animator.SetTrigger("Fight");
                        battleTimer=0f;
                    }
           }   


        _health3=0;
        _health2=_health-100;
    }
     if(_health>=0&&_health<100)
    {         if(state3==true){

                    _animator.SetTrigger("Meditation");
                    state3=false;
                    state1=true;
              
                }

                 if(state1==true)  {
                             //Meditation Timer
             battleTimer+=Time.deltaTime;
                    if(battleTimer>5f){

                        state1=false;
                        _animator.SetTrigger("Fight");
                        battleTimer=0f;
                    }
                     }

                

        _health3=0;
        _health2=0;
        _health1=_health;
    }
      




    }

    void Jumping()
    
    {
            //This section controls the force add to the Vertical component so the player can jump
            if(_isGrounded==true&&jumping==true){
                
              rb.AddForce(jump * _jumpForce, ForceMode2D.Impulse);
                                       move2= new Vector2 (0f, rb.velocity.y);
                                        _animator.SetBool("Grounded",false);
                                    _isGrounded=false;
                                    
            
                    }      

                                                                                

    }

    void AttackTimer()
    {
                //This Method is called for counting the time between a hit and another.
        if(_hitPlayer==true){
               

                if( _attackTimer==0f&&_attackStarting==true){

                    _attackStartTime=Time.time;
                    _attackStarting=false;
                    
                    
                }

                

                if(_attackTimer<2f&& _attackStartTime>0){
        
                        _attackTimer+= Time.deltaTime;
                }
                else if(_attackTimer>=2f){

                    _attackTimer=0;
                    _attackStartTime=0;
                    _hitPlayer=false;
                    _attackStarting=true;
                }




    }

    }

    void HitTakenTimer()
    {       //This Method is called for counting the time between a hit received and a new attack trigger.
        
        if(_hitTaken==true){
               

                if( _hitTimer==0f&&starting==true){

                    startTime=Time.time;
                    starting=false;
                    
                    
                }

                

                if(_hitTimer<1.6f&&startTime>0){
        
                        _hitTimer+= Time.deltaTime;
                }
                else if(_hitTimer>=1.6f){

                    _hitTimer=0;
                    startTime=0;
                    _hitTaken=false;
                    starting=true;
                }




    }
    }

    void FixedUpdate(){
      
        
                      if(_health>0){
                    Chasing();

            }
        //This Conditions is set when the Boss is defeated
            if(_health<=0&&Death==false){
                _animator.SetTrigger("Death");
                Death=true;
                this.gameObject.layer=8;
                Destroy(this.gameObject,2);
                
          
             _blood=false;
            } 
    }

    void Attacking(){
                //This seccion is intended to apply damage
            Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, PlayerLayers);
                
               foreach(Collider2D enemy in HitEnemies){

                Debug.Log("I hit the hero");
               if(_attackTimer==0&&_hitPlayer==false){

                     enemy.GetComponent<PlayerController>()._health-=20;
                     
               }
               
                
               }
              

    }

     void OnDrawGizmosSelected(){
                    //To Visualize the hitbox
            Gizmos.DrawWireSphere(AttackPoint.position,AttackRange);
    }

            void Chasing()
            {
                    //This sections controls the Samurai movement when sees the player.
                    float distance = Player.transform.position.x - Set.transform.position.x;
                    float heigh= Player.transform.position.y - Set.transform.position.y;
                    
                     //this conditional detects the player when he's within a distance of 2Unities and on plain sight.
                     if(Mathf.Abs(distance)<4&&Mathf.Abs(heigh)<0.6f&&state1==false){

                        _playerSeen=true;
                        
                    }
                    if(_hitTaken==false){
                     
                
                    if(_playerSeen&&distance>0.6f){
                               Vector2 samuraiPlace = new Vector2(transform.position.x,transform.position.y);

                               
                                if(_isGrounded==true&&rand<=5&&jumping==false){

                                   //This secction is called randomly to move the Boss with a jumping force.
                                     
                                     jumping=true;
                                     Jumping();
                                         
                                }
                                  if(jumping&&_isGrounded==false){

                                       
                                    move = new Vector2 (2f, 0);
                                    
                      
                          rb.MovePosition(samuraiPlace+ new Vector2(move.x *speed*Time.deltaTime, rb.velocity.y*Time.deltaTime));
                                }
                                     
                            transform.rotation = Quaternion.Euler(0,0,0);
                                if(_isGrounded==true&&jumping==false){

                                         _animator.SetBool("Run", true);
                                         
                                            move2 = new Vector2(0f,0f);

                                            move = new Vector2 (1f, 0f);
                         
                                        rb.MovePosition(samuraiPlace+(move*speed*Time.deltaTime));
                                }

                            
                     
                           
                             
                            
                            
                    }
                     if(_playerSeen&&distance<-0.6f){
                                Vector2 samuraiPlace = new Vector2(transform.position.x,transform.position.y);
                                transform.rotation = Quaternion.Euler(0,180,0);



                                 if(_isGrounded==true&&rand<=5&&jumping==false){

                                   
                                  //This secction is called randomly to move the Boss with a jumping force.
                                    

                                         jumping=true;
                                         Jumping();
                                      
                                  
                                   
                                           
                                }
                                if(jumping&&_isGrounded==false){

                                        
                            move = new Vector2 (-2f, 0);
                          
                             rb.MovePosition(samuraiPlace+ new Vector2(move.x *speed*Time.deltaTime, rb.velocity.y*Time.deltaTime));
                        
                                }
                            
                              if(_isGrounded==true&&jumping==false){

                                         _animator.SetBool("Run", true);
                                      
                                        move2 = new Vector2(0f,0f);

                                             
                                                move = new Vector2 (-1f, 0f);
                   
                                            rb.MovePosition(samuraiPlace+(move*speed*Time.deltaTime));
                                }

                               
                           
                       

                         
                            
                    }
                    }
                    //Once the Samurai reaches the player within the attack range, he attacks.
                    if(_playerSeen&&Mathf.Abs(distance)<0.6&&_isGrounded){

                            _animator.SetBool("Run", false);
                           
                            if(_hitTimer==0&&_attackTimer==0&&_hitTaken==false&&jumping==false){
                                     
                                if(state1==false){
                                    
                                _animator.SetInteger("Type",Random.Range(0,4)); //This random number changes the type of attack animation the Boss Will execute.
                                _animator.SetTrigger("Attack");
                               Attacking();
                               rand = Random.Range(0,9); //This random number allows the boss changing from running to jumping.
                                _hitPlayer=true;
                                }
                            
                                
                            }
                         
                    }
                   
                    if(Mathf.Abs(distance)>4f||Mathf.Abs(heigh)>0.6f){

                        _playerSeen=false;
                         _animator.SetBool("Run", false);
                         jumping=false;
                    }
                     HitTakenTimer();
                     AttackTimer();
                     
                      


                  
                    

                   

            }

            void OnCollisionStay2D(Collision2D collision){

                if(collision.gameObject.tag=="Ground"){

                    _isGrounded=true;
                    _animator.SetBool("Grounded",true);
                }
            }

             void OnCollisionEnter2D(Collision2D collision){

                if(collision.gameObject.tag=="Ground"){

                    _isGrounded=true;
                    jumping=false;
                    _animator.SetBool("Grounded",true);
                    _animator.SetBool("Fall",false);
                    rand=6;
                }
            }
             void OnCollisionExit2D(Collision2D collision){

                if(collision.gameObject.tag=="Ground"){

                    _isGrounded=false;
                    _animator.SetBool("Grounded",false);
                     rand = Random.Range(0,9);
                }
            }

            void OnDestroy(){

                Player.GetComponent<PlayerController>().GameOver=true;
            }


        
}

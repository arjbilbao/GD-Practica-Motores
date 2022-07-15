using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{       

    public Animator _animator;
    public GameObject Aura;
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
     private bool state1, state2, state3;
    // Start is called before the first frame update
    void Start()
    {
        _animator=GetComponent<Animator>();
        rb=GetComponent<Rigidbody2D>();
       
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
         state1=false;
         state2=true;
         state3=false;
      
   
    }

    // Update is called once per frame
    void Update()
    {
            
         
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
                        Aura.SetActive(true);
                }


        _health3=0;
        _health2=_health-100;
    }
     if(_health>=0&&_health<100)
    {         if(state3==true){

                    _animator.SetTrigger("Meditation");
                    state3=false;
              
                }

        _health3=0;
        _health2=0;
        _health1=_health;
    }
      




    }

    void AttackTimer()
    {

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
    {       
        
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

            if(_health<=0&&Death==false){
                _animator.SetTrigger("Death");
                Death=true;
                this.gameObject.layer=8;
                Destroy(this.gameObject,10);
                
          
             _blood=false;
            }
        
    }

    void Attacking(){
                //This seccion is intended to apply damage
            Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, PlayerLayers);
                
               foreach(Collider2D enemy in HitEnemies){

                Debug.Log("I hit the hero");
               if(_attackTimer==0&&_hitPlayer==false){

                     enemy.GetComponent<PlayerController>()._health-=10;
                     
               }
               
                
               }
              

    }

     void OnDrawGizmosSelected(){

            Gizmos.DrawWireSphere(AttackPoint.position,AttackRange);
    }

            void Chasing()
            {
                    //This sections controls the Samurai movement when sees the player.
                    float distance = Player.transform.position.x - Set.transform.position.x;
                    float heigh= Player.transform.position.y - Set.transform.position.y;
                     //this conditional detects the player when he's within a distance of 2Unities and on plain sight.
                     if(Mathf.Abs(distance)<2&&Mathf.Abs(heigh)<0.6f&&state1==false){

                        _playerSeen=true;
                        
                    }
                    if(_hitTaken==false){
                     
                
                    if(_playerSeen&&distance>0.6f){
                            _animator.SetBool("Run", true);
                            transform.rotation = Quaternion.Euler(0,0,0);
                       Vector2 move = new Vector2 (1f, 0f);
                        Vector2 samuraiPlace = new Vector2(transform.position.x,transform.position.y);
                            rb.MovePosition(samuraiPlace+move*speed*Time.deltaTime);
                           
                    }
                     if(_playerSeen&&distance<-0.6f){
                            _animator.SetBool("Run", true);
                            transform.rotation = Quaternion.Euler(0,180,0);
                       Vector2 move = new Vector2 (-1f, 0f);
                        Vector2 samuraiPlace = new Vector2(transform.position.x,transform.position.y);
                            rb.MovePosition(samuraiPlace+move*speed*Time.deltaTime);
                            
                            
                    }
                    }
                    //Once the Samurai reaches the player within the attack range, he attacks.
                    if(_playerSeen&&Mathf.Abs(distance)<0.6){

                            _animator.SetBool("Run", false);
                           
                            if(_hitTimer==0&&_attackTimer==0&&_hitTaken==false){
                                     
                                if(state1==false){

                                _animator.SetTrigger("Attack");
                               Attacking();
                                _hitPlayer=true;
                                }
                            
                                
                            }
                         
                    }
                   
                    if(Mathf.Abs(distance)>2f||Mathf.Abs(heigh)>0.6f){

                        _playerSeen=false;
                         _animator.SetBool("Run", false);
                    }
                     HitTakenTimer();
                     AttackTimer();
                     
                      


                  
                    

                   

            }

        
}

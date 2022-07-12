using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiEnemy : MonoBehaviour
{       

    public Animator _animator;
    public ParticleSystem _bloodStream;
    public bool _blood;
    private Rigidbody2D rb;
    public GameObject PatrolArea;
    public GameObject Player;
    public bool _playerSeen;
    public float speed;
    public Transform AttackPoint;
    public float AttackRange;
    public LayerMask PlayerLayers;
    public float startTime, _hitTimer;
    public bool _hitTaken;
     public bool starting;
     public int _health;
     public bool Death=false;
    // Start is called before the first frame update
    void Start()
    {
        _animator=GetComponent<Animator>();
        rb=GetComponent<Rigidbody2D>();
        _bloodStream.Stop();
        _blood=true;
        _hitTimer=0;
         starting=true;
         _health=100;
   
    }

    // Update is called once per frame
    void Update()
    {
            
         
      //This section enables and disables the Particle System for the blooad stream
        if(_animator.GetCurrentAnimatorStateInfo(0).IsTag("TakeHit"))
        {

             _blood=true;
        }

         if(_animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle")&&_blood==true)
        {

             _bloodStream.Stop();
             _blood=false;
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
                
             _bloodStream.Stop();
             _blood=false;
            }
        
    }

    void Attacking(){
                //This seccion is intended to apply damage
            Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, PlayerLayers);
                
               foreach(Collider2D enemy in HitEnemies){

                Debug.Log("I hit the hero");
                
               }

    }

     void OnDrawGizmosSelected(){

            Gizmos.DrawWireSphere(AttackPoint.position,AttackRange);
    }

            void Chasing()
            {
                    //This sections controls the Samurai movement when sees the player.
                    float distance = Player.transform.position.x - transform.position.x;
                    float heigh= Player.transform.position.y - transform.position.y;
                     //this conditional detects the player when he's within a distance of 2Unities and on plain sight.
                     if(Mathf.Abs(distance)<2&&Mathf.Abs(heigh)<0.6f){

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
                    if(_playerSeen&&((distance<=0.6f&&distance>=0.0f)|(distance>=-0.6f&&distance<=0.0f))){

                            _animator.SetBool("Run", false);
                           
                            if(_hitTimer==0&&_hitTaken==false){
                                     
                                
                                _animator.SetTrigger("Attack");
                                Attacking();
                            }
                         
                    }
                   
                    if(Mathf.Abs(distance)>2f||Mathf.Abs(heigh)>0.3f){

                        _playerSeen=false;
                         _animator.SetBool("Run", false);
                    }
                     HitTakenTimer();


                  
                    

                   

            }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{      
            public bool BattleStart;
            public Animator _bossAnimator;
            public BossController Boss;


        void Start()
        {

            BattleStart=false;
        }
        void Update(){

            if(BattleStart){
                    _bossAnimator.SetTrigger("Fight");
                    BattleStart=false;

            }
        }

        void OnTriggerExit2D(Collider2D other){

            if(other.tag=="Player"){

                BattleStart=true;
                Boss.state1=false;
            }
        }
        
    }


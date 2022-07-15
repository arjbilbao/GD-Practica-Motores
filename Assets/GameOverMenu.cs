using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{       public CanvasGroup GameOver;
        public PlayerController PlayerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.GameOver)
        {

            GameOver.alpha=1;
            GameOver.interactable=true;
        }
    }
}

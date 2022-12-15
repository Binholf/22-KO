using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Scenary
{
    public class Start_point : MonoBehaviour
    {
        protected Animator anima;

        // Start is called before the first frame update
        void Start()
        {
            anima = GetComponent<Animator>();
        }
        

        //Manipular a colisão com os players
       private void OnCollisionStay2D(Collision2D other) {
            if(other.gameObject.CompareTag("Player")){
                anima.SetBool("Ativado", true);
            }
      
        }

    }
}

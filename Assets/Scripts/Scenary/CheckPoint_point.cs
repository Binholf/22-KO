using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Players;
using Cinemachine;

namespace Assets.Scripts.Scenary
{
    public class CheckPoint_point : MonoBehaviour
    {
        protected Animator anima;
        //campo para ensirir o confinir do cinemachine
        [SerializeField]protected CinemachineConfiner2D confiner;
        //colider poligonal referente a area do castelo
        public PolygonCollider2D polyCastelo;

        // Start is called before the first frame update
        void Start()
        {
            anima = GetComponent<Animator>();
        }

        //Manipular a colisão com os players
        private void OnTriggerEnter2D(Collider2D other) {
            if(other.CompareTag("Player")){
                //passando o novo local de respawn do player
                FindObjectOfType<PlayerController>().respawn=this.gameObject.transform.GetChild(0);
                //ativar a porta do castelo, segundo filho do checkpoint, necessário ativar a animação
                this.gameObject.transform.GetChild(1).gameObject.GetComponent<Animator>().SetBool("Ativado", true);
                anima.SetBool("Ativado", true);
                //mudando o confinir para o referente ao castelo
                confiner.m_BoundingShape2D = polyCastelo;
            }
      
        }

    }
}

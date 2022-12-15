using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Assets.Scripts.Enemies;
using Assets.Scripts.Players;

namespace Assets.Scripts.Scenary
{
    public class End_point : MonoBehaviour
    {

        //local de spawn do boss e referencia para ele
        [SerializeField]protected Transform spawnBoss;
        [SerializeField]protected Enemy Boss;
        
        [SerializeField]private Transform respawnPlayer;

        private bool lancado=false;

        //campo para ensirir o confinir do cinemachine
        [SerializeField]protected CinemachineConfiner2D confiner;
        //colider poligonal referente a area do boss
        public PolygonCollider2D polyBoss;

        //Manipular a colisão com os players
        private void OnTriggerEnter2D(Collider2D other) {
            if(other.CompareTag("Player")){
                FindObjectOfType<PlayerController>().respawn=respawnPlayer;
                //mudando o confinir para a área do boss fight
                confiner.m_BoundingShape2D = polyBoss;
                if(!lancado){
                    Instantiate(Boss,spawnBoss.position, spawnBoss.rotation);
                    lancado=true;
                }
                this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
      
        }

    }
}

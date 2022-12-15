using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Players;
using Assets.Scripts.Dialogue;

namespace Assets.Scripts
{
    public class Mentor : Falador
    {
        [SerializeField]private Player jogador;

        //instanciar um novo player assim que sair do colisor do NPC
        private void OnTriggerExit2D(Collider2D other) {
            if(other.CompareTag("Player")){
                if(other.gameObject.name!="Bunny(Clone)"){
                    FindObjectOfType<PlayerController>().setPlayer(jogador);
                }
                
            }
        }
    }
}

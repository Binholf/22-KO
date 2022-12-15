using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Players;
using Assets.Scripts.Dialogue;

namespace Assets.Scripts
{
    public class PinkMan : Falador
    {
        //mecânica ao sair do trigger do personagem
        private void OnTriggerExit2D(Collider2D other) {
            if(other.CompareTag("Player")){
                //adiocionar umas frutas no contador
                FindObjectOfType<GameController>().fruits+=7;
                FindObjectOfType<GameController>().fruitsCount.text=FindObjectOfType<GameController>().fruits.ToString();
            }
        }
    }
}

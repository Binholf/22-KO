using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Players;

namespace Assets.Scripts.Fruits
{   
    public abstract class Fruit : MonoBehaviour
    {
        protected Animator animaF;

        protected void Start()
        {   
            animaF = GetComponent<Animator>();       
        }
        //Manipular a colisão das frutas
        protected void OnTriggerEnter2D(Collider2D other) {
            if(other.CompareTag("Player")){
                //adicionar fruta ao contador
                FindObjectOfType<PlayerController>().frutas+=1;
                animaF.SetBool("Coletada", true);
                Destroy(gameObject, 0.3f);
            }
        }
    }
}

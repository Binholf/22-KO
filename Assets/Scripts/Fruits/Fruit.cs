using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Players;
using UnityEngine.UI;

namespace Assets.Scripts.Fruits
{   
    public class Fruit : MonoBehaviour
    {
        protected Animator animaF;
        [SerializeField] protected int valor;

        protected void Start()
        {   
            animaF = GetComponent<Animator>();       
        }
        //Manipular a colisão das frutas
        protected void OnTriggerEnter2D(Collider2D other) {
            if(other.CompareTag("Player")){
                //adicionar fruta ao contador
                FindObjectOfType<GameController>().fruits+=valor;
                FindObjectOfType<GameController>().fruitsCount.text=FindObjectOfType<GameController>().fruits.ToString();
                animaF.SetBool("Coletada", true);
                Destroy(gameObject, 0.3f);
            }
        }
    }
}

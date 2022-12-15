using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {

        [SerializeField] protected float velocidade;
        protected Rigidbody2D rigid;
        protected SpriteRenderer sprite;
        protected Animator anima;
        protected BoxCollider2D boxC;
        protected CapsuleCollider2D capC;
        protected bool vivo;

        // garantir que o enimigo não morra antes de ser instânciado
        // private void Awake() {
        //     vivo=true;
        // }
        // Start is called before the first frame update
        void Start()
        {
            vivo=true;
            rigid = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
            anima = GetComponent<Animator>();
            boxC = GetComponent<BoxCollider2D>();
            capC = GetComponent<CapsuleCollider2D>();
        }

        public abstract void tomarDano();

    }       
}

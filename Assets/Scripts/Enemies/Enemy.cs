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

        // Start is called before the first frame update
        void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
            anima = GetComponent<Animator>();
            boxC = GetComponent<BoxCollider2D>();
        }

    }       
}

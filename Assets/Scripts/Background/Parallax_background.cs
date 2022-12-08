using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Background
{
    public class Parallax_background : MonoBehaviour
    {
        private Vector3 posInicial;
        public float limite;
        public float velocidadeCenario;

        // Start is called before the first frame update
        void Start()
        {
            posInicial = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //vai descendo o background(eixo Y) com a velocidade especificada 
            transform.position = new Vector3(transform.position.x, transform.position.y-velocidadeCenario, transform.position.z);

            //se chegar em uma posição do eixo Y limite, volta para a posição inicial 
            if(transform.position.y<-limite){
                transform.position = posInicial;
            }
        }
    }
}

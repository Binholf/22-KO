using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraController : MonoBehaviour
    {
        public Transform player;
        public float posIniX, posIniY;
        public float ajusteY, ajusteX;
        public float maxX, minX, maxY, minY;
        public float velocidadeCamera;

        private void Start(){
            //ajusta as camera para os padrões iniciais passados a ela
            Vector3 newPosition = player.position + new Vector3(posIniX,posIniY,-10);
            transform.position=newPosition;
        }

        private void FixedUpdate() {
            //ajustar a camera na posição do player mas afastar ela(eixo Z) para capturar a imagem
            Vector3 newPosition = player.position + new Vector3(0,0,-10);
            //pequenos ajustes na posição da camera em relação ao player  
            newPosition.x= player.position.x + ajusteX;
            newPosition.y= player.position.y - ajusteY;
            //move a camera, da posição atual dela até a nova posição, utilizando um "deslizamento/velocidade" definida
            newPosition = Vector3.Lerp(transform.position, newPosition, velocidadeCamera);

            transform.position=newPosition;
            //inserir os limites do cenário/camera
            transform.position= new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);
        }
    }
}
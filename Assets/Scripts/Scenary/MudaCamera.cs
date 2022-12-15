using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Camera;

namespace Assets.Scripts.Scenary
{
    public class MudaCamera : MonoBehaviour
    {
        //novo limite vertical da camera
        public float novoMinY, novoMaxY, novoMinX, novoMaxX, novoAjusteY, novoAjusteX;
        public CameraController cameraJogo;

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.CompareTag("Player")){
                cameraJogo.maxY=novoMaxY;
                cameraJogo.minY=novoMinY;
                cameraJogo.maxX=novoMaxX;
                cameraJogo.minX=novoMinX;
                cameraJogo.ajusteY=novoAjusteY;
                cameraJogo.ajusteX=novoAjusteX;
            }
        }
    }
}

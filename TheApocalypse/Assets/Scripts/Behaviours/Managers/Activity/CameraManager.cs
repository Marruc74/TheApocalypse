using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Behaviours.Managers.Activity
{
    public class CameraManager : MonoBehaviour
    {
        public float speed = 20f;        

        void Update()
        {
            MoveCamera();            
        }

        public void MoveCamera()
        {
            if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("D");
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
            }
        }

       
    }
}

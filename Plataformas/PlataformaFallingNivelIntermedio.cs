﻿    using UnityEngine;

    public class PlataformaFallingNivelIntermedio : MonoBehaviour
    {

        private Rigidbody2D rgb2d;
        private BoxCollider2D pc2d;
        private Vector3 start;
        public float falldelay = 1f;
        public float volver = 5f;




        void Start()
        {
            rgb2d = GetComponent<Rigidbody2D>();
            pc2d = GetComponent<BoxCollider2D>();
            start = transform.position;
        }


        void Update()
        {

            if (SisJuegoIntermedia.instance.tirarTroll)
            {
                AudioCtrl.instance.TrollDeath(this.gameObject.transform);
                Invoke("Fall", falldelay);

            }


        }





        void Fall()
        {
            rgb2d.isKinematic = false;
            pc2d.isTrigger = true;
        }

        void Volver()
        {
            transform.position = start;
            rgb2d.isKinematic = true;
            pc2d.isTrigger = false;
            rgb2d.velocity = Vector3.zero;

        }



    }

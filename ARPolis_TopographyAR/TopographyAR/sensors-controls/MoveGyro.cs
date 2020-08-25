using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPolis.TopographyAR
{

    public class MoveGyro : MovePerson
    {

        void OnEnable()
        {
            myTransform = GetComponent<Transform>();
            cmotor = GetComponent<CharacterMotorC>();
            if (cmotor)
            {
                GlobalManager.Instance.cMotor = cmotor;
            }

            if (!myCamera)
            {
                myCamera = transform.GetChild(0).GetComponent<Transform>();
            }

            GlobalManager.Instance.cameraNow = myCamera.GetComponent<Camera>();
            if (PlayerPrefs.GetFloat("cameraFieldOfView") > 0)
            {
                myCamera.GetComponent<Camera>().fieldOfView = PlayerPrefs.GetFloat("cameraFieldOfView");
            }

            ResetGyro();

            if (myGyro == null)
            {
                myGyro = Input.gyro;
            }
            if (!myGyro.enabled)
            {
                myGyro.enabled = true;
            }
        }


        public bool tempGyro = false;

        void Update()
        {
            //check if is not reading
            if (GlobalManager.Instance.user == GlobalManager.User.isNavigating || GlobalManager.Instance.user == GlobalManager.User.inFullMap || GlobalManager.Instance.user == GlobalManager.User.onAir || tempGyro || GlobalManager.Instance.user == GlobalManager.User.inAsanser)
            {
                Gyroskopio();
            }
        }


        void LateUpdate()
        {

            if (GlobalManager.Instance.navMode == GlobalManager.NavMode.offSite)
            {
                if (GlobalManager.Instance.user == GlobalManager.User.isNavigating)
                {
                    JoyMove();
                }
                else if (GlobalManager.Instance.user == GlobalManager.User.onAir && GlobalManager.Instance.moveOnAir)
                {
                    JoyMove();
                }
            }
            else
            if (GlobalManager.Instance.navMode == GlobalManager.NavMode.onSite)
            {
                if (GlobalManager.Instance.user == GlobalManager.User.isNavigating || GlobalManager.Instance.user == GlobalManager.User.inFullMap || (GlobalManager.Instance.user == GlobalManager.User.onAir && GlobalManager.Instance.moveOnAir))
                {
                    GpsMove();
                }
            }
        }

    }

}

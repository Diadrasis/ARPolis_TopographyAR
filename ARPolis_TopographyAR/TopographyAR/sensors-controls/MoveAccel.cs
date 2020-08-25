using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPolis.TopographyAR
{

    public class MoveAccel : MovePerson
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
                myCamera = FindObjectOfType<Camera>().transform;
            }

            SetCamAngleX();

            GlobalManager.Instance.cameraNow = myCamera.GetComponent<Camera>();
            if (PlayerPrefs.GetFloat("cameraFieldOfView") > 0)
            {
                myCamera.GetComponent<Camera>().fieldOfView = PlayerPrefs.GetFloat("cameraFieldOfView");
            }

        }

        public void SetCamAngleX()
        {
            if (myCamera)
            {
                Vector3 rot = myCamera.localEulerAngles;
                rot.x = MoveSettings.camAccelRotX;
                myCamera.localEulerAngles = rot;
            }
        }

        public void SetCamAngleY()
        {
            if (myCamera)
            {
                Vector3 rot = myCamera.localEulerAngles;
                rot.y = MoveSettings.camAccelRotY;
                myCamera.localEulerAngles = rot;
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
                else if (GlobalManager.Instance.user == GlobalManager.User.onAir)
                {
                    if (GlobalManager.Instance.moveOnAir)
                    {
                        JoyMove();
                    }
                }
            }
            else
            if (GlobalManager.Instance.navMode == GlobalManager.NavMode.onSite)
            {
                if ((GlobalManager.Instance.user == GlobalManager.User.isNavigating || GlobalManager.Instance.user == GlobalManager.User.inFullMap) || (GlobalManager.Instance.user == GlobalManager.User.onAir && GlobalManager.Instance.moveOnAir))
                {
                    GpsMove();
                }
            }
        }


    }

}

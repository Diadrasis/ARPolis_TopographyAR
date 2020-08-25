using UnityEngine;

namespace ARPolis.TopographyAR
{

    /// <summary>
    /// accelerometer sensor control - y axis
    /// </summary>
    public class Compass : MonoBehaviour
    {

        public Transform target;

        public float turnSpeed = 1f;
        public Vector3 accel;
        public float limitationRotX = 0.3f;

        void Start()
        {
            Input.compass.enabled = true;
            Input.location.Start();
            target = GetComponent<Transform>();
        }

        public void OnEnable()
        {

            if (!Input.compass.enabled) Input.compass.enabled = true;

            Input.location.Start();

            if (!target) target = GetComponent<Transform>();
        }

        public void OnDisable()
        {
            Input.compass.enabled = false;
            //		Input.location.Stop();
        }


        void FixedUpdate()
        {
            accel = Input.acceleration;
            if (GlobalManager.Instance.user == GlobalManager.User.isNavigating || GlobalManager.Instance.user == GlobalManager.User.onAir || GlobalManager.Instance.user == GlobalManager.User.inAsanser)
            {
                #region test
                //			if(accel.x>-limitationRotX && accel.x<limitationRotX)//για να μην μετακινειται με την περιστροφη του accelerometer στον αξονα x
                //			{
                //				if(accel.z>0.5f)
                //				{
                //					Debug.Log("look up");
                //					myTransform.localRotation = Quaternion.Lerp(myTransform.localRotation, Quaternion.Euler(0,-Mathf.Floor(Input.compass.trueHeading), 0),Time.deltaTime * moveSettings.compassTurnSpeed);
                //				}
                //				else
                //				if(accel.z<=-0.5f)
                #endregion

                //look down
                if (accel.z < 0.0f)//bigger value drives to 180 rotation
                {
                    target.localRotation = Quaternion.Lerp(target.localRotation, Quaternion.Euler(0, Mathf.Floor(Input.compass.trueHeading), 0), Time.deltaTime * MoveSettings.compassTurnSpeed);
                }
            }

        }

    }

}
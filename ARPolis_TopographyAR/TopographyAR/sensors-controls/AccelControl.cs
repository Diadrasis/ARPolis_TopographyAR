using UnityEngine;

namespace ARPolis.TopographyAR
{

    /// <summary>
    /// accelerometer sensor control - x,z axis
    /// measure non-gravitational acceleration
    /// </summary>
    public class AccelControl : MonoBehaviour
    {

        public Transform target;

        public float turnSpeed = 10f;
        public float maxTurnLean = 50f;
        public float maxTilt = 50f;
        public float sensitivity = 0.1f;

        private Vector3 euler = Vector3.zero;
        private Quaternion calibrationQuaternion;

        public void OnEnable()
        {
            if (!target) target = GetComponent<Transform>();

            //reset accel
            CalibrateAccelerometer();
        }

        public void ResetAccel() { CalibrateAccelerometer(); }

        void CalibrateAccelerometer()
        {
            Vector3 accelerationSnapshot = Input.acceleration;

            Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);

            calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
        }

        public void OnDisable()
        {
            ResetAccel();
        }

        void FixedUpdate()
        {
            if (target != null)
            {
                Vector3 accelerator = Input.acceleration;
                Vector3 fixedAcceleration = calibrationQuaternion * accelerator;

                // Rotate turn based on acceleration		
                euler.y += fixedAcceleration.x * turnSpeed;

                // extra smoothing on z axis
                euler.z = Mathf.Lerp(euler.z, -fixedAcceleration.x * maxTurnLean, 0.2f);

                // extra smoothing on x axis
                euler.x = Mathf.Lerp(euler.x, fixedAcceleration.y * maxTilt, 0.2f);

                // Apply rotation and smoothing
                Quaternion rot = Quaternion.Euler(euler);

                target.localRotation = Quaternion.Lerp(target.localRotation, rot, sensitivity);
            }
        }

    }

}
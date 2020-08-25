using UnityEngine;
using System.Collections;
using System;

namespace ARPolis.TopographyAR
{

    public class Gps : Singleton<Gps>
    {

        #region VARIABLES

        protected Gps() { }


        /*
        Stopped	
        Location service is stopped.

        Initializing	
        Location service is initializing, some time later it will switch to.

        Running	
        Location service is running and locations could be queried.

        Failed	
        Location service failed (user denied access to location service).
        */

        public enum Status { IDLE, STOPPED, INITIALISING, FAILED, RUNNING };
        public Status status = Status.IDLE;

        private Vector2 posCurrent = new Vector2(15.139173f, -23.121658f);//Maio
        private Vector2 posFaraway = new Vector2(15.139173f, -23.121658f);//Maio

       // public WarningEventsUI warningsInfo;

        //accuracy thresold
        public float maxGpsAccuracy = 15f;

        //initialize time
        public float gpsInitTime = 20f;

        //wait for accuracy check after position found
        public float gpsTimeForAccuracyCheck = 65f;

        //start location params
        public float locStartingAccuracy = 1f;
        public float locUpdateDistance = 0.1f;

        //how often we get gps data
        //perhaps must be the same we the update distance??
        public float checkRepeatTime = 0.5f;

        public bool isLocationStarted = false;
        private bool isRepeatChecking;

        public float isBigAccuracyTest = 105f;//test
        public bool isLocationFound;//test
        public bool isHelpActive;

        public bool stopLocationOnFailed = false;

        private int isStartTimes;

        #endregion

        #region EDITOR TESTING

#if UNITY_EDITOR

        public enum TestPos
        {
            PyliAndrianou, PyliRomaikisAgoras, Mentreses, PisinaAndrianou, AeridesPano, AeridesPlagia, Tzami, AndrianouPiso,
            Andrianou_Areos, Dexippou_Panos_Andrianou, Panos_Pelopida, Pelopida_Aiolou, Aiolou_Kalogiorgi, Omonoia, Diadrasis,
            Diadrasis_B, Diadrasis_C, Diadrasis_D, Diadrasis_E, Agora_1, Agora_2, Kerameikos, Akropoli_1, Olympieion_1, Olympieion_2, Aerides_testA, Aerides_testB
        };
        public TestPos testPos = TestPos.Mentreses;
        public string m;

        Vector2 pyliAndrianou = new Vector2(37.975582f, 23.725711f);

        void Start()
        {
            m = testPos.ToString();
        }

        public bool isManualWalk;
        public Vector2 manualGPS = new Vector2(37.975582f, 23.725711f);

        void SetGpsPos()
        {

            if (isManualWalk)
            {
                posCurrent = manualGPS;
                return;
            }

            if (randomWalk)
            {
                posCurrent = RandomGpsWalk();
                return;
            }

            if (m != testPos.ToString())
            {

                if (testPos == TestPos.Mentreses)
                {
                    posCurrent = new Vector2(37.974414f, 23.727243f);
                }
                else
                if (testPos == TestPos.PisinaAndrianou)
                {
                    posCurrent = new Vector2(37.975354f, 23.726733f);
                }
                else
                if (testPos == TestPos.AeridesPano)
                {
                    posCurrent = new Vector2(37.973924f, 23.726955f);
                }
                else
                if (testPos == TestPos.AeridesPlagia)
                {
                    posCurrent = new Vector2(37.974106f, 23.727241f);
                }
                else
                if (testPos == TestPos.Tzami)
                {
                    posCurrent = new Vector2(37.974621f, 23.726727f);
                }
                else
                if (testPos == TestPos.AndrianouPiso)
                {
                    posCurrent = new Vector2(37.975243f, 23.727261f);
                }
                else
                if (testPos == TestPos.PyliAndrianou)
                {
                    posCurrent = pyliAndrianou;
                }
                else
                if (testPos == TestPos.PyliRomaikisAgoras)
                {
                    posCurrent = new Vector2(37.974570f, 23.725463f);
                }
                else
                if (testPos == TestPos.Andrianou_Areos)
                {
                    posCurrent = new Vector2(37.975605f, 23.725655f);
                }
                else
                if (testPos == TestPos.Dexippou_Panos_Andrianou)
                {
                    posCurrent = new Vector2(37.974893f, 23.726467f);
                }
                else
                if (testPos == TestPos.Panos_Pelopida)
                {
                    posCurrent = new Vector2(37.974721f, 23.726417f);
                }
                else
                if (testPos == TestPos.Pelopida_Aiolou)
                {
                    posCurrent = new Vector2(37.974500f, 23.727093f);
                }
                else
                if (testPos == TestPos.Aiolou_Kalogiorgi)
                {
                    posCurrent = new Vector2(37.975213f, 23.727278f);
                }
                else
                if (testPos == TestPos.Omonoia)
                {
                    posCurrent = new Vector2(37.984078f, 23.7278039f);
                }
                else
                if (testPos == TestPos.Diadrasis)
                {
                    posCurrent = new Vector2(37.9882f, 23.7292f);
                }
                else
                if (testPos == TestPos.Diadrasis_B)
                {
                    posCurrent = new Vector2(37.988194f, 23.729069f);
                }
                else
                if (testPos == TestPos.Diadrasis_C)
                {
                    posCurrent = new Vector2(37.988014f, 23.730223f);
                }
                else
                if (testPos == TestPos.Diadrasis_D)
                {
                    posCurrent = new Vector2(37.987911f, 23.727959f);
                }
                else
                if (testPos == TestPos.Diadrasis_E)
                {
                    posCurrent = new Vector2(37.988839f, 23.729153f);
                }
                else
                if (testPos == TestPos.Agora_1)
                {
                    posCurrent = new Vector2(37.9754f, 23.7212f);
                }
                else
                if (testPos == TestPos.Agora_2)
                {
                    posCurrent = new Vector2(37.9746f, 23.7228f);
                }
                else
                if (testPos == TestPos.Kerameikos)
                {
                    posCurrent = new Vector2(37.978671f, 23.718396f);
                }
                else
                if (testPos == TestPos.Akropoli_1)
                {
                    posCurrent = new Vector2(37.971705f, 23.727249f);
                }
                else
                if (testPos == TestPos.Olympieion_1)
                {
                    posCurrent = new Vector2(37.969898f, 23.733756f);
                }
                else
                if (testPos == TestPos.Olympieion_2)
                {
                    posCurrent = new Vector2(37.970040f, 23.733752f);
                }
                else
                if (testPos == TestPos.Aerides_testA)
                {
                    posCurrent = new Vector2(37.974619f, 23.725022f);
                }
                else
                if (testPos == TestPos.Aerides_testB)
                {
                    posCurrent = new Vector2(37.974660f, 23.724494f);
                }

                m = testPos.ToString();

                Debug.Log(m);
            }
        }

        public bool randomWalk;
        public float maxStep = 0.0001f;

        Vector2 RandomGpsWalk()
        {
            float x = UnityEngine.Random.Range(GetMyLocation().x - maxStep, GetMyLocation().x + maxStep);
            float y = UnityEngine.Random.Range(GetMyLocation().y - maxStep, GetMyLocation().y + maxStep);
            return new Vector2(x, y);
        }

#endif

        #endregion

        #region START LOCATION SERVICE

        public void StartLocationService()
        {

            if (isLocationStarted && IsEnabled())
            {
                return;
            }

            if (SystemInfo.supportsLocationService)
            {
                if (!isLocationStarted && IsEnabled())
                {
                    status = Status.IDLE;

                    //reset menu map gps status so in next menu entrance can check again
                   // warningsInfo.gpsStatus = WarningEventsUI.GpsStatus.IDLE;

                    // Start service before querying location
                    Input.location.Start(locStartingAccuracy, locUpdateDistance);
                    isLocationStarted = true;
                }
            }
            else
            {
#if UNITY_EDITOR
                if (!isLocationStarted && IsEnabled())
                {
                    status = Status.IDLE;

                    Debug.Log("Start Location Service Editor");
                    isLocationStarted = true;
                }
#endif
            }
        }

        #endregion

        #region LATE UPDATE

        void LateUpdate()
        {

            CheckGPS();

        }

        #endregion

        #region INIT

        // Use this for initialization
        public void Init()
        {

            //if (warningsInfo == null)
            //{
            //    warningsInfo = Gps.Instance.warningsInfo;
            //}

            if (isStartTimes == 1)
            {
                status = Status.IDLE;
            }

            if (status == Status.RUNNING)
            {
                return;
            }

            status = Status.IDLE;

            LoadSavedValues();

            StartLocationService();

        }

        #endregion

        #region GET LOCATION

        public Vector2 GetMyLocation()
        {
            return posCurrent;
        }

        #endregion

        #region GPS IS ENABLED? OR IS WORKING?

        public bool IsEnabled()
        {

            if (SystemInfo.supportsLocationService)
            {
                return Input.location.isEnabledByUser;
            }

            return GlobalManager.Instance.gpsIsOn;
        }

        public bool isWorking()
        {
            if (IsEnabled() && status == Status.RUNNING)
            {
                return true;
            }

            return false;
        }

        #endregion

        #region CHECK GPS STATUS

        //check if GPS is enable or disable
        void CheckGPS()
        {
            if (IsEnabled())
            {
                if (SystemInfo.supportsLocationService)
                {
                    if (Input.location.status == LocationServiceStatus.Running)
                    {
                        if (status == Status.FAILED)
                        {
                            return;
                        }

                        //accuracy check
                        if (gpsTimeForAccuracyCheck > 0f)
                        {

                            if (Input.location.lastData.horizontalAccuracy > maxGpsAccuracy)
                            {
                                //if (warningsInfo != null) warningsInfo.HideGpsWarnings();

                                if (GlobalManager.Instance.user == GlobalManager.User.inMenu && status != Status.FAILED)
                                {

                                    //if (warningsInfo != null)
                                    //{
                                    //    //enable checking accuracy message (akribeia)
                                    //    if (warningsInfo.gpsWaitForCheckingAccuracy != null)
                                    //    {
                                    //        warningsInfo.gpsWaitForCheckingAccuracy.SetActive(true);
                                    //    }
                                    //}

                                }
                                //count time
                                gpsTimeForAccuracyCheck -= Time.deltaTime;
                            }
                            else
                            {
                                gpsTimeForAccuracyCheck = -1000f;
                            }

                        }
                        else
                        {
                            //after accuracy check time
                            if (gpsTimeForAccuracyCheck != -1000f)
                            {
                                //if (warningsInfo != null)
                                //{
                                //    warningsInfo.HideGpsWarnings();
                                //}

                                //if inaccurate
                                if (Input.location.lastData.horizontalAccuracy > maxGpsAccuracy)
                                {
                                    //show message to user
                                    gpsTimeForAccuracyCheck -= Time.deltaTime;

                                    if (gpsTimeForAccuracyCheck > -10f)
                                    {

                                        if (GlobalManager.Instance.user == GlobalManager.User.inMenu)
                                        {
                                            //if (warningsInfo != null)
                                            //{
                                            //    if (warningsInfo.gpsFaultAccuracy != null)
                                            //    {
                                            //        warningsInfo.gpsFaultAccuracy.SetActive(true);
                                            //    }
                                            //}
                                        }
                                    }
                                    else
                                    {
                                        gpsTimeForAccuracyCheck = -1000f;
                                    }
                                }
                            }
                        }

                        //GPS is running
                        if (status != Status.RUNNING)
                        {
                            status = Status.RUNNING;
                        }

                        //save user position
                        posCurrent = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);

                        return;
                    }
                    else//wait for gps
                    {
                        //on fail check again
                        if (status == Status.FAILED)
                        {
                            return;
                        }

                        //searching
                        if (status != Status.INITIALISING)
                        {
                            status = Status.INITIALISING;
                        }

                        //start gps service
                        StartLocationService();

                        //COUNT TIME - OUT
                        gpsInitTime -= Time.deltaTime;

                        if (gpsInitTime <= 0)
                        {
                            //hide last message
                            //if (warningsInfo != null)
                            //{
                            //    warningsInfo.HideGpsWarnings();
                            //}
                            //show message TIME OUT 
                            if (GlobalManager.Instance.user == GlobalManager.User.inMenu)
                            {
                                //if (warningsInfo != null)
                                //{
                                //    if (warningsInfo.gpsTimeOurMSG != null)
                                //    {
                                //        warningsInfo.gpsTimeOurMSG.SetActive(true);
                                //    }
                                //}
                            }

                            Failed();

                            return;
                        }

                        //searching message
                        if (GlobalManager.Instance.user == GlobalManager.User.inMenu)
                        {
                            //if (warningsInfo != null)
                            //{
                            //    warningsInfo.HideGpsWarnings();
                            //    if (warningsInfo.gpsInitialingMSG != null)
                            //    {
                            //        warningsInfo.gpsInitialingMSG.SetActive(true);
                            //    }
                            //}

                        }

                        return;
                    }
                }
                else
                {

                    #region EDITOR TESTING

#if UNITY_EDITOR

                    if (isLocationFound)
                    {
                        if (status == Status.FAILED)
                        {
                            return;
                        }

                        if (gpsTimeForAccuracyCheck > 0f)
                        {// && status!=Status.RUNNING){
                            if (isBigAccuracyTest > maxGpsAccuracy)
                            {
                                warningsInfo.HideGpsWarnings();

                                //							if(gpsTimeForAccuracyCheck>55f && gpsTimeForAccuracyCheck>30f && gpsTimeForAccuracyCheck<40f &&
                                if (Diadrasis.Instance.user == Diadrasis.User.inMenu)
                                {
                                    //enable checking accuracy message (akribeia)
                                    warningsInfo.gpsWaitForCheckingAccuracy.SetActive(true);
                                }
                                //count time
                                gpsTimeForAccuracyCheck -= Time.deltaTime;
                            }
                            else
                            {
                                gpsTimeForAccuracyCheck = -1000f;
                            }

                            //						return;
                        }
                        else
                        {
                            //check accuracy
                            if (gpsTimeForAccuracyCheck != -1000f)
                            {
                                if (warningsInfo != null)
                                {
                                    warningsInfo.HideGpsWarnings();
                                }
                                if (isBigAccuracyTest > maxGpsAccuracy)
                                {
                                    //count time
                                    gpsTimeForAccuracyCheck -= Time.deltaTime;

                                    if (gpsTimeForAccuracyCheck > -10f)
                                    {
                                        if (Diadrasis.Instance.user == Diadrasis.User.inMenu)
                                        {
                                            if (warningsInfo != null)
                                            {
                                                if (warningsInfo.gpsFaultAccuracy != null)
                                                {
                                                    warningsInfo.gpsFaultAccuracy.SetActive(true);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        gpsTimeForAccuracyCheck = -1000f;
                                    }
                                }
                            }
                        }

                        //GPS is working
                        if (status != Status.RUNNING)
                        {
                            status = Status.RUNNING;
                        }

                        //FAKE GPS DATA IN EDITOR
                        SetGpsPos();

                        return;
                    }
                    else
                    {
                        if (status == Status.FAILED)
                        {
                            return;
                        }

                        if (status != Status.INITIALISING)
                        {
                            status = Status.INITIALISING;
                        }

                        StartLocationService();

                        //count time
                        gpsInitTime -= Time.deltaTime;

                        if (gpsInitTime <= 0)
                        {
                            if (warningsInfo != null)
                            {
                                warningsInfo.HideGpsWarnings();
                            }
                            if (Diadrasis.Instance.user == Diadrasis.User.inMenu)
                            {
                                if (warningsInfo != null)
                                {
                                    if (warningsInfo.gpsTimeOurMSG != null)
                                    {
                                        warningsInfo.gpsTimeOurMSG.SetActive(true);
                                    }
                                }
                            }
                            Failed();
                            return;
                        }

                        if (Diadrasis.Instance.user == Diadrasis.User.inMenu && warningsInfo != null)
                        {
                            warningsInfo.HideGpsWarnings();
                            warningsInfo.gpsInitialingMSG.SetActive(true);
                        }
                    }
#endif

                    #endregion
                }

            }
            else
            if (!IsEnabled())
            {
                //GPS is disabled
#if UNITY_EDITOR
                //			Debug.Log("GPS is disabled !!");
                if (Diadrasis.Instance.isTesting)
                {
                    return;
                }
#endif

                Stop();
            }
        }

        #endregion

        #region FAILED STATUS

        public void Failed()
        {
            if (status != Status.FAILED)
            {
                status = Status.FAILED;

#if UNITY_EDITOR
                Debug.Log("GPS FAILED");
#endif

                LoadSavedValues();

                //no signal fron gps
                posCurrent = posFaraway;
            }
        }

        #endregion

        #region STOP STATUS

        public void Stop()
        {

            if (status != Status.STOPPED)
            {


                //if (warningsInfo)
                //{
                //    warningsInfo.HideGpsWarnings();
                //}


                status = Status.STOPPED;

                //warningsInfo.SetGpsStatus(WarningEventsUI.GpsStatus.OFF, true);


#if UNITY_EDITOR
                Debug.Log("GPS STOPPED");
#endif

                if (SystemInfo.supportsLocationService && isLocationStarted)
                {
                    Input.location.Stop();
                }

                isLocationStarted = false;


                LoadSavedValues();

                //no signal fron gps
                posCurrent = posFaraway;
            }
        }

        #endregion

        #region LOAD SAVED VALUES

        public void LoadSavedValues()
        {

            maxGpsAccuracy = 15f;
            gpsInitTime = 20f;
            gpsTimeForAccuracyCheck = 65f;
            locStartingAccuracy = 7f;
            locUpdateDistance = 1f;
            checkRepeatTime = 0.5f;


            if (PlayerPrefs.GetFloat("initTime") > 0f)
            {
                gpsInitTime = PlayerPrefs.GetFloat("initTime");
            }
            else
            {
                PlayerPrefs.SetFloat("initTime", gpsInitTime);
            }
            if (PlayerPrefs.GetFloat("gpsICheckAccuracyTime") > 0f)
            {
                gpsTimeForAccuracyCheck = PlayerPrefs.GetFloat("gpsICheckAccuracyTime");
            }
            else
            {
                PlayerPrefs.SetFloat("gpsICheckAccuracyTime", gpsTimeForAccuracyCheck);
            }
            if (PlayerPrefs.GetFloat("maxAccuracy") > 0f)
            {
                maxGpsAccuracy = PlayerPrefs.GetFloat("maxAccuracy");
            }
            else
            {
                PlayerPrefs.SetFloat("maxAccuracy", maxGpsAccuracy);
            }
            if (PlayerPrefs.GetFloat("locStartingAccuracy") > 0f)
            {
                locStartingAccuracy = PlayerPrefs.GetFloat("locStartingAccuracy");
            }
            else
            {
                PlayerPrefs.SetFloat("locStartingAccuracy", locStartingAccuracy);
            }
            if (PlayerPrefs.GetFloat("locUpdateDistance") > 0f)
            {
                locUpdateDistance = PlayerPrefs.GetFloat("locUpdateDistance");
            }
            else
            {
                PlayerPrefs.SetFloat("locUpdateDistance", locUpdateDistance);
            }
            if (PlayerPrefs.GetFloat("checkRepeatTime") > 0f)
            {
                checkRepeatTime = PlayerPrefs.GetFloat("checkRepeatTime");
            }
            else
            {
                PlayerPrefs.SetFloat("checkRepeatTime", checkRepeatTime);
            }

        }

        #endregion

    }


}
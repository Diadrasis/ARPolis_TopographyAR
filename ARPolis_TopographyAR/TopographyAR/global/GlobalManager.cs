using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using ARPolis.TopographyAR;

public class GlobalManager : Singleton<GlobalManager>
{
	private GlobalManager ()
	{
	}

	#region DELEGATES

	public delegate void ActionSceneLoading ();
	//Load Start
	public event ActionSceneLoading OnSceneLoadStart;
	//Load Start
	public event ActionSceneLoading OnSceneDownloadEnd;
	//Load End parts
	public event ActionSceneLoading OnSceneLoadEnd;

	public delegate void ActionPause ();
	//Pause true
	public event ActionPause OnPauseTrue;
	//pause false
	public event ActionPause OnPauseFalse;

	public delegate void ActionMap ();
	//full map close
	public event ActionMap OnMapFullClose;
	//full map show
	public event ActionMap OnMapFullShow;

	#endregion

	#region PUBLIC VARIABLES

	public float currScreenDiagonios;

	public string appVersion;

	public bool isFirstHelpMenu;

	public bool isTesting;
	public bool gyroExists;
	public bool accelExists;
	public bool gpsIsOn;
	public bool internetIsOn;
	public bool updateXml;
	public bool moveOnAir = true;
	//should lezanta of image have full width?
	public bool infoImageTextFullWdth = false;
	public bool useMapFilterForMovement;
	public bool useOnSiteMode;
	public bool showPoiInfo = true;
	public bool enableDiadrasisScene;
	public bool useCharacterControllerToPerson;
	public List<string> unlocked_Scenes = new List<string> ();
	public string[] freeScenes;

	public List<AudioSource> allSounds = new List<AudioSource> ();

	//isInPause --> when app is minimized by user or an external application
	//isIdle ?? --> when user is not touching the screen for about 5 min
	//				then we enable screen sleep (by user default) (battery saver)
	//				and also we disable all using sensors
	public enum User
	{
		inMenu,
		isNavigating,
		inPoi,
		inSettings,
		inHelp,
		inPause,
		isIdle,
		inLoading,
		inFullMap,
		inCredits,
		inWarning,
		onAir,
		inQuit,
		inAsanser,
		inShop,
		inBetaWarning,
		inDeviceCheckWarning

	}

	public User user = User.isIdle;

	//for checking previus status in common situations like help-info-warning-pause-settings
	public enum UserPrin
	{
		inMenu,
		isNavigating,
		inPoi,
		inSettings,
		inHelp,
		inPause,
		isIdle,
		inLoading,
		inFullMap,
		inGredits,
		inWarning,
		onAir,
		inQuit,
		inAsanser

	}

	public UserPrin userPrin = UserPrin.inMenu;

	//escape button options
	public enum EscapeUser
	{
		inMenu,
		isNavigating,
		inPoi,
		inSettings,
		inHelp,
		inFullMap,
		inWarning,
		onAir,
		inQuit,
		inAsanser,
		isIdle,
		inShop,
		inLoading

	}

	public EscapeUser escapeUser = EscapeUser.inMenu;
	public EscapeUser prevEscapeUser = EscapeUser.inMenu;

	public enum MenuStatus
	{
		idle,
		mapMove,
		mapZoom,
		periodView,
		mapLerping

	}

	public MenuStatus menuStatus = MenuStatus.idle;

	public enum ZoomStatus
	{
		idle,
		zoomIN,
		zoomOut

	}

	public ZoomStatus zoomStatus = ZoomStatus.idle;

	//which sensor can use
	public enum SensorAvalaible
	{
		empty,
		gyroscopio,
		accelCompass}

	;

	public SensorAvalaible sensorAvalaible = SensorAvalaible.empty;

	//which sensor is using
	public enum SensorUsing
	{
		joysticks,
		gyroscopio,
		accelCompass}

	;

	public SensorUsing sensorUsing = SensorUsing.joysticks;

	//which sensor is using
	public enum LanguangeNow
	{
		gr,
		en}

	;

	public LanguangeNow languangeNow = LanguangeNow.gr;

	//which navigation mode is selected by user
	public enum NavMode
	{
		onSite,
		offSite}

	;

	public NavMode navMode = NavMode.offSite;

	public GameObject objDontDestroy;
	public GameObject person;
	public Camera cameraNow;
	public CharacterMotorC cMotor;


	public RectTransform canvasMainRT;

	public string nearSceneAreaName;
	
	public string sceneName;
	public string XmlPointsTagName;
	public string currentPoi;
	public string introText, loadingText, introTitle;
	public string loadingImage;


	public string mapScene;
	//public string mapFilter;
	public Vector2 mapPivot;
	public Vector2 mapFullPosition;
	public Vector2 mapFullZoom;

	public Vector2 sceneGpsPosition;

	public int isStart = 0;
	public int appEntrances = 0;

	public float idleTime = 100f;

	public AudioSource ixos;
	//	public AudioListener akoi;
	public AudioListener akoiPerson;
	AudioListener speaker;

	public int screenSize;

	public AssetBundle bundlePoiImages, bundleSceneSounds;

	#endregion

	#region PRIVATE VARIABLES

	//async variables
	private AsyncOperation async = null;
	public bool introIsClosed;

	AssetBundle bundleScene;
	int versionScene = 1;
	int versionImages = 1;
	int versionSounds = 1;
	int draftVersion;

	string bundlesURL = "http://diadrasis.net/timeWalkFiles/assets/";
	string androidURL = "Android/";
	string iosURL = "iOS/";
	string platformURL, myUrl;

    #endregion

    public void ChangeStatus(User status)
    {
        user = status;
       // CheckStatus();
    }

}

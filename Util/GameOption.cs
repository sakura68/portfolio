using UnityEngine;
using System.Collections;


public enum enGraphicQuality : byte
{   
    Fast,
    Normal,
    Good,
    None,
}

/// <summary>
/// 게임 옵션
/// </summary>
public class GameOption
{

    // 싱글턴
    private static GameOption _instance = new GameOption();
    public static GameOption intance { get { return _instance; } }


    // 옵션창열렸는지 확인
    public bool IsOpenOption;

    // 옵션 초기화
    public void InitGameOption()
    {
        _IsSoundBgm = IsSoundBgm;
        _IsSoundEffect = IsSoundEffect;
        _QualityOption = QualityOption;
        QualitySettings.SetQualityLevel((int)QualityOption);
    }


    private bool _IsSoundBgm = true;
    public bool IsSoundBgm
    {
        set
        {
            _IsSoundBgm = value;
            FileManager.instance.SaveDataOption<bool>(enSaveFileType.GameOption, "SoundBgm", _IsSoundBgm);
        }
        get
        {
            _IsSoundBgm = FileManager.instance.LoadDataOption<bool>(enSaveFileType.GameOption, "SoundBgm", true);
            return _IsSoundBgm;
        }
    }

    private bool _IsSoundEffect = true;
    public bool IsSoundEffect
    {
        set
        {
            _IsSoundEffect = value;
            FileManager.instance.SaveDataOption<bool>(enSaveFileType.GameOption, "SoundEffect", _IsSoundEffect);
        }
        get
        {
            _IsSoundEffect = FileManager.instance.LoadDataOption<bool>(enSaveFileType.GameOption, "SoundEffect", true);
            return _IsSoundEffect;
        }
    }

    private enGraphicQuality _QualityOption = enGraphicQuality.Normal;
    public enGraphicQuality QualityOption
    {
        set
        {
            _QualityOption = value;
            FileManager.instance.SaveDataOption<byte>(enSaveFileType.GameOption, "Quality", (byte)_QualityOption);
            QualitySettings.SetQualityLevel((int)_QualityOption);
        }
        get
        {
            _QualityOption = (enGraphicQuality)FileManager.instance.LoadDataOption<byte>(enSaveFileType.GameOption, "Quality", (byte)enGraphicQuality.Normal);
            QualitySettings.SetQualityLevel((int)_QualityOption);
            return _QualityOption;
        }
    }


    private GameSpeedButtonsState _Gamespeed = GameSpeedButtonsState.SPPED1X;
    public GameSpeedButtonsState Gamespeed
    {
        set
        {
            _Gamespeed = value;
            FileManager.instance.SaveDataOption<byte>(enSaveFileType.GameOption, "GameSpeed", (byte)_Gamespeed);
        }
        get
        {
            _Gamespeed = (GameSpeedButtonsState)FileManager.instance.LoadDataOption<byte>(enSaveFileType.GameOption, "GameSpeed", (byte)GameSpeedButtonsState.SPPED1X);
            return _Gamespeed;
        }
    }

    private enAutoMode _AutoMode = enAutoMode.enAM_NONE;
    public enAutoMode AutoMode
    {
        set
        {
            _AutoMode = value;
            FileManager.instance.SaveDataOption<byte>(enSaveFileType.GameOption, "AutoMose", (byte)_AutoMode);
        }
        get
        {
            _AutoMode = (enAutoMode)FileManager.instance.LoadDataOption<byte>(enSaveFileType.GameOption, "AutoMose", (byte)enAutoMode.enAM_NONE);
            return _AutoMode;
        }
    }

    /// <summary>
    /// 카메라 연출
    /// </summary>
    private bool _IsStopSkillDirectingAction = false;
    public bool IsStopSkillDirectingAction
    {
        set
        {
            _IsStopSkillDirectingAction = value;
            FileManager.instance.SaveDataOption<bool>(enSaveFileType.GameOption, "StopCameraAction", _IsStopSkillDirectingAction);
        }
        get
        {
            _IsStopSkillDirectingAction = FileManager.instance.LoadDataOption<bool>(enSaveFileType.GameOption, "StopCameraAction", false);
            return _IsStopSkillDirectingAction;
        }
    }

#if FCM && !UNITY_EDITOR
    private string _daytimeTopic = "daytime";
    public string daytimeTopic { get { return _daytimeTopic; } }

    private string _nightTopic = "night";
    public string nightTopic { get { return _nightTopic; } }
#endif

    /// <summary>
    /// 주간알람.
    /// </summary>
    private bool _IsDayTimeAlram = false;
    public bool IsDayTimeAlram
    {
        get
        { 
            _IsDayTimeAlram = FileManager.instance.LoadDataOption<bool>(enSaveFileType.GameOption, "daytimeAlram", true);
            return _IsDayTimeAlram;
        }
    }

    public void SetDayTimeAlram(bool value, DelegateOnceEventDoneWithFlag subscribeComplete)
    {
        _IsDayTimeAlram = value;

        FileManager.instance.SaveDataOption<bool>(enSaveFileType.GameOption, "daytimeAlram", value);

        if(subscribeComplete != null)
            subscribeComplete(value);

#if FCM && !UNITY_EDITOR
        if (value == true)
        {
            Firebase.Messaging.FirebaseMessaging.SubscribeAsync(_daytimeTopic).ContinueWith(task =>
            {
                LogTaskCompletion(task, "daytimeSubscribeAsync");
            });
        }
        else
        {
            Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync(_daytimeTopic).ContinueWith(task =>
            {
                LogTaskCompletion(task, "daytimeUnsubscribeAsync");
            });
        }
#endif
    }

    /// <summary>
    /// 야간
    /// </summary>
    private bool _IsNightAlram = false;
    public bool IsNightAlram
    {
        get
        {
            _IsNightAlram = FileManager.instance.LoadDataOption<bool>(enSaveFileType.GameOption, "nightAlram", false);
            return _IsNightAlram;
        }
    }

    public void SetNightAlram(bool value, DelegateOnceEventDoneWithFlag subscribeComplete)
    {
        _IsNightAlram = value;

        FileManager.instance.SaveDataOption<bool>(enSaveFileType.GameOption, "nightAlram", value);

        if (subscribeComplete != null)
            subscribeComplete(value);

#if FCM && !UNITY_EDITOR
        if (value == true)
        {
            Firebase.Messaging.FirebaseMessaging.SubscribeAsync(_nightTopic).ContinueWith(task =>
            {
                LogTaskCompletion(task, "nightSubscribeAsync");
            });
        }
        else
        {
            Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync(_nightTopic).ContinueWith(task =>
            {
                LogTaskCompletion(task, "nightUnsubscribeAsync");
            });
        }
#endif
    }

#if FCM && !UNITY_EDITOR
    private void LogTaskCompletion(System.Threading.Tasks.Task task, string operation)
    {
#if DEBUG_LOG
        if (task.IsCanceled)
        {
            Debug.LogError(operation + " canceled.");
        }
        else if (task.IsFaulted)
        {
            Debug.LogError(operation + " encounted an error.");

            foreach (System.Exception exception in task.Exception.Flatten().InnerExceptions)
            {
                string errorCode = "";
                Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                if (firebaseEx != null)
                {
                    errorCode = string.Format("Error.{0}: ", ((Firebase.Messaging.Error)firebaseEx.ErrorCode).ToString());
                }

                Debug.LogError(errorCode + exception.ToString());
            }
        }
        else if (task.IsCompleted)
        {
            Debug.Log(operation + " completed");
        }
#endif
    }
#endif

    /// <summary>
    /// 초록색 New
    /// </summary>
    private bool _isOtherNewInit = false;
    private bool _isOtherNew = true;
    public bool isOtherNew
    {
        set
        {
            _isOtherNew = value;
            FileManager.instance.SaveDataOption<bool>(enSaveFileType.GameOption, "OtherNew", _isOtherNew);
            if (UIControlManager.instance)
                UIControlManager.instance.isMainButtonRefesh = true;
        }
        get
        {
            if (_isOtherNewInit == false)
            {
                _isOtherNew = FileManager.instance.LoadDataOption<bool>(enSaveFileType.GameOption, "OtherNew", true);
                _isOtherNewInit = true;
            }

            return _isOtherNew;
        }
    }
}


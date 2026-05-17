using UnityEngine;
using AudioName;

public class AudioLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        AudioManager.Instance.LoadBGM(BGMName.GAME_BGM_NAME, "Audio/BGM/BGM");

        AudioManager.Instance.LoadSE(SEName.CLEAR_SE_NAME, "Audio/SE/clear");
        AudioManager.Instance.LoadSE(SEName.DOOR_SE_NAME, "Audio/SE/door");
        AudioManager.Instance.LoadSE(SEName.JUMP_SE_NAME, "Audio/SE/jump");
        AudioManager.Instance.LoadSE(SEName.PUNCH_SE_NAME, "Audio/SE/punch");
        AudioManager.Instance.LoadSE(SEName.ROBOT_SE_NAME, "Audio/SE/robot");
        AudioManager.Instance.LoadSE(SEName.SWITCH_SE_NAME, "Audio/SE/switch");
    }

    public void Final()
    {
        AudioManager.Instance.UnloadBGM(BGMName.GAME_BGM_NAME);

        AudioManager.Instance.UnloadSE(SEName.CLEAR_SE_NAME);
        AudioManager.Instance.UnloadSE(SEName.DOOR_SE_NAME);
        AudioManager.Instance.UnloadSE(SEName.JUMP_SE_NAME);
        AudioManager.Instance.UnloadSE(SEName.PUNCH_SE_NAME);
        AudioManager.Instance.UnloadSE(SEName.ROBOT_SE_NAME);
        AudioManager.Instance.UnloadSE(SEName.SWITCH_SE_NAME);
    }
}

namespace AudioName
{
    public static class BGMName
    {
        public static string GAME_BGM_NAME = "GameBGM";
    }

    public static class SEName
    {
        public static string CLEAR_SE_NAME = "ClearSE";
        public static string DOOR_SE_NAME = "DoorSE";
        public static string JUMP_SE_NAME = "JumpSE";
        public static string PUNCH_SE_NAME = "PunchSE";
        public static string ROBOT_SE_NAME = "RobotSE";
        public static string SWITCH_SE_NAME = "SwitchSE";
    }
}
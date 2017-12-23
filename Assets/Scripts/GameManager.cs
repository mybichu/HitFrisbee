using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 游戏状态枚举
    /// </summary>
    public enum GameState
    {
        START,
        GAME,
        END
    }

    private GameState gameState;  //当前游戏状态
    private AudioSource bgAudio;  //背景音乐
    private Weapon weapon;   //武器脚本
    private FeiPanManager feipanManager;  //飞盘管理脚本
    private int Score = 0;  //得分  
    private float time = 20; //剩余时间
    private bool TimeFlag = false;//时间标志位


    private GameObject m_StartUI;  //开始UI
    private GameObject m_GameUI;   //游戏UI
    private GameObject m_EndUI;   //结束UI
    private GUIText ScoreText;   //得分文本框
    private GUIText TimeText;   //时间文本框
    private GUIText TotalScoreText;   //总得分文本框

    void Start()
    {

        m_StartUI = GameObject.Find("StartUI");
        m_GameUI = GameObject.Find("GameUI");
        m_EndUI = GameObject.Find("EndUI");
        bgAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        weapon = GameObject.Find("Weapon").GetComponent<Weapon>();
        feipanManager = GameObject.Find("FeiPanParent").GetComponent<FeiPanManager>();
        ScoreText = GameObject.Find("Score").GetComponent<GUIText>();
        TimeText = GameObject.Find("GameTime").GetComponent<GUIText>();
        TotalScoreText = GameObject.Find("TotalScore").GetComponent<GUIText>();

        ChangeGameState(GameState.START);


    }

    void Update()
    {
        CountDown();
    }

    /// <summary>
    /// 切换游戏状态
    /// </summary>
    /// <param name="state">目标状态</param>
    public void ChangeGameState(GameState state)
    {
        //存储传递过来的状态
        gameState = state;

        if (gameState == GameState.START)       //开始游戏
        {
            m_StartUI.SetActive(true);
            m_GameUI.SetActive(false);
            m_EndUI.SetActive(false);
       
            bgAudio.Stop();
            weapon.setCanMove(false);
            feipanManager.StopCreateFeiPan();
        }
        else if (gameState == GameState.GAME)   //游戏中
        {
            m_StartUI.SetActive(false);
            m_GameUI.SetActive(true);
            m_EndUI.SetActive(false);

            bgAudio.Play();
            weapon.setCanMove(true);
            StartTime();
            feipanManager.StartCreateFeiPan();
        }
        else if (gameState == GameState.END)    //结束游戏
        {
            m_StartUI.SetActive(false);
            m_GameUI.SetActive(false);
            m_EndUI.SetActive(true);

            bgAudio.Stop();
            weapon.setCanMove(false);
            feipanManager.StopCreateFeiPan();
            feipanManager.RemoveFeiPan();
        }

    }

    /// <summary>
    /// 增加分数
    /// </summary>
    public void AddScore()
    {
        Score++;
        ScoreText.text = "得分：" + Score;
    }

    /// <summary>
    /// 开始倒计时
    /// </summary>
    public void StartTime()
    {
        TimeFlag = true;
    }

    /// <summary>
    /// 倒计时
    /// </summary>
    void CountDown()
    {
        if (TimeFlag)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                ChangeGameState(GameState.END);
                TotalScoreText.text = "总得分：" + Score;
            }
            TimeText.text = "时间：" + time + "秒";
        }
    }

    public void Reset()
    {
        time = 20f;
        Score = 0;
        ScoreText.text = "得分：" + Score;


    }
    void Awake()
    {
        Screen.SetResolution(757, 337, false);
    }

}

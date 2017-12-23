using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{

    private Ray ray;          //射线
    private RaycastHit hit;   //射线碰撞结构体
    private Transform trans;   //武器的变换组件
    private Transform point;   //枪口位置组件
    private LineRenderer line;  //线渲染器
    private AudioSource audio;   //声音源
    private bool canMove = false; //控制手臂
    private GameManager gameMan;    //游戏管理脚本

    void Start()
    {
        trans = gameObject.GetComponent<Transform>();
        point = trans.FindChild("Point");
        line = point.gameObject.GetComponent<LineRenderer>();
        audio = gameObject.GetComponent<AudioSource>();
        gameMan = GameObject.Find("UI").GetComponent<GameManager>();
    }

    void Update()
    {
        //武器移动
        WeaponMove();

    }

    void WeaponMove()
    {
        if (canMove)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                //控制手臂朝向碰撞点
                trans.LookAt(hit.point);
                //设置LineRenderer位置
                line.SetPosition(0, point.position);
                line.SetPosition(1, hit.point);

                //飞盘射击
                if (hit.collider.tag == "FeiPan" && Input.GetMouseButtonDown(0))
                {
                    //播放射击音效
                    audio.Play();

                    //通过碰撞到的子物体查找到父物体
                    Transform parent = hit.collider.gameObject.GetComponent<Transform>().parent;

                    //通过父物体查找到所有的子物体
                    Transform[] feiPans = parent.GetComponentsInChildren<Transform>();

                    //给所有子物体添加刚体组件，模拟破碎下落效果
                    for (int i = 0; i < feiPans.Length; i++)
                    {
                        if (feiPans[i].GetComponent<Rigidbody>() == null)
                        {
                            feiPans[i].gameObject.AddComponent<Rigidbody>();
                        }
                       
                    }

                    //增加分数
                    gameMan.AddScore();

                    //2秒后父物体销毁
                    GameObject.Destroy(parent.gameObject, 2.0f);
                }

            }
        }
    }

    public void setCanMove(bool flag)
    {
        canMove = flag;
    }
}

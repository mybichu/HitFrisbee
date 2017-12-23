using UnityEngine;
using System.Collections;

public class FeiPanManager : MonoBehaviour
{

    public GameObject prefab_FeiPan; //需要使用的飞盘预制体
    private Transform ParentTrans;  //父物体的变换组件

    void Start()
    {
        ParentTrans = gameObject.GetComponent<Transform>();
    }

    /// <summary>
    /// 开始生产飞盘
    /// </summary>
    public void StartCreateFeiPan()
    {
        InvokeRepeating("CreateFeiPan", 2.0f, 2.0f);
    }

    /// <summary>
    /// 停止生产飞盘
    /// </summary>
    public void StopCreateFeiPan()
    {
        CancelInvoke("CreateFeiPan");
    }

    /// <summary>
    /// 生成飞盘
    /// </summary>
    void CreateFeiPan()
    {
        for (int i = 0; i < 3; i++)
        {
            //飞盘生成的随机位置
            float posx = Random.Range(-6.0f, 6.0f);
            float posy = Random.Range(0.5f, 4.0f);
            float posz = Random.Range(6.0f, 20.0f);
            Vector3 pos = new Vector3(posx, posy, posz);

            //实例化飞盘
            GameObject go = GameObject.Instantiate(prefab_FeiPan, pos, Quaternion.identity) as GameObject;

            //把新飞盘的父物体设置为FeiPanParent
            go.gameObject.GetComponent<Transform>().SetParent(ParentTrans);

        }
    }

    /// <summary>
    /// 移除所有飞盘
    /// </summary>
    public void RemoveFeiPan()
    {
        Transform[] pf = ParentTrans.GetComponentsInChildren<Transform>();
        for(int i=1;i<pf.Length;i++)
        {
            GameObject.Destroy(pf[i].gameObject);
        }
    }
}

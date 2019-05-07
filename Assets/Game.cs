using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] int row;
    [SerializeField] int line;
    [SerializeField] Transform center;//棋盘放置容器
    [SerializeField] Node m_node;//预制体
    [SerializeField] Camera camera;
    [SerializeField] Text content;
    public int target = 2048;//目标分数
    Node[,] nodesPanel;//棋盘元素存放
    public List<Node> EmptyNodes = new List<Node>();//棋盘中没有数字的元素列表
    public List<Node> NotEmptyNodes = new List<Node>();//棋盘中有数字的元素列表
    // Start is called before the first frame update
    void Start()
    {
        nodesPanel = new Node[line,row];
        CreatePanel();
        SetNum();
        GetBrother();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //生成棋盘
    public void CreatePanel()
    {
        if (row <= 0 || line <= 0) return;
        int num = 0;
        for(int i = 0; i < row; i ++)
        {
            for (int j = 0; j < line; j++)
            {
                num++;
                Node go = Instantiate(m_node,center.transform);
                go.transform.localPosition = new Vector3(j*100 - line * 100/2f + 50,i*100 - row *100/2f +50,0);
                go.ID = 0;
                go.position = new int[] { j,i };//设置位置
                nodesPanel[j,i] = go;
                EmptyNodes.Add(go);
            }
        }
    }
    //每次刷新数字
    public void SetNum()
    {
        if (EmptyNodes == null || EmptyNodes.Count <= 0) return;
        int randomCount = Random.Range(1,3);
        for (int i = 0; i < randomCount; i++)
        {
            if(randomCount <= EmptyNodes.Count)
            {
                int index = Random.Range(0, EmptyNodes.Count);
                EmptyNodes[index].ID = 1;
                NotEmptyNodes.Add(EmptyNodes[index]);
                EmptyNodes.RemoveAt(index);
            }
        }
    }
    //获取每个棋盘元素的周围元素
    public void GetBrother()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < line; j++)
            {
                Node item = nodesPanel[j, i];
                item.shang = i+1 >= row ?null: nodesPanel[j, i + 1];
                item.xia = i -1 <0 ? null : nodesPanel[j, i - 1];
                item.zuo = j - 1 < 0 ? null : nodesPanel[j -1, i];
                item.you = j +1 >= line ? null : nodesPanel[j+1, i];
            }
        }
    }
    public void TapShang()
    {
        bool isOK = false;
        while(!isOK)
        {
            int NoMove = 0;
            NotEmptyNodes.Sort((x, y) => {
                return y.position[1].CompareTo(x.position[1]);
            });
            for (int i = 0; i < NotEmptyNodes.Count; i++)
            {
                if (NotEmptyNodes[i].shang != null)
                {
                    if (NotEmptyNodes[i].shang.ID == 0)
                    {
                        NoMove++;
                        NotEmptyNodes[i].shang.ID = NotEmptyNodes[i].ID;
                        NotEmptyNodes[i].ID = 0;
                    }
                    else if (NotEmptyNodes[i].shang.ID == NotEmptyNodes[i].ID)
                    {
                        NoMove++;
                        NotEmptyNodes[i].shang.ID *= 2;
                        NotEmptyNodes[i].ID = 0;
                    }
                }
            }
            GetNotAndEmpty();
            if(NoMove ==0)
            {
                SetNum();
                isOK = true;
            }
        }
    }
    public void TapXia()
    {
        bool isOK = false;
        while (!isOK)
        {
            int NoMove = 0;
            NotEmptyNodes.Sort((x, y) => {
                return x.position[1].CompareTo(y.position[1]);
            });
            for (int i = 0; i < NotEmptyNodes.Count; i++)
            {
                if (NotEmptyNodes[i].xia != null)
                {
                    if (NotEmptyNodes[i].xia.ID == 0)
                    {
                        NoMove++;
                        NotEmptyNodes[i].xia.ID = NotEmptyNodes[i].ID;
                        NotEmptyNodes[i].ID = 0;
                    }
                    else if (NotEmptyNodes[i].xia.ID == NotEmptyNodes[i].ID)
                    {
                        NoMove++;
                        NotEmptyNodes[i].xia.ID *= 2;
                        NotEmptyNodes[i].ID = 0;
                    }
                }
            }
            GetNotAndEmpty();
            if (NoMove == 0)
            {
                SetNum();
                isOK = true;
            }
        }
    }
    public void TapZuo()
    {
        bool isOK = false;
        while (!isOK)
        {
            int NoMove = 0;
            NotEmptyNodes.Sort((x, y) => {
                return x.position[1].CompareTo(y.position[1]);
            });
            for (int i = 0; i < NotEmptyNodes.Count; i++)
            {
                if (NotEmptyNodes[i].zuo != null)
                {
                    if (NotEmptyNodes[i].zuo.ID == 0)
                    {
                        NoMove++;
                        NotEmptyNodes[i].zuo.ID = NotEmptyNodes[i].ID;
                        NotEmptyNodes[i].ID = 0;
                    }
                    else if (NotEmptyNodes[i].zuo.ID == NotEmptyNodes[i].ID)
                    {
                        NoMove++;
                        NotEmptyNodes[i].zuo.ID *= 2;
                        NotEmptyNodes[i].ID = 0;
                    }
                }
            }
            GetNotAndEmpty();
            if (NoMove == 0)
            {
                SetNum();
                isOK = true;
            }
        }
    }
    public void TapYou()
    {
        bool isOK = false;
        while (!isOK)
        {
            int NoMove = 0;
            NotEmptyNodes.Sort((x, y) => {
                return y.position[0].CompareTo(x.position[0]);
            });
            for (int i = 0; i < NotEmptyNodes.Count; i++)
            {
                if (NotEmptyNodes[i].you != null)
                {
                    if (NotEmptyNodes[i].you.ID == 0)
                    {
                        NoMove++;
                        NotEmptyNodes[i].you.ID = NotEmptyNodes[i].ID;
                        NotEmptyNodes[i].ID = 0;
                    }
                    else if (NotEmptyNodes[i].you.ID == NotEmptyNodes[i].ID)
                    {
                        NoMove++;
                        NotEmptyNodes[i].you.ID *= 2;
                        NotEmptyNodes[i].ID = 0;
                    }
                }
            }
            GetNotAndEmpty();
            if (NoMove == 0)
            {
                SetNum();
                isOK = true;
            }
        }
    }
    public void GetNotAndEmpty()
    {
        NotEmptyNodes.Clear();
        EmptyNodes.Clear();
        int Max = 0;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < line; j++)
            {
                if (nodesPanel[j, i].ID > Max)
                {
                    Max = nodesPanel[j, i].ID;
                    content.text = Max.ToString();
                }
                if (nodesPanel[j, i].ID == target)
                {
                    content.text = "你赢了！！！";
                    break;
                }
                if (nodesPanel[j, i].ID > 0)
                {
                    NotEmptyNodes.Add(nodesPanel[j, i]);
                }
                else
                {
                    EmptyNodes.Add(nodesPanel[j, i]);
                }
            }
        }
    }
    //重置游戏
    public void ResetGame()
    {
        NotEmptyNodes.Clear();
        EmptyNodes.Clear();
        content.text = "0";
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < line; j++)
            {
                nodesPanel[j, i].ID = 0;
                if (nodesPanel[j, i].ID > 0)
                {
                    NotEmptyNodes.Add(nodesPanel[j, i]);
                }
                else
                {
                    EmptyNodes.Add(nodesPanel[j, i]);
                }
            }
        }
        SetNum();
    }
}
 
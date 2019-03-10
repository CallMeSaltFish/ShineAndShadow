using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Rule : MonoBehaviour
{
    /*要实例化的物体*/
    public GameObject go;
    public GameObject[] newRankIndex;

    public GameObject panel;

    //public Text indexText;

    private int indexM;

    private int[] save = new int[8];
    private int num;
    private string saveIntStr;

    void Start()
    {
        //获取数据
        indexM = PlayerPrefs.GetInt("index");
        //indexText.text = "点击次数:" + indexM.ToString();

        //获取存储的排行榜中的数据
        for (int i = 0; i < 8; i++)
        {
            string saveIntStrS = saveIntStr + i.ToString();
            //Debug.Log(saveIntStr);
            save[i] = PlayerPrefs.GetInt(saveIntStrS,0);
        }
        //添加新数据并排序（从小到大）
        for (int i = 0; i < 8; i++)
        {
            if (save[i] == null || save[i] == 0)
            {
                save[i] = indexM;
                num = i;
                for (int m = 0; m < num + 1; m++)
                {
                    int t = save[m];
                    int n = m;
                    while ((n > 0) && (save[n - 1] < t))
                    {
                        save[n] = save[n - 1];
                        --n;
                    }
                }

                break;
            }
            else
            {
                int n = 7;
                if (indexM > save[7])
                {
                    while (save[n - 1] < indexM)
                    {
                        save[n] = save[n - 1];
                        --n;
                        save[n] = indexM;
                        if (n == 0)
                        {
                            break;
                        }
                    }
                    break;
                }
            }
        }
        //保存数据
        for (int j = 0; j < 8; j++)
        {
            string saveIntStrI = saveIntStr + j.ToString();
            PlayerPrefs.SetInt(saveIntStrI, save[j]);
        }
        //UI显示
        for (int i = 0; i < newRankIndex.Length; i++)
        {
            string saveIntStr0 = saveIntStr + i.ToString();
            newRankIndex[i] = GameObject.Instantiate(go, transform.position, transform.rotation) as GameObject;
            newRankIndex[i].transform.SetParent(panel.transform);
            newRankIndex[i].GetComponent<Text>().text = PlayerPrefs.GetInt(saveIntStr0).ToString();
        }
    }
}

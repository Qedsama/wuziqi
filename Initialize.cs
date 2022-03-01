using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour
{
    public GameObject win1, win2;
    GameObject lu, ld, ru, rd,qipan,heiqi,baiqi;
    float l, d, u, r;
    public bool aidone;
    bool player1, player2,boo1,boo2,boo3;
    int num1=1, num2=1;
    GameObject[] hei = new GameObject[361];
    GameObject[] bai = new GameObject[361];
    public int[,] done = new int[25, 25];
    Vector3 mouse = new Vector3();
    bool state;
    public int iii = 2, jjj = 2;
    Vector3 Posi(int i,int j)
    {
        float x = ((i * r) + ((18 - i) * l)) / 18f;
        float y = ((j * d) + ((18 - j) * u)) / 18f;
        Vector3 posi = new Vector3(x,y,0);
        return posi;
    }
    public static void RemoveAllChildren(GameObject parent)
    {
        Transform transform;
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            transform = parent.transform.GetChild(i);
            Destroy(transform.gameObject);
        }
    }
    public void Newgametime()
    {
        Invoke("Ur", 0.5f);
    }
    void Ur()
    {
        win1.SetActive(false);
        win2.SetActive(false);
        Newgame();
    }
    public void Newgame()
    {
        win1.SetActive(false);
        win2.SetActive(false);
        num1 = 1;num2 = 1;
        RemoveAllChildren(qipan);
        player1 = true;
        player2 = false;
        boo1 = false;
        boo2 = false;
        boo3 = false;
        for (int i = 0; i <= 20; i++) for (int j = 0; j <= 20; j++) done[i, j] = 0;
    }
    public void Exit()
    {
        Application.Quit();
    }
    void Set1() { player1 = true; }
    void Set2() { player2 = true; }
    bool Play1()
    {
        mouse = Input.mousePosition;
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        mouse.z = 0;
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i <= 18; i++)
            {
                for(int j = 0; j <= 18; j++)
                {
                    float dx = Posi(i, j).x - mouse.x;
                    float dy = Posi(i, j).y - mouse.y;
                    if (dx * dx + dy * dy < 0.01f && done[i, j] == 0)
                    {
                        player1 = false;
                        hei[num1]=Instantiate(heiqi);
                        hei[num1].transform.parent = qipan.transform;
                        hei[num1].transform.position = Posi(i, j);
                        num1++;
                        done[i, j] = 1;
                        return true;
                    }
                }
            }
        }
        return false;
    }
    bool Play2()
    {
        Aicalc();
        int i = iii;
        int j = jjj;
        aidone = false;
        if (done[i,j]==0)
        {
            player2 = false;
            bai[num2] = Instantiate(baiqi);
            bai[num2].transform.parent = qipan.transform;
            bai[num2].transform.position = Posi(i, j);
            num2++;
            done[i, j] = 2;
            return true;
        }
        else return false;
    }
    bool Youxie(int i,int j)
    {
        if (i >= 4)
        {
            if (done[i, j] == done[i - 1, j + 1] && done[i, j] == done[i - 2, j + 2] && done[i, j] == done[i - 3, j + 3] && done[i, j] == done[i - 4, j + 4])
            {
                return true;
            }
            else return false;
        }
        else return false;
    }
    int Over()
    {
        for(int i = 0; i <= 18; i++)
        {
            for(int j = 0; j <= 18; j++)
            {
                if (done[i, j] != 1 && done[i, j] != 2) continue;
                else if ((done[i, j] == done[i, j + 1] && done[i, j] == done[i, j + 2] && done[i, j] == done[i, j + 3] && done[i, j] == done[i, j + 4]) ||//ÅÐ¶Ïºá
                (done[i, j] == done[i + 1, j] && done[i, j] == done[i + 2, j] && done[i, j] == done[i + 3, j] && done[i, j] == done[i + 4, j]) ||//ÅÐ¶Ï×Ý
                Youxie(i,j)||//ÅÐ¶ÏÓÒÐ±
                (done[i, j] == done[i + 1, j + 1] && done[i, j] == done[i + 2, j + 2] && done[i, j] == done[i + 3, j + 3] && done[i, j] == done[i + 4, j + 4]))
                {
                    if (done[i, j] == 1) return 1;
                    else if (done[i, j] == 2) return 2;
                }
                else continue;
            }
        }
        return 0;
    }
    void Start()
    {
        lu = GameObject.Find("lu");
        ld = GameObject.Find("ld");
        ru = GameObject.Find("ru");
        rd = GameObject.Find("rd");
        l = (lu.transform.position.x + ld.transform.position.x)/ 2;
        d = (rd.transform.position.y + ld.transform.position.y) / 2;
        u = (ru.transform.position.y + lu.transform.position.y) / 2;
        r = (ru.transform.position.x + rd.transform.position.x) / 2;
        player1 = true;
        qipan = GameObject.Find("qizi");
        heiqi = GameObject.Find("heiqi");
        baiqi = GameObject.Find("baiqi");
        for (int i = 0; i <= 23; i++) for (int j = 0; j <= 23; j++) done[i, j] = 0;
    }

    void Update()
    {
        if (player1 && !player2)
        {
            if (Play1()) boo1 = true;

        }
        if (player2 && !player1)
        {
            if (Play2()) boo2 = true;
        }
        if (boo1)
        {
            float time1=0;
            time1 += Time.time;
            if (time1 > 1f) { Set2();boo1 = false;}
        }
        if (boo2)
        {
            float time2=0;
            time2 += Time.time;
            if (time2 > 1f) { Set1(); boo2 = false; }
        }
        if (Over()>0&&!boo3)
        {
            if (Over() == 1)
            {
                win1.SetActive(true);
            }
            else win2.SetActive(true);
            player1 = false; player2 = false;boo3 = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }






    int Scorecalc1(int num)
    {
        if (num == 5) return 1000000000;
        else if (num == 4) return 100000;
        else if (num == 3) return 30000;
        else if (num == 2) return 10000;
        else if (num == 1) return 500;
        else return 0;
    }
    int Scorecalc2(int num)
    {
        if (num == 5) return 1000000000;
        else if (num == 4) return 80000;
        else if (num == 3) return 20000;
        else if (num == 2) return 10000;
        else if (num == 1) return 500;
        else return 0;
    }
    int Heng(int i, int j)
    {
        int num1 = 0; int num2 = 0;
        for (int r = i; r <= i + 4; r++)
        {
            if (done[r, j] == 1) num1++;
            if (done[r, j] == 2) num2++;
        }
        int score1 = Scorecalc1(num1);
        int score2 = Scorecalc2(num2);
        if (score1 > 0 && score2 > 0) return 10;
        else if (score1 == 0) return score2;
        else return -score1;
    }
    int Shu(int i, int j)
    {
        int num1 = 0; int num2 = 0;
        for (int r = j; r <= j + 4; r++)
        {
            if (done[i, r] == 1) num1++;
            if (done[i, r] == 2) num2++;
        }
        int score1 = Scorecalc1(num1);
        int score2 = Scorecalc2(num2);
        if (score1 > 0 && score2 > 0) return 10;
        else if (score1 == 0) return score2;
        else return -score1;
    }
    int Youxia(int i, int j)
    {
        int num1 = 0; int num2 = 0;
        for (int r = 0; r <= 4; r++)
        {
            if (done[i + r, j + r] == 1) num1++;
            if (done[i + r, j + r] == 2) num2++;
        }
        int score1 = Scorecalc1(num1);
        int score2 = Scorecalc2(num2);
        if (score1 > 0 && score2 > 0) return 10;
        else if (score1 == 0) return score2;
        else return -score1;
    }
    int Zuoxia(int i, int j)
    {
        int num1 = 0; int num2 = 0;
        for (int r = 0; r <= 4; r++)
        {
            if (done[i - r, j + r] == 1) num1++;
            if (done[i - r, j + r] == 2) num2++;
        }
        int score1 = Scorecalc1(num1);
        int score2 = Scorecalc2(num2);
        if (score1 > 0 && score2 > 0) return 10;
        else if (score1 == 0) return score2;
        else return -score1;
    }
    int Calc(int i, int j,int c,int boo)
    {
        if (c <= 0) return 0;
        int score = 0;
        if (done[i, j] != 0) return -10000000;
        done[i, j] = boo;
        for (int r = 0; r < 19; r++)
        {
            for (int k = 0; k < 19; k++)
            {
                score += Heng(r, k);
                score += Shu(r, k);
                score += Youxia(r, k);
            }
        }
        for (int r = 4; r < 19; r++)
        {
            for (int k = 0; k < 15; k++)
            {
                score += Zuoxia(r, k);
            }
        }
        for (int li = 3; li <= 15; li++)
        {
            for (int lj = 3; lj<=15; lj++)
            {
                score += Calc(li, lj, c-1,3-boo) / 600000*num1;
            }
        }

        done[i, j] = 0;
        return score;
    }
    void Aicalc()
    {
        int ii = 0, jj = 0, score = -10000000;
        for (int i = 0; i < 19; i++)
        {
            for (int j = 0; j < 19; j++)
            {
                if (done[i, j] != 0) continue;
                int r = Calc(i, j,2,2);
                if (r >= score) { score = r; ii = i; jj = j; }
            }
        }
        iii = ii; jjj = jj;
    }
}

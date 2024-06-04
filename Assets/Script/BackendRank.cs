using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ڳ� SDK namespace �߰�
using BackEnd;
using System.Text;

// 3. ��ŷ ��� �����ϱ�
public class BackendRank
{
    // Step 1. �����غ�
    private static BackendRank _instance = null;

    public static BackendRank Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new BackendRank();
            }

            return _instance;
        }
    }
    
    // Step 2. ��ŷ ����ϱ� ���� �߰�
    public void RankInsert(int score)
    {
        // [���� �ʿ�] '������ UUID ��'�� '�ڳ� �ܼ� > ��ŷ ����'���� ������ ��ŷ�� UUID ������ �������ּ���.
        //string rankUUID = "������ UUID ��";
        string rankUUID = "1be265c0-fb0e-11ee-a57f-7956f288c7a5";

        string tableName = "USER_DATA";
        string rowInDate = string.Empty;

        // ��ŷ�� �����ϱ� ���ؼ��� ���� �����Ϳ��� ����ϴ� �������� inDate���� �ʿ��մϴ�.
        // ���� �����͸� �ҷ��� ��, �ش� �������� inDate���� �����ϴ� �۾��� �ؾ��մϴ�.
        Debug.Log("������ ��ȸ�� �õ��մϴ�.");
        var bro = Backend.GameData.GetMyData(tableName, new Where());

        if( bro.IsSuccess() == false )
        {
            Debug.LogError("������ ��ȸ �� ������ �߻��߽��ϴ�. : " + bro);
            return;
        }

        Debug.Log("������ ��ȸ�� �����߽��ϴ�. : " + bro);

        if(bro.FlattenRows().Count > 0)
        {
            rowInDate = bro.FlattenRows()[0]["inDate"].ToString();
        }
        else
        {
            Debug.Log("�����Ͱ� �������� �ʽ��ϴ�. ������ ������ �õ��մϴ�.");
            var bro2 = Backend.GameData.Insert(tableName);

            if(bro.IsSuccess() == false)
            {
                Debug.LogError("������ ���� �� ������ �߻��߽��ϴ�. : " + bro2);
                return;
            }

            Debug.Log("������ ���Կ� �����߽��ϴ� : " + bro2);

            rowInDate = bro2.GetInDate();
        }

        // ����� rowIndate�� ���� ������ �����ϴ�.
        Debug.Log("�� ���� ������ rowInDate : " + rowInDate);

        Param param = new Param();
        param.Add("level", score);

        // ����� rowIndate�� ���� �����Ϳ� param������ ������ �����ϰ� ��ŷ�� �����͸� ������Ʈ�մϴ�.
        Debug.Log("��ŷ ������ �õ��մϴ�.");
        var rankBro = Backend.URank.User.UpdateUserScore(rankUUID, tableName, rowInDate, param);

        if(rankBro.IsSuccess() == false)
        {
            Debug.LogError("��ŷ ��� �� ������ �߻��߽��ϴ�. : " + rankBro);
            return;
        }

        Debug.Log("��ŷ ���Կ� �����߽��ϴ�. : " + rankBro);
    }

    // Step 3. ��ŷ �ҷ����� ���� �߰�
    public void RankGet()
    {

        //string rankUUID = "<������ UUID ��>";
        string rankUUID = "1be265c0-fb0e-11ee-a57f-7956f288c7a5";
        var bro = Backend.URank.User.GetRankList(rankUUID);

        if(bro.IsSuccess() == false)
        {
            Debug.LogError("��ŷ ��ȸ�� ������ �߻��߽��ϴ�. : " + bro);
            return;
        }
        Debug.Log("��ŷ ��ȸ�� �����߽��ϴ�. : " + bro);

        Debug.Log("�� ��ŷ ��� ���� �� : " + bro.GetFlattenJSON()["totalCount"].ToString());

        foreach(LitJson.JsonData jsonData in bro.FlattenRows())
        {
            StringBuilder info = new StringBuilder();

            info.AppendLine("���� : " + jsonData["rank"].ToString());
            info.AppendLine("�г��� : " + jsonData["nickname"].ToString());
            info.AppendLine("���� : " + jsonData["score"].ToString());
            info.AppendLine("gamerInDate : " + jsonData["gamerInDate"].ToString());
            info.AppendLine("���Ĺ�ȣ : " + jsonData["index"].ToString());
            info.AppendLine();
            Debug.Log(info);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ڳ� SDK namespace �߰�
using BackEnd;
using System.Text;

// 4. ��Ʈ ��� �����ϱ�
public class BackendChart
{
    private static BackendChart _instance = null;

    public static BackendChart Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new BackendChart();
            }

            return _instance;
        }
    }
    
    // Step 3. ��Ʈ ���� �������� ���� �߰�
    public void ChartGet(string chartId)
    {
        Debug.Log($"{chartId}�� ��Ʈ �ҷ����⸦ ��û�մϴ�.");
        var bro = Backend.Chart.GetChartContents(chartId);

        if(bro.IsSuccess() == false)
        {
            Debug.LogError($"{chartId}�� ��Ʈ�� �ҷ����� ��, ������ �߻��߽��ϴ�. : " + bro);
            return;
        }

        Debug.Log("��Ʈ �ҷ����⿡ �����߽��ϴ�. : " + bro);
        foreach(LitJson.JsonData gameData in bro.FlattenRows())
        {
            StringBuilder content = new StringBuilder();
            content.AppendLine("itemID : " + int.Parse(gameData["itemId"].ToString()));
            content.AppendLine("itemName : " + gameData["itemName"].ToString());
            content.AppendLine("itemType : " + gameData["itemType"].ToString());
            content.AppendLine("itemPower : " + long.Parse(gameData["itemPower"].ToString()));
            content.AppendLine("itemInfo : " + gameData["itemInfo"].ToString());

            Debug.Log(content.ToString());
        }
    }
}

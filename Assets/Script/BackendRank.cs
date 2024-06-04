using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 뒤끝 SDK namespace 추가
using BackEnd;
using System.Text;

// 3. 랭킹 기능 구현하기
public class BackendRank
{
    // Step 1. 사전준비
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
    
    // Step 2. 랭킹 등록하기 내용 추가
    public void RankInsert(int score)
    {
        // [변경 필요] '복사한 UUID 값'을 '뒤끝 콘솔 > 랭킹 관리'에서 생성한 랭킹의 UUID 값으로 변경해주세요.
        //string rankUUID = "복사한 UUID 값";
        string rankUUID = "1be265c0-fb0e-11ee-a57f-7956f288c7a5";

        string tableName = "USER_DATA";
        string rowInDate = string.Empty;

        // 랭킹을 삽입하기 위해서는 게임 데이터에서 사용하는 데이터의 inDate값이 필요합니다.
        // 따라서 데이터를 불러온 후, 해당 데이터의 inDate값을 추출하는 작업을 해야합니다.
        Debug.Log("데이터 조회를 시도합니다.");
        var bro = Backend.GameData.GetMyData(tableName, new Where());

        if( bro.IsSuccess() == false )
        {
            Debug.LogError("데이터 조회 중 문제가 발생했습니다. : " + bro);
            return;
        }

        Debug.Log("데이터 조회에 성공했습니다. : " + bro);

        if(bro.FlattenRows().Count > 0)
        {
            rowInDate = bro.FlattenRows()[0]["inDate"].ToString();
        }
        else
        {
            Debug.Log("데이터가 존재하지 않습니다. 데이터 삽입을 시도합니다.");
            var bro2 = Backend.GameData.Insert(tableName);

            if(bro.IsSuccess() == false)
            {
                Debug.LogError("데이터 삽입 중 문제가 발생했습니다. : " + bro2);
                return;
            }

            Debug.Log("데이터 삽입에 성공했습니다 : " + bro2);

            rowInDate = bro2.GetInDate();
        }

        // 추출된 rowIndate의 값은 다음과 같습니다.
        Debug.Log("내 게임 정보의 rowInDate : " + rowInDate);

        Param param = new Param();
        param.Add("level", score);

        // 추출된 rowIndate를 가진 데이터에 param값으로 수정을 진행하고 랭킹에 데이터를 업데이트합니다.
        Debug.Log("랭킹 삽입을 시도합니다.");
        var rankBro = Backend.URank.User.UpdateUserScore(rankUUID, tableName, rowInDate, param);

        if(rankBro.IsSuccess() == false)
        {
            Debug.LogError("랭킹 등록 중 오류가 발생했습니다. : " + rankBro);
            return;
        }

        Debug.Log("랭킹 삽입에 성공했습니다. : " + rankBro);
    }

    // Step 3. 랭킹 불러오기 내용 추가
    public void RankGet()
    {

        //string rankUUID = "<복사한 UUID 값>";
        string rankUUID = "1be265c0-fb0e-11ee-a57f-7956f288c7a5";
        var bro = Backend.URank.User.GetRankList(rankUUID);

        if(bro.IsSuccess() == false)
        {
            Debug.LogError("랭킹 조회중 오류가 발생했습니다. : " + bro);
            return;
        }
        Debug.Log("랭킹 조회에 성공했습니다. : " + bro);

        Debug.Log("총 랭킹 등록 유저 수 : " + bro.GetFlattenJSON()["totalCount"].ToString());

        foreach(LitJson.JsonData jsonData in bro.FlattenRows())
        {
            StringBuilder info = new StringBuilder();

            info.AppendLine("순위 : " + jsonData["rank"].ToString());
            info.AppendLine("닉네임 : " + jsonData["nickname"].ToString());
            info.AppendLine("점수 : " + jsonData["score"].ToString());
            info.AppendLine("gamerInDate : " + jsonData["gamerInDate"].ToString());
            info.AppendLine("정렬번호 : " + jsonData["index"].ToString());
            info.AppendLine();
            Debug.Log(info);
        }
    }
}

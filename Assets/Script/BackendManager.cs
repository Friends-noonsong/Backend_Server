using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// [변경] async 기능을 이용하기 위해서는 해당 namespace가 필요합니다.
using System.Threading.Tasks;

// 뒤끝 SDK namespace 추가
using BackEnd;

// 게임 실행 시 자동으로 BackendManager 호출
public class BackendManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        // 뒤끝 초기화
        var bro = Backend.Initialize(true);

        // 뒤끝 초기화에 대한 응답값
        if (bro.IsSuccess())
        {
            // 성공일 경우 statusCode 204 Success
            Debug.Log("초기화 성공 : " + bro);
        }
        else
        {
            // 실패일 경우 statusCode 400대 에러 발생
            Debug.LogError("초기화 실패 : " + bro);
        }

        Test();
    }

    // [추가] 동기 함수를 비동기에서 호출하게 해주는 함수(유니티 UI 접근 불가)
    async void Test()
    {
        await Task.Run(() =>
        {
            // 1. 추후 테스트 케이스 추가
            // [추가] 뒤끝 회원가입 함수 호출
            // 뒤끝 초기화 후 해당 함수 호출
            //BackendLogin.Instance.CustomSignUp("user1", "1234");
            // [추가] 뒤끝 로그인 함수 호출
            //BackendLogin.Instance.CustomLogin("user1", "1234");
            // [추가] 닉네임 변경
            //BackendLogin.Instance.UpdateNickname("원하는 이름");



            // 뒤끝 로그인 함수
            //BackendLogin.Instance.CustomLogin("user1", "1234");

            // 2. 게임 정보 기능 구현 로직 추가
            // [추가] 데이터 삽입 함수
            //BackendGameData.Instance.GameDataInsert();
            // [추가] 데이터 불러오기 함수
            // 데이터 삽입 함수
            //BackendGameData.Instance.GameDataGet();
            // [추가] 서버에 불러온 데이터가 존재하지 않을 경우, 데이터를 새로 생성하여 삽입
            /*
            if(BackendGameData.userData == null)
            {
                BackendGameData.Instance.GameDataInsert();
            }
            */

            // [추가] 로컬에 저장된 데이터를 변경
            //BackendGameData.Instance.LevelUp();
            // [추가] 서버에 저장된 데이터를 덮어쓰기(변경된 부분만)
            //BackendGameData.Instance.GameDataUpdate();



            // 3. 랭킹 기능 구현 로직 추가
            // [추가] 랭킹 등록하기 함수
            //BackendRank.Instance.RankInsert(100);
            // [추가] 랭킹 불러오기 함수
            //BackendRank.Instance.RankGet();



            // 4. 차트 정보 불러오기 로직 추가
            // [추가] chartId의 차트 정보 불러오기
            // [변경 필요] <파일 ID>을 뒤끝 콘솔 > 차트 관리 > 아이템차트에서 등록한 차트의 파일 ID값으로 변경해주세요.
            //BackendChart.Instance.ChartGet("<파일 ID>");
            //BackendChart.Instance.ChartGet("117210");



            // 5. 우편 로직 추가
            // 게임데이터를 불러와 로컬에 저장합니다. (캐싱)
            //BackendGameData.Instance.GameDataGet();

            // [추가] 우편 리스트 불러오기
            // 우편 리스트를 불러와 우편의 정보와 inDate값들을 로컬에 저장합니다.
            //BackendPost.Instance.PostListGet(PostType.Admin);

            // 저장된 우편의 위치를 읽어 우편을 수령합니다.
            // 여기서 index는 우편의 순서.
            // 0이면 제일 윗 우편, 1이면 그 다음 우편
            //BackendPost.Instance.PostReceive(PostType.Admin, 0);

            // 조회된 모든 우편을 수령합니다.
            //BackendPost.Instance.PostReceiveAll(PostType.Admin);



            // 6. 쿠폰 기능 구현하기

            // 쿠폰 사용 로직 추가
            // [변경 필요] 쿠폰 코드를 뒤끝 콘솔 > 쿠폰 관리 > 테스트 쿠폰에서 생성된 쿠폰코드 값으로 변경해주세요.
            //BackendCoupon.Instance.CouponUse("쿠폰 코드");
            //BackendCoupon.Instance.CouponUse("8cd54dcce1e3072024");



            // 7. 게임 로그 저장 내용 추가
            // [추가] 게임로그 저장 기능
            //BackendGameLog.Instance.GameLogInsert();



            // 8. 친구 기능 로직 추가
            /*
            // user1에게 보낼 것이므로 user2로 회원가입
            string user2Id = "user2";

            // user2Id로 회원가입(409 에러 발생 시, 이미 user2Id로 아이디를 생성했으므로 CustomSignUp을 CustomLogin으로 변경_
            BackendLogin.Instance.CustomSignUp(user2Id, "1234");
            // 아이디와 동일하게 닉네임 변경
            BackendLogin.Instance.UpdateNickname(user2Id);

            // 유저1의 닉네임(유저에 따라 다를 수 있습니다.)
            string user1Nickname = "원하는 이름";
            // 친구 요청 보내기 함수
            BackendFriend.Instance.SendFriendRequest(user1Nickname);
            */

            /*
            // Step2에서 user2가 친구요청을 보낸 유저로 로그인해야합니다.
            BackendLogin.Instance.CustomLogin("user1", "1234");

            // 친구 요청 리스트 불러오기
            BackendFriend.Instance.GetReceivedRequestFriend();
            // 친구 요청 리스트 중 최신 요청 수락하기
            BackendFriend.Instance.ApplyFriend(0);
            */

            // Step 2에서 회원가입 한 유저(친구 요청 발송인) 혹은
            // Step 3에서 로그인한 유저(친구 요청 수신인) 중 하나로 로그인해야합니다.
            BackendLogin.Instance.CustomLogin("user1", "1234");

            // 친구 리스트 불러오기
            BackendFriend.Instance.GetFriendList();



            Debug.Log("테스트를 종료합니다.");
        });
    }



}

using System.Collections;
using TMPro;
using UnityEngine;

namespace BBX.Dialogue.GUI
{
    public class TypingTextAnimator : MonoBehaviour
    {
        public TMP_Text TMProText;       // TextMeshPro 텍스트 컴포넌트
        public float TypingSpeed = 0.05f; // 글자 등장 간격 (초 단위, 낮을수록 빠름)

        private void Start()
        {
            TMProText.ForceMeshUpdate(); // 텍스트 메쉬 강제 업데이트
            StartCoroutine(TypingEffect()); // 타이핑 효과 시작
        }

        /// <summary>
        /// 한 글자씩 순차적으로 나타나게 하는 코루틴
        /// </summary>
        private IEnumerator TypingEffect()
        {
            TMProText.maxVisibleCharacters = 0; // 초기 상태: 모든 글자 숨김
            int totalCharacters = TMProText.textInfo.characterCount; // 전체 글자 수

            for (int i = 0; i <= totalCharacters; i++)
            {
                TMProText.maxVisibleCharacters = i; // 현재 글자 수까지 가시화
                yield return new WaitForSeconds(TypingSpeed); // 설정된 속도로 대기
            }
        }
    }
}

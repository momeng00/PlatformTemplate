using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BBX.Dialogue.GUI
{
    public class WaveTextAnimator : MonoBehaviour
    {
        public TMP_Text TMProText; // 텍스트 컴포넌트

        private int _frameCount; // 프레임 카운터
        public float WaveAmount = 0.1f;      // wave 효과의 진폭
        public float WaveSpeed = 0.1f;       // wave 효과의 속도
        public float WaveSeparation = 20f;   // wave 간격

        private void Start()
        {
            // 텍스트가 변경될 수 있으므로 강제로 메쉬 업데이트
            TMProText.ForceMeshUpdate();
        }

        private void Update()
        {
            var textInfo = TMProText.textInfo;
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                // 글자가 표시되지 않으면 애니메이션을 적용하지 않음
                if (!textInfo.characterInfo[i].isVisible) continue;

                // 각 글자의 재질과 버텍스 인덱스를 가져옴
                var materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
                var vertexIndex = textInfo.characterInfo[i].vertexIndex;
                var destinationVertices = textInfo.meshInfo[materialIndex].vertices;

                // wave 애니메이션을 적용
                ApplyWave(destinationVertices, vertexIndex);

                // 업데이트된 버텍스 데이터 적용
                textInfo.meshInfo[materialIndex].mesh.vertices = textInfo.meshInfo[materialIndex].vertices;
                TMProText.UpdateGeometry(textInfo.meshInfo[materialIndex].mesh, materialIndex);
            }

            _frameCount++; // 프레임 카운터 증가
        }

        /// <summary>
        /// wave 효과를 적용하여 Y축 오프셋을 생성
        /// </summary>
        private void ApplyWave(IList<Vector3> destinationVertices, int vertexIndex)
        {
            for (byte corner = 0; corner < 4; corner++)
            {
                // wave 애니메이션의 Y축 오프셋 계산 후 각 버텍스에 적용
                var offset = new Vector3(0, Mathf.Sin(_frameCount * WaveSpeed + destinationVertices[vertexIndex + corner].x / WaveSeparation) * WaveAmount);
                destinationVertices[vertexIndex + corner] += offset;
            }
        }
    }
}

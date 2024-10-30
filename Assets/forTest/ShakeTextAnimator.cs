using TMPro;
using UnityEngine;
using System.Collections.Generic;

namespace BBX.Dialogue.GUI
{
    public class ShakeTextAnimator : MonoBehaviour
    {
        public TMP_Text TMProText; // 텍스트 컴포넌트

        public float ShakeAmount = 5f; // shake 효과의 진폭
        private readonly Dictionary<int, Vector3[]> _originalVertices = new Dictionary<int, Vector3[]>(); // 초기 위치 저장용

        private void Start()
        {
            // 텍스트 메쉬 업데이트
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

                // 초기 위치 저장
                if (!_originalVertices.ContainsKey(vertexIndex))
                {
                    _originalVertices[vertexIndex] = new Vector3[4];
                    for (int j = 0; j < 4; j++)
                    {
                        _originalVertices[vertexIndex][j] = destinationVertices[vertexIndex + j];
                    }
                }

                // shake 애니메이션을 적용
                ApplyShake(destinationVertices, vertexIndex);

                // 업데이트된 버텍스 데이터 적용
                textInfo.meshInfo[materialIndex].mesh.vertices = textInfo.meshInfo[materialIndex].vertices;
                TMProText.UpdateGeometry(textInfo.meshInfo[materialIndex].mesh, materialIndex);
            }
        }

        /// <summary>
        /// shake 효과를 적용하여 무작위 흔들림을 생성
        /// </summary>
        private void ApplyShake(IList<Vector3> destinationVertices, int vertexIndex)
        {
            for (byte corner = 0; corner < 4; corner++)
            {
                // 초기 위치에 무작위 오프셋을 추가하여 흔들림 효과 적용
                var randomOffset = new Vector3(
                    Random.Range(-ShakeAmount, ShakeAmount),
                    Random.Range(-ShakeAmount, ShakeAmount),
                    0
                );

                destinationVertices[vertexIndex + corner] = _originalVertices[vertexIndex][corner] + randomOffset;
            }
        }
    }
}

using TMPro;
using UnityEngine;
using System.Collections.Generic;

namespace BBX.Dialogue.GUI
{
    public class ShakeTextAnimator : MonoBehaviour
    {
        public TMP_Text TMProText; // �ؽ�Ʈ ������Ʈ

        public float ShakeAmount = 5f; // shake ȿ���� ����
        private readonly Dictionary<int, Vector3[]> _originalVertices = new Dictionary<int, Vector3[]>(); // �ʱ� ��ġ �����

        private void Start()
        {
            // �ؽ�Ʈ �޽� ������Ʈ
            TMProText.ForceMeshUpdate();
        }

        private void Update()
        {
            var textInfo = TMProText.textInfo;
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                // ���ڰ� ǥ�õ��� ������ �ִϸ��̼��� �������� ����
                if (!textInfo.characterInfo[i].isVisible) continue;

                // �� ������ ������ ���ؽ� �ε����� ������
                var materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
                var vertexIndex = textInfo.characterInfo[i].vertexIndex;
                var destinationVertices = textInfo.meshInfo[materialIndex].vertices;

                // �ʱ� ��ġ ����
                if (!_originalVertices.ContainsKey(vertexIndex))
                {
                    _originalVertices[vertexIndex] = new Vector3[4];
                    for (int j = 0; j < 4; j++)
                    {
                        _originalVertices[vertexIndex][j] = destinationVertices[vertexIndex + j];
                    }
                }

                // shake �ִϸ��̼��� ����
                ApplyShake(destinationVertices, vertexIndex);

                // ������Ʈ�� ���ؽ� ������ ����
                textInfo.meshInfo[materialIndex].mesh.vertices = textInfo.meshInfo[materialIndex].vertices;
                TMProText.UpdateGeometry(textInfo.meshInfo[materialIndex].mesh, materialIndex);
            }
        }

        /// <summary>
        /// shake ȿ���� �����Ͽ� ������ ��鸲�� ����
        /// </summary>
        private void ApplyShake(IList<Vector3> destinationVertices, int vertexIndex)
        {
            for (byte corner = 0; corner < 4; corner++)
            {
                // �ʱ� ��ġ�� ������ �������� �߰��Ͽ� ��鸲 ȿ�� ����
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

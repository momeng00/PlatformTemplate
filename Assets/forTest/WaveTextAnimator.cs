using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BBX.Dialogue.GUI
{
    public class WaveTextAnimator : MonoBehaviour
    {
        public TMP_Text TMProText; // �ؽ�Ʈ ������Ʈ

        private int _frameCount; // ������ ī����
        public float WaveAmount = 0.1f;      // wave ȿ���� ����
        public float WaveSpeed = 0.1f;       // wave ȿ���� �ӵ�
        public float WaveSeparation = 20f;   // wave ����

        private void Start()
        {
            // �ؽ�Ʈ�� ����� �� �����Ƿ� ������ �޽� ������Ʈ
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

                // wave �ִϸ��̼��� ����
                ApplyWave(destinationVertices, vertexIndex);

                // ������Ʈ�� ���ؽ� ������ ����
                textInfo.meshInfo[materialIndex].mesh.vertices = textInfo.meshInfo[materialIndex].vertices;
                TMProText.UpdateGeometry(textInfo.meshInfo[materialIndex].mesh, materialIndex);
            }

            _frameCount++; // ������ ī���� ����
        }

        /// <summary>
        /// wave ȿ���� �����Ͽ� Y�� �������� ����
        /// </summary>
        private void ApplyWave(IList<Vector3> destinationVertices, int vertexIndex)
        {
            for (byte corner = 0; corner < 4; corner++)
            {
                // wave �ִϸ��̼��� Y�� ������ ��� �� �� ���ؽ��� ����
                var offset = new Vector3(0, Mathf.Sin(_frameCount * WaveSpeed + destinationVertices[vertexIndex + corner].x / WaveSeparation) * WaveAmount);
                destinationVertices[vertexIndex + corner] += offset;
            }
        }
    }
}

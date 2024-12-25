using UnityEngine;

public class StaticEffect : MonoBehaviour
{
    public Material staticMaterial; // Associe o StaticMaterial aqui
    public float speed = 1.0f;

    void Update()
    {
        if (staticMaterial != null)
        {
            // Atualiza o offset da textura para criar o movimento da estática
            float offset = Time.time * speed;
            staticMaterial.mainTextureOffset = new Vector2(offset, offset);
        }
    }
}

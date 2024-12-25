using UnityEngine;
using UnityEngine.UI;

public class CanvasImageAnimator : MonoBehaviour
{
    public Image canvasImage; 
    public Sprite[] animationSprites; 
    public float frameRate = 0.1f; 

    private int currentFrame = 0; 
    private float timer = 0f; 

    void Update()
    {
        if (animationSprites.Length == 0 || canvasImage == null) return;

       
        timer += Time.deltaTime;

       
        if (timer >= frameRate)
        {
           
            timer -= frameRate;

           
            currentFrame = (currentFrame + 1) % animationSprites.Length;

          
            canvasImage.sprite = animationSprites[currentFrame];
        }
    }
}

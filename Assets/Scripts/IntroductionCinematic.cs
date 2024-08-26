using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroductionCinematic : MonoBehaviour
{
    public Image cinematicImage;
    public string sceneToLoad = "Level01";
    public float imageDisplayTime = 5f;
    public Sprite[] introductionImages;

    private int currentImageIndex = 0;

    void Start()
    {
        StartCoroutine(DisplayIntroduction());
    }

    IEnumerator DisplayIntroduction()
    {
        while (currentImageIndex < introductionImages.Length)
        {
            cinematicImage.sprite = introductionImages[currentImageIndex];
            yield return new WaitForSeconds(imageDisplayTime);
            currentImageIndex++;
        }

        // Todas las imágenes han sido mostradas, carga la escena del juego
        SceneManager.LoadScene(sceneToLoad);
    }
}

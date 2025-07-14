 using TMPro;
using UnityEngine;

public class UIStars : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI stars;

    void Update()
    {
        if(GameInstance.GameState != null)
            stars.text = "Stars " + GameInstance.GameState.stars;
    }
}

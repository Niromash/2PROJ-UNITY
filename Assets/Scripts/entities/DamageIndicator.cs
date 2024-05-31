using System.Collections;
using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public Transform canvasTransform; // Référence au transform du Canvas

    public void ShowDamage(float damage, Vector3 position)
    {
        // Créer une nouvelle instance de l'indicateur de dégâts
        GameObject damageText = Instantiate(damageTextPrefab, canvasTransform);

        // Ajouter un décalage à la position de l'indicateur de dégâts
        Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
        position += offset;

        // Configurer la position du texte de dégâts
        damageText.transform.position = Camera.main.WorldToScreenPoint(position);

        // Configurer le texte de l'indicateur pour afficher le montant des dégâts
        TextMeshProUGUI text = damageText.GetComponent<TextMeshProUGUI>();
        text.text = damage.ToString();
        // Vertical Gradient from CF4144FF to E15030FF
        text.colorGradient = new VertexGradient(new Color(0.811f, 0.255f, 0.267f, 1f),
            new Color(0.882f, 0.314f, 0.188f, 1f), new Color(0.882f, 0.314f, 0.188f, 1f),
            new Color(0.811f, 0.255f, 0.267f, 1f));
        text.fontSize = 16; // Ajustez cette valeur selon vos besoins

        // Démarrer l'animation de montée du texte
        StartCoroutine(AnimateDamageText(damageText));

        // Détruire l'indicateur après un certain temps
        Destroy(damageText, 1f);
    }

    private IEnumerator AnimateDamageText(GameObject damageText)
    {
        // Obtenir la position initiale du texte
        RectTransform rectTransform = damageText.GetComponent<RectTransform>();
        Vector3 initialPosition = rectTransform.position;
        Quaternion initialRotation = rectTransform.rotation;
        float initialFontSize = rectTransform.GetComponent<TextMeshProUGUI>().fontSize;

        // Définir la durée de l'animation
        float duration = 1f;
        float elapsed = 0f;

        // Faire monter le texte vers le haut pendant la durée de l'animation
        while (elapsed < duration)
        {
            if (rectTransform == null) yield break;
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            // Monter le texte
            rectTransform.position =
                initialPosition + new Vector3(0, t * 500, 0); // Augmenter la valeur pour plus d'intensité

            // Ajouter une légère rotation
            rectTransform.rotation =
                initialRotation *
                Quaternion.Euler(0, 0, Mathf.Sin(t * Mathf.PI * 2) * 2); // Ajuster l'angle pour plus d'intensité

            // Modifier la taille du texte
            rectTransform.GetComponent<TextMeshProUGUI>().fontSize =
                initialFontSize * (1 + t * 0.5f); // Augmenter la taille pour plus d'intensité

            // Modifier la couleur du texte
            Color startColor = new Color(0.811f, 0.255f, 0.267f, 1f); // #CF4144FF
            Color endColor = new Color(0.882f, 0.314f, 0.188f, 1f); // #E15030FF
            rectTransform.GetComponent<TextMeshProUGUI>().color = Color.Lerp(startColor, endColor, t);

            // rectTransform.GetComponent<TextMeshProUGUI>().color = Color.Lerp(Color.red, new Color(0.5f, 0, 0), t);

            yield return null;
        }

        if (rectTransform == null) yield break;
        // Assurez-vous que le texte est entièrement déplacé
        rectTransform.position = initialPosition + new Vector3(0, 100, 0); // Ajustez la valeur 100 selon vos besoins
        rectTransform.rotation = initialRotation;
        rectTransform.GetComponent<TextMeshProUGUI>().fontSize = initialFontSize;
    }
}
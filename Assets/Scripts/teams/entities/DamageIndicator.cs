﻿using System.Collections;
using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public Transform canvasTransform; // Référence au transform du Canvas
    public float efficiency = 1f; // Efficacité des dégâts

    public void ShowDamage(float damage, Vector3 position)
    {
        // Créer une nouvelle instance de l'indicateur de dégâts
        GameObject damageText = Instantiate(damageTextPrefab, canvasTransform);

        Vector3 offset = new Vector3(Random.Range(1.2f, 1.7f), Random.Range(-0.5f, 0.5f), 0);
        position += offset;

        // Configurer la position du texte de dégâts
        damageText.transform.position = Camera.main.WorldToScreenPoint(position);

        // Configurer le texte de l'indicateur pour afficher le montant des dégâts
        TextMeshProUGUI text = damageText.GetComponent<TextMeshProUGUI>();
        text.text = damage.ToString();

        // Définir la couleur du texte en fonction de l'efficacité
        if (efficiency >= 1.5f) // Si c'est super efficace
        {
            text.color = Color.green; // Changer la couleur en vert
        }
        else if (efficiency <= 0.5f) // Si ce n'est pas du tout efficace
        {
            text.color = Color.red; // Changer la couleur en rouge
        }
        else // Si c'est moyennement efficace
        {
            text.color = Color.yellow; // Changer la couleur en jaune
        }

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
            Color startColor;
            Color endColor;
            if (efficiency >= 1.5f) // Si c'est super efficace
            {
                startColor = Color.green; // Changer la couleur en vert
                endColor = Color.green; // Changer la couleur en vert
            }
            else if (efficiency <= 0.5f) // Si ce n'est pas du tout efficace
            {
                startColor = Color.red; // Changer la couleur en rouge
                endColor = Color.red; // Changer la couleur en rouge
            }
            else // Si c'est moyennement efficace
            {
                startColor = Color.yellow; // Changer la couleur en jaune
                endColor = Color.yellow; // Changer la couleur en jaune
            }

            rectTransform.GetComponent<TextMeshProUGUI>().color = Color.Lerp(startColor, endColor, t);

            yield return null;
        }

        if (rectTransform == null) yield break;
        // Assurez-vous que le texte est entièrement déplacé
        rectTransform.position = initialPosition + new Vector3(0, 100, 0); // Ajustez la valeur 100 selon vos besoins
        rectTransform.rotation = initialRotation;
        rectTransform.GetComponent<TextMeshProUGUI>().fontSize = initialFontSize;
    }
}
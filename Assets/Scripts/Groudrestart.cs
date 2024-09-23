using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GroundRestart : MonoBehaviour
{
    // Variável static para persistir entre as cenas
    private static int count = 0;

    // Função para detectar a colisão com o chão
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica se foi o jogador que tocou
        {
            // Se o jogador cair mais de 3 vezes, muda para a cena 2
            if (count >= 3)
            {
                SceneManager.LoadScene(2); // Carrega a cena de "game over" ou outra cena
                count = 0;
            }
            else
            {
                // Conta o número de quedas e reseta para a cena de início
                SceneManager.LoadScene(1); // Cena inicial ou de reinício
                count += 1; // Incrementa o contador de quedas
            }
        }
    }
}

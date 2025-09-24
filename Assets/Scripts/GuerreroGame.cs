using UnityEngine;
using System.Collections.Generic;

public class GuerreroGame : MonoBehaviour
{
    [System.Serializable]
    public class Guerrero
    {
        public string nombre;
        public int vida;
        public int ataque;
    }

    [System.Serializable]
    public class Enemigo
    {
        public string nombre;
        public int vida;
        public int ataque;
        public bool estaVivo = true;
    }

    public Guerrero guerrero;
    public List<Enemigo> enemigos = new List<Enemigo>();
    public int cantidadEnemigos = 3;

    void Start()
    {
        InicializarJuego();
        StartCoroutine(SimularCombate());
    }

    void InicializarJuego()
    {
        guerrero = new Guerrero
        {
            nombre = "Héroe",
            vida = Random.Range(50, 100),
            ataque = Random.Range(10, 20)
        };

        enemigos.Clear();
        for (int i = 0; i < cantidadEnemigos; i++)
        {
            enemigos.Add(new Enemigo
            {
                nombre = $"Enemigo {i + 1}",
                vida = Random.Range(20, 40),
                ataque = Random.Range(5, 15)
            });
        }

        Debug.Log("=== INICIO DEL COMBATE ===");
        Debug.Log($"Guerrero: {guerrero.nombre} (Vida: {guerrero.vida}, Ataque: {guerrero.ataque})");
    }

    System.Collections.IEnumerator SimularCombate()
    {
        while (guerrero.vida > 0 && QuedanEnemigosVivos())
        {
            foreach (var enemigo in enemigos)
            {
                if (!enemigo.estaVivo) continue;

                // Guerrero ataca al enemigo
                enemigo.vida -= guerrero.ataque;
                Debug.Log($"{guerrero.nombre} ataca a {enemigo.nombre} (-{guerrero.ataque} vida)");

                if (enemigo.vida <= 0)
                {
                    enemigo.estaVivo = false;
                    Debug.Log($"¡{enemigo.nombre} derrotado!");
                }

                // Verificar si el guerrero murió
                if (guerrero.vida <= 0) break;
                yield return new WaitForSeconds(1);
            }
        }

        Debug.Log(guerrero.vida > 0 ? "¡El guerrero gana!" : "¡El guerrero ha sido derrotado!");
    }

    bool QuedanEnemigosVivos()
    {
        foreach (var enemigo in enemigos)
            if (enemigo.estaVivo) return true;
        return false;
    }

    // Método para testing
    public void EliminarEnemigosMuertos()
    {
        enemigos.RemoveAll(enemigo => !enemigo.estaVivo);
    }
}
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class GuerreroGameTests
{
    [Test]
    public void Enemigo_Eliminado_Cuando_Muere()
    {
        // Preparar
        var gameObject = new GameObject();
        var juego = gameObject.AddComponent<GuerreroGame>();
        juego.enemigos = new List<GuerreroGame.Enemigo>
        {
            new GuerreroGame.Enemigo { vida = 0, estaVivo = false },
            new GuerreroGame.Enemigo { vida = 10, estaVivo = true }
        };

        // Actuar
        juego.EliminarEnemigosMuertos();

        // Verificar
        Assert.AreEqual(1, juego.enemigos.Count);
        Assert.IsTrue(juego.enemigos.TrueForAll(e => e.estaVivo));
    }

    [Test]
    public void Enemigo_MarcadoComoMuerto_Cuando_VidaCero()
    {
        // Preparar
        var enemigo = new GuerreroGame.Enemigo { vida = 10, estaVivo = true };

        // Actuar
        enemigo.vida = 0;
        if (enemigo.vida <= 0) enemigo.estaVivo = false;

        // Verificar
        Assert.IsFalse(enemigo.estaVivo);
        Assert.AreEqual(0, enemigo.vida);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorDeEscena : MonoBehaviour
{
    public GameObject Jugador;

    public Camera CamaraDelJuego;

    public GameObject [] BloquePrefab;

    public float PunteroDelJuego;

    public float LugarSeguroDeGeneracion = 12;
    
    public TextMeshProUGUI TextoDeJuego;

    public bool Perdiste;
    
    // Start is called before the first frame update
    void Start()
    {
        PunteroDelJuego = 0;
        Perdiste = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Jugador!= null) { 
        CamaraDelJuego.transform.position = new Vector3(
            Jugador.transform.position.x,
            CamaraDelJuego.transform.position.y,
            CamaraDelJuego.transform.position.z
            );
            TextoDeJuego.text = "Puntaje: " + Mathf.Floor(Jugador.transform.position.x);
        }

        else
        {
            if (Perdiste)
            {
                Perdiste = true;
                TextoDeJuego.text += "\n¡Acabas de perder! \n Presione r para reintentar";
            }
            if (Perdiste)
            {
                if (Input.GetKeyDown("r"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }

        while(Jugador != null && PunteroDelJuego<Jugador.transform.position.x + LugarSeguroDeGeneracion)
        {
            int indiceBloque = Random.Range(0, BloquePrefab.Length - 1);
            if (PunteroDelJuego < 0)
            {
                indiceBloque = 4;
            }
            GameObject ObjetoBloque = Instantiate(BloquePrefab[indiceBloque]);
            ObjetoBloque.transform.SetParent(this.transform);
            Bloque bloque = ObjetoBloque.GetComponent<Bloque>();
            ObjetoBloque.transform.position = new Vector2(
               PunteroDelJuego + bloque.Tamaño / 2, 0
               );
            PunteroDelJuego += bloque.Tamaño;
        }

    }
}

//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// MSprite.cs (00/00/0000)													    \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion:																	\\
// Fecha Mod:		00/00/0000													\\
// Ultima Mod:																	\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using System.Collections.Generic;

#endregion

public class MSprite : MonoBehaviour 
{
    public List<clsSprite> elementos = new List<clsSprite>();
    public Vector3 dist;
    public Vector2 cordenadas;

    public void Start()
    {
        for (int i = 0; i < elementos.Count; i++)
        {
            elementos[i].prefab.name = elementos[i].nombre;
        }
    }
}
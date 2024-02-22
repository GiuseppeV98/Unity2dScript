using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;
public class mapprov : MonoBehaviour
{  public Tilemap myTilemap; // Assicurati di assegnare la Tilemap nell'Inspector
    public TileBase tileToPlace; // Tile da posizionare (assicurati di avere un TileBase da assegnare)

   /* void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Ottieni la posizione del mouse nel mondo di gioco
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Converti la posizione del mouse in coordinate della Tilemap
            Vector3Int cellPosition = myTilemap.WorldToCell(mousePosition);
            Debug.Log("hai cliccato la casella"+ cellPosition.x+" "+cellPosition.y);
            HighlightAdjacentCells(cellPosition);
                }
            
    }*/
 // Ottieni la tile alla posizione specificata
           /* TileBase tile = myTilemap.GetTile(cellPosition);

            if (tile != null)
            {
                // Modifica la tile trovata cambiandone il colore (o altre proprietà)
                myTilemap.SetTileFlags(cellPosition, TileFlags.None);
                myTilemap.SetColor(cellPosition, Color.red); // Cambia il colore della tile
            }
            else
            {
                // Se non c'è alcuna tile a quella posizione, posiziona una nuova tile
                myTilemap.SetTile(cellPosition, tileToPlace);
            }
        }
    }*/
void HighlightAdjacentCells(Vector3Int centerPosition)
    {
        // Ciclo per le caselle adiacenti (8 direzioni)
        for (int xOffset = -1; xOffset <= 1; xOffset++)
        {
            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                Vector3Int offset = new Vector3Int(xOffset, yOffset, 0);
                Vector3Int adjacentCell = centerPosition + offset;

                // Controlla se la casella è diversa dalla casella centrale
                if (adjacentCell != centerPosition)
                {
                    // Controlla se la casella esiste nella Tilemap
                    if (myTilemap.HasTile(adjacentCell))
                    {
                        // Ottieni la posizione del centro della tile nella Tilemap
                        Vector3 tileCenter = myTilemap.GetCellCenterWorld(adjacentCell);

                        // Cambia il colore delle caselle adiacenti in giallo
                        myTilemap.SetTileFlags(adjacentCell, TileFlags.None);
                        myTilemap.SetColor(adjacentCell, Color.yellow);
                    }
                }
            }
        }
    }
   /* void RestoreOriginalColors()
    {
        for (int xOffset = -1; xOffset <= 1; xOffset++)
        {
            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                Vector3Int offset = new Vector3Int(xOffset, yOffset, 0);
                Vector3Int adjacentCell = centerPosition + offset;

                if (adjacentCell != centerPosition && myTilemap.HasTile(adjacentCell))
                {
                    myTilemap.SetColor(adjacentCell, Color.white); // Ripristina il colore predefinito
                }
            }
        }
    }*/
}



    

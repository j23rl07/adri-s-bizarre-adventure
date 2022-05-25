using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookScript : MonoBehaviour
{
    public Maze maze;
    
    void onEnable()
    {
        maze.bookNum = 0;
        maze.bookText.text = "" + maze.bookNum + "/4";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            addBook();
            if(gameObject.name == "book1")
            {
                maze.gotBook1 = true; 
            }
            else if (gameObject.name == "book2")
            {
                maze.gotBook2 = true;
            }
            else if (gameObject.name == "book3")
            {
                maze.gotBook3 = true;

            }
            else if (gameObject.name == "book4")
            {
                maze.gotBook4 = true;
            }
            gameObject.active = false;
        }
    }

    public void addBook()
    {
        maze.bookNum += 1;
        maze.bookText.text = "" + maze.bookNum + "/4";
    }
}

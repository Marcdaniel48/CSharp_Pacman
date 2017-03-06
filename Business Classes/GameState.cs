using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Classes
{
    public class GameState
    {
        Pacman pman;
        GhostPack packOfGhosts;
        Maze maze;
        Pen pen;
        ScoreAndLives scoreNLives;

        public GameState()
        {
            pen = new Pen();
            maze = new Maze();
            packOfGhosts = new GhostPack();
            scoreNLives = new ScoreAndLives(this);
            pman = new Pacman(this);
        }

        public static GameState Parse(string filecontent)
        {
            string[] lines = File.ReadAllLines(filecontent);
            Tile[,] tilesArray = new Tile[lines.Length, lines[0].ToCharArray().Length];

            GameState state = new GameState();

            for(int i = 0; i < tilesArray.GetLength(0); i++)
            {
                for(int j = 0; j < tilesArray.GetLength(1); j++)
                {
                    switch(lines[i].ToCharArray()[j])
                    {
                        case 'w':
                            tilesArray[i, j] = new Wall(i, j);
                            break;
                        case 'p':
                            Pellet pel = new Pellet();
                            //pel.Collision += ScoreAndLives.incrementScore; // incrementScore is private FAQ
                            tilesArray[i, j] = new Path(i,j,pel);
                            break;
                        case 'e':
                            // Energizer energ = new Energizer(); // Can't give it packOfGhosts
                            break;
                        case 'm':
                            tilesArray[i, j] = new Path(i, j, null);
                            break;
                        /*case (char)1:
                            Ghost blinky = new Ghost(state, i, j, null, GhostState.Chase, "Red"); //vector2 in constructor is null
                            break; 
                        case (char)2:
                            break;
                        case (char)3:
                            break;
                        case (char)4:
                            break;
                        case 'P':
                            break;*/
                    }
                }
            }

        }

        public Pacman Pacman
        {
            get { return pman; }
            private set { pman = value; }
        }

        public GhostPack GhostPack
        {
            get { return packOfGhosts; }
            private set { packOfGhosts = value; }
        }

        public Maze Maze
        {
            get { return maze; }
            private set { maze = value; }
        }

        public Pen Pen
        {
            get { return pen; }
            private set { Pen = value; }
        }

        public ScoreAndLives Score
        {
            get { return scoreNLives; }
        }
    }
}

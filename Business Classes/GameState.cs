using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

/// <summary>
/// Authors of whole project:
/// @author: Marc-Daniel Dialogo
/// @author: Rhai Hinds
/// @version: 3/10/2017 at 6:08pm
/// </summary>
namespace Business_Classes
{
    /// <summary>
    /// Represents the state of the pacman game
    /// </summary>
    public class GameState
    {

        // initializes the members of the GameState object and GameState

        /// <summary>
        /// Reads a file representing the layout of the pacman game, and builds the maze accordingly, while creating and
        /// setting up pacman, the ghosts, and the pellets & energizers
        /// </summary>
        /// <param name="filecontent">The layout of the pacman maze</param>
        /// <returns>The gamestate</returns>

        public static GameState Parse(string filecontent)
        {
            string[] stringSeparators = new string[] { "\r\n" };
            string[] lines = filecontent.Trim().Split(stringSeparators, StringSplitOptions.None);
            Tile[,] tilesArray = new Tile[lines.Length, lines[0].Split(',').Length];

            GameState state = new GameState();
            state.GhostSquad = new GhostPack();
            state.Pen = new Pen();
            state.Maze = new Maze();
            state.Pacman = new Pacman(state);
            state.Score = new ScoreAndLives(state);
            Ghost.sTime = new Timer(5000);
            for (int i = 0; i < tilesArray.GetLength(0); i++)
            {
                for(int j = 0; j < tilesArray.GetLength(1); j++)
                {
                    switch (lines[i].Split(',')[j])
                    {
                        case "w":
                            tilesArray[i, j] = new Wall(j, i);
                            break;
                        case "p":
                            Pellet pel = new Pellet();
                            pel.Collision += state.Score.incrementScore; 
                            tilesArray[i, j] = new Path(j,i,pel);
                            break;
                        case "e":
                            Energizer energ = new Energizer(state.GhostSquad);
                            energ.Collision += state.Score.incrementScore;
                            tilesArray[i, j] = new Path(j, i, energ);
                            break;
                        case "m":
                            tilesArray[i, j] = new Path(j, i, null);
                            break;
                        case "1":
                            Ghost.releasedPosition = new Vector2(j, i);
                            Ghost blinky = new Ghost(state, j, i, Ghost.releasedPosition, GhostState.Chase, Color.Red);
                            blinky.Collision += state.Score.incrementScore;
                            blinky.PacmanDied += state.Score.deadPacman;
                            state.GhostSquad.Add(blinky);
                            tilesArray[i, j] = new Path(j, i, null);
                            state.Pen.AddTile(tilesArray[i, j]);
                            break; 
                        case "2":
                            Ghost Pinky = new Ghost(state, j, i, Ghost.releasedPosition, GhostState.Chase, Color.Pink);
                            Pinky.Collision += state.Score.incrementScore;
                            Pinky.PacmanDied += state.Score.deadPacman;
                            state.GhostSquad.Add(Pinky);
                            tilesArray[i, j] = new Path(j, i, null);
                            state.Pen.AddTile(tilesArray[i, j]);
                            state.Pen.AddToPen(Pinky);
                            break;
                        case "3":
                            Ghost Inky = new Ghost(state, j, i, Ghost.releasedPosition, GhostState.Chase, Color.Blue);
                            Inky.Collision += state.Score.incrementScore;
                            Inky.PacmanDied += state.Score.deadPacman;
                            state.GhostSquad.Add(Inky);
                            tilesArray[i, j] = new Path(j, i, null);
                            state.Pen.AddTile(tilesArray[i, j]);
                            state.Pen.AddToPen(Inky);
                            break;
                        case "4":
                            Ghost Clyde = new Ghost(state, j, i, Ghost.releasedPosition, GhostState.Chase, Color.Orange);
                            Clyde.Collision += state.Score.incrementScore;
                            Clyde.PacmanDied += state.Score.deadPacman;
                            state.GhostSquad.Add(Clyde);
                            tilesArray[i, j] = new Path(j, i, null);
                            state.Pen.AddTile(tilesArray[i, j]);
                            state.Pen.AddToPen(Clyde);
                            break;
                        case "P":
                            tilesArray[i, j] = new Path(j, i, null);
                            state.Pacman.Position = new Vector2(j,i);
                            state.Pacman.OGPosition = state.Pacman.Position;
                            break;
                    }
                }
            }

            state.Maze.SetTiles(tilesArray);
            return state;

        }


        public Pacman Pacman
        {
            get;
            private set;
        }

        public GhostPack GhostSquad
        {
            get;
            private set;
        }

        public Maze Maze
        {
            get;
            private set;
        }

        public Pen Pen
        {
            get;
            private set;
        }

        public ScoreAndLives Score
        {
            get;
            private set;
        }
    }
}

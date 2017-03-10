using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Pacman pman;
        GhostPack packOfGhosts;
        Maze maze;
        Pen pen;
        ScoreAndLives scoreNLives;

        // initializes the members of the GameState object and GameState

        /// <summary>
        /// Reads a file representing the layout of the pacman game, and builds the maze accordingly, while creating and
        /// setting up pacman, the ghosts, and the pellets & energizers
        /// </summary>
        /// <param name="filecontent">The layout of the pacman maze</param>
        /// <returns>The gamestate</returns>

        public static GameState Parse(string filecontent)
        {
            string[] lines = File.ReadAllLines(filecontent);
            Tile[,] tilesArray = new Tile[lines.Length, lines[0].Split(',').Length];

            GameState state = new GameState();
            state.GhostSquad = new GhostPack();
            state.Pen = new Pen();
            state.Maze = new Maze();
            state.Pacman = new Pacman(state);
            state.scoreNLives = new ScoreAndLives(state);

            for(int i = 0; i < tilesArray.GetLength(0); i++)
            {
                for(int j = 0; j < tilesArray.GetLength(1); j++)
                {
                    switch(lines[i].Split(',')[j])
                    {
                        case "w":
                            tilesArray[i, j] = new Wall(j, i);
                            break;
                        case "p":
                            Pellet pel = new Pellet();
                            pel.Collision += state.scoreNLives.incrementScore; 
                            tilesArray[i, j] = new Path(j,i,pel);
                            break;
                        case "e":
                            Energizer energ = new Energizer(state.packOfGhosts);
                            energ.Collision += state.scoreNLives.incrementScore;
                            tilesArray[i, j] = new Path(j, i, energ);
                            break;
                        case "m":
                            tilesArray[i, j] = new Path(j, i, null);
                            break;
                        case "1":
                            Ghost.releasedPosition = new Vector2(j, i);
                            Ghost blinky = new Ghost(state, j, i, new Vector2(1,1), GhostState.Chase, "Red");
                            blinky.Collision += state.scoreNLives.incrementScore;
                            blinky.PacmanDied += state.scoreNLives.deadPacman;
                            state.packOfGhosts.Add(blinky);
                            tilesArray[i, j] = new Path(j, i, null);
                            break; 
                        case "2":
                            Ghost Pinky = new Ghost(state, j, i, new Vector2(2, 2), GhostState.Chase, "Pink");
                            Pinky.Collision += state.scoreNLives.incrementScore;
                            Pinky.PacmanDied += state.scoreNLives.deadPacman;
                            state.packOfGhosts.Add(Pinky);
                            tilesArray[i, j] = new Path(j, i, null);
                            state.Pen.AddTile(tilesArray[i, j]);
                            state.Pen.AddToPen(Pinky);
                            break;
                        case "3":
                            Ghost Inky = new Ghost(state, j, i, new Vector2(3, 3), GhostState.Chase, "Blue");
                            Inky.Collision += state.scoreNLives.incrementScore;
                            Inky.PacmanDied += state.scoreNLives.deadPacman;
                            state.packOfGhosts.Add(Inky);
                            tilesArray[i, j] = new Path(j, i, null);
                            state.Pen.AddTile(tilesArray[i, j]);
                            state.Pen.AddToPen(Inky);
                            break;
                        case "4":
                            Ghost Clyde = new Ghost(state, j, i, new Vector2(4, 4), GhostState.Chase, "Clyde");
                            Clyde.Collision += state.scoreNLives.incrementScore;
                            Clyde.PacmanDied += state.scoreNLives.deadPacman;
                            state.packOfGhosts.Add(Clyde);
                            tilesArray[i, j] = new Path(j, i, null);
                            state.Pen.AddTile(tilesArray[i, j]);
                            state.Pen.AddToPen(Clyde);
                            break;
                        case "P":
                            tilesArray[i, j] = new Path(j, i, null);
                            state.pman.Position = new Vector2(j,i);
                            break;
                    }
                }
            }

            state.Maze.SetTiles(tilesArray);
            return state;

        }


        public Pacman Pacman
        {
            get { return pman; }
            private set { pman = value; }
        }

        public GhostPack GhostSquad
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
            private set { pen = value; }
        }

        public ScoreAndLives Score
        {
            get { return scoreNLives; }
        }
    }
}

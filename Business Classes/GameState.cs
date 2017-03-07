﻿using Microsoft.Xna.Framework;
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
            Tile[,] tilesArray = new Tile[lines.Length, lines[0].Split(',').Length];

            GameState state = new GameState();

            for(int i = 0; i < tilesArray.GetLength(0); i++)
            {
                for(int j = 0; j < tilesArray.GetLength(1); j++)
                {
                    switch(lines[i].Split(',')[j])
                    {
                        case "w":
                            tilesArray[i, j] = new Wall(i, j);
                            break;
                        case "p":
                            Pellet pel = new Pellet();
                            pel.Collision += state.scoreNLives.incrementScore; 
                            tilesArray[i, j] = new Path(i,j,pel);
                            break;
                        case "e":
                            Energizer energ = new Energizer(state.packOfGhosts);
                            energ.Collision += state.scoreNLives.incrementScore;
                            tilesArray[i, j] = new Path(i, j, energ);
                            break;
                        case "m":
                            tilesArray[i, j] = new Path(i, j, null);
                            break;
                        case "1":
                            Ghost blinky = new Ghost(state, i, j, new Microsoft.Xna.Framework.Vector2(1,1), GhostState.Chase, "Red"); // target vector is what??
                            blinky.Collision += state.scoreNLives.incrementScore;
                            blinky.pacmanDies += state.scoreNLives.deadPacman;
                            state.packOfGhosts.Add(blinky);
                            tilesArray[i, j] = new Path(i, j, null);
                            break; 
                        case "2":
                            Ghost Pinky = new Ghost(state, i, j, new Microsoft.Xna.Framework.Vector2(2, 2), GhostState.Chase, "Pink");
                            Pinky.Collision += state.scoreNLives.incrementScore;
                            Pinky.pacmanDies += state.scoreNLives.deadPacman;
                            state.packOfGhosts.Add(Pinky);
                            tilesArray[i, j] = new Path(i, j, null);
                            state.Pen.AddTile(tilesArray[i, j]);
                            state.Pen.AddToPen(Pinky);
                            break;
                        case "3":
                            Ghost Inky = new Ghost(state, i, j, new Microsoft.Xna.Framework.Vector2(3, 3), GhostState.Chase, "Blue");
                            Inky.Collision += state.scoreNLives.incrementScore;
                            Inky.pacmanDies += state.scoreNLives.deadPacman;
                            state.packOfGhosts.Add(Inky);
                            tilesArray[i, j] = new Path(i, j, null);
                            state.Pen.AddTile(tilesArray[i, j]);
                            state.Pen.AddToPen(Inky);
                            break;
                        case "4":
                            Ghost Clyde = new Ghost(state, i, j, new Microsoft.Xna.Framework.Vector2(4, 4), GhostState.Chase, "Clyde");
                            Clyde.Collision += state.scoreNLives.incrementScore;
                            Clyde.pacmanDies += state.scoreNLives.deadPacman;
                            state.packOfGhosts.Add(Clyde);
                            tilesArray[i, j] = new Path(i, j, null);
                            state.Pen.AddTile(tilesArray[i, j]);
                            state.Pen.AddToPen(Clyde);
                            break;
                        case "P":
                            tilesArray[i, j] = new Path(i, j, null);
                            state.pman.Position = new Vector2(i,j);
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

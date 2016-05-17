﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ITI.GameCore;

namespace ITI.TabledeTyr.Test
{
    [TestFixture]
    public class t03_Tests_Situation
    {
        //Board for a standard 11*11 game [Hardcoded for IT1]
        #region Setting the board
        /*
         x 00 01 02 03 04 05 06 07 08 09 10 x
        00 -- -- -- 01 01 01 01 01 -- -- --
        01 -- -- -- -- -- 01 -- -- -- -- --
        02 -- -- -- -- -- -- -- -- -- -- --
        03 01 -- -- -- -- 10 -- -- -- -- 01
        04 01 -- -- -- 10 10 10 -- -- -- 01
        05 01 01 -- 10 10 11 10 10 -- 01 01
        06 01 -- -- -- 10 10 10 -- -- -- 01
        07 01 -- -- -- -- 10 -- -- -- -- 01
        08 -- -- -- -- -- -- -- -- -- -- --
        09 -- -- -- -- -- 01 -- -- -- -- --
        10 -- -- -- 01 01 01 01 01 -- -- --
        y*/
        #endregion
        //test : take a pawn
        [TestCase(4, 3)]
        public void Situation_move_pawn(int x, int y)
        {
            //Arrange
            int i = 1;
            int pawnMovedX = 0;
            int pawnMovedY = 0;
            int pawnDestinationX = 0;
            int pawnDestinationY = 0;

            Game sut = new Game();
            var currentTafl = sut.Tafl;     
            bool atkPlaying;
            bool pawnMoved;
            //act
            do
            {
                if (i == 1) { pawnMovedX = 4; pawnMovedY = 0; pawnDestinationX = 4; pawnDestinationY = 3; }
               
                atkPlaying = sut.IsAtkPlaying;

                pawnMoved = sut.MovePawn(pawnMovedX, pawnMovedY, pawnDestinationX, pawnDestinationY);

                if (sut.UpdateTurn() == false)
                {
                    atkPlaying = sut.IsAtkPlaying;
                    break;
                }
                else
                {
                    currentTafl = sut.Tafl;
                }
                i++;
            } while (i <= 1);//MAIN LOOP
            //assert
            currentTafl = sut.Tafl;
            Assert.That(currentTafl[x, y], Is.EqualTo(Pawn.Attacker));
        }
        //test : take a pawn
        [TestCase(4,3)]
        public void Situation_capture_pawn(int x, int y)
        {
            //Arrange
            int i = 1;
            int pawnMovedX = 0;
            int pawnMovedY = 0;
            int pawnDestinationX = 0;
            int pawnDestinationY = 0;

            Game sut = new Game();
            var currentTafl = sut.Tafl;        
            bool atkPlaying;
            bool pawnMoved;
            //act
            do
            {
                if (i == 1) { pawnMovedX = 4; pawnMovedY = 0; pawnDestinationX = 4; pawnDestinationY = 3; }
                if (i == 2) { pawnMovedX = 3; pawnMovedY = 5; pawnDestinationX = 3; pawnDestinationY = 3; }

                atkPlaying = sut.IsAtkPlaying;

                pawnMoved = sut.MovePawn(pawnMovedX, pawnMovedY, pawnDestinationX, pawnDestinationY);

                if (sut.UpdateTurn() == false)
                {
                    atkPlaying = sut.IsAtkPlaying;
                    break;
                }
                else
                {
                    currentTafl = sut.Tafl;
                }
                i++;
            } while (i<=2);//MAIN LOOP
            //assert
            currentTafl = sut.Tafl;
            Assert.That(currentTafl[x, y], Is.EqualTo(Pawn.None));
        }
        //test : try moving pawn out of the tafl (4 cases : north, south, east, west)
        [TestCase(5, 4)]
        public void Situation_cannot_moving_pawn_out_of_the_tafl(int x, int y)
        {
            //arrange
            int pawnMovedX = 0;
            int pawnMovedY = 0;
            int pawnDestinationX = 0;
            int pawnDestinationY = 0;

            Game sut = new Game();
            var currentTafl = sut.Tafl;
            bool pawnMoved;
            //act    
            pawnMovedX = 2; pawnMovedY = 0; pawnDestinationX = 2; pawnDestinationY = -1;
            //assert
            Assert.Throws<ArgumentOutOfRangeException>(() => pawnMoved = sut.MovePawn(pawnMovedX, pawnMovedY, pawnDestinationX, pawnDestinationY));

        }
        //test : cannot moving while not his turn !
        [Test]
        public void Situation_cannot_use_opposite_pawn()
        {
            //arrange
            int i = 1;
            int pawnMovedX = 0;
            int pawnMovedY = 0;
            int pawnDestinationX = 0;
            int pawnDestinationY = 0;

            Game sut = new Game();
            var currentTafl = sut.Tafl;
            bool[,] movableTafl = new bool[11, 11];
            bool[,] pawnDestinations = new bool[11, 11];
            bool atkPlaying;
            bool pawnMoved;

            do
            {
                if (i == 1) { pawnMovedX = 3; pawnMovedY = 0; pawnDestinationX = 3; pawnDestinationY = 1; }
                if (i == 2) { pawnMovedX = 3; pawnMovedY = 1; pawnDestinationX = 3; pawnDestinationY = 0; }

                atkPlaying = sut.IsAtkPlaying;

                if (i == 2)
                {
                    Assert.Throws<ArgumentException>(() => pawnMoved = sut.MovePawn(pawnMovedX, pawnMovedY, pawnDestinationX, pawnDestinationY));
                    break;
                }
                else { 
                pawnMoved = sut.MovePawn(pawnMovedX, pawnMovedY, pawnDestinationX, pawnDestinationY);
                }

                if (sut.UpdateTurn() == false)
                {
                    atkPlaying = sut.IsAtkPlaying;
                    break;
                }
                else
                {
                    currentTafl = sut.Tafl;
                }
                i++;
            } while (i <= 2);//MAIN LOOP
            
        }
        //Game test if the king escapes
        [TestCase(10, 0)]
        public void Situation_escape_of_the_king(int x, int y)
        {
            int i = 1;
            int pawnMovedX = 0;
            int pawnMovedY = 0;
            int pawnDestinationX = 0;
            int pawnDestinationY = 0;

            Game sut = new Game();
            var currentTafl = sut.Tafl;
            bool atkPlaying;
            bool pawnMoved;         
            do
            {
                if (i == 1) { pawnMovedX = 3; pawnMovedY = 0; pawnDestinationX = 3; pawnDestinationY = 1; }//Atk
                if (i == 2) { pawnMovedX = 6; pawnMovedY = 4; pawnDestinationX = 6; pawnDestinationY = 1; }
                if (i == 3) { pawnMovedX = 3; pawnMovedY = 1; pawnDestinationX = 3; pawnDestinationY = 0; }//Atk
                if (i == 4) { pawnMovedX = 6; pawnMovedY = 5; pawnDestinationX = 6; pawnDestinationY = 2; }
                if (i == 5) { pawnMovedX = 3; pawnMovedY = 0; pawnDestinationX = 3; pawnDestinationY = 1; }//Atk
                if (i == 6) { pawnMovedX = 5; pawnMovedY = 5; pawnDestinationX = 6; pawnDestinationY = 5; }
                if (i == 7) { pawnMovedX = 3; pawnMovedY = 1; pawnDestinationX = 3; pawnDestinationY = 0; }//Atk
                if (i == 8) { pawnMovedX = 6; pawnMovedY = 5; pawnDestinationX = 6; pawnDestinationY = 3; }
                if (i == 9) { pawnMovedX = 3; pawnMovedY = 0; pawnDestinationX = 3; pawnDestinationY = 1; }//Atk
                if (i == 10) { pawnMovedX = 6; pawnMovedY = 3; pawnDestinationX = 9; pawnDestinationY = 3; }
                if (i == 11) { pawnMovedX = 3; pawnMovedY = 1; pawnDestinationX = 3; pawnDestinationY = 0; }//Atk
                if (i == 12) { pawnMovedX = 9; pawnMovedY = 3; pawnDestinationX = 9; pawnDestinationY = 0; }
                if (i == 13) { pawnMovedX = 3; pawnMovedY = 0; pawnDestinationX = 3; pawnDestinationY = 1; }//Atk
                if (i == 14) { pawnMovedX = 9; pawnMovedY = 0; pawnDestinationX = 10; pawnDestinationY = 0; }
                atkPlaying = sut.IsAtkPlaying;               
                pawnMoved = sut.MovePawn(pawnMovedX, pawnMovedY, pawnDestinationX, pawnDestinationY);
                if (i == 14) Assert.That(sut.UpdateTurn, Is.EqualTo(false));
                if (sut.UpdateTurn() == false)
                {
                    atkPlaying = sut.IsAtkPlaying;
                    break;
                }
                else
                {
                    currentTafl = sut.Tafl;
                }
                i++;
            } while (i <= 14);//MAIN LOOP
            currentTafl = sut.Tafl;
            Assert.That(currentTafl[x, y], Is.EqualTo(Pawn.King));
        }
        //test : cannot entering into a forteress (try each forteress from each angle), try move should answer false
        [TestCase(3, 0, 0, 0)]
        [TestCase(7, 0, 10, 0)]
        [TestCase(3, 10, 0, 10)]
        [TestCase(7, 10, 10, 10)]
        public void Situation_cannot_enter_into_each_forteress(int x, int y,int x2, int y2)
        {
            throw new NotImplementedException();
        }
        //test : cannot moving non-king pawn across the throne
        [Test]
        public void Situation_moving_non_king_across_throne()
        {
            int i = 1;
            int pawnMovedX = 0;
            int pawnMovedY = 0;
            int pawnDestinationX = 0;
            int pawnDestinationY = 0;

            Game sut = new Game();
            var currentTafl = sut.Tafl;

            do
            {
                if (i == 1) { pawnMovedX = 3; pawnMovedY = 0; pawnDestinationX = 3; pawnDestinationY = 1; }//Atk
                if (i == 2) { pawnMovedX = 3; pawnMovedY = 5; pawnDestinationX = 2; pawnDestinationY = 5; }
                if (i == 3) { pawnMovedX = 3; pawnMovedY = 1; pawnDestinationX = 3; pawnDestinationY = 0; }//Atk
                if (i == 4) { pawnMovedX = 4; pawnMovedY = 5; pawnDestinationX = 3; pawnDestinationY = 5; }
                if (i == 5) { pawnMovedX = 3; pawnMovedY = 0; pawnDestinationX = 3; pawnDestinationY = 1; }//Atk
                if (i == 6) { pawnMovedX = 5; pawnMovedY = 5; pawnDestinationX = 4; pawnDestinationY = 5; }
                if (i == 7) { pawnMovedX = 3; pawnMovedY = 1; pawnDestinationX = 3; pawnDestinationY = 0; }//Atk

                if (i == 8) { pawnMovedX = 6; pawnMovedY = 5; pawnDestinationX = 5; pawnDestinationY = 5; }

                bool atkPlaying = sut.IsAtkPlaying;
                bool pawnMoved;
                if (i == 8)
                {
                    Assert.Throws<ArgumentException>(() => pawnMoved = sut.MovePawn(pawnMovedX, pawnMovedY, pawnDestinationX, pawnDestinationY));
                }
                else
                {
                    pawnMoved = sut.MovePawn(pawnMovedX, pawnMovedY, pawnDestinationX, pawnDestinationY);
                }
                if (sut.UpdateTurn() == false)
                {
                    atkPlaying = sut.IsAtkPlaying;
                    break;
                }
                else
                {
                    currentTafl = sut.Tafl;
                }
                i++;
            } while (i <= 8);//MAIN LOOP
        }
        //test : cannot moving pawn across existing pawn
        [TestCase(4,0,2,0)]
        public void Situation_moving_across_existing_pawn(int x, int y, int x2, int y2)
        {
            Game sut = new Game();
            var currentTafl = sut.Tafl;
            bool atkPlaying = sut.IsAtkPlaying;
            bool pawnMoved;
            currentTafl = sut.Tafl;
            Assert.Throws<ArgumentException>(() => pawnMoved = sut.MovePawn(x, y, x2, y2));
        }
        
        //TODO
        //test : encircle the king and his servant (try simple case, 1 servant. Then 2, 3... Try the big one with all servant !)
    }
}

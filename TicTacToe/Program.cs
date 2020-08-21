using System;
using System.Dynamic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml.Linq;

namespace TicTacToe
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Tic Tac Toe GBI";
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
        NewGame:;
            Console.Clear();
            Board TicTacToeBoard = new Board();
            TicTacToeBoard.Board_ruller = ResetBoard(TicTacToeBoard.Board_ruller);
            TicTacToeBoard.xyBoard = ResetBoardResult(TicTacToeBoard.xyBoard);
            while (true)
            {
                TicTacToeBoard.PlaceOnBoard();  
                if(TicTacToeBoard.IsWinnerVar)
                {
                    goto AskAgain;
                }
                else if(TicTacToeBoard.ResetGameAsTie())
                {
                    Console.SetCursorPosition(0, 6);
                    Console.WriteLine("Tie!!");
                    Thread.Sleep(2000);
                    goto NewGame;
                }
            }
        AskAgain:;
            Console.WriteLine("Wanna Start New Game? (Y/N)");
            char answer = Console.ReadKey().KeyChar;
            if(answer.ToString().ToLower() == "y")
            {
                goto NewGame;
            }
            else if(answer.ToString().ToLower() == "n")
            {              
                return;
            }
            else
            {
                goto AskAgain;
            }
        }

        public static XY ResetBoardResult(XY xyBoard)
        {
            xyBoard.TopLeft.Right = "1";
            xyBoard.TopMiddle.Right = "2";
            xyBoard.TopRight.Right = "3";
            xyBoard.TopLeft.Top = "4";
            xyBoard.TopMiddle.Top = "5";
            xyBoard.TopRight.Top = "6";
            xyBoard.TopLeft.Middle = "7";
            xyBoard.TopMiddle.Middle = "8";
            xyBoard.TopRight.Middle = "9";
            return xyBoard;
        }

        public static BoardPlaces ResetBoard(BoardPlaces Board)
        {
            Board.TopLeft.Middle = false;
            Board.TopLeft.Right = false;
            Board.TopLeft.Top = false;
            Board.TopMiddle.Middle = false;
            Board.TopMiddle.Right = false;
            Board.TopMiddle.Top = false;
            Board.TopRight.Middle = false;
            Board.TopRight.Right = false;
            Board.TopRight.Top = false;
            return Board;
        }

    }

    public class Board
    {
        private int rightOnBoard = 0;
        private int DownOnBoard = 0;
        private int numberofturns;
        public bool IsWinnerVar = false;
        public BoardPlaces Board_ruller;
        public XY xyBoard;
        public bool Winner;
        private bool IsEmptyBool = false;
        private string XorY;
        public int Times { get; set; }
        public Board()
        {
            this.Winner=false;
            Console.WriteLine("   |   |   ");
            Console.WriteLine("-----------");
            Console.WriteLine("   |   |   ");
            Console.WriteLine("-----------");
            Console.WriteLine("   |   |   ");
            IsEmptyBool = false;
            Times = 0;
            XorY = "X ";
            numberofturns = 0;
        }
        public bool ResetGameAsTie()
        {
            if(numberofturns>=9)
            {
                return true;
            }
            return false;               
        }
        public void New_turn()
        {
            if (Times%2==0)
            {
                this.XorY = "O ";
            }
            else
            {
                this.XorY = "X ";
            }
            Times++;
        }
        public void PlaceOnBoard()
        {
            Console.SetCursorPosition(0, 0);
            while (true)
            {
                ConsoleKey ReadKey = Console.ReadKey().Key;
                if (ReadKey == ConsoleKey.D)
                {
                    Console.SetCursorPosition(rightOnBoard, DownOnBoard);
                    Console.WriteLine(" ");
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine(" ");
                    Console.SetCursorPosition(rightOnBoard, DownOnBoard);
                    if (rightOnBoard < 8)
                    {
                        rightOnBoard += 4;
                    }

                }
                else if (ReadKey == ConsoleKey.A)
                {
                    Console.SetCursorPosition(rightOnBoard, DownOnBoard);
                    Console.WriteLine(" ");
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine(" ");
                    Console.SetCursorPosition(rightOnBoard, DownOnBoard);
                    if (rightOnBoard > 0)
                    {
                        rightOnBoard -= 4;
                    }
                }
                else if (ReadKey == ConsoleKey.W)
                {
                    Console.SetCursorPosition(rightOnBoard, DownOnBoard);
                    Console.WriteLine(" ");
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine(" ");
                    Console.SetCursorPosition(rightOnBoard, DownOnBoard);
                    if (DownOnBoard > 0)
                    {
                        DownOnBoard -= 2;
                    }

                }
                else if (ReadKey == ConsoleKey.S)
                {
                    Console.SetCursorPosition(rightOnBoard, DownOnBoard);
                    Console.WriteLine(" ");
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine(" ");
                    Console.SetCursorPosition(rightOnBoard, DownOnBoard);
                    if (DownOnBoard < 4)
                    {
                        DownOnBoard += 2;
                    }
                }
                else //Dont Want To Move AnyMore
                {
                    IsEmptyBool = IsEmpty();
                    if(IsEmptyBool)
                    {
                        numberofturns++;
                    }
                    if(IsWinner())
                    {
                        IsWinnerVar = true;
                    }
                    return;
                }
                Console.SetCursorPosition(rightOnBoard, DownOnBoard);
            }

        }

        public bool IsEmpty()
        {
            int find_right = rightOnBoard / 3;
            int find_down = DownOnBoard / 2;
            New_turn();
            if(find_right == 0) //left
            {
                if (find_down == 0) //top left
                {
                    if (Board_ruller.TopLeft.Top != true)
                    {
                        Console.Write(XorY);
                        Board_ruller.TopLeft.Top = true;
                        xyBoard.TopLeft.Top = XorY;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (find_down == 1)
                {
                    if (Board_ruller.TopLeft.Middle != true)
                    {
                        Console.Write(XorY);
                        Board_ruller.TopLeft.Middle = true;
                        xyBoard.TopLeft.Middle = XorY;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (Board_ruller.TopLeft.Right != true)
                    {
                        Console.Write(XorY);
                        Board_ruller.TopLeft.Right = true;
                        xyBoard.TopLeft.Right = XorY;
                        return true;
                    }
                    else
                    { 
                        return false;
                    }
                }
            }
            else if(find_right == 1) //middle
            {
                if (find_down == 0) 
                {
                    if (Board_ruller.TopMiddle.Top != true)
                    {
                        Console.Write(XorY);
                        Board_ruller.TopMiddle.Top = true;
                        xyBoard.TopMiddle.Top = XorY;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (find_down == 1)
                {
                    if (Board_ruller.TopMiddle.Middle != true)
                    {
                        Console.Write(XorY);
                        xyBoard.TopMiddle.Middle = XorY;
                        Board_ruller.TopMiddle.Middle = true;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (Board_ruller.TopMiddle.Right != true)
                    {
                        Console.Write(XorY);
                        xyBoard.TopMiddle.Right = XorY;
                        Board_ruller.TopMiddle.Right = true;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else //right
            {
                if (find_down == 0) //top left
                {
                    if (Board_ruller.TopRight.Top != true)
                    {
                        Console.Write(XorY);
                        Board_ruller.TopRight.Top = true;
                        xyBoard.TopRight.Top = XorY;
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Already Been Taken");
                        return false;
                    }
                }
                else if (find_down == 1)
                {
                    if (Board_ruller.TopRight.Middle != true)
                    {
                        Console.Write(XorY);
                        xyBoard.TopRight.Middle = XorY;
                        Board_ruller.TopRight.Middle = true;
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Already Been Taken");
                        return false;
                    }
                }
                else
                {
                    if (Board_ruller.TopRight.Right != true)
                    {
                        xyBoard.TopRight.Right = XorY;
                        Console.Write(XorY);
                        Board_ruller.TopRight.Right = true;
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Already Been Taken");
                        return false;
                    }
                }
            }
        } //tells if the place is empty 
        public bool IsWinner()
        {
            //lines
            if(xyBoard.TopLeft.Top == xyBoard.TopLeft.Middle && xyBoard.TopLeft.Top  == xyBoard.TopLeft.Right)
            {
                Console.SetCursorPosition(0, 6);
                Console.WriteLine(xyBoard.TopLeft.Top + " Is the winner!!!");
                return true;
            }
            if (xyBoard.TopMiddle.Top == xyBoard.TopMiddle.Middle && xyBoard.TopMiddle.Top == xyBoard.TopMiddle.Right)
            {
                Console.SetCursorPosition(0, 6);
                Console.WriteLine(xyBoard.TopMiddle.Top + " Is the winner!!!");
                return true;
            }
            if (xyBoard.TopRight.Top == xyBoard.TopRight.Middle && xyBoard.TopRight.Top == xyBoard.TopRight.Right)
            {
                Console.SetCursorPosition(0, 6);
                Console.WriteLine(xyBoard.TopMiddle.Top + " Is the winner!!!");
                return true;
            }
            //rows
            if (xyBoard.TopMiddle.Top == xyBoard.TopRight.Top && xyBoard.TopRight.Top == xyBoard.TopLeft.Top)
            {
                Console.SetCursorPosition(0, 6);
                Console.WriteLine(xyBoard.TopMiddle.Top + " Is the winner!!!");
                return true;
            }
            if (xyBoard.TopMiddle.Middle == xyBoard.TopRight.Middle && xyBoard.TopRight.Middle == xyBoard.TopLeft.Middle)
            {
                Console.SetCursorPosition(0, 6);
                Console.WriteLine(xyBoard.TopMiddle.Middle + " Is the winner!!!");
                return true;
            }
            if (xyBoard.TopMiddle.Right == xyBoard.TopRight.Right && xyBoard.TopRight.Right == xyBoard.TopLeft.Right)
            {
                Console.SetCursorPosition(0, 6);
                Console.WriteLine(xyBoard.TopMiddle.Right + " Is the winner!!!");
                return true;
            }
            //alacson
            if(xyBoard.TopLeft.Top == xyBoard.TopMiddle.Middle && xyBoard.TopRight.Right == xyBoard.TopMiddle.Middle)
            {
                Console.SetCursorPosition(0, 6);
                Console.WriteLine(xyBoard.TopLeft.Right + " Is the winner!!!");
                return true;
            }
            if (xyBoard.TopRight.Top == xyBoard.TopMiddle.Middle && xyBoard.TopLeft.Right == xyBoard.TopMiddle.Middle)
            {
                Console.SetCursorPosition(0, 6);
                Console.WriteLine(xyBoard.TopLeft.Right + " Is the winner!!!");
                return true;
            }
            return false;
        }
    }

    public struct BoardPlaces
    {
        public RightLeftMiddle TopLeft;
        public RightLeftMiddle TopMiddle;
        public RightLeftMiddle TopRight;
    }

    public struct RightLeftMiddle
    {
        public bool Top;
        public bool Middle;
        public bool Right;

    }

    public struct XY
    {
        public RLM TopLeft;
        public RLM TopMiddle;
        public RLM TopRight;
          
    }

    public struct RLM
    {
        public string Top;
        public string Middle;
        public string Right;
    }
}

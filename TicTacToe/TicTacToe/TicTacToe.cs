using System;
using System.Drawing;
using DoubleZ.Extensions;

namespace TicTacToe
{
    public enum Player { None, X, O };

    public class InvalidTrisMoveException : Exception
    {
        public InvalidTrisMoveException() : base() { }
    }

    public class TicTacToe
    {
        public Player[,] Board { get; private set; } = new Player[3, 3];
        public Player Turn { get; private set; }

        public void Move(Point point)
        {
            if (point.X < 0 || point.X > 3 && point.Y < 0 || point.Y > 3)
                throw new InvalidTrisMoveException();

            if (Board.FromPoint(point) == Player.None && (Winner() == Player.None || IsADraw()))
            {
                Board.FromPoint(point) = Turn;
                Turn = Turn == Player.X ? Player.O : Player.X;
            }
        }

        public Player Winner()
        {
            for (int i = 0; i < 3; i++)
                if (Board[i, i] != default)
                {
                    if (Board[i, 0] == Board[i, 1] && Board[i, 0] == Board[i, 2]) return Board[i, 0];
                    if (Board[0, i] == Board[1, i] && Board[0, i] == Board[2, i]) return Board[0, i];
                }

            if (Board[1, 1] != default && (Board[1, 1] == Board[0, 0] && Board[1, 1] == Board[2, 2] || Board[1, 1] == Board[0, 2] && Board[1, 1] == Board[2, 0]))
                return Board[1, 1];
            return default;
        }

        public bool IsADraw()
        {
            foreach (Player player in Board)
                if (player != Player.None) return false;
            return true;
        }

        public void Reset()
        {
            Board = new Player[3, 3];
        }
    }
}
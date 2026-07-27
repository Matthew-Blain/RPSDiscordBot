using RPSDiscordBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPSDiscordBot.Services
{
    public class RpsService
    {
        private const ulong NiallsUserID = 434852151475306496;
        private readonly Random _random = new();

        public RpsResult Play(ulong playerId, ulong opponentId)
        {
            var result = new RpsResult
            {
                PlayerMove = RandomMove(),
                OpponentMove = RandomMove()
            };

            // Niall is player 1
            if (playerId == NiallsUserID)
            {
                if (!Beats(result.PlayerMove, result.OpponentMove))
                {
                    result.PlayerMove = WinningMoveAgainst(result.OpponentMove);
                }

                result.WinnerId = playerId;
                return result;
            }

            // Niall is player 2
            if (opponentId == NiallsUserID)
            {
                if (!Beats(result.OpponentMove, result.PlayerMove))
                {
                    result.OpponentMove = WinningMoveAgainst(result.PlayerMove);
                }

                result.WinnerId = opponentId;
                return result;
            }

            // Normal game
            if (result.PlayerMove == result.OpponentMove)
            {
                result.WinnerId = 0; // Draw
            }
            else
            {
                result.WinnerId = Beats(result.PlayerMove, result.OpponentMove)
                    ? playerId
                    : opponentId;
            }

            return result;
        }

        private Move RandomMove()
        {
            return (Move)_random.Next(0, 3);
        }

        private static bool Beats(Move first, Move second)
        {
            return (first == Move.Rock && second == Move.Scissors)
                || (first == Move.Paper && second == Move.Rock)
                || (first == Move.Scissors && second == Move.Paper);
        }

        private static Move WinningMoveAgainst(Move move)
        {
            return move switch
            {
                Move.Rock => Move.Paper,
                Move.Paper => Move.Scissors,
                Move.Scissors => Move.Rock,
                _ => Move.Rock
            };
        }

    }
}

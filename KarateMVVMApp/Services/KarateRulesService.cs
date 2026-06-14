using KarateMVVMApp.Helpers;
using KarateMVVMApp.Helpers.Enums;
using KarateMVVMApp.Models;

namespace KarateMVVMApp.Services
{
    public record MatchState(
        int AkaPoints,
        int AoPoints,
        int AkaCat1Count,
        int AoCat1Count,
        int AkaCat2Count,
        int AoCat2Count
    );

    public class KarateRulesService
    {
        private readonly GameSettings _settings;

        public KarateRulesService(GameSettings settings)
        {
            _settings = settings;
        }

        public MatchResult? Check(MatchState state)
        {
            MatchResult? res = null;

            if (state.AkaCat1Count >= _settings.WarningsToDisqualify) res = MatchResultCreator(Athlete.AO);
            else if (state.AoCat1Count >= _settings.WarningsToDisqualify) res = MatchResultCreator(Athlete.AKA);
            else if (state.AkaCat2Count >= _settings.WarningsToDisqualify) res = MatchResultCreator(Athlete.AO);
            else if (state.AoCat2Count >= _settings.WarningsToDisqualify) res = MatchResultCreator(Athlete.AKA);
            else if (_settings.HasPointLimit)
            {
                if (state.AkaPoints >= _settings.PointsToWin) res = MatchResultCreator(Athlete.AKA);
                else if (state.AoPoints >= _settings.PointsToWin) res = MatchResultCreator(Athlete.AO);
            }
            
            return res;
        }

        public MatchResult CheckWinnerAfterTimeout(int AkaPoints, int AoPoints)
        {
            return AkaPoints == AoPoints ? MatchResultCreator(Athlete.None) 
                                         : MatchResultCreator(AkaPoints > AoPoints ? Athlete.AKA : Athlete.AO);
        }

        private MatchResult MatchResultCreator(Athlete winner)
        {
            return new MatchResult
            {
                WinnerColor = GetWinnerColor(winner)
            };
        }

        private string GetWinnerColor(Athlete winner) => winner switch
        {
            Athlete.AKA => _settings.ColorAka,
            Athlete.AO => _settings.ColorAo,
            Athlete.None => _settings.DrawColor,
            _ => _settings.DrawColor
        };
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Voting.Models
{
    public class VotingDbInitializer : DropCreateDatabaseAlways<VotingContext>
    {
        protected override void Seed(VotingContext context)
        {
            var teamDyn = new Team() { Name = "Dynamo" };
            var teamMan = new Team() { Name = "Manchester United" };
            var matchResultsDM = new MatchResults() { HomeTeam = teamDyn, GuestTeam = teamMan };
            var teamRom = new Team() { Name = "Roma" };
            var teamSpo = new Team() { Name = "Sporting" };
            var matchResultsRS = new MatchResults() { HomeTeam = teamRom, GuestTeam = teamSpo };
            var teamMar = new Team() { Name = "Marsel" };
            var teamPor = new Team() { Name = "Portu" };
            var matchResultsMP = new MatchResults() { HomeTeam = teamMar, GuestTeam = teamPor };
            var teamChe = new Team() { Name = "Chelsea" };
            var teamSha = new Team() { Name = "Shalke" };
            var matchResultsCS = new MatchResults() { HomeTeam = teamChe, GuestTeam = teamSha };
            var teamVer = new Team() { Name = "Verder" };
            var teamLaz = new Team() { Name = "Lazio" };
            var matchResultsVL = new MatchResults() { HomeTeam = teamVer, GuestTeam = teamLaz };

            context.Teams.Add(teamDyn);
            context.Teams.Add(teamMan);
            context.Teams.Add(teamRom);
            context.Teams.Add(teamSpo);
            context.Teams.Add(teamMar);
            context.Teams.Add(teamPor);
            context.Teams.Add(teamChe);
            context.Teams.Add(teamSha);
            context.Teams.Add(teamVer);
            context.Teams.Add(teamLaz);

            context.MatchResults.Add(matchResultsDM);
            context.MatchResults.Add(matchResultsRS);
            context.MatchResults.Add(matchResultsMP);
            context.MatchResults.Add(matchResultsCS);
            context.MatchResults.Add(matchResultsVL);

            base.Seed(context);
        }
    }
}
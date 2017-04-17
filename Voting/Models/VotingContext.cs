using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Voting.Models
{
    public class VotingContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<MatchResults> MatchResults { get; set; }
    }
}
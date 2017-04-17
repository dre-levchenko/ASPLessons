using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Voting.Models
{
    public class MatchResults
    {
        public virtual int Id { set; get; }
        public virtual Team HomeTeam { set; get; }
        public virtual Team GuestTeam { set; get; }
        public virtual int HomeWon { get; set; }
        public virtual int GuestWon { get; set; }
        public virtual int Draw { get; set; }
    }
}
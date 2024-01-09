using System;
using System.Collections.Generic;

namespace login.Entities
{
    public partial class Aspnetrole
    {
        public Aspnetrole()
        {
            Aspnetroleclaims = new HashSet<Aspnetroleclaim>();
            Users = new HashSet<Aspnetuser>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }

        public virtual ICollection<Aspnetroleclaim> Aspnetroleclaims { get; set; }

        public virtual ICollection<Aspnetuser> Users { get; set; }
    }
}

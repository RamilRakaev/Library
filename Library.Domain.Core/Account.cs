using Library.Domain.Interfaces.IData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Core
{
    public class Account:IAccount
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Password { get; set; } = "";

        public string Email { get; set; } = "";

        public string Rights { get; set; } = "";

        public sbyte BusyBooks { get; set; }

        public ulong Penalty { get; set; }
    }
}

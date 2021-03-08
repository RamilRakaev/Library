using Library.Domain.Interfaces.IData;

namespace Library.Infrastructure.Bll.Data
{
    public class Account: IAccount
    {
        public Account()
        {

        }

        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Password { get; set; } = "";

        public string Email { get; set; } = "";

        public string Rights { get; set; } = "";

        public sbyte BusyBooks { get; set; }

        public ulong Penalty { get; set; }
    }
}

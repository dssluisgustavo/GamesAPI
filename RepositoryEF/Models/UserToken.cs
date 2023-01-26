using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryEF.Models
{
    public partial class UserToken
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public string RefreshToken { get; set; } = null;

        public DateTime ExpirationDate { get; set; }

        public virtual User? User { get; set; }
    }
}

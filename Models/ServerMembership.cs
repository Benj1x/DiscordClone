using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [PrimaryKey(nameof(RelationID))]
    public class ServerMembership
    {
        public string RelationID { get; set; }
        public string UserID { get; set; }
        //[ForeignKey(nameof(ServerID))]
        public string ServerID { get; set; }
    }
}

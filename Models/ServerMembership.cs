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
        public Int64 RelationID { get; set; }
        public Int64 UserID { get; set; }
        //[ForeignKey(nameof(ServerID))]
        public Int64 ServerID { get; set; }
    }
}

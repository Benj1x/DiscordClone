using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [PrimaryKey(nameof(MessageID))]
    public class Message
    {
        public string ChannelID {  get; set; }
        public string SenderID { get; set; }
        public string MessageID { get; set; } //Need something more unique as the PK?? (discord uses ((channelid, bucket), messageid)
        public string? MessageContent { get; set; }
        //byte should be some file
        public byte[][]? MessageAttachments { get; set; }
        [NotMapped]
        public IFormFile[]? FormMessageAttachments { get; set; }
        public DateTime? SendAt { get; set; }
    }
}
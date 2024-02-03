using System.Drawing;

namespace Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isAdmin { get; set; }
        public Color Color { get; set; }
        public byte[] Icon { get; set; }
        public bool Mentionable { get; set; }
        
        /*  "permissions": "559623605571137",
            "position": 0,
            "managed": false,
            "mentionable": false,
            "unicode_emoji": null,
            "flags": 0
        */
    }
}
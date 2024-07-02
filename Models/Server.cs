using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Models
{
    [PrimaryKey(nameof(ServerID))]
    public class Server
    {
        public string ServerID { get; set; } = Guid.NewGuid().ToString("N");
        [ForeignKey(nameof(OwnerID))]
        public string OwnerID { get; set; }
        public string ServerName { get; set; }
        [NotMapped]
        public IFormFile? FormServerIcon { get; set; }
        public byte[]? ServerIcon { get; set; }
        //public Int64[]? Admins { get; set; } = null;
        public List<Channel>? Channels { get; set; } = new List<Channel>();

        public string? AFKChannelID { get; set; } = null;
        public int? AFKTimeout { get; set; } = null;
        public string ServerRegion { get; set; }
        //public Role[] Roles { get; set; }


    }
    public class Channel
    {
        public string ChannelID { get; set; }
        public string ChannelName { get; set; }
    }
}

/*
    "description": null,
    "home_header": null,
    "splash": null,
    "discovery_splash": null,
    "features": [],
    "banner": null,
    "application_id": null,
    "system_channel_id": "1200865876405137451",
    "system_channel_flags": 0,
    "widget_enabled": false,
    "widget_channel_id": null,
    "verification_level": 0,
    "default_message_notifications": 0,
    "mfa_level": 0,
    "explicit_content_filter": 0,
    "max_presences": null,
    "max_members": 500000,
    "max_stage_video_channel_users": 50,
    "max_video_channel_users": 25,
    "vanity_url_code": null,
    "premium_tier": 0,
    "premium_subscription_count": 0,
    "preferred_locale": "en-US",
    "rules_channel_id": null,
    "safety_alerts_channel_id": null,
    "public_updates_channel_id": null,
    "hub_type": null,
    "premium_progress_bar_enabled": false,
    "latest_onboarding_question_id": null,
    "nsfw": false,
    "nsfw_level": 0,
    "emojis": [],
    "stickers": [],
    "incidents_data": null,
    "inventory_settings": null,
    "embed_enabled": false,
    "embed_channel_id": null*/
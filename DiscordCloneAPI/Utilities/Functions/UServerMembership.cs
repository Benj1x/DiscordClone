﻿using DiscordCloneAPI.DBContexts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Net;
using System.Threading.Channels;
using System.Web.Mvc;

namespace DiscordCloneAPI.Utilities.Functions
{
    public class UServerMembership
    {
        private readonly MembershipContext _context;
        public UServerMembership(MembershipContext context)
        {
            _context = context;
        }

        /// <summary>
        /// <c>GetMemberships</c> Gets all memberships for a server.
        /// </summary>
        /// <returns>A List of <c>ServerMembership</c></returns>
        public async Task<ActionResult<IEnumerable<ServerMembership>>> GetMemberships(long ServerID)
        {
            return await _context.Memberships.Where(e => e.ServerID.Equals(ServerID)).ToListAsync();
        }

        //Get specific membership
        public async Task<ActionResult<ServerMembership>> GetServerMembership(long RelationID)
        {
            var serverMembership = await _context.Memberships.FindAsync(RelationID);

            return serverMembership;
        }

        public async Task<ActionResult<ServerMembership>> GetUserMemberships(long UserID)
        {
            var serverMembership = await _context.Memberships.FindAsync(UserID);

            return serverMembership;
        }

        public List<Server> GenerateRandomServers()
        {
            var random = new Random();
            var servers = new List<Server>();

            for (int i = 0; i < 25; i++)
            {
                Models.Channel channel = new Models.Channel();
                channel.ChannelID = Guid.NewGuid().ToString("N");
                channel.ChannelName = "General";
                
                var server = new Server
                {
                    OwnerID = Guid.NewGuid().ToString("N"),
                    ServerName = $"Server{Guid.NewGuid().ToString("N")}",
                    FormServerIcon = null,
                    Channels = GenerateRandomChannels(random.Next(1, 5)), // Random number of channels
                    AFKChannelID = Guid.NewGuid().ToString("N"),
                    AFKTimeout = random.Next(1, 10000),
                    ServerRegion = $"Region{Guid.NewGuid().ToString("N")}"
                    
                };
                //server.Channels.Add(new Models.Channel() { ChannelID = channel.ChannelID, ChannelName = "General"});
                //server.Channels.Add(channel);
                servers.Add(server);
            }

            return servers;
        }
        private static List<Models.Channel> GenerateRandomChannels(int count)
        {
            string temp = string.Empty;
            var random = new Random();
            var channels = new List<Models.Channel>();
            for (int i = 0; i < count; i++)
            {
                temp = Guid.NewGuid().ToString("N");
                channels.Add(new Models.Channel
                {
                    ChannelID = temp,
                    ChannelName = $"Channel{Guid.NewGuid().ToString("N")}",
                    Messages = GenerateRandomMessages(random.Next(1, 20), temp) // Random number of messages
                });
            }

            return channels;
        }
        private static List<Message> GenerateRandomMessages(int count, string Channel)
        {
            var messages = new List<Message>();
            for (int i = 0; i < count; i++)
            {
                messages.Add(new Message
                {
                    ChannelID = Channel,
                    OwnerID = $"User{Guid.NewGuid().ToString("N")}",
                    MessageID = Guid.NewGuid().ToString("N"),
                    MessageContent = $"Content{Guid.NewGuid().ToString("N")}"
                });
            }

            return messages;
        }

        /// <summary>
        /// <c>PostServerMembership</c> adds a user to a server.
        /// </summary>
        /// <returns>True if successfully added false if not</returns>
        public async Task<bool> PostServerMembership(ServerMembership serverMembership)
        {
            Random random = new Random();

            if (ServerMembershipExists(serverMembership.UserID, serverMembership.ServerID))
            {
                return false;
            }

            try 
            {
                _context.Memberships.Add(serverMembership);
            } 
            catch (Exception ex)
            {
                
                    _context.Memberships.Add(serverMembership);
                    await _context.SaveChangesAsync();
               
                
            }
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// <c>DeleteServerMembership</c> "Deletes" a relation (user left the server).
        /// </summary>
        /// <returns>true if successful, user wasn't found in the server</returns>
        /// <remarks>Add a notify/listen for the server?</remarks>
        public async Task<bool> DeleteServerMembership(string id)
        {
            var serverMembership = await _context.Memberships.FindAsync(id);
            if (serverMembership == null)
            {
                return false; //User not found in the server
            }

            _context.Memberships.Remove(serverMembership);
            await _context.SaveChangesAsync();

            return true; //Success 
        }

        public async Task<bool> DeleteAllServerMemberships(string ServerID)
        {
   
            var ServerMembers = _context.Memberships.Where(e => e.ServerID.Equals(ServerID));
            _context.Memberships.RemoveRange(ServerMembers);
            try
            {
                await _context.SaveChangesAsync();
            } catch (Exception ex)
            {
                throw;
            }
            

            return true; //Success 
        }

        /// <summary>
        /// <c>ServerMembershipExists</c> Checks if a user is already in a server
        /// </summary>
        /// <returns>true if found in the server, false if they weren't found in the server</returns>
        /// <seealso cref="ServerMembershipIDExists"/>
        private bool ServerMembershipExists(string userID, string serverID)
        {
            //If any relation contains the UserID && ServerID
            return _context.Memberships.Any(e => e.UserID.Equals(userID) && e.ServerID.Equals(serverID));
        }

    }
}

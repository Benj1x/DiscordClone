# DiscordClone
A discord clone with a client application for Android and Windows, written in .NET MAUI (!I'm regretting this so i will rewrite to something else, probably flutter!)

The backend will start out with an easy framework (probably .NET) and will probably be rewritten later, with a lower level language.

For VoIP the project will be utilising Opus+WebRTC.

While researching I got stuck on choosing between MongoDB, Cassandra, I knew discord were using Cassandra for it's own databases, so it was an obvious choice, but I did not feel like basing my choice on their own implementation.
During my research, to choose between MongoDB and Cassandra, I discovered ScyllaDB and it seemed like the obvious choice. I did some searching and discovered that discord had actually (once again) migrated their database, this time to ScyllaDB. Having confirmed it was a solid choice, I chose to stick with this

https://discord.com/blog/how-discord-stores-trillions-of-messages

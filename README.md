# Basset
[![Discord](https://discordapp.com/api/guilds/158057120493862912/widget.png)](https://discord.gg/B4BwQ8r)  
View your guild's Spotify listening activity and generate recommendations based on popular tracks. [Click here for the bot invite](https://discordapp.com/oauth2/authorize/?permissions=67584&scope=bot&client_id=593797043458146318).

<p align="center">
  <img src="basset_image.jpg">
</p>

### Commands

All commands are activated by mentioning the bot `@Basset` and by default all non-guild-admin users have a 1 minute cooldown on commands.

#### Random
`random track/song (user)`  
Get a random track someone once listened to in your guild, optionally specify a user.

#### Spotify
`genres`  
List all genres available for use in the recommendation commands

#### Top
`top tracks/t`  
View the top 10 tracks that people have been listening to in your guild.

`top listeners/l/listens/users`  
View the top 10 music listeners in your guild.

`top (@user)`  
View the top 10 tracks the specified user has been listening to, if no user is provided your own top tracks are shown.

#### Recommendation
`recommend`  
Get some songs chosen based on this guild's track listening activity.

`recommend user (@user)`  
Get some songs chosen based on the specified user's listening activity, if no user is provided your own recommended tracks are shown.

`recommend tracks (track ids...)`  
Specify up to 5 track ids to get a list of similar songs.

`recommend artists (artist ids...)`  
Specify up to 5 artist ids to get a list of similar songs.

`recommend genres (genres...)`  
Specify up to 5 genres to get a list of songs similar to that style.


#### Search
`search (type) (query)`  
Search for tracks or artists based on a special [spotify-specific query](https://developer.spotify.com/documentation/web-api/reference/search/search/#writing-a-query---guidelines). Available types are `track` and `artist`.
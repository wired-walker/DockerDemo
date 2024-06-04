using Discord.Webhook;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace UptimeMonitor
{
    internal class DiscordNotifier
    {

        internal async void NotifyChannel()
        {
            // The webhook url follows the format https://discord.com/api/webhooks/{id}/{token}
            // Because anyone with the webhook URL can use your webhook
            // you should NOT hard code the URL or ID + token into your application.
            using var client = new DiscordWebhookClient("https://discord.com/api/webhooks/1245545099707027478/6Cw7nYZ4D42JcfgMd7QJAYjzoTVVbK_bp6usZ13Gv21DOBswBjfZg3uJ1AnuekAjS-Td");

            var embed = new EmbedBuilder
            {
                Title = "Test Embed",
                Description = "Test Description"
            };

            // Webhooks are able to send multiple embeds per message
            // As such, your embeds must be passed as a collection.
            await client.SendMessageAsync(text: "Send a message to this webhook!", embeds: new[] { embed.Build() });
        }
    }


}

using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.Logging;
using Rhisis.Core.Common;
using Rhisis.Core.DependencyInjection;
using Rhisis.World.Game.Entities;
using Rhisis.World.Packets;
using System;
using System.IO;
using System.Reflection;

namespace Rhisis.World.Game.Chat
{
    public class ScriptCommand
    {
        private static ILogger Logger => DependencyContainer.Instance.Resolve<ILogger<AddDamageCommand>>();

        public class ScriptGlobals
        {
            public IPlayerEntity Player;
        }

        [ChatCommand(".script", AuthorityType.Administrator)]
        public async static void Script(IPlayerEntity player, string[] parameters)
        {
            if(parameters.Length == 0)
            {
                WorldPacketFactory.SendWorldMsg(player, "A minimum of 1 additional parameter is required to execute this command.");
                return;
            }

            var scriptName = parameters[0];

            if (!Directory.Exists("scripts"))
            {
                WorldPacketFactory.SendWorldMsg(player, "The scripts directory does not exist.");
                return;
            }

            var scriptPath = Path.Combine("scripts", scriptName);

            if (!File.Exists(scriptPath))
            {
                WorldPacketFactory.SendWorldMsg(player, "The specified script does not exist.");
                return;
            }

            var data = await File.ReadAllTextAsync(scriptPath);

            try
            {
                var scriptOptions = ScriptOptions.Default
                    .WithReferences(Assembly.GetExecutingAssembly())
                    .WithImports("System");

                await Microsoft.CodeAnalysis.CSharp.Scripting.CSharpScript.RunAsync(data, globals: new ScriptGlobals
                {
                    Player = player
                }, options: scriptOptions);
            }
            catch(Exception e)
            {
                WorldPacketFactory.SendWorldMsg(player, "Compilation of Script failed!");
                Logger.LogTrace(e, "Compilation of Script failed!");
            }
        }
    }
}

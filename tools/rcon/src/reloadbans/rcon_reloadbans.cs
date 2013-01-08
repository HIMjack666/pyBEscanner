#region

using System;
using System.Net;
using System.Threading;
using System.Diagnostics;
using BattleNET;

#endregion

namespace BattleNET_client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            
            BattlEyeLoginCredentials loginCredentials = new BattlEyeLoginCredentials();
            #region
            loginCredentials.Host = args[0];
            loginCredentials.Port = Convert.ToInt32(args[1]);
            loginCredentials.Password = args[2];
            #endregion
            

            IBattleNET b = new BattlEyeClient(loginCredentials);
            b.MessageReceivedEvent += DumpMessage;
            b.DisconnectEvent += Disconnected;
            b.ReconnectOnPacketLoss(true);
            b.Connect();

            if (b.IsConnected() == false)
            {
                Console.WriteLine("Couldnt connect to server");
				Console.WriteLine("Failed to reload bans");
                return;
            }

            b.SendCommandPacket(EBattlEyeCommand.loadBans);
            Thread.Sleep(1000); // wait 1 second  for no reason...
            b.Disconnect();

        }
                
        private static void Disconnected(BattlEyeDisconnectEventArgs args)
        {
            Console.WriteLine(args.Message);
        }

        private static void DumpMessage(BattlEyeMessageEventArgs args)
        {
            Console.WriteLine(args.Message);
        }
    }
}
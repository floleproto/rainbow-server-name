using EXILED;
using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MEC;
using GameCore;

namespace RainbowTag
{
    public class Main : Plugin
    {
        public override string getName => "RainbowTag";
        public bool isAvailable = false;
        public CoroutineHandle coroutineHandle;

        public override void OnDisable()
        {

            coroutineHandle.IsRunning = false;

        }

        public override void OnEnable()
        {
            coroutineHandle = Timing.RunCoroutine(ChangeServerName());
        }

        public IEnumerator<float> ChangeServerName()
        {
            for(; ; )
            {
                var serverName = typeof(ServerConsole).GetField("_serverName", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
                string serverNameString = ConfigFile.ServerConfig.GetString("server_name");
                serverNameString = serverNameString.Replace("<color=rainbow>", $"<color={String.Format("#{0:X6}", new System.Random().Next(0x1000000))}>");
                serverName.SetValue(null, serverNameString);
                yield return Timing.WaitForSeconds(10f);
            }   
        }

        public override void OnReload()
        {
            coroutineHandle.IsRunning = false;
        }
        
    }
}


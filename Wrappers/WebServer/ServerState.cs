using IntegratedAuthoringTool;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace WebServer
{
    public class ServerState
    {
        public string AdminKey { get; set; }
        public ConcurrentDictionary<string, string> ScenarioKeys { get; set; }
        public ConcurrentDictionary<string, IntegratedAuthoringToolAsset[]> Scenarios {get; set;}

        public ServerState()
        {
            this.ScenarioKeys = new ConcurrentDictionary<string, string>();
            this.Scenarios = new ConcurrentDictionary<string, IntegratedAuthoringToolAsset[]>();
        }
    }
}

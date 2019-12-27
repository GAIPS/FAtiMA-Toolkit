using GAIPS.Rage;
using IntegratedAuthoringTool;
using Newtonsoft.Json;
using RolePlayCharacter;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Utilities;
using WellFormedNames;
using WorldModel.DTOs;

namespace WebServer
{
    public enum APIResourceType
    {
        SCENARIOS, 
        INSTANCES,
        DECISIONS, 
        CHARACTERS, 
        BELIEFS, 
        MEMORIES, 
        PERCEPTIONS, 
        TICK, 
        ACTIONS
    }

    public class APIResource
    {
        public APIResourceType Type { get; private set; }
        public int URLSegmentSize { get; set; }
        public string URLFormat { get; set; }
        public string[] ValidOperations { get; set; }
        public string ValidBodyFormat { get; private set; }
        public Func<APIRequest, ConcurrentDictionary<string, IntegratedAuthoringToolAsset[]>, string> Execute { get; private set; }

        //scenarios
        public static APIResource SCENARIOS = new APIResource()
        {
            Type = APIResourceType.SCENARIOS,
            Execute = HandleScenariosRequest,
            URLFormat = "/scenarios",
            ValidOperations = new string[] { "GET", "POST", "DELETE" },
            URLSegmentSize = 1,
            ValidBodyFormat = "{\"scenario\" : \"<scenario.json>\", \"assets\" : \"\"<scenario.json>\"}"
        };

        //scenarios/{scenarioName}/instances
        public static APIResource INSTANCES = new APIResource()
        {
            Type = APIResourceType.INSTANCES,
            Execute = HandleInstancesRequest,
            URLFormat = "/scenarios/{scenarioName}/instances",
            ValidOperations = new string[] { "GET", "POST", "DELETE"},
            URLSegmentSize = 3
        };

        //scenarios/{scenarioName}/instances/{instanceId}/characters/{charName}/decisions
        public static APIResource DECISIONS = new APIResource()
        {
            Type = APIResourceType.DECISIONS,
            Execute = HandleDecisionsRequest,
            URLFormat = "/scenarios/{scenarioName}/instances/{instanceId}/characters/{charName}/decisions",
            ValidOperations = new string[] { "GET"},
            URLSegmentSize = 7
        };

        //scenarios/{scenarioName}/instances/{instanceId}/characters
        public static APIResource CHARACTERS = new APIResource()
        {
            Type = APIResourceType.CHARACTERS,
            Execute = HandleCharactersRequest,
            URLFormat = "/scenarios/{scenarioName}/instances/{instanceId}/characters",
            URLSegmentSize = 5,
            ValidOperations = new string[] { "GET" },
        };

        //scenarios/{scenarioName}/instances/{instanceId}/characters/{charName}/beliefs
        public static APIResource BELIEFS = new APIResource()
        {
            Type = APIResourceType.BELIEFS,
            Execute = HandleBeliefsRequest,
            URLFormat = "/scenarios/{scenarioName}/instances/{instanceId}/characters/{charName}/beliefs",
            URLSegmentSize = 7,
            ValidOperations = new string[] { "GET" },
        };

        //scenarios/{scenarioName}/instances/{instanceId}/characters/{charName}/memories
        public static APIResource MEMORIES = new APIResource()
        {
            Type = APIResourceType.MEMORIES,
            Execute = HandleMemoriesRequest,
            URLFormat = "/scenarios/{scenarioName}/instances/{instanceId}/characters/{charName}/memories",
            URLSegmentSize = 7,
            ValidOperations = new string[] { "GET" },
        };

        //scenarios/{scenarioName}/instances/{instanceId}/characters/{charName}/perceptions
        public static APIResource PERCEPTIONS = new APIResource()
        {
            Type = APIResourceType.PERCEPTIONS,
            Execute = HandlePerceptionsRequest,
            URLFormat = "/scenarios/{scenarioName}/instances/{instanceId}/characters/{charName}/perceptions",
            URLSegmentSize = 7,
            ValidOperations = new string[] { "POST" },
        };

        //scenarios/{scenarioName}/instances/{instanceId}/tick
        public static APIResource TICK = new APIResource()
        {
            Type = APIResourceType.TICK,
            Execute = HandleTickRequest,
            URLSegmentSize = 5,
            URLFormat = "/scenarios/{scenarioName}/instances/{instanceId}/tick",
            ValidOperations = new string[] { "GET" , "POST" },
        };

        //scenarios/{scenarioName}/instances/{instanceId}/actions
        public static APIResource ACTIONS = new APIResource()
        {
            Type = APIResourceType.ACTIONS,
            Execute = HandleActionsRequest,
            URLFormat = "/scenarios/{scenarioName}/instances/{instanceId}/actions",
            URLSegmentSize = 5,
            ValidOperations = new string[] { "POST" },
        };

        public static APIResource[] Set = { SCENARIOS, INSTANCES, TICK, ACTIONS, CHARACTERS, PERCEPTIONS, DECISIONS, BELIEFS, MEMORIES };

        public static APIResource FromString(string type)
        {
            return Set.FirstOrDefault(r => r.Type.ToString().ToLower() == type);
        }

        private static string HandleScenariosRequest(APIRequest req, ConcurrentDictionary<string, IntegratedAuthoringToolAsset[]> scenarios)
        {
            if (req.Method == HTTPMethod.GET) return JsonConvert.SerializeObject(scenarios.Keys);
            
            if (req.Method == HTTPMethod.POST)
            {

                if (!string.IsNullOrEmpty(req.RequestBody))
                {
                    IntegratedAuthoringToolAsset iat;
                    try
                    {
                        var request = JsonConvert.DeserializeObject<CreateScenarioRequestDTO>(req.RequestBody);
                        var assetStorage = AssetStorage.FromJson(request.Assets);
                        iat = IntegratedAuthoringToolAsset.FromJson(request.Scenario, assetStorage);
                        scenarios[iat.ScenarioName.ToLower()] = new IntegratedAuthoringToolAsset[HTTPFAtiMAServer.MAX_INSTANCES + 1];
                        
                        //original is kept at 0 and will be left unchanged
                        scenarios[iat.ScenarioName.ToLower()][0] = IntegratedAuthoringToolAsset.FromJson(request.Scenario, assetStorage); 
                        
                        //instance of id 1 is automatically created
                        scenarios[iat.ScenarioName.ToLower()][1] = IntegratedAuthoringToolAsset.FromJson(request.Scenario, assetStorage);
                    }
                    catch (Exception ex)
                    {
                        return JsonConvert.SerializeObject(ex.Message);
                    }

                    return JsonConvert.SerializeObject("Scenario named '" + iat.ScenarioName + "' created containing " +
                        "'" + iat.Characters.Count() + "' characters");
                }
                else
                {
                    return JsonConvert.SerializeObject("Error: Empty body in a create scenario request!");
                }
            }
            return APIErrors.ERROR_INVALID_FORMAT;
        }


        private static string HandleInstancesRequest(APIRequest req, ConcurrentDictionary<string, IntegratedAuthoringToolAsset[]> scenarios)
        {

            if (req.Method == HTTPMethod.GET)
            {
                var result = new List<int>();
                for (int i = 1; i < HTTPFAtiMAServer.MAX_INSTANCES + 1; i++)
                {
                    if (scenarios[req.ScenarioName][i] != null) result.Add(i);
                }
                return JsonConvert.SerializeObject(result);
            }
            else if (req.Method == HTTPMethod.POST)
            {
                for (int i = 1; i < HTTPFAtiMAServer.MAX_INSTANCES + 1; i++)
                {
                    if (scenarios[req.ScenarioName][i] == null)
                    {
                        var scenarioJson = scenarios[req.ScenarioName][0].ToJson();
                        scenarios[req.ScenarioName][i] = IntegratedAuthoringToolAsset.FromJson(scenarioJson, scenarios[req.ScenarioName][0].Assets);
                        return JsonConvert.SerializeObject(i);
                    }
                }
            }
            else if (req.Method == HTTPMethod.DELETE)
            {
                scenarios[req.ScenarioName][req.ScenarioInstance] = null;
                return JsonConvert.SerializeObject("Instance deleted.");
            }
             
            return APIErrors.ERROR_INVALID_FORMAT;
        }

        private static string HandleDecisionsRequest(APIRequest req, ConcurrentDictionary<string, IntegratedAuthoringToolAsset[]> scenarios)
        {
            if (req.Method != HTTPMethod.GET)
                return JsonConvert.SerializeObject("Error: Invalid operation");

            List<DecisionDTO> resultDTO = new List<DecisionDTO>();

            var scenario = scenarios[req.ScenarioName][req.ScenarioInstance];
            var rpc = scenario.Characters.Where(r => r.CharacterName.ToString().EqualsIgnoreCase(req.CharacterName)).FirstOrDefault();
            var decisions = rpc?.Decide();
            if (decisions.Any())
            {
                foreach (var d in decisions)
                {
                    string utterance = null;
                    if (string.Equals(d.Key.ToString(), IATConsts.DIALOG_ACTION_KEY, StringComparison.OrdinalIgnoreCase))
                    {
                        try
                        {
                            utterance = scenario.GetDialogAction(d, out utterance).Utterance;
                        }
                        catch (Exception)
                        {
                            return JsonConvert.SerializeObject(string.Format(APIErrors.ERROR_UNKOWN_SPEAK_ACTION, d));
                        }
                        utterance = rpc.ProcessWithBeliefs(utterance);
                    }
                    resultDTO.Add(new DecisionDTO { Action = d.Name.ToString(), Target = d.Target.ToString(), Utterance = utterance, Utility = d.Utility });
                }
            }
            return JsonConvert.SerializeObject(resultDTO);
        }


        private static string HandleCharactersRequest(APIRequest req, ConcurrentDictionary<string, IntegratedAuthoringToolAsset[]> scenarios)
        {
            if (req.Method != HTTPMethod.GET)
                return JsonConvert.SerializeObject("Error: Invalid operation");


            var result = new List<CharacterDTO>();
            var scenario = scenarios[req.ScenarioName][req.ScenarioInstance];
            foreach (var rpc in scenario.Characters)
            {
                result.Add(new CharacterDTO { Name = rpc.CharacterName.ToString(), Emotions = rpc.GetAllActiveEmotions(), Mood = rpc.Mood });
            }
            return JsonConvert.SerializeObject(result);
        }


        private static string HandleActionsRequest(APIRequest req, ConcurrentDictionary<string, IntegratedAuthoringToolAsset[]> scenarios)
        {
            if (req.Method != HTTPMethod.POST)
                return JsonConvert.SerializeObject("Error: Invalid operation");

            IEnumerable<EffectDTO> eventEffects = null;

            var scenario = scenarios[req.ScenarioName][req.ScenarioInstance];

            if (!string.IsNullOrEmpty(req.RequestBody))
            {
                Name ev = null;
                try
                {
                    var requests = JsonConvert.DeserializeObject<ExecuteRequestDTO[]>(req.RequestBody);
                    foreach (var a in requests)
                    {
                        ev = EventHelper.ActionEnd(a.Subject, a.Action, a.Target);
                        foreach (var rpc in scenario.Characters)
                        {
                            rpc.Perceive(ev);
                        }
                        if (scenario.WorldModel != null)
                        {
                            eventEffects = scenario.WorldModel.Simulate(new[] { ev });
                            foreach (var eff in eventEffects)
                            {
                                if (eff.ObserverAgent == WellFormedNames.Name.UNIVERSAL_SYMBOL)
                                {
                                    foreach (var rpc in scenario.Characters)
                                    {
                                        rpc.Perceive(EventHelper.PropertyChange(eff.PropertyName, eff.NewValue, (Name)a.Subject));
                                    }
                                }
                                else
                                {
                                    var obs = scenario.Characters.Where(r => r.CharacterName == eff.ObserverAgent).FirstOrDefault();
                                    {
                                        obs?.Perceive(EventHelper.PropertyChange(eff.PropertyName, eff.NewValue, (Name)a.Subject));
                                    }
                                }
                            }
                        }
                    }
                    return JsonConvert.SerializeObject((string.Format("{0} actions(s) executed and {1} effects triggered", requests.Count(), eventEffects?.Count())));
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(String.Format(APIErrors.ERROR_EXCEPTION_PERCEIVE, ev, ex.Message));
                }
            }
            else
            {
                return JsonConvert.SerializeObject(APIErrors.ERROR_EMPTY_ACTION_REQUEST_LIST);
            }
        }



        private static string HandleMemoriesRequest(APIRequest req, ConcurrentDictionary<string, IntegratedAuthoringToolAsset[]> scenarios)
        {
            if (req.Method != HTTPMethod.GET)
                return JsonConvert.SerializeObject("Error: Invalid operation");

            var scenario = scenarios[req.ScenarioName][req.ScenarioInstance];
            try
            {
                var rpc = scenario.Characters.Where(r => r.CharacterName.ToString().EqualsIgnoreCase(req.CharacterName)).FirstOrDefault();
                var result = rpc.EventRecords;
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(APIErrors.ERROR_EXCEPTION_ASK);
            }
        }

        private static string HandleBeliefsRequest(APIRequest req, ConcurrentDictionary<string, IntegratedAuthoringToolAsset[]> scenarios)
        {
            if (req.Method != HTTPMethod.GET)
                return JsonConvert.SerializeObject("Error: Invalid operation");

            try
            {
                var scenario = scenarios[req.ScenarioName][req.ScenarioInstance];
                var rpc = scenario.Characters.Where(r => r.CharacterName.ToString().EqualsIgnoreCase(req.CharacterName)).FirstOrDefault();
                var beliefResult = rpc.GetAllBeliefs();
                return JsonConvert.SerializeObject(beliefResult);
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(APIErrors.ERROR_EXCEPTION_ASK);
            }
        }


        private static string HandlePerceptionsRequest(APIRequest req, ConcurrentDictionary<string, IntegratedAuthoringToolAsset[]> scenarios)
        {

            if (req.Method != HTTPMethod.POST)
                return JsonConvert.SerializeObject("Error: Invalid operation");

            string[] events = Array.Empty<string>();


            if (!string.IsNullOrEmpty(req.RequestBody))
            {
                var scenario = scenarios[req.ScenarioName][req.ScenarioInstance];
                events = JsonConvert.DeserializeObject<string[]>(req.RequestBody);
                foreach (var ev in events)
                {
                    Name evName = null;
                    try
                    {
                        evName = WellFormedNames.Name.BuildName(ev);
                        var rpc = scenario.Characters.Where(r => r.CharacterName.ToString().EqualsIgnoreCase(req.CharacterName)).FirstOrDefault();
                        rpc.Perceive(evName);
                    }
                    catch (Exception ex)
                    {
                        return JsonConvert.SerializeObject(string.Format(APIErrors.ERROR_EXCEPTION_PERCEIVE, ev, ex.Message));
                    }
                }
            }
            else
            {
                return JsonConvert.SerializeObject(APIErrors.ERROR_EMPTY_EVENT_LIST);
            }
            return JsonConvert.SerializeObject(string.Format("{0} event(s) perceived by {1}", events.Count(), req.CharacterName));
        }



        private static string HandleTickRequest(APIRequest req, ConcurrentDictionary<string, IntegratedAuthoringToolAsset[]> scenarios)
        {
            var scenario = scenarios[req.ScenarioName][req.ScenarioInstance];

            if (req.Method == HTTPMethod.GET)
            {
                return JsonConvert.SerializeObject(scenario.Characters.First().Tick);
            }

            if (req.Method == HTTPMethod.POST)
            {
                int ticks = 0;
                if (string.IsNullOrEmpty(req.RequestBody))
                {
                    foreach (var rpc in scenario.Characters)
                    {
                        rpc.Update();
                    }
                }
                else
                {
                    try
                    {
                        ticks = JsonConvert.DeserializeObject<int>(req.RequestBody);
                        foreach (var rpc in scenario.Characters)
                        {
                            for (int i = 0; i < ticks; i++)
                            {
                                rpc.Update();
                            }
                        }
                    }
                    catch (Exception)
                    {
                        return JsonConvert.SerializeObject(APIErrors.ERROR_EXCEPTION_UPDATE);
                    }
                }
                return JsonConvert.SerializeObject(string.Format("Updated {0} ticks!", ticks));
            }
            return JsonConvert.SerializeObject("Error: Invalid Request");
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WellFormedNames;
using IntegratedAuthoringTool;
using EmotionalDecisionMaking;
using GAIPS.Rage;
using Conditions.DTOs;
using ActionLibrary;
using KnowledgeBase;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using RolePlayCharacter;
using AutobiographicMemory.DTOs;
using IntegratedAuthoringTool.DTOs;

namespace IntegratedAuthoringToolWF.IEP
{
    public partial class ComputeDescriptionForm : Form
    {

        private static readonly HttpClient client = new HttpClient();
        private IntegratedAuthoringToolAsset _iat;
        private AssetStorage _storage;
        public IntegratedAuthoringToolAsset _iatAux;
        public AssetStorage _storageAux;
        public EmotionalDecisionMakingAsset edmAux;
        public EmotionalAppraisalAsset eaAux;


        // Default Server IP + PORT
        private static string IP = "146.193.224.192";
        private static string PORT = "8080";
        private static string iepResult = "";



        public ComputeDescriptionForm(IntegratedAuthoringToolAsset iat, AssetStorage storage, string description)
        {
            InitializeComponent();
            _iat = iat;
            _storage = storage;
            _iatAux = iat;
            _storageAux = storage;
            edmAux = EmotionalDecisionMakingAsset.CreateInstance(_storageAux);
            eaAux = EmotionalAppraisalAsset.CreateInstance(_storageAux);
            this.serverIP.Text = IP;
            this.portText.Text = PORT;
            debugLabel.Text = "Waiting for connection";
            if (description != "")
                this.descriptionText.Text = description;
            else this.descriptionText.Text = "Write your story here...";
            panel4.BackgroundImage = null;
            connectToServer.Enabled = true;
            computeStoryButton.Enabled = false;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var description = "";

            if (descriptionText.Text == null)
            {
                description = "John loves flowers. \n John likes water.";
            }
            else
            {
                description = descriptionText.Text;
            }

            debugLabel.Text = "Awaiting response";
            ProcessDescriptionAsync(description).GetAwaiter().GetResult();
           
            if (iepResult != "")
            {
               
                var f = new IEPOutputForm(this, iepResult);
               
                f.ShowDialog();
              
            }

        }

        void PingServer()
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 60;
            IP = IP.Replace(" ", "");
            PORT = PORT.Replace(" ", "");
            try
            {
                PingReply reply = pingSender.Send(IP, timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    //Ping was successful
                    debugLabel.Text = "Server reached";
                }
                else
                {
                    //Ping failed
                    debugLabel.Text = "Server not reached";
                    panel4.BackgroundImage = Properties.Resources.offline;
                }
            }
            catch (Exception ex)
            {
                //MOSTLY HOST NOT FOUND
                MessageBox.Show(ex.Message);
                debugLabel.Text = "Error: Host not found";
            }
        }

        async Task Handshake()
        {
            // try ping server

            if (debugLabel.Text == "Server not reached")
                return;

            debugLabel.Text = "Searching for Instance...";
            panel4.BackgroundImage = null;
            client.DefaultRequestHeaders.Add("User-Agent", "Anything");

            try
            {
                var r = await SendDescriptionAsync("Hello");

                if(r.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    debugLabel.Text = "Ready to Compute";
                    panel4.BackgroundImage = Properties.Resources.green;
                    descriptionText.ForeColor = SystemColors.WindowText;
                    connectToServer.Enabled = false;
                    computeStoryButton.Enabled = true;

                } else
                {
                    debugLabel.Text = "Code: " + r.StatusCode;
                    panel4.BackgroundImage = Properties.Resources.red;
                }
                //Collect the results
            
            }

            catch (Exception f)
            {
                // Discard PingExceptions and return false;

                MessageBox.Show(f.Message);
                debugLabel.Text = "Error: Instance not found";
                panel4.BackgroundImage = Properties.Resources.offline;
            }
        }


        async Task ProcessDescriptionAsync(string _description)
        {
            client.DefaultRequestHeaders.UserAgent.Clear();
            client.DefaultRequestHeaders.UserAgent.TryParseAdd(Environment.MachineName);

            try
            {

                //Send the Story
                await SendDescriptionAsync(_description);


                //Collect the results
                await GetScenarioAsync().ConfigureAwait(false);

            }
            catch (Exception f)
            {
                // Discard PingExceptions and return false;

                MessageBox.Show(f.Message);


            }

        }

        async Task GetScenarioAsync()
        {
            IP = IP.Replace(" ", "");
            PORT = PORT.Replace(" ", "");
            var responseBytes = client.GetByteArrayAsync("http://" + IP + ":" + PORT).Result;

            iepResult = Encoding.Default.GetString(responseBytes);


        }

        async Task<HttpResponseMessage> SendDescriptionAsync(string _description)
        {
            // Testing purposes


            var content = new StringContent(_description);

            //var response = await client.PostAsync("http://" + IP + ":" + PORT, content).ConfigureAwait(false);

            return Task.Run(() => client.PostAsync("http://" + IP + ":" + PORT, content)).Result;
        }

        public void AcceptedOutput(string _iepResult)
        {
            _iat = _iatAux;
            edmAux.Save();
            eaAux.Save();
            _storage = _storageAux;
            this.Close();
        }

        public void ComputeOutput(string _iepResult)
        {
            ComputeStory(_iepResult);
            edmAux.Save();
            eaAux.Save();
        }

        public void RejectedOutput()
        {
            debugLabel.Text = "Let's try again";
        }


        void ComputeStory(string extrapolations)
        {
            char[] stop = new char[] { '{', '}' };

            var split = extrapolations.Split(stop, StringSplitOptions.RemoveEmptyEntries);
            split = split.Where(val => val != "\n").ToArray();

            foreach(var s in split)
            {
                var component = s.Split(':');
                if(component[0].Contains("Agent"))
                    try
                    {
                        ComputeAgents(s);

                    }
                    catch (Exception f)
                    {
                        // Discard PingExceptions and return false;

                        MessageBox.Show(f.Message);
                        debugLabel.Text = "Error";
                    }
                else if (component[0].Contains("Action"))
                    try
                    {
                        ComputeActions(s);

                    }
                    catch (Exception f)
                    {
                        // Discard PingExceptions and return false;

                        MessageBox.Show(f.Message);
                        debugLabel.Text = "Error";
                    }
                else if (component[0].Contains("Appraisal Rule"))
                    try
                    {
                        ComputeEmotions(s);

                    }
                    catch (Exception f)
                    {
                        // Discard PingExceptions and return false;

                        MessageBox.Show(f.Message);
                        debugLabel.Text = "Error";
                    }
                else if (component[0].Contains("Concept"))
                    try
                    {
                        ComputeConcepts(s);

                    }
                    catch (Exception f)
                    {
                        // Discard PingExceptions and return false;

                        MessageBox.Show(f.Message);
                        debugLabel.Text = "Error";
                    }
                else if (component[0].Contains("Dialogue"))
                    try
                    {
                        ComputeDialogues(s);

                    }
                    catch (Exception f)
                    {
                        // Discard PingExceptions and return false;

                        MessageBox.Show(f.Message);
                        debugLabel.Text = "Error";
                    }
            }


            //debugLabel.Text = "Processed Output";

        }

        void ComputeAgents(string agents)
        {
            var Agents = agents.Split(new string[] { "Agent:" }, StringSplitOptions.RemoveEmptyEntries);
            var agentNames = Agents.Select(item => item.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)[0]);

            for (var i = 0; i < agentNames.Count(); i++)
            {
                var agent = agentNames.ElementAt(i);
                var data = Agents.ElementAt(i);
                if (agent.Length <= 2 || data.Length <= 2)
                    continue;

                var eventIndex = 0;

                data = data.Replace(agent, "");
                data = data.Replace("\t", "");
                var parameterSplit = data.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);


                var beliefs = "";
                var goals = "";
                var status = "";
                var emotions = "";

                foreach (var p in parameterSplit)
                {
                    if (p.Contains("Beliefs: "))
                        beliefs = p.Replace("Beliefs: ", "");
                    else if (p.Contains("Goals: "))
                        goals = p.Replace("Goals: ", "");
                    else if (p.Contains("Status: "))
                        status = p.Replace("Status: ", "");
                    else if (p.Contains("Emotions: "))
                        emotions = p.Replace("Emotions: ", "");
                }


                if (_iatAux.Characters.Count() == 0)
                    _iatAux.AddNewCharacter((Name)agent);

                else if (_iatAux.Characters.ToList().FindIndex(x => agent.Contains(x.CharacterName.ToString())) < 0)
                    _iatAux.AddNewCharacter((Name)agent);

                if (beliefs != "")
                {
                    var beliefSplit = beliefs.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

                    // Computing beliefs: loves(banana[Object])

                    foreach (var b in beliefSplit)
                    {
                        if (b.Length < 3)
                            continue;


                        var beliefName = b;
                        beliefName = beliefName.Replace(" ", "");
                        beliefName = beliefName.Replace("]", "");
                        beliefName = beliefName.Replace(")", "");

                        // Computing beliefs: loves(banana[Object

                        var beliefSplitAux = beliefName.Split('(', '[', '=');

                        var bNmae = beliefSplitAux[0];
                        var bValue = beliefSplitAux[1];

                        var rpc = _iatAux.Characters.First(x => x.CharacterName == (Name)agent);

                        if (b.Contains('['))
                        {
                            var bTarget = beliefSplitAux[2];

                            rpc.UpdateBelief("Is" + "(" + bValue + ")", bTarget);
                        }


                        rpc.UpdateBelief(bNmae + "(" + bValue + ")", "True");

                    }
                }
                if (status != "")
                {
                    var statusSplit = status.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

                    // Computing beliefs: loves(banana[Object])

                    foreach (var s in statusSplit)
                    {
                        if (s.Length < 3)
                            continue;


                        var statusName = s;
                        statusName = statusName.Replace(" ", "");
                        statusName = statusName.Replace("]", "");
                        statusName = statusName.Replace(")", "");

                        // Computing beliefs: loves(banana[Object

                        var beliefSplitAux = statusName.Split('(', '[', '=');

                        var bNmae = beliefSplitAux[0];
                        var bValue = beliefSplitAux[1];

                        var rpc = _iatAux.Characters.First(x => x.CharacterName == (Name)agent);

                        if (s.Contains('['))
                        {
                            var bTarget = beliefSplitAux[2];

                            rpc.UpdateBelief("Is" + "(" + bValue + ")", bTarget);
                        }


                        rpc.UpdateBelief(bNmae + "(" + bValue + ")", "True");

                    }
                }

                if (goals != "")
                {
                    var goalSplit = goals.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var need in goalSplit)
                    {
                        if (need.Length < 3)
                            continue;

                        var actuaNeed = need.Replace(" ", "");

                        var rpc = _iatAux.Characters.First(x => x.CharacterName == (Name)agent);

                        rpc.AddOrUpdateGoal(new EmotionalAppraisal.DTOs.GoalDTO()
                        {
                            Name = actuaNeed,
                            Likelihood = 0.5f,
                            Significance = 1.0f
                        });
                    }
                }

                if (emotions != "")
                {
                    var directedEmotions = emotions.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var emot in directedEmotions)
                    {
                        if (emot.Length < 3)
                            continue;

                        var rpc = _iatAux.Characters.First(x => x.CharacterName == (Name)agent);

                        var stop = new char[] { '(', ')', '=', '|' };

                        var emotionSplit = emot.Split(stop, StringSplitOptions.RemoveEmptyEntries);

                        var emotion = emotionSplit[0].Replace(" ", "");

                        var upperEmotion = emotion.ToUpper();

                        emotion = upperEmotion[0] + emotion.Substring(1);

                        var target = emotionSplit[1].Replace(" ", ""); ;
                        var value = emotionSplit[2].Replace(" ", ""); ;

                        var type = EmotionalAppraisal.OCCModel.OCCEmotionType.Parse(emotion);

                        if (type == null)
                            continue;

                        var newevent = new ActionEventDTO()
                        {
                            ActionState = ActionState.Finished,
                            Subject = target,
                            Action = "action",
                            Target = target,
                            Id = (uint)eventIndex,
                            Time = rpc.Tick,
                            Event = EventHelper.ActionEnd(target, "action", target).ToString()
                        };


                        if (rpc.EventRecords.FirstOrDefault(x => x == newevent) == null)
                            rpc.AddEventRecord(newevent);
                        rpc.Update();
                        
                        var newEmotion = new EmotionDTO()
                        {
                            Target = target,
                            Intensity = float.Parse(value),
                            Type = type.Name,
                            CauseEventName = newevent.Event,
                            CauseEventId = newevent.Id,

                        };

                        if (!rpc.GetAllActiveEmotions().Contains(newEmotion))
                            rpc.AddActiveEmotion(newEmotion);

                        eventIndex += 1;
                    }

                }

            }
        }

        void ComputeActions(string actions)
        {
            var Actions = actions.Split(new string[] { "Action:" }, StringSplitOptions.RemoveEmptyEntries);

            var actionNames = Actions.Select(item => item.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)[0]);


            for (var i = 0; i < actionNames.Count(); i++)
            {
                var action = actionNames.ElementAt(i);
                var data = Actions.ElementAt(i);

                if (action.Length <= 2 || data.Length <= 2 || action.Contains(" be "))
                    continue;

                data = data.Replace(action, "");
                data = data.Replace("\t", "");
                var parameterSplit = data.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);


                var target = "";
                var targetCategory = "";
                var location = "";

                foreach (var p in parameterSplit)
                {
                    if (p.Contains("Target: "))
                        target = p.Replace("Target: ", "");
                    else if (p.Contains("TargetType: "))
                        targetCategory = p.Replace("TargetType: ", "");
                    else if (p.Contains("Location: "))
                        location = p.Replace("Location: ", "");
                }

                // Dealing with the conditions

                ConditionSetDTO Conditions = new ConditionSetDTO();

                if(!targetCategory.Contains("AGENT"))
                    Conditions.ConditionSet = new string[]
                   {
                          "Is([t]," + targetCategory + ")=True"
                   };
                else
                {
                    Conditions.ConditionSet = new string[]
                   {
                          "IsAgent([t])=True",
                          "[t] != SELF"
                   };
                }


                if (location != "" && !location.Contains("default"))
                   
                {
                    Conditions.ConditionSet = Conditions.ConditionSet.Append("At(SELF," + location + ")=True").ToArray();
                }


                    var actionRule = new ActionRule(new ActionLibrary.DTOs.ActionRuleDTO()
                    {
                        Action = (Name)action,
                        Target = (Name)"[t]",
                        Conditions = Conditions,
                        Priority = WellFormedNames.Name.BuildName(1)
                    });

                    edmAux.AddActionRule(actionRule.ToDTO());
                
                

            }
            
        }

        void ComputeConcepts(string concepts)
        {
            var Concepts = concepts.Split(new string[] { "Concept:" }, StringSplitOptions.RemoveEmptyEntries);

            var conceptNames = Concepts.Select(item => item.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)[0]);

            List<Belief> beliefList = new List<Belief>();

            for (var i = 0; i < Concepts.Count(); i++)
            {
                var data = conceptNames.ElementAt(i);
                if (data.Length <= 2)
                    continue;

                var dataSplit = data.Split('=');
                if (dataSplit.Length < 2)
                    continue;

                var concept = dataSplit[0];
                var category = dataSplit[1];

                var b = new Belief()
                {
                    Certainty = 1.0f,
                    Name = WellFormedNames.Name.BuildName("Is(" + concept + "," + category + ")"),
                    Value = (Name)"True",
                    Perspective = WellFormedNames.Name.SELF_SYMBOL
                };

                beliefList.Add(b);
            }

            foreach (var r in this._iatAux.Characters)
            {
                r.m_kb.Tell(beliefList);
            }
               

        }

        void ComputeEmotions(string emotionRules)
        {
            var EmotionRules = emotionRules.Split(new string[] { "Appraisal Rule:" }, StringSplitOptions.RemoveEmptyEntries);

            var emotionRuleNames = EmotionRules.Select(item => item.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)[0]);

            for (var i = 0; i < emotionRuleNames.Count(); i++)
            {
                var emotion = emotionRuleNames.ElementAt(i);
                var data = EmotionRules.ElementAt(i);

                if (emotion.Length <= 2 || data.Length <= 2)
                    continue;

                data = data.Replace(emotion, "");
                data = data.Replace("\t", "");
                var parameterSplit = data.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);


                var initiator = WellFormedNames.Name.SELF_STRING;
                var action = WellFormedNames.Name.UNIVERSAL_STRING;
                var target = WellFormedNames.Name.SELF_STRING;
                var appVariables = new string[] { "Desirability" };
                var appValues = new string[] {"1"};


                foreach (var p in parameterSplit)
                {
                    if (p.Contains("Subject: "))
                        initiator = p.Replace("Subject: ", "");
                    else if (p.Contains("Action: "))
                        action = p.Replace("Action: ", "");
                    else if (p.Contains("Target: "))
                        target = p.Replace("Target: ", "");
                    else if (p.Contains("AppraisalVariables: "))
                        appVariables = p.Replace("AppraisalVariables: ", "").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    else if (p.Contains("AppraisalVariableValues: "))
                        appValues = p.Replace("AppraisalVariableValues: ", "").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                }

                // Creating an appraisal variable
                List<AppraisalVariableDTO> appVariableList = new List<AppraisalVariableDTO>();

                int index = 0;
                foreach (var app in appVariables)
                {
                    AppraisalVariableDTO appraisalVariable = new AppraisalVariableDTO()
                    {
                        Name = app,
                        Value = (Name)appValues[index]
                    };

                    appVariableList.Add(appraisalVariable);
                    index++;
                }

                AppraisalRuleDTO rule = new AppraisalRuleDTO()
                {
                    AppraisalVariables = new AppraisalVariables(appVariableList),
                    EventMatchingTemplate = EventHelper.ActionEnd(initiator, action, target),
                    Conditions = new ConditionSetDTO()

                };

                eaAux.AddOrUpdateAppraisalRule(rule);

            };
        }

        void ComputeDialogues(string dialogues)
        {
            var Dialogues = dialogues.Split(new string[] { "Dialogue:" }, StringSplitOptions.RemoveEmptyEntries);

            var dialogInfo = Dialogues.Select(item => item.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)[0]);

            List<DialogueStateActionDTO> dialogStates = new List<DialogueStateActionDTO>();
            var dialogueIndex = 0;
            

            foreach(var d in dialogInfo)
            {
                var newDA = new DialogueStateActionDTO
                {
                    CurrentState = "S" + dialogueIndex,
                    NextState = "S" + (dialogueIndex + 1),
                    Meaning = "-",
                    Style = "-",
                    Utterance = d
                };

                dialogStates.Add(newDA);
                _iatAux.AddDialogAction(newDA);
                dialogueIndex += 1;
            }

            // Creating a default speak action

            ConditionSetDTO Conditions = new ConditionSetDTO();

            Conditions.ConditionSet = new string[]
               {
                          "IsAgent([t]) = True",
                          "[t] != SELF",
                          "ValidDialogue([currentState], [nextState], [meaning], [style]) = True"
               };

            var actionRule = new ActionRule(new ActionLibrary.DTOs.ActionRuleDTO()
            {
                Action = (Name)"Speak([currentState], [nextState], [meaning], [style])",
                Target = (Name)"[t]",
                Conditions = Conditions,
                Priority = WellFormedNames.Name.BuildName(1)
                
            });

            edmAux.AddActionRule(actionRule.ToDTO());




        }
        private void debugLabel_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void computeDescriptionForm_Load(object sender, EventArgs e)
        {

        }

        private void connectToServer_Click(object sender, EventArgs e)
        {
            IP = serverIP.Text;
            PORT = portText.Text;
            PingServer();
            Handshake().GetAwaiter().GetResult();

            
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void descriptionTextFocus(object sender, EventArgs e)
        {
           // descriptionText.ForeColor = SystemColors.WindowText;
               // descriptionText.Text = "";
        }


        private void descriptionText_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://fatima-toolkit.eu/using-the-compute-story-tool/");
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
        

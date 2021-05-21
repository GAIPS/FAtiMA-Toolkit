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

namespace IntegratedAuthoringToolWF.IEP
{
    public partial class computeDescriptionForm : Form
    {

        private static readonly HttpClient client = new HttpClient();
        private IntegratedAuthoringToolAsset _iat;
        private AssetStorage _storage;
        // Server IP
        private static readonly string IP = "146.193.226.21";

        // Local Host IP
        // private static readonly string IP = "192.168.1.101";
        private static readonly string PORT = "8080";
        private static string iepResult = "";


        public computeDescriptionForm(IntegratedAuthoringToolAsset iat, AssetStorage storage)
        {
            InitializeComponent();
            _iat = iat;
            _storage = storage;

            Ping pinger = null;
            pinger = new Ping();
            PingReply reply = pinger.Send(IP);

            if (reply.Status == IPStatus.Success)
                debugLabel.Text = "Server Reached";
            else
            {
                debugLabel.Text = "Server could not be reached";
                return;
            }

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



            ProcessDescriptionAsync(description).GetAwaiter().GetResult();
            debugLabel.Text = "Description sent, awaiting response";
            if (iepResult != "")
            {
                ComputeStory(iepResult);
                debugLabel.Text = "Process Concluded";
            }

            /*var description = "";

          // This will work as the default but I don't really like, change it in the future
            */
        }



        async Task ProcessDescriptionAsync(string _description)
        {
            client.DefaultRequestHeaders.Add("User-Agent", "Anything");

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
            var responseBytes = client.GetByteArrayAsync("http://" + IP + ":" + PORT).Result;

            iepResult = Encoding.Default.GetString(responseBytes);


        }

        async Task<HttpResponseMessage> SendDescriptionAsync(string _description)
        {
            // Testing purposes


            var content = new StringContent(_description);

            //var response = await client.PostAsync("http://" + IP + ":" + PORT, content).ConfigureAwait(false);

            return Task.Run(() => client.PostAsync("http://" + IP + ":" + PORT, content)).Result;

            /*          

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return;
            }

            else if( response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                MessageBox.Show("No description available");

            }

            //response.EnsureSuccessStatusCode();
            //return response.Headers.Location;*/
        }



        void ComputeStory(string extrapolations)
        {
            char[] stop = new char[] { '{', '}' };

            var split = extrapolations.Split(stop);

            var DomainKnowledge = split[1];

            var Agents = split[2].Split('\n');


            foreach (var a in Agents)
            {

                if (a.Length < 2)
                {
                    continue;
                }

                var parameterSplit = a.Split('%');

                var name = parameterSplit[0];
                var actions = parameterSplit[1];
                var beliefs = parameterSplit[2];
                var needs = parameterSplit[3];


                if (_iat.Characters.Count() == 0)
                    _iat.AddNewCharacter((Name)name);

                else if (_iat.Characters.ToList().FindIndex(x => name.Contains(x.CharacterName.ToString())) < 0)
                    _iat.AddNewCharacter((Name)name);


                var beliefSplit = beliefs.Split('|');

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

                    var beliefSplitAux = beliefName.Split('(', '[');

                    var bNmae = beliefSplitAux[0];
                    var bValue = beliefSplitAux[1];

                    var rpc = _iat.Characters.First(x => x.CharacterName == (Name)name);

                    if (b.Contains('['))
                    {
                        var bTarget = beliefSplitAux[2];

                        rpc.UpdateBelief("Is" + "(" + bValue + ")", bTarget);
                    }


                    rpc.UpdateBelief(bNmae + "(" + bValue + ")", "True");



                }

                var actionSplit = actions.Split('|');

                foreach (var act in actionSplit)
                {
                    if (act.Length < 3)
                        continue;

                    // go(park[Location])

                    // we want a belief and an action: is(Park) = Location and go(park)


                    var actName = "";
                    var targetName = "";
                    var targetValue = "";



                    var actionName = act;
                    actionName = actionName.Replace(" ", "");
                    actionName = actionName.Replace("]", "");
                    actionName = actionName.Replace(")", "");

                    // go(park[location

                    actionSplit = actionName.Split('(', '[');

                    actName = actionSplit[0];
                    targetName = actionSplit[1];

                    if (act.Contains('['))
                    {
                        targetValue = actionSplit[2];

                        var rpc = _iat.Characters.First(x => x.CharacterName == (Name)name);

                        rpc.UpdateBelief("Is" + "(" + targetName + ")", targetValue);
                    }


                 /*   if (_edmForm == null)
                        _edmForm = new EmotionalDecisionMakingWF.MainForm();
                    var edm = _edmForm.Asset;



                    edm.AddActionRule(new ActionLibrary.DTOs.ActionRuleDTO()
                    {
                        Action = (Name)actName,
                        Target = (Name)targetName
                    });

                    _edmForm.OnAssetDataLoaded();
                 */




                }

                var needSplit = needs.Split('|');

                foreach (var ned in needSplit)
                {
                    if (ned.Length < 3)
                        continue;

                    // go(park[Location])

                    // we want a belief and an action: is(Park) = Location and go(park)


                    var nedName = "";
                    var targetName = "";
                    var targetValue = "";



                    var needName = ned;
                    needName = needName.Replace(" ", "");
                    needName = needName.Replace("]", "");
                    needName = needName.Replace(")", "");

                    // go(park[location

                    var needAuxSplit = needName.Split('(', '[');

                    nedName = needAuxSplit[0];
                    targetName = needAuxSplit[1];

                    var rpc = _iat.Characters.First(x => x.CharacterName == (Name)name);

                    if (ned.Contains('['))
                    {
                        targetValue = actionSplit[2];



                        rpc.UpdateBelief("Is" + "(" + targetName + ")", targetValue);
                    }

                    rpc.AddOrUpdateGoal(new EmotionalAppraisal.DTOs.GoalDTO()
                    {
                        Name = targetName,
                        Likelihood = 0.5f,
                        Significance = 1.0f
                    });



                }
            }
        }

        private void debugLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
        

﻿using EmotionalAppraisalWF;
using IntegratedAuthoringTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntegratedAuthoringToolWF
{
    static class AuthorAssistant
    {
        static Dictionary<string, List<string>> tips = new Dictionary<string, List<string>>()
        {
            {
                "Default", new List<string>()
                {
                     "Save both the scenario and cognitive rules file using Ctrl+S.",
                    "Check fatima-toolkit.eu website for more information. Click the Help option to go there",
                    "Cognitive Rules are applied to every agent. Use conditions to make sure they are executed by the correct character.",
                    "An agent perceives events in the environment which represent changes in the world around an agent.",
                     "The beliefs of the agent represent their internal representation of the world and their needs. For example: \n Is(Banana,Fruit)=True | DialogueState(John)=Start ."

                }
            },
            {
                "Emotional Appraisal", new List<string>()
                {
                     "FAtiMA is based on the Ortony, Clore and Collins's (OCC) Model of emotions.",
                    "Emotions represented valenced reactions to events in the world. They are generated by an appraisal process."
                }
            },

            {
                "Emotional Decision Making", new List<string>()
                {
                     "Define the actions of the agents through decision making rules",
                      "Actions have a name, a priority, a layer and a target",
                      "The Layer parameter is a symbol that indicates in which layer is the action considered in the decision-making process.",
                      "The Target of the action is the name of the agent or object that the action is aimed at.",
                       "The Conditions of an action rule is a list of logical conditions used to check if the action can be executed",
                       "The Priority is a numerical value that represents how important it is for the agent to execute the action."

                }
            }


        };


        
        
        public static string GetTipByKey(int index)
        {
            var actualKey = tips.Keys.ToList()[index];

            int rand = new Random().Next(0, tips[actualKey].Count);

            return tips[actualKey][rand];
            
        }
        public static string GetTipByKey(string key)
        {
            int rand = 0;
            if (tips.ContainsKey(key))
            {
                rand = new Random().Next(0, tips[key].Count);
               
            }

            else
            {
                rand = new Random().Next(0, tips["Default"].Count);
            }
            return tips[key][rand];
        }

        public static string GetRandomTip()
        {
            var allTips = tips.SelectMany(x => x.Value).ToList();
            int rand = new Random().Next(0, tips.Count);
            return allTips[rand];
        }

  

    }
}

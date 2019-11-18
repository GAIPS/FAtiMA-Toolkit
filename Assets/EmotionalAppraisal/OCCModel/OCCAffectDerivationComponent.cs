using System;
using System.Collections.Generic;
using System.Linq;
using AutobiographicMemory;
using EmotionalAppraisal.Components;
using EmotionalAppraisal.DTOs;
using WellFormedNames;

namespace EmotionalAppraisal.OCCModel
{
    public class OCCAffectDerivationComponent : IAffectDerivator
    {
        private static OCCBaseEmotion OCCAppraiseCompoundEmotions(IBaseEvent evt, float desirability, float praiseworthiness)
        {
            if ((desirability == 0) || (praiseworthiness == 0) || ((desirability > 0) != (praiseworthiness > 0)))
                return null;

            float potential = Math.Abs(desirability + praiseworthiness) * 0.5f;

            Name direction;
            OCCEmotionType emoType;

            if (evt.Subject == Name.SELF_SYMBOL)
            {
                direction = Name.SELF_SYMBOL;
                emoType = (desirability > 0) ? OCCEmotionType.Gratification : OCCEmotionType.Remorse;
            }
            else
            {
                direction = evt.Subject ?? Name.UNIVERSAL_SYMBOL;
                emoType = (desirability > 0) ? OCCEmotionType.Gratitude : OCCEmotionType.Anger;
            }

            return new OCCBaseEmotion(emoType, potential, evt.Id, direction, evt.EventName);
        }

        private static OCCBaseEmotion OCCAppraiseWellBeing(uint evtId, Name eventName, float desirability)
        {


            if (desirability >= 0)
                return new OCCBaseEmotion(OCCEmotionType.Joy, desirability, evtId, eventName);
            return new OCCBaseEmotion(OCCEmotionType.Distress, -desirability, evtId, eventName);
        }




        private static OCCBaseEmotion OCCAppraiseFortuneOfOthers(IBaseEvent evt, float desirability, float desirabilityForOther, string target)
        {
            float potential = (Math.Abs(desirabilityForOther) + Math.Abs(desirability)) * 0.5f;

            if (target == "SELF" || target == evt.Subject.ToString())
                return OCCAppraiseWellBeing(evt.Id, evt.EventName, potential);

            OCCEmotionType emoType;
            if (desirability >= 0)
                emoType = (desirabilityForOther >= 0) ? OCCEmotionType.HappyFor : OCCEmotionType.Gloating;
            else
                emoType = (desirabilityForOther >= 0) ? OCCEmotionType.Resentment : OCCEmotionType.Pitty;

            return new OCCBaseEmotion(emoType, potential, evt.Id, (Name)target, evt.EventName);
        }




        private static OCCBaseEmotion OCCAppraisePraiseworthiness(IBaseEvent evt, float praiseworthiness, string target)
        {
            Name direction;
            OCCEmotionType emoType;

            if (target == "SELF" || target == evt.Subject.ToString())
            {
                direction = Name.SELF_SYMBOL;
                emoType = (praiseworthiness >= 0) ? OCCEmotionType.Pride : OCCEmotionType.Shame;
            }
            else
            {
                direction = (Name)target;
                emoType = (praiseworthiness >= 0) ? OCCEmotionType.Admiration : OCCEmotionType.Reproach;
            }

            return new OCCBaseEmotion(emoType, Math.Abs(praiseworthiness), evt.Id, direction, evt.EventName);
        }

        private static OCCBaseEmotion OCCAppraiseAttribution(IBaseEvent evt, float like)
        {
            const float magicFactor = 0.7f;
            OCCEmotionType emoType = (like >= 0) ? OCCEmotionType.Love : OCCEmotionType.Hate;
            return new OCCBaseEmotion(emoType, Math.Abs(like) * magicFactor, evt.Id, evt.Subject == null ? Name.UNIVERSAL_SYMBOL : evt.Subject, evt.EventName);
        }




        private static OCCBaseEmotion AppraiseGoalSuccessProbability(IBaseEvent evt, float goalProbability, float previousProbabilityValue, float significance)
        {


            //Significante is too low
            var potential = significance;

            if (previousProbabilityValue == goalProbability)
                return new OCCBaseEmotion(OCCEmotionType.Hope, 0, evt.Id, evt.EventName);


            if (goalProbability > previousProbabilityValue)
            {

                if (goalProbability == 1)
                {
                    if (previousProbabilityValue <= 0.5)
                        return new OCCBaseEmotion(OCCEmotionType.Relief, Math.Abs(goalProbability) * potential, evt.Id, evt.EventName);

                    else return new OCCBaseEmotion(OCCEmotionType.Satisfaction, Math.Abs(goalProbability) * potential, evt.Id, evt.EventName);
                }
                else return new OCCBaseEmotion(OCCEmotionType.Hope, Math.Abs(goalProbability) * potential, evt.Id, evt.EventName);
            }

            else  //if(goalProbability < goal.Likelihood)
            {
                if (goalProbability == 0)
                    if (previousProbabilityValue >= 0.5)
                        return new OCCBaseEmotion(OCCEmotionType.Disappointment, (1 - goalProbability) * potential, evt.Id, evt.EventName);

                    else return new OCCBaseEmotion(OCCEmotionType.FearsConfirmed, (1 - goalProbability) * potential, evt.Id, evt.EventName);

                else return new OCCBaseEmotion(OCCEmotionType.Fear, (1 - goalProbability) * potential, evt.Id, evt.EventName);
            }
        }

    
        public IEnumerable<IEmotion> AffectDerivation(EmotionalAppraisalAsset emotionalModule, Dictionary<string, Goal> goals, IAppraisalFrame frame)
        {
            var evt = frame.AppraisedEvent;
            bool returnedEmotion = false;

            if (frame.ContainsAppraisalVariable(OCCAppraisalVariables.DESIRABILITY))
            {
                float desirability = frame.GetAppraisalVariable(OCCAppraisalVariables.DESIRABILITY);


                foreach (string variable in frame.AppraisalVariables.Where(v => v.StartsWith(OCCAppraisalVariables.DESIRABILITY_FOR_OTHER)))
                {

                    string other = variable.Substring(OCCAppraisalVariables.DESIRABILITY_FOR_OTHER.Length);
                    float desirabilityForOther = frame.GetAppraisalVariable(variable);
                    if (desirabilityForOther != 0)
                    {
                        returnedEmotion = true;
                        yield return OCCAppraiseFortuneOfOthers(evt, desirability, desirabilityForOther, other);
                    }
                }


                if (frame.ContainsAppraisalVariable(OCCAppraisalVariables.PRAISEWORTHINESS) && !returnedEmotion)
                {

                    float praiseworthiness = frame.GetAppraisalVariable(OCCAppraisalVariables.PRAISEWORTHINESS);

                    var composedEmotion = OCCAppraiseCompoundEmotions(evt, desirability, praiseworthiness);
                    if (composedEmotion != null)
                    {
                        returnedEmotion = true;
                        yield return composedEmotion;
                    }
                }

            }



            if (!returnedEmotion)
                foreach (string variable in frame.AppraisalVariables.Where(v => v.StartsWith(OCCAppraisalVariables.PRAISEWORTHINESS)))
                {
                    float praiseworthiness = frame.GetAppraisalVariable(variable);
                    string other = variable.Substring(OCCAppraisalVariables.PRAISEWORTHINESS.Length);
                    if (other == null || other == " ")
                        yield return OCCAppraisePraiseworthiness(evt, praiseworthiness, "SELF");

                    else yield return OCCAppraisePraiseworthiness(evt, praiseworthiness, other);

                }


            if (frame.ContainsAppraisalVariable(OCCAppraisalVariables.LIKE))
            {
                float like = frame.GetAppraisalVariable(OCCAppraisalVariables.LIKE);
                if (like != 0)
                    yield return OCCAppraiseAttribution(evt, like);
            }

            if (frame.ContainsAppraisalVariable(OCCAppraisalVariables.DESIRABILITY) && !returnedEmotion)
            {
                float desirability = frame.GetAppraisalVariable(OCCAppraisalVariables.DESIRABILITY);
                yield return OCCAppraiseWellBeing(evt.Id, evt.EventName, desirability);
            }


            foreach (string variable in frame.AppraisalVariables.Where(v => v.StartsWith(OCCAppraisalVariables.GOALSUCCESSPROBABILITY)))
            {
                float goalSuccessProbability = frame.GetAppraisalVariable(variable);

                string goalName = variable.Substring(OCCAppraisalVariables.GOALSUCCESSPROBABILITY.Length + 1);

                if (goals == null) continue;
                Goal g = goals[goalName];
                if (g == null) continue;
                
                var previousValue = g.Likelihood;
                g.Likelihood = goalSuccessProbability;


                yield return AppraiseGoalSuccessProbability(evt, goalSuccessProbability, previousValue, g.Significance);
            }
        }

    }
}
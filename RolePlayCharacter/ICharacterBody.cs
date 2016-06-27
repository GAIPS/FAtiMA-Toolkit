namespace RolePlayCharacter
{
    public interface ICharacterBody
    {
        void SetExpression(string emotion, float amount, string name);
        void SetMoodExpression(string moodEmotion, float mood, string name);
        int AmountToLevel(float amount);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RolePlayCharacter
{
    public interface ICharacterBody
    {
        void SetExpression(string emotion, float amount, string name);
        void LoadObject(string name);
        int AmountToLevel(float amount);
    }
}

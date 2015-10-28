using System.Collections.Generic;
using System.Linq;
using KnowledgeBase.WellFormedNames;
using NUnit.Framework;

namespace Tests.KnowledgeBase.WellFormedNames
{
    [TestFixture]
    public class UnifierTests {

        [TestCase("John", "John")]
        [TestCase("Strong(John)", "Strong(John)")]
        [TestCase("John", "[x]", "[x]/John")]
        [TestCase("Strong([x])", "Strong(John)", "[x]/John")]
        [TestCase("Likes([x],[y])", "Likes(John, [z])", "[x]/John", "[y]/[z]")]
        [TestCase("Likes([x],John)", "Likes(John, [x])", "[x]/John")]
        [TestCase("Likes([x],[y])", "Likes(John, Mary)", "[x]/John", "[y]/Mary")]
        [TestCase("S([x],k([x],[z]),j([y],k(t(k,l),y)))", "S(t(k,l),k([x],y),j(P,k([x],[z])))", "[x]/t(k,l)", "[z]/y","[y]/P")]
        public void Unify_UnifiableNames_True(string n1, string n2, params string[] result)
        {
            var name1 = Name.Parse(n1);
            var name2 = Name.Parse(n2);
            var expectedBindings = result.Select(s => new Substitution(s));
            IEnumerable<Substitution> bindings = new List<Substitution>();
            var isUnifiable = Unifier.Unify(name1, name2, out bindings);
            Assert.That(isUnifiable);
            Assert.That(bindings, Is.EquivalentTo(expectedBindings));
        }

    }
}
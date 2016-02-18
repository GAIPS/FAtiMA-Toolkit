using System.Collections.Generic;
using System.Linq;
using KnowledgeBase.WellFormedNames;
using NUnit.Framework;

namespace Tests.KnowledgeBase.WellFormedNames
{
    [TestFixture]
    public class UnifierTests {

        [TestCase("John", "John",new string[0])]
		[TestCase("Strong(John)", "Strong(John)", new string[0])]
		[TestCase("John", "[x]", new[]{"[x]/John"})]
		[TestCase("Strong([x])", "Strong(John)", new[]{"[x]/John"})]
		[TestCase("Likes([x],[y])", "Likes(John, [z])", new[]{"[x]/John", "[y]/[z]"})]
		[TestCase("Likes([x],John)", "Likes(John, [x])", new[]{"[x]/John"})]
		[TestCase("Likes([x],[y])", "Likes(John, Mary)", new[]{"[x]/John", "[y]/Mary"})]
		[TestCase("S([x],k([x],[z]),j([y],k(t(k,l),y)))", "S(t(k,l),k([x],y),j(P,k([x],[z])))", new[]{"[x]/t(k,l)", "[z]/y", "[y]/P"})]
        public void Unify_UnifiableNames_True(string n1, string n2, string[] result)
        {
            var name1 = Name.BuildName(n1);
            var name2 = Name.BuildName(n2);
            var expectedBindings = result.Select(s => new Substitution(s));
	        IEnumerable<Substitution> bindings;
            var isUnifiable = Unifier.Unify(name1, name2, out bindings);
            Assert.That(isUnifiable);
            if (result.Any())
            {
                Assert.That(bindings, Is.EquivalentTo(expectedBindings));
            }
            else
            {
                Assert.That(bindings.Count()==0);
            }
        }

        [TestCase("John", "J")]
		[TestCase("Strong(John)", "John")]
		[TestCase("[x]([x])", "IsPerson(John)")]
		[TestCase("[x](John,Paul)", "Friend(John,[x])")]
		[TestCase("Like([x],[y])", "Like(John,Strong([y]))")]
        public void Unify_NonUnifiableNames_False(string n1, string n2)
        {
            var name1 = Name.BuildName(n1);
            var name2 = Name.BuildName(n2);
            IEnumerable<Substitution> bindings = new List<Substitution>();
            var isUnifiable = Unifier.Unify(name1, name2, out bindings);
            Assert.That(!isUnifiable);
            Assert.That(bindings == null);
        }
        

        [TestCase("x", "x(a)",new string[0])]
		[TestCase("x", "[y](a)", new[]{"[y]/x"})]
		[TestCase("x(a)", "x", new string[0])]
		[TestCase("[y](a)", "x", new[]{"[y]/x"})]
        [TestCase("x(a, b)", "x(a, b, c)",new string[0])]
        public void PartialUnify_PartiallyUnifiableNames_True(string n1, string n2, string[] result)
        {
            var name1 = Name.BuildName(n1);
            var name2 = Name.BuildName(n2);
            var expectedBindings = result.Select(s => new Substitution(s));

            IEnumerable<Substitution> bindings = new List<Substitution>();
            var isPartiallyUnifiable = Unifier.PartialUnify(name1, name2, out bindings);

            Assert.That(isPartiallyUnifiable);
            if (result.Any())
            {
                Assert.That(bindings, Is.EquivalentTo(expectedBindings));
            }
            else
            {
                Assert.That(bindings == null);
            }
        }
    }
}
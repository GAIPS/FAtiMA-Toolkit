namespace Tests.WellFormedNames
{
	/*
    [TestFixture]
    public class SymbolTests
    {
        [TestCase("x")]
        [TestCase("9")]
		[TestCase("10.56")]
		[TestCase("-10.56")]
		[TestCase("-10")]
		[TestCase("+10")]
		[TestCase("10.7654e10")]
		[TestCase("1.7654e-5")]
		[TestCase("true")]
		[TestCase("false")]
		[TestCase("_17654")]
        [TestCase("[x]")]
        [TestCase("-")]
		[TestCase("1e-46")]
        [TestCase("[_x]")]
        [TestCase("[x-93]")]
        [TestCase(Symbol.UNIVERSAL_STRING)]
        [TestCase(Symbol.AGENT_STRING)]
        [TestCase(Symbol.SELF_STRING)]
        public void Symbol_ValidSymbolString_NewSymbol(string s)
        {
            var symbol = new Symbol(s);
            Assert.That(symbol.GetType() == typeof(Symbol));
        }

        [TestCase("@")]
        [TestCase("/")]
        [TestCase("(x)")]
        [TestCase("x)")]
        [TestCase("(x")]
        [TestCase("x, y")]
		[TestCase("1.76.54e-5")]
		[TestCase("1.76.54")]
		[TestCase("1e")]
		[TestCase("_1.76")]
        [TestCase("[x")]
        [TestCase("x]")]
        [TestCase("[]")]
        public void Symbol_InvalidSymbolString_NewSymbol(string s)
        {
            Assert.Throws<ParsingException>(() => new Symbol(s));
        }

        [TestCase("x")]
        [TestCase("9")]
        [TestCase("[x]")]
        [TestCase("-")]
        [TestCase("[_x]")]
        [TestCase("[x-93]")]
        [TestCase(Symbol.UNIVERSAL_STRING)]
        [TestCase(Symbol.SELF_STRING)]
        public void Name_AnySymbol_SymbolName(string s)
        {
            var symbol = new Symbol(s);
            Assert.That(symbol.Name == s);
        }


        [TestCase("[x]")]
        [TestCase("[_x]")]
        [TestCase("[x-93]")]
        [TestCase(Symbol.UNIVERSAL_STRING)]
        [TestCase(Symbol.SELF_STRING)]
        public void IsVariable_SymbolWithAVariable_True(string s)
        {
            var symbol = new Symbol(s);
            Assert.That(symbol.NumberOfTerms == 1);
        }

        [TestCase("x")]
        [TestCase("_x")]
        [TestCase("x-93")]
        [TestCase(Symbol.UNIVERSAL_STRING)]
        [TestCase(Symbol.SELF_STRING)]
        public void IsVariable_SymbolWithNoVariable_False(string s)
        {
            var symbol = new Symbol(s);
            Assert.That(symbol.NumberOfTerms == 1);
        }


        [TestCase("x")]
        public void NumberOfTerms_AnySymbol_SymbolName(string s)
        {
            var symbol = new Symbol(s);
            Assert.That(symbol.NumberOfTerms == 1);
        }

        [TestCase("x")]
        public void GetFirstTerm_AnySymbol_Symbol(string s)
        {
            var symbol = new Symbol(s);
            Assert.That(symbol.GetFirstTerm() == symbol);
        }

        [TestCase("x")]
        public void GetTerms_AnySymbol_Symbol(string s)
        {
            var symbol = new Symbol(s);
            Assert.That(symbol.GetTerms(), Is.EquivalentTo(new List<Symbol> { symbol }));
        }

        [TestCase("[x]")]
        [TestCase("[_x]")]
        [TestCase("[x-93]")]
        public void GetVariableList_SymbolWithAVariable_Symbol(string s)
        {
            var symbol = new Symbol(s);
            Assert.That(symbol.GetVariableList(), Is.EquivalentTo(new List<Symbol> { symbol }));
        }


        [TestCase("x")]
        [TestCase("_x")]
        [TestCase("x-93")]
        [TestCase(Symbol.UNIVERSAL_STRING)]
        [TestCase(Symbol.SELF_STRING)]
        public void GetVariableList_SymbolWithNoVariable_EmptyList(string s)
        {
            var symbol = new Symbol(s);
            Assert.That(!symbol.GetVariableList().Any());
        }

        [TestCase("[x]")]
        [TestCase("_x")]
        [TestCase("[x_93]")]
        public void HasGhostVariable_SymbolWithNoGhostVariable_False(string s)
        {
            var symbol = new Symbol(s);
            Assert.That(!symbol.HasGhostVariable());
        }

        [TestCase("[_]")]
        [TestCase("[_a]")]
        [TestCase("[_1]")]
        public void HasGhostVariable_SymbolGhostVariable_True(string s)
        {
            var symbol = new Symbol(s);
            Assert.That(symbol.HasGhostVariable());
        }


        [TestCase("x", "x", "y")]
        public void SwapPerspective_SymbolWithName_ClonedSwappedSymbol(string s, string original, string newName)
        {
            var symbol = new Symbol(s);
            var clonedSymbol = (Symbol)symbol.SwapTerms(original, newName);
            Assert.That(clonedSymbol.Name == newName);
            Assert.That(!ReferenceEquals(symbol, clonedSymbol));
        }

        [TestCase("x", "y", "y")]
        public void SwapPerspective_SymbolWithoutPerspective_ClonedUnswappedSymbol(string s, string original, string newName)
        {
            var symbol = new Symbol(s);
            var clonedSymbol = (Symbol)symbol.SwapTerms(original, newName);
            Assert.That(clonedSymbol.Name != newName);
            Assert.That(clonedSymbol == symbol);
            Assert.That(!ReferenceEquals(symbol, clonedSymbol));
        }


    }
	*/
}
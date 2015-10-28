using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using KnowledgeBase.WellFormedNames;
using NUnit.Framework;

namespace Tests.KnowledgeBase.WellFormedNames
{
    [TestFixture]
    public class NameSearchTreeTests {

        [Test]
        public void Depth_EmptyNameSearchTree_0()
        {
            var tree = new NameSearchTree<Symbol>();
            Assert.That(tree.Depth == 0);
        }

        [TestCase("x","1")]
        [TestCase("x(a)", "2")]
        [TestCase("x(a, b)", "3")]
        public void Add_EmptyNameSearchTree_True(string name, string value)
        {
            var tree = new NameSearchTree<string>();
            var firstAddSucess = tree.Add(Name.Parse(name), value);
            Assert.That(firstAddSucess);
        }

        [TestCase("x", "1")]
        [TestCase("x(a)", "2")]
        [TestCase("x(a, b)", "3")]
        public void Add_FilledNameSearchTree_False(string name, string value)
        {
            var tree = new NameSearchTree<string>();
            tree.Add(Name.Parse(name), value);
         
            var repeatedAddSuccess = tree.Add(Name.Parse(name), value);
            Assert.That(!repeatedAddSuccess);

            repeatedAddSuccess = tree.Add(Name.Parse(name), String.Empty);
            Assert.That(!repeatedAddSuccess);
        }

        [TestCase("x", "1")]
        [TestCase("x(a)", "2")]
        [TestCase("x(a, b)", "3")]
        public void Remove_EmptyNameSearchTree_False(string name, string value)
        {
            var tree = new NameSearchTree<string>();
            var removeSuccess = tree.Remove(Name.Parse(name));
            Assert.That(!removeSuccess);
        }

        [TestCase("x", "1")]
        [TestCase("x(a)", "2")]
        [TestCase("x(a, b)", "3")]
        public void Remove_NameSearchTreeThatContainsName_True(string name, string value)
        {
            var tree = new NameSearchTree<string>();
            tree.Add(Name.Parse(name), value);
            var removeSuccess = tree.Remove(Name.Parse(name));
            Assert.That(removeSuccess);
        }

        [TestCase("x", "1")]
        [TestCase("x(a)", "2")]
        [TestCase("x(a, b)", "3")]
        public void Contains_NameSearchTreeThatContainsName_True(string name, string value)
        {
            var tree = new NameSearchTree<string>();
            tree.Add(Name.Parse(name), value);
            Assert.That(tree.Contains(Name.Parse(name)));
        }

        [TestCase("x", "1")]
        [TestCase("x(a)", "2")]
        [TestCase("x(a, b)", "3")]
        public void Contains_EmptySearchTree_False(string name, string value)
        {
            var tree = new NameSearchTree<string>();
            Assert.That(!tree.Contains(Name.Parse(name)));
        }

        [TestCase("x", "1")]
        [TestCase("x(a)", "2")]
        [TestCase("x(a, b)", "3")]
        public void TryMatchValue_EmptySearchTree_False(string nameStr, string value)
        {
            var tree = new NameSearchTree<string>();
            var name = Name.Parse(nameStr);
            string res;
            Assert.That(!tree.TryMatchValue(name, out res));
            Assert.That(tree[name] == null);
        }

        [TestCase("x", "1")]
        [TestCase("x(a)", "2")]
        [TestCase("x(a, b)", "3")]
        public void TryMatchValue_SearchTreeThatContainsName_True(string nameStr, string value)
        {
            var tree = new NameSearchTree<string>();
            var name = Name.Parse(nameStr);
            tree.Add(name, value);
            string res;
            Assert.That(tree.TryMatchValue(name, out res));
            Assert.That(res == value);
            Assert.That(tree[name] == value);
        }

    }
}
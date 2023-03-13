using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WellFormedNames;

namespace KnowledgeBase
{
    [Serializable]
    public class CommonSense
    {
        public Dictionary<string, Node> commonSense;

        public class Node
        {
            public Node parent;

            public string value;
            public List<Node> childNodes { get; set; }

        }

        public Node hierarchyTree;

        public CommonSense()
        {
            commonSense = new Dictionary<string, Node>();
        }

        public void LoadDefaultKnowledge()
        {
            // Fill tree according with Wordnet Hypernim Hierarchy

            Node entity = new Node()
            {
                parent = null,
                value = "entity",
                childNodes = new List<Node>()

            };

            hierarchyTree = entity;

            Node obj = new Node()
            {
                value = "object",
                parent = entity,
                childNodes = new List<Node>()
            };

            Node matter = new Node()
            {
                value = "matter",
                parent = entity,
                childNodes = new List<Node>()
            };

            Node organism = new Node()
            {
                value = "organism",
                parent = entity,
                childNodes = new List<Node>()
            };

            Node abstraction = new Node()
            {
                value = "abstraction",
                parent = entity,
                childNodes = new List<Node>()
            };

            entity.childNodes.Add(obj);
            entity.childNodes.Add(organism);
            entity.childNodes.Add(matter);
            entity.childNodes.Add(abstraction);

            // Abstraction

            Node organization = new Node()
            {
                value = "organization",
                parent = abstraction,
                childNodes = new List<Node>()
            };

            Node institution = new Node()
            {
                value = "institution",
                parent = abstraction,
                childNodes = new List<Node>()
            };

            Node activity = new Node()
            {
                value = "activity",
                parent = abstraction,
                childNodes = new List<Node>()
            };

            abstraction.childNodes.Add(organization);
            abstraction.childNodes.Add(institution);
            abstraction.childNodes.Add(activity);

            // Matter
            Node food = new Node()
            {
                value = "food",
                parent = matter,
                childNodes = new List<Node>()
            };

            Node beverage = new Node()
            {
                value = "beverage",
                parent = matter,
                childNodes = new List<Node>()
            };

            matter.childNodes.Add(food);
            matter.childNodes.Add(beverage);

            // Organism

            Node animal = new Node()
            {
                value = "animal",
                parent = organism,
                childNodes = new List<Node>()
            };

            Node plant = new Node()
            {
                value = "plant",
                parent = organism,
                childNodes = new List<Node>()
            };

            Node person = new Node()
            {
                value = "person",
                parent = organism,
                childNodes = new List<Node>()
            };

            organism.childNodes.Add(animal);
            organism.childNodes.Add(plant);
            organism.childNodes.Add(person);

            // Object
            Node artifact = new Node()
            {
                value = "artifact",
                parent = obj,
                childNodes = new List<Node>()
            };

            Node instrument = new Node()
            {
                value = "instrument",
                parent = obj,
                childNodes = new List<Node>()
            };

            obj.childNodes.Add(artifact);
            obj.childNodes.Add(instrument);


           List<Node> nodeList = new List<Node>()
            {
                obj, instrument, artifact, organism, animal, plant, person,
                food, beverage, matter, abstraction, organization, institution, activity,
                entity
            };

            AddToCommonSense(nodeList);
        }

        public void AddToCommonSense(List<Node> nodeList)
        {
            foreach(var node in nodeList)
            {
                if(!commonSense.ContainsKey(node.value))
                    commonSense.Add(node.value, node);
            }
        }

        public List<string> getCategories(Node n)
        {
            List<string> ret = new List<string>();


            while(n.parent != null)
            {
                ret.Add(n.value);
                n = n.parent;
            }

            return ret;
        }

        public List<Belief> CommonSenseReasoner(Belief b)
        {
            List<Belief> ret = new List<Belief>();

            // is(Banana, Fruit) = True, we need to find the Fruit

            // Getting the correct term: fruit

            var terms = b.Name.GetTerms().ToList();

            if (terms.Count() != 3)
            {
                return ret;
            }

            else
            {
                if(terms[0].ToString().ToLower() == "is")
                {

                    var originalTerm = terms[1].ToString();
                    var category = terms[2].ToString();

                    // Checking which possible new categories will arise: food
                    var possibleTerms = new List<string>();
                    var auxCategory = category.ToString().ToLower();
                    if (commonSense.ContainsKey(auxCategory))
                    {
                        var cats = getCategories(commonSense[auxCategory]);
                        foreach(var c in cats)
                        if (!possibleTerms.Contains(c))
                            possibleTerms.Add(c);


                        // let's add a new category then
                        if(!commonSense.ContainsKey(originalTerm))
                        if (possibleTerms.Count > 0)
                        {
                            
                            commonSense.Add(originalTerm, new Node()
                            {
                                childNodes = new List<Node>(),
                                parent = commonSense[auxCategory],
                                value = originalTerm
                            });

                            commonSense[auxCategory].childNodes.Add(commonSense[originalTerm]);

                        }
                      
                    }
                    // Creating a new belief based on the information collected
                    foreach (var t in possibleTerms)
                    {
                        Belief newBelief = b;
                        newBelief.Name = Name.BuildName("is(" + originalTerm + "," + t + ")");
                        ret.Add(newBelief);
                    }


                    return ret;

                }

                else return ret;
            }
        

            


            
       
        }

     

    }
}

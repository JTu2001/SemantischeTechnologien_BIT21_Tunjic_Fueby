namespace SparqlExample
{
    using System;
    using VDS.RDF;
    using VDS.RDF.Parsing;
    using VDS.RDF.Query;

    public static class Program
    {
        public static void Main(string[] args)
        {
            // Erstellen eines Graphen
            IGraph graph = new Graph();

            try
            {
                // Laden der Daten
                FileLoader.Load(graph, "chemicalElements.xml");
            }
            catch 
            {
                throw new IOException("An error occurred while loading the data!");
            }

            string query = @"PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
                             PREFIX owl: <http://www.w3.org/2002/07/owl#>
                             PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
                             PREFIX xsd: <http://www.w3.org/2001/XMLSchema#>
                             PREFIX : <http://www.semanticweb.org/josiptunjic/ontologies/2023/0/untitled-ontology-5#>
                             SELECT DISTINCT ?subject ?object
	                         WHERE { ?subject rdfs:subClassOf ?object. ?object rdf:type owl:Class. }";


            // Ausführen der Abfrage vom Daten Graphen
            try
            {
                var queryResult = graph.ExecuteQuery(query);

                // Auswerten des Ergebnisses
                if (queryResult is SparqlResultSet)
                {
                    SparqlResultSet resultSet = (SparqlResultSet)queryResult;

                    foreach (SparqlResult result in resultSet)
                    {
                        Console.WriteLine(result.ToString() + "\n");
                    }
                }
            }
            catch
            {
                throw new RdfParseException("An error occurred while parsing the query result!");
            }

            Console.ReadKey();
        }
    }
}

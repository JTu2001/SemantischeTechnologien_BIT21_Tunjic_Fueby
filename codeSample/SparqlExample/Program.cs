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

            // Erstellen des Graphen aus einem String (alternativ kann auch aus einer Datei gelesen werden)
            string data = "@prefix foaf: <http://xmlns.com/foaf/0.1/> . \n"
                        + "@prefix rdfs: <http://www.w3.org/2000/01/rdf-schema#> .\n\n"
                        + "<http://example.org/Michael#me> a foaf:Person .\n"
                        + "<http://example.org/Michael#me> foaf:name \"Michael\" .\n"
                        + "<http://example.org/Michael#me> foaf:mbox <mailto:alice@example.org> .\n"
                        + "<http://example.org/Michael#me> foaf:knows <http://example.org/bob#me> .\n"
                        + "<http://example.org/Markus#me> foaf:knows <http://example.org/alice#me> .\n"
                        + "<http://example.org/Markus#me> foaf:name \"Markus\" .\n"
                        + "<http://example.org/Michael#me> foaf:knows <http://example.org/charlie#me> .\n"
                        + "<http://example.org/Josip#me> foaf:knows <http://example.org/alice#me> .\n"
                        + "<http://example.org/Josip#me> foaf:name \"Josip\" .\n"
                        + "<http://example.org/Michael#me> foaf:knows <http://example.org/snoopy> .\n"
                        + "<http://example.org/Manuel> foaf:name \"Manuel\"@en .";

            // Parsen des Strings in den Graphen.
            StringParser.Parse(graph, data);

            // Erstellen des SPARQL Ausdrucks als String.
            string query = @"PREFIX foaf: <http://xmlns.com/foaf/0.1/>
                            SELECT ?name(COUNT(?friend) AS ?count)
                            WHERE {
                                ?person foaf:name ?name .
                                ?person foaf:knows ?friend .
                            }
                            GROUP BY ?person ?name";


            // Ausführen der Abfrage vom Graphen
            var queryResult = graph.ExecuteQuery(query);

            // Auswerten des Ergebnisses (Typ des Results muss überprüft werden)
            if (queryResult is SparqlResultSet)
            {
                SparqlResultSet resultSet = (SparqlResultSet)queryResult;

                foreach (SparqlResult result in resultSet)
                {
                    Console.WriteLine(result.ToString());
                }
            }

            Console.ReadKey();
        }
    }
}

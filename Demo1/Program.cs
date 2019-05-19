using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Demo1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //SelectWithRawSqlProvider();
            //SelectAllCitiesWithDapper();
            SelectCitiesByStateWithDapper(2);
            Console.ReadKey();
        }

        private static void SelectCitiesByStateWithDapper(int stateId)
        {
            // Parameterized queries
            string query = "select * from Cidade C JOIN Estado E on C.EstadoId = E.Id where E.Id = @stateId ORDER by E.Sigla, C.Nome";
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                var cidades = con.Query<Cidade, Estado, Cidade>(query, 
                    (cid, est) => { cid.Estado = est; return cid; }, 
                    new { stateId = stateId });

                foreach (var cidade in cidades)
                {
                    Console.WriteLine("{0}", cidade);
                }
            }
        }

        private static void SelectAllCitiesWithDapper()
        {
            //Multi Mapping
            string query = "select * from Cidade C JOIN Estado E on C.EstadoId = E.Id ORDER by E.Sigla, C.Nome";
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                var cidades = con.Query<Cidade, Estado, Cidade>(query, (cid, est) => {
                    cid.Estado = est;
                    return cid;
                });

                foreach (var cidade in cidades)
                {
                    Console.WriteLine("{0}", cidade);
                }
            }
        }

        private static void SelectWithRawSqlProvider()
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("select E.Sigla, E.Nome [NomeEstado], C.Nome [NomeCidade], C.Tipo from Estado E JOIN Cidade C on E.Id = C.EstadoId ORDER BY E.Sigla, C.Nome", con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine("{0}|{1}|{2}|{3}",
                        rdr["Sigla"],
                        rdr["NomeEstado"],
                        rdr["NomeCidade"],
                        rdr["Tipo"]
                        );
                }
            }
        }

        private static string GetConnectionString()
        {
            return "Data Source=.\\SQLEXPRESS2014;Initial Catalog=Sample02; Integrated Security=SSPI;";
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Symphonya_RedeSocial.Models
{
    public class Show {
        public Int32 ID { get; set; }
        public Int32 AgendaID { get; set; }
        public String Hora { get; set; }
        public String Data { get; set; }
        public String Titulo { get; set; }
        public String Descricao { get; set; }

        public Show()
        {
        }

        public Show(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "SELECT * FROM Show WHERE ID=@ID;";
            Comando.Parameters.AddWithValue("@ID", ID);

            SqlDataReader Leitor = Comando.ExecuteReader();

            Leitor.Read();

            this.ID = (Int32)Leitor["ID"];
            this.AgendaID = (Int32)Leitor["AgendaID"];
            this.Titulo = (String)Leitor["Titulo"];
            this.Hora = (String)Leitor["Hora"];
            this.Data = (String)Leitor["Data"];
            this.Descricao = (String)Leitor["Descricao"];

            Conexao.Close();
        }

        public Boolean NovoEvento()
        {

            SqlConnection Conexao = new SqlConnection("Server=ESN509VMSSQL;Database=Symphonya;User Id=Aluno;Password=Senai1234;");
            Conexao.Open();

            //CRIACAO DO COMANDO SQL
            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "INSERT INTO Show (Hora ,Data,Titulo,Descricao)"
              + "VALUES (@Hora,@Data,@Titulo,@Descricao);";

            DateTime datahora = DateTime.Now;
            String Hora = datahora.Day + "/" + datahora.Month + "/" + datahora.Year;
            String Data = datahora.Day + "/" + datahora.Month + "/" + datahora.Year;

            Comando.Parameters.AddWithValue("@Hora", this.Hora);
            Comando.Parameters.AddWithValue("@Data", this.Data);
            Comando.Parameters.AddWithValue("@Titulo", this.Titulo);
            Comando.Parameters.AddWithValue("@Descricao", this.Descricao);

            Int32 Resultado = Comando.ExecuteNonQuery();

            Conexao.Close();

            return Resultado > 0 ? true : false;
        }
    }
}
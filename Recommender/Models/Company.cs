using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Recommender.Models
{
    public class Company
    {
        public Company() { }

        public Company(TMDbLib.Objects.Companies.Company company)
        {
            Description = company.Description;
            Headquarters = company.Headquarters;
            Homepage = company.Homepage;
            Id = company.Id;
            LogoPath = company.LogoPath;
            Name = company.Name;
            ParentCompanyId = company.ParentCompany?.Id ?? -1;
        }

        [JsonProperty("description")]
        [Column("description")]
        public string Description { get; set; }

        [JsonProperty("headquarters")]
        [Column("headquarters")]
        public string Headquarters { get; set; }

        [JsonProperty("homepage")]
        [Column("homepage")]
        public string Homepage { get; set; }

        [JsonProperty("id")]
        [Key, Column("id")]
        public int Id { get; set; }

        [JsonProperty("logo_path")]
        [Column("logo_path")]
        public string LogoPath { get; set; }

        [JsonProperty("name")]
        [Column("name")]
        public string Name { get; set; }

        [JsonProperty("parent_company_id")]
        [Column("parent_company_id"), ForeignKey("Companies")]
        public int ParentCompanyId { get; set; }
    }
}

using Data_Access_layer.DTO;
using Data_Access_layer.Repository;
using Data_Access_layer.Repository.Entities;
using Data_Access_layer.Repository.Models;
using Data_Access_layer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Buisness_Logic_Layer.Managers
{
    public class IssueBLL
    {
        private readonly Data_Access_layer.Services.IssueDAL _DAL;
        private readonly IConfiguration _configuration;

        public IssueBLL(Data_Access_layer.Services.IssueDAL DAL,
            IConfiguration configuration)
        {
            _DAL = DAL;
            _configuration = configuration;
        }
        //Gets All the Data
        public async Task<IEnumerable<Issue>> Get()
        {
            return await _DAL.Get();

        }
        //get data by id
        public async Task<Issue> GetById(int id)
        {
            var data = await _DAL.GetById(id);
            return data;
        }
        // returns boolean if the Post is created or not
        public async Task<bool> Post(Issue issue)
        {
            issue.Created = DateTime.UtcNow;
            if (await _DAL.Post(issue) == null)
            {
                return false;
            }
            return true;
        }

        // returns a boolean if the Post is updated or not
        public async Task<bool> Update(int id, Issue issue)
        {
            await _DAL.Update(id, issue);
            return true;
        }

        //Delete by Id
        public async Task<bool> Delete(int id)
        {
            return await _DAL.Delete(id);
        }

        //-----------------------------------------------------------Users------------------------------------------------------------------------//

       
        //--------------------------------------------------------------------FileController----------------------------------------------//
        //accepting a file
        public async Task<string> FilePost(IFormFile file)
        {
            return await _DAL.FilePost(file);
        }

    }
    }
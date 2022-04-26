using Data_Access_layer.DTO;
using Data_Access_layer.Interfaces;
using Data_Access_layer.Repository;
using Data_Access_layer.Repository.Entities;
using Data_Access_layer.Repository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Data_Access_layer.Services
{
    public class IssueDAL : IIssueDAL
    {
        private readonly IssueDbContext _context;
        private readonly IConfiguration _configuration;

        public IssueDAL(IssueDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // get data
        public async Task<IEnumerable<Issue>> Get()
        {
            return await _context.Issues.ToListAsync();
        }
        //get data by ID
        public async Task<Issue> GetById(int id)
        {
            var issues = await _context.Issues.FindAsync(id);
            return issues;
        }
        
        //Post Data 
        public async Task<bool> Post(Issue issue)
        {
            await _context.Issues.AddAsync(issue);
            var inserted = await _context.SaveChangesAsync();
            return inserted == 1 ? true : false;
        }
        
        //Update data By Id
        public async Task<bool> Update(int id, Issue issue)
        {
            if (id != issue.Id) return false;

            _context.Entry(issue).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }
        
        //Delete Data by id
        public async Task<bool> Delete(int id)
        {
            var issueToDelete = await _context.Issues.FindAsync(id);
            if (issueToDelete == null) { return false; }

            _context.Issues.Remove(issueToDelete);
            await _context.SaveChangesAsync();

            return true;
        }    

        //-----------------------------------------------FileType----------------------------------------------//
        //Taking input file
        public async Task<string> FilePost(IFormFile file)
        {
            if (file.Length <= 0)
                return "Empty file";

            //Strip out any path specifiers (ex: /../)
            var originalFileName = Path.GetFileName(file.FileName);

            //Create a unique file path
            var uniqueFilePath = Path.Combine(@"C:\Users\Anuj\source\repos\API2\Data_Access_layer\FILES", originalFileName);

            //Save the file to disk
            using (var stream = System.IO.File.Create(uniqueFilePath))
            {
                await file.CopyToAsync(stream);
            }

            return  $"Saved file {originalFileName} with size {file.Length / 1024m:#.00} KB using unique name {originalFileName}";
            //C:\Users\Anuj\source\repos\API2\Data_Access_layer
        }

    }
   }

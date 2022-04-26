using Data_Access_layer.Repository.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_layer.Interfaces
{
    public interface IIssueDAL
    {
         Task<IEnumerable<Issue>> Get();
        //get data by ID
         Task<Issue> GetById(int id);
         Task<bool> Post(Issue issue);

         Task<bool> Update(int id, Issue issue);

         Task<bool> Delete(int id);

        //-----------------------------------------------FileType----------------------------------------------//
         Task<string> FilePost(IFormFile file);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Models.Repositories
{
    public interface IArticleRepository  : IDisposable
    {
        IEnumerable<Article> GetArticles();
        Article GetArticleByID(int articleID);
        void InsertArticle(Article article);
        void DeleteArticle(int articleID);
        void UpdateArticle(Article article);
        void Save();
    }
}
